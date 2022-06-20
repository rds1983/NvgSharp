using FontStashSharp;

namespace NvgSharp
{
	internal unsafe class PathCache
	{
		public readonly Buffer<Path> Paths = new Buffer<Path>(16);
		public readonly Buffer<Vertex> Vertexes = new Buffer<Vertex>(256);
		public Bounds Bounds = new Bounds();
	}
}