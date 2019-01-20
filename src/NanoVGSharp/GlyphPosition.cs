using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GlyphPosition
	{
		public StringSegment str;
		public float x;
		public float minx;
		public float maxx;
	}
}
