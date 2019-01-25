using System;
using NvgSharp;
using StbSharp;

namespace FontStashSharp
{
	internal unsafe class FontSystem : IDisposable
	{
		public delegate void handleErrorDelegate(void* uptr, int error, int val);

		public const int FONS_ZERO_TOPLEFT = 1;
		public const int FONS_ZERO_BOTTOMLEFT = 2;
		public const int FONS_GLYPH_BITMAP_OPTIONAL = 1;
		public const int FONS_GLYPH_BITMAP_REQUIRED = 2;
		public const int FONS_ATLAS_FULL = 1;
		public const int FONS_SCRATCH_FULL = 2;
		public const int FONS_STATES_OVERFLOW = 3;
		public const int FONS_STATES_UNDERFLOW = 4;
		private readonly uint[] _colors = new uint[1024];
		private readonly int[] _dirtyRect = new int[4];
		private Font[] _fonts;
		private int _fontsNumber;
		private float _ith;
		private float _itw;
		private readonly FontSystemParams _params_ = new FontSystemParams();
		private readonly byte* _scratch;
		private int _scratchCount;
		private readonly FontSystemState[] _states = new FontSystemState[20];
		private int _statesCount;
		private byte[] _texData;
		private readonly float[] _textureCoords = new float[1024 * 2];
		private readonly float[] _verts = new float[1024 * 2];
		private int _vertsNumber;
		private readonly FontAtlas atlas;

		private int flags;

		public FontSystem(FontSystemParams p)
		{
			_params_ = p;
			_scratch = (byte*)CRuntime.malloc((ulong)96000);

			atlas = new FontAtlas(_params_.Width, _params_.Height, 256);
			_fonts = new Font[4];
			_fontsNumber = 0;
			_itw = 1.0f / _params_.Width;
			_ith = 1.0f / _params_.Height;
			_texData = new byte[_params_.Width * _params_.Height];
			Array.Clear(_texData, 0, _texData.Length);
			_dirtyRect[0] = _params_.Width;
			_dirtyRect[1] = _params_.Height;
			_dirtyRect[2] = 0;
			_dirtyRect[3] = 0;
			AddWhiteRect(2, 2);
			PushState();
			ClearState();
		}

		public void Dispose()
		{
			var i = 0;
			for (i = 0; i < _fontsNumber; ++i)
				FreeFont(_fonts[i]);

			if (_scratch != null)
				CRuntime.free(_scratch);
		}


		public void AddWhiteRect(int w, int h)
		{
			var x = 0;
			var y = 0;
			var gx = 0;
			var gy = 0;
			if (atlas.AddRect(w, h, &gx, &gy) == 0)
				return;
			fixed (byte* dst2 = &_texData[gx + gy * _params_.Width])
			{
				var dst = dst2;
				for (y = 0; y < h; y++)
				{
					for (x = 0; x < w; x++)
						dst[x] = 0xff;
					dst += _params_.Width;
				}
			}

			_dirtyRect[0] = Mini(_dirtyRect[0], gx);
			_dirtyRect[1] = Mini(_dirtyRect[1], gy);
			_dirtyRect[2] = Maxi(_dirtyRect[2], gx + w);
			_dirtyRect[3] = Maxi(_dirtyRect[3], gy + h);
		}

		public FontSystemState GetState()
		{
			return _states[_statesCount - 1];
		}

		public int AddFallbackFont(int _base_, int fallback)
		{
			var baseFont = _fonts[_base_];
			if (baseFont.FallbacksCount < 20)
			{
				baseFont.Fallbacks[baseFont.FallbacksCount++] = fallback;
				return 1;
			}

			return 0;
		}

		public void SetSize(float size)
		{
			GetState().Size = size;
		}

		public void SetColor(uint color)
		{
			GetState().Color = color;
		}

		public void SetSpacing(float spacing)
		{
			GetState().Spacing = spacing;
		}

		public void SetBlur(float blur)
		{
			GetState().Blur = blur;
		}

		public void SetAlign(Alignment align)
		{
			GetState().Align = align;
		}

		public void SetFont(int font)
		{
			GetState().Font = font;
		}

		public void PushState()
		{
			if (_statesCount >= _states.Length)
				throw new Exception("FONS_STATES_OVERFLOW");

			if (_statesCount > 0)
				_states[_statesCount] = _states[_statesCount - 1].Clone();
			else
				_states[_statesCount] = new FontSystemState();
			_statesCount++;
		}

		public void PopState()
		{
			if (_statesCount <= 1)
				throw new Exception("FONS_STATES_OVERFLOW");

			_statesCount--;
		}

		public void ClearState()
		{
			var state = GetState();
			state.Size = 12.0f;
			state.Color = 0xffffffff;
			state.Font = 0;
			state.Blur = 0;
			state.Spacing = 0;
			state.Align = Alignment.Left | Alignment.Baseline;
		}

		public int LoadFont(StbTrueType.stbtt_fontinfo font, byte* data, int dataSize)
		{
			var stbError = 0;
			font.userdata = this;
			stbError = StbTrueType.stbtt_InitFont(font, data, 0);
			return stbError;
		}

		public void FreeFont(Font font)
		{
			if (font == null)
				return;
			if (font.Glyphs != null)
				CRuntime.free(font.Glyphs);
		}

		public int AllocFont()
		{
			Font font = null;
			if (_fontsNumber + 1 > _fonts.Length)
			{
				_fonts = new Font[_fonts.Length];
				if (_fonts == null)
					return -1;
			}

			font = new Font();
			if (font == null)
				goto error;
			font.Glyphs = (FontGlyph*)CRuntime.malloc((ulong)(sizeof(FontGlyph) * 256));
			if (font.Glyphs == null)
				goto error;
			font.GlyphsCount = 256;
			font.GlyphsNumber = 0;
			_fonts[_fontsNumber++] = font;
			return _fontsNumber - 1;
		error:
			;
			FreeFont(font);
			return -1;
		}

		public int AddFontMem(string name, byte[] data)
		{
			var i = 0;
			var ascent = 0;
			var descent = 0;
			var fh = 0;
			var lineGap = 0;
			Font font;
			var idx = AllocFont();
			if (idx == -1)
				return -1;
			font = _fonts[idx];
			font.Name = name;
			for (i = 0; i < 256; ++i)
				font.Lut[i] = -1;
			font.Data = data;
			_scratchCount = 0;
			fixed (byte* ptr = data)
			{
				if (LoadFont(font.FontInfo, ptr, data.Length) == 0)
					goto error;
			}

			font.FontInfo.__tt_getFontVMetrics(&ascent, &descent, &lineGap);
			fh = ascent - descent;
			font.Ascender = ascent / (float)fh;
			font.Descender = descent / (float)fh;
			font.LineHeight = (fh + lineGap) / (float)fh;
			return idx;
		error:
			;
			FreeFont(font);
			_fontsNumber--;
			return -1;
		}

		public int GetFontByName(string name)
		{
			var i = 0;
			for (i = 0; i < _fontsNumber; i++)
				if (_fonts[i].Name == name)
					return i;
			return -1;
		}

		public void Blur(byte* dst, int w, int h, int dstStride, int blur)
		{
			var alpha = 0;
			float sigma = 0;
			if (blur < 1)
				return;
			sigma = blur * 0.57735f;
			alpha = (int)((1 << 16) * (1.0f - Math.Exp(-2.3f / (sigma + 1.0f))));
			BlurRows(dst, w, h, dstStride, alpha);
			BlurCols(dst, w, h, dstStride, alpha);
			BlurRows(dst, w, h, dstStride, alpha);
			BlurCols(dst, w, h, dstStride, alpha);
		}

		public FontGlyph* GetGlyph(Font font, int codepoint, short isize, short iblur, int bitmapOption)
		{
			var i = 0;
			var g = 0;
			var advance = 0;
			var lsb = 0;
			var x0 = 0;
			var y0 = 0;
			var x1 = 0;
			var y1 = 0;
			var gw = 0;
			var gh = 0;
			var gx = 0;
			var gy = 0;
			var x = 0;
			var y = 0;
			float scale = 0;
			FontGlyph* glyph = null;
			int h = 0;
			var size = isize / 10.0f;
			var pad = 0;
			var added = 0;
			var renderFont = font;
			if (isize < 2)
				return null;
			if (iblur > 20)
				iblur = 20;
			pad = iblur + 2;
			_scratchCount = 0;
			h = HashInt(codepoint) & (256 - 1);
			i = font.Lut[h];
			while (i != -1)
			{
				if (font.Glyphs[i].Codepoint == codepoint && font.Glyphs[i].Size == isize &&
					font.Glyphs[i].Blur == iblur)
				{
					glyph = &font.Glyphs[i];
					if (bitmapOption == FONS_GLYPH_BITMAP_OPTIONAL || glyph->X0 >= 0 && glyph->Y0 >= 0)
						return glyph;
					break;
				}

				i = font.Glyphs[i].Next;
			}

			g = font.FontInfo.__tt_getGlyphIndex((int)codepoint);
			if (g == 0)
				for (i = 0; i < font.FallbacksCount; ++i)
				{
					var fallbackFont = _fonts[font.Fallbacks[i]];
					var fallbackIndex = fallbackFont.FontInfo.__tt_getGlyphIndex((int)codepoint);
					if (fallbackIndex != 0)
					{
						g = fallbackIndex;
						renderFont = fallbackFont;
						break;
					}
				}

			scale = renderFont.FontInfo.__tt_getPixelHeightScale(size);
			renderFont.FontInfo.__tt_buildGlyphBitmap(g, size, scale, &advance, &lsb, &x0, &y0, &x1, &y1);
			gw = x1 - x0 + pad * 2;
			gh = y1 - y0 + pad * 2;
			if (bitmapOption == FONS_GLYPH_BITMAP_REQUIRED)
			{
				added = atlas.AddRect(gw, gh, &gx, &gy);
				if (added == 0)
					throw new Exception("FONS_ATLAS_FULL");
			}
			else
			{
				gx = -1;
				gy = -1;
			}

			if (glyph == null)
			{
				glyph = AllocGlyph(font);
				glyph->Codepoint = codepoint;
				glyph->Size = isize;
				glyph->Blur = iblur;
				glyph->Next = 0;
				glyph->Next = font.Lut[h];
				font.Lut[h] = font.GlyphsNumber - 1;
			}

			glyph->Index = g;
			glyph->X0 = (short)gx;
			glyph->Y0 = (short)gy;
			glyph->X1 = (short)(glyph->X0 + gw);
			glyph->Y1 = (short)(glyph->Y0 + gh);
			glyph->XAdvance = (short)(scale * advance * 10.0f);
			glyph->XOffset = (short)(x0 - pad);
			glyph->YOffset = (short)(y0 - pad);
			if (bitmapOption == FONS_GLYPH_BITMAP_OPTIONAL)
				return glyph;

			fixed (byte* dst = &_texData[glyph->X0 + pad + (glyph->Y0 + pad) * _params_.Width])
			{
				renderFont.FontInfo.__tt_renderGlyphBitmap(dst, gw - pad * 2, gh - pad * 2, _params_.Width, scale,
					scale, g);
			}

			fixed (byte* dst = &_texData[glyph->X0 + glyph->Y0 * _params_.Width])
			{
				for (y = 0; y < gh; y++)
				{
					dst[y * _params_.Width] = 0;
					dst[gw - 1 + y * _params_.Width] = 0;
				}

				for (x = 0; x < gw; x++)
				{
					dst[x] = 0;
					dst[x + (gh - 1) * _params_.Width] = 0;
				}
			}


			if (iblur > 0)
			{
				_scratchCount = 0;
				fixed (byte* bdst = &_texData[glyph->X0 + glyph->Y0 * _params_.Width])
				{
					Blur(bdst, gw, gh, _params_.Width, iblur);
				}
			}

			_dirtyRect[0] = Mini(_dirtyRect[0], glyph->X0);
			_dirtyRect[1] = Mini(_dirtyRect[1], glyph->Y0);
			_dirtyRect[2] = Maxi(_dirtyRect[2], glyph->X1);
			_dirtyRect[3] = Maxi(_dirtyRect[3], glyph->Y1);
			return glyph;
		}

		public void GetQuad(Font font, int prevGlyphIndex, FontGlyph* glyph, float scale, float spacing, ref float x,
			ref float y, FontGlyphSquad* q)
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
				var adv = font.FontInfo.__tt_getGlyphKernAdvance(prevGlyphIndex, glyph->Index) * scale;
				x += (int)(adv + spacing + 0.5f);
			}

			xoff = (short)(glyph->XOffset + 1);
			yoff = (short)(glyph->YOffset + 1);
			x0 = glyph->X0 + 1;
			y0 = glyph->Y0 + 1;
			x1 = glyph->X1 - 1;
			y1 = glyph->Y1 - 1;
			if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
			{
				rx = (int)(x + xoff);
				ry = (int)(y + yoff);
				q->X0 = rx;
				q->Y0 = ry;
				q->X1 = rx + x1 - x0;
				q->Y1 = ry + y1 - y0;
				q->S0 = x0 * _itw;
				q->T0 = y0 * _ith;
				q->S1 = x1 * _itw;
				q->T1 = y1 * _ith;
			}
			else
			{
				rx = (int)(x + xoff);
				ry = (int)(y - yoff);
				q->X0 = rx;
				q->Y0 = ry;
				q->X1 = rx + x1 - x0;
				q->Y1 = ry - y1 + y0;
				q->S0 = x0 * _itw;
				q->T0 = y0 * _ith;
				q->S1 = x1 * _itw;
				q->T1 = y1 * _ith;
			}

			x += (int)(glyph->XAdvance / 10.0f + 0.5f);
		}

		public void Flush()
		{
			if (_dirtyRect[0] < _dirtyRect[2] && _dirtyRect[1] < _dirtyRect[3])
			{
				_dirtyRect[0] = _params_.Width;
				_dirtyRect[1] = _params_.Height;
				_dirtyRect[2] = 0;
				_dirtyRect[3] = 0;
			}

			if (_vertsNumber > 0)
				_vertsNumber = 0;
		}

		public void AddVertex(float x, float y, float s, float t, uint c)
		{
			_verts[_vertsNumber * 2 + 0] = x;
			_verts[_vertsNumber * 2 + 1] = y;
			_textureCoords[_vertsNumber * 2 + 0] = s;
			_textureCoords[_vertsNumber * 2 + 1] = t;
			_colors[_vertsNumber] = c;
			_vertsNumber++;
		}

		public float GetVertAlign(Font font, Alignment align, short isize)
		{
			if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
			{
				if ((align & Alignment.Top) != 0)
					return font.Ascender * isize / 10.0f;
				if ((align & Alignment.Middle) != 0)
					return (font.Ascender + font.Descender) / 2.0f * isize / 10.0f;
				if ((align & Alignment.Baseline) != 0)
					return 0.0f;
				if ((align & Alignment.Bottom) != 0)
					return font.Descender * isize / 10.0f;
			}
			else
			{
				if ((align & Alignment.Top) != 0)
					return -font.Ascender * isize / 10.0f;
				if ((align & Alignment.Middle) != 0)
					return -(font.Ascender + font.Descender) / 2.0f * isize / 10.0f;
				if ((align & Alignment.Baseline) != 0)
					return 0.0f;
				if ((align & Alignment.Bottom) != 0)
					return -font.Descender * isize / 10.0f;
			}

			return (float)0.0;
		}
		public int TextIterInit(FontTextIterator iter, float x, float y, StringSegment str, int bitmapOption)
		{
			var state = GetState();
			float width = 0;

			if (state.Font < 0 || state.Font >= _fontsNumber)
				return 0;
			iter.Font = _fonts[state.Font];
			if (iter.Font.Data == null)
				return 0;
			iter.iSize = (short)(state.Size * 10.0f);
			iter.iBlur = (short)state.Blur;
			iter.Scale = iter.Font.FontInfo.__tt_getPixelHeightScale(iter.iSize / 10.0f);
			if ((state.Align & Alignment.Left) != 0)
			{
			}
			else if ((state.Align & Alignment.Right) != 0)
			{
				var bounds = new Bounds();
				width = TextBounds(x, y, str, ref bounds);
				x -= width;
			}
			else if ((state.Align & Alignment.Center) != 0)
			{
				var bounds = new Bounds();
				width = TextBounds(x, y, str, ref bounds);
				x -= width * 0.5f;
			}

			y += GetVertAlign(iter.Font, state.Align, iter.iSize);
			iter.X = iter.NextX = x;
			iter.Y = iter.NextY = y;
			iter.Spacing = state.Spacing;
			iter.Str = str;
			iter.Next = str;
			iter.Codepoint = 0;
			iter.PrevGlyphIndex = -1;
			iter.BitmapOption = bitmapOption;
			return 1;
		}

		public bool TextIterNext(FontTextIterator iter, FontGlyphSquad* quad)
		{
			iter.Str = iter.Next;

			if (iter.Str.IsNullOrEmpty)
				return false;

			iter.Codepoint = Char.ConvertToUtf32(iter.Str.String, iter.Str.Location);
			iter.X = iter.NextX;
			iter.Y = iter.NextY;
			var glyph = GetGlyph(iter.Font, iter.Codepoint, iter.iSize, iter.iBlur, iter.BitmapOption);
			if (glyph != null)
				GetQuad(iter.Font, iter.PrevGlyphIndex, glyph, iter.Scale, iter.Spacing, ref iter.NextX, ref iter.NextY,
					quad);
			iter.PrevGlyphIndex = glyph != null ? glyph->Index : -1;

			if (Char.IsSurrogatePair(iter.Str.String, iter.Str.Location))
			{
				iter.Next.Location += 2;
			}
			else
			{
				++iter.Next.Location;
			}

			return true;
		}

		public void DrawDebug(float x, float y)
		{
			var i = 0;
			var w = _params_.Width;
			var h = _params_.Height;
			var u = w == 0 ? 0 : 1.0f / w;
			var v = h == 0 ? 0 : 1.0f / h;
			if (_vertsNumber + 6 + 6 > 1024)
				Flush();
			AddVertex(x + 0, y + 0, u, v, 0x0fffffff);
			AddVertex(x + w, y + h, u, v, 0x0fffffff);
			AddVertex(x + w, y + 0, u, v, 0x0fffffff);
			AddVertex(x + 0, y + 0, u, v, 0x0fffffff);
			AddVertex(x + 0, y + h, u, v, 0x0fffffff);
			AddVertex(x + w, y + h, u, v, 0x0fffffff);
			AddVertex(x + 0, y + 0, 0, 0, 0xffffffff);
			AddVertex(x + w, y + h, 1, 1, 0xffffffff);
			AddVertex(x + w, y + 0, 1, 0, 0xffffffff);
			AddVertex(x + 0, y + 0, 0, 0, 0xffffffff);
			AddVertex(x + 0, y + h, 0, 1, 0xffffffff);
			AddVertex(x + w, y + h, 1, 1, 0xffffffff);
			for (i = 0; i < atlas.NodesNumber; i++)
			{
				var n = &atlas.Nodes[i];
				if (_vertsNumber + 6 > 1024)
					Flush();
				AddVertex(x + n->X + 0, y + n->Y + 0, u, v, 0xc00000ff);
				AddVertex(x + n->X + n->Width, y + n->Y + 1, u, v, 0xc00000ff);
				AddVertex(x + n->X + n->Width, y + n->Y + 0, u, v, 0xc00000ff);
				AddVertex(x + n->X + 0, y + n->Y + 0, u, v, 0xc00000ff);
				AddVertex(x + n->X + 0, y + n->Y + 1, u, v, 0xc00000ff);
				AddVertex(x + n->X + n->Width, y + n->Y + 1, u, v, 0xc00000ff);
			}

			Flush();
		}

		public float TextBounds(float x, float y, StringSegment str, ref Bounds bounds)
		{
			var state = GetState();
			var q = new FontGlyphSquad();
			FontGlyph* glyph = null;
			var prevGlyphIndex = -1;
			var isize = (short)(state.Size * 10.0f);
			var iblur = (short)state.Blur;
			float scale = 0;
			Font font;
			float startx = 0;
			float advance = 0;
			float minx = 0;
			float miny = 0;
			float maxx = 0;
			float maxy = 0;
			if (state.Font < 0 || state.Font >= _fontsNumber)
				return 0;
			font = _fonts[state.Font];
			if (font.Data == null)
				return 0;
			scale = font.FontInfo.__tt_getPixelHeightScale(isize / 10.0f);
			y += GetVertAlign(font, state.Align, isize);
			minx = maxx = x;
			miny = maxy = y;
			startx = x;
			for (int i = 0; i < str.Length; i += Char.IsSurrogatePair(str.String, i + str.Location) ? 2 : 1)
			{
				var codepoint = Char.ConvertToUtf32(str.String, i + str.Location);
				glyph = GetGlyph(font, codepoint, isize, iblur, FONS_GLYPH_BITMAP_OPTIONAL);
				if (glyph != null)
				{
					GetQuad(font, prevGlyphIndex, glyph, scale, state.Spacing, ref x, ref y, &q);
					if (q.X0 < minx)
						minx = q.X0;
					if (q.X1 > maxx)
						maxx = q.X1;
					if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
					{
						if (q.Y0 < miny)
							miny = q.Y0;
						if (q.Y1 > maxy)
							maxy = q.Y1;
					}
					else
					{
						if (q.Y1 < miny)
							miny = q.Y1;
						if (q.Y0 > maxy)
							maxy = q.Y0;
					}
				}

				prevGlyphIndex = glyph != null ? glyph->Index : -1;
			}

			advance = x - startx;
			if ((state.Align & Alignment.Left) != 0)
			{
			}
			else if ((state.Align & Alignment.Right) != 0)
			{
				minx -= advance;
				maxx -= advance;
			}
			else if ((state.Align & Alignment.Center) != 0)
			{
				minx -= advance * 0.5f;
				maxx -= advance * 0.5f;
			}

			bounds.b1 = minx;
			bounds.b2 = miny;
			bounds.b3 = maxx;
			bounds.b4 = maxy;

			return advance;
		}

		public void VertMetrics(out float ascender, out float descender, out float lineh)
		{
			ascender = descender = lineh = 0;
			Font font;
			var state = GetState();
			short isize = 0;
			if (state.Font < 0 || state.Font >= _fontsNumber)
				return;
			font = _fonts[state.Font];
			isize = (short)(state.Size * 10.0f);
			if (font.Data == null)
				return;

			ascender = font.Ascender * isize / 10.0f;
			descender = font.Descender * isize / 10.0f;
			lineh = font.LineHeight * isize / 10.0f;
		}

		public void LineBounds(float y, ref float miny, ref float maxy)
		{
			Font font;
			var state = GetState();
			short isize = 0;
			if (state.Font < 0 || state.Font >= _fontsNumber)
				return;
			font = _fonts[state.Font];
			isize = (short)(state.Size * 10.0f);
			if (font.Data == null)
				return;
			y += GetVertAlign(font, state.Align, isize);
			if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
			{
				miny = y - font.Ascender * isize / 10.0f;
				maxy = miny + font.LineHeight * isize / 10.0f;
			}
			else
			{
				maxy = y + font.Descender * isize / 10.0f;
				miny = maxy - font.LineHeight * isize / 10.0f;
			}
		}

		public byte[] GetTextureData(int* width, int* height)
		{
			if (width != null)
				*width = _params_.Width;
			if (height != null)
				*height = _params_.Height;
			return _texData;
		}

		public int ValidateTexture(int* dirty)
		{
			if (_dirtyRect[0] < _dirtyRect[2] && _dirtyRect[1] < _dirtyRect[3])
			{
				dirty[0] = _dirtyRect[0];
				dirty[1] = _dirtyRect[1];
				dirty[2] = _dirtyRect[2];
				dirty[3] = _dirtyRect[3];
				_dirtyRect[0] = _params_.Width;
				_dirtyRect[1] = _params_.Height;
				_dirtyRect[2] = 0;
				_dirtyRect[3] = 0;
				return 1;
			}

			return 0;
		}

		public void GetAtlasSize(int* width, int* height)
		{
			*width = _params_.Width;
			*height = _params_.Height;
		}

		public int ExpandAtlas(int width, int height)
		{
			var i = 0;
			var maxy = 0;
			width = Maxi(width, _params_.Width);
			height = Maxi(height, _params_.Height);
			if (width == _params_.Width && height == _params_.Height)
				return 1;
			Flush();

			var data = new byte[width * height];
			for (i = 0; i < _params_.Height; i++)
				fixed (byte* dst = &data[i * width])
				{
					fixed (byte* src = &_texData[i * _params_.Width])
					{
						CRuntime.memcpy(dst, src, (ulong)_params_.Width);
						if (width > _params_.Width)
							CRuntime.memset(dst + _params_.Width, 0, (ulong)(width - _params_.Width));
					}
				}

			if (height > _params_.Height)
				Array.Clear(data, _params_.Height * width, (height - _params_.Height) * width);

			_texData = data;
			atlas.Expand(width, height);
			for (i = 0; i < atlas.NodesNumber; i++)
				maxy = Maxi(maxy, atlas.Nodes[i].Y);
			_dirtyRect[0] = 0;
			_dirtyRect[1] = 0;
			_dirtyRect[2] = _params_.Width;
			_dirtyRect[3] = maxy;
			_params_.Width = width;
			_params_.Height = height;
			_itw = 1.0f / _params_.Width;
			_ith = 1.0f / _params_.Height;
			return 1;
		}

		public int ResetAtlas(int width, int height)
		{
			var i = 0;
			var j = 0;
			Flush();

			atlas.Reset(width, height);
			_texData = new byte[width * height];
			Array.Clear(_texData, 0, _texData.Length);
			_dirtyRect[0] = width;
			_dirtyRect[1] = height;
			_dirtyRect[2] = 0;
			_dirtyRect[3] = 0;
			for (i = 0; i < _fontsNumber; i++)
			{
				var font = _fonts[i];
				font.GlyphsNumber = 0;
				for (j = 0; j < 256; j++)
					font.Lut[j] = -1;
			}

			_params_.Width = width;
			_params_.Height = height;
			_itw = 1.0f / _params_.Width;
			_ith = 1.0f / _params_.Height;
			AddWhiteRect(2, 2);
			return 1;
		}

		public static FontGlyph* AllocGlyph(Font font)
		{
			if (font.GlyphsNumber + 1 > font.GlyphsCount)
			{
				font.GlyphsCount = font.GlyphsCount == 0 ? 8 : font.GlyphsCount * 2;
				font.Glyphs =
					(FontGlyph*)CRuntime.realloc(font.Glyphs, (ulong)(sizeof(FontGlyph) * font.GlyphsCount));
				if (font.Glyphs == null)
					return null;
			}

			font.GlyphsNumber++;
			return &font.Glyphs[font.GlyphsNumber - 1];
		}

		public static void BlurCols(byte* dst, int w, int h, int dstStride, int alpha)
		{
			var x = 0;
			var y = 0;
			for (y = 0; y < h; y++)
			{
				var z = 0;
				for (x = 1; x < w; x++)
				{
					z += (alpha * ((dst[x] << 7) - z)) >> 16;
					dst[x] = (byte)(z >> 7);
				}

				dst[w - 1] = 0;
				z = 0;
				for (x = w - 2; x >= 0; x--)
				{
					z += (alpha * ((dst[x] << 7) - z)) >> 16;
					dst[x] = (byte)(z >> 7);
				}

				dst[0] = 0;
				dst += dstStride;
			}
		}

		public static void BlurRows(byte* dst, int w, int h, int dstStride, int alpha)
		{
			var x = 0;
			var y = 0;
			for (x = 0; x < w; x++)
			{
				var z = 0;
				for (y = dstStride; y < h * dstStride; y += dstStride)
				{
					z += (alpha * ((dst[y] << 7) - z)) >> 16;
					dst[y] = (byte)(z >> 7);
				}

				dst[(h - 1) * dstStride] = 0;
				z = 0;
				for (y = (h - 2) * dstStride; y >= 0; y -= dstStride)
				{
					z += (alpha * ((dst[y] << 7) - z)) >> 16;
					dst[y] = (byte)(z >> 7);
				}

				dst[0] = 0;
				dst++;
			}
		}

		public static int HashInt(int a)
		{
			a += ~(a << 15);
			a ^= a >> 10;
			a += a << 3;
			a ^= a >> 6;
			a += ~(a << 11);
			a ^= a >> 16;
			return a;
		}

		public static int Mini(int a, int b)
		{
			return a < b ? a : b;
		}

		public static int Maxi(int a, int b)
		{
			return a > b ? a : b;
		}
	}
}