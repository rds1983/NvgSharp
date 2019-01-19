using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct NVGglyphPosition
	{
		public string str;
		public float x;
		public float minx;
		public float maxx;
	}
}
