using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct FontAtlasNode
	{
		public short x;
		public short y;
		public short width;
	}
}
