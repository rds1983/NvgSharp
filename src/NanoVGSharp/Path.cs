using System;

namespace NanoVGSharp
{
	public class Path
	{
		public int first;
		public int count;
		public byte closed;
		public int nbevel;
		public ArraySegment<Vertex>? fill;
		public ArraySegment<Vertex>? stroke;
		public int winding;
		public int convex;
	}
}
