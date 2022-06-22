using Silk.NET.OpenGL;
using System;
using System.Drawing;
using System.Numerics;


namespace NvgSharp
{
	internal static class GLUtility
	{
		public static void CheckError()
		{
			var error = (ErrorCode)Env.Gl.GetError();
			if (error != ErrorCode.NoError)
				throw new Exception("GL.GetError() returned " + error.ToString());
		}

		public static Matrix4x4 ToMatrix4x4(this Transform t)
		{
			var result = Matrix4x4.Identity;

			result.M11 = t.T1;
			result.M12 = t.T2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = t.T3;
			result.M22 = t.T4;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = t.T5;
			result.M42 = t.T6;
			result.M43 = 0f;
			result.M44 = 1f;

			return result;
		}

		public static void MakeZero(this Matrix4x4 m)
		{
			m.M11 = m.M12 = m.M13 = m.M14 = 0;
			m.M21 = m.M22 = m.M23 = m.M24 = 0;
			m.M31 = m.M32 = m.M33 = m.M34 = 0;
			m.M41 = m.M42 = m.M43 = m.M44 = 0;
		}
	}
}
