using System;
using Microsoft.Xna.Framework.Graphics;
using StbSharp;

namespace NanoVGSharp
{
    public unsafe class PathCache : IDisposable
    {
        public Bounds bounds = new Bounds();
        public int npoints, cpoints;
        public Buffer<Path> paths;
        public NVGpoint* points;
        public Buffer<VertexPositionColorTexture> verts;

        public PathCache()
        {
            points = (NVGpoint*) CRuntime.malloc((ulong) (sizeof(NVGpoint) * 128));
            npoints = 0;
            cpoints = 128;
            paths = new Buffer<Path>(16);
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