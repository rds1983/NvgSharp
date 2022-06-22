using Microsoft.Xna.Framework;

namespace NvgSharp
{
	internal static class Utility
	{
		public static ColorInfo ToColorInfo(this Color c) => new ColorInfo
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

		public static Matrix ToMatrix(this Transform t)
		{
			var m3 = Matrix.Identity;

			m3.M11 = t.T1;
			m3.M21 = t.T2;
			m3.M31 = 0.0f;
			m3.M41 = 0.0f;
			m3.M12 = t.T3;
			m3.M22 = t.T4;
			m3.M32 = 0.0f;
			m3.M42 = 0.0f;
			m3.M13 = t.T5;
			m3.M23 = t.T6;
			m3.M33 = 1.0f;
			m3.M43 = 0.0f;

			return m3;
		}

		public static void MakeZero(this Matrix m)
		{
			m.M11 = m.M12 = m.M13 = m.M14 = 0;
			m.M21 = m.M22 = m.M23 = m.M24 = 0;
			m.M31 = m.M32 = m.M33 = m.M34 = 0;
			m.M41 = m.M42 = m.M43 = m.M44 = 0;
		}
	}
}
