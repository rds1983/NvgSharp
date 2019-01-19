using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Scissor
	{
		public Transform xform;
		public float extent1, extent2;
	}
}
