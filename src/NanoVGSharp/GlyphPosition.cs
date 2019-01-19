using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GlyphPosition
	{
		public StringLocation str;
		public float x;
		public float minx;
		public float maxx;
	}
}
