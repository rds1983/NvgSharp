using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct NVGpoint
	{
		public float x;
		public float y;
		public float dx;
		public float dy;
		public float len;
		public float dmx;
		public float dmy;
		public byte flags;

		public void Reset()
		{
			x = y = dx = dy = len = dmx = dmy = 0;
			flags = 0;
		}
	}
}