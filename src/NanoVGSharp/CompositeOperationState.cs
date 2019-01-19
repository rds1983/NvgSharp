using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompositeOperationState
	{
		public int srcRGB;
		public int dstRGB;
		public int srcAlpha;
		public int dstAlpha;
	}
}
