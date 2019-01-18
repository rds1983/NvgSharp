namespace NanoVGSharp
{
	public unsafe class FONScontext
	{
		public delegate void handleErrorDelegate(void* uptr, int error, int val);

		public FONSparams _params_ = new FONSparams();
		public float itw;
		public float ith;
		public byte* texData;
		public int[] dirtyRect = new int[4];
		public NanoVG.FONSfont[] fonts;
		public NanoVG.FONSatlas* atlas;
		public int cfonts;
		public int nfonts;
		public float[] verts = new float[1024 * 2];
		public float[] tcoords = new float[1024 * 2];
		public uint[] colors = new uint[1024];
		public int nverts;
		public byte* scratch;
		public int nscratch;
		public NanoVG.FONSstate[] states = new NanoVG.FONSstate[20];
		public int nstates;
		public handleErrorDelegate handleError;
		public void* errorUptr;
	}
}
