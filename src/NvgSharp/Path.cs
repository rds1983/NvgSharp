using System;

namespace NvgSharp
{
	internal class Path
	{
		public int First;
		public int Count;
		public byte Closed;
		public int BevelCount;
		public ArraySegment<Vertex>? Fill;
		public ArraySegment<Vertex>? Stroke;
		public Winding Winding;
		public int Convex;
	}
}
