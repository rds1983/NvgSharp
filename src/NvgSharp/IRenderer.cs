using System;
using FontStashSharp;

namespace NvgSharp
{
	internal interface IRenderer
	{
		void Begin();
		void End();

		int CreateTexture(int type, int w, int h, int imageFlags, byte[] data);
		void DeleteTexture(int image);
		void UpdateTexture(int image, int x, int y, int w, int h, byte[] data);
		void GetTextureSize(int image, out int w, out int h);
		void Viewport(float width, float height, float devicePixelRatio);
		void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths);
		void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, ArraySegment<Path> paths);
		void RenderTriangles(ref Paint paint, ref Scissor scissor, ArraySegment<Vertex> verts);
	}
}
