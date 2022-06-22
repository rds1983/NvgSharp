#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Drawing;
#endif

namespace NvgSharp
{
	internal static class Utility
	{
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
