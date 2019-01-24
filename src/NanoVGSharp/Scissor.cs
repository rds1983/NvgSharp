using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Scissor
	{
		public Transform Transform;
		public Vector2 Extent;
	}
}
