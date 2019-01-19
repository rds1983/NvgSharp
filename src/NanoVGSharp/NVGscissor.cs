using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct NVGscissor
	{
		public fixed float xform[6];
		public fixed float extent[2];
	}
}
