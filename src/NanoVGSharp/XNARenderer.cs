using System;
using System.Collections.Generic;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NanoVGSharp
{
	internal class XNARenderer : IRenderer
	{
		private readonly BlendState _blendStateNoDraw = new BlendState
		{
			ColorWriteChannels = ColorWriteChannels.None
		};

		private readonly GraphicsDevice _device;
		private readonly Effect _effect;

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

		private readonly EffectTechnique[] _techniques = new EffectTechnique[4];
		private readonly List<Texture2D> _textures = new List<Texture2D>();
		private readonly Buffer<Vertex> _vertexes = new Buffer<Vertex>(1024);
		private readonly EffectParameter _extentParam;
		private readonly EffectParameter _radiusParam;
		private readonly EffectParameter _featherParam;
		private readonly EffectParameter _innerColParam;
		private readonly EffectParameter _outerColParam;
		private readonly EffectParameter _textureParam;
		private short[] _indexes;
		private int _indexesCount;
		private BlendState _oldBlendState;
		private DepthStencilState _oldDepthStencilState;
		private RasterizerState _oldRasterizerState;

		private SamplerState _oldSamplerState;
		private Vector2 _scissorExt, _scissorScale;
		private Transform _scissorTransform;
		private float _strokeMult;

		private readonly EffectParameter _viewSizeParam;
		private readonly EffectParameter _scissorMatParam;
		private readonly EffectParameter _scissorExtParam;
		private readonly EffectParameter _scissorScaleParam;
		private readonly EffectParameter _paintMatParam;

		public XNARenderer(GraphicsDevice device)
		{
			if (device == null)
				throw new ArgumentNullException("device");

			_device = device;

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
				_techniques[(int)param] = _effect.Techniques[param.ToString()];
		}

		public int CreateTexture(int type, int w, int h, int imageFlags, byte[] d)
		{
			var texture = new Texture2D(_device, w, h);

			if (d != null)
				texture.SetData(d);

			_textures.Add(texture);

			return _textures.Count;
		}

		public void DeleteTexture(int image)
		{
			throw new NotImplementedException();
		}

		public void UpdateTexture(int image, int x, int y, int w, int h, byte[] d)
		{
			var texture = GetTextureById(image);

			if (d != null)
			{
				var b = new byte[texture.Width * texture.Height * 4];

				texture.GetData(b);
				for (var xx = x; xx < x + w; ++xx)
					for (var yy = y; yy < y + h; ++yy)
					{
						var destPos = yy * texture.Width + xx;

						for (var k = 0; k < 3; ++k)
							b[destPos * 4 + k] = 255;

						b[destPos * 4 + 3] = d[destPos];
					}

				texture.SetData(b);
			}
		}

		public void GetTextureSize(int image, out int w, out int h)
		{
			var texture = GetTextureById(image);

			w = texture.Width;
			h = texture.Height;
		}

		public void Viewport(float width, float height, float devicePixelRatio)
		{
			// TO DO
		}

		public void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds,
			ArraySegment<Path> paths)
		{
			var isConvex = paths.Count == 1 && paths.Array[paths.Offset].Convex == 1;

			RenderingType renderingType;
			if (isConvex)
			{
				renderingType = paint.Image != 0 ? RenderingType.FillImage : RenderingType.FillGradient;
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

				if (path.Fill != null)
				{
					var fill = path.Fill.Value;
					for (var j = 0; j < fill.Count; ++j)
						_vertexes.Add(fill.Array[fill.Offset + j]);

					SetIndexBufferFill(fill.Count, (fill.Count - 2) * 3);
					RenderTriangles(ref paint, ref scissor, fringe, fringe, -1.0f,
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

				RenderTriangles(ref paint, ref scissor, fringe, fringe, -1.0f,
					RenderingType.FillGradient, _vertexes.ToArraySegment(), PrimitiveType.TriangleStrip, false);

				_device.DepthStencilState = DepthStencilState.None;
			}
		}

		public void RenderStroke(ref Paint paint, ref Scissor scissor,
			float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.Stroke != null)
				{
					var stroke = path.Stroke.Value;

					for (var j = 0; j < stroke.Count; ++j)
						_vertexes.Add(stroke.Array[stroke.Offset + j]);

					RenderTriangles(ref paint, ref scissor,
						strokeWidth, fringe, -1.0f,
						paint.Image != 0 ? RenderingType.FillImage : RenderingType.FillGradient,
						_vertexes.ToArraySegment(),
						PrimitiveType.TriangleStrip,
						false);
				}
			}
		}

		public void RenderTriangles(ref Paint paint, ref Scissor scissor, ArraySegment<Vertex> verts)
		{
			RenderTriangles(ref paint, ref scissor,
				1.0f, 1.0f, -1.0f,
				RenderingType.Triangles,
				verts,
				PrimitiveType.TriangleList,
				false);
		}

		public void Begin()
		{
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

		private Texture2D GetTextureById(int id)
		{
			return _textures[id - 1];
		}

		private Color premultiplyColor(Color src)
		{
			var f = src.A / 255.0f;

			return new Color((int)(src.R * f),
				(int)(src.G * f),
				(int)(src.B * f),
				src.A);
		}

		private void RenderTriangles(ref Paint paint,
			ref Scissor scissor,
			float width, float fringe, float strokeThr,
			RenderingType renderingType,
			ArraySegment<Vertex> verts,
			PrimitiveType primitiveType,
			bool indexed)
		{
			if (verts.Count <= 0 ||
				indexed && _indexesCount <= 0)
				return;

			var innerColor = premultiplyColor(paint.InnerColor);
			var outerColor = premultiplyColor(paint.OuterColor);

			_strokeMult = (width * 0.5f + fringe * 0.5f) / fringe;

			if (scissor.Extent.X < -0.5f || scissor.Extent.Y < -0.5f)
			{
				_scissorTransform.Zero();

				_scissorExt.X = 1.0f;
				_scissorExt.Y = 1.0f;
				_scissorScale.X = 1.0f;
				_scissorScale.Y = 1.0f;
			}
			else
			{
				_scissorTransform = scissor.Transform.BuildInverse();
				_scissorExt.X = scissor.Extent.X;
				_scissorExt.Y = scissor.Extent.Y;
				_scissorScale.X =
					(float)Math.Sqrt(scissor.Transform.T1 * scissor.Transform.T1 +
									  scissor.Transform.T3 * scissor.Transform.T3) / fringe;
				_scissorScale.Y =
					(float)Math.Sqrt(scissor.Transform.T2 * scissor.Transform.T2 +
									  scissor.Transform.T4 * scissor.Transform.T4) / fringe;
			}

			var transform = paint.Transform.BuildInverse();

			var transformMatrix = transform.ToMatrix();

			_viewSizeParam.SetValue(new Vector2(_device.PresentationParameters.Bounds.Width,
				_device.PresentationParameters.Bounds.Height));
			_scissorMatParam.SetValue(_scissorTransform.ToMatrix());
			_scissorExtParam.SetValue(_scissorExt);
			_scissorScaleParam.SetValue(_scissorScale);
			_paintMatParam.SetValue(transformMatrix);
			_extentParam.SetValue(new Vector4(paint.Extent.X, paint.Extent.Y, 0.0f, 0.0f));
			_radiusParam.SetValue(new Vector4(paint.Radius, 0.0f, 0.0f, 0.0f));
			_featherParam.SetValue(new Vector4(paint.Feather, 0.0f, 0.0f, 0.0f));
			_innerColParam.SetValue(innerColor.ToVector4());
			_outerColParam.SetValue(outerColor.ToVector4());
			// _effect.Parameters["strokeMult"].SetValue(new Vector4(_strokeMult, 0.0f, 0.0f, 0.0f));

			if (paint.Image > 0)
			{
				var texture = GetTextureById(paint.Image);
				_textureParam.SetValue(texture);
			}

			var technique = _techniques[(int)renderingType];
			_effect.CurrentTechnique = technique;
			foreach (var pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();

				if (indexed)
				{
					_device.DrawUserIndexedPrimitives(primitiveType, verts.Array, verts.Offset, verts.Count,
						_indexes, 0, _indexesCount / 3);
				}
				else
				{
					var primitiveCount =
						primitiveType == PrimitiveType.TriangleList ? verts.Count / 3 : verts.Count - 2;
					_device.DrawUserPrimitives(primitiveType, verts.Array, verts.Offset, primitiveCount);
				}
			}

			_vertexes.Clear();
		}

		private void SetIndexBufferFill(int vertexesCount, int indexesCount)
		{
			if (_indexes == null || _indexes.Length < indexesCount)
			{
				var result = new List<short>();
				for (var j = 2; j < indexesCount; ++j)
				{
					result.Add(0);
					result.Add((short)(j - 1));
					result.Add((short)j);
				}

				_indexes = result.ToArray();
			}

			_indexesCount = indexesCount;
		}

		private enum RenderingType
		{
			FillGradient,
			FillImage,
			Simple,
			Triangles
		}
	}
}