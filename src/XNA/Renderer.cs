using System;
using System.Collections.Generic;

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
			TwoSidedStencilMode = false,
			StencilWriteMask = 0xff,
			ReferenceStencil = 0,
			StencilMask = 0xff,
			StencilFunction = CompareFunction.Equal,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilPass = StencilOperation.Keep,
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
		private Vertex[] _vertexArray;

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

		public void Draw(Vector2 viewportSize, float devicePixelRatio, IEnumerable<CallInfo> calls, Vertex[] vertexes)
		{
			try
			{
				_oldSamplerState = _device.SamplerStates[0];
				_oldBlendState = _device.BlendState;
				_oldDepthStencilState = _device.DepthStencilState;
				_oldRasterizerState = _device.RasterizerState;

				_device.BlendState = BlendState.AlphaBlend;
				_device.DepthStencilState = DepthStencilState.None;
				_device.RasterizerState = RasterizerState.CullNone;
				_device.SamplerStates[0] = SamplerState.PointClamp;

				_vertexArray = vertexes;

				var transform = Matrix.CreateOrthographicOffCenter(0, viewportSize.X, viewportSize.Y, 0, 0, -1);
				_transformMatParam.SetValue(transform);

				foreach (var call in calls)
				{
					switch (call.Type)
					{
						case CallType.Fill:
							RenderFill(call);
							break;
						case CallType.ConvexFill:
							RenderConvexFill(call);
							break;
						case CallType.Stroke:
							RenderStroke(call);
							break;
						case CallType.Triangles:
							RenderTriangles(call);
							break;
					}
				}
			}
			finally
			{
				_device.SamplerStates[0] = _oldSamplerState;
				_device.BlendState = _oldBlendState;
				_device.DepthStencilState = _oldDepthStencilState;
				_device.RasterizerState = _oldRasterizerState;
				_vertexArray = null;
			}
		}

		private void SetUniform(ref UniformInfo uniform)
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
			_textureParam.SetValue(uniform.Image);

			if (_edgeAntiAlias)
			{
				_strokeMultParam.SetValue(uniform.strokeMult);
				_strokeThrParam.SetValue(uniform.strokeThr);
			}
		}

		private void DrawVertexes(RenderingType renderingType, PrimitiveType primitiveType, int vertexOffset, int vertexCount, bool indexed)
		{
			if (vertexCount == 0)
			{
				return;
			}

			var technique = _techniques[(int)renderingType];
			_effect.CurrentTechnique = technique;
			foreach (var pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();

				if (indexed)
				{
					var primitiveCount = vertexCount - 2;
					_device.DrawUserIndexedPrimitives(primitiveType, _vertexArray, vertexOffset, vertexCount, _indexes, 0, primitiveCount);
				}
				else
				{
					var primitiveCount =
						primitiveType == PrimitiveType.TriangleList ? vertexCount / 3 : vertexCount - 2;
					_device.DrawUserPrimitives(primitiveType, _vertexArray, vertexOffset, primitiveCount);
				}
			}
		}

		private void RenderConvexFill(CallInfo call)
		{
			// Convex Fill
			SetUniform(ref call.UniformInfo);

			var renderingType = call.UniformInfo.Image != null ? RenderingType.FillImage : RenderingType.FillGradient;
			for (var i = 0; i < call.FillStrokeInfos.Count; i++)
			{
				var fillStrokeInfo = call.FillStrokeInfos[i];

				DrawVertexes(renderingType, PrimitiveType.TriangleList, fillStrokeInfo.FillOffset, fillStrokeInfo.FillCount, true);
				DrawVertexes(renderingType, PrimitiveType.TriangleStrip, fillStrokeInfo.StrokeOffset, fillStrokeInfo.StrokeCount, false);
			}
		}

		private void RenderFill(CallInfo call)
		{
			_device.BlendState = _blendStateNoDraw;
			_device.DepthStencilState = _stencilStateFill1;
			var renderingType = RenderingType.Simple;

			SetUniform(ref call.UniformInfo);

			for (var i = 0; i < call.FillStrokeInfos.Count; i++)
			{
				var fillStrokeInfo = call.FillStrokeInfos[i];

				DrawVertexes(renderingType, PrimitiveType.TriangleList, fillStrokeInfo.FillOffset, fillStrokeInfo.FillCount, true);
			}

			// Draw anti-aliased pixels
			_device.BlendState = BlendState.AlphaBlend;

			SetUniform(ref call.UniformInfo2);

			if (_edgeAntiAlias)
			{
				_device.DepthStencilState = _stencilStateFill2;
				renderingType = call.UniformInfo2.Image != null ? RenderingType.FillImage : RenderingType.FillGradient;

				// Draw fringes
				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					var fillStrokeInfo = call.FillStrokeInfos[i];

					DrawVertexes(renderingType, PrimitiveType.TriangleStrip, fillStrokeInfo.StrokeOffset, fillStrokeInfo.StrokeCount, false);
				}
			}

			_device.DepthStencilState = _stencilStateFill3;

			renderingType = RenderingType.FillGradient;
			DrawVertexes(renderingType, PrimitiveType.TriangleStrip, call.TriangleOffset, call.TriangleCount, false);

			_device.DepthStencilState = DepthStencilState.None;
		}

		private void RenderStroke(CallInfo call)
		{
			SetUniform(ref call.UniformInfo);
			var renderingType = call.UniformInfo.Image != null ? RenderingType.FillImage : RenderingType.FillGradient;

			for (var i = 0; i < call.FillStrokeInfos.Count; i++)
			{
				var fillStrokeInfo = call.FillStrokeInfos[i];

				DrawVertexes(renderingType, PrimitiveType.TriangleStrip, fillStrokeInfo.StrokeOffset, fillStrokeInfo.StrokeCount, false);
			}
		}

		private void RenderTriangles(CallInfo call)
		{
			SetUniform(ref call.UniformInfo);

			DrawVertexes(RenderingType.Triangles, PrimitiveType.TriangleList, call.TriangleOffset, call.TriangleCount, false);
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