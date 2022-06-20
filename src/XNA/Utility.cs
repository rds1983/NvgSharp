using Microsoft.Xna.Framework;

namespace NvgSharp
{
	internal static class Utility
	{
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
	}
}
