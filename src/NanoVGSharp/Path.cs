using System;

namespace NanoVGSharp
{
	internal class Path
	{
		public int First;
		public int Count;
		public byte Closed;
		public int BevelCount;
		public ArraySegment<Vertex>? Fill;
		public ArraySegment<Vertex>? Stroke;
		public int Winding;
		public int Convex;
	}
}
