using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
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
		public Texture2D Image;
		
		public Paint(Color color)
		{
			Transform = new Transform();
			Extent = new Vector2();
			Transform.SetIdentity();
			Radius = 0.0f;
			Feather = 1.0f;
			InnerColor = color;
			OuterColor = color;
			Image = null;
		}

		public void Zero()
		{
			Transform.Zero();
			Extent = Vector2.Zero;
			Radius = 0;
			Feather = 0;
			InnerColor = Color.Transparent;
			OuterColor = Color.Transparent;
			Image = null;
		}
	}
}