using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Paint
	{
		public Transform Transform;
		public Vector2 Extent;
		public float Radius;
		public float Feather;
		public Color InnerColor;
		public Color OuterColor;
		public int Image;
		
		public Paint(Color color)
		{
			Transform = new Transform();
			Extent = new Vector2();
			Transform.SetIdentity();
			Radius = 0.0f;
			Feather = 1.0f;
			InnerColor = color;
			OuterColor = color;
			Image = 0;
		}

		public void Zero()
		{
			Transform.Zero();
			Extent = Vector2.Zero;
			Radius = 0;
			Feather = 0;
			InnerColor = Color.Transparent;
			OuterColor = Color.Transparent;
			Image = 0;
		}
	}
}