using System;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StbSharp;

namespace NvgSharp
{
	public unsafe class NvgContext : IDisposable
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
		private readonly int[] _fontImages = new int[4];

		private readonly IRenderer _renderer;
		private readonly TextRow[] _rows = new TextRow[MaxTextRows];
		private readonly PathCache _cache;
		private float* _commands;
		private int _commandsCount;
		private int _commandsNumber;
		private float _commandX;
		private float _commandY;
		private float _devicePxRatio;
		private float _distTol;
		private int _drawCallCount;
		private readonly int _edgeAntiAlias;
		private int _fillTriCount;
		private int _fontImageIdx;
		private FontSystem _fontSyst;
		private float _fringeWidth;
		private readonly NvgContextState[] _states = new NvgContextState[32];
		private int _statesNumber;
		private int _strokeTriCount;
		private float _tessTol;
		private int _textTriCount;

		public NvgContext(GraphicsDevice device, int edgeAntiAlias)
		{
			_renderer = new XNARenderer(device);

			var fontParams = new FontSystemParams();

			_edgeAntiAlias = edgeAntiAlias;
			for (var i = 0; i < 4; i++)
				_fontImages[i] = 0;
			_commands = (float*)CRuntime.malloc((ulong)(sizeof(float) * 256));
			_commandsNumber = 0;
			_commandsCount = 256;
			_cache = new PathCache();
			Save();
			Reset();
			SetDevicePixelRatio(1.0f);
			fontParams.Width = 512;
			fontParams.Height = 512;
			fontParams.Flags = FontSystem.FONS_ZERO_TOPLEFT;
			_fontSyst = new FontSystem(fontParams);
			_fontImages[0] = _renderer.CreateTexture(NVG_TEXTURE_ALPHA, fontParams.Width, fontParams.Height, 0, null);
			_fontImageIdx = 0;

			for (var i = 0; i < _rows.Length; ++i)
				_rows[i] = new TextRow();
		}

		public void Dispose()
		{
			var i = 0;
			if (_commands != null)
			{
				CRuntime.free(_commands);
				_commands = null;
			}

			if (_fontSyst != null)
			{
				_fontSyst.Dispose();
				_fontSyst = null;
			}

			for (i = 0; i < 4; i++)
				if (_fontImages[i] != 0)
				{
					DeleteImage(_fontImages[i]);
					_fontImages[i] = 0;
				}
		}

		public void BeginFrame(float windowWidth, float windowHeight, float devicePixelRatio)
		{
			_statesNumber = 0;
			Save();
			Reset();
			SetDevicePixelRatio(devicePixelRatio);

			_renderer.Begin();

			_renderer.Viewport(windowWidth, windowHeight, devicePixelRatio);
			_drawCallCount = 0;
			_fillTriCount = 0;
			_strokeTriCount = 0;
			_textTriCount = 0;
		}

		public void EndFrame()
		{
			if (_fontImageIdx != 0)
			{
				var fontImage = _fontImages[_fontImageIdx];
				var i = 0;
				var j = 0;
				var iw = 0;
				var ih = 0;
				if (fontImage == 0)
					return;
				ImageSize(fontImage, out iw, out ih);
				for (i = j = 0; i < _fontImageIdx; i++)
					if (_fontImages[i] != 0)
					{
						var nw = 0;
						var nh = 0;
						ImageSize(_fontImages[i], out nw, out nh);
						if (nw < iw || nh < ih)
							DeleteImage(_fontImages[i]);
						else
							_fontImages[j++] = _fontImages[i];
					}

				_fontImages[j++] = _fontImages[0];
				_fontImages[0] = fontImage;
				_fontImageIdx = 0;
				for (i = j; i < 4; i++)
					_fontImages[i] = 0;
			}

			_renderer.End();
		}

		public void Save()
		{
			if (_statesNumber >= 32)
				return;
			if (_statesNumber > 0)
				_states[_statesNumber] = _states[_statesNumber - 1].Clone();
			else
				_states[_statesNumber] = new NvgContextState();
			_statesNumber++;
		}

		public void Restore()
		{
			if (_statesNumber <= 1)
				return;
			_statesNumber--;
		}

		public void Reset()
		{
			var state = GetState();
			state.Fill = new Paint(Color.White);
			state.Stroke = new Paint(Color.Black);
			state.ShapeAntiAlias = 1;
			state.StrokeWidth = 1.0f;
			state.MiterLimit = 10.0f;
			state.LineCap = NVG_BUTT;
			state.LineJoin = NVG_MITER;
			state.Alpha = 1.0f;
			state.Transform.SetIdentity();
			state.Scissor.Extent.X = -1.0f;
			state.Scissor.Extent.Y = -1.0f;
			state.FontSize = 16.0f;
			state.LetterSpacing = 0.0f;
			state.LineHeight = 1.0f;
			state.FontBlur = 0.0f;
			state.TextAlign = NVG_ALIGN_LEFT | NVG_ALIGN_BASELINE;
			state.FontId = 0;
		}

		public void ShapeAntiAlias(int enabled)
		{
			var state = GetState();
			state.ShapeAntiAlias = enabled;
		}

		public void StrokeWidth(float width)
		{
			var state = GetState();
			state.StrokeWidth = width;
		}

		public void MiterLimit(float limit)
		{
			var state = GetState();
			state.MiterLimit = limit;
		}

		public void LineCap(int cap)
		{
			var state = GetState();
			state.LineCap = cap;
		}

		public void LineJoin(int join)
		{
			var state = GetState();
			state.LineJoin = join;
		}

		public void GlobalAlpha(float alpha)
		{
			var state = GetState();
			state.Alpha = alpha;
		}

		public void Transform(float a, float b, float c, float d, float e, float f)
		{
			var state = GetState();
			Transform t;
			t.T1 = a;
			t.T2 = b;
			t.T3 = c;
			t.T4 = d;
			t.T5 = e;
			t.T6 = f;

			state.Transform.Premultiply(ref t);
		}

		public void ResetTransform()
		{
			var state = GetState();
			state.Transform.SetIdentity();
		}

		public void Translate(float x, float y)
		{
			var state = GetState();
			var t = new Transform();
			t.SetTranslate(x, y);
			state.Transform.Premultiply(ref t);
		}

		public void Rotate(float angle)
		{
			var state = GetState();
			var t = new Transform();
			t.SetRotate(angle);
			state.Transform.Premultiply(ref t);
		}

		public void SkewX(float angle)
		{
			var state = GetState();
			var t = new Transform();
			t.SetSkewX(angle);
			state.Transform.Premultiply(ref t);
		}

		public void SkewY(float angle)
		{
			var state = GetState();
			var t = new Transform();
			t.SetSkewY(angle);
			state.Transform.Premultiply(ref t);
		}

		public void Scale(float x, float y)
		{
			var state = GetState();
			var t = new Transform();
			t.SetScale(x, y);
			state.Transform.Premultiply(ref t);
		}

		public void CurrentTransform(Transform xform)
		{
			var state = GetState();

			state.Transform = xform;
		}

		public void StrokeColor(Color color)
		{
			var state = GetState();
			state.Stroke = new Paint(color);
		}

		public void StrokePaint(Paint paint)
		{
			var state = GetState();
			state.Stroke = paint;
			state.Stroke.Transform.Multiply(ref state.Transform);
		}

		public void FillColor(Color color)
		{
			var state = GetState();
			state.Fill = new Paint(color);
		}

		public void FillPaint(Paint paint)
		{
			var state = GetState();
			state.Fill = paint;
			state.Fill.Transform.Multiply(ref state.Transform);
		}

		public int CreateImageRGBA(int w, int h, int imageFlags, byte[] data)
		{
			return _renderer.CreateTexture(NVG_TEXTURE_RGBA, w, h, imageFlags, data);
		}

		public void UpdateImage(int image, byte[] data)
		{
			var w = 0;
			var h = 0;
			_renderer.GetTextureSize(image, out w, out h);
			_renderer.UpdateTexture(image, 0, 0, w, h, data);
		}

		public void ImageSize(int image, out int w, out int h)
		{
			_renderer.GetTextureSize(image, out w, out h);
		}

		public void DeleteImage(int image)
		{
			_renderer.DeleteTexture(image);
		}

		public Paint LinearGradient(float sx, float sy, float ex, float ey, Color icol, Color ocol)
		{
			var p = new Paint();
			float dx = 0;
			float dy = 0;
			float d = 0;
			var large = (float)1e5;
			dx = ex - sx;
			dy = ey - sy;
			d = (float)Math.Sqrt(dx * dx + dy * dy);
			if (d > 0.0001f)
			{
				dx /= d;
				dy /= d;
			}
			else
			{
				dx = 0;
				dy = 1;
			}

			p.Transform.T1 = dy;
			p.Transform.T2 = -dx;
			p.Transform.T3 = dx;
			p.Transform.T4 = dy;
			p.Transform.T5 = sx - dx * large;
			p.Transform.T6 = sy - dy * large;
			p.Extent.X = large;
			p.Extent.Y = large + d * 0.5f;
			p.Radius = 0.0f;
			p.Feather = NvgUtility.__maxf(1.0f, d);
			p.InnerColor = icol;
			p.OuterColor = ocol;
			return p;
		}

		public Paint RadialGradient(float cx, float cy, float inr, float outr, Color icol, Color ocol)
		{
			var p = new Paint();
			var r = (inr + outr) * 0.5f;
			var f = outr - inr;
			p.Transform.SetIdentity();
			p.Transform.T5 = cx;
			p.Transform.T6 = cy;
			p.Extent.X = r;
			p.Extent.Y = r;
			p.Radius = r;
			p.Feather = NvgUtility.__maxf(1.0f, f);
			p.InnerColor = icol;
			p.OuterColor = ocol;
			return p;
		}

		public Paint BoxGradient(float x, float y, float w, float h, float r, float f, Color icol, Color ocol)
		{
			var p = new Paint();
			p.Transform.SetIdentity();
			p.Transform.T5 = x + w * 0.5f;
			p.Transform.T6 = y + h * 0.5f;
			p.Extent.X = w * 0.5f;
			p.Extent.Y = h * 0.5f;
			p.Radius = r;
			p.Feather = NvgUtility.__maxf(1.0f, f);
			p.InnerColor = icol;
			p.OuterColor = ocol;
			return p;
		}

		public Paint ImagePattern(float cx, float cy, float w, float h, float angle, int image, float alpha)
		{
			var p = new Paint();
			p.Transform.SetRotate(angle);
			p.Transform.T5 = cx;
			p.Transform.T6 = cy;
			p.Extent.X = w;
			p.Extent.Y = h;
			p.Image = image;
			p.InnerColor = p.OuterColor = new Color(1.0f, 1.0f, 1.0f, alpha);
			return p;
		}

		public void Scissor(float x, float y, float w, float h)
		{
			var state = GetState();
			w = NvgUtility.__maxf(0.0f, w);
			h = NvgUtility.__maxf(0.0f, h);
			state.Scissor.Transform.SetIdentity();
			state.Scissor.Transform.T5 = x + w * 0.5f;
			state.Scissor.Transform.T6 = y + h * 0.5f;
			state.Scissor.Transform.Multiply(ref state.Transform);
			state.Scissor.Extent.X = w * 0.5f;
			state.Scissor.Extent.Y = h * 0.5f;
		}

		public void IntersectScissor(float x, float y, float w, float h)
		{
			var state = GetState();
			var pxform = new Transform();
			var invxorm = new Transform();
			var rect = stackalloc float[4];
			float ex = 0;
			float ey = 0;
			float tex = 0;
			float tey = 0;
			if (state.Scissor.Extent.X < 0)
			{
				Scissor(x, y, w, h);
				return;
			}

			pxform = state.Scissor.Transform;
			ex = state.Scissor.Extent.X;
			ey = state.Scissor.Extent.Y;
			invxorm = state.Transform.BuildInverse();
			pxform.Multiply(ref invxorm);
			tex = ex * NvgUtility.__absf(pxform.T1) + ey * NvgUtility.__absf(pxform.T3);
			tey = ex * NvgUtility.__absf(pxform.T2) + ey * NvgUtility.__absf(pxform.T4);
			__isectRects(rect, pxform.T5 - tex, pxform.T6 - tey, tex * 2, tey * 2, x, y, w, h);
			Scissor(rect[0], rect[1], rect[2], rect[3]);
		}

		public void ResetScissor()
		{
			var state = GetState();
			state.Scissor.Transform.Zero();
			state.Scissor.Extent.X = -1.0f;
			state.Scissor.Extent.Y = -1.0f;
		}

		public void BeginPath()
		{
			_commandsNumber = 0;
			__clearPathCache();
		}

		public void MoveTo(float x, float y)
		{
			var vals = stackalloc float[3];
			vals[0] = NVG_MOVETO;
			vals[1] = x;
			vals[2] = y;

			__appendCommands(vals, 3);
		}

		public void LineTo(float x, float y)
		{
			var vals = stackalloc float[3];
			vals[0] = NVG_LINETO;
			vals[1] = x;
			vals[2] = y;

			__appendCommands(vals, 3);
		}

		public void BezierTo(float c1x, float c1y, float c2x, float c2y, float x, float y)
		{
			var vals = stackalloc float[7];
			vals[0] = NVG_BEZIERTO;
			vals[1] = c1x;
			vals[2] = c1y;
			vals[3] = c2x;
			vals[4] = c2y;
			vals[5] = x;
			vals[6] = y;

			__appendCommands(vals, 7);
		}

		public void QuadTo(float cx, float cy, float x, float y)
		{
			var x0 = _commandX;
			var y0 = _commandY;
			var vals = stackalloc float[7];
			vals[0] = NVG_BEZIERTO;
			vals[1] = x0 + 2.0f / 3.0f * (cx - x0);
			vals[2] = y0 + 2.0f / 3.0f * (cy - y0);
			vals[3] = x + 2.0f / 3.0f * (cx - x);
			vals[4] = y + 2.0f / 3.0f * (cy - y);
			vals[5] = x;
			vals[6] = y;

			__appendCommands(vals, 7);
		}

		public void ArcTo(float x1, float y1, float x2, float y2, float radius)
		{
			var x0 = _commandX;
			var y0 = _commandY;
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
			var dir = 0;
			if (_commandsNumber == 0)
				return;

			if (__ptEquals(x0, y0, x1, y1, _distTol) != 0 || __ptEquals(x1, y1, x2, y2, _distTol) != 0 ||
				__distPtSeg(x1, y1, x0, y0, x2, y2) < _distTol * _distTol || radius < _distTol)
			{
				LineTo(x1, y1);
				return;
			}

			dx0 = x0 - x1;
			dy0 = y0 - y1;
			dx1 = x2 - x1;
			dy1 = y2 - y1;
			NvgUtility.__normalize(&dx0, &dy0);
			NvgUtility.__normalize(&dx1, &dy1);
			a = NvgUtility.acosf(dx0 * dx1 + dy0 * dy1);
			d = radius / NvgUtility.tanf(a / 2.0f);
			if (d > 10000.0f)
			{
				LineTo(x1, y1);
				return;
			}

			if (NvgUtility.__cross(dx0, dy0, dx1, dy1) > 0.0f)
			{
				cx = x1 + dx0 * d + dy0 * radius;
				cy = y1 + dy0 * d + -dx0 * radius;
				a0 = NvgUtility.atan2f(dx0, -dy0);
				a1 = NvgUtility.atan2f(-dx1, dy1);
				dir = NVG_CW;
			}
			else
			{
				cx = x1 + dx0 * d + -dy0 * radius;
				cy = y1 + dy0 * d + dx0 * radius;
				a0 = NvgUtility.atan2f(-dx0, dy0);
				a1 = NvgUtility.atan2f(dx1, -dy1);
				dir = NVG_CCW;
			}

			Arc(cx, cy, radius, a0, a1, dir);
		}

		public void ClosePath()
		{
			var vals = stackalloc float[1];
			vals[0] = NVG_CLOSE;

			__appendCommands(vals, 1);
		}

		public void PathWinding(int dir)
		{
			var vals = stackalloc float[2];
			vals[0] = NVG_WINDING;
			vals[1] = dir;

			__appendCommands(vals, 2);
		}

		public void Arc(float cx, float cy, float r, float a0, float a1, int dir)
		{
			var a = (float)0;
			var da = (float)0;
			var hda = (float)0;
			var kappa = (float)0;
			var dx = (float)0;
			var dy = (float)0;
			var x = (float)0;
			var y = (float)0;
			var tanx = (float)0;
			var tany = (float)0;
			var px = (float)0;
			var py = (float)0;
			var ptanx = (float)0;
			var ptany = (float)0;
			var vals = stackalloc float[3 + 5 * 7 + 100];
			var i = 0;
			var ndivs = 0;
			var nvals = 0;
			var move = _commandsNumber > 0 ? NVG_LINETO : NVG_MOVETO;
			da = a1 - a0;
			if (dir == NVG_CW)
			{
				if (NvgUtility.__absf(da) >= 3.14159274 * 2)
					da = (float)(3.14159274 * 2);
				else
					while (da < 0.0f)
						da += (float)(3.14159274 * 2);
			}
			else
			{
				if (NvgUtility.__absf(da) >= 3.14159274 * 2)
					da = (float)(-3.14159274 * 2);
				else
					while (da > 0.0f)
						da -= (float)(3.14159274 * 2);
			}

			ndivs = NvgUtility.__maxi(1,
				NvgUtility.__mini((int)(NvgUtility.__absf(da) / (3.14159274 * 0.5f) + 0.5f), 5));
			hda = da / ndivs / 2.0f;
			kappa = NvgUtility.__absf(4.0f / 3.0f * (1.0f - NvgUtility.cosf(hda)) / NvgUtility.sinf(hda));
			if (dir == NVG_CCW)
				kappa = -kappa;
			nvals = 0;
			for (i = 0; i <= ndivs; i++)
			{
				a = a0 + da * (i / (float)ndivs);
				dx = NvgUtility.cosf(a);
				dy = NvgUtility.sinf(a);
				x = cx + dx * r;
				y = cy + dy * r;
				tanx = -dy * r * kappa;
				tany = dx * r * kappa;
				if (i == 0)
				{
					vals[nvals++] = move;
					vals[nvals++] = x;
					vals[nvals++] = y;
				}
				else
				{
					vals[nvals++] = NVG_BEZIERTO;
					vals[nvals++] = px + ptanx;
					vals[nvals++] = py + ptany;
					vals[nvals++] = x - tanx;
					vals[nvals++] = y - tany;
					vals[nvals++] = x;
					vals[nvals++] = y;
				}

				px = x;
				py = y;
				ptanx = tanx;
				ptany = tany;
			}

			__appendCommands(vals, nvals);
		}

		public void Rect(float x, float y, float w, float h)
		{
			var vals = stackalloc float[13];
			vals[0] = NVG_MOVETO;
			vals[1] = x;
			vals[2] = y;
			vals[3] = NVG_LINETO;
			vals[4] = x;
			vals[5] = y + h;
			vals[6] = NVG_LINETO;
			vals[7] = x + w;
			vals[8] = y + h;
			vals[9] = NVG_LINETO;
			vals[10] = x + w;
			vals[11] = y;
			vals[12] = NVG_CLOSE;

			__appendCommands(vals, 13);
		}

		public void RoundedRect(float x, float y, float w, float h, float r)
		{
			RoundedRectVarying(x, y, w, h, r, r, r, r);
		}

		public void RoundedRectVarying(float x, float y, float w, float h, float radTopLeft, float radTopRight,
			float radBottomRight, float radBottomLeft)
		{
			if (radTopLeft < 0.1f && radTopRight < 0.1f && radBottomRight < 0.1f && radBottomLeft < 0.1f)
			{
				Rect(x, y, w, h);
			}
			else
			{
				var halfw = NvgUtility.__absf(w) * 0.5f;
				var halfh = NvgUtility.__absf(h) * 0.5f;
				var rxBL = NvgUtility.__minf(radBottomLeft, halfw) * NvgUtility.__signf(w);
				var ryBL = NvgUtility.__minf(radBottomLeft, halfh) * NvgUtility.__signf(h);
				var rxBR = NvgUtility.__minf(radBottomRight, halfw) * NvgUtility.__signf(w);
				var ryBR = NvgUtility.__minf(radBottomRight, halfh) * NvgUtility.__signf(h);
				var rxTR = NvgUtility.__minf(radTopRight, halfw) * NvgUtility.__signf(w);
				var ryTR = NvgUtility.__minf(radTopRight, halfh) * NvgUtility.__signf(h);
				var rxTL = NvgUtility.__minf(radTopLeft, halfw) * NvgUtility.__signf(w);
				var ryTL = NvgUtility.__minf(radTopLeft, halfh) * NvgUtility.__signf(h);
				var vals = stackalloc float[44];
				vals[0] = NVG_MOVETO;
				vals[1] = x;
				vals[2] = y + ryTL;
				vals[3] = NVG_LINETO;
				vals[4] = x;
				vals[5] = y + h - ryBL;
				vals[6] = NVG_BEZIERTO;
				vals[7] = x;
				vals[8] = y + h - ryBL * (1 - 0.5522847493f);
				vals[9] = x + rxBL * (1 - 0.5522847493f);
				vals[10] = y + h;
				vals[11] = x + rxBL;
				vals[12] = y + h;
				vals[13] = NVG_LINETO;
				vals[14] = x + w - rxBR;
				vals[15] = y + h;
				vals[16] = NVG_BEZIERTO;
				vals[17] = x + w - rxBR * (1 - 0.5522847493f);
				vals[18] = y + h;
				vals[19] = x + w;
				vals[20] = y + h - ryBR * (1 - 0.5522847493f);
				vals[21] = x + w;
				vals[22] = y + h - ryBR;
				vals[23] = NVG_LINETO;
				vals[24] = x + w;
				vals[25] = y + ryTR;
				vals[26] = NVG_BEZIERTO;
				vals[27] = x + w;
				vals[28] = y + ryTR * (1 - 0.5522847493f);
				vals[29] = x + w - rxTR * (1 - 0.5522847493f);
				vals[30] = y;
				vals[31] = x + w - rxTR;
				vals[32] = y;
				vals[33] = NVG_LINETO;
				vals[34] = x + rxTL;
				vals[35] = y;
				vals[36] = NVG_BEZIERTO;
				vals[37] = x + rxTL * (1 - 0.5522847493f);
				vals[38] = y;
				vals[39] = x;
				vals[40] = y + ryTL * (1 - 0.5522847493f);
				vals[41] = x;
				vals[42] = y + ryTL;
				vals[43] = NVG_CLOSE;
				__appendCommands(vals, 44);
			}
		}

		public void Ellipse(float cx, float cy, float rx, float ry)
		{
			var vals = stackalloc float[32];
			vals[0] = NVG_MOVETO;
			vals[1] = cx - rx;
			vals[2] = cy;
			vals[3] = NVG_BEZIERTO;
			vals[4] = cx - rx;
			vals[5] = cy + ry * 0.5522847493f;
			vals[6] = cx - rx * 0.5522847493f;
			vals[7] = cy + ry;
			vals[8] = cx;
			vals[9] = cy + ry;
			vals[10] = NVG_BEZIERTO;
			vals[11] = cx + rx * 0.5522847493f;
			vals[12] = cy + ry;
			vals[13] = cx + rx;
			vals[14] = cy + ry * 0.5522847493f;
			vals[15] = cx + rx;
			vals[16] = cy;
			vals[17] = NVG_BEZIERTO;
			vals[18] = cx + rx;
			vals[19] = cy - ry * 0.5522847493f;
			vals[20] = cx + rx * 0.5522847493f;
			vals[21] = cy - ry;
			vals[22] = cx;
			vals[23] = cy - ry;
			vals[24] = NVG_BEZIERTO;
			vals[25] = cx - rx * 0.5522847493f;
			vals[26] = cy - ry;
			vals[27] = cx - rx;
			vals[28] = cy - ry * 0.5522847493f;
			vals[29] = cx - rx;
			vals[30] = cy;
			vals[31] = NVG_CLOSE;

			__appendCommands(vals, 32);
		}

		public void Circle(float cx, float cy, float r)
		{
			Ellipse(cx, cy, r, r);
		}

		/*		public void DebugDumpPathCache()
                {
                    NVGpath path;
                    int i = 0;
                    int j = 0;
                    printf("Dumping %d cached paths\n", (int)(cache.Paths.Count));
                    for (i = (int)(0); (i) < (cache.Paths.Count); i++)
                    {
                        path = &cache.Paths[i];
                        printf(" - Path %d\n", (int)(i));
                        if ((path.nfill) != 0)
                        {
                            printf("   - fill: %d\n", (int)(path.nfill));
                            for (j = (int)(0); (j) < (path.nfill); j++)
                            {
                                printf("%f\t%f\n", (double)(path.Fill[j].X), (double)(path.Fill[j].Y));
                            }
                        }
                        if ((path.nstroke) != 0)
                        {
                            printf("   - stroke: %d\n", (int)(path.nstroke));
                            for (j = (int)(0); (j) < (path.nstroke); j++)
                            {
                                printf("%f\t%f\n", (double)(path.Stroke[j].X), (double)(path.Stroke[j].Y));
                            }
                        }
                    }
                }*/

		private static void MultiplyAlpha(ref Color c, float alpha)
		{
			var na = (int)(c.A * alpha);

			c = new Color(c.R, c.G, c.B, na);
		}

		public void Fill()
		{
			var state = GetState();
			Path path;
			var fillPaint = state.Fill;
			var i = 0;
			__flattenPaths();
			if (_edgeAntiAlias != 0 && state.ShapeAntiAlias != 0)
				__expandFill(_fringeWidth, NVG_MITER, 2.4f);
			else
				__expandFill(0.0f, NVG_MITER, 2.4f);
			MultiplyAlpha(ref fillPaint.InnerColor, state.Alpha);
			MultiplyAlpha(ref fillPaint.OuterColor, state.Alpha);
			_renderer.RenderFill(ref fillPaint, ref state.Scissor, _fringeWidth, _cache.Bounds,
				_cache.Paths.ToArraySegment());

			for (i = 0; i < _cache.Paths.Count; i++)
			{
				path = _cache.Paths[i];
				if (path.Fill != null)
					_fillTriCount += path.Fill.Value.Count - 2;

				if (path.Stroke != null)
					_fillTriCount += path.Stroke.Value.Count - 2;
				_drawCallCount += 2;
			}
		}

		public void Stroke()
		{
			var state = GetState();
			var scale = __getAverageScale(ref state.Transform);
			var strokeWidth = NvgUtility.__clampf(state.StrokeWidth * scale, 0.0f, 200.0f);
			var strokePaint = state.Stroke;
			Path path;
			var i = 0;
			if (strokeWidth < _fringeWidth)
			{
				var alpha = NvgUtility.__clampf(strokeWidth / _fringeWidth, 0.0f, 1.0f);

				MultiplyAlpha(ref strokePaint.InnerColor, alpha * alpha);
				MultiplyAlpha(ref strokePaint.OuterColor, alpha * alpha);
				strokeWidth = _fringeWidth;
			}

			MultiplyAlpha(ref strokePaint.InnerColor, state.Alpha);
			MultiplyAlpha(ref strokePaint.OuterColor, state.Alpha);

			__flattenPaths();
			if (_edgeAntiAlias != 0 && state.ShapeAntiAlias != 0)
				__expandStroke(strokeWidth * 0.5f, _fringeWidth, state.LineCap, state.LineJoin, state.MiterLimit);
			else
				__expandStroke(strokeWidth * 0.5f, 0.0f, state.LineCap, state.LineJoin, state.MiterLimit);
			_renderer.RenderStroke(ref strokePaint, ref state.Scissor,
				_fringeWidth, strokeWidth, _cache.Paths.ToArraySegment());
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				path = _cache.Paths[i];
				_strokeTriCount += path.Stroke.Value.Count - 2;
				_drawCallCount++;
			}
		}

		public int CreateFontMem(string name, byte[] data)
		{
			return _fontSyst.AddFontMem(name, data);
		}

		public int FindFont(string name)
		{
			if (name == null)
				return -1;
			return _fontSyst.GetFontByName(name);
		}

		public int AddFallbackFontId(int baseFont, int fallbackFont)
		{
			if (baseFont == -1 || fallbackFont == -1)
				return 0;
			return _fontSyst.AddFallbackFont(baseFont, fallbackFont);
		}

		public int AddFallbackFont(string baseFont, string fallbackFont)
		{
			return AddFallbackFontId(FindFont(baseFont), FindFont(fallbackFont));
		}

		public void FontSize(float size)
		{
			var state = GetState();
			state.FontSize = size;
		}

		public void FontBlur(float blur)
		{
			var state = GetState();
			state.FontBlur = blur;
		}

		public void TextLetterSpacing(float spacing)
		{
			var state = GetState();
			state.LetterSpacing = spacing;
		}

		public void TextLineHeight(float lineHeight)
		{
			var state = GetState();
			state.LineHeight = lineHeight;
		}

		public void TextAlign(int align)
		{
			var state = GetState();
			state.TextAlign = align;
		}

		public void FontFaceId(int font)
		{
			var state = GetState();
			state.FontId = font;
		}

		public void FontFace(string font)
		{
			var state = GetState();
			state.FontId = _fontSyst.GetFontByName(font);
		}

		public float Text(float x, float y, StringSegment _string_)
		{
			var state = GetState();
			var iter = new FontTextIterator();
			var prevIter = new FontTextIterator();
			var q = new FontGlyphSquad();
			var scale = __getFontScale(state) * _devicePxRatio;
			var invscale = 1.0f / scale;
			var cverts = 0;
			var nverts = 0;
			if (state.FontId == -1)
				return x;
			_fontSyst.SetSize(state.FontSize * scale);
			_fontSyst.SetSpacing(state.LetterSpacing * scale);
			_fontSyst.SetBlur(state.FontBlur * scale);
			_fontSyst.SetAlign(state.TextAlign);
			_fontSyst.SetFont(state.FontId);
			cverts = NvgUtility.__maxi(2, _string_.Length) * 6;
			var verts = __allocTempVerts(cverts);

			_fontSyst.TextIterInit(iter, x * scale, y * scale, _string_, FontSystem.FONS_GLYPH_BITMAP_REQUIRED);
			prevIter = iter;

			while (_fontSyst.TextIterNext(iter, &q))
			{
				var c = stackalloc float[4 * 2];
				if (iter.PrevGlyphIndex == -1)
				{
					if (nverts != 0)
					{
						var segment = new ArraySegment<Vertex>(verts.Array, verts.Offset, nverts);
						__renderText(segment);
						nverts = 0;
					}

					if (__allocTextAtlas() == 0)
						break;
					iter = prevIter;
					_fontSyst.TextIterNext(iter, &q);
					if (iter.PrevGlyphIndex == -1)
						break;
				}

				prevIter = iter;
				state.Transform.TransformPoint(out c[0], out c[1], q.X0 * invscale, q.Y0 * invscale);
				state.Transform.TransformPoint(out c[2], out c[3], q.X1 * invscale, q.Y0 * invscale);
				state.Transform.TransformPoint(out c[4], out c[5], q.X1 * invscale, q.Y1 * invscale);
				state.Transform.TransformPoint(out c[6], out c[7], q.X0 * invscale, q.Y1 * invscale);
				if (nverts + 6 <= cverts)
				{
					__vset(ref verts.Array[verts.Offset + nverts], c[0], c[1], q.S0, q.T0);
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], c[4], c[5], q.S1, q.T1);
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], c[2], c[3], q.S1, q.T0);
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], c[0], c[1], q.S0, q.T0);
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], c[6], c[7], q.S0, q.T1);
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], c[4], c[5], q.S1, q.T1);
					nverts++;
				}
			}

			__flushTextTexture();

			var segment2 = new ArraySegment<Vertex>(verts.Array, verts.Offset, nverts);
			__renderText(segment2);

			return iter.NextX / scale;
		}

		public void TextBox(float x, float y, float breakRowWidth, StringSegment _string_)
		{
			var state = GetState();
			var i = 0;
			var oldAlign = state.TextAlign;
			var haling = state.TextAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT);
			var valign = state.TextAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE);
			var lineh = (float)0;
			if (state.FontId == -1)
				return;
			float ascender, descender;
			TextMetrics(out ascender, out descender, out lineh);
			state.TextAlign = NVG_ALIGN_LEFT | valign;
			while (true)
			{
				var nrows = TextBreakLines(_string_, breakRowWidth, _rows, out _string_);

				if (nrows <= 0)
					break;
				for (i = 0; i < nrows; i++)
				{
					var row = _rows[i];
					if ((haling & NVG_ALIGN_LEFT) != 0)
						Text(x, y, row.Str);
					else if ((haling & NVG_ALIGN_CENTER) != 0)
						Text(x + breakRowWidth * 0.5f - row.Width * 0.5f, y, row.Str);
					else if ((haling & NVG_ALIGN_RIGHT) != 0)
						Text(x + breakRowWidth - row.Width, y, row.Str);
					y += lineh * state.LineHeight;
				}
			}

			state.TextAlign = oldAlign;
		}

		public int TextGlyphPositions(float x, float y, StringSegment _string_, GlyphPosition[] positions)
		{
			var state = GetState();
			var scale = __getFontScale(state) * _devicePxRatio;
			var invscale = 1.0f / scale;
			var iter = new FontTextIterator();
			var prevIter = new FontTextIterator();
			var q = new FontGlyphSquad();
			var npos = 0;
			if (state.FontId == -1)
				return 0;

			if (_string_.IsNullOrEmpty)
				return 0;

			_fontSyst.SetSize(state.FontSize * scale);
			_fontSyst.SetSpacing(state.LetterSpacing * scale);
			_fontSyst.SetBlur(state.FontBlur * scale);
			_fontSyst.SetAlign(state.TextAlign);
			_fontSyst.SetFont(state.FontId);
			_fontSyst.TextIterInit(iter, x * scale, y * scale, _string_, FontSystem.FONS_GLYPH_BITMAP_OPTIONAL);
			prevIter = iter;
			while (_fontSyst.TextIterNext(iter, &q))
			{
				if (iter.PrevGlyphIndex < 0 && __allocTextAtlas() != 0)
				{
					iter = prevIter;
					_fontSyst.TextIterNext(iter, &q);
				}

				prevIter = iter;
				positions[npos].Str = iter.Str;
				positions[npos].X = iter.X * invscale;
				positions[npos].MinX = NvgUtility.__minf(iter.X, q.X0) * invscale;
				positions[npos].MaxX = NvgUtility.__maxf(iter.NextX, q.X1) * invscale;
				npos++;
				if (npos >= positions.Length)
					break;
			}

			return npos;
		}

		public int TextBreakLines(StringSegment _string_, float breakRowWidth, TextRow[] rows,
			out StringSegment remaining)
		{
			remaining = StringSegment.Null;

			var state = GetState();
			var scale = __getFontScale(state) * _devicePxRatio;
			var invscale = 1.0f / scale;
			var iter = new FontTextIterator();
			var prevIter = new FontTextIterator();
			var q = new FontGlyphSquad();
			var nrows = 0;
			var rowStartX = (float)0;
			var rowWidth = (float)0;
			var rowMinX = (float)0;
			var rowMaxX = (float)0;
			int? rowStart = null;
			int? rowEnd = null;
			int? wordStart = null;
			int? breakEnd = null;
			var wordStartX = (float)0;
			var wordMinX = (float)0;
			var breakWidth = (float)0;
			var breakMaxX = (float)0;
			var type = NVG_SPACE;
			var ptype = NVG_SPACE;
			var pcodepoint = (uint)0;

			if (state.FontId == -1)
				return 0;

			if (_string_.IsNullOrEmpty)
				return 0;
			_fontSyst.SetSize(state.FontSize * scale);
			_fontSyst.SetSpacing(state.LetterSpacing * scale);
			_fontSyst.SetBlur(state.FontBlur * scale);
			_fontSyst.SetAlign(state.TextAlign);
			_fontSyst.SetFont(state.FontId);
			breakRowWidth *= scale;
			_fontSyst.TextIterInit(iter, 0, 0, _string_, FontSystem.FONS_GLYPH_BITMAP_OPTIONAL);
			prevIter = iter;
			while (_fontSyst.TextIterNext(iter, &q))
			{
				if (iter.PrevGlyphIndex < 0 && __allocTextAtlas() != 0)
				{
					iter = prevIter;
					_fontSyst.TextIterNext(iter, &q);
				}

				prevIter = iter;
				switch (iter.Codepoint)
				{
					case 9:
					case 11:
					case 12:
					case 32:
					case 0x00a0:
						type = NVG_SPACE;
						break;
					case 10:
						type = pcodepoint == 13 ? NVG_SPACE : NVG_NEWLINE;
						break;
					case 13:
						type = pcodepoint == 10 ? NVG_SPACE : NVG_NEWLINE;
						break;
					case 0x0085:
						type = NVG_NEWLINE;
						break;
					default:
						if (iter.Codepoint >= 0x4E00 && iter.Codepoint <= 0x9FFF ||
							iter.Codepoint >= 0x3000 && iter.Codepoint <= 0x30FF ||
							iter.Codepoint >= 0xFF00 && iter.Codepoint <= 0xFFEF ||
							iter.Codepoint >= 0x1100 && iter.Codepoint <= 0x11FF ||
							iter.Codepoint >= 0x3130 && iter.Codepoint <= 0x318F ||
							iter.Codepoint >= 0xAC00 && iter.Codepoint <= 0xD7AF)
							type = NVG_CJK_CHAR;
						else
							type = NVG_CHAR;
						break;
				}

				if (type == NVG_NEWLINE)
				{
					rows[nrows].Str = rowStart == null ? iter.Str : new StringSegment(iter.Str, rowStart.Value);
					if (rowEnd != null)
						rows[nrows].Str.Length = rowEnd.Value - rows[nrows].Str.Location;
					else
						rows[nrows].Str.Length = 0;
					rows[nrows].Width = rowWidth * invscale;
					rows[nrows].MinX = rowMinX * invscale;
					rows[nrows].MaxX = rowMaxX * invscale;
					remaining = iter.Next;
					nrows++;
					if (nrows >= rows.Length)
						return nrows;
					breakEnd = rowStart;
					breakWidth = (float)0.0;
					breakMaxX = (float)0.0;
					rowStart = null;
					rowEnd = null;
					rowWidth = 0;
					rowMinX = rowMaxX = 0;
				}
				else
				{
					if (rowStart == null)
					{
						if (type == NVG_CHAR || type == NVG_CJK_CHAR)
						{
							rowStartX = iter.X;
							rowStart = iter.Str.Location;
							rowEnd = iter.Str.Location + 1;
							rowWidth = iter.NextX - rowStartX;
							rowMinX = q.X0 - rowStartX;
							rowMaxX = q.X1 - rowStartX;
							wordStart = iter.Str.Location;
							wordStartX = iter.X;
							wordMinX = q.X0 - rowStartX;
							breakEnd = rowStart;
							breakWidth = (float)0.0;
							breakMaxX = (float)0.0;
						}
					}
					else
					{
						var nextWidth = iter.NextX - rowStartX;
						if (type == NVG_CHAR || type == NVG_CJK_CHAR)
						{
							rowEnd = iter.Str.Location + 1;
							rowWidth = iter.NextX - rowStartX;
							rowMaxX = q.X1 - rowStartX;
						}

						if ((ptype == NVG_CHAR || ptype == NVG_CJK_CHAR) && type == NVG_SPACE || type == NVG_CJK_CHAR)
						{
							breakEnd = iter.Str.Location;
							breakWidth = rowWidth;
							breakMaxX = rowMaxX;
						}

						if (ptype == NVG_SPACE && (type == NVG_CHAR || type == NVG_CJK_CHAR) || type == NVG_CJK_CHAR)
						{
							wordStart = iter.Str.Location;
							wordStartX = iter.X;
							wordMinX = q.X0 - rowStartX;
						}

						if ((type == NVG_CHAR || type == NVG_CJK_CHAR) && nextWidth > breakRowWidth)
						{
							if (breakEnd == rowStart)
							{
								rows[nrows].Str = new StringSegment(_string_, rowStart.Value, iter.Str.Location);
								rows[nrows].Width = rowWidth * invscale;
								rows[nrows].MinX = rowMinX * invscale;
								rows[nrows].MaxX = rowMaxX * invscale;
								remaining = iter.Str;
								nrows++;
								if (nrows >= rows.Length)
									return nrows;
								rowStartX = iter.X;
								rowStart = iter.Str.Location;
								rowEnd = iter.Str.Location + 1;
								rowWidth = iter.NextX - rowStartX;
								rowMinX = q.X0 - rowStartX;
								rowMaxX = q.X1 - rowStartX;
								wordStart = iter.Str.Location;
								wordStartX = iter.X;
								wordMinX = q.X0 - rowStartX;
							}
							else
							{
								rows[nrows].Str = new StringSegment(_string_, rowStart.Value,
									breakEnd.Value - rowStart.Value);
								rows[nrows].Width = breakWidth * invscale;
								rows[nrows].MinX = rowMinX * invscale;
								rows[nrows].MaxX = breakMaxX * invscale;
								remaining = new StringSegment(_string_, wordStart.Value);

								nrows++;
								if (nrows >= rows.Length)
									return nrows;
								rowStartX = wordStartX;
								rowStart = wordStart;
								rowEnd = iter.Str.Location + 1;
								rowWidth = iter.NextX - rowStartX;
								rowMinX = wordMinX;
								rowMaxX = q.X1 - rowStartX;
							}

							breakEnd = rowStart;
							breakWidth = (float)0.0;
							breakMaxX = (float)0.0;
						}
					}
				}

				pcodepoint = iter.Codepoint;
				ptype = type;
			}

			if (rowStart != null)
			{
				rows[nrows].Str = new StringSegment(_string_, rowStart.Value, rowEnd.Value - rowStart.Value);
				rows[nrows].Width = rowWidth * invscale;
				rows[nrows].MinX = rowMinX * invscale;
				rows[nrows].MaxX = rowMaxX * invscale;
				remaining = StringSegment.Null;

				nrows++;
			}

			return nrows;
		}

		public float TextBounds(float x, float y, string _string_, ref Bounds bounds)
		{
			var state = GetState();
			var scale = __getFontScale(state) * _devicePxRatio;
			var invscale = 1.0f / scale;
			float width = 0;
			if (state.FontId == -1)
				return 0;
			_fontSyst.SetSize(state.FontSize * scale);
			_fontSyst.SetSpacing(state.LetterSpacing * scale);
			_fontSyst.SetBlur(state.FontBlur * scale);
			_fontSyst.SetAlign(state.TextAlign);
			_fontSyst.SetFont(state.FontId);
			width = _fontSyst.TextBounds(x * scale, y * scale, _string_, ref bounds);
			_fontSyst.LineBounds(y * scale, ref bounds.b2, ref bounds.b4);
			bounds.b1 *= invscale;
			bounds.b2 *= invscale;
			bounds.b3 *= invscale;
			bounds.b4 *= invscale;

			return width * invscale;
		}

		public void TextBoxBounds(float x, float y, float breakRowWidth, StringSegment _string_, ref Bounds bounds)
		{
			var state = GetState();
			var scale = __getFontScale(state) * _devicePxRatio;
			var invscale = 1.0f / scale;
			var i = 0;
			var oldAlign = state.TextAlign;
			var haling = state.TextAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT);
			var valign = state.TextAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE);
			var lineh = (float)0;
			var rminy = (float)0;
			var rmaxy = (float)0;
			float minx = 0;
			float miny = 0;
			float maxx = 0;
			float maxy = 0;
			if (state.FontId == -1)
			{
				bounds.b1 = bounds.b2 = bounds.b3 = bounds.b4 = 0.0f;
				return;
			}

			float ascender, descender;
			TextMetrics(out ascender, out descender, out lineh);
			state.TextAlign = NVG_ALIGN_LEFT | valign;
			minx = maxx = x;
			miny = maxy = y;
			_fontSyst.SetSize(state.FontSize * scale);
			_fontSyst.SetSpacing(state.LetterSpacing * scale);
			_fontSyst.SetBlur(state.FontBlur * scale);
			_fontSyst.SetAlign(state.TextAlign);
			_fontSyst.SetFont(state.FontId);
			_fontSyst.LineBounds(0, ref rminy, ref rmaxy);
			rminy *= invscale;
			rmaxy *= invscale;
			while (true)
			{
				var nrows = TextBreakLines(_string_, breakRowWidth, _rows, out _string_);
				if (nrows <= 0)
					break;
				for (i = 0; i < nrows; i++)
				{
					var row = _rows[i];
					float rminx = 0;
					float rmaxx = 0;
					var dx = (float)0;
					if ((haling & NVG_ALIGN_LEFT) != 0)
						dx = 0;
					else if ((haling & NVG_ALIGN_CENTER) != 0)
						dx = breakRowWidth * 0.5f - row.Width * 0.5f;
					else if ((haling & NVG_ALIGN_RIGHT) != 0)
						dx = breakRowWidth - row.Width;
					rminx = x + row.MinX + dx;
					rmaxx = x + row.MaxX + dx;
					minx = NvgUtility.__minf(minx, rminx);
					maxx = NvgUtility.__maxf(maxx, rmaxx);
					miny = NvgUtility.__minf(miny, y + rminy);
					maxy = NvgUtility.__maxf(maxy, y + rmaxy);
					y += lineh * state.LineHeight;
				}
			}

			state.TextAlign = oldAlign;
			bounds.b1 = minx;
			bounds.b2 = miny;
			bounds.b3 = maxx;
			bounds.b4 = maxy;
		}

		public void TextMetrics(out float ascender, out float descender, out float lineh)
		{
			ascender = descender = lineh = 0;

			var state = GetState();
			var scale = __getFontScale(state) * _devicePxRatio;
			var invscale = 1.0f / scale;
			if (state.FontId == -1)
				return;
			_fontSyst.SetSize(state.FontSize * scale);
			_fontSyst.SetSpacing(state.LetterSpacing * scale);
			_fontSyst.SetBlur(state.FontBlur * scale);
			_fontSyst.SetAlign(state.TextAlign);
			_fontSyst.SetFont(state.FontId);
			_fontSyst.VertMetrics(out ascender, out descender, out lineh);
			ascender *= invscale;
			descender *= invscale;
			lineh *= invscale;
		}

		private void SetDevicePixelRatio(float ratio)
		{
			_tessTol = 0.25f / ratio;
			_distTol = 0.01f / ratio;
			_fringeWidth = 1.0f / ratio;
			_devicePxRatio = ratio;
		}

		private NvgContextState GetState()
		{
			return _states[_statesNumber - 1];
		}

		private void __appendCommands(float* vals, int nvals)
		{
			var state = GetState();
			var i = 0;
			if (_commandsNumber + nvals > _commandsCount)
			{
				float* commands;
				var ccommands = _commandsNumber + nvals + _commandsCount / 2;
				commands = (float*)CRuntime.realloc(_commands, (ulong)(sizeof(float) * ccommands));
				if (commands == null)
					return;
				_commands = commands;
				_commandsCount = ccommands;
			}

			if ((int)vals[0] != NVG_CLOSE && (int)vals[0] != NVG_WINDING)
			{
				_commandX = vals[nvals - 2];
				_commandY = vals[nvals - 1];
			}

			i = 0;
			while (i < nvals)
			{
				var cmd = (int)vals[i];
				switch (cmd)
				{
					case NVG_MOVETO:
						state.Transform.TransformPoint(out vals[i + 1], out vals[i + 2], vals[i + 1], vals[i + 2]);
						i += 3;
						break;
					case NVG_LINETO:
						state.Transform.TransformPoint(out vals[i + 1], out vals[i + 2], vals[i + 1], vals[i + 2]);
						i += 3;
						break;
					case NVG_BEZIERTO:
						state.Transform.TransformPoint(out vals[i + 1], out vals[i + 2], vals[i + 1], vals[i + 2]);
						state.Transform.TransformPoint(out vals[i + 3], out vals[i + 4], vals[i + 3], vals[i + 4]);
						state.Transform.TransformPoint(out vals[i + 5], out vals[i + 6], vals[i + 5], vals[i + 6]);
						i += 7;
						break;
					case NVG_CLOSE:
						i++;
						break;
					case NVG_WINDING:
						i += 2;
						break;
					default:
						i++;
						break;
				}
			}

			CRuntime.memcpy(&_commands[_commandsNumber], vals, (ulong)(nvals * sizeof(float)));
			_commandsNumber += nvals;
		}

		private void __clearPathCache()
		{
			_cache.Paths.Clear();
			_cache.PointsNumber = 0;
		}

		private Path __lastPath()
		{
			if (_cache.Paths.Count > 0)
				return _cache.Paths[_cache.Paths.Count - 1];
			return null;
		}

		private void __addPath()
		{
			var newPath = new Path
			{
				First = _cache.PointsNumber,
				Winding = NVG_CCW
			};

			_cache.Paths.Add(newPath);
		}

		private NvgPoint* __lastPoint()
		{
			if (_cache.PointsNumber > 0)
				return &_cache.Points[_cache.PointsNumber - 1];
			return null;
		}

		private void __addPoint(float x, float y, int flags)
		{
			var path = __lastPath();
			NvgPoint* pt;
			if (path == null)
				return;
			if (path.Count > 0 && _cache.PointsNumber > 0)
			{
				pt = __lastPoint();
				if (__ptEquals(pt->X, pt->Y, x, y, _distTol) != 0)
				{
					pt->flags |= (byte)flags;
					return;
				}
			}

			if (_cache.PointsNumber + 1 > _cache.PointsCount)
			{
				NvgPoint* points;
				var cpoints = _cache.PointsNumber + 1 + _cache.PointsCount / 2;
				points = (NvgPoint*)CRuntime.realloc(_cache.Points, (ulong)(sizeof(NvgPoint) * cpoints));
				if (points == null)
					return;
				_cache.Points = points;
				_cache.PointsCount = cpoints;
			}

			pt = &_cache.Points[_cache.PointsNumber];
			pt->Reset();
			pt->X = x;
			pt->Y = y;
			pt->flags = (byte)flags;
			_cache.PointsNumber++;
			path.Count++;
		}

		private void __closePath()
		{
			var path = __lastPath();
			if (path == null)
				return;
			path.Closed = 1;
		}

		private void __pathWinding(int winding)
		{
			var path = __lastPath();
			if (path == null)
				return;
			path.Winding = winding;
		}

		private ArraySegment<Vertex> __allocTempVerts(int nverts)
		{
			_cache.Vertexes.EnsureSize(nverts);

			return new ArraySegment<Vertex>(_cache.Vertexes.Array);
		}

		private void __tesselateBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4,
			int level, int type)
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
			if (level > 10)
				return;
			x12 = (x1 + x2) * 0.5f;
			y12 = (y1 + y2) * 0.5f;
			x23 = (x2 + x3) * 0.5f;
			y23 = (y2 + y3) * 0.5f;
			x34 = (x3 + x4) * 0.5f;
			y34 = (y3 + y4) * 0.5f;
			x123 = (x12 + x23) * 0.5f;
			y123 = (y12 + y23) * 0.5f;
			dx = x4 - x1;
			dy = y4 - y1;
			d2 = NvgUtility.__absf((x2 - x4) * dy - (y2 - y4) * dx);
			d3 = NvgUtility.__absf((x3 - x4) * dy - (y3 - y4) * dx);
			if ((d2 + d3) * (d2 + d3) < _tessTol * (dx * dx + dy * dy))
			{
				__addPoint(x4, y4, type);
				return;
			}

			x234 = (x23 + x34) * 0.5f;
			y234 = (y23 + y34) * 0.5f;
			x1234 = (x123 + x234) * 0.5f;
			y1234 = (y123 + y234) * 0.5f;
			__tesselateBezier(x1, y1, x12, y12, x123, y123, x1234, y1234, level + 1, 0);
			__tesselateBezier(x1234, y1234, x234, y234, x34, y34, x4, y4, level + 1, type);
		}

		private void __flattenPaths()
		{
			NvgPoint* last;
			NvgPoint* p0;
			NvgPoint* p1;
			NvgPoint* pts;
			Path path;
			var i = 0;
			var j = 0;
			float* cp1;
			float* cp2;
			float* p;
			float area = 0;
			if (_cache.Paths.Count > 0)
				return;
			i = 0;
			while (i < _commandsNumber)
			{
				var cmd = (int)_commands[i];
				switch (cmd)
				{
					case NVG_MOVETO:
						__addPath();
						p = &_commands[i + 1];
						__addPoint(p[0], p[1], NVG_PT_CORNER);
						i += 3;
						break;
					case NVG_LINETO:
						p = &_commands[i + 1];
						__addPoint(p[0], p[1], NVG_PT_CORNER);
						i += 3;
						break;
					case NVG_BEZIERTO:
						last = __lastPoint();
						if (last != null)
						{
							cp1 = &_commands[i + 1];
							cp2 = &_commands[i + 3];
							p = &_commands[i + 5];
							__tesselateBezier(last->X, last->Y, cp1[0], cp1[1], cp2[0], cp2[1], p[0], p[1], 0,
								NVG_PT_CORNER);
						}

						i += 7;
						break;
					case NVG_CLOSE:
						__closePath();
						i++;
						break;
					case NVG_WINDING:
						__pathWinding((int)_commands[i + 1]);
						i += 2;
						break;
					default:
						i++;
						break;
				}
			}

			_cache.Bounds.b1 = _cache.Bounds.b2 = 1e6f;
			_cache.Bounds.b3 = _cache.Bounds.b4 = -1e6f;
			for (j = 0; j < _cache.Paths.Count; j++)
			{
				path = _cache.Paths[j];
				pts = &_cache.Points[path.First];
				p0 = &pts[path.Count - 1];
				p1 = &pts[0];
				if (__ptEquals(p0->X, p0->Y, p1->X, p1->Y, _distTol) != 0)
				{
					path.Count--;
					p0 = &pts[path.Count - 1];
					path.Closed = 1;
				}

				if (path.Count > 2)
				{
					area = __polyArea(pts, path.Count);
					if (path.Winding == NVG_CCW && area < 0.0f)
						__polyReverse(pts, path.Count);
					if (path.Winding == NVG_CW && area > 0.0f)
						__polyReverse(pts, path.Count);
				}

				for (i = 0; i < path.Count; i++)
				{
					p0->DeltaX = p1->X - p0->X;
					p0->DeltaY = p1->Y - p0->Y;
					p0->Length = NvgUtility.__normalize(&p0->DeltaX, &p0->DeltaY);
					_cache.Bounds.b1 = NvgUtility.__minf(_cache.Bounds.b1, p0->X);
					_cache.Bounds.b2 = NvgUtility.__minf(_cache.Bounds.b2, p0->Y);
					_cache.Bounds.b3 = NvgUtility.__maxf(_cache.Bounds.b3, p0->X);
					_cache.Bounds.b4 = NvgUtility.__maxf(_cache.Bounds.b4, p0->Y);
					p0 = p1++;
				}
			}
		}

		private void __calculateJoins(float w, int lineJoin, float miterLimit)
		{
			var i = 0;
			var j = 0;
			var iw = 0.0f;
			if (w > 0.0f)
				iw = 1.0f / w;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				var pts = &_cache.Points[path.First];
				var p0 = &pts[path.Count - 1];
				var p1 = &pts[0];
				var nleft = 0;
				path.BevelCount = 0;
				for (j = 0; j < path.Count; j++)
				{
					float dlx0 = 0;
					float dly0 = 0;
					float dlx1 = 0;
					float dly1 = 0;
					float dmr2 = 0;
					float cross = 0;
					float limit = 0;
					dlx0 = p0->DeltaY;
					dly0 = -p0->DeltaX;
					dlx1 = p1->DeltaY;
					dly1 = -p1->DeltaX;
					p1->dmx = (dlx0 + dlx1) * 0.5f;
					p1->dmy = (dly0 + dly1) * 0.5f;
					dmr2 = p1->dmx * p1->dmx + p1->dmy * p1->dmy;
					if (dmr2 > 0.000001f)
					{
						var scale = 1.0f / dmr2;
						if (scale > 600.0f)
							scale = 600.0f;
						p1->dmx *= scale;
						p1->dmy *= scale;
					}

					p1->flags = (byte)((p1->flags & NVG_PT_CORNER) != 0 ? NVG_PT_CORNER : 0);
					cross = p1->DeltaX * p0->DeltaY - p0->DeltaX * p1->DeltaY;
					if (cross > 0.0f)
					{
						nleft++;
						p1->flags |= NVG_PT_LEFT;
					}

					limit = NvgUtility.__maxf(1.01f, NvgUtility.__minf(p0->Length, p1->Length) * iw);
					if (dmr2 * limit * limit < 1.0f)
						p1->flags |= NVG_PR_INNERBEVEL;
					if ((p1->flags & NVG_PT_CORNER) != 0)
						if (dmr2 * miterLimit * miterLimit < 1.0f || lineJoin == NVG_BEVEL || lineJoin == NVG_ROUND)
							p1->flags |= NVG_PT_BEVEL;
					if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
						path.BevelCount++;
					p0 = p1++;
				}

				path.Convex = nleft == path.Count ? 1 : 0;
			}
		}

		private int __expandStroke(float w, float fringe, int lineCap, int lineJoin, float miterLimit)
		{
			var cverts = 0;
			var i = 0;
			var j = 0;
			var aa = fringe;
			var u0 = 0.0f;
			var u1 = 1.0f;
			var ncap = __curveDivs(w, (float)3.14159274, _tessTol);
			w += aa * 0.5f;
			if (aa == 0.0f)
			{
				u0 = 0.5f;
				u1 = 0.5f;
			}

			__calculateJoins(w, lineJoin, miterLimit);
			cverts = 0;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				var loop = path.Closed == 0 ? 0 : 1;
				if (lineJoin == NVG_ROUND)
					cverts += (path.Count + path.BevelCount * (ncap + 2) + 1) * 2;
				else
					cverts += (path.Count + path.BevelCount * 5 + 1) * 2;
				if (loop == 0)
				{
					if (lineCap == NVG_ROUND)
						cverts += (ncap * 2 + 2) * 2;
					else
						cverts += (3 + 3) * 2;
				}
			}

			var verts = __allocTempVerts(cverts);
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				var pts = &_cache.Points[path.First];
				NvgPoint* p0;
				NvgPoint* p1;
				var s = 0;
				var e = 0;
				var loop = 0;
				float dx = 0;
				float dy = 0;
				path.Fill = null;
				loop = path.Closed == 0 ? 0 : 1;
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					if (loop != 0)
					{
						p0 = &pts[path.Count - 1];
						p1 = &pts[0];
						s = 0;
						e = path.Count;
					}
					else
					{
						p0 = &pts[0];
						p1 = &pts[1];
						s = 1;
						e = path.Count - 1;
					}

					if (loop == 0)
					{
						dx = p1->X - p0->X;
						dy = p1->Y - p0->Y;
						NvgUtility.__normalize(&dx, &dy);
						if (lineCap == NVG_BUTT)
							dst = __buttCapStart(dst, p0, dx, dy, w, -aa * 0.5f, aa, u0, u1);
						else if (lineCap == NVG_BUTT || lineCap == NVG_SQUARE)
							dst = __buttCapStart(dst, p0, dx, dy, w, w - aa, aa, u0, u1);
						else if (lineCap == NVG_ROUND)
							dst = __roundCapStart(dst, p0, dx, dy, w, ncap, aa, u0, u1);
					}

					for (j = s; j < e; ++j)
					{
						if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
						{
							if (lineJoin == NVG_ROUND)
								dst = __roundJoin(dst, p0, p1, w, w, u0, u1, ncap, aa);
							else
								dst = __bevelJoin(dst, p0, p1, w, w, u0, u1, aa);
						}
						else
						{
							__vset(dst, p1->X + p1->dmx * w, p1->Y + p1->dmy * w, u0, 1);
							dst++;
							__vset(dst, p1->X - p1->dmx * w, p1->Y - p1->dmy * w, u1, 1);
							dst++;
						}

						p0 = p1++;
					}

					if (loop != 0)
					{
						__vset(dst, verts.Array[verts.Offset].Position.X, verts.Array[verts.Offset].Position.Y, u0, 1);
						dst++;
						__vset(dst, verts.Array[verts.Offset + 1].Position.X, verts.Array[verts.Offset + 1].Position.Y,
							u1, 1);
						dst++;
					}
					else
					{
						dx = p1->X - p0->X;
						dy = p1->Y - p0->Y;
						NvgUtility.__normalize(&dx, &dy);
						if (lineCap == NVG_BUTT)
							dst = __buttCapEnd(dst, p1, dx, dy, w, -aa * 0.5f, aa, u0, u1);
						else if (lineCap == NVG_BUTT || lineCap == NVG_SQUARE)
							dst = __buttCapEnd(dst, p1, dx, dy, w, w - aa, aa, u0, u1);
						else if (lineCap == NVG_ROUND)
							dst = __roundCapEnd(dst, p1, dx, dy, w, ncap, aa, u0, u1);
					}

					path.Stroke = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.Stroke.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}
			}

			return 1;
		}

		private int __expandFill(float w, int lineJoin, float miterLimit)
		{
			var cverts = 0;
			var convex = 0;
			var i = 0;
			var j = 0;
			var aa = _fringeWidth;
			var fringe = w > 0.0f ? 1 : 0;
			__calculateJoins(w, lineJoin, miterLimit);
			cverts = 0;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				cverts += path.Count + path.BevelCount + 1;
				if (fringe != 0)
					cverts += (path.Count + path.BevelCount * 5 + 1) * 2;
			}

			var verts = __allocTempVerts(cverts);
			convex = _cache.Paths.Count == 1 && _cache.Paths[0].Convex != 0 ? 1 : 0;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				var pts = &_cache.Points[path.First];
				NvgPoint* p0;
				NvgPoint* p1;
				float rw = 0;
				float lw = 0;
				float woff = 0;
				float ru = 0;
				float lu = 0;
				woff = 0.5f * aa;
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					if (fringe != 0)
					{
						p0 = &pts[path.Count - 1];
						p1 = &pts[0];
						for (j = 0; j < path.Count; ++j)
						{
							if ((p1->flags & NVG_PT_BEVEL) != 0)
							{
								var dlx0 = p0->DeltaY;
								var dly0 = -p0->DeltaX;
								var dlx1 = p1->DeltaY;
								var dly1 = -p1->DeltaX;
								if ((p1->flags & NVG_PT_LEFT) != 0)
								{
									var lx = p1->X + p1->dmx * woff;
									var ly = p1->Y + p1->dmy * woff;
									__vset(dst, lx, ly, 0.5f, 1);
									dst++;
								}
								else
								{
									var lx0 = p1->X + dlx0 * woff;
									var ly0 = p1->Y + dly0 * woff;
									var lx1 = p1->X + dlx1 * woff;
									var ly1 = p1->Y + dly1 * woff;
									__vset(dst, lx0, ly0, 0.5f, 1);
									dst++;
									__vset(dst, lx1, ly1, 0.5f, 1);
									dst++;
								}
							}
							else
							{
								__vset(dst, p1->X + p1->dmx * woff, p1->Y + p1->dmy * woff, 0.5f, 1);
								dst++;
							}

							p0 = p1++;
						}
					}
					else
					{
						for (j = 0; j < path.Count; ++j)
						{
							__vset(dst, pts[j].X, pts[j].Y, 0.5f, 1);
							dst++;
						}
					}

					path.Fill = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.Fill.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}

				if (fringe != 0)
				{
					lw = w + woff;
					rw = w - woff;
					lu = 0;
					ru = 1;
					fixed (Vertex* dst2 = &verts.Array[verts.Offset])
					{
						var dst = dst2;
						if (convex != 0)
						{
							lw = woff;
							lu = 0.5f;
						}

						p0 = &pts[path.Count - 1];
						p1 = &pts[0];
						for (j = 0; j < path.Count; ++j)
						{
							if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
							{
								dst = __bevelJoin(dst, p0, p1, lw, rw, lu, ru, _fringeWidth);
							}
							else
							{
								__vset(dst, p1->X + p1->dmx * lw, p1->Y + p1->dmy * lw, lu, 1);
								dst++;
								__vset(dst, p1->X - p1->dmx * rw, p1->Y - p1->dmy * rw, ru, 1);
								dst++;
							}

							p0 = p1++;
						}

						__vset(dst, verts.Array[verts.Offset].Position.X,
							verts.Array[verts.Offset].Position.Y,
							lu, 1);
						dst++;
						__vset(dst, verts.Array[verts.Offset + 1].Position.X,
							verts.Array[verts.Offset + 1].Position.Y,
							ru, 1);
						dst++;

						path.Stroke = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

						var newPos = verts.Offset + path.Stroke.Value.Count;
						verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
					}
				}
				else
				{
					path.Stroke = null;
				}
			}

			return 1;
		}


		private void __flushTextTexture()
		{
			var dirty = stackalloc int[4];
			if (_fontSyst.ValidateTexture(dirty) != 0)
			{
				var fontImage = _fontImages[_fontImageIdx];
				if (fontImage != 0)
				{
					var iw = 0;
					var ih = 0;
					var data = _fontSyst.GetTextureData(&iw, &ih);
					var x = dirty[0];
					var y = dirty[1];
					var w = dirty[2] - dirty[0];
					var h = dirty[3] - dirty[1];
					_renderer.UpdateTexture(fontImage, x, y, w, h, data);
				}
			}
		}

		private int __allocTextAtlas()
		{
			var iw = 0;
			var ih = 0;
			__flushTextTexture();
			if (_fontImageIdx >= 4 - 1)
				return 0;
			if (_fontImages[_fontImageIdx + 1] != 0)
			{
				ImageSize(_fontImages[_fontImageIdx + 1], out iw, out ih);
			}
			else
			{
				ImageSize(_fontImages[_fontImageIdx], out iw, out ih);
				if (iw > ih)
					ih *= 2;
				else
					iw *= 2;
				if (iw > 2048 || ih > 2048)
					iw = ih = 2048;
				_fontImages[_fontImageIdx + 1] = _renderer.CreateTexture(NVG_TEXTURE_ALPHA, iw, ih, 0, null);
			}

			++_fontImageIdx;
			_fontSyst.ResetAtlas(iw, ih);
			return 1;
		}

		private void __renderText(ArraySegment<Vertex> verts)
		{
			var state = GetState();
			var paint = state.Fill;
			paint.Image = _fontImages[_fontImageIdx];

			MultiplyAlpha(ref paint.InnerColor, state.Alpha);
			MultiplyAlpha(ref paint.OuterColor, state.Alpha);

			_renderer.RenderTriangles(ref paint, ref state.Scissor, verts);
			_drawCallCount++;
			_textTriCount += verts.Count / 3;
		}

		private static float __triarea2(float ax, float ay, float bx, float by, float cx, float cy)
		{
			var abx = bx - ax;
			var aby = by - ay;
			var acx = cx - ax;
			var acy = cy - ay;
			return acx * aby - abx * acy;
		}

		private static float __polyArea(NvgPoint* pts, int npts)
		{
			var i = 0;
			var area = (float)0;
			for (i = 2; i < npts; i++)
			{
				var a = &pts[0];
				var b = &pts[i - 1];
				var c = &pts[i];
				area += __triarea2(a->X, a->Y, b->X, b->Y, c->X, c->Y);
			}

			return area * 0.5f;
		}

		internal static void __polyReverse(NvgPoint* pts, int npts)
		{
			var tmp = new NvgPoint();
			var i = 0;
			var j = npts - 1;
			while (i < j)
			{
				tmp = pts[i];
				pts[i] = pts[j];
				pts[j] = tmp;
				i++;
				j--;
			}
		}

		private static void __vset(Vertex* vtx, float x, float y, float u, float v)
		{
			__vset(ref *vtx, x, y, u, v);
		}

		private static void __vset(ref Vertex vtx, float x, float y, float u, float v)
		{
			vtx.Position.X = x;
			vtx.Position.Y = y;
			vtx.TextureCoordinate.X = u;
			vtx.TextureCoordinate.Y = v;
		}

		private static void __isectRects(float* dst, float ax, float ay, float aw, float ah, float bx, float by,
			float bw, float bh)
		{
			var minx = NvgUtility.__maxf(ax, bx);
			var miny = NvgUtility.__maxf(ay, by);
			var maxx = NvgUtility.__minf(ax + aw, bx + bw);
			var maxy = NvgUtility.__minf(ay + ah, by + bh);
			dst[0] = minx;
			dst[1] = miny;
			dst[2] = NvgUtility.__maxf(0.0f, maxx - minx);
			dst[3] = NvgUtility.__maxf(0.0f, maxy - miny);
		}

		private static float __getAverageScale(ref Transform t)
		{
			var sx = (float)Math.Sqrt(t.T1 * t.T1 + t.T3 * t.T3);
			var sy = (float)Math.Sqrt(t.T2 * t.T2 + t.T4 * t.T4);
			return (sx + sy) * 0.5f;
		}

		private static int __curveDivs(float r, float arc, float tol)
		{
			var da = NvgUtility.acosf(r / (r + tol)) * 2.0f;
			return NvgUtility.__maxi(2, (int)NvgUtility.ceilf(arc / da));
		}

		private static void __chooseBevel(int bevel, NvgPoint* p0, NvgPoint* p1, float w, float* x0, float* y0,
			float* x1, float* y1)
		{
			if (bevel != 0)
			{
				*x0 = p1->X + p0->DeltaY * w;
				*y0 = p1->Y - p0->DeltaX * w;
				*x1 = p1->X + p1->DeltaY * w;
				*y1 = p1->Y - p1->DeltaX * w;
			}
			else
			{
				*x0 = p1->X + p1->dmx * w;
				*y0 = p1->Y + p1->dmy * w;
				*x1 = p1->X + p1->dmx * w;
				*y1 = p1->Y + p1->dmy * w;
			}
		}

		private static float __quantize(float a, float d)
		{
			return (int)(a / d + 0.5f) * d;
		}

		private static float __getFontScale(NvgContextState state)
		{
			return NvgUtility.__minf(__quantize(__getAverageScale(ref state.Transform), 0.01f), 4.0f);
		}

		private static Vertex* __roundJoin(Vertex* dst, NvgPoint* p0, NvgPoint* p1, float lw, float rw, float lu,
			float ru, int ncap, float fringe)
		{
			var i = 0;
			var n = 0;
			var dlx0 = p0->DeltaY;
			var dly0 = -p0->DeltaX;
			var dlx1 = p1->DeltaY;
			var dly1 = -p1->DeltaX;
			if ((p1->flags & NVG_PT_LEFT) != 0)
			{
				float lx0 = 0;
				float ly0 = 0;
				float lx1 = 0;
				float ly1 = 0;
				float a0 = 0;
				float a1 = 0;
				__chooseBevel(p1->flags & NVG_PR_INNERBEVEL, p0, p1, lw, &lx0, &ly0, &lx1, &ly1);
				a0 = NvgUtility.atan2f(-dly0, -dlx0);
				a1 = NvgUtility.atan2f(-dly1, -dlx1);
				if (a1 > a0)
					a1 -= (float)(3.14159274 * 2);
				__vset(dst, lx0, ly0, lu, 1);
				dst++;
				__vset(dst, p1->X - dlx0 * rw, p1->Y - dly0 * rw, ru, 1);
				dst++;
				n = NvgUtility.__clampi((int)NvgUtility.ceilf((float)((a0 - a1) / 3.14159274 * ncap)), 2, ncap);
				for (i = 0; i < n; i++)
				{
					var u = i / (float)(n - 1);
					var a = a0 + u * (a1 - a0);
					var rx = p1->X + NvgUtility.cosf(a) * rw;
					var ry = p1->Y + NvgUtility.sinf(a) * rw;
					__vset(dst, p1->X, p1->Y, 0.5f, 1);
					dst++;
					__vset(dst, rx, ry, ru, 1);
					dst++;
				}

				__vset(dst, lx1, ly1, lu, 1);
				dst++;
				__vset(dst, p1->X - dlx1 * rw, p1->Y - dly1 * rw, ru, 1);
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
				__chooseBevel(p1->flags & NVG_PR_INNERBEVEL, p0, p1, -rw, &rx0, &ry0, &rx1, &ry1);
				a0 = NvgUtility.atan2f(dly0, dlx0);
				a1 = NvgUtility.atan2f(dly1, dlx1);
				if (a1 < a0)
					a1 += (float)(3.14159274 * 2);
				__vset(dst, p1->X + dlx0 * rw, p1->Y + dly0 * rw, lu, 1);
				dst++;
				__vset(dst, rx0, ry0, ru, 1);
				dst++;
				n = NvgUtility.__clampi((int)NvgUtility.ceilf((float)((a1 - a0) / 3.14159274 * ncap)), 2, ncap);
				for (i = 0; i < n; i++)
				{
					var u = i / (float)(n - 1);
					var a = a0 + u * (a1 - a0);
					var lx = p1->X + NvgUtility.cosf(a) * lw;
					var ly = p1->Y + NvgUtility.sinf(a) * lw;
					__vset(dst, lx, ly, lu, 1);
					dst++;
					__vset(dst, p1->X, p1->Y, 0.5f, 1);
					dst++;
				}

				__vset(dst, p1->X + dlx1 * rw, p1->Y + dly1 * rw, lu, 1);
				dst++;
				__vset(dst, rx1, ry1, ru, 1);
				dst++;
			}

			return dst;
		}

		private static Vertex* __bevelJoin(Vertex* dst, NvgPoint* p0, NvgPoint* p1, float lw, float rw, float lu,
			float ru, float fringe)
		{
			float rx0 = 0;
			float ry0 = 0;
			float rx1 = 0;
			float ry1 = 0;
			float lx0 = 0;
			float ly0 = 0;
			float lx1 = 0;
			float ly1 = 0;
			var dlx0 = p0->DeltaY;
			var dly0 = -p0->DeltaX;
			var dlx1 = p1->DeltaY;
			var dly1 = -p1->DeltaX;
			if ((p1->flags & NVG_PT_LEFT) != 0)
			{
				__chooseBevel(p1->flags & NVG_PR_INNERBEVEL, p0, p1, lw, &lx0, &ly0, &lx1, &ly1);
				__vset(dst, lx0, ly0, lu, 1);
				dst++;
				__vset(dst, p1->X - dlx0 * rw, p1->Y - dly0 * rw, ru, 1);
				dst++;
				if ((p1->flags & NVG_PT_BEVEL) != 0)
				{
					__vset(dst, lx0, ly0, lu, 1);
					dst++;
					__vset(dst, p1->X - dlx0 * rw, p1->Y - dly0 * rw, ru, 1);
					dst++;
					__vset(dst, lx1, ly1, lu, 1);
					dst++;
					__vset(dst, p1->X - dlx1 * rw, p1->Y - dly1 * rw, ru, 1);
					dst++;
				}
				else
				{
					rx0 = p1->X - p1->dmx * rw;
					ry0 = p1->Y - p1->dmy * rw;
					__vset(dst, p1->X, p1->Y, 0.5f, 1);
					dst++;
					__vset(dst, p1->X - dlx0 * rw, p1->Y - dly0 * rw, ru, 1);
					dst++;
					__vset(dst, rx0, ry0, ru, 1);
					dst++;
					__vset(dst, rx0, ry0, ru, 1);
					dst++;
					__vset(dst, p1->X, p1->Y, 0.5f, 1);
					dst++;
					__vset(dst, p1->X - dlx1 * rw, p1->Y - dly1 * rw, ru, 1);
					dst++;
				}

				__vset(dst, lx1, ly1, lu, 1);
				dst++;
				__vset(dst, p1->X - dlx1 * rw, p1->Y - dly1 * rw, ru, 1);
				dst++;
			}
			else
			{
				__chooseBevel(p1->flags & NVG_PR_INNERBEVEL, p0, p1, -rw, &rx0, &ry0, &rx1, &ry1);
				__vset(dst, p1->X + dlx0 * lw, p1->Y + dly0 * lw, lu, 1);
				dst++;
				__vset(dst, rx0, ry0, ru, 1);
				dst++;
				if ((p1->flags & NVG_PT_BEVEL) != 0)
				{
					__vset(dst, p1->X + dlx0 * lw, p1->Y + dly0 * lw, lu, 1);
					dst++;
					__vset(dst, rx0, ry0, ru, 1);
					dst++;
					__vset(dst, p1->X + dlx1 * lw, p1->Y + dly1 * lw, lu, 1);
					dst++;
					__vset(dst, rx1, ry1, ru, 1);
					dst++;
				}
				else
				{
					lx0 = p1->X + p1->dmx * lw;
					ly0 = p1->Y + p1->dmy * lw;
					__vset(dst, p1->X + dlx0 * lw, p1->Y + dly0 * lw, lu, 1);
					dst++;
					__vset(dst, p1->X, p1->Y, 0.5f, 1);
					dst++;
					__vset(dst, lx0, ly0, lu, 1);
					dst++;
					__vset(dst, lx0, ly0, lu, 1);
					dst++;
					__vset(dst, p1->X + dlx1 * lw, p1->Y + dly1 * lw, lu, 1);
					dst++;
					__vset(dst, p1->X, p1->Y, 0.5f, 1);
					dst++;
				}

				__vset(dst, p1->X + dlx1 * lw, p1->Y + dly1 * lw, lu, 1);
				dst++;
				__vset(dst, rx1, ry1, ru, 1);
				dst++;
			}

			return dst;
		}

		private static Vertex* __buttCapStart(Vertex* dst, NvgPoint* p, float dx, float dy, float w, float d, float aa,
			float u0, float u1)
		{
			var px = p->X - dx * d;
			var py = p->Y - dy * d;
			var dlx = dy;
			var dly = -dx;
			__vset(dst, px + dlx * w - dx * aa, py + dly * w - dy * aa, u0, 0);
			dst++;
			__vset(dst, px - dlx * w - dx * aa, py - dly * w - dy * aa, u1, 0);
			dst++;
			__vset(dst, px + dlx * w, py + dly * w, u0, 1);
			dst++;
			__vset(dst, px - dlx * w, py - dly * w, u1, 1);
			dst++;
			return dst;
		}

		private static Vertex* __buttCapEnd(Vertex* dst, NvgPoint* p, float dx, float dy, float w, float d, float aa,
			float u0, float u1)
		{
			var px = p->X + dx * d;
			var py = p->Y + dy * d;
			var dlx = dy;
			var dly = -dx;
			__vset(dst, px + dlx * w, py + dly * w, u0, 1);
			dst++;
			__vset(dst, px - dlx * w, py - dly * w, u1, 1);
			dst++;
			__vset(dst, px + dlx * w + dx * aa, py + dly * w + dy * aa, u0, 0);
			dst++;
			__vset(dst, px - dlx * w + dx * aa, py - dly * w + dy * aa, u1, 0);
			dst++;
			return dst;
		}

		private static Vertex* __roundCapStart(Vertex* dst, NvgPoint* p, float dx, float dy, float w, int ncap,
			float aa, float u0, float u1)
		{
			var i = 0;
			var px = p->X;
			var py = p->Y;
			var dlx = dy;
			var dly = -dx;
			for (i = 0; i < ncap; i++)
			{
				var a = (float)(i / (float)(ncap - 1) * 3.14159274);
				var ax = NvgUtility.cosf(a) * w;
				var ay = NvgUtility.sinf(a) * w;
				__vset(dst, px - dlx * ax - dx * ay, py - dly * ax - dy * ay, u0, 1);
				dst++;
				__vset(dst, px, py, 0.5f, 1);
				dst++;
			}

			__vset(dst, px + dlx * w, py + dly * w, u0, 1);
			dst++;
			__vset(dst, px - dlx * w, py - dly * w, u1, 1);
			dst++;
			return dst;
		}

		private static Vertex* __roundCapEnd(Vertex* dst, NvgPoint* p, float dx, float dy, float w, int ncap, float aa,
			float u0, float u1)
		{
			var i = 0;
			var px = p->X;
			var py = p->Y;
			var dlx = dy;
			var dly = -dx;
			__vset(dst, px + dlx * w, py + dly * w, u0, 1);
			dst++;
			__vset(dst, px - dlx * w, py - dly * w, u1, 1);
			dst++;
			for (i = 0; i < ncap; i++)
			{
				var a = (float)(i / (float)(ncap - 1) * 3.14159274);
				var ax = NvgUtility.cosf(a) * w;
				var ay = NvgUtility.sinf(a) * w;
				__vset(dst, px, py, 0.5f, 1);
				dst++;
				__vset(dst, px - dlx * ax + dx * ay, py - dly * ax + dy * ay, u0, 1);
				dst++;
			}

			return dst;
		}

		private static int __ptEquals(float x1, float y1, float x2, float y2, float tol)
		{
			var dx = x2 - x1;
			var dy = y2 - y1;
			return dx * dx + dy * dy < tol * tol ? 1 : 0;
		}

		private static float __distPtSeg(float x, float y, float px, float py, float qx, float qy)
		{
			float pqx = 0;
			float pqy = 0;
			float dx = 0;
			float dy = 0;
			float d = 0;
			float t = 0;
			pqx = qx - px;
			pqy = qy - py;
			dx = x - px;
			dy = y - py;
			d = pqx * pqx + pqy * pqy;
			t = pqx * dx + pqy * dy;
			if (d > 0)
				t /= d;
			if (t < 0)
				t = 0;
			else if (t > 1)
				t = 1;
			dx = px + t * pqx - x;
			dy = py + t * pqy - y;
			return dx * dx + dy * dy;
		}
	}
}