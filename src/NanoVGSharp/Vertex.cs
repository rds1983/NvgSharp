using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex
	{
		public float X, Y, U, V;

		public Vertex(float x, float y, float u, float v)
		{
			X = x;
			Y = y;
			U = u;
			V = v;
		}
	}
}
