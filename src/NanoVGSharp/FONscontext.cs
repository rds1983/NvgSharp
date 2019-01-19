using StbSharp;
using System;

namespace NanoVGSharp
{
	public unsafe class FONScontext : IDisposable
	{
		public const int FONS_ZERO_TOPLEFT = 1;
		public const int FONS_ZERO_BOTTOMLEFT = 2;
		public const int FONS_ALIGN_LEFT = 1 << 0;
		public const int FONS_ALIGN_CENTER = 1 << 1;
		public const int FONS_ALIGN_RIGHT = 1 << 2;
		public const int FONS_ALIGN_TOP = 1 << 3;
		public const int FONS_ALIGN_MIDDLE = 1 << 4;
		public const int FONS_ALIGN_BOTTOM = 1 << 5;
		public const int FONS_ALIGN_BASELINE = 1 << 6;
		public const int FONS_GLYPH_BITMAP_OPTIONAL = 1;
		public const int FONS_GLYPH_BITMAP_REQUIRED = 2;
		public const int FONS_ATLAS_FULL = 1;
		public const int FONS_SCRATCH_FULL = 2;
		public const int FONS_STATES_OVERFLOW = 3;
		public const int FONS_STATES_UNDERFLOW = 4;

		public delegate void handleErrorDelegate(void* uptr, int error, int val);

		public FONSparams _params_ = new FONSparams();
		public float itw;
		public float ith;
		public byte* texData;
		public int[] dirtyRect = new int[4];
		public FONSfont[] fonts;
		public FONSatlas atlas;
		public int cfonts;
		public int nfonts;
		public float[] verts = new float[1024 * 2];
		public float[] tcoords = new float[1024 * 2];
		public uint[] colors = new uint[1024];
		public int nverts;
		public byte* scratch;
		public int nscratch;
		public FONSstate[] states = new FONSstate[20];
		public int nstates;
		public handleErrorDelegate handleError;
		public void* errorUptr;

		public FONScontext(FONSparams p)
		{
			_params_ = p;
			scratch = (byte*)(CRuntime.malloc((ulong)(96000)));

			atlas = new FONSatlas((int)(_params_.width), (int)(_params_.height), (int)(256));
			fonts = new FONSfont[4];
			cfonts = (int)(4);
			nfonts = (int)(0);
			itw = (float)(1.0f / _params_.width);
			ith = (float)(1.0f / _params_.height);
			texData = (byte*)(CRuntime.malloc((ulong)(_params_.width * _params_.height)));
			CRuntime.memset(texData, (int)(0), (ulong)(_params_.width * _params_.height));
			dirtyRect[0] = (int)(_params_.width);
			dirtyRect[1] = (int)(_params_.height);
			dirtyRect[2] = (int)(0);
			dirtyRect[3] = (int)(0);
			fons__addWhiteRect((int)(2), (int)(2));
			fonsPushState();
			fonsClearState();
		}

		public void Dispose()
		{
			int i = 0;
			for (i = (int)(0); (i) < (nfonts); ++i)
			{
				fons__freeFont(fonts[i]);
			}
			if ((texData) != null)
				CRuntime.free(texData);
			if ((scratch) != null)
				CRuntime.free(scratch);
		}


		public void fons__addWhiteRect(int w, int h)
		{
			int x = 0;
			int y = 0;
			int gx = 0;
			int gy = 0;
			byte* dst;
			if ((atlas.fons__atlasAddRect((int)(w), (int)(h), &gx, &gy)) == (0))
				return;
			dst = &texData[gx + gy * _params_.width];
			for (y = (int)(0); (y) < (h); y++)
			{
				for (x = (int)(0); (x) < (w); x++)
				{
					dst[x] = (byte)(0xff);
				}
				dst += _params_.width;
			}
			dirtyRect[0] = (int)(fons__mini((int)(dirtyRect[0]), (int)(gx)));
			dirtyRect[1] = (int)(fons__mini((int)(dirtyRect[1]), (int)(gy)));
			dirtyRect[2] = (int)(fons__maxi((int)(dirtyRect[2]), (int)(gx + w)));
			dirtyRect[3] = (int)(fons__maxi((int)(dirtyRect[3]), (int)(gy + h)));
		}

		public FONSstate fons__getState()
		{
			return states[nstates - 1];
		}

		public int fonsAddFallbackFont(int _base_, int fallback)
		{
			FONSfont baseFont = fonts[_base_];
			if ((baseFont.nfallbacks) < (20))
			{
				baseFont.fallbacks[baseFont.nfallbacks++] = (int)(fallback);
				return (int)(1);
			}

			return (int)(0);
		}

		public void fonsSetSize(float size)
		{
			fons__getState().size = (float)(size);
		}

		public void fonsSetColor(uint color)
		{
			fons__getState().color = (uint)(color);
		}

		public void fonsSetSpacing(float spacing)
		{
			fons__getState().spacing = (float)(spacing);
		}

		public void fonsSetBlur(float blur)
		{
			fons__getState().blur = (float)(blur);
		}

		public void fonsSetAlign(int align)
		{
			fons__getState().align = (int)(align);
		}

		public void fonsSetFont(int font)
		{
			fons__getState().font = (int)(font);
		}

		public void fonsPushState()
		{
			if ((nstates) >= (20))
			{
				if ((handleError) != null)
					handleError(errorUptr, (int)(FONS_STATES_OVERFLOW), (int)(0));
				return;
			}

			if ((nstates) > (0))
			{
				states[nstates] = states[nstates - 1];
			}
			nstates++;
		}

		public void fonsPopState()
		{
			if (nstates <= 1)
			{
				if ((handleError) != null)
					handleError(errorUptr, (int)(FONS_STATES_UNDERFLOW), (int)(0));
				return;
			}

			nstates--;
		}

		public void fonsClearState()
		{
			FONSstate state = fons__getState();
			state.size = (float)(12.0f);
			state.color = (uint)(0xffffffff);
			state.font = (int)(0);
			state.blur = (float)(0);
			state.spacing = (float)(0);
			state.align = (int)(FONS_ALIGN_LEFT | FONS_ALIGN_BASELINE);
		}

		public int fons__tt_loadFont(StbTrueType.stbtt_fontinfo font, byte* data, int dataSize)
		{
			int stbError = 0;
			font.userdata = this;
			stbError = (int)(StbTrueType.stbtt_InitFont(font, data, (int)(0)));
			return (int)(stbError);
		}

		public void fons__freeFont(FONSfont font)
		{
			if ((font) == null)
				return;
			if ((font.glyphs) != null)
				CRuntime.free(font.glyphs);
			if (((font.freeData) != 0) && ((font.data) != null))
				CRuntime.free(font.data);
		}

		public int fons__allocFont()
		{
			FONSfont font = null;
			if ((nfonts + 1) > (cfonts))
			{
				cfonts = (int)((cfonts) == (0) ? 8 : cfonts * 2);
				fonts = new FONSfont[cfonts];
				if ((fonts) == null)
					return (int)(-1);
			}

			font = new FONSfont();
			if ((font) == null)
				goto error;
			font.glyphs = (FONSglyph*)(CRuntime.malloc((ulong)(sizeof(FONSglyph) * 256)));
			if ((font.glyphs) == null)
				goto error;
			font.cglyphs = (int)(256);
			font.nglyphs = (int)(0);
			fonts[nfonts++] = font;
			return (int)(nfonts - 1);
		error:
			;
			fons__freeFont(font);
			return (int)(-1);
		}

		public int fonsAddFontMem(string name, byte* data, int dataSize, int freeData)
		{
			int i = 0;
			int ascent = 0;
			int descent = 0;
			int fh = 0;
			int lineGap = 0;
			FONSfont font;
			int idx = (int)(fons__allocFont());
			if ((idx) == (-1))
				return (int)(-1);
			font = fonts[idx];
			font.name = name;
			for (i = (int)(0); (i) < (256); ++i)
			{
				font.lut[i] = (int)(-1);
			}
			font.dataSize = (int)(dataSize);
			font.data = data;
			font.freeData = ((byte)(freeData));
			nscratch = (int)(0);
			if (fons__tt_loadFont(font.font, data, (int)(dataSize)) == 0)
				goto error;
			font.font.fons__tt_getFontVMetrics(&ascent, &descent, &lineGap);
			fh = (int)(ascent - descent);
			font.ascender = (float)((float)(ascent) / (float)(fh));
			font.descender = (float)((float)(descent) / (float)(fh));
			font.lineh = (float)((float)(fh + lineGap) / (float)(fh));
			return (int)(idx);
		error:
			;
			fons__freeFont(font);
			nfonts--;
			return (int)(-1);
		}

		public int fonsGetFontByName(string name)
		{
			int i = 0;
			for (i = (int)(0); (i) < (nfonts); i++)
			{
				if (fonts[i].name == name)
					return (int)(i);
			}
			return (int)(-1);
		}

		public void fons__blur(byte* dst, int w, int h, int dstStride, int blur)
		{
			int alpha = 0;
			float sigma = 0;
			if ((blur) < (1))
				return;
			sigma = (float)((float)(blur) * 0.57735f);
			alpha = ((int)((1 << 16) * (1.0f - Math.Exp((float)(-2.3f / (sigma + 1.0f))))));
			fons__blurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
		}

		public FONSglyph* fons__getGlyph(FONSfont font, uint codepoint, short isize, short iblur, int bitmapOption)
		{
			int i = 0;
			int g = 0;
			int advance = 0;
			int lsb = 0;
			int x0 = 0;
			int y0 = 0;
			int x1 = 0;
			int y1 = 0;
			int gw = 0;
			int gh = 0;
			int gx = 0;
			int gy = 0;
			int x = 0;
			int y = 0;
			float scale = 0;
			FONSglyph* glyph = null;
			uint h = 0;
			float size = (float)(isize / 10.0f);
			int pad = 0;
			int added = 0;
			byte* bdst;
			byte* dst;
			FONSfont renderFont = font;
			if ((isize) < (2))
				return null;
			if ((iblur) > (20))
				iblur = (short)(20);
			pad = (int)(iblur + 2);
			nscratch = (int)(0);
			h = (uint)(fons__hashint((uint)(codepoint)) & (256 - 1));
			i = (int)(font.lut[h]);
			while (i != -1)
			{
				if ((((font.glyphs[i].codepoint) == (codepoint)) && ((font.glyphs[i].size) == (isize))) && ((font.glyphs[i].blur) == (iblur)))
				{
					glyph = &font.glyphs[i];
					if (((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL)) || (((glyph->x0) >= (0)) && ((glyph->y0) >= (0))))
					{
						return glyph;
					}
					break;
				}
				i = (int)(font.glyphs[i].next);
			}
			g = (int)(font.font.fons__tt_getGlyphIndex((int)(codepoint)));
			if ((g) == (0))
			{
				for (i = (int)(0); (i) < (font.nfallbacks); ++i)
				{
					FONSfont fallbackFont = fonts[font.fallbacks[i]];
					int fallbackIndex = (int)(fallbackFont.font.fons__tt_getGlyphIndex((int)(codepoint)));
					if (fallbackIndex != 0)
					{
						g = (int)(fallbackIndex);
						renderFont = fallbackFont;
						break;
					}
				}
			}

			scale = (float)(renderFont.font.fons__tt_getPixelHeightScale((float)(size)));
			renderFont.font.fons__tt_buildGlyphBitmap((int)(g), (float)(size), (float)(scale), &advance, &lsb, &x0, &y0, &x1, &y1);
			gw = (int)(x1 - x0 + pad * 2);
			gh = (int)(y1 - y0 + pad * 2);
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_REQUIRED))
			{
				added = (int)(atlas.fons__atlasAddRect((int)(gw), (int)(gh), &gx, &gy));
				if (((added) == (0)) && (handleError != null))
				{
					handleError(errorUptr, (int)(FONS_ATLAS_FULL), (int)(0));
					added = (int)(atlas.fons__atlasAddRect((int)(gw), (int)(gh), &gx, &gy));
				}
				if ((added) == (0))
					return null;
			}
			else
			{
				gx = (int)(-1);
				gy = (int)(-1);
			}

			if ((glyph) == null)
			{
				glyph = fons__allocGlyph(font);
				glyph->codepoint = (uint)(codepoint);
				glyph->size = (short)(isize);
				glyph->blur = (short)(iblur);
				glyph->next = (int)(0);
				glyph->next = (int)(font.lut[h]);
				font.lut[h] = (int)(font.nglyphs - 1);
			}

			glyph->index = (int)(g);
			glyph->x0 = ((short)(gx));
			glyph->y0 = ((short)(gy));
			glyph->x1 = ((short)(glyph->x0 + gw));
			glyph->y1 = ((short)(glyph->y0 + gh));
			glyph->xadv = ((short)(scale * advance * 10.0f));
			glyph->xoff = ((short)(x0 - pad));
			glyph->yoff = ((short)(y0 - pad));
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL))
			{
				return glyph;
			}

			dst = &texData[(glyph->x0 + pad) + (glyph->y0 + pad) * _params_.width];
			renderFont.font.fons__tt_renderGlyphBitmap(dst, (int)(gw - pad * 2), (int)(gh - pad * 2), (int)(_params_.width), (float)(scale), (float)(scale), (int)(g));
			dst = &texData[glyph->x0 + glyph->y0 * _params_.width];
			for (y = (int)(0); (y) < (gh); y++)
			{
				dst[y * _params_.width] = (byte)(0);
				dst[gw - 1 + y * _params_.width] = (byte)(0);
			}
			for (x = (int)(0); (x) < (gw); x++)
			{
				dst[x] = (byte)(0);
				dst[x + (gh - 1) * _params_.width] = (byte)(0);
			}
			if ((iblur) > (0))
			{
				nscratch = (int)(0);
				bdst = &texData[glyph->x0 + glyph->y0 * _params_.width];
				fons__blur(bdst, (int)(gw), (int)(gh), (int)(_params_.width), (int)(iblur));
			}

			dirtyRect[0] = (int)(fons__mini((int)(dirtyRect[0]), (int)(glyph->x0)));
			dirtyRect[1] = (int)(fons__mini((int)(dirtyRect[1]), (int)(glyph->y0)));
			dirtyRect[2] = (int)(fons__maxi((int)(dirtyRect[2]), (int)(glyph->x1)));
			dirtyRect[3] = (int)(fons__maxi((int)(dirtyRect[3]), (int)(glyph->y1)));
			return glyph;
		}

		public void fons__getQuad(FONSfont font, int prevGlyphIndex, FONSglyph* glyph, float scale, float spacing, ref float x, ref float y, FONSquad* q)
		{
			float rx = 0;
			float ry = 0;
			float xoff = 0;
			float yoff = 0;
			float x0 = 0;
			float y0 = 0;
			float x1 = 0;
			float y1 = 0;
			if (prevGlyphIndex != -1)
			{
				float adv = (float)(font.font.fons__tt_getGlyphKernAdvance((int)(prevGlyphIndex), (int)(glyph->index)) * scale);
				x += (float)((int)(adv + spacing + 0.5f));
			}

			xoff = (float)((short)(glyph->xoff + 1));
			yoff = (float)((short)(glyph->yoff + 1));
			x0 = ((float)(glyph->x0 + 1));
			y0 = ((float)(glyph->y0 + 1));
			x1 = ((float)(glyph->x1 - 1));
			y1 = ((float)(glyph->y1 - 1));
			if ((_params_.flags & FONS_ZERO_TOPLEFT) != 0)
			{
				rx = ((float)((int)(x + xoff)));
				ry = ((float)((int)(y + yoff)));
				q->x0 = (float)(rx);
				q->y0 = (float)(ry);
				q->x1 = (float)(rx + x1 - x0);
				q->y1 = (float)(ry + y1 - y0);
				q->s0 = (float)(x0 * itw);
				q->t0 = (float)(y0 * ith);
				q->s1 = (float)(x1 * itw);
				q->t1 = (float)(y1 * ith);
			}
			else
			{
				rx = ((float)((int)(x + xoff)));
				ry = ((float)((int)(y - yoff)));
				q->x0 = (float)(rx);
				q->y0 = (float)(ry);
				q->x1 = (float)(rx + x1 - x0);
				q->y1 = (float)(ry - y1 + y0);
				q->s0 = (float)(x0 * itw);
				q->t0 = (float)(y0 * ith);
				q->s1 = (float)(x1 * itw);
				q->t1 = (float)(y1 * ith);
			}

			x += (float)((int)(glyph->xadv / 10.0f + 0.5f));
		}

		public void fons__flush()
		{
			if (((dirtyRect[0]) < (dirtyRect[2])) && ((dirtyRect[1]) < (dirtyRect[3])))
			{
				dirtyRect[0] = (int)(_params_.width);
				dirtyRect[1] = (int)(_params_.height);
				dirtyRect[2] = (int)(0);
				dirtyRect[3] = (int)(0);
			}

			if ((nverts) > (0))
			{
				nverts = (int)(0);
			}

		}

		public void fons__vertex(float x, float y, float s, float t, uint c)
		{
			verts[nverts * 2 + 0] = (float)(x);
			verts[nverts * 2 + 1] = (float)(y);
			tcoords[nverts * 2 + 0] = (float)(s);
			tcoords[nverts * 2 + 1] = (float)(t);
			colors[nverts] = (uint)(c);
			nverts++;
		}

		public float fons__getVertAlign(FONSfont font, int align, short isize)
		{
			if ((_params_.flags & FONS_ZERO_TOPLEFT) != 0)
			{
				if ((align & FONS_ALIGN_TOP) != 0)
				{
					return (float)(font.ascender * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_MIDDLE) != 0)
				{
					return (float)((font.ascender + font.descender) / 2.0f * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_BASELINE) != 0)
				{
					return (float)(0.0f);
				}
				else if ((align & FONS_ALIGN_BOTTOM) != 0)
				{
					return (float)(font.descender * (float)(isize) / 10.0f);
				}
			}
			else
			{
				if ((align & FONS_ALIGN_TOP) != 0)
				{
					return (float)(-font.ascender * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_MIDDLE) != 0)
				{
					return (float)(-(font.ascender + font.descender) / 2.0f * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_BASELINE) != 0)
				{
					return (float)(0.0f);
				}
				else if ((align & FONS_ALIGN_BOTTOM) != 0)
				{
					return (float)(-font.descender * (float)(isize) / 10.0f);
				}
			}

			return (float)(0.0);
		}

		public float fonsDrawText(float x, float y, StringLocation str)
		{
			if (str.IsNullOrEmpty)
			{
				return 0.0f;
			}

			FONSstate state = fons__getState();
			FONSglyph* glyph = null;
			FONSquad q = new FONSquad();
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state.size * 10.0f);
			short iblur = (short)(state.blur);
			float scale = 0;
			FONSfont font;
			float width = 0;
			if (((state.font) < (0)) || ((state.font) >= (nfonts)))
				return (float)(x);
			font = fonts[state.font];
			if ((font.data) == null)
				return (float)(x);
			scale = (float)(font.font.fons__tt_getPixelHeightScale((float)((float)(isize) / 10.0f)));

			if ((state.align & FONS_ALIGN_LEFT) != 0)
			{
			}
			else if ((state.align & FONS_ALIGN_RIGHT) != 0)
			{
				var bounds = new Bounds();
				width = (float)(fonsTextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width);
			}
			else if ((state.align & FONS_ALIGN_CENTER) != 0)
			{
				var bounds = new Bounds();
				width = (float)(fonsTextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width * 0.5f);
			}

			y += (float)(fons__getVertAlign(font, (int)(state.align), (short)(isize)));
			for (var i = 0; i < str.Remaining; ++i)
			{
				var codepoint = str[i];
				glyph = fons__getGlyph(font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_REQUIRED));
				if (glyph != null)
				{
					fons__getQuad(font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state.spacing), ref x, ref y, &q);
					if ((nverts + 6) > (1024))
						fons__flush();
					fons__vertex((float)(q.x0), (float)(q.y0), (float)(q.s0), (float)(q.t0), (uint)(state.color));
					fons__vertex((float)(q.x1), (float)(q.y1), (float)(q.s1), (float)(q.t1), (uint)(state.color));
					fons__vertex((float)(q.x1), (float)(q.y0), (float)(q.s1), (float)(q.t0), (uint)(state.color));
					fons__vertex((float)(q.x0), (float)(q.y0), (float)(q.s0), (float)(q.t0), (uint)(state.color));
					fons__vertex((float)(q.x0), (float)(q.y1), (float)(q.s0), (float)(q.t1), (uint)(state.color));
					fons__vertex((float)(q.x1), (float)(q.y1), (float)(q.s1), (float)(q.t1), (uint)(state.color));
				}
				prevGlyphIndex = (int)(glyph != null ? glyph->index : -1);
			}
			fons__flush();
			return (float)(x);
		}

		public int fonsTextIterInit(FONStextIter iter, float x, float y, StringLocation str, int bitmapOption)
		{
			FONSstate state = fons__getState();
			float width = 0;

			if (((state.font) < (0)) || ((state.font) >= (nfonts)))
				return (int)(0);
			iter.font = fonts[state.font];
			if ((iter.font.data) == null)
				return (int)(0);
			iter.isize = ((short)(state.size * 10.0f));
			iter.iblur = ((short)(state.blur));
			iter.scale = (float)(iter.font.font.fons__tt_getPixelHeightScale((float)((float)(iter.isize) / 10.0f)));
			if ((state.align & FONS_ALIGN_LEFT) != 0)
			{
			}
			else if ((state.align & FONS_ALIGN_RIGHT) != 0)
			{
				var bounds = new Bounds();
				width = (float)(fonsTextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width);
			}
			else if ((state.align & FONS_ALIGN_CENTER) != 0)
			{
				var bounds = new Bounds();
				width = (float)(fonsTextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width * 0.5f);
			}

			y += (float)(fons__getVertAlign(iter.font, (int)(state.align), (short)(iter.isize)));
			iter.x = (float)(iter.nextx = (float)(x));
			iter.y = (float)(iter.nexty = (float)(y));
			iter.spacing = (float)(state.spacing);
			iter.str = iter.next = str;
			iter.codepoint = (uint)(0);
			iter.prevGlyphIndex = (int)(-1);
			iter.bitmapOption = (int)(bitmapOption);
			return (int)(1);
		}

		public bool fonsTextIterNext(FONStextIter iter, FONSquad* quad)
		{
			iter.str = iter.next;

			if (iter.str.IsNullOrEmpty)
			{
				return false;
			}

			iter.codepoint = iter.str[0];
			iter.x = (float)(iter.nextx);
			iter.y = (float)(iter.nexty);
			var glyph = fons__getGlyph(iter.font, (uint)(iter.codepoint), (short)(iter.isize), (short)(iter.iblur), (int)(iter.bitmapOption));
			if (glyph != null)
				fons__getQuad(iter.font, (int)(iter.prevGlyphIndex), glyph, (float)(iter.scale), (float)(iter.spacing), ref iter.nextx, ref iter.nexty, quad);
			iter.prevGlyphIndex = (int)(glyph != null ? glyph->index : -1);

			++iter.next;

			return true;
		}

		public void fonsDrawDebug(float x, float y)
		{
			int i = 0;
			int w = (int)(_params_.width);
			int h = (int)(_params_.height);
			float u = (float)((w) == (0) ? 0 : (1.0f / w));
			float v = (float)((h) == (0) ? 0 : (1.0f / h));
			if ((nverts + 6 + 6) > (1024))
				fons__flush();
			fons__vertex((float)(x + 0), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex((float)(x + w), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex((float)(x + w), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex((float)(x + 0), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex((float)(x + 0), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex((float)(x + w), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex((float)(x + 0), (float)(y + 0), (float)(0), (float)(0), (uint)(0xffffffff));
			fons__vertex((float)(x + w), (float)(y + h), (float)(1), (float)(1), (uint)(0xffffffff));
			fons__vertex((float)(x + w), (float)(y + 0), (float)(1), (float)(0), (uint)(0xffffffff));
			fons__vertex((float)(x + 0), (float)(y + 0), (float)(0), (float)(0), (uint)(0xffffffff));
			fons__vertex((float)(x + 0), (float)(y + h), (float)(0), (float)(1), (uint)(0xffffffff));
			fons__vertex((float)(x + w), (float)(y + h), (float)(1), (float)(1), (uint)(0xffffffff));
			for (i = (int)(0); (i) < (atlas.nnodes); i++)
			{
				FONSatlasNode* n = &atlas.nodes[i];
				if ((nverts + 6) > (1024))
					fons__flush();
				fons__vertex((float)(x + n->x + 0), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));
				fons__vertex((float)(x + n->x + n->width), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));
				fons__vertex((float)(x + n->x + n->width), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));
				fons__vertex((float)(x + n->x + 0), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));
				fons__vertex((float)(x + n->x + 0), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));
				fons__vertex((float)(x + n->x + n->width), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));
			}
			fons__flush();
		}

		public float fonsTextBounds(float x, float y, StringLocation str, ref Bounds bounds)
		{
			FONSstate state = fons__getState();
			FONSquad q = new FONSquad();
			FONSglyph* glyph = null;
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state.size * 10.0f);
			short iblur = (short)(state.blur);
			float scale = 0;
			FONSfont font;
			float startx = 0;
			float advance = 0;
			float minx = 0;
			float miny = 0;
			float maxx = 0;
			float maxy = 0;
			if (((state.font) < (0)) || ((state.font) >= (nfonts)))
				return (float)(0);
			font = fonts[state.font];
			if ((font.data) == null)
				return (float)(0);
			scale = (float)(font.font.fons__tt_getPixelHeightScale((float)((float)(isize) / 10.0f)));
			y += (float)(fons__getVertAlign(font, (int)(state.align), (short)(isize)));
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			startx = (float)(x);
			for (var i = 0; i < str.Remaining; ++i)
			{
				var codepoint = str[i];
				glyph = fons__getGlyph(font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_OPTIONAL));
				if (glyph != null)
				{
					fons__getQuad(font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state.spacing), ref x, ref y, &q);
					if ((q.x0) < (minx))
						minx = (float)(q.x0);
					if ((q.x1) > (maxx))
						maxx = (float)(q.x1);
					if ((_params_.flags & FONS_ZERO_TOPLEFT) != 0)
					{
						if ((q.y0) < (miny))
							miny = (float)(q.y0);
						if ((q.y1) > (maxy))
							maxy = (float)(q.y1);
					}
					else
					{
						if ((q.y1) < (miny))
							miny = (float)(q.y1);
						if ((q.y0) > (maxy))
							maxy = (float)(q.y0);
					}
				}
				prevGlyphIndex = (int)(glyph != null ? glyph->index : -1);
			}
			advance = (float)(x - startx);
			if ((state.align & FONS_ALIGN_LEFT) != 0)
			{
			}
			else if ((state.align & FONS_ALIGN_RIGHT) != 0)
			{
				minx -= (float)(advance);
				maxx -= (float)(advance);
			}
			else if ((state.align & FONS_ALIGN_CENTER) != 0)
			{
				minx -= (float)(advance * 0.5f);
				maxx -= (float)(advance * 0.5f);
			}

			bounds.b1 = (float)(minx);
			bounds.b2 = (float)(miny);
			bounds.b3 = (float)(maxx);
			bounds.b4 = (float)(maxy);

			return (float)(advance);
		}

		public void fonsVertMetrics(float* ascender, float* descender, float* lineh)
		{
			FONSfont font;
			FONSstate state = fons__getState();
			short isize = 0;
			if (((state.font) < (0)) || ((state.font) >= (nfonts)))
				return;
			font = fonts[state.font];
			isize = ((short)(state.size * 10.0f));
			if ((font.data) == null)
				return;
			if ((ascender) != null)
				*ascender = (float)(font.ascender * isize / 10.0f);
			if ((descender) != null)
				*descender = (float)(font.descender * isize / 10.0f);
			if ((lineh) != null)
				*lineh = (float)(font.lineh * isize / 10.0f);
		}

		public void fonsLineBounds(float y, ref float miny, ref float maxy)
		{
			FONSfont font;
			FONSstate state = fons__getState();
			short isize = 0;
			if (((state.font) < (0)) || ((state.font) >= (nfonts)))
				return;
			font = fonts[state.font];
			isize = ((short)(state.size * 10.0f));
			if ((font.data) == null)
				return;
			y += (float)(fons__getVertAlign(font, (int)(state.align), (short)(isize)));
			if ((_params_.flags & FONS_ZERO_TOPLEFT) != 0)
			{
				miny = (float)(y - font.ascender * (float)(isize) / 10.0f);
				maxy = (float)(miny + font.lineh * isize / 10.0f);
			}
			else
			{
				maxy = (float)(y + font.descender * (float)(isize) / 10.0f);
				miny = (float)(maxy - font.lineh * isize / 10.0f);
			}

		}

		public byte* fonsGetTextureData(int* width, int* height)
		{
			if (width != null)
				*width = (int)(_params_.width);
			if (height != null)
				*height = (int)(_params_.height);
			return texData;
		}

		public int fonsValidateTexture(int* dirty)
		{
			if (((dirtyRect[0]) < (dirtyRect[2])) && ((dirtyRect[1]) < (dirtyRect[3])))
			{
				dirty[0] = (int)(dirtyRect[0]);
				dirty[1] = (int)(dirtyRect[1]);
				dirty[2] = (int)(dirtyRect[2]);
				dirty[3] = (int)(dirtyRect[3]);
				dirtyRect[0] = (int)(_params_.width);
				dirtyRect[1] = (int)(_params_.height);
				dirtyRect[2] = (int)(0);
				dirtyRect[3] = (int)(0);
				return (int)(1);
			}

			return (int)(0);
		}

		public void fonsSetErrorCallback(FONScontext.handleErrorDelegate callback, void* uptr)
		{
			handleError = callback;
			errorUptr = uptr;
		}

		public void fonsGetAtlasSize(int* width, int* height)
		{
			*width = (int)(_params_.width);
			*height = (int)(_params_.height);
		}

		public int fonsExpandAtlas(int width, int height)
		{
			int i = 0;
			int maxy = (int)(0);
			byte* data = null;
			width = (int)(fons__maxi((int)(width), (int)(_params_.width)));
			height = (int)(fons__maxi((int)(height), (int)(_params_.height)));
			if (((width) == (_params_.width)) && ((height) == (_params_.height)))
				return (int)(1);
			fons__flush();

			data = (byte*)(CRuntime.malloc((ulong)(width * height)));
			if ((data) == null)
				return (int)(0);
			for (i = (int)(0); (i) < (_params_.height); i++)
			{
				byte* dst = &data[i * width];
				byte* src = &texData[i * _params_.width];
				CRuntime.memcpy(dst, src, (ulong)(_params_.width));
				if ((width) > (_params_.width))
					CRuntime.memset(dst + _params_.width, (int)(0), (ulong)(width - _params_.width));
			}
			if ((height) > (_params_.height))
				CRuntime.memset(&data[_params_.height * width], (int)(0), (ulong)((height - _params_.height) * width));
			CRuntime.free(texData);
			texData = data;
			atlas.fons__atlasExpand((int)(width), (int)(height));
			for (i = (int)(0); (i) < (atlas.nnodes); i++)
			{
				maxy = (int)(fons__maxi((int)(maxy), (int)(atlas.nodes[i].y)));
			}
			dirtyRect[0] = (int)(0);
			dirtyRect[1] = (int)(0);
			dirtyRect[2] = (int)(_params_.width);
			dirtyRect[3] = (int)(maxy);
			_params_.width = (int)(width);
			_params_.height = (int)(height);
			itw = (float)(1.0f / _params_.width);
			ith = (float)(1.0f / _params_.height);
			return (int)(1);
		}

		public int fonsResetAtlas(int width, int height)
		{
			int i = 0;
			int j = 0;
			fons__flush();

			atlas.fons__atlasReset((int)(width), (int)(height));
			texData = (byte*)(CRuntime.realloc(texData, (ulong)(width * height)));
			if ((texData) == null)
				return (int)(0);
			CRuntime.memset(texData, (int)(0), (ulong)(width * height));
			dirtyRect[0] = (int)(width);
			dirtyRect[1] = (int)(height);
			dirtyRect[2] = (int)(0);
			dirtyRect[3] = (int)(0);
			for (i = (int)(0); (i) < (nfonts); i++)
			{
				FONSfont font = fonts[i];
				font.nglyphs = (int)(0);
				for (j = (int)(0); (j) < (256); j++)
				{
					font.lut[j] = (int)(-1);
				}
			}
			_params_.width = (int)(width);
			_params_.height = (int)(height);
			itw = (float)(1.0f / _params_.width);
			ith = (float)(1.0f / _params_.height);
			fons__addWhiteRect((int)(2), (int)(2));
			return (int)(1);
		}

		public static FONSglyph* fons__allocGlyph(FONSfont font)
		{
			if ((font.nglyphs + 1) > (font.cglyphs))
			{
				font.cglyphs = (int)((font.cglyphs) == (0) ? 8 : font.cglyphs * 2);
				font.glyphs = (FONSglyph*)(CRuntime.realloc(font.glyphs, (ulong)(sizeof(FONSglyph) * font.cglyphs)));
				if ((font.glyphs) == null)
					return null;
			}

			font.nglyphs++;
			return &font.glyphs[font.nglyphs - 1];
		}

		public static void fons__blurCols(byte* dst, int w, int h, int dstStride, int alpha)
		{
			int x = 0;
			int y = 0;
			for (y = (int)(0); (y) < (h); y++)
			{
				int z = (int)(0);
				for (x = (int)(1); (x) < (w); x++)
				{
					z += (int)((alpha * (((int)(dst[x]) << 7) - z)) >> 16);
					dst[x] = ((byte)(z >> 7));
				}
				dst[w - 1] = (byte)(0);
				z = (int)(0);
				for (x = (int)(w - 2); (x) >= (0); x--)
				{
					z += (int)((alpha * (((int)(dst[x]) << 7) - z)) >> 16);
					dst[x] = ((byte)(z >> 7));
				}
				dst[0] = (byte)(0);
				dst += dstStride;
			}
		}

		public static void fons__blurRows(byte* dst, int w, int h, int dstStride, int alpha)
		{
			int x = 0;
			int y = 0;
			for (x = (int)(0); (x) < (w); x++)
			{
				int z = (int)(0);
				for (y = (int)(dstStride); (y) < (h * dstStride); y += (int)(dstStride))
				{
					z += (int)((alpha * (((int)(dst[y]) << 7) - z)) >> 16);
					dst[y] = ((byte)(z >> 7));
				}
				dst[(h - 1) * dstStride] = (byte)(0);
				z = (int)(0);
				for (y = (int)((h - 2) * dstStride); (y) >= (0); y -= (int)(dstStride))
				{
					z += (int)((alpha * (((int)(dst[y]) << 7) - z)) >> 16);
					dst[y] = ((byte)(z >> 7));
				}
				dst[0] = (byte)(0);
				dst++;
			}
		}

		public static uint fons__hashint(uint a)
		{
			a += (uint)(~(a << 15));
			a ^= (uint)(a >> 10);
			a += (uint)(a << 3);
			a ^= (uint)(a >> 6);
			a += (uint)(~(a << 11));
			a ^= (uint)(a >> 16);
			return (uint)(a);
		}

		public static int fons__mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int fons__maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}
	}
}