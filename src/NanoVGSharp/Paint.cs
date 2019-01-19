using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Paint
	{
		public Transform xform;
		public float extent1, extent2;
		public float radius;
		public float feather;
		public Color innerColor;
		public Color outerColor;
		public int image;

		public void Zero()
		{
			xform.Zero();
			extent1 = extent2 = 0;
			radius = 0;
			feather = 0;
			innerColor = Color.Transparent;
			outerColor = Color.Transparent;
			image = 0;
		}
	}
}