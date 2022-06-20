using System.Runtime.InteropServices;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Numerics;
#endif

namespace NvgSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Scissor
	{
		public Transform Transform;
		public Vector2 Extent;
	}
}
