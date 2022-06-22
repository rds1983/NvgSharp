using System;
using System.Collections.Generic;
using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
{
	internal class Renderer : IRenderer
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
			TwoSidedStencilMode = true,
			StencilWriteMask = 0xff,
			ReferenceStencil = 0,
			StencilMask = 0xff,
			StencilFunction = CompareFunction.Equal,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilPass = StencilOperation.Keep,
			CounterClockwiseStencilFunction = CompareFunction.Always,
			CounterClockwiseStencilFail = StencilOperation.Keep,
			CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep,
			CounterClockwiseStencilPass = StencilOperation.Decrement
		};

		private readonly DepthStencilState _stencilStateFill3 = new DepthStencilState
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
		private readonly EffectParameter _extentParam;
		private readonly EffectParameter _radiusParam;
		private readonly EffectParameter _featherParam;
		private readonly EffectParameter _innerColParam;
		private readonly EffectParameter _outerColParam;
		private readonly EffectParameter _textureParam;
		private short[] _indexes = BuildTriangleFanIndexBuffer(2048 * 6);
		private BlendState _oldBlendState;
		private DepthStencilState _oldDepthStencilState;
		private RasterizerState _oldRasterizerState;
		private SamplerState _oldSamplerState;
		private readonly bool _edgeAntiAlias;
		private float _width, _height, _devicePx;

		private readonly EffectParameter _transformMatParam;
		private readonly EffectParameter _scissorMatParam;
		private readonly EffectParameter _scissorExtParam;
		private readonly EffectParameter _scissorScaleParam;
		private readonly EffectParameter _paintMatParam;
		private readonly EffectParameter _strokeMultParam, _strokeThrParam;

		public GraphicsDevice GraphicsDevice => _device;

		public Renderer(GraphicsDevice device, bool edgeAntiAlias)
		{
			if (device == null)
				throw new ArgumentNullException("device");

			_device = device;
			_edgeAntiAlias = edgeAntiAlias;
			_effect = new Effect(device, Resources.GetNvgEffectSource(edgeAntiAlias));

			_transformMatParam = _effect.Parameters["transformMat"];
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

			if (_edgeAntiAlias)
			{
				_strokeThrParam = _effect.Parameters["strokeThr"];
				_strokeMultParam = _effect.Parameters["strokeMult"];
			}

			foreach (RenderingType param in Enum.GetValues(typeof(RenderingType)))
				_techniques[(int)param] = _effect.Techniques[param.ToString()];
		}

		public void Viewport(float width, float height, float devicePixelRatio)
		{
			_width = width;
			_height = height;
			_devicePx = devicePixelRatio;
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

			var transform = Matrix.CreateOrthographicOffCenter(0, _width, _height, 0, 0, -1);
			_transformMatParam.SetValue(transform);
		}

		private static void BuildUniform(ref Paint paint, ref Scissor scissor, float width, 
			float fringe, float strokeThr, ref UniformInfo uniform)
		{
			uniform.innerCol = paint.InnerColor.ToColorInfo().MakePremultiplied();
			uniform.outerCol = paint.OuterColor.ToColorInfo().MakePremultiplied();

			if (scissor.Extent.X < -0.5f || scissor.Extent.Y < -0.5f)
			{
				uniform.scissorMat.MakeZero();
				uniform.scissorExt.X = 1.0f;
				uniform.scissorExt.Y = 1.0f;
				uniform.scissorScale.X = 1.0f;
				uniform.scissorScale.Y = 1.0f;
			}
			else
			{
				uniform.scissorMat = scissor.Transform.BuildInverse().ToMatrix();
				uniform.scissorExt.X = scissor.Extent.X;
				uniform.scissorExt.Y = scissor.Extent.Y;
				uniform.scissorScale.X = (float)Math.Sqrt(scissor.Transform.T1 * scissor.Transform.T1 + scissor.Transform.T3 * scissor.Transform.T3) / fringe;
				uniform.scissorScale.Y = (float)Math.Sqrt(scissor.Transform.T2 * scissor.Transform.T2 + scissor.Transform.T4 * scissor.Transform.T4) / fringe;
			}

			uniform.extent = paint.Extent;
			uniform.strokeMult = (width * 0.5f + fringe * 0.5f) / fringe;
			uniform.strokeThr = strokeThr;

			if (paint.Image == null)
			{
				uniform.radius = paint.Radius;
				uniform.feather = paint.Feather;
			}

			uniform.paintMat = paint.Transform.BuildInverse().ToMatrix();
		}

		private void SetUniform(ref UniformInfo uniform, Texture2D texture)
		{
			_scissorMatParam.SetValue(uniform.scissorMat);
			_paintMatParam.SetValue(uniform.paintMat);
			_innerColParam.SetValue(uniform.innerCol.ToVector4());
			_outerColParam.SetValue(uniform.outerCol.ToVector4());
			_scissorExtParam.SetValue(uniform.scissorExt);
			_scissorScaleParam.SetValue(uniform.scissorScale);
			_extentParam.SetValue(uniform.extent);
			_radiusParam.SetValue(uniform.radius);
			_featherParam.SetValue(uniform.feather);
			_textureParam.SetValue(texture);

			if (_edgeAntiAlias)
			{
				_strokeMultParam.SetValue(uniform.strokeMult);
				_strokeThrParam.SetValue(uniform.strokeThr);
			}
		}

		private void DrawVertexes(RenderingType renderingType, ArraySegment<Vertex> verts, PrimitiveType primitiveType, bool indexed)
		{
			var technique = _techniques[(int)renderingType];
			_effect.CurrentTechnique = technique;
			foreach (var pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();

				if (indexed)
				{
					var primitiveCount = (verts.Count - 2);
					_device.DrawUserIndexedPrimitives(primitiveType, verts.Array, verts.Offset, verts.Count,
						_indexes, 0, primitiveCount);
				}
				else
				{
					var primitiveCount =
						primitiveType == PrimitiveType.TriangleList ? verts.Count / 3 : verts.Count - 2;
					_device.DrawUserPrimitives(primitiveType, verts.Array, verts.Offset, primitiveCount);
				}
			}
		}

		private ArrayBuffer<Vertex> _tempVerts = new ArrayBuffer<Vertex>(4);

		public void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			if (paths.Count == 1 && paths.Array[paths.Offset].Convex)
			{
				// Convex Fill
				var uniformInfo = new UniformInfo();
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref uniformInfo);
				SetUniform(ref uniformInfo, paint.Image);

				var renderingType = paint.Image != null ? RenderingType.FillImage : RenderingType.FillGradient;
				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths.Array[paths.Offset + i];

					if (path.Fill != null)
					{
						DrawVertexes(renderingType, path.Fill.Value, PrimitiveType.TriangleList, true);
					}

					if (path.Stroke != null)
					{
						DrawVertexes(renderingType, path.Stroke.Value, PrimitiveType.TriangleStrip, false);
					}
				}
			}
			else
			{
				// Setup uniforms for draw calls
				var uniformInfo = new UniformInfo
				{
					strokeThr = -1.0f,
				};

				var uniformInfo2 = new UniformInfo();

				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref uniformInfo2);

				_device.BlendState = _blendStateNoDraw;
				_device.DepthStencilState = _stencilStateFill1;
				var renderingType = RenderingType.Simple;

				SetUniform(ref uniformInfo, null);

				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths.Array[paths.Offset + i];

					if (path.Fill != null)
					{
						DrawVertexes(renderingType, path.Fill.Value, PrimitiveType.TriangleList, true);
					}
				}

				// Draw anti-aliased pixels
				_device.BlendState = BlendState.AlphaBlend;

				SetUniform(ref uniformInfo2, paint.Image);

				if (_edgeAntiAlias)
				{
					_device.DepthStencilState = _stencilStateFill2;
					renderingType = paint.Image != null ? RenderingType.FillImage : RenderingType.FillGradient;

					// Draw fringes
					for (var i = 0; i < paths.Count; i++)
					{
						var path = paths.Array[paths.Offset + i];

						if (path.Stroke != null)
						{
							DrawVertexes(renderingType, path.Stroke.Value, PrimitiveType.TriangleStrip, false);
						}
					}
				}

				_device.DepthStencilState = _stencilStateFill3;

				_tempVerts.Clear();
				_tempVerts.Add(new Vertex(bounds.X2, bounds.Y2, 0.5f, 1.0f));
				_tempVerts.Add(new Vertex(bounds.X2, bounds.Y, 0.5f, 1.0f));
				_tempVerts.Add(new Vertex(bounds.X, bounds.Y2, 0.5f, 1.0f));
				_tempVerts.Add(new Vertex(bounds.X, bounds.Y, 0.5f, 1.0f));

				renderingType = RenderingType.FillGradient;
				DrawVertexes(renderingType, _tempVerts.ToArraySegment(), PrimitiveType.TriangleStrip, false);

				_device.DepthStencilState = DepthStencilState.None;
			}
		}

		public void RenderStroke(ref Paint paint, ref Scissor scissor,
			float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			var uniformInfo = new UniformInfo();

			BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref uniformInfo);
			SetUniform(ref uniformInfo, paint.Image);
			var renderingType = paint.Image != null ? RenderingType.FillImage : RenderingType.FillGradient;

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.Stroke != null)
				{
					DrawVertexes(renderingType, path.Stroke.Value, PrimitiveType.TriangleStrip, false);
				}
			}
		}

		public void RenderTriangles(ref Paint paint, ref Scissor scissor, float fringe, ArraySegment<Vertex> verts)
		{
			var uniformInfo = new UniformInfo();
			BuildUniform(ref paint, ref scissor, 1.0f, fringe, -1.0f, ref uniformInfo);
			SetUniform(ref uniformInfo, paint.Image);
			var renderingType = RenderingType.Triangles;

			DrawVertexes(renderingType, verts, PrimitiveType.TriangleList, false);
		}

		public void End()
		{
			_device.SamplerStates[0] = _oldSamplerState;
			_device.BlendState = _oldBlendState;
			_device.DepthStencilState = _oldDepthStencilState;
			_device.RasterizerState = _oldRasterizerState;
		}

		private static short[] BuildTriangleFanIndexBuffer(int indexesCount)
		{
			var result = new List<short>();
			for (var j = 2; j < indexesCount; ++j)
			{
				result.Add(0);
				result.Add((short)(j - 1));
				result.Add((short)j);
			}

			return result.ToArray();
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