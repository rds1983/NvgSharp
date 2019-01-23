using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NanoVGSharp
{
	public class XNARenderer : IRenderer
	{
		private readonly GraphicsDevice _device;
		private DynamicVertexBuffer _vertexBuffer;
		private IndexBuffer _indexBufferFill;
		private readonly Effect _effect;
		private int _indexesCount = 0;
		private readonly List<Texture2D> _textures = new List<Texture2D>();

		private readonly BlendState _blendStateNoDraw = new BlendState
		{
			ColorWriteChannels = ColorWriteChannels.None
		};

		private readonly DepthStencilState _stencilStateFill1 = new DepthStencilState
		{
			StencilEnable = true,
			TwoSidedStencilMode = true,
			StencilWriteMask = 0xff,
			ReferenceStencil = 0,
			StencilMask = 0xff,
			StencilFunction = CompareFunction.Always,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilPass = StencilOperation.Increment,
			CounterClockwiseStencilFunction = CompareFunction.Always,
			CounterClockwiseStencilFail = StencilOperation.Keep,
			CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep,
			CounterClockwiseStencilPass = StencilOperation.Decrement
		};

		private readonly DepthStencilState _stencilStateFill2 = new DepthStencilState
		{
			StencilEnable = true,
			TwoSidedStencilMode = false,
			StencilWriteMask = 0xff,
			ReferenceStencil = 0,
			StencilMask = 0xff,
			StencilFunction = CompareFunction.NotEqual,
			StencilFail = StencilOperation.Zero,
			StencilDepthBufferFail = StencilOperation.Zero,
			StencilPass = StencilOperation.Zero
		};

		private SamplerState _oldSamplerState;
		private BlendState _oldBlendState;
		private RasterizerState _oldRasterizerState;
		private DepthStencilState _oldDepthStencilState;
		private readonly Buffer<Vertex> _vertexes = new Buffer<Vertex>(1024);
		private Transform _scissorTransform;
		private Vector2 _scissorExt, _scissorScale;
		float _strokeMult;

		private enum RenderingType
		{
			FillGradient,
			FillImage,
			Simple,
			Triangles
		}

		private EffectParameter _viewSizeParam, _scissorMatParam, _scissorExtParam, _scissorScaleParam, _paintMatParam;
		private EffectParameter _extentParam, _radiusParam, _featherParam, _innerColParam, _outerColParam, _textureParam;
		private readonly EffectTechnique[] _techniques = new EffectTechnique[4];

		public XNARenderer(GraphicsDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}

			_device = device;

			_vertexBuffer = new DynamicVertexBuffer(device, Vertex.VertexDeclaration, 2000, BufferUsage.WriteOnly);

			_effect = new Effect(device, Resources.NvgEffectSource);

			_viewSizeParam = _effect.Parameters["viewSize"];
			_scissorMatParam = _effect.Parameters["scissorMat"];
			_scissorExtParam = _effect.Parameters["scissorExt"];
			_scissorScaleParam = _effect.Parameters["scissorScale"];
			_paintMatParam = _effect.Parameters["paintMat"];
			_extentParam = _effect.Parameters["extent"];
			_radiusParam = _effect.Parameters["radius"];
			_featherParam = _effect.Parameters["feather"];
			_innerColParam = _effect.Parameters["innerCol"];
			_outerColParam = _effect.Parameters["outerCol"];
			_textureParam = _effect.Parameters["g_texture"];

			foreach (RenderingType param in Enum.GetValues(typeof(RenderingType)))
			{
				_techniques[(int)param] = _effect.Techniques[param.ToString()];
			}
		}

		private Texture2D GetTextureById(int id)
		{
			return _textures[id - 1];
		}

		public int renderCreateTexture(int type, int w, int h, int imageFlags, byte[] d)
		{
			var texture = new Texture2D(_device, w, h);

			if (d != null)
			{
				texture.SetData(d);
			}

			_textures.Add(texture);

			return _textures.Count;
		}

		public void renderDeleteTexture(int image)
		{
			throw new NotImplementedException();
		}

		public void renderUpdateTexture(int image, int x, int y, int w, int h, byte[] d)
		{
			var texture = GetTextureById(image);

			if (d != null)
			{
				var sz = w * h;
				var b = new byte[texture.Width * texture.Height * 4];

				texture.GetData<byte>(b);
				for (var xx = x; xx < x + w; ++xx)
				{
					for (var yy = y; yy < y + h; ++yy)
					{
						var destPos = yy * texture.Width + xx;

						for (var k = 0; k < 3; ++k)
						{
							b[destPos * 4 + k] = 255;
						}

						b[destPos * 4 + 3] = d[destPos];
					}
				}

				texture.SetData(b);
			}
		}

		public void renderGetTextureSize(int image, out int w, out int h)
		{
			var texture = GetTextureById(image);

			w = texture.Width;
			h = texture.Height;
		}

		public void renderViewport(float width, float height, float devicePixelRatio)
		{
			// TO DO
		}

		private Color premultiplyColor(Color src)
		{
			var f = (src.A / 255.0f);

			return new Color((int)(src.R * f),
				(int)(src.G * f),
				(int)(src.B * f),
				src.A);
		}

		private void renderTriangles(ref Paint paint,
			CompositeOperationState compositeOperation,
			ref Scissor scissor,
			float width, float fringe, float strokeThr,
			RenderingType renderingType,
			ArraySegment<Vertex> verts,
			PrimitiveType primitiveType,
			bool indexed)
		{
			if (verts.Count <= 0 || _indexesCount <= 0)
			{
				return;
			}

			var innerColor = premultiplyColor(paint.innerColor);
			var outerColor = premultiplyColor(paint.outerColor);

			_strokeMult = (width * 0.5f + fringe * 0.5f) / fringe;

			if (scissor.extent1 < -0.5f || scissor.extent2 < -0.5f)
			{
				_scissorTransform.Zero();

				_scissorExt.X = 1.0f;
				_scissorExt.Y = 1.0f;
				_scissorScale.X = 1.0f;
				_scissorScale.Y = 1.0f;
			}
			else
			{
				_scissorTransform = scissor.xform.BuildInverse();
				_scissorExt.X = scissor.extent1;
				_scissorExt.Y = scissor.extent2;
				_scissorScale.X = (float)Math.Sqrt(scissor.xform.t1 * scissor.xform.t1 + scissor.xform.t3 * scissor.xform.t3) / fringe;
				_scissorScale.Y = (float)Math.Sqrt(scissor.xform.t2 * scissor.xform.t2 + scissor.xform.t4 * scissor.xform.t4) / fringe;
			}

			var transform = paint.xform.BuildInverse();

			if (_vertexBuffer.VertexCount < verts.Count)
			{
				// Resize vertex ArraySegment if data doesnt fit
				_vertexBuffer = new DynamicVertexBuffer(_device, Vertex.VertexDeclaration, verts.Count * 2,
					BufferUsage.WriteOnly);
			}
			_vertexBuffer.SetData(verts.Array, 0, verts.Count);

			var transformMatrix = transform.ToMatrix();

			_viewSizeParam.SetValue(new Vector2(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height));
			_scissorMatParam.SetValue(_scissorTransform.ToMatrix());
			_scissorExtParam.SetValue(_scissorExt);
			_scissorScaleParam.SetValue(_scissorScale);
			_paintMatParam.SetValue(transformMatrix);
			_extentParam.SetValue(new Vector4(paint.extent1, paint.extent2, 0.0f, 0.0f));
			_radiusParam.SetValue(new Vector4(paint.radius, 0.0f, 0.0f, 0.0f));
			_featherParam.SetValue(new Vector4(paint.feather, 0.0f, 0.0f, 0.0f));
			_innerColParam.SetValue(innerColor.ToVector4());
			_outerColParam.SetValue(outerColor.ToVector4());
			// _effect.Parameters["strokeMult"].SetValue(new Vector4(_strokeMult, 0.0f, 0.0f, 0.0f));

			if (paint.image > 0)
			{
				var texture = GetTextureById(paint.image);
				_textureParam.SetValue(texture);
			}

			var technique = _techniques[(int)renderingType];
			_effect.CurrentTechnique = technique;
			foreach (var pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();

				if (indexed)
				{
					_device.DrawIndexedPrimitives(primitiveType, 0, 0, verts.Count, 0, _indexesCount / 3);
				}
				else
				{
					int count = primitiveType == PrimitiveType.TriangleList ? verts.Count / 3 : (verts.Count - 2);
					_device.DrawPrimitives(primitiveType, 0, count);
				}
			}

			_vertexes.Clear();
		}

		private void SetIndexBuffer(ref IndexBuffer indexBuffer, int vertexesCount, int indexesCount, Func<int, ushort[]> indexGenerator)
		{
			if (indexBuffer == null || indexBuffer.IndexCount < indexesCount)
			{
				// Reallocate index buffer
				indexBuffer = new DynamicIndexBuffer(_device, typeof(ushort), indexesCount, BufferUsage.WriteOnly);
				var indexes = indexGenerator(vertexesCount);
				indexBuffer.SetData<ushort>(indexes);
			}

			_device.Indices = indexBuffer;
			_indexesCount = indexesCount;
		}

		private void SetIndexBufferFill(int vertexesCount, int indexesCount)
		{
			SetIndexBuffer(ref _indexBufferFill, vertexesCount, indexesCount, i =>
			{
				var result = new List<ushort>();
				for (var j = 2; j < i; ++j)
				{
					result.Add(0);
					result.Add((ushort)(j - 1));
					result.Add((ushort)(j));
				}

				return result.ToArray();
			});
		}

		public void renderFill(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor,
			float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			var isConvex = paths.Count == 1 && paths.Array[paths.Offset].convex == 1;

			RenderingType renderingType;
			if (isConvex)
			{
				renderingType = paint.image != 0 ? RenderingType.FillImage : RenderingType.FillGradient;
			}
			else
			{
				_device.BlendState = _blendStateNoDraw;
				_device.DepthStencilState = _stencilStateFill1;
				renderingType = RenderingType.Simple;
			}

			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.fill != null)
				{
					var fill = path.fill.Value;
					for (var j = 0; j < fill.Count; ++j)
					{
						_vertexes.Add(fill.Array[fill.Offset + j]);
					}

					SetIndexBufferFill(fill.Count, (fill.Count - 2) * 3);
					renderTriangles(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, 
						renderingType, _vertexes.ToArraySegment(), PrimitiveType.TriangleList, true);
				}
			}

			if (!isConvex)
			{
				_device.BlendState = BlendState.AlphaBlend;
				_device.DepthStencilState = _stencilStateFill2;

				_vertexes.Add(new Vertex(bounds.b1, bounds.b2, 0.5f, 1.0f));
				_vertexes.Add(new Vertex(bounds.b3, bounds.b2, 0.5f, 1.0f));
				_vertexes.Add(new Vertex(bounds.b1, bounds.b4, 0.5f, 1.0f));
				_vertexes.Add(new Vertex(bounds.b3, bounds.b4, 0.5f, 1.0f));

				renderTriangles(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, 
					RenderingType.FillGradient, _vertexes.ToArraySegment(), PrimitiveType.TriangleStrip, false);

				_device.DepthStencilState = DepthStencilState.None;
			}
		}

		public void renderStroke(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor,
			float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.stroke != null)
				{
					var stroke = path.stroke.Value;

					for (var j = 0; j < stroke.Count; ++j)
					{
						_vertexes.Add(stroke.Array[stroke.Offset + j]);
					}

					renderTriangles(ref paint, compositeOperation, ref scissor, 
						strokeWidth, fringe, -1.0f, 
						paint.image != 0? RenderingType.FillImage:RenderingType.FillGradient,
						_vertexes.ToArraySegment(),
						PrimitiveType.TriangleStrip,
						false);
				}
			}
		}

		public void renderTriangles(ref Paint paint, CompositeOperationState compositeOperation,
			ref Scissor scissor, ArraySegment<Vertex> verts)
		{
			if (verts.Count <= 0)
			{
				return;
			}

			renderTriangles(ref paint, compositeOperation, ref scissor,
				1.0f, 1.0f, -1.0f,
				RenderingType.Triangles,
				verts,
				PrimitiveType.TriangleList,
				false);
		}

		public void Begin()
		{
			_device.SetVertexBuffer(_vertexBuffer);

			_oldSamplerState = _device.SamplerStates[0];
			_oldBlendState = _device.BlendState;
			_oldDepthStencilState = _device.DepthStencilState;
			_oldRasterizerState = _device.RasterizerState;

			_device.BlendState = BlendState.AlphaBlend;
			_device.DepthStencilState = DepthStencilState.None;
			_device.RasterizerState = RasterizerState.CullNone;
			_device.SamplerStates[0] = SamplerState.PointClamp;
		}

		public void End()
		{
			_device.SamplerStates[0] = _oldSamplerState;
			_device.BlendState = _oldBlendState;
			_device.DepthStencilState = _oldDepthStencilState;
			_device.RasterizerState = _oldRasterizerState;
		}
	}
}