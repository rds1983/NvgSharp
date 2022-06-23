using FontStashSharp;
using System;
using System.Collections.Generic;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Numerics;
using Matrix = System.Numerics.Matrix4x4;
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

		private static void BuildUniform(ref Paint paint, ref Scissor scissor, float width, float fringe, 
			float strokeThr, ref UniformInfo uniform)
		{
			uniform.innerCol = new ColorInfo(paint.InnerColor);
			uniform.innerCol.MakePremultiplied();
			uniform.outerCol = new ColorInfo(paint.OuterColor);
			uniform.outerCol.MakePremultiplied();

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

		public void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			var call = new CallInfo
			{
				Type = CallType.Fill
			};

			if (paths.Count == 1 && paths.Array[paths.Offset].Convex)
			{
				call.Type = CallType.ConvexFill;
			}

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths.Array[i + paths.Offset];

				var drawCallInfo = new FillStrokeInfo();
				if (path.Fill != null && path.Fill.Value.Count > 0)
				{
					drawCallInfo.FillOffset = VertexArray.Count;
					drawCallInfo.FillCount = path.Fill.Value.Count;
					VertexArray.Add(path.Fill.Value);
				}
				if (path.Stroke != null && path.Stroke.Value.Count > 0)
				{
					drawCallInfo.StrokeOffset = VertexArray.Count;
					drawCallInfo.StrokeCount = path.Stroke.Value.Count;
					VertexArray.Add(path.Stroke.Value);
				}

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

		public void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			var call = new CallInfo
			{
				Type = CallType.Stroke,
			};

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths.Array[i + paths.Offset];

				var drawCallInfo = new FillStrokeInfo();
				if (path.Stroke != null && path.Stroke.Value.Count > 0)
				{
					drawCallInfo.StrokeOffset = VertexArray.Count;
					drawCallInfo.StrokeCount = path.Stroke.Value.Count;
					VertexArray.Add(path.Stroke.Value);
				}

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

		public void RenderTriangles(ref Paint paint, ref Scissor scissor, float fringe, ArraySegment<Vertex> verts)
		{
			var call = new CallInfo
			{
				Type = CallType.Triangles,
				TriangleOffset = VertexArray.Count,
				TriangleCount = verts.Count
			};

			VertexArray.Add(verts);

			// Fill shader
			BuildUniform(ref paint, ref scissor, 1.0f, fringe, -1.0f, ref call.UniformInfo);
			call.UniformInfo.type = RenderType.Image;

			Calls.Add(call);
		}
	}
}