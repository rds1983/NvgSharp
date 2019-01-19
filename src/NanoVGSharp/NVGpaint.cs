using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct NVGpaint
	{
		public fixed float xform[6];
		public fixed float extent[2];
		public float radius;
		public float feather;
		public NVGcolor innerColor;
		public NVGcolor outerColor;
		public int image;
	}
}