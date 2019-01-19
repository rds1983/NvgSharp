namespace NanoVGSharp
{
	public unsafe class NVGcontext
	{
		public NVGparams _params_ = new NVGparams();
		public float* commands;
		public int ccommands;
		public int ncommands;
		public float commandx;
		public float commandy;
		public NVGstate[] states = new NVGstate[32];
		public int nstates;
		public NVGpathCache cache;
		public float tessTol;
		public float distTol;
		public float fringeWidth;
		public float devicePxRatio;
		public FONScontext fs;
		public int[] fontImages = new int[4];
		public int fontImageIdx;
		public int drawCallCount;
		public int fillTriCount;
		public int strokeTriCount;
		public int textTriCount;
	}
}
