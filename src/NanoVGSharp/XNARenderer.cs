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
		private IndexBuffer _indexBufferFill, _indexBufferSimple, _indexBufferTriangles;
		private Effect _effect;
		private int _indexesCount = 0;
		public readonly List<Texture2D> _textures = new List<Texture2D>();
		private readonly RasterizerState _rasterizerState = new RasterizerState
		{
			CullMode = CullMode.None,
			ScissorTestEnable = true
		};

		private readonly BlendState _blendStateNoDraw = new BlendState
		{
			ColorWriteChannels = ColorWriteChannels.None
		};

		private readonly DepthStencilState _stencilStateFill1 = new DepthStencilState
		{
			StencilEnable = true,
			TwoSidedStencilMode = true,
			StencilWriteMask = 0xff,
			StencilFunction = CompareFunction.Always,
			CounterClockwiseStencilFunction = CompareFunction.Always,
			ReferenceStencil = 0,
			StencilMask = 0xff,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilPass = StencilOperation.Increment,
			CounterClockwiseStencilFail = StencilOperation.Keep,
			CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep,
			CounterClockwiseStencilPass = StencilOperation.Decrement
		};

		private readonly DepthStencilState _stencilStateFill2 = new DepthStencilState
		{
			StencilEnable = true,
			TwoSidedStencilMode = false,
			StencilWriteMask = 0xff,
			StencilFunction = CompareFunction.NotEqual,
			ReferenceStencil = 0,
			StencilMask = 0xff,
			StencilFail = StencilOperation.Zero,
			StencilDepthBufferFail = StencilOperation.Zero,
			StencilPass = StencilOperation.Zero
		};

		private BlendState _oldBlendState;
		private RasterizerState _oldRasterizerState;
		private SamplerState _oldSamplerState;
		private DepthStencilState _oldDepthStencilState;
		private readonly Buffer<Vertex> _vertexes = new Buffer<Vertex>(1024);
		Transform _scissorTransform;
		Vector2 _scissorExt, _scissorScale;
		float _strokeMult;

		public bool AntiAliasingOn
		{
			get; set;
		}

		private enum RenderingType
		{
			FillGradient,
			FillImage,
			Simple,
			Triangles
		}

		public XNARenderer(GraphicsDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}

			_device = device;

			_vertexBuffer = new DynamicVertexBuffer(device, Vertex.VertexDeclaration, 2000, BufferUsage.WriteOnly);

			_effect = new Effect(device, Resources.NvgEffectSource);

			AntiAliasingOn = true;
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
			ArraySegment<Vertex> verts)
		{
			if (verts.Count <= 0)
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

			if (_vertexBuffer.VertexCount < _vertexes.Count)
			{
				// Resize vertex ArraySegment if data doesnt fit
				_vertexBuffer = new DynamicVertexBuffer(_device, Vertex.VertexDeclaration, verts.Count * 2,
					BufferUsage.WriteOnly);
			}
			_vertexBuffer.SetData(verts.Array, verts.Offset, verts.Count);

			var transformMatrix = transform.ToMatrix();

			_effect.Parameters["viewSize"].SetValue(new Vector2(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height));
			_effect.Parameters["scissorMat"].SetValue(_scissorTransform.ToMatrix());
			_effect.Parameters["scissorExt"].SetValue(_scissorExt);
			_effect.Parameters["scissorScale"].SetValue(_scissorScale);
			_effect.Parameters["paintMat"].SetValue(transformMatrix);
			_effect.Parameters["extent"].SetValue(new Vector4(paint.extent1, paint.extent2, 0.0f, 0.0f));
			_effect.Parameters["radius"].SetValue(new Vector4(paint.radius, 0.0f, 0.0f, 0.0f));
			_effect.Parameters["feather"].SetValue(new Vector4(paint.feather, 0.0f, 0.0f, 0.0f));
			_effect.Parameters["innerCol"].SetValue(innerColor.ToVector4());
			_effect.Parameters["outerCol"].SetValue(outerColor.ToVector4());
			_effect.Parameters["strokeMult"].SetValue(new Vector4(_strokeMult, 0.0f, 0.0f, 0.0f));

			if (paint.image > 0)
			{
				var texture = GetTextureById(paint.image);
				_effect.Parameters["g_texture"].SetValue(texture);
			}

			_effect.CurrentTechnique = _effect.Techniques[renderingType.ToString()];
			foreach (var pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, verts.Count, 0, _indexesCount / 3);
			}
		}

		private void DrawBuffers(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor,
			float width, float fringe, float strokeThr, RenderingType renderingType)
		{
			renderTriangles(ref paint, compositeOperation, ref scissor,
				width, fringe, strokeThr,
				renderingType,
				_vertexes.ToArraySegment());

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

		private void SetIndexBufferSimple(int vertexesCount, int indexesCount)
		{
			SetIndexBuffer(ref _indexBufferSimple, vertexesCount, indexesCount, i =>
			{
				var result = new List<ushort>();
				for (var j = 2; j < i; ++j)
				{
					result.Add((ushort)(j - 2));
					result.Add((ushort)(j - 1));
					result.Add((ushort)(j));
				}

				return result.ToArray();
			});
		}

		private void SetIndexBufferTriangles(int vertexesCount)
		{
			SetIndexBuffer(ref _indexBufferTriangles, vertexesCount, vertexesCount, i =>
			{
				var result = new List<ushort>();
				for (var j = 0; j < i; ++j)
				{
					result.Add((ushort)(j));
				}

				return result.ToArray();
			});
		}

		public void renderFill(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor,
			float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			var isConvex = paths.Count == 1 && paths.Array[paths.Offset].convex == 1;

			var renderingType = RenderingType.Simple;
			if (isConvex)
			{
				renderingType = paint.image != 0 ? RenderingType.FillImage : RenderingType.FillGradient;
			}
			else
			{
				_device.BlendState = _blendStateNoDraw;
				_device.DepthStencilState = _stencilStateFill1;
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
					DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, renderingType);
				}

				if (path.stroke != null && AntiAliasingOn)
				{
					var stroke = path.stroke.Value;

					for (var j = 0; j < stroke.Count; ++j)
					{
						_vertexes.Add(stroke.Array[stroke.Offset + j]);
					}

					SetIndexBufferFill(stroke.Count, (stroke.Count - 2) * 3);
					DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, renderingType);
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

				SetIndexBufferSimple(4, 6);
				DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, RenderingType.FillGradient);

				_device.DepthStencilState = DepthStencilState.None;
			}
		}

		public void renderStroke(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor,
			float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.stroke != null && AntiAliasingOn)
				{
					var stroke = path.stroke.Value;

					for (var j = 0; j < stroke.Count; ++j)
					{
						_vertexes.Add(stroke.Array[stroke.Offset + j]);
					}

					SetIndexBufferSimple(stroke.Count, (stroke.Count - 2) * 3);
					DrawBuffers(ref paint, compositeOperation, ref scissor, 
						strokeWidth, fringe, -1.0f, 
						paint.image != 0? RenderingType.FillImage:RenderingType.FillGradient);
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

			SetIndexBufferTriangles(verts.Count);

			renderTriangles(ref paint, compositeOperation, ref scissor,
				1.0f, 1.0f, -1.0f,
				RenderingType.Triangles,
				verts);
		}

		public void Begin()
		{
			_device.SetVertexBuffer(_vertexBuffer);

			_oldSamplerState = _device.SamplerStates[0];
			_oldBlendState = _device.BlendState;
			_oldDepthStencilState = _device.DepthStencilState;
			_oldRasterizerState = _device.RasterizerState;

			_device.SamplerStates[0] = SamplerState.LinearClamp;
			_device.BlendState = BlendState.AlphaBlend;
			_device.DepthStencilState = DepthStencilState.None;
			_device.RasterizerState = _rasterizerState;
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