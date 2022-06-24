using FontStashSharp;
using System;
using System.Collections.Generic;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Numerics;
#endif

namespace NvgSharp
{
	internal class RenderCache
	{
		private const int MAX_VERTICES = 8192;

		private readonly bool _stencilStrokes;

		public readonly ArrayBuffer<Vertex> VertexArray = new ArrayBuffer<Vertex>(MAX_VERTICES);
		public readonly List<CallInfo> Calls = new List<CallInfo>();
		public Vector2 ViewportSize;
		public float DevicePixelRatio;

		public int VertexCount => VertexArray.Count;

		public bool StencilStrokes => _stencilStrokes;

		public RenderCache(bool stencilStrokes)
		{
			_stencilStrokes = stencilStrokes;
		}

		public void Reset()
		{
			VertexArray.Clear();
			Calls.Clear();
		}

		public void AddVertex(float x, float y, float u, float v)
		{
			VertexArray.Add(new Vertex(x, y, u, v));
		}

		public void AddVertex(Vertex v)
		{
			VertexArray.Add(v);
		}

		private static void BuildUniform(ref Paint paint, ref Scissor scissor, float width, float fringe,
			float strokeThr, ref UniformInfo uniform)
		{
			uniform.innerCol = paint.InnerColor.ToVector4(true);
			uniform.outerCol = paint.OuterColor.ToVector4(true);

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

			uniform.Image = paint.Image;

			if (paint.Image != null)
			{
				uniform.type = RenderType.FillImage;
			}
			else
			{
				uniform.type = (int)RenderType.FillGradient;
				uniform.radius = paint.Radius;
				uniform.feather = paint.Feather;
			}

			uniform.paintMat = paint.Transform.BuildInverse().ToMatrix();
		}

		public void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, IReadOnlyList<Path> paths)
		{
			var call = new CallInfo
			{
				Type = CallType.Fill
			};

			if (paths.Count == 1 && paths[0].Convex)
			{
				call.Type = CallType.ConvexFill;
			}

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths[i];

				var drawCallInfo = new FillStrokeInfo
				{
					FillOffset = path.FillOffset,
					FillCount = path.FillCount,
					StrokeOffset = path.StrokeOffset,
					StrokeCount = path.StrokeCount,
				};

				call.FillStrokeInfos.Add(drawCallInfo);
			}

			// Setup uniforms for draw calls
			if (call.Type == CallType.Fill)
			{
				// Quad
				call.TriangleOffset = VertexArray.Count;
				call.TriangleCount = 4;
				VertexArray.Add(new Vertex(bounds.X2, bounds.Y2, 0.5f, 1.0f));
				VertexArray.Add(new Vertex(bounds.X2, bounds.Y, 0.5f, 1.0f));
				VertexArray.Add(new Vertex(bounds.X, bounds.Y2, 0.5f, 1.0f));
				VertexArray.Add(new Vertex(bounds.X, bounds.Y, 0.5f, 1.0f));

				// Simple shader for stencil
				call.UniformInfo.strokeThr = -1.0f;
				call.UniformInfo.type = RenderType.Simple;

				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref call.UniformInfo2);
			}
			else
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref call.UniformInfo);
			}

			Calls.Add(call);
		}

		public void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, IReadOnlyList<Path> paths)
		{
			var call = new CallInfo
			{
				Type = CallType.Stroke,
			};

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths[i];

				var drawCallInfo = new FillStrokeInfo
				{
					StrokeOffset = path.StrokeOffset,
					StrokeCount = path.StrokeCount,
				};

				call.FillStrokeInfos.Add(drawCallInfo);
			}

			// Setup uniforms for draw calls
			if (_stencilStrokes)
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref call.UniformInfo);
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, 1.0f - 0.5f / 255.0f, ref call.UniformInfo2);
			}
			else
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref call.UniformInfo);
			}

			Calls.Add(call);
		}

		public void RenderTriangles(ref Paint paint, ref Scissor scissor, float fringe, int triangleOffset, int triangleCount)
		{
			var call = new CallInfo
			{
				Type = CallType.Triangles,
				TriangleOffset = triangleOffset,
				TriangleCount = triangleCount
			};

			// Fill shader
			BuildUniform(ref paint, ref scissor, 1.0f, fringe, -1.0f, ref call.UniformInfo);
			call.UniformInfo.type = RenderType.Image;

			Calls.Add(call);
		}
	}
}