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

		public static ColorInfo ToColorInfo(this Color c) => new()
		{
			R = c.R / 255.0f,
			G = c.G / 255.0f,
			B = c.B / 255.0f,
			A = c.A / 255.0f
		};

		public static ColorInfo MakePremultiplied(this ColorInfo c)
		{
			c.R *= c.A;
			c.G *= c.A;
			c.B *= c.A;

			return c;
		}

		public static Matrix3x2 ToMatrix3x2(this Transform transform)
		{
			var result = new Matrix3x2
			{
				M11 = transform.T1,
				M12 = transform.T2,
				M21 = transform.T3,
				M22 = transform.T4,
				M31 = transform.T5,
				M32 = transform.T6
			};

			return result;
		}

		public static void MakeZero(this Matrix3x2 m)
		{
			m.M11 = m.M12 = m.M21 = m.M22 = m.M31 = m.M32 = 0;
		}
	}
}
