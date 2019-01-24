using System.Runtime.InteropServices;
using FontStashSharp;

namespace NvgSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GlyphPosition
	{
		public StringSegment Str;
		public float X;
		public float MinX;
		public float MaxX;
	}
}
