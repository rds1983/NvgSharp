using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct NVGglyphPosition
	{
		public StringLocation str;
		public float x;
		public float minx;
		public float maxx;
	}
}
