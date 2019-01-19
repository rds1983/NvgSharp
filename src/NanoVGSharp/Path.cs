using Microsoft.Xna.Framework.Graphics;
using System;

namespace NanoVGSharp
{
	public class Path
	{
		public int first;
		public int count;
		public byte closed;
		public int nbevel;
		public ArraySegment<VertexPositionColorTexture>? fill;
		public ArraySegment<VertexPositionColorTexture>? stroke;
		public int winding;
		public int convex;
	}
}
