using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct NVGpaint
	{
		public Transform xform;
		public float extent1, extent2;
		public float radius;
		public float feather;
		public NVGcolor innerColor;
		public NVGcolor outerColor;
		public int image;

		public void Zero()
		{
			xform.Zero();
			extent1 = extent2 = 0;
			radius = 0;
			feather = 0;
			innerColor.Zero();
			outerColor.Zero();
			image = 0;
		}
	}
}