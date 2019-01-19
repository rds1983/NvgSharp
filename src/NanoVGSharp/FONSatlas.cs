using StbSharp;
using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe class FONSatlas
	{
		public int width;
		public int height;
		public FONSatlasNode* nodes;
		public int nnodes;
		public int cnodes;

		public FONSatlas(int w, int h, int nnodes)
		{
			width = (int)(w);
			height = (int)(h);
			nodes = (FONSatlasNode*)(CRuntime.malloc((ulong)(sizeof(FONSatlasNode) * nnodes)));
			CRuntime.memset(nodes, (int)(0), (ulong)(sizeof(FONSatlasNode) * nnodes));
			nnodes = (int)(0);
			cnodes = (int)(nnodes);
			nodes[0].x = (short)(0);
			nodes[0].y = (short)(0);
			nodes[0].width = ((short)(w));
			nnodes++;
		}

		public int fons__atlasInsertNode(int idx, int x, int y, int w)
		{
			int i = 0;
			if ((nnodes + 1) > (cnodes))
			{
				cnodes = (int)((cnodes) == (0) ? 8 : cnodes * 2);
				nodes = (FONSatlasNode*)(CRuntime.realloc(nodes, (ulong)(sizeof(FONSatlasNode) * cnodes)));
				if ((nodes) == null)
					return (int)(0);
			}

			for (i = (int)(nnodes); (i) > (idx); i--)
			{
				nodes[i] = (FONSatlasNode)(nodes[i - 1]);
			}
			nodes[idx].x = ((short)(x));
			nodes[idx].y = ((short)(y));
			nodes[idx].width = ((short)(w));
			nnodes++;
			return (int)(1);
		}

		public void fons__atlasRemoveNode(int idx)
		{
			int i = 0;
			if ((nnodes) == (0))
				return;
			for (i = (int)(idx); (i) < (nnodes - 1); i++)
			{
				nodes[i] = (FONSatlasNode)(nodes[i + 1]);
			}
			nnodes--;
		}

		public void fons__atlasExpand(int w, int h)
		{
			if ((w) > (width))
				fons__atlasInsertNode((int)(nnodes), (int)(width), (int)(0), (int)(w - width));
			width = (int)(w);
			height = (int)(h);
		}

		public void fons__atlasReset(int w, int h)
		{
			width = (int)(w);
			height = (int)(h);
			nnodes = (int)(0);
			nodes[0].x = (short)(0);
			nodes[0].y = (short)(0);
			nodes[0].width = ((short)(w));
			nnodes++;
		}

		public int fons__atlasAddSkylineLevel(int idx, int x, int y, int w, int h)
		{
			int i = 0;
			if ((fons__atlasInsertNode((int)(idx), (int)(x), (int)(y + h), (int)(w))) == (0))
				return (int)(0);
			for (i = (int)(idx + 1); (i) < (nnodes); i++)
			{
				if ((nodes[i].x) < (nodes[i - 1].x + nodes[i - 1].width))
				{
					int shrink = (int)(nodes[i - 1].x + nodes[i - 1].width - nodes[i].x);
					nodes[i].x += ((short)(shrink));
					nodes[i].width -= ((short)(shrink));
					if (nodes[i].width <= 0)
					{
						fons__atlasRemoveNode((int)(i));
						i--;
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
			}
			for (i = (int)(0); (i) < (nnodes - 1); i++)
			{
				if ((nodes[i].y) == (nodes[i + 1].y))
				{
					nodes[i].width += (short)(nodes[i + 1].width);
					fons__atlasRemoveNode((int)(i + 1));
					i--;
				}
			}
			return (int)(1);
		}

		public int fons__atlasRectFits(int i, int w, int h)
		{
			int x = (int)(nodes[i].x);
			int y = (int)(nodes[i].y);
			int spaceLeft = 0;
			if ((x + w) > (width))
				return (int)(-1);
			spaceLeft = (int)(w);
			while ((spaceLeft) > (0))
			{
				if ((i) == (nnodes))
					return (int)(-1);
				y = (int)(FONScontext.fons__maxi((int)(y), (int)(nodes[i].y)));
				if ((y + h) > (height))
					return (int)(-1);
				spaceLeft -= (int)(nodes[i].width);
				++i;
			}
			return (int)(y);
		}

		public int fons__atlasAddRect(int rw, int rh, int* rx, int* ry)
		{
			int besth = (int)(height);
			int bestw = (int)(width);
			int besti = (int)(-1);
			int bestx = (int)(-1);
			int besty = (int)(-1);
			int i = 0;
			for (i = (int)(0); (i) < (nnodes); i++)
			{
				int y = (int)(fons__atlasRectFits((int)(i), (int)(rw), (int)(rh)));
				if (y != -1)
				{
					if (((y + rh) < (besth)) || (((y + rh) == (besth)) && ((nodes[i].width) < (bestw))))
					{
						besti = (int)(i);
						bestw = (int)(nodes[i].width);
						besth = (int)(y + rh);
						bestx = (int)(nodes[i].x);
						besty = (int)(y);
					}
				}
			}
			if ((besti) == (-1))
				return (int)(0);
			if ((fons__atlasAddSkylineLevel((int)(besti), (int)(bestx), (int)(besty), (int)(rw), (int)(rh))) == (0))
				return (int)(0);
			*rx = (int)(bestx);
			*ry = (int)(besty);
			return (int)(1);
		}
	}
}
