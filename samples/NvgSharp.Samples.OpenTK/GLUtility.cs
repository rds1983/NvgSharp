using OpenTK.Graphics.OpenGL4;
using System;


namespace NvgSharp.Samples.OpenTK
{
	internal static class GLUtility
	{
		public static void CheckError()
		{
			var error = GL.GetError();
			if (error != ErrorCode.NoError)
				throw new Exception("GL.GetError() returned " + error.ToString());
		}

		public static void DrawStroke(this FillStrokeInfo fillStrokeInfo, PrimitiveType primitiveType)
		{
			if (fillStrokeInfo.StrokeCount <= 0)
			{
				return;
			}

			GL.DrawArrays(primitiveType, fillStrokeInfo.StrokeOffset, fillStrokeInfo.StrokeCount);
			CheckError();
		}

		public static void DrawFill(this FillStrokeInfo fillStrokeInfo, PrimitiveType primitiveType)
		{
			if (fillStrokeInfo.FillCount <= 0)
			{
				return;
			}

			GL.DrawArrays(primitiveType, fillStrokeInfo.FillOffset, fillStrokeInfo.FillCount);
			CheckError();
		}

		public static void DrawTriangles(this CallInfo callInfo, PrimitiveType primitiveType)
		{
			if (callInfo.TriangleCount <= 0)
			{
				return;
			}

			GL.DrawArrays(primitiveType, callInfo.TriangleOffset, callInfo.TriangleCount);
			CheckError();
		}
	}
}
