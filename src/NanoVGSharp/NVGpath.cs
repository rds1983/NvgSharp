using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct NVGpath
	{
		public int first;
		public int count;
		public byte closed;
		public int nbevel;
		public NVGvertex* fill;
		public int nfill;
		public NVGvertex* stroke;
		public int nstroke;
		public int winding;
		public int convex;
	}
}
