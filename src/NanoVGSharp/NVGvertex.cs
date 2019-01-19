using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct NVGvertex
	{
		public float x;
		public float y;
		public float u;
		public float v;
	}
}
