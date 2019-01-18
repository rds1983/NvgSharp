namespace NanoVGSharp
{
	public unsafe class FONSparams
	{
		public delegate int renderCreateDelegate(void* uptr, int width, int height);
		public delegate int renderResizeDelegate(void* uptr, int width, int height);
		public delegate void renderUpdateDelegate(void* uptr, int* rect, byte* data);
		public delegate void renderDrawDelegate(void* uptr, float* verts, float* tcoords, uint* colors, int nverts);
		public delegate void renderDeleteDelegate(void* uptr);

		public int width;
		public int height;
		public byte flags;
		public void* userPtr;
		public renderCreateDelegate renderCreate;
		public renderResizeDelegate renderResize;
		public renderUpdateDelegate renderUpdate;
		public renderDrawDelegate renderDraw;
		public renderDeleteDelegate renderDelete;
	}
}