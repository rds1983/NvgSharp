using FontStashSharp;

namespace NvgSharp
{
	internal unsafe class PathCache
	{
		public readonly ArrayBuffer<Path> Paths = new ArrayBuffer<Path>(16);
		public readonly ArrayBuffer<Vertex> Vertexes = new ArrayBuffer<Vertex>(256);
		public Bounds Bounds = new Bounds();
	}
}