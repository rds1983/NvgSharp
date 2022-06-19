using System;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
{
	internal interface IRenderer
	{
		GraphicsDevice GraphicsDevice { get; }

		void Begin();
		void End();

		void Viewport(float width, float height, float devicePixelRatio);
		void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths);
		void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, ArraySegment<Path> paths);
		void RenderTriangles(ref Paint paint, ref Scissor scissor, ArraySegment<Vertex> verts);
	}
}
