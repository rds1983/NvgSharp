using System.Runtime.InteropServices;
using FontStashSharp;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GlyphPosition
	{
		public StringSegment Segment;
		public float X;
		public float MinX;
		public float MaxX;
	}
}
