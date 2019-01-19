using StbSharp;
using System;

namespace NanoVGSharp
{
	public unsafe class NVGpathCache: IDisposable
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

		public NVGpathCache()
		{
			points = (NVGpoint*)(CRuntime.malloc((ulong)(sizeof(NVGpoint) * 128)));
			npoints = (int)(0);
			cpoints = (int)(128);
			paths = (NVGpath*)(CRuntime.malloc((ulong)(sizeof(NVGpath) * 16)));
			npaths = (int)(0);
			cpaths = (int)(16);
			verts = (NVGvertex*)(CRuntime.malloc((ulong)(sizeof(NVGvertex) * 256)));
			nverts = (int)(0);
			cverts = (int)(256);
		}

		public void Dispose()
		{
			if (points != null)
			{
				CRuntime.free(points);
				points = null;
			}

			if (paths != null)
			{
				CRuntime.free(paths);
				paths = null;
			}

			if (verts != null)
			{
				CRuntime.free(verts);
				verts = null;
			}
		}
	}
}
