namespace NanoVGSharp
{
	public unsafe class NVGpathCache
	{
		public NVGpoint* points;
		public int npoints;
		public int cpoints;
		public NVGpath* paths;
		public int npaths;
		public int cpaths;
		public NVGvertex* verts;
		public int nverts;
		public int cverts;
		public float[] bounds = new float[4];
	}
}
