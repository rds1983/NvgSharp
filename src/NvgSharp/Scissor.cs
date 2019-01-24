using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace NvgSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Scissor
	{
		public Transform Transform;
		public Vector2 Extent;
	}
}
