namespace NanoVGSharp
{
	public unsafe class NVGparams
	{
		public delegate int renderCreateDelegate(void *uptr);
		public delegate int renderCreateTextureDelegate(void* uptr, int type, int w, int h, int imageFlags, byte* data);
		public delegate int renderDeleteTextureDelegate(void* uptr, int image);
		public delegate int renderUpdateTextureDelegate(void* uptr, int image, int x, int y, int w, int h, byte* data);
		public delegate int renderGetTextureSizeDelegate(void* uptr, int image, int* w, int* h);
		public delegate void renderViewportDelegate (void* uptr, float width, float height, float devicePixelRatio);
		public delegate void renderCancelDelegate(void* uptr);
		public delegate void renderFlushDelegate(void* uptr);
		public delegate void renderFillDelegate(void* uptr, NanoVG.NVGpaint* paint, NanoVG.NVGcompositeOperationState compositeOperation, NanoVG.NVGscissor* scissor, float fringe, float* bounds, NanoVG.NVGpath* paths, int npaths);
		public delegate void renderStrokeDelegate(void* uptr, NanoVG.NVGpaint* paint, NanoVG.NVGcompositeOperationState compositeOperation, NanoVG.NVGscissor* scissor, float fringe, float strokeWidth, NanoVG.NVGpath* paths, int npaths);
		public delegate void renderTrianglesDelegate(void* uptr, NanoVG.NVGpaint* paint, NanoVG.NVGcompositeOperationState compositeOperation, NanoVG.NVGscissor* scissor, NanoVG.NVGvertex* verts, int nverts);
		public delegate void renderDeleteDelegate(void* uptr);


		public void* userPtr;
		public int edgeAntiAlias;
		public renderCreateDelegate renderCreate;
		public renderCreateTextureDelegate renderCreateTexture;
		public renderDeleteTextureDelegate renderDeleteTexture;
		public renderUpdateTextureDelegate renderUpdateTexture;
		public renderGetTextureSizeDelegate renderGetTextureSize;
		public renderViewportDelegate renderViewport;
		public renderCancelDelegate renderCancel;
		public renderFlushDelegate renderFlush;
		public renderFillDelegate renderFill;
		public renderStrokeDelegate renderStroke;
		public renderTrianglesDelegate renderTriangles;
		public renderDeleteDelegate renderDelete;
	}
}
