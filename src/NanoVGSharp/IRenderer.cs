using Microsoft.Xna.Framework.Graphics;
using System;

namespace NanoVGSharp
{
	public interface IRenderer
	{
		void Begin();
		void End();

		int renderCreateTexture(int type, int w, int h, int imageFlags, byte[] data);
		void renderDeleteTexture(int image);
		void renderUpdateTexture(int image, int x, int y, int w, int h, byte[] data);
		void renderGetTextureSize(int image, out int w, out int h);
		void renderViewport(float width, float height, float devicePixelRatio);
		void renderFill(ref NVGpaint paint, NVGcompositeOperationState compositeOperation, ref NVGscissor scissor, float fringe, Bounds bounds, Buffer<NVGpath> paths);
		void renderStroke(ref NVGpaint paint, NVGcompositeOperationState compositeOperation, ref NVGscissor scissor, float fringe, float strokeWidth, Buffer<NVGpath> paths);
		void renderTriangles(ref NVGpaint paint, NVGcompositeOperationState compositeOperation, ref NVGscissor scissor, ArraySegment<VertexPositionColorTexture> verts);
	}
}
