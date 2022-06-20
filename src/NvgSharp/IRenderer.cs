using System;
using FontStashSharp;
using FontStashSharp.Interfaces;

#if MONOGAME || FNA
using Microsoft.Xna.Framework.Graphics;
#elif STRIDE
using Stride.Graphics;
#endif

namespace NvgSharp
{
	public interface IRenderer
	{
#if MONOGAME || FNA || STRIDE
		GraphicsDevice GraphicsDevice { get; }
#else
		ITexture2DManager TextureManager { get; }
#endif

		void Begin();
		void End();

		void Viewport(float width, float height, float devicePixelRatio);
		void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths);
		void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, ArraySegment<Path> paths);
		void RenderTriangles(ref Paint paint, ref Scissor scissor, ArraySegment<Vertex> verts);
	}
}
