using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct FONSstate
	{
		public int font;
		public int align;
		public float size;
		public uint color;
		public float blur;
		public float spacing;
	}
}
