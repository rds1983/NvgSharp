using System.Runtime.InteropServices;

namespace NvgSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct NvgPoint
	{
		public float X;
		public float Y;
		public float DeltaX;
		public float DeltaY;
		public float Length;
		public float dmx;
		public float dmy;
		public byte flags;

		public void Reset()
		{
			X = Y = DeltaX = DeltaY = Length = dmx = dmy = 0;
			flags = 0;
		}
	}
}