using System;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Drawing;
using Matrix = System.Numerics.Matrix4x4;
#endif

namespace NvgSharp
{
	internal static unsafe class NvgUtility
	{
		public static float sqrtf(float a)
		{
			return (float)(Math.Sqrt((float)(a)));
		}

		public static float sinf(float a)
		{
			return (float)(Math.Sin((float)(a)));
		}

		public static float tanf(float a)
		{
			return (float)(Math.Tan((float)(a)));
		}

		public static float atan2f(float a, float b)
		{
			return (float)(Math.Atan2(a, b));
		}

		public static float cosf(float a)
		{
			return (float)(Math.Cos((float)(a)));
		}

		public static float acosf(float a)
		{
			return (float)(Math.Acos((float)(a)));
		}

		public static float ceilf(float a)
		{
			return (float)(Math.Ceiling((float)(a)));
		}

		public static float __modf(float a, float b)
		{
			return (float)(a % b);
		}

		public static int __mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int __maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}

		public static int __clampi(int a, int mn, int mx)
		{
			return (int)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		public static float __minf(float a, float b)
		{
			return (float)((a) < (b) ? a : b);
		}

		public static float __maxf(float a, float b)
		{
			return (float)((a) > (b) ? a : b);
		}

		public static float __absf(float a)
		{
			return (float)((a) >= (0.0f) ? a : -a);
		}

		public static float __signf(float a)
		{
			return (float)((a) >= (0.0f) ? 1.0f : -1.0f);
		}

		public static float __clampf(float a, float mn, float mx)
		{
			return (float)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		public static float __cross(float dx0, float dy0, float dx1, float dy1)
		{
			return (float)(dx1 * dy0 - dx0 * dy1);
		}

		public static float __normalize(ref float x, ref float y)
		{
			float d = (float)sqrtf((float)((x * x) + (y * y)));
			if ((d) > (1e-6f))
			{
				float id = (float)(1.0f / d);
				x *= (float)(id);
				y *= (float)(id);
			}

			return (float)(d);
		}

		public static void MakeZero(this Matrix m)
		{
			m.M11 = m.M12 = m.M13 = m.M14 = 0;
			m.M21 = m.M22 = m.M23 = m.M24 = 0;
			m.M31 = m.M32 = m.M33 = m.M34 = 0;
			m.M41 = m.M42 = m.M43 = m.M44 = 0;
		}

		public static Color FromRGBA(byte r, byte g, byte b, byte a)
		{
#if MONOGAME || FNA || STRIDE
			return new Color(r, g, b, a);
#else
			return Color.FromArgb(a, r, g, b);
#endif
		}

	}
}