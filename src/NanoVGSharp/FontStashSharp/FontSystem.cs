using StbSharp;
using System;

namespace FontStashSharp
{
	internal unsafe class FontSystem : IDisposable
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

		private int flags;
		private FontSystemParams _params_ = new FontSystemParams();
		private float _itw;
		private float _ith;
		private byte[] _texData;
		private int[] _dirtyRect = new int[4];
		private Font[] _fonts;
		private FontAtlas atlas;
		private int _fontsNumber;
		private float[] _verts = new float[1024 * 2];
		private float[] _textureCoords = new float[1024 * 2];
		private uint[] _colors = new uint[1024];
		private int _vertsNumber;
		private byte* _scratch;
		private int _scratchCount;
		private FontSystemState[] _states = new FontSystemState[20];
		private int _statesCount;

		public FontSystem(FontSystemParams p)
		{
			_params_ = p;
			_scratch = (byte*)(CRuntime.malloc((ulong)(96000)));

			atlas = new FontAtlas((int)(_params_.Width), (int)(_params_.Height), (int)(256));
			_fonts = new Font[4];
			_fontsNumber = (int)(0);
			_itw = (float)(1.0f / _params_.Width);
			_ith = (float)(1.0f / _params_.Height);
			_texData = new byte[_params_.Width * _params_.Height];
			Array.Clear(_texData, 0, _texData.Length);
			_dirtyRect[0] = (int)(_params_.Width);
			_dirtyRect[1] = (int)(_params_.Height);
			_dirtyRect[2] = (int)(0);
			_dirtyRect[3] = (int)(0);
			AddWhiteRect((int)(2), (int)(2));
			PushState();
			ClearState();
		}

		public void Dispose()
		{
			int i = 0;
			for (i = (int)(0); (i) < (_fontsNumber); ++i)
			{
				FreeFont(_fonts[i]);
			}

			if ((_scratch) != null)
				CRuntime.free(_scratch);
		}


		public void AddWhiteRect(int w, int h)
		{
			int x = 0;
			int y = 0;
			int gx = 0;
			int gy = 0;
			if ((atlas.AddRect((int)(w), (int)(h), &gx, &gy)) == (0))
				return;
			fixed (byte *dst2 = &_texData[gx + gy * _params_.Width])
			{
				var dst = dst2;
				for (y = (int)(0); (y) < (h); y++)
				{
					for (x = (int)(0); (x) < (w); x++)
					{
						dst[x] = (byte)(0xff);
					}
					dst += _params_.Width;
				}
			}
			_dirtyRect[0] = (int)(Mini((int)(_dirtyRect[0]), (int)(gx)));
			_dirtyRect[1] = (int)(Mini((int)(_dirtyRect[1]), (int)(gy)));
			_dirtyRect[2] = (int)(Maxi((int)(_dirtyRect[2]), (int)(gx + w)));
			_dirtyRect[3] = (int)(Maxi((int)(_dirtyRect[3]), (int)(gy + h)));
		}

		public FontSystemState GetState()
		{
			return _states[_statesCount - 1];
		}

		public int AddFallbackFont(int _base_, int fallback)
		{
			Font baseFont = _fonts[_base_];
			if ((baseFont.FallbacksCount) < (20))
			{
				baseFont.Fallbacks[baseFont.FallbacksCount++] = (int)(fallback);
				return (int)(1);
			}

			return (int)(0);
		}

		public void SetSize(float size)
		{
			GetState().Size = (float)(size);
		}

		public void SetColor(uint color)
		{
			GetState().Color = (uint)(color);
		}

		public void SetSpacing(float spacing)
		{
			GetState().Spacing = (float)(spacing);
		}

		public void SetBlur(float blur)
		{
			GetState().Blur = (float)(blur);
		}

		public void SetAlign(int align)
		{
			GetState().Align = (int)(align);
		}

		public void SetFont(int font)
		{
			GetState().Font = (int)(font);
		}

		public void PushState()
		{
			if ((_statesCount) >= _states.Length)
			{
				throw new Exception("FONS_STATES_OVERFLOW");
			}

			if ((_statesCount) > (0))
			{
				_states[_statesCount] = _states[_statesCount - 1].Clone();
			}
			else
			{
				_states[_statesCount] = new FontSystemState();
			}
			_statesCount++;
		}

		public void PopState()
		{
			if (_statesCount <= 1)
			{
				throw new Exception("FONS_STATES_OVERFLOW");
			}

			_statesCount--;
		}

		public void ClearState()
		{
			FontSystemState state = GetState();
			state.Size = (float)(12.0f);
			state.Color = (uint)(0xffffffff);
			state.Font = (int)(0);
			state.Blur = (float)(0);
			state.Spacing = (float)(0);
			state.Align = (int)(FONS_ALIGN_LEFT | FONS_ALIGN_BASELINE);
		}

		public int LoadFont(StbTrueType.stbtt_fontinfo font, byte* data, int dataSize)
		{
			int stbError = 0;
			font.userdata = this;
			stbError = (int)(StbTrueType.stbtt_InitFont(font, data, (int)(0)));
			return (int)(stbError);
		}

		public void FreeFont(Font font)
		{
			if ((font) == null)
				return;
			if ((font.Glyphs) != null)
				CRuntime.free(font.Glyphs);
		}

		public int AllocFont()
		{
			Font font = null;
			if ((_fontsNumber + 1) > (_fonts.Length))
			{
				_fonts = new Font[_fonts.Length];
				if ((_fonts) == null)
					return (int)(-1);
			}

			font = new Font();
			if ((font) == null)
				goto error;
			font.Glyphs = (FontGlyph*)(CRuntime.malloc((ulong)(sizeof(FontGlyph) * 256)));
			if ((font.Glyphs) == null)
				goto error;
			font.GlyphsCount = (int)(256);
			font.GlyphsNumber = (int)(0);
			_fonts[_fontsNumber++] = font;
			return (int)(_fontsNumber - 1);
		error:
			;
			FreeFont(font);
			return (int)(-1);
		}

		public int AddFontMem(string name, byte[] data)
		{
			int i = 0;
			int ascent = 0;
			int descent = 0;
			int fh = 0;
			int lineGap = 0;
			Font font;
			int idx = (int)(AllocFont());
			if ((idx) == (-1))
				return (int)(-1);
			font = _fonts[idx];
			font.Name = name;
			for (i = (int)(0); (i) < (256); ++i)
			{
				font.Lut[i] = (int)(-1);
			}
			font.Data = data;
			_scratchCount = (int)(0);
			fixed (byte* ptr = data)
			{
				if (LoadFont(font.FontInfo, ptr, data.Length) == 0)
					goto error;
			}
			font.FontInfo.__tt_getFontVMetrics(&ascent, &descent, &lineGap);
			fh = (int)(ascent - descent);
			font.Ascender = (float)((float)(ascent) / (float)(fh));
			font.Descender = (float)((float)(descent) / (float)(fh));
			font.LineHeight = (float)((float)(fh + lineGap) / (float)(fh));
			return (int)(idx);
		error:
			;
			FreeFont(font);
			_fontsNumber--;
			return (int)(-1);
		}

		public int GetFontByName(string name)
		{
			int i = 0;
			for (i = (int)(0); (i) < (_fontsNumber); i++)
			{
				if (_fonts[i].Name == name)
					return (int)(i);
			}
			return (int)(-1);
		}

		public void Blur(byte* dst, int w, int h, int dstStride, int blur)
		{
			int alpha = 0;
			float sigma = 0;
			if ((blur) < (1))
				return;
			sigma = (float)((float)(blur) * 0.57735f);
			alpha = ((int)((1 << 16) * (1.0f - Math.Exp((float)(-2.3f / (sigma + 1.0f))))));
			BlurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			BlurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			BlurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			BlurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
		}

		public FontGlyph* GetGlyph(Font font, uint codepoint, short isize, short iblur, int bitmapOption)
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
			FontGlyph* glyph = null;
			uint h = 0;
			float size = (float)(isize / 10.0f);
			int pad = 0;
			int added = 0;
			Font renderFont = font;
			if ((isize) < (2))
				return null;
			if ((iblur) > (20))
				iblur = (short)(20);
			pad = (int)(iblur + 2);
			_scratchCount = (int)(0);
			h = (uint)(HashInt((uint)(codepoint)) & (256 - 1));
			i = (int)(font.Lut[h]);
			while (i != -1)
			{
				if ((((font.Glyphs[i].Codepoint) == (codepoint)) && ((font.Glyphs[i].Size) == (isize))) && ((font.Glyphs[i].Blur) == (iblur)))
				{
					glyph = &font.Glyphs[i];
					if (((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL)) || (((glyph->X0) >= (0)) && ((glyph->Y0) >= (0))))
					{
						return glyph;
					}
					break;
				}
				i = (int)(font.Glyphs[i].Next);
			}
			g = (int)(font.FontInfo.__tt_getGlyphIndex((int)(codepoint)));
			if ((g) == (0))
			{
				for (i = (int)(0); (i) < (font.FallbacksCount); ++i)
				{
					Font fallbackFont = _fonts[font.Fallbacks[i]];
					int fallbackIndex = (int)(fallbackFont.FontInfo.__tt_getGlyphIndex((int)(codepoint)));
					if (fallbackIndex != 0)
					{
						g = (int)(fallbackIndex);
						renderFont = fallbackFont;
						break;
					}
				}
			}

			scale = (float)(renderFont.FontInfo.__tt_getPixelHeightScale((float)(size)));
			renderFont.FontInfo.__tt_buildGlyphBitmap((int)(g), (float)(size), (float)(scale), &advance, &lsb, &x0, &y0, &x1, &y1);
			gw = (int)(x1 - x0 + pad * 2);
			gh = (int)(y1 - y0 + pad * 2);
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_REQUIRED))
			{
				added = (int)(atlas.AddRect((int)(gw), (int)(gh), &gx, &gy));
				if (((added) == (0)))
				{
					throw new Exception("FONS_ATLAS_FULL");
				}
			}
			else
			{
				gx = (int)(-1);
				gy = (int)(-1);
			}

			if ((glyph) == null)
			{
				glyph = AllocGlyph(font);
				glyph->Codepoint = (uint)(codepoint);
				glyph->Size = (short)(isize);
				glyph->Blur = (short)(iblur);
				glyph->Next = (int)(0);
				glyph->Next = (int)(font.Lut[h]);
				font.Lut[h] = (int)(font.GlyphsNumber - 1);
			}

			glyph->Index = (int)(g);
			glyph->X0 = ((short)(gx));
			glyph->Y0 = ((short)(gy));
			glyph->X1 = ((short)(glyph->X0 + gw));
			glyph->Y1 = ((short)(glyph->Y0 + gh));
			glyph->XAdvance = ((short)(scale * advance * 10.0f));
			glyph->XOffset = ((short)(x0 - pad));
			glyph->YOffset = ((short)(y0 - pad));
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL))
			{
				return glyph;
			}

			fixed (byte *dst = &_texData[(glyph->X0 + pad) + (glyph->Y0 + pad) * _params_.Width])
			{
				renderFont.FontInfo.__tt_renderGlyphBitmap(dst, (int)(gw - pad * 2), (int)(gh - pad * 2), (int)(_params_.Width), (float)(scale), (float)(scale), (int)(g));
			}
			fixed (byte* dst = &_texData[glyph->X0 + glyph->Y0 * _params_.Width])
			{

				for (y = (int)(0); (y) < (gh); y++)
				{
					dst[y * _params_.Width] = (byte)(0);
					dst[gw - 1 + y * _params_.Width] = (byte)(0);
				}
				for (x = (int)(0); (x) < (gw); x++)
				{
					dst[x] = (byte)(0);
					dst[x + (gh - 1) * _params_.Width] = (byte)(0);
				}
			}


			if ((iblur) > (0))
			{
				_scratchCount = (int)(0);
				fixed (byte* bdst = &_texData[glyph->X0 + glyph->Y0 * _params_.Width])
				{
					Blur(bdst, (int)(gw), (int)(gh), (int)(_params_.Width), (int)(iblur));
				}
			}

			_dirtyRect[0] = (int)(Mini((int)(_dirtyRect[0]), (int)(glyph->X0)));
			_dirtyRect[1] = (int)(Mini((int)(_dirtyRect[1]), (int)(glyph->Y0)));
			_dirtyRect[2] = (int)(Maxi((int)(_dirtyRect[2]), (int)(glyph->X1)));
			_dirtyRect[3] = (int)(Maxi((int)(_dirtyRect[3]), (int)(glyph->Y1)));
			return glyph;
		}

		public void GetQuad(Font font, int prevGlyphIndex, FontGlyph* glyph, float scale, float spacing, ref float x, ref float y, FontGlyphSquad* q)
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
				float adv = (float)(font.FontInfo.__tt_getGlyphKernAdvance((int)(prevGlyphIndex), (int)(glyph->Index)) * scale);
				x += (float)((int)(adv + spacing + 0.5f));
			}

			xoff = (float)((short)(glyph->XOffset + 1));
			yoff = (float)((short)(glyph->YOffset + 1));
			x0 = ((float)(glyph->X0 + 1));
			y0 = ((float)(glyph->Y0 + 1));
			x1 = ((float)(glyph->X1 - 1));
			y1 = ((float)(glyph->Y1 - 1));
			if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
			{
				rx = ((float)((int)(x + xoff)));
				ry = ((float)((int)(y + yoff)));
				q->X0 = (float)(rx);
				q->Y0 = (float)(ry);
				q->X1 = (float)(rx + x1 - x0);
				q->Y1 = (float)(ry + y1 - y0);
				q->S0 = (float)(x0 * _itw);
				q->T0 = (float)(y0 * _ith);
				q->S1 = (float)(x1 * _itw);
				q->T1 = (float)(y1 * _ith);
			}
			else
			{
				rx = ((float)((int)(x + xoff)));
				ry = ((float)((int)(y - yoff)));
				q->X0 = (float)(rx);
				q->Y0 = (float)(ry);
				q->X1 = (float)(rx + x1 - x0);
				q->Y1 = (float)(ry - y1 + y0);
				q->S0 = (float)(x0 * _itw);
				q->T0 = (float)(y0 * _ith);
				q->S1 = (float)(x1 * _itw);
				q->T1 = (float)(y1 * _ith);
			}

			x += (float)((int)(glyph->XAdvance / 10.0f + 0.5f));
		}

		public void Flush()
		{
			if (((_dirtyRect[0]) < (_dirtyRect[2])) && ((_dirtyRect[1]) < (_dirtyRect[3])))
			{
				_dirtyRect[0] = (int)(_params_.Width);
				_dirtyRect[1] = (int)(_params_.Height);
				_dirtyRect[2] = (int)(0);
				_dirtyRect[3] = (int)(0);
			}

			if ((_vertsNumber) > (0))
			{
				_vertsNumber = (int)(0);
			}

		}

		public void AddVertex(float x, float y, float s, float t, uint c)
		{
			_verts[_vertsNumber * 2 + 0] = (float)(x);
			_verts[_vertsNumber * 2 + 1] = (float)(y);
			_textureCoords[_vertsNumber * 2 + 0] = (float)(s);
			_textureCoords[_vertsNumber * 2 + 1] = (float)(t);
			_colors[_vertsNumber] = (uint)(c);
			_vertsNumber++;
		}

		public float GetVertAlign(Font font, int align, short isize)
		{
			if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
			{
				if ((align & FONS_ALIGN_TOP) != 0)
				{
					return (float)(font.Ascender * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_MIDDLE) != 0)
				{
					return (float)((font.Ascender + font.Descender) / 2.0f * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_BASELINE) != 0)
				{
					return (float)(0.0f);
				}
				else if ((align & FONS_ALIGN_BOTTOM) != 0)
				{
					return (float)(font.Descender * (float)(isize) / 10.0f);
				}
			}
			else
			{
				if ((align & FONS_ALIGN_TOP) != 0)
				{
					return (float)(-font.Ascender * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_MIDDLE) != 0)
				{
					return (float)(-(font.Ascender + font.Descender) / 2.0f * (float)(isize) / 10.0f);
				}
				else if ((align & FONS_ALIGN_BASELINE) != 0)
				{
					return (float)(0.0f);
				}
				else if ((align & FONS_ALIGN_BOTTOM) != 0)
				{
					return (float)(-font.Descender * (float)(isize) / 10.0f);
				}
			}

			return (float)(0.0);
		}

		public float DrawText(float x, float y, StringSegment str)
		{
			if (str.IsNullOrEmpty)
			{
				return 0.0f;
			}

			FontSystemState state = GetState();
			FontGlyph* glyph = null;
			FontGlyphSquad q = new FontGlyphSquad();
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state.Size * 10.0f);
			short iblur = (short)(state.Blur);
			float scale = 0;
			Font font;
			float width = 0;
			if (((state.Font) < (0)) || ((state.Font) >= (_fontsNumber)))
				return (float)(x);
			font = _fonts[state.Font];
			if ((font.Data) == null)
				return (float)(x);
			scale = (float)(font.FontInfo.__tt_getPixelHeightScale((float)((float)(isize) / 10.0f)));

			if ((state.Align & FONS_ALIGN_LEFT) != 0)
			{
			}
			else if ((state.Align & FONS_ALIGN_RIGHT) != 0)
			{
				var bounds = new Bounds();
				width = (float)(TextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width);
			}
			else if ((state.Align & FONS_ALIGN_CENTER) != 0)
			{
				var bounds = new Bounds();
				width = (float)(TextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width * 0.5f);
			}

			y += (float)(GetVertAlign(font, (int)(state.Align), (short)(isize)));
			for (var i = 0; i < str.Length; ++i)
			{
				var codepoint = str[i];
				glyph = GetGlyph(font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_REQUIRED));
				if (glyph != null)
				{
					GetQuad(font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state.Spacing), ref x, ref y, &q);
					if ((_vertsNumber + 6) > (1024))
						Flush();
					AddVertex((float)(q.X0), (float)(q.Y0), (float)(q.S0), (float)(q.T0), (uint)(state.Color));
					AddVertex((float)(q.X1), (float)(q.Y1), (float)(q.S1), (float)(q.T1), (uint)(state.Color));
					AddVertex((float)(q.X1), (float)(q.Y0), (float)(q.S1), (float)(q.T0), (uint)(state.Color));
					AddVertex((float)(q.X0), (float)(q.Y0), (float)(q.S0), (float)(q.T0), (uint)(state.Color));
					AddVertex((float)(q.X0), (float)(q.Y1), (float)(q.S0), (float)(q.T1), (uint)(state.Color));
					AddVertex((float)(q.X1), (float)(q.Y1), (float)(q.S1), (float)(q.T1), (uint)(state.Color));
				}
				prevGlyphIndex = (int)(glyph != null ? glyph->Index : -1);
			}
			Flush();
			return (float)(x);
		}

		public int TextIterInit(FontTextIterator iter, float x, float y, StringSegment str, int bitmapOption)
		{
			FontSystemState state = GetState();
			float width = 0;

			if (((state.Font) < (0)) || ((state.Font) >= (_fontsNumber)))
				return (int)(0);
			iter.Font = _fonts[state.Font];
			if ((iter.Font.Data) == null)
				return (int)(0);
			iter.iSize = ((short)(state.Size * 10.0f));
			iter.iBlur = ((short)(state.Blur));
			iter.Scale = (float)(iter.Font.FontInfo.__tt_getPixelHeightScale((float)((float)(iter.iSize) / 10.0f)));
			if ((state.Align & FONS_ALIGN_LEFT) != 0)
			{
			}
			else if ((state.Align & FONS_ALIGN_RIGHT) != 0)
			{
				var bounds = new Bounds();
				width = (float)(TextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width);
			}
			else if ((state.Align & FONS_ALIGN_CENTER) != 0)
			{
				var bounds = new Bounds();
				width = (float)(TextBounds((float)(x), (float)(y), str, ref bounds));
				x -= (float)(width * 0.5f);
			}

			y += (float)(GetVertAlign(iter.Font, (int)(state.Align), (short)(iter.iSize)));
			iter.X = (float)(iter.NextX = (float)(x));
			iter.Y = (float)(iter.NextY = (float)(y));
			iter.Spacing = (float)(state.Spacing);
			iter.Str = str;
			iter.Next = str;
			iter.Codepoint = (uint)(0);
			iter.PrevGlyphIndex = (int)(-1);
			iter.BitmapOption = (int)(bitmapOption);
			return (int)(1);
		}

		public bool TextIterNext(FontTextIterator iter, FontGlyphSquad* quad)
		{
			iter.Str = iter.Next;

			if (iter.Str.IsNullOrEmpty)
			{
				return false;
			}

			iter.Codepoint = iter.Str[0];
			iter.X = (float)(iter.NextX);
			iter.Y = (float)(iter.NextY);
			var glyph = GetGlyph(iter.Font, (uint)(iter.Codepoint), (short)(iter.iSize), (short)(iter.iBlur), (int)(iter.BitmapOption));
			if (glyph != null)
				GetQuad(iter.Font, (int)(iter.PrevGlyphIndex), glyph, (float)(iter.Scale), (float)(iter.Spacing), ref iter.NextX, ref iter.NextY, quad);
			iter.PrevGlyphIndex = (int)(glyph != null ? glyph->Index : -1);

			++iter.Next.Location;

			return true;
		}

		public void DrawDebug(float x, float y)
		{
			int i = 0;
			int w = (int)(_params_.Width);
			int h = (int)(_params_.Height);
			float u = (float)((w) == (0) ? 0 : (1.0f / w));
			float v = (float)((h) == (0) ? 0 : (1.0f / h));
			if ((_vertsNumber + 6 + 6) > (1024))
				Flush();
			AddVertex((float)(x + 0), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			AddVertex((float)(x + w), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			AddVertex((float)(x + w), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			AddVertex((float)(x + 0), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			AddVertex((float)(x + 0), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			AddVertex((float)(x + w), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			AddVertex((float)(x + 0), (float)(y + 0), (float)(0), (float)(0), (uint)(0xffffffff));
			AddVertex((float)(x + w), (float)(y + h), (float)(1), (float)(1), (uint)(0xffffffff));
			AddVertex((float)(x + w), (float)(y + 0), (float)(1), (float)(0), (uint)(0xffffffff));
			AddVertex((float)(x + 0), (float)(y + 0), (float)(0), (float)(0), (uint)(0xffffffff));
			AddVertex((float)(x + 0), (float)(y + h), (float)(0), (float)(1), (uint)(0xffffffff));
			AddVertex((float)(x + w), (float)(y + h), (float)(1), (float)(1), (uint)(0xffffffff));
			for (i = (int)(0); (i) < (atlas.NodesNumber); i++)
			{
				FontAtlasNode* n = &atlas.Nodes[i];
				if ((_vertsNumber + 6) > (1024))
					Flush();
				AddVertex((float)(x + n->X + 0), (float)(y + n->Y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));
				AddVertex((float)(x + n->X + n->Width), (float)(y + n->Y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));
				AddVertex((float)(x + n->X + n->Width), (float)(y + n->Y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));
				AddVertex((float)(x + n->X + 0), (float)(y + n->Y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));
				AddVertex((float)(x + n->X + 0), (float)(y + n->Y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));
				AddVertex((float)(x + n->X + n->Width), (float)(y + n->Y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));
			}
			Flush();
		}

		public float TextBounds(float x, float y, StringSegment str, ref Bounds bounds)
		{
			FontSystemState state = GetState();
			FontGlyphSquad q = new FontGlyphSquad();
			FontGlyph* glyph = null;
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state.Size * 10.0f);
			short iblur = (short)(state.Blur);
			float scale = 0;
			Font font;
			float startx = 0;
			float advance = 0;
			float minx = 0;
			float miny = 0;
			float maxx = 0;
			float maxy = 0;
			if (((state.Font) < (0)) || ((state.Font) >= (_fontsNumber)))
				return (float)(0);
			font = _fonts[state.Font];
			if ((font.Data) == null)
				return (float)(0);
			scale = (float)(font.FontInfo.__tt_getPixelHeightScale((float)((float)(isize) / 10.0f)));
			y += (float)(GetVertAlign(font, (int)(state.Align), (short)(isize)));
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			startx = (float)(x);
			for (var i = 0; i < str.Length; ++i)
			{
				var codepoint = str[i];
				glyph = GetGlyph(font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_OPTIONAL));
				if (glyph != null)
				{
					GetQuad(font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state.Spacing), ref x, ref y, &q);
					if ((q.X0) < (minx))
						minx = (float)(q.X0);
					if ((q.X1) > (maxx))
						maxx = (float)(q.X1);
					if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
					{
						if ((q.Y0) < (miny))
							miny = (float)(q.Y0);
						if ((q.Y1) > (maxy))
							maxy = (float)(q.Y1);
					}
					else
					{
						if ((q.Y1) < (miny))
							miny = (float)(q.Y1);
						if ((q.Y0) > (maxy))
							maxy = (float)(q.Y0);
					}
				}
				prevGlyphIndex = (int)(glyph != null ? glyph->Index : -1);
			}
			advance = (float)(x - startx);
			if ((state.Align & FONS_ALIGN_LEFT) != 0)
			{
			}
			else if ((state.Align & FONS_ALIGN_RIGHT) != 0)
			{
				minx -= (float)(advance);
				maxx -= (float)(advance);
			}
			else if ((state.Align & FONS_ALIGN_CENTER) != 0)
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

		public void VertMetrics(out float ascender, out float descender, out float lineh)
		{
			ascender = descender = lineh = 0;
			Font font;
			FontSystemState state = GetState();
			short isize = 0;
			if (((state.Font) < (0)) || ((state.Font) >= (_fontsNumber)))
				return;
			font = _fonts[state.Font];
			isize = ((short)(state.Size * 10.0f));
			if ((font.Data) == null)
				return;

			ascender = (float)(font.Ascender * isize / 10.0f);
			descender = (float)(font.Descender * isize / 10.0f);
			lineh = (float)(font.LineHeight * isize / 10.0f);
		}

		public void LineBounds(float y, ref float miny, ref float maxy)
		{
			Font font;
			FontSystemState state = GetState();
			short isize = 0;
			if (((state.Font) < (0)) || ((state.Font) >= (_fontsNumber)))
				return;
			font = _fonts[state.Font];
			isize = ((short)(state.Size * 10.0f));
			if ((font.Data) == null)
				return;
			y += (float)(GetVertAlign(font, (int)(state.Align), (short)(isize)));
			if ((_params_.Flags & FONS_ZERO_TOPLEFT) != 0)
			{
				miny = (float)(y - font.Ascender * (float)(isize) / 10.0f);
				maxy = (float)(miny + font.LineHeight * isize / 10.0f);
			}
			else
			{
				maxy = (float)(y + font.Descender * (float)(isize) / 10.0f);
				miny = (float)(maxy - font.LineHeight * isize / 10.0f);
			}

		}

		public byte[] GetTextureData(int* width, int* height)
		{
			if (width != null)
				*width = (int)(_params_.Width);
			if (height != null)
				*height = (int)(_params_.Height);
			return _texData;
		}

		public int ValidateTexture(int* dirty)
		{
			if (((_dirtyRect[0]) < (_dirtyRect[2])) && ((_dirtyRect[1]) < (_dirtyRect[3])))
			{
				dirty[0] = (int)(_dirtyRect[0]);
				dirty[1] = (int)(_dirtyRect[1]);
				dirty[2] = (int)(_dirtyRect[2]);
				dirty[3] = (int)(_dirtyRect[3]);
				_dirtyRect[0] = (int)(_params_.Width);
				_dirtyRect[1] = (int)(_params_.Height);
				_dirtyRect[2] = (int)(0);
				_dirtyRect[3] = (int)(0);
				return (int)(1);
			}

			return (int)(0);
		}

		public void GetAtlasSize(int* width, int* height)
		{
			*width = (int)(_params_.Width);
			*height = (int)(_params_.Height);
		}

		public int ExpandAtlas(int width, int height)
		{
			int i = 0;
			int maxy = (int)(0);
			width = (int)(Maxi((int)(width), (int)(_params_.Width)));
			height = (int)(Maxi((int)(height), (int)(_params_.Height)));
			if (((width) == (_params_.Width)) && ((height) == (_params_.Height)))
				return (int)(1);
			Flush();

			var data = new byte[width * height];
			for (i = (int)(0); (i) < (_params_.Height); i++)
			{
				fixed (byte* dst = &data[i * width])
				{
					fixed (byte* src = &_texData[i * _params_.Width])
					{
						CRuntime.memcpy(dst, src, (ulong)(_params_.Width));
						if ((width) > (_params_.Width))
							CRuntime.memset(dst + _params_.Width, (int)(0), (ulong)(width - _params_.Width));
					}
				}
			}
			if ((height) > (_params_.Height))
			{
				Array.Clear(data, _params_.Height * width, (height - _params_.Height) * width);
			}

			_texData = data;
			atlas.Expand((int)(width), (int)(height));
			for (i = (int)(0); (i) < (atlas.NodesNumber); i++)
			{
				maxy = (int)(Maxi((int)(maxy), (int)(atlas.Nodes[i].Y)));
			}
			_dirtyRect[0] = (int)(0);
			_dirtyRect[1] = (int)(0);
			_dirtyRect[2] = (int)(_params_.Width);
			_dirtyRect[3] = (int)(maxy);
			_params_.Width = (int)(width);
			_params_.Height = (int)(height);
			_itw = (float)(1.0f / _params_.Width);
			_ith = (float)(1.0f / _params_.Height);
			return (int)(1);
		}

		public int ResetAtlas(int width, int height)
		{
			int i = 0;
			int j = 0;
			Flush();

			atlas.Reset((int)(width), (int)(height));
			_texData = new byte[width * height];
			Array.Clear(_texData, 0, _texData.Length);
			_dirtyRect[0] = (int)(width);
			_dirtyRect[1] = (int)(height);
			_dirtyRect[2] = (int)(0);
			_dirtyRect[3] = (int)(0);
			for (i = (int)(0); (i) < (_fontsNumber); i++)
			{
				Font font = _fonts[i];
				font.GlyphsNumber = (int)(0);
				for (j = (int)(0); (j) < (256); j++)
				{
					font.Lut[j] = (int)(-1);
				}
			}
			_params_.Width = (int)(width);
			_params_.Height = (int)(height);
			_itw = (float)(1.0f / _params_.Width);
			_ith = (float)(1.0f / _params_.Height);
			AddWhiteRect((int)(2), (int)(2));
			return (int)(1);
		}

		public static FontGlyph* AllocGlyph(Font font)
		{
			if ((font.GlyphsNumber + 1) > (font.GlyphsCount))
			{
				font.GlyphsCount = (int)((font.GlyphsCount) == (0) ? 8 : font.GlyphsCount * 2);
				font.Glyphs = (FontGlyph*)(CRuntime.realloc(font.Glyphs, (ulong)(sizeof(FontGlyph) * font.GlyphsCount)));
				if ((font.Glyphs) == null)
					return null;
			}

			font.GlyphsNumber++;
			return &font.Glyphs[font.GlyphsNumber - 1];
		}

		public static void BlurCols(byte* dst, int w, int h, int dstStride, int alpha)
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

		public static void BlurRows(byte* dst, int w, int h, int dstStride, int alpha)
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

		public static uint HashInt(uint a)
		{
			a += (uint)(~(a << 15));
			a ^= (uint)(a >> 10);
			a += (uint)(a << 3);
			a ^= (uint)(a >> 6);
			a += (uint)(~(a << 11));
			a ^= (uint)(a >> 16);
			return (uint)(a);
		}

		public static int Mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int Maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}
	}
}