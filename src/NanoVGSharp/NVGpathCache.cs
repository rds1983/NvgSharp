using Microsoft.Xna.Framework.Graphics;
using StbSharp;
using System;

namespace NanoVGSharp
{
	public unsafe class NVGpathCache: IDisposable
	{
		public NVGpoint* points;
		public int npoints, cpoints;
		public Buffer<NVGpath> paths;
		public Buffer<VertexPositionColorTexture> verts;
		public Bounds bounds = new Bounds();

		public NVGpathCache()
		{
			points = (NVGpoint*)(CRuntime.malloc((ulong)(sizeof(NVGpoint) * 128)));
			npoints = (int)(0);
			cpoints = (int)(128);
			paths = new Buffer<NVGpath>(16);
			verts = new Buffer<VertexPositionColorTexture>(256);
		}

		public void Dispose()
		{
			if (points != null)
			{
				CRuntime.free(points);
				points = null;
			}
		}
	}
}
