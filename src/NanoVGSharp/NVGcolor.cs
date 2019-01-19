using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct NVGcolor
	{
		public float r;
		public float g;
		public float b;
		public float a;

		public void Zero()
		{
			r = g = b = a = 0;
		}
	}
}
