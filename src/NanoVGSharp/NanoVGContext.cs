using Microsoft.Xna.Framework.Graphics;
using StbSharp;
using System;
using Microsoft.Xna.Framework;

namespace NanoVGSharp
{
	public unsafe class NanoVGContext : IDisposable
	{
		public const int NVG_CCW = 1;
		public const int NVG_CW = 2;
		public const int NVG_SOLID = 1;
		public const int NVG_HOLE = 2;
		public const int NVG_BUTT = 0;
		public const int NVG_ROUND = 1;
		public const int NVG_SQUARE = 2;
		public const int NVG_BEVEL = 3;
		public const int NVG_MITER = 4;
		public const int NVG_ALIGN_LEFT = 1 << 0;
		public const int NVG_ALIGN_CENTER = 1 << 1;
		public const int NVG_ALIGN_RIGHT = 1 << 2;
		public const int NVG_ALIGN_TOP = 1 << 3;
		public const int NVG_ALIGN_MIDDLE = 1 << 4;
		public const int NVG_ALIGN_BOTTOM = 1 << 5;
		public const int NVG_ALIGN_BASELINE = 1 << 6;
		public const int NVG_ZERO = 1 << 0;
		public const int NVG_ONE = 1 << 1;
		public const int NVG_SRC_COLOR = 1 << 2;
		public const int NVG_ONE_MINUS_SRC_COLOR = 1 << 3;
		public const int NVG_DST_COLOR = 1 << 4;
		public const int NVG_ONE_MINUS_DST_COLOR = 1 << 5;
		public const int NVG_SRC_ALPHA = 1 << 6;
		public const int NVG_ONE_MINUS_SRC_ALPHA = 1 << 7;
		public const int NVG_DST_ALPHA = 1 << 8;
		public const int NVG_ONE_MINUS_DST_ALPHA = 1 << 9;
		public const int NVG_SRC_ALPHA_SATURATE = 1 << 10;
		public const int NVG_SOURCE_OVER = 0;
		public const int NVG_SOURCE_IN = 1;
		public const int NVG_SOURCE_OUT = 2;
		public const int NVG_ATOP = 3;
		public const int NVG_DESTINATION_OVER = 4;
		public const int NVG_DESTINATION_IN = 5;
		public const int NVG_DESTINATION_OUT = 6;
		public const int NVG_DESTINATION_ATOP = 7;
		public const int NVG_LIGHTER = 8;
		public const int NVG_COPY = 9;
		public const int NVG_XOR = 10;
		public const int NVG_IMAGE_GENERATE_MIPMAPS = 1 << 0;
		public const int NVG_IMAGE_REPEATX = 1 << 1;
		public const int NVG_IMAGE_REPEATY = 1 << 2;
		public const int NVG_IMAGE_FLIPY = 1 << 3;
		public const int NVG_IMAGE_PREMULTIPLIED = 1 << 4;
		public const int NVG_IMAGE_NEAREST = 1 << 5;
		public const int NVG_TEXTURE_ALPHA = 0x01;
		public const int NVG_TEXTURE_RGBA = 0x02;
		public const int NVG_MOVETO = 0;
		public const int NVG_LINETO = 1;
		public const int NVG_BEZIERTO = 2;
		public const int NVG_CLOSE = 3;
		public const int NVG_WINDING = 4;
		public const int NVG_PT_CORNER = 0x01;
		public const int NVG_PT_LEFT = 0x02;
		public const int NVG_PT_BEVEL = 0x04;
		public const int NVG_PR_INNERBEVEL = 0x08;
		public const int NVG_SPACE = 0;
		public const int NVG_NEWLINE = 1;
		public const int NVG_CHAR = 2;
		public const int NVG_CJK_CHAR = 3;

		public const int MaxTextRows = 10;

		public readonly IRenderer _renderer;
		public int edgeAntiAlias;
		public float* commands;
		public int ccommands;
		public int ncommands;
		public float commandx;
		public float commandy;
		public NanoVGContextState[] states = new NanoVGContextState[32];
		public int nstates;
		private PathCache cache;
		public float tessTol;
		public float distTol;
		public float fringeWidth;
		public float devicePxRatio;
		public FontSystem fs;
		public int[] fontImages = new int[4];
		public int fontImageIdx;
		public int drawCallCount;
		public int fillTriCount;
		public int strokeTriCount;
		public int textTriCount;
		private TextRow[] _rows = new TextRow[MaxTextRows];

		public NanoVGContext(GraphicsDevice device, int edgeAntiAlias)
		{
			_renderer = new XNARenderer(device);

			FontSystemParams fontParams = new FontSystemParams();

			this.edgeAntiAlias = edgeAntiAlias;
			for (var i = 0; i < 4; i++)
			{
				fontImages[i] = 0;
			}
			commands = (float*)(CRuntime.malloc((ulong)(sizeof(float) * 256)));
			ncommands = (int)(0);
			ccommands = (int)(256);
			cache = new PathCache();
			nvgSave();
			nvgReset();
			nvg__setDevicePixelRatio((float)(1.0f));
			fontParams.width = (int)(512);
			fontParams.height = (int)(512);
			fontParams.flags = (byte)(FontSystem.FONS_ZERO_TOPLEFT);
			fs = new FontSystem(fontParams);
			fontImages[0] = (int)(_renderer.renderCreateTexture((int)(NVG_TEXTURE_ALPHA), (int)(fontParams.width), (int)(fontParams.height), (int)(0), null));
			fontImageIdx = (int)(0);

			for (var i = 0; i < _rows.Length; ++i)
			{
				_rows[i] = new TextRow();
			}
		}

		public void Dispose()
		{
			int i = 0;
			if (commands != null)
			{
				CRuntime.free(commands);
				commands = null;
			}

			if ((fs) != null)
			{
				fs.Dispose();
				fs = null;
			}

			for (i = (int)(0); (i) < (4); i++)
			{
				if (fontImages[i] != 0)
				{
					nvgDeleteImage((int)(fontImages[i]));
					fontImages[i] = (int)(0);
				}
			}
		}

		public void nvg__setDevicePixelRatio(float ratio)
		{
			tessTol = (float)(0.25f / ratio);
			distTol = (float)(0.01f / ratio);
			fringeWidth = (float)(1.0f / ratio);
			devicePxRatio = (float)(ratio);
		}

		public NanoVGContextState nvg__getState()
		{
			return states[nstates - 1];
		}

		public void nvgBeginFrame(float windowWidth, float windowHeight, float devicePixelRatio)
		{
			nstates = (int)(0);
			nvgSave();
			nvgReset();
			nvg__setDevicePixelRatio((float)(devicePixelRatio));

			_renderer.Begin();

			_renderer.renderViewport((float)(windowWidth), (float)(windowHeight), (float)(devicePixelRatio));
			drawCallCount = (int)(0);
			fillTriCount = (int)(0);
			strokeTriCount = (int)(0);
			textTriCount = (int)(0);
		}

		public void nvgEndFrame()
		{
			if (fontImageIdx != 0)
			{
				int fontImage = (int)(fontImages[fontImageIdx]);
				int i = 0;
				int j = 0;
				int iw = 0;
				int ih = 0;
				if ((fontImage) == (0))
					return;
				nvgImageSize((int)(fontImage), out iw, out ih);
				for (i = (int)(j = (int)(0)); (i) < (fontImageIdx); i++)
				{
					if (fontImages[i] != 0)
					{
						int nw = 0;
						int nh = 0;
						nvgImageSize((int)(fontImages[i]), out nw, out nh);
						if (((nw) < (iw)) || ((nh) < (ih)))
							nvgDeleteImage((int)(fontImages[i]));
						else
							fontImages[j++] = (int)(fontImages[i]);
					}
				}
				fontImages[j++] = (int)(fontImages[0]);
				fontImages[0] = (int)(fontImage);
				fontImageIdx = (int)(0);
				for (i = (int)(j); (i) < (4); i++)
				{
					fontImages[i] = (int)(0);
				}
			}

			_renderer.End();
		}

		public void nvgSave()
		{
			if ((nstates) >= (32))
				return;
			if ((nstates) > (0))
			{
				states[nstates] = states[nstates - 1].Clone();
			}
			else
			{
				states[nstates] = new NanoVGContextState();
			}
			nstates++;
		}

		public void nvgRestore()
		{
			if (nstates <= 1)
				return;
			nstates--;
		}

		public void nvgReset()
		{
			NanoVGContextState state = nvg__getState();
			nvg__setPaintColor(ref state.fill, Color.White);
			nvg__setPaintColor(ref state.stroke, Color.Black);
			state.compositeOperation = nvg__compositeOperationState((int)(NVG_SOURCE_OVER));
			state.shapeAntiAlias = (int)(1);
			state.strokeWidth = (float)(1.0f);
			state.miterLimit = (float)(10.0f);
			state.lineCap = (int)(NVG_BUTT);
			state.lineJoin = (int)(NVG_MITER);
			state.alpha = (float)(1.0f);
			state.xform.SetIdentity();
			state.scissor.extent1 = (float)(-1.0f);
			state.scissor.extent2 = (float)(-1.0f);
			state.fontSize = (float)(16.0f);
			state.letterSpacing = (float)(0.0f);
			state.lineHeight = (float)(1.0f);
			state.fontBlur = (float)(0.0f);
			state.textAlign = (int)(NVG_ALIGN_LEFT | NVG_ALIGN_BASELINE);
			state.fontId = (int)(0);
		}

		public void nvgShapeAntiAlias(int enabled)
		{
			NanoVGContextState state = nvg__getState();
			state.shapeAntiAlias = (int)(enabled);
		}

		public void nvgStrokeWidth(float width)
		{
			NanoVGContextState state = nvg__getState();
			state.strokeWidth = (float)(width);
		}

		public void nvgMiterLimit(float limit)
		{
			NanoVGContextState state = nvg__getState();
			state.miterLimit = (float)(limit);
		}

		public void nvgLineCap(int cap)
		{
			NanoVGContextState state = nvg__getState();
			state.lineCap = (int)(cap);
		}

		public void nvgLineJoin(int join)
		{
			NanoVGContextState state = nvg__getState();
			state.lineJoin = (int)(join);
		}

		public void nvgGlobalAlpha(float alpha)
		{
			NanoVGContextState state = nvg__getState();
			state.alpha = (float)(alpha);
		}

		public void nvgTransform(float a, float b, float c, float d, float e, float f)
		{
			NanoVGContextState state = nvg__getState();
			Transform t;
			t.t1 = (float)(a);
			t.t2 = (float)(b);
			t.t3 = (float)(c);
			t.t4 = (float)(d);
			t.t5 = (float)(e);
			t.t6 = (float)(f);

			state.xform.Premultiply(ref t);
		}

		public void nvgResetTransform()
		{
			NanoVGContextState state = nvg__getState();
			state.xform.SetIdentity();
		}

		public void nvgTranslate(float x, float y)
		{
			NanoVGContextState state = nvg__getState();
			var t = new Transform();
			t.SetTranslate((float)(x), (float)(y));
			state.xform.Premultiply(ref t);
		}

		public void nvgRotate(float angle)
		{
			NanoVGContextState state = nvg__getState();
			var t = new Transform();
			t.SetRotate((float)(angle));
			state.xform.Premultiply(ref t);
		}

		public void nvgSkewX(float angle)
		{
			NanoVGContextState state = nvg__getState();
			var t = new Transform();
			t.SetSkewX((float)(angle));
			state.xform.Premultiply(ref t);
		}

		public void nvgSkewY(float angle)
		{
			NanoVGContextState state = nvg__getState();
			var t = new Transform();
			t.SetSkewY((float)(angle));
			state.xform.Premultiply(ref t);
		}

		public void nvgScale(float x, float y)
		{
			NanoVGContextState state = nvg__getState();
			var t = new Transform();
			t.SetScale((float)(x), (float)(y));
			state.xform.Premultiply(ref t);
		}

		public void nvgCurrentTransform(Transform xform)
		{
			NanoVGContextState state = nvg__getState();

			state.xform = xform;
		}

		public void nvgStrokeColor(Color color)
		{
			NanoVGContextState state = nvg__getState();
			nvg__setPaintColor(ref state.stroke, (Color)(color));
		}

		public void nvgStrokePaint(Paint paint)
		{
			NanoVGContextState state = nvg__getState();
			state.stroke = paint;
			state.stroke.xform.Multiply(ref state.xform);
		}

		public void nvgFillColor(Color color)
		{
			NanoVGContextState state = nvg__getState();
			nvg__setPaintColor(ref state.fill, (Color)(color));
		}

		public void nvgFillPaint(Paint paint)
		{
			NanoVGContextState state = nvg__getState();
			state.fill = (Paint)(paint);
			state.fill.xform.Multiply(ref state.xform);
		}

		public int nvgCreateImageRGBA(int w, int h, int imageFlags, byte[] data)
		{
			return (int)(_renderer.renderCreateTexture((int)(NVG_TEXTURE_RGBA), (int)(w), (int)(h), (int)(imageFlags), data));
		}

		public void nvgUpdateImage(int image, byte[] data)
		{
			int w = 0;
			int h = 0;
			_renderer.renderGetTextureSize((int)(image), out w, out h);
			_renderer.renderUpdateTexture((int)(image), (int)(0), (int)(0), (int)(w), (int)(h), data);
		}

		public void nvgImageSize(int image, out int w, out int h)
		{
			_renderer.renderGetTextureSize((int)(image), out w, out h);
		}

		public void nvgDeleteImage(int image)
		{
			_renderer.renderDeleteTexture((int)(image));
		}

		public Paint nvgLinearGradient(float sx, float sy, float ex, float ey, Color icol, Color ocol)
		{
			Paint p = new Paint();
			float dx = 0;
			float dy = 0;
			float d = 0;
			float large = (float)(1e5);
			dx = (float)(ex - sx);
			dy = (float)(ey - sy);
			d = (float)(Math.Sqrt((float)(dx * dx + dy * dy)));
			if ((d) > (0.0001f))
			{
				dx /= (float)(d);
				dy /= (float)(d);
			}
			else
			{
				dx = (float)(0);
				dy = (float)(1);
			}

			p.xform.t1 = (float)(dy);
			p.xform.t2 = (float)(-dx);
			p.xform.t3 = (float)(dx);
			p.xform.t4 = (float)(dy);
			p.xform.t5 = (float)(sx - dx * large);
			p.xform.t6 = (float)(sy - dy * large);
			p.extent1 = (float)(large);
			p.extent2 = (float)(large + d * 0.5f);
			p.radius = (float)(0.0f);
			p.feather = (float)(nvg__maxf((float)(1.0f), (float)(d)));
			p.innerColor = (Color)(icol);
			p.outerColor = (Color)(ocol);
			return (Paint)(p);
		}

		public Paint nvgRadialGradient(float cx, float cy, float inr, float outr, Color icol, Color ocol)
		{
			Paint p = new Paint();
			float r = (float)((inr + outr) * 0.5f);
			float f = (float)(outr - inr);
			p.xform.SetIdentity();
			p.xform.t5 = (float)(cx);
			p.xform.t6 = (float)(cy);
			p.extent1 = (float)(r);
			p.extent2 = (float)(r);
			p.radius = (float)(r);
			p.feather = (float)(nvg__maxf((float)(1.0f), (float)(f)));
			p.innerColor = (Color)(icol);
			p.outerColor = (Color)(ocol);
			return (Paint)(p);
		}

		public Paint nvgBoxGradient(float x, float y, float w, float h, float r, float f, Color icol, Color ocol)
		{
			Paint p = new Paint();
			p.xform.SetIdentity();
			p.xform.t5 = (float)(x + w * 0.5f);
			p.xform.t6 = (float)(y + h * 0.5f);
			p.extent1 = (float)(w * 0.5f);
			p.extent2 = (float)(h * 0.5f);
			p.radius = (float)(r);
			p.feather = (float)(nvg__maxf((float)(1.0f), (float)(f)));
			p.innerColor = (Color)(icol);
			p.outerColor = (Color)(ocol);
			return (Paint)(p);
		}

		public Paint nvgImagePattern(float cx, float cy, float w, float h, float angle, int image, float alpha)
		{
			Paint p = new Paint();
			p.xform.SetRotate((float)(angle));
			p.xform.t5 = (float)(cx);
			p.xform.t6 = (float)(cy);
			p.extent1 = (float)(w);
			p.extent2 = (float)(h);
			p.image = (int)(image);
			p.innerColor = p.outerColor = new Color(1.0f, 1.0f, 1.0f, alpha);
			return (Paint)(p);
		}

		public void nvgScissor(float x, float y, float w, float h)
		{
			NanoVGContextState state = nvg__getState();
			w = (float)(nvg__maxf((float)(0.0f), (float)(w)));
			h = (float)(nvg__maxf((float)(0.0f), (float)(h)));
			state.scissor.xform.SetIdentity();
			state.scissor.xform.t5 = (float)(x + w * 0.5f);
			state.scissor.xform.t6 = (float)(y + h * 0.5f);
			state.scissor.xform.Multiply(ref state.xform);
			state.scissor.extent1 = (float)(w * 0.5f);
			state.scissor.extent2 = (float)(h * 0.5f);
		}

		public void nvgIntersectScissor(float x, float y, float w, float h)
		{
			NanoVGContextState state = nvg__getState();
			var pxform = new Transform();
			var invxorm = new Transform();
			float* rect = stackalloc float[4];
			float ex = 0;
			float ey = 0;
			float tex = 0;
			float tey = 0;
			if ((state.scissor.extent1) < (0))
			{
				nvgScissor((float)(x), (float)(y), (float)(w), (float)(h));
				return;
			}

			pxform = state.scissor.xform;
			ex = (float)(state.scissor.extent1);
			ey = (float)(state.scissor.extent2);
			invxorm = state.xform.BuildInverse();
			pxform.Multiply(ref invxorm);
			tex = (float)(ex * nvg__absf((float)(pxform.t1)) + ey * nvg__absf((float)(pxform.t3)));
			tey = (float)(ex * nvg__absf((float)(pxform.t2)) + ey * nvg__absf((float)(pxform.t4)));
			nvg__isectRects(rect, (float)(pxform.t5 - tex), (float)(pxform.t6 - tey), (float)(tex * 2), (float)(tey * 2), (float)(x), (float)(y), (float)(w), (float)(h));
			nvgScissor((float)(rect[0]), (float)(rect[1]), (float)(rect[2]), (float)(rect[3]));
		}

		public void nvgResetScissor()
		{
			NanoVGContextState state = nvg__getState();
			state.scissor.xform.Zero();
			state.scissor.extent1 = (float)(-1.0f);
			state.scissor.extent2 = (float)(-1.0f);
		}

		public void nvgGlobalCompositeOperation(int op)
		{
			NanoVGContextState state = nvg__getState();
			state.compositeOperation = (CompositeOperationState)(nvg__compositeOperationState((int)(op)));
		}

		public void nvgGlobalCompositeBlendFunc(int sfactor, int dfactor)
		{
			nvgGlobalCompositeBlendFuncSeparate((int)(sfactor), (int)(dfactor), (int)(sfactor), (int)(dfactor));
		}

		public void nvgGlobalCompositeBlendFuncSeparate(int srcRGB, int dstRGB, int srcAlpha, int dstAlpha)
		{
			CompositeOperationState op = new CompositeOperationState();
			op.srcRGB = (int)(srcRGB);
			op.dstRGB = (int)(dstRGB);
			op.srcAlpha = (int)(srcAlpha);
			op.dstAlpha = (int)(dstAlpha);
			NanoVGContextState state = nvg__getState();
			state.compositeOperation = (CompositeOperationState)(op);
		}

		public void nvg__appendCommands(float* vals, int nvals)
		{
			NanoVGContextState state = nvg__getState();
			int i = 0;
			if ((ncommands + nvals) > (ccommands))
			{
				float* commands;
				int ccommands = (int)(ncommands + nvals + this.ccommands / 2);
				commands = (float*)(CRuntime.realloc(this.commands, (ulong)(sizeof(float) * ccommands)));
				if ((commands) == null)
					return;
				this.commands = commands;
				this.ccommands = (int)(ccommands);
			}

			if (((int)(vals[0]) != NVG_CLOSE) && ((int)(vals[0]) != NVG_WINDING))
			{
				commandx = (float)(vals[nvals - 2]);
				commandy = (float)(vals[nvals - 1]);
			}

			i = (int)(0);
			while ((i) < (nvals))
			{
				int cmd = (int)(vals[i]);
				switch (cmd)
				{
					case NVG_MOVETO:
						state.xform.TransformPoint(out vals[i + 1], out vals[i + 2], (float)(vals[i + 1]), (float)(vals[i + 2]));
						i += (int)(3);
						break;
					case NVG_LINETO:
						state.xform.TransformPoint(out vals[i + 1], out vals[i + 2], (float)(vals[i + 1]), (float)(vals[i + 2]));
						i += (int)(3);
						break;
					case NVG_BEZIERTO:
						state.xform.TransformPoint(out vals[i + 1], out vals[i + 2], (float)(vals[i + 1]), (float)(vals[i + 2]));
						state.xform.TransformPoint(out vals[i + 3], out vals[i + 4], (float)(vals[i + 3]), (float)(vals[i + 4]));
						state.xform.TransformPoint(out vals[i + 5], out vals[i + 6], (float)(vals[i + 5]), (float)(vals[i + 6]));
						i += (int)(7);
						break;
					case NVG_CLOSE:
						i++;
						break;
					case NVG_WINDING:
						i += (int)(2);
						break;
					default:
						i++;
						break;
				}
			}
			CRuntime.memcpy(&commands[ncommands], vals, (ulong)(nvals * sizeof(float)));
			ncommands += (int)(nvals);
		}

		public void nvg__clearPathCache()
		{
			cache.paths.Clear();
		}

		public Path nvg__lastPath()
		{
			if ((cache.paths.Count) > (0))
				return cache.paths[cache.paths.Count - 1];
			return null;
		}

		public void nvg__addPath()
		{
			var newPath = new Path
			{
				first = cache.npoints,
				winding = NVG_CCW
			};

			cache.paths.Add(newPath);
		}

		private NVGpoint* nvg__lastPoint()
		{
			if ((cache.npoints) > (0))
				return &cache.points[cache.npoints - 1];
			return null;
		}

		public void nvg__addPoint(float x, float y, int flags)
		{
			Path path = nvg__lastPath();
			NVGpoint* pt;
			if ((path) == null)
				return;
			if (((path.count) > (0)) && ((cache.npoints) > (0)))
			{
				pt = nvg__lastPoint();
				if ((nvg__ptEquals((float)(pt->x), (float)(pt->y), (float)(x), (float)(y), (float)(distTol))) != 0)
				{
					pt->flags |= (byte)(flags);
					return;
				}
			}

			if ((cache.npoints + 1) > (cache.cpoints))
			{
				NVGpoint* points;
				int cpoints = (int)(cache.npoints + 1 + cache.cpoints / 2);
				points = (NVGpoint*)(CRuntime.realloc(cache.points, (ulong)(sizeof(NVGpoint) * cpoints)));
				if ((points) == null)
					return;
				cache.points = points;
				cache.cpoints = (int)(cpoints);
			}

			pt = &cache.points[cache.npoints];
			CRuntime.memset(pt, (int)(0), (ulong)(sizeof(NVGpoint)));
			pt->x = (float)(x);
			pt->y = (float)(y);
			pt->flags = ((byte)(flags));
			cache.npoints++;
			path.count++;
		}

		public void nvg__closePath()
		{
			Path path = nvg__lastPath();
			if ((path) == null)
				return;
			path.closed = (byte)(1);
		}

		public void nvg__pathWinding(int winding)
		{
			Path path = nvg__lastPath();
			if ((path) == null)
				return;
			path.winding = (int)(winding);
		}

		public ArraySegment<Vertex> nvg__allocTempVerts(int nverts)
		{
			cache.verts.EnsureSize(nverts);

			return new ArraySegment<Vertex>(cache.verts.Array);
		}

		public void nvg__tesselateBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, int level, int type)
		{
			float x12 = 0;
			float y12 = 0;
			float x23 = 0;
			float y23 = 0;
			float x34 = 0;
			float y34 = 0;
			float x123 = 0;
			float y123 = 0;
			float x234 = 0;
			float y234 = 0;
			float x1234 = 0;
			float y1234 = 0;
			float dx = 0;
			float dy = 0;
			float d2 = 0;
			float d3 = 0;
			if ((level) > (10))
				return;
			x12 = (float)((x1 + x2) * 0.5f);
			y12 = (float)((y1 + y2) * 0.5f);
			x23 = (float)((x2 + x3) * 0.5f);
			y23 = (float)((y2 + y3) * 0.5f);
			x34 = (float)((x3 + x4) * 0.5f);
			y34 = (float)((y3 + y4) * 0.5f);
			x123 = (float)((x12 + x23) * 0.5f);
			y123 = (float)((y12 + y23) * 0.5f);
			dx = (float)(x4 - x1);
			dy = (float)(y4 - y1);
			d2 = (float)(nvg__absf((float)((x2 - x4) * dy - (y2 - y4) * dx)));
			d3 = (float)(nvg__absf((float)((x3 - x4) * dy - (y3 - y4) * dx)));
			if (((d2 + d3) * (d2 + d3)) < (tessTol * (dx * dx + dy * dy)))
			{
				nvg__addPoint((float)(x4), (float)(y4), (int)(type));
				return;
			}

			x234 = (float)((x23 + x34) * 0.5f);
			y234 = (float)((y23 + y34) * 0.5f);
			x1234 = (float)((x123 + x234) * 0.5f);
			y1234 = (float)((y123 + y234) * 0.5f);
			nvg__tesselateBezier((float)(x1), (float)(y1), (float)(x12), (float)(y12), (float)(x123), (float)(y123), (float)(x1234), (float)(y1234), (int)(level + 1), (int)(0));
			nvg__tesselateBezier((float)(x1234), (float)(y1234), (float)(x234), (float)(y234), (float)(x34), (float)(y34), (float)(x4), (float)(y4), (int)(level + 1), (int)(type));
		}

		public void nvg__flattenPaths()
		{
			NVGpoint* last;
			NVGpoint* p0;
			NVGpoint* p1;
			NVGpoint* pts;
			Path path;
			int i = 0;
			int j = 0;
			float* cp1;
			float* cp2;
			float* p;
			float area = 0;
			if ((cache.paths.Count) > (0))
				return;
			i = (int)(0);
			while ((i) < (ncommands))
			{
				int cmd = (int)(commands[i]);
				switch (cmd)
				{
					case NVG_MOVETO:
						nvg__addPath();
						p = &commands[i + 1];
						nvg__addPoint((float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER));
						i += (int)(3);
						break;
					case NVG_LINETO:
						p = &commands[i + 1];
						nvg__addPoint((float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER));
						i += (int)(3);
						break;
					case NVG_BEZIERTO:
						last = nvg__lastPoint();
						if (last != null)
						{
							cp1 = &commands[i + 1];
							cp2 = &commands[i + 3];
							p = &commands[i + 5];
							nvg__tesselateBezier((float)(last->x), (float)(last->y), (float)(cp1[0]), (float)(cp1[1]), (float)(cp2[0]), (float)(cp2[1]), (float)(p[0]), (float)(p[1]), (int)(0), (int)(NVG_PT_CORNER));
						}
						i += (int)(7);
						break;
					case NVG_CLOSE:
						nvg__closePath();
						i++;
						break;
					case NVG_WINDING:
						nvg__pathWinding((int)(commands[i + 1]));
						i += (int)(2);
						break;
					default:
						i++;
						break;
				}
			}
			cache.bounds.b1 = (float)(cache.bounds.b2 = (float)(1e6f));
			cache.bounds.b3 = (float)(cache.bounds.b4 = (float)(-1e6f));
			for (j = (int)(0); (j) < (cache.paths.Count); j++)
			{
				path = cache.paths[j];
				pts = &cache.points[path.first];
				p0 = &pts[path.count - 1];
				p1 = &pts[0];
				if ((nvg__ptEquals((float)(p0->x), (float)(p0->y), (float)(p1->x), (float)(p1->y), (float)(distTol))) != 0)
				{
					path.count--;
					p0 = &pts[path.count - 1];
					path.closed = (byte)(1);
				}
				if ((path.count) > (2))
				{
					area = (float)(nvg__polyArea(pts, (int)(path.count)));
					if (((path.winding) == (NVG_CCW)) && ((area) < (0.0f)))
						nvg__polyReverse(pts, (int)(path.count));
					if (((path.winding) == (NVG_CW)) && ((area) > (0.0f)))
						nvg__polyReverse(pts, (int)(path.count));
				}
				for (i = (int)(0); (i) < (path.count); i++)
				{
					p0->dx = (float)(p1->x - p0->x);
					p0->dy = (float)(p1->y - p0->y);
					p0->len = (float)(nvg__normalize(&p0->dx, &p0->dy));
					cache.bounds.b1 = (float)(nvg__minf((float)(cache.bounds.b1), (float)(p0->x)));
					cache.bounds.b2 = (float)(nvg__minf((float)(cache.bounds.b2), (float)(p0->y)));
					cache.bounds.b3 = (float)(nvg__maxf((float)(cache.bounds.b3), (float)(p0->x)));
					cache.bounds.b4 = (float)(nvg__maxf((float)(cache.bounds.b4), (float)(p0->y)));
					p0 = p1++;
				}
			}
		}

		public void nvg__calculateJoins(float w, int lineJoin, float miterLimit)
		{
			int i = 0;
			int j = 0;
			float iw = (float)(0.0f);
			if ((w) > (0.0f))
				iw = (float)(1.0f / w);
			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				Path path = cache.paths[i];
				NVGpoint* pts = &cache.points[path.first];
				NVGpoint* p0 = &pts[path.count - 1];
				NVGpoint* p1 = &pts[0];
				int nleft = (int)(0);
				path.nbevel = (int)(0);
				for (j = (int)(0); (j) < (path.count); j++)
				{
					float dlx0 = 0;
					float dly0 = 0;
					float dlx1 = 0;
					float dly1 = 0;
					float dmr2 = 0;
					float cross = 0;
					float limit = 0;
					dlx0 = (float)(p0->dy);
					dly0 = (float)(-p0->dx);
					dlx1 = (float)(p1->dy);
					dly1 = (float)(-p1->dx);
					p1->dmx = (float)((dlx0 + dlx1) * 0.5f);
					p1->dmy = (float)((dly0 + dly1) * 0.5f);
					dmr2 = (float)(p1->dmx * p1->dmx + p1->dmy * p1->dmy);
					if ((dmr2) > (0.000001f))
					{
						float scale = (float)(1.0f / dmr2);
						if ((scale) > (600.0f))
						{
							scale = (float)(600.0f);
						}
						p1->dmx *= (float)(scale);
						p1->dmy *= (float)(scale);
					}
					p1->flags = (byte)(((p1->flags & NVG_PT_CORNER) != 0) ? NVG_PT_CORNER : 0);
					cross = (float)(p1->dx * p0->dy - p0->dx * p1->dy);
					if ((cross) > (0.0f))
					{
						nleft++;
						p1->flags |= (byte)(NVG_PT_LEFT);
					}
					limit = (float)(nvg__maxf((float)(1.01f), (float)(nvg__minf((float)(p0->len), (float)(p1->len)) * iw)));
					if ((dmr2 * limit * limit) < (1.0f))
						p1->flags |= (byte)(NVG_PR_INNERBEVEL);
					if ((p1->flags & NVG_PT_CORNER) != 0)
					{
						if ((((dmr2 * miterLimit * miterLimit) < (1.0f)) || ((lineJoin) == (NVG_BEVEL))) || ((lineJoin) == (NVG_ROUND)))
						{
							p1->flags |= (byte)(NVG_PT_BEVEL);
						}
					}
					if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
						path.nbevel++;
					p0 = p1++;
				}
				path.convex = (int)(((nleft) == (path.count)) ? 1 : 0);
			}
		}

		public int nvg__expandStroke(float w, float fringe, int lineCap, int lineJoin, float miterLimit)
		{
			int cverts = 0;
			int i = 0;
			int j = 0;
			float aa = (float)(fringe);
			float u0 = (float)(0.0f);
			float u1 = (float)(1.0f);
			int ncap = (int)(nvg__curveDivs((float)(w), (float)(3.14159274), (float)(tessTol)));
			w += (float)(aa * 0.5f);
			if ((aa) == (0.0f))
			{
				u0 = (float)(0.5f);
				u1 = (float)(0.5f);
			}

			nvg__calculateJoins((float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				Path path = cache.paths[i];
				int loop = (int)(((path.closed) == (0)) ? 0 : 1);
				if ((lineJoin) == (NVG_ROUND))
					cverts += (int)((path.count + path.nbevel * (ncap + 2) + 1) * 2);
				else
					cverts += (int)((path.count + path.nbevel * 5 + 1) * 2);
				if ((loop) == (0))
				{
					if ((lineCap) == (NVG_ROUND))
					{
						cverts += (int)((ncap * 2 + 2) * 2);
					}
					else
					{
						cverts += (int)((3 + 3) * 2);
					}
				}
			}
			var verts = nvg__allocTempVerts((int)(cverts));
			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				Path path = cache.paths[i];
				NVGpoint* pts = &cache.points[path.first];
				NVGpoint* p0;
				NVGpoint* p1;
				int s = 0;
				int e = 0;
				int loop = 0;
				float dx = 0;
				float dy = 0;
				path.fill = null;
				loop = (int)(((path.closed) == (0)) ? 0 : 1);
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					if ((loop) != 0)
					{
						p0 = &pts[path.count - 1];
						p1 = &pts[0];
						s = (int)(0);
						e = (int)(path.count);
					}
					else
					{
						p0 = &pts[0];
						p1 = &pts[1];
						s = (int)(1);
						e = (int)(path.count - 1);
					}
					if ((loop) == (0))
					{
						dx = (float)(p1->x - p0->x);
						dy = (float)(p1->y - p0->y);
						nvg__normalize(&dx, &dy);
						if ((lineCap) == (NVG_BUTT))
							dst = nvg__buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1));
						else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE)))
							dst = nvg__buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1));
						else if ((lineCap) == (NVG_ROUND))
							dst = nvg__roundCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1));
					}
					for (j = (int)(s); (j) < (e); ++j)
					{
						if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
						{
							if ((lineJoin) == (NVG_ROUND))
							{
								dst = nvg__roundJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (int)(ncap), (float)(aa));
							}
							else
							{
								dst = nvg__bevelJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (float)(aa));
							}
						}
						else
						{
							nvg__vset(dst, (float)(p1->x + (p1->dmx * w)), (float)(p1->y + (p1->dmy * w)), (float)(u0), (float)(1));
							dst++;
							nvg__vset(dst, (float)(p1->x - (p1->dmx * w)), (float)(p1->y - (p1->dmy * w)), (float)(u1), (float)(1));
							dst++;
						}
						p0 = p1++;
					}
					if ((loop) != 0)
					{
						nvg__vset(dst, (float)(verts.Array[verts.Offset].Position.X), (float)(verts.Array[verts.Offset].Position.Y), (float)(u0), (float)(1));
						dst++;
						nvg__vset(dst, (float)(verts.Array[verts.Offset + 1].Position.X), (float)(verts.Array[verts.Offset + 1].Position.Y), (float)(u1), (float)(1));
						dst++;
					}
					else
					{
						dx = (float)(p1->x - p0->x);
						dy = (float)(p1->y - p0->y);
						nvg__normalize(&dx, &dy);
						if ((lineCap) == (NVG_BUTT))
							dst = nvg__buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1));
						else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE)))
							dst = nvg__buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1));
						else if ((lineCap) == (NVG_ROUND))
							dst = nvg__roundCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1));
					}

					path.stroke = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.stroke.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}
			}
			return (int)(1);
		}

		public int nvg__expandFill(float w, int lineJoin, float miterLimit)
		{
			int cverts = 0;
			int convex = 0;
			int i = 0;
			int j = 0;
			float aa = (float)(fringeWidth);
			int fringe = (int)((w) > (0.0f) ? 1 : 0);
			nvg__calculateJoins((float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				Path path = cache.paths[i];
				cverts += (int)(path.count + path.nbevel + 1);
				if ((fringe) != 0)
					cverts += (int)((path.count + path.nbevel * 5 + 1) * 2);
			}
			var verts = nvg__allocTempVerts((int)(cverts));
			convex = (int)(((cache.paths.Count) == (1)) && ((cache.paths[0].convex) != 0) ? 1 : 0);
			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				Path path = cache.paths[i];
				NVGpoint* pts = &cache.points[path.first];
				NVGpoint* p0;
				NVGpoint* p1;
				float rw = 0;
				float lw = 0;
				float woff = 0;
				float ru = 0;
				float lu = 0;
				woff = (float)(0.5f * aa);
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					if ((fringe) != 0)
					{
						p0 = &pts[path.count - 1];
						p1 = &pts[0];
						for (j = (int)(0); (j) < (path.count); ++j)
						{
							if ((p1->flags & NVG_PT_BEVEL) != 0)
							{
								float dlx0 = (float)(p0->dy);
								float dly0 = (float)(-p0->dx);
								float dlx1 = (float)(p1->dy);
								float dly1 = (float)(-p1->dx);
								if ((p1->flags & NVG_PT_LEFT) != 0)
								{
									float lx = (float)(p1->x + p1->dmx * woff);
									float ly = (float)(p1->y + p1->dmy * woff);
									nvg__vset(dst, (float)(lx), (float)(ly), (float)(0.5f), (float)(1));
									dst++;
								}
								else
								{
									float lx0 = (float)(p1->x + dlx0 * woff);
									float ly0 = (float)(p1->y + dly0 * woff);
									float lx1 = (float)(p1->x + dlx1 * woff);
									float ly1 = (float)(p1->y + dly1 * woff);
									nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(0.5f), (float)(1));
									dst++;
									nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(0.5f), (float)(1));
									dst++;
								}
							}
							else
							{
								nvg__vset(dst, (float)(p1->x + (p1->dmx * woff)), (float)(p1->y + (p1->dmy * woff)), (float)(0.5f), (float)(1));
								dst++;
							}
							p0 = p1++;
						}
					}
					else
					{
						for (j = (int)(0); (j) < (path.count); ++j)
						{
							nvg__vset(dst, (float)(pts[j].x), (float)(pts[j].y), (float)(0.5f), (float)(1));
							dst++;
						}
					}

					path.fill = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.fill.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}
				if ((fringe) != 0)
				{
					lw = (float)(w + woff);
					rw = (float)(w - woff);
					lu = (float)(0);
					ru = (float)(1);
					fixed (Vertex* dst2 = &verts.Array[verts.Offset])
					{
						var dst = dst2;
						if ((convex) != 0)
						{
							lw = (float)(woff);
							lu = (float)(0.5f);
						}
						p0 = &pts[path.count - 1];
						p1 = &pts[0];
						for (j = (int)(0); (j) < (path.count); ++j)
						{
							if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
							{
								dst = nvg__bevelJoin(dst, p0, p1, (float)(lw), (float)(rw), (float)(lu), (float)(ru), (float)(fringeWidth));
							}
							else
							{
								nvg__vset(dst, (float)(p1->x + (p1->dmx * lw)), (float)(p1->y + (p1->dmy * lw)), (float)(lu), (float)(1));
								dst++;
								nvg__vset(dst, (float)(p1->x - (p1->dmx * rw)), (float)(p1->y - (p1->dmy * rw)), (float)(ru), (float)(1));
								dst++;
							}
							p0 = p1++;
						}
						nvg__vset(dst, (float)(verts.Array[verts.Offset].Position.X),
							(float)(verts.Array[verts.Offset].Position.Y),
							(float)(lu), (float)(1));
						dst++;
						nvg__vset(dst, (float)(verts.Array[verts.Offset + 1].Position.X),
							(float)(verts.Array[verts.Offset + 1].Position.Y),
							(float)(ru), (float)(1));
						dst++;

						path.stroke = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

						var newPos = verts.Offset + path.stroke.Value.Count;
						verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
					}
				}
				else
				{
					path.stroke = null;
				}
			}

			return (int)(1);
		}

		public void nvgBeginPath()
		{
			ncommands = (int)(0);
			nvg__clearPathCache();
		}

		public void nvgMoveTo(float x, float y)
		{
			float* vals = stackalloc float[3];
			vals[0] = (float)(NVG_MOVETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);

			nvg__appendCommands(vals, 3);
		}

		public void nvgLineTo(float x, float y)
		{
			float* vals = stackalloc float[3];
			vals[0] = (float)(NVG_LINETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);

			nvg__appendCommands(vals, 3);
		}

		public void nvgBezierTo(float c1x, float c1y, float c2x, float c2y, float x, float y)
		{
			float* vals = stackalloc float[7];
			vals[0] = (float)(NVG_BEZIERTO);
			vals[1] = (float)(c1x);
			vals[2] = (float)(c1y);
			vals[3] = (float)(c2x);
			vals[4] = (float)(c2y);
			vals[5] = (float)(x);
			vals[6] = (float)(y);

			nvg__appendCommands(vals, 7);
		}

		public void nvgQuadTo(float cx, float cy, float x, float y)
		{
			float x0 = (float)(commandx);
			float y0 = (float)(commandy);
			float* vals = stackalloc float[7];
			vals[0] = (float)(NVG_BEZIERTO);
			vals[1] = (float)(x0 + 2.0f / 3.0f * (cx - x0));
			vals[2] = (float)(y0 + 2.0f / 3.0f * (cy - y0));
			vals[3] = (float)(x + 2.0f / 3.0f * (cx - x));
			vals[4] = (float)(y + 2.0f / 3.0f * (cy - y));
			vals[5] = (float)(x);
			vals[6] = (float)(y);

			nvg__appendCommands(vals, 7);
		}

		public void nvgArcTo(float x1, float y1, float x2, float y2, float radius)
		{
			float x0 = (float)(commandx);
			float y0 = (float)(commandy);
			float dx0 = 0;
			float dy0 = 0;
			float dx1 = 0;
			float dy1 = 0;
			float a = 0;
			float d = 0;
			float cx = 0;
			float cy = 0;
			float a0 = 0;
			float a1 = 0;
			int dir = 0;
			if ((ncommands) == (0))
			{
				return;
			}

			if (((((nvg__ptEquals((float)(x0), (float)(y0), (float)(x1), (float)(y1), (float)(distTol))) != 0) || ((nvg__ptEquals((float)(x1), (float)(y1), (float)(x2), (float)(y2), (float)(distTol))) != 0)) || ((nvg__distPtSeg((float)(x1), (float)(y1), (float)(x0), (float)(y0), (float)(x2), (float)(y2))) < (distTol * distTol))) || ((radius) < (distTol)))
			{
				nvgLineTo((float)(x1), (float)(y1));
				return;
			}

			dx0 = (float)(x0 - x1);
			dy0 = (float)(y0 - y1);
			dx1 = (float)(x2 - x1);
			dy1 = (float)(y2 - y1);
			nvg__normalize(&dx0, &dy0);
			nvg__normalize(&dx1, &dy1);
			a = (float)(acosf((float)(dx0 * dx1 + dy0 * dy1)));
			d = (float)(radius / tanf((float)(a / 2.0f)));
			if ((d) > (10000.0f))
			{
				nvgLineTo((float)(x1), (float)(y1));
				return;
			}

			if ((nvg__cross((float)(dx0), (float)(dy0), (float)(dx1), (float)(dy1))) > (0.0f))
			{
				cx = (float)(x1 + dx0 * d + dy0 * radius);
				cy = (float)(y1 + dy0 * d + -dx0 * radius);
				a0 = (float)(atan2f((float)(dx0), (float)(-dy0)));
				a1 = (float)(atan2f((float)(-dx1), (float)(dy1)));
				dir = (int)(NVG_CW);
			}
			else
			{
				cx = (float)(x1 + dx0 * d + -dy0 * radius);
				cy = (float)(y1 + dy0 * d + dx0 * radius);
				a0 = (float)(atan2f((float)(-dx0), (float)(dy0)));
				a1 = (float)(atan2f((float)(dx1), (float)(-dy1)));
				dir = (int)(NVG_CCW);
			}

			nvgArc((float)(cx), (float)(cy), (float)(radius), (float)(a0), (float)(a1), (int)(dir));
		}

		public void nvgClosePath()
		{
			float* vals = stackalloc float[1];
			vals[0] = (float)(NVG_CLOSE);

			nvg__appendCommands(vals, 1);
		}

		public void nvgPathWinding(int dir)
		{
			float* vals = stackalloc float[2];
			vals[0] = (float)(NVG_WINDING);
			vals[1] = (float)(dir);

			nvg__appendCommands(vals, 2);
		}

		public void nvgArc(float cx, float cy, float r, float a0, float a1, int dir)
		{
			float a = (float)(0);
			float da = (float)(0);
			float hda = (float)(0);
			float kappa = (float)(0);
			float dx = (float)(0);
			float dy = (float)(0);
			float x = (float)(0);
			float y = (float)(0);
			float tanx = (float)(0);
			float tany = (float)(0);
			float px = (float)(0);
			float py = (float)(0);
			float ptanx = (float)(0);
			float ptany = (float)(0);
			float* vals = stackalloc float[3 + 5 * 7 + 100];
			int i = 0;
			int ndivs = 0;
			int nvals = 0;
			int move = (int)((ncommands) > (0) ? NVG_LINETO : NVG_MOVETO);
			da = (float)(a1 - a0);
			if ((dir) == (NVG_CW))
			{
				if ((nvg__absf((float)(da))) >= (3.14159274 * 2))
				{
					da = (float)(3.14159274 * 2);
				}
				else
				{
					while ((da) < (0.0f))
					{
						da += (float)(3.14159274 * 2);
					}
				}
			}
			else
			{
				if ((nvg__absf((float)(da))) >= (3.14159274 * 2))
				{
					da = (float)(-3.14159274 * 2);
				}
				else
				{
					while ((da) > (0.0f))
					{
						da -= (float)(3.14159274 * 2);
					}
				}
			}

			ndivs = (int)(nvg__maxi((int)(1), (int)(nvg__mini((int)(nvg__absf((float)(da)) / (3.14159274 * 0.5f) + 0.5f), (int)(5)))));
			hda = (float)((da / (float)(ndivs)) / 2.0f);
			kappa = (float)(nvg__absf((float)(4.0f / 3.0f * (1.0f - cosf((float)(hda))) / sinf((float)(hda)))));
			if ((dir) == (NVG_CCW))
				kappa = (float)(-kappa);
			nvals = (int)(0);
			for (i = (int)(0); i <= ndivs; i++)
			{
				a = (float)(a0 + da * (i / (float)(ndivs)));
				dx = (float)(cosf((float)(a)));
				dy = (float)(sinf((float)(a)));
				x = (float)(cx + dx * r);
				y = (float)(cy + dy * r);
				tanx = (float)(-dy * r * kappa);
				tany = (float)(dx * r * kappa);
				if ((i) == (0))
				{
					vals[nvals++] = ((float)(move));
					vals[nvals++] = (float)(x);
					vals[nvals++] = (float)(y);
				}
				else
				{
					vals[nvals++] = (float)(NVG_BEZIERTO);
					vals[nvals++] = (float)(px + ptanx);
					vals[nvals++] = (float)(py + ptany);
					vals[nvals++] = (float)(x - tanx);
					vals[nvals++] = (float)(y - tany);
					vals[nvals++] = (float)(x);
					vals[nvals++] = (float)(y);
				}
				px = (float)(x);
				py = (float)(y);
				ptanx = (float)(tanx);
				ptany = (float)(tany);
			}
			nvg__appendCommands(vals, (int)(nvals));
		}

		public void nvgRect(float x, float y, float w, float h)
		{
			float* vals = stackalloc float[13];
			vals[0] = (float)(NVG_MOVETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);
			vals[3] = (float)(NVG_LINETO);
			vals[4] = (float)(x);
			vals[5] = (float)(y + h);
			vals[6] = (float)(NVG_LINETO);
			vals[7] = (float)(x + w);
			vals[8] = (float)(y + h);
			vals[9] = (float)(NVG_LINETO);
			vals[10] = (float)(x + w);
			vals[11] = (float)(y);
			vals[12] = (float)(NVG_CLOSE);

			nvg__appendCommands(vals, 13);
		}

		public void nvgRoundedRect(float x, float y, float w, float h, float r)
		{
			nvgRoundedRectVarying((float)(x), (float)(y), (float)(w), (float)(h), (float)(r), (float)(r), (float)(r), (float)(r));
		}

		public void nvgRoundedRectVarying(float x, float y, float w, float h, float radTopLeft, float radTopRight, float radBottomRight, float radBottomLeft)
		{
			if (((((radTopLeft) < (0.1f)) && ((radTopRight) < (0.1f))) && ((radBottomRight) < (0.1f))) && ((radBottomLeft) < (0.1f)))
			{
				nvgRect((float)(x), (float)(y), (float)(w), (float)(h));
				return;
			}
			else
			{
				float halfw = (float)(nvg__absf((float)(w)) * 0.5f);
				float halfh = (float)(nvg__absf((float)(h)) * 0.5f);
				float rxBL = (float)(nvg__minf((float)(radBottomLeft), (float)(halfw)) * nvg__signf((float)(w)));
				float ryBL = (float)(nvg__minf((float)(radBottomLeft), (float)(halfh)) * nvg__signf((float)(h)));
				float rxBR = (float)(nvg__minf((float)(radBottomRight), (float)(halfw)) * nvg__signf((float)(w)));
				float ryBR = (float)(nvg__minf((float)(radBottomRight), (float)(halfh)) * nvg__signf((float)(h)));
				float rxTR = (float)(nvg__minf((float)(radTopRight), (float)(halfw)) * nvg__signf((float)(w)));
				float ryTR = (float)(nvg__minf((float)(radTopRight), (float)(halfh)) * nvg__signf((float)(h)));
				float rxTL = (float)(nvg__minf((float)(radTopLeft), (float)(halfw)) * nvg__signf((float)(w)));
				float ryTL = (float)(nvg__minf((float)(radTopLeft), (float)(halfh)) * nvg__signf((float)(h)));
				float* vals = stackalloc float[44];
				vals[0] = (float)(NVG_MOVETO);
				vals[1] = (float)(x);
				vals[2] = (float)(y + ryTL);
				vals[3] = (float)(NVG_LINETO);
				vals[4] = (float)(x);
				vals[5] = (float)(y + h - ryBL);
				vals[6] = (float)(NVG_BEZIERTO);
				vals[7] = (float)(x);
				vals[8] = (float)(y + h - ryBL * (1 - 0.5522847493f));
				vals[9] = (float)(x + rxBL * (1 - 0.5522847493f));
				vals[10] = (float)(y + h);
				vals[11] = (float)(x + rxBL);
				vals[12] = (float)(y + h);
				vals[13] = (float)(NVG_LINETO);
				vals[14] = (float)(x + w - rxBR);
				vals[15] = (float)(y + h);
				vals[16] = (float)(NVG_BEZIERTO);
				vals[17] = (float)(x + w - rxBR * (1 - 0.5522847493f));
				vals[18] = (float)(y + h);
				vals[19] = (float)(x + w);
				vals[20] = (float)(y + h - ryBR * (1 - 0.5522847493f));
				vals[21] = (float)(x + w);
				vals[22] = (float)(y + h - ryBR);
				vals[23] = (float)(NVG_LINETO);
				vals[24] = (float)(x + w);
				vals[25] = (float)(y + ryTR);
				vals[26] = (float)(NVG_BEZIERTO);
				vals[27] = (float)(x + w);
				vals[28] = (float)(y + ryTR * (1 - 0.5522847493f));
				vals[29] = (float)(x + w - rxTR * (1 - 0.5522847493f));
				vals[30] = (float)(y);
				vals[31] = (float)(x + w - rxTR);
				vals[32] = (float)(y);
				vals[33] = (float)(NVG_LINETO);
				vals[34] = (float)(x + rxTL);
				vals[35] = (float)(y);
				vals[36] = (float)(NVG_BEZIERTO);
				vals[37] = (float)(x + rxTL * (1 - 0.5522847493f));
				vals[38] = (float)(y);
				vals[39] = (float)(x);
				vals[40] = (float)(y + ryTL * (1 - 0.5522847493f));
				vals[41] = (float)(x);
				vals[42] = (float)(y + ryTL);
				vals[43] = (float)(NVG_CLOSE);
				nvg__appendCommands(vals, 44);
			}

		}

		public void nvgEllipse(float cx, float cy, float rx, float ry)
		{
			float* vals = stackalloc float[32];
			vals[0] = (float)(NVG_MOVETO);
			vals[1] = (float)(cx - rx);
			vals[2] = (float)(cy);
			vals[3] = (float)(NVG_BEZIERTO);
			vals[4] = (float)(cx - rx);
			vals[5] = (float)(cy + ry * 0.5522847493f);
			vals[6] = (float)(cx - rx * 0.5522847493f);
			vals[7] = (float)(cy + ry);
			vals[8] = (float)(cx);
			vals[9] = (float)(cy + ry);
			vals[10] = (float)(NVG_BEZIERTO);
			vals[11] = (float)(cx + rx * 0.5522847493f);
			vals[12] = (float)(cy + ry);
			vals[13] = (float)(cx + rx);
			vals[14] = (float)(cy + ry * 0.5522847493f);
			vals[15] = (float)(cx + rx);
			vals[16] = (float)(cy);
			vals[17] = (float)(NVG_BEZIERTO);
			vals[18] = (float)(cx + rx);
			vals[19] = (float)(cy - ry * 0.5522847493f);
			vals[20] = (float)(cx + rx * 0.5522847493f);
			vals[21] = (float)(cy - ry);
			vals[22] = (float)(cx);
			vals[23] = (float)(cy - ry);
			vals[24] = (float)(NVG_BEZIERTO);
			vals[25] = (float)(cx - rx * 0.5522847493f);
			vals[26] = (float)(cy - ry);
			vals[27] = (float)(cx - rx);
			vals[28] = (float)(cy - ry * 0.5522847493f);
			vals[29] = (float)(cx - rx);
			vals[30] = (float)(cy);
			vals[31] = (float)(NVG_CLOSE);

			nvg__appendCommands(vals, 32);
		}

		public void nvgCircle(float cx, float cy, float r)
		{
			nvgEllipse((float)(cx), (float)(cy), (float)(r), (float)(r));
		}

		/*		public void nvgDebugDumpPathCache()
				{
					NVGpath path;
					int i = 0;
					int j = 0;
					printf("Dumping %d cached paths\n", (int)(cache.paths.Count));
					for (i = (int)(0); (i) < (cache.paths.Count); i++)
					{
						path = &cache.paths[i];
						printf(" - Path %d\n", (int)(i));
						if ((path.nfill) != 0)
						{
							printf("   - fill: %d\n", (int)(path.nfill));
							for (j = (int)(0); (j) < (path.nfill); j++)
							{
								printf("%f\t%f\n", (double)(path.fill[j].x), (double)(path.fill[j].y));
							}
						}
						if ((path.nstroke) != 0)
						{
							printf("   - stroke: %d\n", (int)(path.nstroke));
							for (j = (int)(0); (j) < (path.nstroke); j++)
							{
								printf("%f\t%f\n", (double)(path.stroke[j].x), (double)(path.stroke[j].y));
							}
						}
					}
				}*/

		private static void MultiplyAlpha(ref Color c, float alpha)
		{
			var na = (int)((float)c.A * alpha);

			c = new Color(c.R, c.G, c.B, na);
		}

		public void nvgFill()
		{
			NanoVGContextState state = nvg__getState();
			Path path;
			Paint fillPaint = (Paint)(state.fill);
			int i = 0;
			nvg__flattenPaths();
			if (((edgeAntiAlias) != 0) && ((state.shapeAntiAlias) != 0))
				nvg__expandFill((float)(fringeWidth), (int)(NVG_MITER), (float)(2.4f));
			else
				nvg__expandFill((float)(0.0f), (int)(NVG_MITER), (float)(2.4f));
			MultiplyAlpha(ref fillPaint.innerColor, state.alpha);
			MultiplyAlpha(ref fillPaint.outerColor, state.alpha);
			_renderer.renderFill(ref fillPaint, (CompositeOperationState)(state.compositeOperation),
				ref state.scissor, (float)(fringeWidth), cache.bounds, cache.paths.ToArraySegment());

			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				path = cache.paths[i];
				if (path.fill != null)
				{
					fillTriCount += (int)(path.fill.Value.Count - 2);
				}

				if (path.stroke != null)
				{
					fillTriCount += (int)(path.stroke.Value.Count - 2);
				}
				drawCallCount += (int)(2);
			}
		}

		public void nvgStroke()
		{
			NanoVGContextState state = nvg__getState();
			float scale = (float)(nvg__getAverageScale(ref state.xform));
			float strokeWidth = (float)(nvg__clampf((float)(state.strokeWidth * scale), (float)(0.0f), (float)(200.0f)));
			Paint strokePaint = (Paint)(state.stroke);
			Path path;
			int i = 0;
			if ((strokeWidth) < (fringeWidth))
			{
				float alpha = (float)(nvg__clampf((float)(strokeWidth / fringeWidth), (float)(0.0f), (float)(1.0f)));

				MultiplyAlpha(ref strokePaint.innerColor, alpha * alpha);
				MultiplyAlpha(ref strokePaint.outerColor, alpha * alpha);
				strokeWidth = fringeWidth;
			}

			MultiplyAlpha(ref strokePaint.innerColor, state.alpha);
			MultiplyAlpha(ref strokePaint.outerColor, state.alpha);

			nvg__flattenPaths();
			if (((edgeAntiAlias) != 0) && ((state.shapeAntiAlias) != 0))
				nvg__expandStroke((float)(strokeWidth * 0.5f), (float)(fringeWidth), (int)(state.lineCap), (int)(state.lineJoin), (float)(state.miterLimit));
			else
				nvg__expandStroke((float)(strokeWidth * 0.5f), (float)(0.0f), (int)(state.lineCap), (int)(state.lineJoin), (float)(state.miterLimit));
			_renderer.renderStroke(ref strokePaint, (CompositeOperationState)(state.compositeOperation), ref state.scissor,
				(float)(fringeWidth), (float)(strokeWidth), cache.paths.ToArraySegment());
			for (i = (int)(0); (i) < (cache.paths.Count); i++)
			{
				path = cache.paths[i];
				strokeTriCount += (int)(path.stroke.Value.Count - 2);
				drawCallCount++;
			}
		}

		public int nvgCreateFontMem(string name, byte[] data, int freeData)
		{
			return (int)(fs.fonsAddFontMem(name, data, (int)(freeData)));
		}

		public int nvgFindFont(string name)
		{
			if ((name) == null)
				return (int)(-1);
			return (int)(fs.fonsGetFontByName(name));
		}

		public int nvgAddFallbackFontId(int baseFont, int fallbackFont)
		{
			if (((baseFont) == (-1)) || ((fallbackFont) == (-1)))
				return (int)(0);
			return (int)(fs.fonsAddFallbackFont((int)(baseFont), (int)(fallbackFont)));
		}

		public int nvgAddFallbackFont(string baseFont, string fallbackFont)
		{
			return (int)(nvgAddFallbackFontId((int)(nvgFindFont(baseFont)), (int)(nvgFindFont(fallbackFont))));
		}

		public void nvgFontSize(float size)
		{
			NanoVGContextState state = nvg__getState();
			state.fontSize = (float)(size);
		}

		public void nvgFontBlur(float blur)
		{
			NanoVGContextState state = nvg__getState();
			state.fontBlur = (float)(blur);
		}

		public void nvgTextLetterSpacing(float spacing)
		{
			NanoVGContextState state = nvg__getState();
			state.letterSpacing = (float)(spacing);
		}

		public void nvgTextLineHeight(float lineHeight)
		{
			NanoVGContextState state = nvg__getState();
			state.lineHeight = (float)(lineHeight);
		}

		public void nvgTextAlign(int align)
		{
			NanoVGContextState state = nvg__getState();
			state.textAlign = (int)(align);
		}

		public void nvgFontFaceId(int font)
		{
			NanoVGContextState state = nvg__getState();
			state.fontId = (int)(font);
		}

		public void nvgFontFace(string font)
		{
			NanoVGContextState state = nvg__getState();
			state.fontId = (int)(fs.fonsGetFontByName(font));
		}

		public void nvg__flushTextTexture()
		{
			int* dirty = stackalloc int[4];
			if ((fs.fonsValidateTexture(dirty)) != 0)
			{
				int fontImage = (int)(fontImages[fontImageIdx]);
				if (fontImage != 0)
				{
					int iw = 0;
					int ih = 0;
					byte[] data = fs.fonsGetTextureData(&iw, &ih);
					int x = (int)(dirty[0]);
					int y = (int)(dirty[1]);
					int w = (int)(dirty[2] - dirty[0]);
					int h = (int)(dirty[3] - dirty[1]);
					_renderer.renderUpdateTexture((int)(fontImage), (int)(x), (int)(y), (int)(w), (int)(h), data);
				}
			}
		}

		public int nvg__allocTextAtlas()
		{
			int iw = 0;
			int ih = 0;
			nvg__flushTextTexture();
			if ((fontImageIdx) >= (4 - 1))
				return (int)(0);
			if (fontImages[fontImageIdx + 1] != 0)
				nvgImageSize((int)(fontImages[fontImageIdx + 1]), out iw, out ih);
			else
			{
				nvgImageSize((int)(fontImages[fontImageIdx]), out iw, out ih);
				if ((iw) > (ih))
					ih *= (int)(2);
				else
					iw *= (int)(2);
				if (((iw) > (2048)) || ((ih) > (2048)))
					iw = (int)(ih = (int)(2048));
				fontImages[fontImageIdx + 1] = (int)(_renderer.renderCreateTexture((int)(NVG_TEXTURE_ALPHA), (int)(iw), (int)(ih), (int)(0), null));
			}

			++fontImageIdx;
			fs.fonsResetAtlas((int)(iw), (int)(ih));
			return (int)(1);
		}

		public void nvg__renderText(ArraySegment<Vertex> verts)
		{
			NanoVGContextState state = nvg__getState();
			Paint paint = (Paint)(state.fill);
			paint.image = (int)(fontImages[fontImageIdx]);

			MultiplyAlpha(ref paint.innerColor, state.alpha);
			MultiplyAlpha(ref paint.outerColor, state.alpha);

			_renderer.renderTriangles(ref paint, state.compositeOperation, ref state.scissor, verts);
			drawCallCount++;
			textTriCount += (int)(verts.Count / 3);
		}

		public float nvgText(float x, float y, StringSegment _string_)
		{
			NanoVGContextState state = nvg__getState();
			FontTextIterator iter = new FontTextIterator();
			FontTextIterator prevIter = new FontTextIterator();
			FontGlyphSquad q = new FontGlyphSquad();
			float scale = (float)(nvg__getFontScale(state) * devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int cverts = (int)(0);
			int nverts = (int)(0);
			if ((state.fontId) == (-1))
				return (float)(x);
			fs.fonsSetSize((float)(state.fontSize * scale));
			fs.fonsSetSpacing((float)(state.letterSpacing * scale));
			fs.fonsSetBlur((float)(state.fontBlur * scale));
			fs.fonsSetAlign((int)(state.textAlign));
			fs.fonsSetFont((int)(state.fontId));
			cverts = (int)(nvg__maxi((int)(2), (int)(_string_.Length)) * 6);
			var verts = nvg__allocTempVerts((int)(cverts));

			fs.fonsTextIterInit(iter, (float)(x * scale), (float)(y * scale), _string_, (int)(FontSystem.FONS_GLYPH_BITMAP_REQUIRED));
			prevIter = (FontTextIterator)(iter);

			while (fs.fonsTextIterNext(iter, &q))
			{
				float* c = stackalloc float[4 * 2];
				if ((iter.prevGlyphIndex) == (-1))
				{
					if (nverts != 0)
					{
						var segment = new ArraySegment<Vertex>(verts.Array, verts.Offset, nverts);
						nvg__renderText(segment);
						nverts = (int)(0);
					}
					if (nvg__allocTextAtlas() == 0)
						break;
					iter = (FontTextIterator)(prevIter);
					fs.fonsTextIterNext(iter, &q);
					if ((iter.prevGlyphIndex) == (-1))
						break;
				}
				prevIter = (FontTextIterator)(iter);
				state.xform.TransformPoint(out c[0], out c[1], (float)(q.x0 * invscale), (float)(q.y0 * invscale));
				state.xform.TransformPoint(out c[2], out c[3], (float)(q.x1 * invscale), (float)(q.y0 * invscale));
				state.xform.TransformPoint(out c[4], out c[5], (float)(q.x1 * invscale), (float)(q.y1 * invscale));
				state.xform.TransformPoint(out c[6], out c[7], (float)(q.x0 * invscale), (float)(q.y1 * invscale));
				if (nverts + 6 <= cverts)
				{
					nvg__vset(ref verts.Array[verts.Offset + nverts], (float)(c[0]), (float)(c[1]), (float)(q.s0), (float)(q.t0));
					nverts++;
					nvg__vset(ref verts.Array[verts.Offset + nverts], (float)(c[4]), (float)(c[5]), (float)(q.s1), (float)(q.t1));
					nverts++;
					nvg__vset(ref verts.Array[verts.Offset + nverts], (float)(c[2]), (float)(c[3]), (float)(q.s1), (float)(q.t0));
					nverts++;
					nvg__vset(ref verts.Array[verts.Offset + nverts], (float)(c[0]), (float)(c[1]), (float)(q.s0), (float)(q.t0));
					nverts++;
					nvg__vset(ref verts.Array[verts.Offset + nverts], (float)(c[6]), (float)(c[7]), (float)(q.s0), (float)(q.t1));
					nverts++;
					nvg__vset(ref verts.Array[verts.Offset + nverts], (float)(c[4]), (float)(c[5]), (float)(q.s1), (float)(q.t1));
					nverts++;
				}
			}

			nvg__flushTextTexture();

			var segment2 = new ArraySegment<Vertex>(verts.Array, verts.Offset, nverts);
			nvg__renderText(segment2);

			return (float)(iter.nextx / scale);
		}

		public void nvgTextBox(float x, float y, float breakRowWidth, StringSegment _string_)
		{
			NanoVGContextState state = nvg__getState();
			int i = 0;
			int oldAlign = (int)(state.textAlign);
			int haling = (int)(state.textAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state.textAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);
			if ((state.fontId) == (-1))
				return;
			float ascender, descender;
			nvgTextMetrics(out ascender, out descender, out lineh);
			state.textAlign = (int)(NVG_ALIGN_LEFT | valign);
			while (true)
			{
				var nrows = (int)(nvgTextBreakLines(_string_, (float)(breakRowWidth), _rows, out _string_));

				if (nrows <= 0)
				{
					break;
				}
				for (i = (int)(0); (i) < (nrows); i++)
				{
					var row = _rows[i];
					if ((haling & NVG_ALIGN_LEFT) != 0)
						nvgText((float)(x), (float)(y), row.str);
					else if ((haling & NVG_ALIGN_CENTER) != 0)
						nvgText((float)(x + breakRowWidth * 0.5f - row.width * 0.5f), (float)(y), row.str);
					else if ((haling & NVG_ALIGN_RIGHT) != 0)
						nvgText((float)(x + breakRowWidth - row.width), (float)(y), row.str);
					y += (float)(lineh * state.lineHeight);
				}
			}
			state.textAlign = (int)(oldAlign);
		}

		public int nvgTextGlyphPositions(float x, float y, StringSegment _string_, GlyphPosition[] positions)
		{
			NanoVGContextState state = nvg__getState();
			float scale = (float)(nvg__getFontScale(state) * devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FontTextIterator iter = new FontTextIterator();
			FontTextIterator prevIter = new FontTextIterator();
			FontGlyphSquad q = new FontGlyphSquad();
			int npos = (int)(0);
			if ((state.fontId) == (-1))
				return (int)(0);

			if (_string_.IsNullOrEmpty)
			{
				return 0;
			}

			fs.fonsSetSize((float)(state.fontSize * scale));
			fs.fonsSetSpacing((float)(state.letterSpacing * scale));
			fs.fonsSetBlur((float)(state.fontBlur * scale));
			fs.fonsSetAlign((int)(state.textAlign));
			fs.fonsSetFont((int)(state.fontId));
			fs.fonsTextIterInit(iter, (float)(x * scale), (float)(y * scale), _string_, (int)(FontSystem.FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FontTextIterator)(iter);
			while (fs.fonsTextIterNext(iter, &q))
			{
				if (((iter.prevGlyphIndex) < (0)) && ((nvg__allocTextAtlas()) != 0))
				{
					iter = (FontTextIterator)(prevIter);
					fs.fonsTextIterNext(iter, &q);
				}
				prevIter = (FontTextIterator)(iter);
				positions[npos].str = iter.str;
				positions[npos].x = (float)(iter.x * invscale);
				positions[npos].minx = (float)(nvg__minf((float)(iter.x), (float)(q.x0)) * invscale);
				positions[npos].maxx = (float)(nvg__maxf((float)(iter.nextx), (float)(q.x1)) * invscale);
				npos++;
				if ((npos) >= (positions.Length))
					break;
			}
			return (int)(npos);
		}

		public int nvgTextBreakLines(StringSegment _string_, float breakRowWidth, TextRow[] rows, out StringSegment remaining)
		{
			remaining = StringSegment.Null;

			NanoVGContextState state = nvg__getState();
			float scale = (float)(nvg__getFontScale(state) * devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FontTextIterator iter = new FontTextIterator();
			FontTextIterator prevIter = new FontTextIterator();
			FontGlyphSquad q = new FontGlyphSquad();
			int nrows = (int)(0);
			float rowStartX = (float)(0);
			float rowWidth = (float)(0);
			float rowMinX = (float)(0);
			float rowMaxX = (float)(0);
			int? rowStart = null;
			int? rowEnd = null;
			int? wordStart = null;
			int? breakEnd = null;
			float wordStartX = (float)(0);
			float wordMinX = (float)(0);
			float breakWidth = (float)(0);
			float breakMaxX = (float)(0);
			int type = (int)(NVG_SPACE);
			int ptype = (int)(NVG_SPACE);
			uint pcodepoint = (uint)(0);

			if ((state.fontId) == (-1))
				return (int)(0);

			if (_string_.IsNullOrEmpty)
			{
				return 0;
			}
			fs.fonsSetSize((float)(state.fontSize * scale));
			fs.fonsSetSpacing((float)(state.letterSpacing * scale));
			fs.fonsSetBlur((float)(state.fontBlur * scale));
			fs.fonsSetAlign((int)(state.textAlign));
			fs.fonsSetFont((int)(state.fontId));
			breakRowWidth *= (float)(scale);
			fs.fonsTextIterInit(iter, (float)(0), (float)(0), _string_, (int)(FontSystem.FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FontTextIterator)(iter);
			while ((fs.fonsTextIterNext(iter, &q)))
			{
				if (((iter.prevGlyphIndex) < (0)) && ((nvg__allocTextAtlas()) != 0))
				{
					iter = (FontTextIterator)(prevIter);
					fs.fonsTextIterNext(iter, &q);
				}
				prevIter = (FontTextIterator)(iter);
				switch (iter.codepoint)
				{
					case 9:
					case 11:
					case 12:
					case 32:
					case 0x00a0:
						type = (int)(NVG_SPACE);
						break;
					case 10:
						type = (int)((pcodepoint) == (13) ? NVG_SPACE : NVG_NEWLINE);
						break;
					case 13:
						type = (int)((pcodepoint) == (10) ? NVG_SPACE : NVG_NEWLINE);
						break;
					case 0x0085:
						type = (int)(NVG_NEWLINE);
						break;
					default:
						if ((((((((iter.codepoint) >= (0x4E00)) && (iter.codepoint <= 0x9FFF)) || (((iter.codepoint) >= (0x3000)) && (iter.codepoint <= 0x30FF))) || (((iter.codepoint) >= (0xFF00)) && (iter.codepoint <= 0xFFEF))) || (((iter.codepoint) >= (0x1100)) && (iter.codepoint <= 0x11FF))) || (((iter.codepoint) >= (0x3130)) && (iter.codepoint <= 0x318F))) || (((iter.codepoint) >= (0xAC00)) && (iter.codepoint <= 0xD7AF)))
							type = (int)(NVG_CJK_CHAR);
						else
							type = (int)(NVG_CHAR);
						break;
				}
				if ((type) == (NVG_NEWLINE))
				{
					rows[nrows].str = rowStart == null ? iter.str : new StringSegment(iter.str, rowStart.Value);
					if (rowEnd != null)
					{
						rows[nrows].str.Length = rowEnd.Value - rows[nrows].str.Location;
					}
					else
					{
						rows[nrows].str.Length = 0;
					}
					rows[nrows].width = (float)(rowWidth * invscale);
					rows[nrows].minx = (float)(rowMinX * invscale);
					rows[nrows].maxx = (float)(rowMaxX * invscale);
					remaining = iter.next;
					nrows++;
					if ((nrows) >= (rows.Length))
						return (int)(nrows);
					breakEnd = rowStart;
					breakWidth = (float)(0.0);
					breakMaxX = (float)(0.0);
					rowStart = null;
					rowEnd = null;
					rowWidth = (float)(0);
					rowMinX = (float)(rowMaxX = (float)(0));
				}
				else
				{
					if (rowStart == null)
					{
						if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))
						{
							rowStartX = (float)(iter.x);
							rowStart = iter.str.Location;
							rowEnd = iter.str.Location + 1;
							rowWidth = (float)(iter.nextx - rowStartX);
							rowMinX = (float)(q.x0 - rowStartX);
							rowMaxX = (float)(q.x1 - rowStartX);
							wordStart = iter.str.Location;
							wordStartX = (float)(iter.x);
							wordMinX = (float)(q.x0 - rowStartX);
							breakEnd = rowStart;
							breakWidth = (float)(0.0);
							breakMaxX = (float)(0.0);
						}
					}
					else
					{
						float nextWidth = (float)(iter.nextx - rowStartX);
						if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))
						{
							rowEnd = iter.str.Location + 1;
							rowWidth = (float)(iter.nextx - rowStartX);
							rowMaxX = (float)(q.x1 - rowStartX);
						}
						if (((((ptype) == (NVG_CHAR)) || ((ptype) == (NVG_CJK_CHAR))) && ((type) == (NVG_SPACE))) || ((type) == (NVG_CJK_CHAR)))
						{
							breakEnd = iter.str.Location;
							breakWidth = (float)(rowWidth);
							breakMaxX = (float)(rowMaxX);
						}
						if ((((ptype) == (NVG_SPACE)) && (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))) || ((type) == (NVG_CJK_CHAR)))
						{
							wordStart = iter.str.Location;
							wordStartX = (float)(iter.x);
							wordMinX = (float)(q.x0 - rowStartX);
						}
						if ((((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) && ((nextWidth) > (breakRowWidth)))
						{
							if ((breakEnd) == (rowStart))
							{
								rows[nrows].str = new StringSegment(_string_, rowStart.Value, iter.str.Location);
								rows[nrows].width = (float)(rowWidth * invscale);
								rows[nrows].minx = (float)(rowMinX * invscale);
								rows[nrows].maxx = (float)(rowMaxX * invscale);
								remaining = iter.str;
								nrows++;
								if ((nrows) >= (rows.Length))
									return (int)(nrows);
								rowStartX = (float)(iter.x);
								rowStart = iter.str.Location;
								rowEnd = iter.str.Location + 1;
								rowWidth = (float)(iter.nextx - rowStartX);
								rowMinX = (float)(q.x0 - rowStartX);
								rowMaxX = (float)(q.x1 - rowStartX);
								wordStart = iter.str.Location;
								wordStartX = (float)(iter.x);
								wordMinX = (float)(q.x0 - rowStartX);
							}
							else
							{
								rows[nrows].str = new StringSegment(_string_, rowStart.Value, breakEnd.Value - rowStart.Value);
								rows[nrows].width = (float)(breakWidth * invscale);
								rows[nrows].minx = (float)(rowMinX * invscale);
								rows[nrows].maxx = (float)(breakMaxX * invscale);
								remaining = new StringSegment(_string_, wordStart.Value);

								nrows++;
								if ((nrows) >= rows.Length)
									return (int)(nrows);
								rowStartX = (float)(wordStartX);
								rowStart = wordStart;
								rowEnd = iter.str.Location + 1;
								rowWidth = (float)(iter.nextx - rowStartX);
								rowMinX = (float)(wordMinX);
								rowMaxX = (float)(q.x1 - rowStartX);
							}
							breakEnd = rowStart;
							breakWidth = (float)(0.0);
							breakMaxX = (float)(0.0);
						}
					}
				}
				pcodepoint = (uint)(iter.codepoint);
				ptype = (int)(type);
			}
			if (rowStart != null)
			{
				rows[nrows].str = new StringSegment(_string_, rowStart.Value, rowEnd.Value - rowStart.Value);
				rows[nrows].width = (float)(rowWidth * invscale);
				rows[nrows].minx = (float)(rowMinX * invscale);
				rows[nrows].maxx = (float)(rowMaxX * invscale);
				remaining = StringSegment.Null;

				nrows++;
			}

			return (int)(nrows);
		}

		public float nvgTextBounds(float x, float y, string _string_, ref Bounds bounds)
		{
			NanoVGContextState state = nvg__getState();
			float scale = (float)(nvg__getFontScale(state) * devicePxRatio);
			float invscale = (float)(1.0f / scale);
			float width = 0;
			if ((state.fontId) == (-1))
				return (float)(0);
			fs.fonsSetSize((float)(state.fontSize * scale));
			fs.fonsSetSpacing((float)(state.letterSpacing * scale));
			fs.fonsSetBlur((float)(state.fontBlur * scale));
			fs.fonsSetAlign((int)(state.textAlign));
			fs.fonsSetFont((int)(state.fontId));
			width = (float)(fs.fonsTextBounds((float)(x * scale), (float)(y * scale), _string_, ref bounds));
			fs.fonsLineBounds((float)(y * scale), ref bounds.b2, ref bounds.b4);
			bounds.b1 *= (float)(invscale);
			bounds.b2 *= (float)(invscale);
			bounds.b3 *= (float)(invscale);
			bounds.b4 *= (float)(invscale);

			return (float)(width * invscale);
		}

		public void nvgTextBoxBounds(float x, float y, float breakRowWidth, StringSegment _string_, ref Bounds bounds)
		{
			NanoVGContextState state = nvg__getState();
			float scale = (float)(nvg__getFontScale(state) * devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int i = 0;
			int oldAlign = (int)(state.textAlign);
			int haling = (int)(state.textAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state.textAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);
			float rminy = (float)(0);
			float rmaxy = (float)(0);
			float minx = 0;
			float miny = 0;
			float maxx = 0;
			float maxy = 0;
			if ((state.fontId) == (-1))
			{
				bounds.b1 = (float)(bounds.b2 = (float)(bounds.b3 = (float)(bounds.b4 = (float)(0.0f))));
				return;
			}

			float ascender, descender;
			nvgTextMetrics(out ascender, out descender, out lineh);
			state.textAlign = (int)(NVG_ALIGN_LEFT | valign);
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			fs.fonsSetSize((float)(state.fontSize * scale));
			fs.fonsSetSpacing((float)(state.letterSpacing * scale));
			fs.fonsSetBlur((float)(state.fontBlur * scale));
			fs.fonsSetAlign((int)(state.textAlign));
			fs.fonsSetFont((int)(state.fontId));
			fs.fonsLineBounds((float)(0), ref rminy, ref rmaxy);
			rminy *= (float)(invscale);
			rmaxy *= (float)(invscale);
			while (true)
			{
				var nrows = nvgTextBreakLines(_string_, (float)(breakRowWidth), _rows, out _string_);
				if (nrows <= 0)
				{
					break;
				}
				for (i = (int)(0); (i) < (nrows); i++)
				{
					TextRow row = _rows[i];
					float rminx = 0;
					float rmaxx = 0;
					float dx = (float)(0);
					if ((haling & NVG_ALIGN_LEFT) != 0)
						dx = (float)(0);
					else if ((haling & NVG_ALIGN_CENTER) != 0)
						dx = (float)(breakRowWidth * 0.5f - row.width * 0.5f);
					else if ((haling & NVG_ALIGN_RIGHT) != 0)
						dx = (float)(breakRowWidth - row.width);
					rminx = (float)(x + row.minx + dx);
					rmaxx = (float)(x + row.maxx + dx);
					minx = (float)(nvg__minf((float)(minx), (float)(rminx)));
					maxx = (float)(nvg__maxf((float)(maxx), (float)(rmaxx)));
					miny = (float)(nvg__minf((float)(miny), (float)(y + rminy)));
					maxy = (float)(nvg__maxf((float)(maxy), (float)(y + rmaxy)));
					y += (float)(lineh * state.lineHeight);
				}
			}
			state.textAlign = (int)(oldAlign);
			bounds.b1 = (float)(minx);
			bounds.b2 = (float)(miny);
			bounds.b3 = (float)(maxx);
			bounds.b4 = (float)(maxy);
		}

		public void nvgTextMetrics(out float ascender, out float descender, out float lineh)
		{
			ascender = descender = lineh = 0;

			NanoVGContextState state = nvg__getState();
			float scale = (float)(nvg__getFontScale(state) * devicePxRatio);
			float invscale = (float)(1.0f / scale);
			if ((state.fontId) == (-1))
				return;
			fs.fonsSetSize((float)(state.fontSize * scale));
			fs.fonsSetSpacing((float)(state.letterSpacing * scale));
			fs.fonsSetBlur((float)(state.fontBlur * scale));
			fs.fonsSetAlign((int)(state.textAlign));
			fs.fonsSetFont((int)(state.fontId));
			fs.fonsVertMetrics(out ascender, out descender, out lineh);
			ascender *= (float)(invscale);
			descender *= (float)(invscale);
			lineh *= (float)(invscale);
		}

		public static float sqrtf(float a)
		{
			return (float)(Math.Sqrt((float)(a)));
		}

		public static float sinf(float a)
		{
			return (float)(Math.Sin((float)(a)));
		}

		public static float tanf(float a)
		{
			return (float)(Math.Tan((float)(a)));
		}

		public static float atan2f(float a, float b)
		{
			return (float)(Math.Atan2(a, b));
		}

		public static float cosf(float a)
		{
			return (float)(Math.Cos((float)(a)));
		}

		public static float acosf(float a)
		{
			return (float)(Math.Acos((float)(a)));
		}

		public static float ceilf(float a)
		{
			return (float)(Math.Ceiling((float)(a)));
		}

		public static float nvg__modf(float a, float b)
		{
			return (float)(CRuntime.fmod((float)(a), (float)(b)));
		}

		public static int nvg__mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int nvg__maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}

		public static int nvg__clampi(int a, int mn, int mx)
		{
			return (int)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		public static float nvg__minf(float a, float b)
		{
			return (float)((a) < (b) ? a : b);
		}

		public static float nvg__maxf(float a, float b)
		{
			return (float)((a) > (b) ? a : b);
		}

		public static float nvg__absf(float a)
		{
			return (float)((a) >= (0.0f) ? a : -a);
		}

		public static float nvg__signf(float a)
		{
			return (float)((a) >= (0.0f) ? 1.0f : -1.0f);
		}

		public static float nvg__clampf(float a, float mn, float mx)
		{
			return (float)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		public static float nvg__cross(float dx0, float dy0, float dx1, float dy1)
		{
			return (float)(dx1 * dy0 - dx0 * dy1);
		}

		public static float nvg__normalize(float* x, float* y)
		{
			float d = (float)(sqrtf((float)((*x) * (*x) + (*y) * (*y))));
			if ((d) > (1e-6f))
			{
				float id = (float)(1.0f / d);
				*x *= (float)(id);
				*y *= (float)(id);
			}

			return (float)(d);
		}

		public static CompositeOperationState nvg__compositeOperationState(int op)
		{
			int sfactor = 0;
			int dfactor = 0;
			if ((op) == (NVG_SOURCE_OVER))
			{
				sfactor = (int)(NVG_ONE);
				dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);
			}
			else if ((op) == (NVG_SOURCE_IN))
			{
				sfactor = (int)(NVG_DST_ALPHA);
				dfactor = (int)(NVG_ZERO);
			}
			else if ((op) == (NVG_SOURCE_OUT))
			{
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);
				dfactor = (int)(NVG_ZERO);
			}
			else if ((op) == (NVG_ATOP))
			{
				sfactor = (int)(NVG_DST_ALPHA);
				dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);
			}
			else if ((op) == (NVG_DESTINATION_OVER))
			{
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);
				dfactor = (int)(NVG_ONE);
			}
			else if ((op) == (NVG_DESTINATION_IN))
			{
				sfactor = (int)(NVG_ZERO);
				dfactor = (int)(NVG_SRC_ALPHA);
			}
			else if ((op) == (NVG_DESTINATION_OUT))
			{
				sfactor = (int)(NVG_ZERO);
				dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);
			}
			else if ((op) == (NVG_DESTINATION_ATOP))
			{
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);
				dfactor = (int)(NVG_SRC_ALPHA);
			}
			else if ((op) == (NVG_LIGHTER))
			{
				sfactor = (int)(NVG_ONE);
				dfactor = (int)(NVG_ONE);
			}
			else if ((op) == (NVG_COPY))
			{
				sfactor = (int)(NVG_ONE);
				dfactor = (int)(NVG_ZERO);
			}
			else if ((op) == (NVG_XOR))
			{
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);
				dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);
			}
			else
			{
				sfactor = (int)(NVG_ONE);
				dfactor = (int)(NVG_ZERO);
			}

			CompositeOperationState state = new CompositeOperationState();
			state.srcRGB = (int)(sfactor);
			state.dstRGB = (int)(dfactor);
			state.srcAlpha = (int)(sfactor);
			state.dstAlpha = (int)(dfactor);
			return (CompositeOperationState)(state);
		}

		public static float nvg__hue(float h, float m1, float m2)
		{
			if ((h) < (0))
				h += (float)(1);
			if ((h) > (1))
				h -= (float)(1);
			if ((h) < (1.0f / 6.0f))
				return (float)(m1 + (m2 - m1) * h * 6.0f);
			else if ((h) < (3.0f / 6.0f))
				return (float)(m2);
			else if ((h) < (4.0f / 6.0f))
				return (float)(m1 + (m2 - m1) * (2.0f / 3.0f - h) * 6.0f);
			return (float)(m1);
		}

		public static Color nvgHSLA(float h, float s, float l, byte a)
		{
			float m1 = 0;
			float m2 = 0;
			h = (float)(nvg__modf((float)(h), (float)(1.0f)));
			if ((h) < (0.0f))
				h += (float)(1.0f);
			s = (float)(nvg__clampf((float)(s), (float)(0.0f), (float)(1.0f)));
			l = (float)(nvg__clampf((float)(l), (float)(0.0f), (float)(1.0f)));
			m2 = (float)(l <= 0.5f ? (l * (1 + s)) : (l + s - l * s));
			m1 = (float)(2 * l - m2);

			float fr = (float)(nvg__clampf((float)(nvg__hue((float)(h + 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			float fg = (float)(nvg__clampf((float)(nvg__hue((float)(h), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			float fb = (float)(nvg__clampf((float)(nvg__hue((float)(h - 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			float fa = (float)(a / 255.0f);

			return new Color(fr, fg, fb, fa);
		}

		public static float nvgDegToRad(float deg)
		{
			return (float)(deg / 180.0f * 3.14159274);
		}

		public static float nvgRadToDeg(float rad)
		{
			return (float)(rad / 3.14159274 * 180.0f);
		}

		public static void nvg__setPaintColor(ref Paint p, Color color)
		{
			p.Zero();
			p.xform.SetIdentity();
			p.radius = 0.0f;
			p.feather = 1.0f;
			p.innerColor = color;
			p.outerColor = color;
		}

		public static float nvg__triarea2(float ax, float ay, float bx, float by, float cx, float cy)
		{
			float abx = bx - ax;
			float aby = (float)(by - ay);
			float acx = (float)(cx - ax);
			float acy = (float)(cy - ay);
			return (float)(acx * aby - abx * acy);
		}

		public static float nvg__polyArea(NVGpoint* pts, int npts)
		{
			int i = 0;
			float area = (float)(0);
			for (i = (int)(2); (i) < (npts); i++)
			{
				NVGpoint* a = &pts[0];
				NVGpoint* b = &pts[i - 1];
				NVGpoint* c = &pts[i];
				area += (float)(nvg__triarea2((float)(a->x), (float)(a->y), (float)(b->x), (float)(b->y), (float)(c->x), (float)(c->y)));
			}
			return (float)(area * 0.5f);
		}

		internal static void nvg__polyReverse(NVGpoint* pts, int npts)
		{
			NVGpoint tmp = new NVGpoint();
			int i = (int)(0);
			int j = (int)(npts - 1);
			while ((i) < (j))
			{
				tmp = (NVGpoint)(pts[i]);
				pts[i] = (NVGpoint)(pts[j]);
				pts[j] = (NVGpoint)(tmp);
				i++;
				j--;
			}
		}

		public static void nvg__vset(Vertex* vtx, float x, float y, float u, float v)
		{
			nvg__vset(ref *vtx, x, y, u, v);
		}

		public static void nvg__vset(ref Vertex vtx, float x, float y, float u, float v)
		{
			vtx.Position.X = (float)(x);
			vtx.Position.Y = (float)(y);
			vtx.TextureCoordinate.X = (float)(u);
			vtx.TextureCoordinate.Y = (float)(v);
		}

		public static void nvg__isectRects(float* dst, float ax, float ay, float aw, float ah, float bx, float by, float bw, float bh)
		{
			float minx = (float)(nvg__maxf((float)(ax), (float)(bx)));
			float miny = (float)(nvg__maxf((float)(ay), (float)(by)));
			float maxx = (float)(nvg__minf((float)(ax + aw), (float)(bx + bw)));
			float maxy = (float)(nvg__minf((float)(ay + ah), (float)(by + bh)));
			dst[0] = (float)(minx);
			dst[1] = (float)(miny);
			dst[2] = (float)(nvg__maxf((float)(0.0f), (float)(maxx - minx)));
			dst[3] = (float)(nvg__maxf((float)(0.0f), (float)(maxy - miny)));
		}

		public static float nvg__getAverageScale(ref Transform t)
		{
			float sx = (float)(Math.Sqrt((float)(t.t1 * t.t1 + t.t3 * t.t3)));
			float sy = (float)(Math.Sqrt((float)(t.t2 * t.t2 + t.t4 * t.t4)));
			return (float)((sx + sy) * 0.5f);
		}

		public static int nvg__curveDivs(float r, float arc, float tol)
		{
			float da = (float)(acosf((float)(r / (r + tol))) * 2.0f);
			return (int)(nvg__maxi((int)(2), (int)(ceilf((float)(arc / da)))));
		}

		public static void nvg__chooseBevel(int bevel, NVGpoint* p0, NVGpoint* p1, float w, float* x0, float* y0, float* x1, float* y1)
		{
			if ((bevel) != 0)
			{
				*x0 = (float)(p1->x + p0->dy * w);
				*y0 = (float)(p1->y - p0->dx * w);
				*x1 = (float)(p1->x + p1->dy * w);
				*y1 = (float)(p1->y - p1->dx * w);
			}
			else
			{
				*x0 = (float)(p1->x + p1->dmx * w);
				*y0 = (float)(p1->y + p1->dmy * w);
				*x1 = (float)(p1->x + p1->dmx * w);
				*y1 = (float)(p1->y + p1->dmy * w);
			}
		}

		public static float nvg__quantize(float a, float d)
		{
			return (float)(((int)(a / d + 0.5f)) * d);
		}

		public static float nvg__getFontScale(NanoVGContextState state)
		{
			return (float)(nvg__minf((float)(nvg__quantize((float)(nvg__getAverageScale(ref state.xform)), (float)(0.01f))), (float)(4.0f)));
		}

		public static Vertex* nvg__roundJoin(Vertex* dst, NVGpoint* p0, NVGpoint* p1, float lw, float rw, float lu, float ru, int ncap, float fringe)
		{
			int i = 0;
			int n = 0;
			float dlx0 = (float)(p0->dy);
			float dly0 = (float)(-p0->dx);
			float dlx1 = (float)(p1->dy);
			float dly1 = (float)(-p1->dx);
			if ((p1->flags & NVG_PT_LEFT) != 0)
			{
				float lx0 = 0;
				float ly0 = 0;
				float lx1 = 0;
				float ly1 = 0;
				float a0 = 0;
				float a1 = 0;
				nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1);
				a0 = (float)(atan2f((float)(-dly0), (float)(-dlx0)));
				a1 = (float)(atan2f((float)(-dly1), (float)(-dlx1)));
				if ((a1) > (a0))
					a1 -= (float)(3.14159274 * 2);
				nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));
				dst++;
				n = (int)(nvg__clampi((int)(ceilf((float)(((a0 - a1) / 3.14159274) * ncap))), (int)(2), (int)(ncap)));
				for (i = (int)(0); (i) < (n); i++)
				{
					float u = (float)(i / (float)(n - 1));
					float a = (float)(a0 + u * (a1 - a0));
					float rx = (float)(p1->x + cosf((float)(a)) * rw);
					float ry = (float)(p1->y + sinf((float)(a)) * rw);
					nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));
					dst++;
					nvg__vset(dst, (float)(rx), (float)(ry), (float)(ru), (float)(1));
					dst++;
				}
				nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));
				dst++;
			}
			else
			{
				float rx0 = 0;
				float ry0 = 0;
				float rx1 = 0;
				float ry1 = 0;
				float a0 = 0;
				float a1 = 0;
				nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1);
				a0 = (float)(atan2f((float)(dly0), (float)(dlx0)));
				a1 = (float)(atan2f((float)(dly1), (float)(dlx1)));
				if ((a1) < (a0))
					a1 += (float)(3.14159274 * 2);
				nvg__vset(dst, (float)(p1->x + dlx0 * rw), (float)(p1->y + dly0 * rw), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
				dst++;
				n = (int)(nvg__clampi((int)(ceilf((float)(((a1 - a0) / 3.14159274) * ncap))), (int)(2), (int)(ncap)));
				for (i = (int)(0); (i) < (n); i++)
				{
					float u = (float)(i / (float)(n - 1));
					float a = (float)(a0 + u * (a1 - a0));
					float lx = (float)(p1->x + cosf((float)(a)) * lw);
					float ly = (float)(p1->y + sinf((float)(a)) * lw);
					nvg__vset(dst, (float)(lx), (float)(ly), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));
					dst++;
				}
				nvg__vset(dst, (float)(p1->x + dlx1 * rw), (float)(p1->y + dly1 * rw), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));
				dst++;
			}

			return dst;
		}

		public static Vertex* nvg__bevelJoin(Vertex* dst, NVGpoint* p0, NVGpoint* p1, float lw, float rw, float lu, float ru, float fringe)
		{
			float rx0 = 0;
			float ry0 = 0;
			float rx1 = 0;
			float ry1 = 0;
			float lx0 = 0;
			float ly0 = 0;
			float lx1 = 0;
			float ly1 = 0;
			float dlx0 = (float)(p0->dy);
			float dly0 = (float)(-p0->dx);
			float dlx1 = (float)(p1->dy);
			float dly1 = (float)(-p1->dx);
			if ((p1->flags & NVG_PT_LEFT) != 0)
			{
				nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1);
				nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));
				dst++;
				if ((p1->flags & NVG_PT_BEVEL) != 0)
				{
					nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));
					dst++;
					nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));
					dst++;
				}
				else
				{
					rx0 = (float)(p1->x - p1->dmx * rw);
					ry0 = (float)(p1->y - p1->dmy * rw);
					nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));
					dst++;
					nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
					dst++;
					nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));
					dst++;
				}
				nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));
				dst++;
			}
			else
			{
				nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1);
				nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
				dst++;
				if ((p1->flags & NVG_PT_BEVEL) != 0)
				{
					nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));
					dst++;
				}
				else
				{
					lx0 = (float)(p1->x + p1->dmx * lw);
					ly0 = (float)(p1->y + p1->dmy * lw);
					nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));
					dst++;
					nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1));
					dst++;
					nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));
					dst++;
				}
				nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1));
				dst++;
				nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));
				dst++;
			}

			return dst;
		}

		public static Vertex* nvg__buttCapStart(Vertex* dst, NVGpoint* p, float dx, float dy, float w, float d, float aa, float u0, float u1)
		{
			float px = (float)(p->x - dx * d);
			float py = (float)(p->y - dy * d);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			nvg__vset(dst, (float)(px + dlx * w - dx * aa), (float)(py + dly * w - dy * aa), (float)(u0), (float)(0));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w - dx * aa), (float)(py - dly * w - dy * aa), (float)(u1), (float)(0));
			dst++;
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			return dst;
		}

		public static Vertex* nvg__buttCapEnd(Vertex* dst, NVGpoint* p, float dx, float dy, float w, float d, float aa, float u0, float u1)
		{
			float px = (float)(p->x + dx * d);
			float py = (float)(p->y + dy * d);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px + dlx * w + dx * aa), (float)(py + dly * w + dy * aa), (float)(u0), (float)(0));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w + dx * aa), (float)(py - dly * w + dy * aa), (float)(u1), (float)(0));
			dst++;
			return dst;
		}

		public static Vertex* nvg__roundCapStart(Vertex* dst, NVGpoint* p, float dx, float dy, float w, int ncap, float aa, float u0, float u1)
		{
			int i = 0;
			float px = (float)(p->x);
			float py = (float)(p->y);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			for (i = (int)(0); (i) < (ncap); i++)
			{
				float a = (float)(i / (float)(ncap - 1) * 3.14159274);
				float ax = (float)(cosf((float)(a)) * w);
				float ay = (float)(sinf((float)(a)) * w);
				nvg__vset(dst, (float)(px - dlx * ax - dx * ay), (float)(py - dly * ax - dy * ay), (float)(u0), (float)(1));
				dst++;
				nvg__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1));
				dst++;
			}
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			return dst;
		}

		public static Vertex* nvg__roundCapEnd(Vertex* dst, NVGpoint* p, float dx, float dy, float w, int ncap, float aa, float u0, float u1)
		{
			int i = 0;
			float px = (float)(p->x);
			float py = (float)(p->y);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			for (i = (int)(0); (i) < (ncap); i++)
			{
				float a = (float)(i / (float)(ncap - 1) * 3.14159274);
				float ax = (float)(cosf((float)(a)) * w);
				float ay = (float)(sinf((float)(a)) * w);
				nvg__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1));
				dst++;
				nvg__vset(dst, (float)(px - dlx * ax + dx * ay), (float)(py - dly * ax + dy * ay), (float)(u0), (float)(1));
				dst++;
			}
			return dst;
		}

		public static int nvg__ptEquals(float x1, float y1, float x2, float y2, float tol)
		{
			float dx = (float)(x2 - x1);
			float dy = (float)(y2 - y1);
			return (int)((dx * dx + dy * dy) < (tol * tol) ? 1 : 0);
		}

		public static float nvg__distPtSeg(float x, float y, float px, float py, float qx, float qy)
		{
			float pqx = 0;
			float pqy = 0;
			float dx = 0;
			float dy = 0;
			float d = 0;
			float t = 0;
			pqx = (float)(qx - px);
			pqy = (float)(qy - py);
			dx = (float)(x - px);
			dy = (float)(y - py);
			d = (float)(pqx * pqx + pqy * pqy);
			t = (float)(pqx * dx + pqy * dy);
			if ((d) > (0))
				t /= (float)(d);
			if ((t) < (0))
				t = (float)(0);
			else if ((t) > (1))
				t = (float)(1);
			dx = (float)(px + t * pqx - x);
			dy = (float)(py + t * pqy - y);
			return (float)(dx * dx + dy * dy);
		}
	}
}