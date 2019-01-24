using Microsoft.Xna.Framework.Graphics;
using StbSharp;
using System;
using Microsoft.Xna.Framework;
using FontStashSharp;

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

		private readonly IRenderer _renderer;
		private int _edgeAntiAlias;
		private float* _commands;
		private int _commandsCount;
		private int _commandsNumber;
		private float _commandX;
		private float _commandY;
		private NanoVGContextState[] _states = new NanoVGContextState[32];
		private int _statesNumber;
		private PathCache _cache;
		private float _tessTol;
		private float _distTol;
		private float _fringeWidth;
		private float _devicePxRatio;
		private FontSystem _fontSyst;
		private readonly int[] _fontImages = new int[4];
		private int _fontImageIdx;
		private int _drawCallCount;
		private int _fillTriCount;
		private int _strokeTriCount;
		private int _textTriCount;
		private readonly TextRow[] _rows = new TextRow[MaxTextRows];

		public NanoVGContext(GraphicsDevice device, int edgeAntiAlias)
		{
			_renderer = new XNARenderer(device);

			FontSystemParams fontParams = new FontSystemParams();

			this._edgeAntiAlias = edgeAntiAlias;
			for (var i = 0; i < 4; i++)
			{
				_fontImages[i] = 0;
			}
			_commands = (float*)(CRuntime.malloc((ulong)(sizeof(float) * 256)));
			_commandsNumber = (int)(0);
			_commandsCount = (int)(256);
			_cache = new PathCache();
			Save();
			Reset();
			__setDevicePixelRatio((float)(1.0f));
			fontParams.Width = (int)(512);
			fontParams.Height = (int)(512);
			fontParams.Flags = (byte)(FontSystem.FONS_ZERO_TOPLEFT);
			_fontSyst = new FontSystem(fontParams);
			_fontImages[0] = (int)(_renderer.CreateTexture((int)(NVG_TEXTURE_ALPHA), (int)(fontParams.Width), (int)(fontParams.Height), (int)(0), null));
			_fontImageIdx = (int)(0);

			for (var i = 0; i < _rows.Length; ++i)
			{
				_rows[i] = new TextRow();
			}
		}

		public void Dispose()
		{
			int i = 0;
			if (_commands != null)
			{
				CRuntime.free(_commands);
				_commands = null;
			}

			if ((_fontSyst) != null)
			{
				_fontSyst.Dispose();
				_fontSyst = null;
			}

			for (i = (int)(0); (i) < (4); i++)
			{
				if (_fontImages[i] != 0)
				{
					DeleteImage((int)(_fontImages[i]));
					_fontImages[i] = (int)(0);
				}
			}
		}

		public void __setDevicePixelRatio(float ratio)
		{
			_tessTol = (float)(0.25f / ratio);
			_distTol = (float)(0.01f / ratio);
			_fringeWidth = (float)(1.0f / ratio);
			_devicePxRatio = (float)(ratio);
		}

		private NanoVGContextState GetState()
		{
			return _states[_statesNumber - 1];
		}

		public void BeginFrame(float windowWidth, float windowHeight, float devicePixelRatio)
		{
			_statesNumber = (int)(0);
			Save();
			Reset();
			__setDevicePixelRatio((float)(devicePixelRatio));

			_renderer.Begin();

			_renderer.Viewport((float)(windowWidth), (float)(windowHeight), (float)(devicePixelRatio));
			_drawCallCount = (int)(0);
			_fillTriCount = (int)(0);
			_strokeTriCount = (int)(0);
			_textTriCount = (int)(0);
		}

		public void EndFrame()
		{
			if (_fontImageIdx != 0)
			{
				int fontImage = (int)(_fontImages[_fontImageIdx]);
				int i = 0;
				int j = 0;
				int iw = 0;
				int ih = 0;
				if ((fontImage) == (0))
					return;
				ImageSize((int)(fontImage), out iw, out ih);
				for (i = (int)(j = (int)(0)); (i) < (_fontImageIdx); i++)
				{
					if (_fontImages[i] != 0)
					{
						int nw = 0;
						int nh = 0;
						ImageSize((int)(_fontImages[i]), out nw, out nh);
						if (((nw) < (iw)) || ((nh) < (ih)))
							DeleteImage((int)(_fontImages[i]));
						else
							_fontImages[j++] = (int)(_fontImages[i]);
					}
				}
				_fontImages[j++] = (int)(_fontImages[0]);
				_fontImages[0] = (int)(fontImage);
				_fontImageIdx = (int)(0);
				for (i = (int)(j); (i) < (4); i++)
				{
					_fontImages[i] = (int)(0);
				}
			}

			_renderer.End();
		}

		public void Save()
		{
			if ((_statesNumber) >= (32))
				return;
			if ((_statesNumber) > (0))
			{
				_states[_statesNumber] = _states[_statesNumber - 1].Clone();
			}
			else
			{
				_states[_statesNumber] = new NanoVGContextState();
			}
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
			NanoVGContextState state = GetState();
			__setPaintColor(ref state.Fill, Color.White);
			__setPaintColor(ref state.Stroke, Color.Black);
			state.ShapeAntiAlias = (int)(1);
			state.StrokeWidth = (float)(1.0f);
			state.MiterLimit = (float)(10.0f);
			state.LineCap = (int)(NVG_BUTT);
			state.LineJoin = (int)(NVG_MITER);
			state.Alpha = (float)(1.0f);
			state.Transform.SetIdentity();
			state.Scissor.Extent.X = (float)(-1.0f);
			state.Scissor.Extent.Y = (float)(-1.0f);
			state.FontSize = (float)(16.0f);
			state.LetterSpacing = (float)(0.0f);
			state.LineHeight = (float)(1.0f);
			state.FontBlur = (float)(0.0f);
			state.TextAlign = (int)(NVG_ALIGN_LEFT | NVG_ALIGN_BASELINE);
			state.FontId = (int)(0);
		}

		public void ShapeAntiAlias(int enabled)
		{
			NanoVGContextState state = GetState();
			state.ShapeAntiAlias = (int)(enabled);
		}

		public void StrokeWidth(float width)
		{
			NanoVGContextState state = GetState();
			state.StrokeWidth = (float)(width);
		}

		public void MiterLimit(float limit)
		{
			NanoVGContextState state = GetState();
			state.MiterLimit = (float)(limit);
		}

		public void LineCap(int cap)
		{
			NanoVGContextState state = GetState();
			state.LineCap = (int)(cap);
		}

		public void LineJoin(int join)
		{
			NanoVGContextState state = GetState();
			state.LineJoin = (int)(join);
		}

		public void GlobalAlpha(float alpha)
		{
			NanoVGContextState state = GetState();
			state.Alpha = (float)(alpha);
		}

		public void Transform(float a, float b, float c, float d, float e, float f)
		{
			NanoVGContextState state = GetState();
			Transform t;
			t.T1 = (float)(a);
			t.T2 = (float)(b);
			t.T3 = (float)(c);
			t.T4 = (float)(d);
			t.T5 = (float)(e);
			t.T6 = (float)(f);

			state.Transform.Premultiply(ref t);
		}

		public void ResetTransform()
		{
			NanoVGContextState state = GetState();
			state.Transform.SetIdentity();
		}

		public void Translate(float x, float y)
		{
			NanoVGContextState state = GetState();
			var t = new Transform();
			t.SetTranslate((float)(x), (float)(y));
			state.Transform.Premultiply(ref t);
		}

		public void Rotate(float angle)
		{
			NanoVGContextState state = GetState();
			var t = new Transform();
			t.SetRotate((float)(angle));
			state.Transform.Premultiply(ref t);
		}

		public void SkewX(float angle)
		{
			NanoVGContextState state = GetState();
			var t = new Transform();
			t.SetSkewX((float)(angle));
			state.Transform.Premultiply(ref t);
		}

		public void SkewY(float angle)
		{
			NanoVGContextState state = GetState();
			var t = new Transform();
			t.SetSkewY((float)(angle));
			state.Transform.Premultiply(ref t);
		}

		public void Scale(float x, float y)
		{
			NanoVGContextState state = GetState();
			var t = new Transform();
			t.SetScale((float)(x), (float)(y));
			state.Transform.Premultiply(ref t);
		}

		public void CurrentTransform(Transform xform)
		{
			NanoVGContextState state = GetState();

			state.Transform = xform;
		}

		public void StrokeColor(Color color)
		{
			NanoVGContextState state = GetState();
			__setPaintColor(ref state.Stroke, (Color)(color));
		}

		public void StrokePaint(Paint paint)
		{
			NanoVGContextState state = GetState();
			state.Stroke = paint;
			state.Stroke.Transform.Multiply(ref state.Transform);
		}

		public void FillColor(Color color)
		{
			NanoVGContextState state = GetState();
			__setPaintColor(ref state.Fill, (Color)(color));
		}

		public void FillPaint(Paint paint)
		{
			NanoVGContextState state = GetState();
			state.Fill = (Paint)(paint);
			state.Fill.Transform.Multiply(ref state.Transform);
		}

		public int CreateImageRGBA(int w, int h, int imageFlags, byte[] data)
		{
			return (int)(_renderer.CreateTexture((int)(NVG_TEXTURE_RGBA), (int)(w), (int)(h), (int)(imageFlags), data));
		}

		public void UpdateImage(int image, byte[] data)
		{
			int w = 0;
			int h = 0;
			_renderer.GetTextureSize((int)(image), out w, out h);
			_renderer.UpdateTexture((int)(image), (int)(0), (int)(0), (int)(w), (int)(h), data);
		}

		public void ImageSize(int image, out int w, out int h)
		{
			_renderer.GetTextureSize((int)(image), out w, out h);
		}

		public void DeleteImage(int image)
		{
			_renderer.DeleteTexture((int)(image));
		}

		public Paint LinearGradient(float sx, float sy, float ex, float ey, Color icol, Color ocol)
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

			p.Transform.T1 = (float)(dy);
			p.Transform.T2 = (float)(-dx);
			p.Transform.T3 = (float)(dx);
			p.Transform.T4 = (float)(dy);
			p.Transform.T5 = (float)(sx - dx * large);
			p.Transform.T6 = (float)(sy - dy * large);
			p.Extent.X = (float)(large);
			p.Extent.Y = (float)(large + d * 0.5f);
			p.Radius = (float)(0.0f);
			p.Feather = (float)(__maxf((float)(1.0f), (float)(d)));
			p.InnerColor = (Color)(icol);
			p.OuterColor = (Color)(ocol);
			return (Paint)(p);
		}

		public Paint RadialGradient(float cx, float cy, float inr, float outr, Color icol, Color ocol)
		{
			Paint p = new Paint();
			float r = (float)((inr + outr) * 0.5f);
			float f = (float)(outr - inr);
			p.Transform.SetIdentity();
			p.Transform.T5 = (float)(cx);
			p.Transform.T6 = (float)(cy);
			p.Extent.X = (float)(r);
			p.Extent.Y = (float)(r);
			p.Radius = (float)(r);
			p.Feather = (float)(__maxf((float)(1.0f), (float)(f)));
			p.InnerColor = (Color)(icol);
			p.OuterColor = (Color)(ocol);
			return (Paint)(p);
		}

		public Paint BoxGradient(float x, float y, float w, float h, float r, float f, Color icol, Color ocol)
		{
			Paint p = new Paint();
			p.Transform.SetIdentity();
			p.Transform.T5 = (float)(x + w * 0.5f);
			p.Transform.T6 = (float)(y + h * 0.5f);
			p.Extent.X = (float)(w * 0.5f);
			p.Extent.Y = (float)(h * 0.5f);
			p.Radius = (float)(r);
			p.Feather = (float)(__maxf((float)(1.0f), (float)(f)));
			p.InnerColor = (Color)(icol);
			p.OuterColor = (Color)(ocol);
			return (Paint)(p);
		}

		public Paint ImagePattern(float cx, float cy, float w, float h, float angle, int image, float alpha)
		{
			Paint p = new Paint();
			p.Transform.SetRotate((float)(angle));
			p.Transform.T5 = (float)(cx);
			p.Transform.T6 = (float)(cy);
			p.Extent.X = (float)(w);
			p.Extent.Y = (float)(h);
			p.Image = (int)(image);
			p.InnerColor = p.OuterColor = new Color(1.0f, 1.0f, 1.0f, alpha);
			return (Paint)(p);
		}

		public void Scissor(float x, float y, float w, float h)
		{
			NanoVGContextState state = GetState();
			w = (float)(__maxf((float)(0.0f), (float)(w)));
			h = (float)(__maxf((float)(0.0f), (float)(h)));
			state.Scissor.Transform.SetIdentity();
			state.Scissor.Transform.T5 = (float)(x + w * 0.5f);
			state.Scissor.Transform.T6 = (float)(y + h * 0.5f);
			state.Scissor.Transform.Multiply(ref state.Transform);
			state.Scissor.Extent.X = (float)(w * 0.5f);
			state.Scissor.Extent.Y = (float)(h * 0.5f);
		}

		public void IntersectScissor(float x, float y, float w, float h)
		{
			NanoVGContextState state = GetState();
			var pxform = new Transform();
			var invxorm = new Transform();
			float* rect = stackalloc float[4];
			float ex = 0;
			float ey = 0;
			float tex = 0;
			float tey = 0;
			if ((state.Scissor.Extent.X) < (0))
			{
				Scissor((float)(x), (float)(y), (float)(w), (float)(h));
				return;
			}

			pxform = state.Scissor.Transform;
			ex = (float)(state.Scissor.Extent.X);
			ey = (float)(state.Scissor.Extent.Y);
			invxorm = state.Transform.BuildInverse();
			pxform.Multiply(ref invxorm);
			tex = (float)(ex * __absf((float)(pxform.T1)) + ey * __absf((float)(pxform.T3)));
			tey = (float)(ex * __absf((float)(pxform.T2)) + ey * __absf((float)(pxform.T4)));
			__isectRects(rect, (float)(pxform.T5 - tex), (float)(pxform.T6 - tey), (float)(tex * 2), (float)(tey * 2), (float)(x), (float)(y), (float)(w), (float)(h));
			Scissor((float)(rect[0]), (float)(rect[1]), (float)(rect[2]), (float)(rect[3]));
		}

		public void ResetScissor()
		{
			NanoVGContextState state = GetState();
			state.Scissor.Transform.Zero();
			state.Scissor.Extent.X = (float)(-1.0f);
			state.Scissor.Extent.Y = (float)(-1.0f);
		}

		public void __appendCommands(float* vals, int nvals)
		{
			NanoVGContextState state = GetState();
			int i = 0;
			if ((_commandsNumber + nvals) > (_commandsCount))
			{
				float* commands;
				int ccommands = (int)(_commandsNumber + nvals + this._commandsCount / 2);
				commands = (float*)(CRuntime.realloc(this._commands, (ulong)(sizeof(float) * ccommands)));
				if ((commands) == null)
					return;
				this._commands = commands;
				this._commandsCount = (int)(ccommands);
			}

			if (((int)(vals[0]) != NVG_CLOSE) && ((int)(vals[0]) != NVG_WINDING))
			{
				_commandX = (float)(vals[nvals - 2]);
				_commandY = (float)(vals[nvals - 1]);
			}

			i = (int)(0);
			while ((i) < (nvals))
			{
				int cmd = (int)(vals[i]);
				switch (cmd)
				{
					case NVG_MOVETO:
						state.Transform.TransformPoint(out vals[i + 1], out vals[i + 2], (float)(vals[i + 1]), (float)(vals[i + 2]));
						i += (int)(3);
						break;
					case NVG_LINETO:
						state.Transform.TransformPoint(out vals[i + 1], out vals[i + 2], (float)(vals[i + 1]), (float)(vals[i + 2]));
						i += (int)(3);
						break;
					case NVG_BEZIERTO:
						state.Transform.TransformPoint(out vals[i + 1], out vals[i + 2], (float)(vals[i + 1]), (float)(vals[i + 2]));
						state.Transform.TransformPoint(out vals[i + 3], out vals[i + 4], (float)(vals[i + 3]), (float)(vals[i + 4]));
						state.Transform.TransformPoint(out vals[i + 5], out vals[i + 6], (float)(vals[i + 5]), (float)(vals[i + 6]));
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
			CRuntime.memcpy(&_commands[_commandsNumber], vals, (ulong)(nvals * sizeof(float)));
			_commandsNumber += (int)(nvals);
		}

		public void __clearPathCache()
		{
			_cache.Paths.Clear();
			_cache.PointsNumber = 0;
		}

		private Path __lastPath()
		{
			if ((_cache.Paths.Count) > (0))
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
			if ((_cache.PointsNumber) > (0))
				return &_cache.Points[_cache.PointsNumber - 1];
			return null;
		}

		public void __addPoint(float x, float y, int flags)
		{
			Path path = __lastPath();
			NvgPoint* pt;
			if ((path) == null)
				return;
			if (((path.Count) > (0)) && ((_cache.PointsNumber) > (0)))
			{
				pt = __lastPoint();
				if ((__ptEquals((float)(pt->X), (float)(pt->Y), (float)(x), (float)(y), (float)(_distTol))) != 0)
				{
					pt->flags |= (byte)(flags);
					return;
				}
			}

			if ((_cache.PointsNumber + 1) > (_cache.PointsCount))
			{
				NvgPoint* points;
				int cpoints = (int)(_cache.PointsNumber + 1 + _cache.PointsCount / 2);
				points = (NvgPoint*)(CRuntime.realloc(_cache.Points, (ulong)(sizeof(NvgPoint) * cpoints)));
				if ((points) == null)
					return;
				_cache.Points = points;
				_cache.PointsCount = (int)(cpoints);
			}

			pt = &_cache.Points[_cache.PointsNumber];
			pt->Reset();
			pt->X = (float)(x);
			pt->Y = (float)(y);
			pt->flags = ((byte)(flags));
			_cache.PointsNumber++;
			path.Count++;
		}

		public void __closePath()
		{
			Path path = __lastPath();
			if ((path) == null)
				return;
			path.Closed = (byte)(1);
		}

		public void __pathWinding(int winding)
		{
			Path path = __lastPath();
			if ((path) == null)
				return;
			path.Winding = (int)(winding);
		}

		private ArraySegment<Vertex> __allocTempVerts(int nverts)
		{
			_cache.Vertexes.EnsureSize(nverts);

			return new ArraySegment<Vertex>(_cache.Vertexes.Array);
		}

		public void __tesselateBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, int level, int type)
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
			d2 = (float)(__absf((float)((x2 - x4) * dy - (y2 - y4) * dx)));
			d3 = (float)(__absf((float)((x3 - x4) * dy - (y3 - y4) * dx)));
			if (((d2 + d3) * (d2 + d3)) < (_tessTol * (dx * dx + dy * dy)))
			{
				__addPoint((float)(x4), (float)(y4), (int)(type));
				return;
			}

			x234 = (float)((x23 + x34) * 0.5f);
			y234 = (float)((y23 + y34) * 0.5f);
			x1234 = (float)((x123 + x234) * 0.5f);
			y1234 = (float)((y123 + y234) * 0.5f);
			__tesselateBezier((float)(x1), (float)(y1), (float)(x12), (float)(y12), (float)(x123), (float)(y123), (float)(x1234), (float)(y1234), (int)(level + 1), (int)(0));
			__tesselateBezier((float)(x1234), (float)(y1234), (float)(x234), (float)(y234), (float)(x34), (float)(y34), (float)(x4), (float)(y4), (int)(level + 1), (int)(type));
		}

		public void __flattenPaths()
		{
			NvgPoint* last;
			NvgPoint* p0;
			NvgPoint* p1;
			NvgPoint* pts;
			Path path;
			int i = 0;
			int j = 0;
			float* cp1;
			float* cp2;
			float* p;
			float area = 0;
			if ((_cache.Paths.Count) > (0))
				return;
			i = (int)(0);
			while ((i) < (_commandsNumber))
			{
				int cmd = (int)(_commands[i]);
				switch (cmd)
				{
					case NVG_MOVETO:
						__addPath();
						p = &_commands[i + 1];
						__addPoint((float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER));
						i += (int)(3);
						break;
					case NVG_LINETO:
						p = &_commands[i + 1];
						__addPoint((float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER));
						i += (int)(3);
						break;
					case NVG_BEZIERTO:
						last = __lastPoint();
						if (last != null)
						{
							cp1 = &_commands[i + 1];
							cp2 = &_commands[i + 3];
							p = &_commands[i + 5];
							__tesselateBezier((float)(last->X), (float)(last->Y), (float)(cp1[0]), (float)(cp1[1]), (float)(cp2[0]), (float)(cp2[1]), (float)(p[0]), (float)(p[1]), (int)(0), (int)(NVG_PT_CORNER));
						}
						i += (int)(7);
						break;
					case NVG_CLOSE:
						__closePath();
						i++;
						break;
					case NVG_WINDING:
						__pathWinding((int)(_commands[i + 1]));
						i += (int)(2);
						break;
					default:
						i++;
						break;
				}
			}
			_cache.Bounds.b1 = (float)(_cache.Bounds.b2 = (float)(1e6f));
			_cache.Bounds.b3 = (float)(_cache.Bounds.b4 = (float)(-1e6f));
			for (j = (int)(0); (j) < (_cache.Paths.Count); j++)
			{
				path = _cache.Paths[j];
				pts = &_cache.Points[path.First];
				p0 = &pts[path.Count - 1];
				p1 = &pts[0];
				if ((__ptEquals((float)(p0->X), (float)(p0->Y), (float)(p1->X), (float)(p1->Y), (float)(_distTol))) != 0)
				{
					path.Count--;
					p0 = &pts[path.Count - 1];
					path.Closed = (byte)(1);
				}
				if ((path.Count) > (2))
				{
					area = (float)(__polyArea(pts, (int)(path.Count)));
					if (((path.Winding) == (NVG_CCW)) && ((area) < (0.0f)))
						__polyReverse(pts, (int)(path.Count));
					if (((path.Winding) == (NVG_CW)) && ((area) > (0.0f)))
						__polyReverse(pts, (int)(path.Count));
				}
				for (i = (int)(0); (i) < (path.Count); i++)
				{
					p0->DeltaX = (float)(p1->X - p0->X);
					p0->DeltaY = (float)(p1->Y - p0->Y);
					p0->Length = (float)(__normalize(&p0->DeltaX, &p0->DeltaY));
					_cache.Bounds.b1 = (float)(__minf((float)(_cache.Bounds.b1), (float)(p0->X)));
					_cache.Bounds.b2 = (float)(__minf((float)(_cache.Bounds.b2), (float)(p0->Y)));
					_cache.Bounds.b3 = (float)(__maxf((float)(_cache.Bounds.b3), (float)(p0->X)));
					_cache.Bounds.b4 = (float)(__maxf((float)(_cache.Bounds.b4), (float)(p0->Y)));
					p0 = p1++;
				}
			}
		}

		public void __calculateJoins(float w, int lineJoin, float miterLimit)
		{
			int i = 0;
			int j = 0;
			float iw = (float)(0.0f);
			if ((w) > (0.0f))
				iw = (float)(1.0f / w);
			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				Path path = _cache.Paths[i];
				NvgPoint* pts = &_cache.Points[path.First];
				NvgPoint* p0 = &pts[path.Count - 1];
				NvgPoint* p1 = &pts[0];
				int nleft = (int)(0);
				path.BevelCount = (int)(0);
				for (j = (int)(0); (j) < (path.Count); j++)
				{
					float dlx0 = 0;
					float dly0 = 0;
					float dlx1 = 0;
					float dly1 = 0;
					float dmr2 = 0;
					float cross = 0;
					float limit = 0;
					dlx0 = (float)(p0->DeltaY);
					dly0 = (float)(-p0->DeltaX);
					dlx1 = (float)(p1->DeltaY);
					dly1 = (float)(-p1->DeltaX);
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
					cross = (float)(p1->DeltaX * p0->DeltaY - p0->DeltaX * p1->DeltaY);
					if ((cross) > (0.0f))
					{
						nleft++;
						p1->flags |= (byte)(NVG_PT_LEFT);
					}
					limit = (float)(__maxf((float)(1.01f), (float)(__minf((float)(p0->Length), (float)(p1->Length)) * iw)));
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
						path.BevelCount++;
					p0 = p1++;
				}
				path.Convex = (int)(((nleft) == (path.Count)) ? 1 : 0);
			}
		}

		public int __expandStroke(float w, float fringe, int lineCap, int lineJoin, float miterLimit)
		{
			int cverts = 0;
			int i = 0;
			int j = 0;
			float aa = (float)(fringe);
			float u0 = (float)(0.0f);
			float u1 = (float)(1.0f);
			int ncap = (int)(__curveDivs((float)(w), (float)(3.14159274), (float)(_tessTol)));
			w += (float)(aa * 0.5f);
			if ((aa) == (0.0f))
			{
				u0 = (float)(0.5f);
				u1 = (float)(0.5f);
			}

			__calculateJoins((float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				Path path = _cache.Paths[i];
				int loop = (int)(((path.Closed) == (0)) ? 0 : 1);
				if ((lineJoin) == (NVG_ROUND))
					cverts += (int)((path.Count + path.BevelCount * (ncap + 2) + 1) * 2);
				else
					cverts += (int)((path.Count + path.BevelCount * 5 + 1) * 2);
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
			var verts = __allocTempVerts((int)(cverts));
			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				Path path = _cache.Paths[i];
				NvgPoint* pts = &_cache.Points[path.First];
				NvgPoint* p0;
				NvgPoint* p1;
				int s = 0;
				int e = 0;
				int loop = 0;
				float dx = 0;
				float dy = 0;
				path.Fill = null;
				loop = (int)(((path.Closed) == (0)) ? 0 : 1);
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					if ((loop) != 0)
					{
						p0 = &pts[path.Count - 1];
						p1 = &pts[0];
						s = (int)(0);
						e = (int)(path.Count);
					}
					else
					{
						p0 = &pts[0];
						p1 = &pts[1];
						s = (int)(1);
						e = (int)(path.Count - 1);
					}
					if ((loop) == (0))
					{
						dx = (float)(p1->X - p0->X);
						dy = (float)(p1->Y - p0->Y);
						__normalize(&dx, &dy);
						if ((lineCap) == (NVG_BUTT))
							dst = __buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1));
						else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE)))
							dst = __buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1));
						else if ((lineCap) == (NVG_ROUND))
							dst = __roundCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1));
					}
					for (j = (int)(s); (j) < (e); ++j)
					{
						if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
						{
							if ((lineJoin) == (NVG_ROUND))
							{
								dst = __roundJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (int)(ncap), (float)(aa));
							}
							else
							{
								dst = __bevelJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (float)(aa));
							}
						}
						else
						{
							__vset(dst, (float)(p1->X + (p1->dmx * w)), (float)(p1->Y + (p1->dmy * w)), (float)(u0), (float)(1));
							dst++;
							__vset(dst, (float)(p1->X - (p1->dmx * w)), (float)(p1->Y - (p1->dmy * w)), (float)(u1), (float)(1));
							dst++;
						}
						p0 = p1++;
					}
					if ((loop) != 0)
					{
						__vset(dst, (float)(verts.Array[verts.Offset].Position.X), (float)(verts.Array[verts.Offset].Position.Y), (float)(u0), (float)(1));
						dst++;
						__vset(dst, (float)(verts.Array[verts.Offset + 1].Position.X), (float)(verts.Array[verts.Offset + 1].Position.Y), (float)(u1), (float)(1));
						dst++;
					}
					else
					{
						dx = (float)(p1->X - p0->X);
						dy = (float)(p1->Y - p0->Y);
						__normalize(&dx, &dy);
						if ((lineCap) == (NVG_BUTT))
							dst = __buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1));
						else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE)))
							dst = __buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1));
						else if ((lineCap) == (NVG_ROUND))
							dst = __roundCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1));
					}

					path.Stroke = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.Stroke.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}
			}
			return (int)(1);
		}

		public int __expandFill(float w, int lineJoin, float miterLimit)
		{
			int cverts = 0;
			int convex = 0;
			int i = 0;
			int j = 0;
			float aa = (float)(_fringeWidth);
			int fringe = (int)((w) > (0.0f) ? 1 : 0);
			__calculateJoins((float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				Path path = _cache.Paths[i];
				cverts += (int)(path.Count + path.BevelCount + 1);
				if ((fringe) != 0)
					cverts += (int)((path.Count + path.BevelCount * 5 + 1) * 2);
			}
			var verts = __allocTempVerts((int)(cverts));
			convex = (int)(((_cache.Paths.Count) == (1)) && ((_cache.Paths[0].Convex) != 0) ? 1 : 0);
			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				Path path = _cache.Paths[i];
				NvgPoint* pts = &_cache.Points[path.First];
				NvgPoint* p0;
				NvgPoint* p1;
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
						p0 = &pts[path.Count - 1];
						p1 = &pts[0];
						for (j = (int)(0); (j) < (path.Count); ++j)
						{
							if ((p1->flags & NVG_PT_BEVEL) != 0)
							{
								float dlx0 = (float)(p0->DeltaY);
								float dly0 = (float)(-p0->DeltaX);
								float dlx1 = (float)(p1->DeltaY);
								float dly1 = (float)(-p1->DeltaX);
								if ((p1->flags & NVG_PT_LEFT) != 0)
								{
									float lx = (float)(p1->X + p1->dmx * woff);
									float ly = (float)(p1->Y + p1->dmy * woff);
									__vset(dst, (float)(lx), (float)(ly), (float)(0.5f), (float)(1));
									dst++;
								}
								else
								{
									float lx0 = (float)(p1->X + dlx0 * woff);
									float ly0 = (float)(p1->Y + dly0 * woff);
									float lx1 = (float)(p1->X + dlx1 * woff);
									float ly1 = (float)(p1->Y + dly1 * woff);
									__vset(dst, (float)(lx0), (float)(ly0), (float)(0.5f), (float)(1));
									dst++;
									__vset(dst, (float)(lx1), (float)(ly1), (float)(0.5f), (float)(1));
									dst++;
								}
							}
							else
							{
								__vset(dst, (float)(p1->X + (p1->dmx * woff)), (float)(p1->Y + (p1->dmy * woff)), (float)(0.5f), (float)(1));
								dst++;
							}
							p0 = p1++;
						}
					}
					else
					{
						for (j = (int)(0); (j) < (path.Count); ++j)
						{
							__vset(dst, (float)(pts[j].X), (float)(pts[j].Y), (float)(0.5f), (float)(1));
							dst++;
						}
					}

					path.Fill = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.Fill.Value.Count;
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
						p0 = &pts[path.Count - 1];
						p1 = &pts[0];
						for (j = (int)(0); (j) < (path.Count); ++j)
						{
							if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0)
							{
								dst = __bevelJoin(dst, p0, p1, (float)(lw), (float)(rw), (float)(lu), (float)(ru), (float)(_fringeWidth));
							}
							else
							{
								__vset(dst, (float)(p1->X + (p1->dmx * lw)), (float)(p1->Y + (p1->dmy * lw)), (float)(lu), (float)(1));
								dst++;
								__vset(dst, (float)(p1->X - (p1->dmx * rw)), (float)(p1->Y - (p1->dmy * rw)), (float)(ru), (float)(1));
								dst++;
							}
							p0 = p1++;
						}
						__vset(dst, (float)(verts.Array[verts.Offset].Position.X),
							(float)(verts.Array[verts.Offset].Position.Y),
							(float)(lu), (float)(1));
						dst++;
						__vset(dst, (float)(verts.Array[verts.Offset + 1].Position.X),
							(float)(verts.Array[verts.Offset + 1].Position.Y),
							(float)(ru), (float)(1));
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

			return (int)(1);
		}

		public void BeginPath()
		{
			_commandsNumber = (int)(0);
			__clearPathCache();
		}

		public void MoveTo(float x, float y)
		{
			float* vals = stackalloc float[3];
			vals[0] = (float)(NVG_MOVETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);

			__appendCommands(vals, 3);
		}

		public void LineTo(float x, float y)
		{
			float* vals = stackalloc float[3];
			vals[0] = (float)(NVG_LINETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);

			__appendCommands(vals, 3);
		}

		public void BezierTo(float c1x, float c1y, float c2x, float c2y, float x, float y)
		{
			float* vals = stackalloc float[7];
			vals[0] = (float)(NVG_BEZIERTO);
			vals[1] = (float)(c1x);
			vals[2] = (float)(c1y);
			vals[3] = (float)(c2x);
			vals[4] = (float)(c2y);
			vals[5] = (float)(x);
			vals[6] = (float)(y);

			__appendCommands(vals, 7);
		}

		public void QuadTo(float cx, float cy, float x, float y)
		{
			float x0 = (float)(_commandX);
			float y0 = (float)(_commandY);
			float* vals = stackalloc float[7];
			vals[0] = (float)(NVG_BEZIERTO);
			vals[1] = (float)(x0 + 2.0f / 3.0f * (cx - x0));
			vals[2] = (float)(y0 + 2.0f / 3.0f * (cy - y0));
			vals[3] = (float)(x + 2.0f / 3.0f * (cx - x));
			vals[4] = (float)(y + 2.0f / 3.0f * (cy - y));
			vals[5] = (float)(x);
			vals[6] = (float)(y);

			__appendCommands(vals, 7);
		}

		public void ArcTo(float x1, float y1, float x2, float y2, float radius)
		{
			float x0 = (float)(_commandX);
			float y0 = (float)(_commandY);
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
			if ((_commandsNumber) == (0))
			{
				return;
			}

			if (((((__ptEquals((float)(x0), (float)(y0), (float)(x1), (float)(y1), (float)(_distTol))) != 0) || ((__ptEquals((float)(x1), (float)(y1), (float)(x2), (float)(y2), (float)(_distTol))) != 0)) || ((__distPtSeg((float)(x1), (float)(y1), (float)(x0), (float)(y0), (float)(x2), (float)(y2))) < (_distTol * _distTol))) || ((radius) < (_distTol)))
			{
				LineTo((float)(x1), (float)(y1));
				return;
			}

			dx0 = (float)(x0 - x1);
			dy0 = (float)(y0 - y1);
			dx1 = (float)(x2 - x1);
			dy1 = (float)(y2 - y1);
			__normalize(&dx0, &dy0);
			__normalize(&dx1, &dy1);
			a = (float)(acosf((float)(dx0 * dx1 + dy0 * dy1)));
			d = (float)(radius / tanf((float)(a / 2.0f)));
			if ((d) > (10000.0f))
			{
				LineTo((float)(x1), (float)(y1));
				return;
			}

			if ((__cross((float)(dx0), (float)(dy0), (float)(dx1), (float)(dy1))) > (0.0f))
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

			Arc((float)(cx), (float)(cy), (float)(radius), (float)(a0), (float)(a1), (int)(dir));
		}

		public void ClosePath()
		{
			float* vals = stackalloc float[1];
			vals[0] = (float)(NVG_CLOSE);

			__appendCommands(vals, 1);
		}

		public void PathWinding(int dir)
		{
			float* vals = stackalloc float[2];
			vals[0] = (float)(NVG_WINDING);
			vals[1] = (float)(dir);

			__appendCommands(vals, 2);
		}

		public void Arc(float cx, float cy, float r, float a0, float a1, int dir)
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
			int move = (int)((_commandsNumber) > (0) ? NVG_LINETO : NVG_MOVETO);
			da = (float)(a1 - a0);
			if ((dir) == (NVG_CW))
			{
				if ((__absf((float)(da))) >= (3.14159274 * 2))
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
				if ((__absf((float)(da))) >= (3.14159274 * 2))
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

			ndivs = (int)(__maxi((int)(1), (int)(__mini((int)(__absf((float)(da)) / (3.14159274 * 0.5f) + 0.5f), (int)(5)))));
			hda = (float)((da / (float)(ndivs)) / 2.0f);
			kappa = (float)(__absf((float)(4.0f / 3.0f * (1.0f - cosf((float)(hda))) / sinf((float)(hda)))));
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
			__appendCommands(vals, (int)(nvals));
		}

		public void Rect(float x, float y, float w, float h)
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

			__appendCommands(vals, 13);
		}

		public void RoundedRect(float x, float y, float w, float h, float r)
		{
			RoundedRectVarying((float)(x), (float)(y), (float)(w), (float)(h), (float)(r), (float)(r), (float)(r), (float)(r));
		}

		public void RoundedRectVarying(float x, float y, float w, float h, float radTopLeft, float radTopRight, float radBottomRight, float radBottomLeft)
		{
			if (((((radTopLeft) < (0.1f)) && ((radTopRight) < (0.1f))) && ((radBottomRight) < (0.1f))) && ((radBottomLeft) < (0.1f)))
			{
				Rect((float)(x), (float)(y), (float)(w), (float)(h));
				return;
			}
			else
			{
				float halfw = (float)(__absf((float)(w)) * 0.5f);
				float halfh = (float)(__absf((float)(h)) * 0.5f);
				float rxBL = (float)(__minf((float)(radBottomLeft), (float)(halfw)) * __signf((float)(w)));
				float ryBL = (float)(__minf((float)(radBottomLeft), (float)(halfh)) * __signf((float)(h)));
				float rxBR = (float)(__minf((float)(radBottomRight), (float)(halfw)) * __signf((float)(w)));
				float ryBR = (float)(__minf((float)(radBottomRight), (float)(halfh)) * __signf((float)(h)));
				float rxTR = (float)(__minf((float)(radTopRight), (float)(halfw)) * __signf((float)(w)));
				float ryTR = (float)(__minf((float)(radTopRight), (float)(halfh)) * __signf((float)(h)));
				float rxTL = (float)(__minf((float)(radTopLeft), (float)(halfw)) * __signf((float)(w)));
				float ryTL = (float)(__minf((float)(radTopLeft), (float)(halfh)) * __signf((float)(h)));
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
				__appendCommands(vals, 44);
			}

		}

		public void Ellipse(float cx, float cy, float rx, float ry)
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

			__appendCommands(vals, 32);
		}

		public void Circle(float cx, float cy, float r)
		{
			Ellipse((float)(cx), (float)(cy), (float)(r), (float)(r));
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
			var na = (int)((float)c.A * alpha);

			c = new Color(c.R, c.G, c.B, na);
		}

		public void Fill()
		{
			NanoVGContextState state = GetState();
			Path path;
			Paint fillPaint = (Paint)(state.Fill);
			int i = 0;
			__flattenPaths();
			if (((_edgeAntiAlias) != 0) && ((state.ShapeAntiAlias) != 0))
				__expandFill((float)(_fringeWidth), (int)(NVG_MITER), (float)(2.4f));
			else
				__expandFill((float)(0.0f), (int)(NVG_MITER), (float)(2.4f));
			MultiplyAlpha(ref fillPaint.InnerColor, state.Alpha);
			MultiplyAlpha(ref fillPaint.OuterColor, state.Alpha);
			_renderer.RenderFill(ref fillPaint, ref state.Scissor, (float)(_fringeWidth), _cache.Bounds, _cache.Paths.ToArraySegment());

			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				path = _cache.Paths[i];
				if (path.Fill != null)
				{
					_fillTriCount += (int)(path.Fill.Value.Count - 2);
				}

				if (path.Stroke != null)
				{
					_fillTriCount += (int)(path.Stroke.Value.Count - 2);
				}
				_drawCallCount += (int)(2);
			}
		}

		public void Stroke()
		{
			NanoVGContextState state = GetState();
			float scale = (float)(__getAverageScale(ref state.Transform));
			float strokeWidth = (float)(__clampf((float)(state.StrokeWidth * scale), (float)(0.0f), (float)(200.0f)));
			Paint strokePaint = (Paint)(state.Stroke);
			Path path;
			int i = 0;
			if ((strokeWidth) < (_fringeWidth))
			{
				float alpha = (float)(__clampf((float)(strokeWidth / _fringeWidth), (float)(0.0f), (float)(1.0f)));

				MultiplyAlpha(ref strokePaint.InnerColor, alpha * alpha);
				MultiplyAlpha(ref strokePaint.OuterColor, alpha * alpha);
				strokeWidth = _fringeWidth;
			}

			MultiplyAlpha(ref strokePaint.InnerColor, state.Alpha);
			MultiplyAlpha(ref strokePaint.OuterColor, state.Alpha);

			__flattenPaths();
			if (((_edgeAntiAlias) != 0) && ((state.ShapeAntiAlias) != 0))
				__expandStroke((float)(strokeWidth * 0.5f), (float)(_fringeWidth), (int)(state.LineCap), (int)(state.LineJoin), (float)(state.MiterLimit));
			else
				__expandStroke((float)(strokeWidth * 0.5f), (float)(0.0f), (int)(state.LineCap), (int)(state.LineJoin), (float)(state.MiterLimit));
			_renderer.RenderStroke(ref strokePaint, ref state.Scissor, 
				(float)(_fringeWidth), (float)(strokeWidth), _cache.Paths.ToArraySegment());
			for (i = (int)(0); (i) < (_cache.Paths.Count); i++)
			{
				path = _cache.Paths[i];
				_strokeTriCount += (int)(path.Stroke.Value.Count - 2);
				_drawCallCount++;
			}
		}

		public int CreateFontMem(string name, byte[] data)
		{
			return (int)(_fontSyst.AddFontMem(name, data));
		}

		public int FindFont(string name)
		{
			if ((name) == null)
				return (int)(-1);
			return (int)(_fontSyst.GetFontByName(name));
		}

		public int AddFallbackFontId(int baseFont, int fallbackFont)
		{
			if (((baseFont) == (-1)) || ((fallbackFont) == (-1)))
				return (int)(0);
			return (int)(_fontSyst.AddFallbackFont((int)(baseFont), (int)(fallbackFont)));
		}

		public int AddFallbackFont(string baseFont, string fallbackFont)
		{
			return (int)(AddFallbackFontId((int)(FindFont(baseFont)), (int)(FindFont(fallbackFont))));
		}

		public void FontSize(float size)
		{
			NanoVGContextState state = GetState();
			state.FontSize = (float)(size);
		}

		public void FontBlur(float blur)
		{
			NanoVGContextState state = GetState();
			state.FontBlur = (float)(blur);
		}

		public void TextLetterSpacing(float spacing)
		{
			NanoVGContextState state = GetState();
			state.LetterSpacing = (float)(spacing);
		}

		public void TextLineHeight(float lineHeight)
		{
			NanoVGContextState state = GetState();
			state.LineHeight = (float)(lineHeight);
		}

		public void TextAlign(int align)
		{
			NanoVGContextState state = GetState();
			state.TextAlign = (int)(align);
		}

		public void FontFaceId(int font)
		{
			NanoVGContextState state = GetState();
			state.FontId = (int)(font);
		}

		public void FontFace(string font)
		{
			NanoVGContextState state = GetState();
			state.FontId = (int)(_fontSyst.GetFontByName(font));
		}

		public void __flushTextTexture()
		{
			int* dirty = stackalloc int[4];
			if ((_fontSyst.ValidateTexture(dirty)) != 0)
			{
				int fontImage = (int)(_fontImages[_fontImageIdx]);
				if (fontImage != 0)
				{
					int iw = 0;
					int ih = 0;
					byte[] data = _fontSyst.GetTextureData(&iw, &ih);
					int x = (int)(dirty[0]);
					int y = (int)(dirty[1]);
					int w = (int)(dirty[2] - dirty[0]);
					int h = (int)(dirty[3] - dirty[1]);
					_renderer.UpdateTexture((int)(fontImage), (int)(x), (int)(y), (int)(w), (int)(h), data);
				}
			}
		}

		public int __allocTextAtlas()
		{
			int iw = 0;
			int ih = 0;
			__flushTextTexture();
			if ((_fontImageIdx) >= (4 - 1))
				return (int)(0);
			if (_fontImages[_fontImageIdx + 1] != 0)
				ImageSize((int)(_fontImages[_fontImageIdx + 1]), out iw, out ih);
			else
			{
				ImageSize((int)(_fontImages[_fontImageIdx]), out iw, out ih);
				if ((iw) > (ih))
					ih *= (int)(2);
				else
					iw *= (int)(2);
				if (((iw) > (2048)) || ((ih) > (2048)))
					iw = (int)(ih = (int)(2048));
				_fontImages[_fontImageIdx + 1] = (int)(_renderer.CreateTexture((int)(NVG_TEXTURE_ALPHA), (int)(iw), (int)(ih), (int)(0), null));
			}

			++_fontImageIdx;
			_fontSyst.ResetAtlas((int)(iw), (int)(ih));
			return (int)(1);
		}

		private void __renderText(ArraySegment<Vertex> verts)
		{
			NanoVGContextState state = GetState();
			Paint paint = (Paint)(state.Fill);
			paint.Image = (int)(_fontImages[_fontImageIdx]);

			MultiplyAlpha(ref paint.InnerColor, state.Alpha);
			MultiplyAlpha(ref paint.OuterColor, state.Alpha);

			_renderer.RenderTriangles(ref paint, ref state.Scissor, verts);
			_drawCallCount++;
			_textTriCount += (int)(verts.Count / 3);
		}

		public float Text(float x, float y, StringSegment _string_)
		{
			NanoVGContextState state = GetState();
			FontTextIterator iter = new FontTextIterator();
			FontTextIterator prevIter = new FontTextIterator();
			FontGlyphSquad q = new FontGlyphSquad();
			float scale = (float)(__getFontScale(state) * _devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int cverts = (int)(0);
			int nverts = (int)(0);
			if ((state.FontId) == (-1))
				return (float)(x);
			_fontSyst.SetSize((float)(state.FontSize * scale));
			_fontSyst.SetSpacing((float)(state.LetterSpacing * scale));
			_fontSyst.SetBlur((float)(state.FontBlur * scale));
			_fontSyst.SetAlign((int)(state.TextAlign));
			_fontSyst.SetFont((int)(state.FontId));
			cverts = (int)(__maxi((int)(2), (int)(_string_.Length)) * 6);
			var verts = __allocTempVerts((int)(cverts));

			_fontSyst.TextIterInit(iter, (float)(x * scale), (float)(y * scale), _string_, (int)(FontSystem.FONS_GLYPH_BITMAP_REQUIRED));
			prevIter = (FontTextIterator)(iter);

			while (_fontSyst.TextIterNext(iter, &q))
			{
				float* c = stackalloc float[4 * 2];
				if ((iter.PrevGlyphIndex) == (-1))
				{
					if (nverts != 0)
					{
						var segment = new ArraySegment<Vertex>(verts.Array, verts.Offset, nverts);
						__renderText(segment);
						nverts = (int)(0);
					}
					if (__allocTextAtlas() == 0)
						break;
					iter = (FontTextIterator)(prevIter);
					_fontSyst.TextIterNext(iter, &q);
					if ((iter.PrevGlyphIndex) == (-1))
						break;
				}
				prevIter = (FontTextIterator)(iter);
				state.Transform.TransformPoint(out c[0], out c[1], (float)(q.X0 * invscale), (float)(q.Y0 * invscale));
				state.Transform.TransformPoint(out c[2], out c[3], (float)(q.X1 * invscale), (float)(q.Y0 * invscale));
				state.Transform.TransformPoint(out c[4], out c[5], (float)(q.X1 * invscale), (float)(q.Y1 * invscale));
				state.Transform.TransformPoint(out c[6], out c[7], (float)(q.X0 * invscale), (float)(q.Y1 * invscale));
				if (nverts + 6 <= cverts)
				{
					__vset(ref verts.Array[verts.Offset + nverts], (float)(c[0]), (float)(c[1]), (float)(q.S0), (float)(q.T0));
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], (float)(c[4]), (float)(c[5]), (float)(q.S1), (float)(q.T1));
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], (float)(c[2]), (float)(c[3]), (float)(q.S1), (float)(q.T0));
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], (float)(c[0]), (float)(c[1]), (float)(q.S0), (float)(q.T0));
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], (float)(c[6]), (float)(c[7]), (float)(q.S0), (float)(q.T1));
					nverts++;
					__vset(ref verts.Array[verts.Offset + nverts], (float)(c[4]), (float)(c[5]), (float)(q.S1), (float)(q.T1));
					nverts++;
				}
			}

			__flushTextTexture();

			var segment2 = new ArraySegment<Vertex>(verts.Array, verts.Offset, nverts);
			__renderText(segment2);

			return (float)(iter.NextX / scale);
		}

		public void TextBox(float x, float y, float breakRowWidth, StringSegment _string_)
		{
			NanoVGContextState state = GetState();
			int i = 0;
			int oldAlign = (int)(state.TextAlign);
			int haling = (int)(state.TextAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state.TextAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);
			if ((state.FontId) == (-1))
				return;
			float ascender, descender;
			TextMetrics(out ascender, out descender, out lineh);
			state.TextAlign = (int)(NVG_ALIGN_LEFT | valign);
			while (true)
			{
				var nrows = (int)(TextBreakLines(_string_, (float)(breakRowWidth), _rows, out _string_));

				if (nrows <= 0)
				{
					break;
				}
				for (i = (int)(0); (i) < (nrows); i++)
				{
					var row = _rows[i];
					if ((haling & NVG_ALIGN_LEFT) != 0)
						Text((float)(x), (float)(y), row.Str);
					else if ((haling & NVG_ALIGN_CENTER) != 0)
						Text((float)(x + breakRowWidth * 0.5f - row.Width * 0.5f), (float)(y), row.Str);
					else if ((haling & NVG_ALIGN_RIGHT) != 0)
						Text((float)(x + breakRowWidth - row.Width), (float)(y), row.Str);
					y += (float)(lineh * state.LineHeight);
				}
			}
			state.TextAlign = (int)(oldAlign);
		}

		public int TextGlyphPositions(float x, float y, StringSegment _string_, GlyphPosition[] positions)
		{
			NanoVGContextState state = GetState();
			float scale = (float)(__getFontScale(state) * _devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FontTextIterator iter = new FontTextIterator();
			FontTextIterator prevIter = new FontTextIterator();
			FontGlyphSquad q = new FontGlyphSquad();
			int npos = (int)(0);
			if ((state.FontId) == (-1))
				return (int)(0);

			if (_string_.IsNullOrEmpty)
			{
				return 0;
			}

			_fontSyst.SetSize((float)(state.FontSize * scale));
			_fontSyst.SetSpacing((float)(state.LetterSpacing * scale));
			_fontSyst.SetBlur((float)(state.FontBlur * scale));
			_fontSyst.SetAlign((int)(state.TextAlign));
			_fontSyst.SetFont((int)(state.FontId));
			_fontSyst.TextIterInit(iter, (float)(x * scale), (float)(y * scale), _string_, (int)(FontSystem.FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FontTextIterator)(iter);
			while (_fontSyst.TextIterNext(iter, &q))
			{
				if (((iter.PrevGlyphIndex) < (0)) && ((__allocTextAtlas()) != 0))
				{
					iter = (FontTextIterator)(prevIter);
					_fontSyst.TextIterNext(iter, &q);
				}
				prevIter = (FontTextIterator)(iter);
				positions[npos].Str = iter.Str;
				positions[npos].X = (float)(iter.X * invscale);
				positions[npos].MinX = (float)(__minf((float)(iter.X), (float)(q.X0)) * invscale);
				positions[npos].MaxX = (float)(__maxf((float)(iter.NextX), (float)(q.X1)) * invscale);
				npos++;
				if ((npos) >= (positions.Length))
					break;
			}
			return (int)(npos);
		}

		public int TextBreakLines(StringSegment _string_, float breakRowWidth, TextRow[] rows, out StringSegment remaining)
		{
			remaining = StringSegment.Null;

			NanoVGContextState state = GetState();
			float scale = (float)(__getFontScale(state) * _devicePxRatio);
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

			if ((state.FontId) == (-1))
				return (int)(0);

			if (_string_.IsNullOrEmpty)
			{
				return 0;
			}
			_fontSyst.SetSize((float)(state.FontSize * scale));
			_fontSyst.SetSpacing((float)(state.LetterSpacing * scale));
			_fontSyst.SetBlur((float)(state.FontBlur * scale));
			_fontSyst.SetAlign((int)(state.TextAlign));
			_fontSyst.SetFont((int)(state.FontId));
			breakRowWidth *= (float)(scale);
			_fontSyst.TextIterInit(iter, (float)(0), (float)(0), _string_, (int)(FontSystem.FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FontTextIterator)(iter);
			while ((_fontSyst.TextIterNext(iter, &q)))
			{
				if (((iter.PrevGlyphIndex) < (0)) && ((__allocTextAtlas()) != 0))
				{
					iter = (FontTextIterator)(prevIter);
					_fontSyst.TextIterNext(iter, &q);
				}
				prevIter = (FontTextIterator)(iter);
				switch (iter.Codepoint)
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
						if ((((((((iter.Codepoint) >= (0x4E00)) && (iter.Codepoint <= 0x9FFF)) || (((iter.Codepoint) >= (0x3000)) && (iter.Codepoint <= 0x30FF))) || (((iter.Codepoint) >= (0xFF00)) && (iter.Codepoint <= 0xFFEF))) || (((iter.Codepoint) >= (0x1100)) && (iter.Codepoint <= 0x11FF))) || (((iter.Codepoint) >= (0x3130)) && (iter.Codepoint <= 0x318F))) || (((iter.Codepoint) >= (0xAC00)) && (iter.Codepoint <= 0xD7AF)))
							type = (int)(NVG_CJK_CHAR);
						else
							type = (int)(NVG_CHAR);
						break;
				}
				if ((type) == (NVG_NEWLINE))
				{
					rows[nrows].Str = rowStart == null ? iter.Str : new StringSegment(iter.Str, rowStart.Value);
					if (rowEnd != null)
					{
						rows[nrows].Str.Length = rowEnd.Value - rows[nrows].Str.Location;
					}
					else
					{
						rows[nrows].Str.Length = 0;
					}
					rows[nrows].Width = (float)(rowWidth * invscale);
					rows[nrows].MinX = (float)(rowMinX * invscale);
					rows[nrows].MaxX = (float)(rowMaxX * invscale);
					remaining = iter.Next;
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
							rowStartX = (float)(iter.X);
							rowStart = iter.Str.Location;
							rowEnd = iter.Str.Location + 1;
							rowWidth = (float)(iter.NextX - rowStartX);
							rowMinX = (float)(q.X0 - rowStartX);
							rowMaxX = (float)(q.X1 - rowStartX);
							wordStart = iter.Str.Location;
							wordStartX = (float)(iter.X);
							wordMinX = (float)(q.X0 - rowStartX);
							breakEnd = rowStart;
							breakWidth = (float)(0.0);
							breakMaxX = (float)(0.0);
						}
					}
					else
					{
						float nextWidth = (float)(iter.NextX - rowStartX);
						if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))
						{
							rowEnd = iter.Str.Location + 1;
							rowWidth = (float)(iter.NextX - rowStartX);
							rowMaxX = (float)(q.X1 - rowStartX);
						}
						if (((((ptype) == (NVG_CHAR)) || ((ptype) == (NVG_CJK_CHAR))) && ((type) == (NVG_SPACE))) || ((type) == (NVG_CJK_CHAR)))
						{
							breakEnd = iter.Str.Location;
							breakWidth = (float)(rowWidth);
							breakMaxX = (float)(rowMaxX);
						}
						if ((((ptype) == (NVG_SPACE)) && (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))) || ((type) == (NVG_CJK_CHAR)))
						{
							wordStart = iter.Str.Location;
							wordStartX = (float)(iter.X);
							wordMinX = (float)(q.X0 - rowStartX);
						}
						if ((((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) && ((nextWidth) > (breakRowWidth)))
						{
							if ((breakEnd) == (rowStart))
							{
								rows[nrows].Str = new StringSegment(_string_, rowStart.Value, iter.Str.Location);
								rows[nrows].Width = (float)(rowWidth * invscale);
								rows[nrows].MinX = (float)(rowMinX * invscale);
								rows[nrows].MaxX = (float)(rowMaxX * invscale);
								remaining = iter.Str;
								nrows++;
								if ((nrows) >= (rows.Length))
									return (int)(nrows);
								rowStartX = (float)(iter.X);
								rowStart = iter.Str.Location;
								rowEnd = iter.Str.Location + 1;
								rowWidth = (float)(iter.NextX - rowStartX);
								rowMinX = (float)(q.X0 - rowStartX);
								rowMaxX = (float)(q.X1 - rowStartX);
								wordStart = iter.Str.Location;
								wordStartX = (float)(iter.X);
								wordMinX = (float)(q.X0 - rowStartX);
							}
							else
							{
								rows[nrows].Str = new StringSegment(_string_, rowStart.Value, breakEnd.Value - rowStart.Value);
								rows[nrows].Width = (float)(breakWidth * invscale);
								rows[nrows].MinX = (float)(rowMinX * invscale);
								rows[nrows].MaxX = (float)(breakMaxX * invscale);
								remaining = new StringSegment(_string_, wordStart.Value);

								nrows++;
								if ((nrows) >= rows.Length)
									return (int)(nrows);
								rowStartX = (float)(wordStartX);
								rowStart = wordStart;
								rowEnd = iter.Str.Location + 1;
								rowWidth = (float)(iter.NextX - rowStartX);
								rowMinX = (float)(wordMinX);
								rowMaxX = (float)(q.X1 - rowStartX);
							}
							breakEnd = rowStart;
							breakWidth = (float)(0.0);
							breakMaxX = (float)(0.0);
						}
					}
				}
				pcodepoint = (uint)(iter.Codepoint);
				ptype = (int)(type);
			}
			if (rowStart != null)
			{
				rows[nrows].Str = new StringSegment(_string_, rowStart.Value, rowEnd.Value - rowStart.Value);
				rows[nrows].Width = (float)(rowWidth * invscale);
				rows[nrows].MinX = (float)(rowMinX * invscale);
				rows[nrows].MaxX = (float)(rowMaxX * invscale);
				remaining = StringSegment.Null;

				nrows++;
			}

			return (int)(nrows);
		}

		public float TextBounds(float x, float y, string _string_, ref Bounds bounds)
		{
			NanoVGContextState state = GetState();
			float scale = (float)(__getFontScale(state) * _devicePxRatio);
			float invscale = (float)(1.0f / scale);
			float width = 0;
			if ((state.FontId) == (-1))
				return (float)(0);
			_fontSyst.SetSize((float)(state.FontSize * scale));
			_fontSyst.SetSpacing((float)(state.LetterSpacing * scale));
			_fontSyst.SetBlur((float)(state.FontBlur * scale));
			_fontSyst.SetAlign((int)(state.TextAlign));
			_fontSyst.SetFont((int)(state.FontId));
			width = (float)(_fontSyst.TextBounds((float)(x * scale), (float)(y * scale), _string_, ref bounds));
			_fontSyst.LineBounds((float)(y * scale), ref bounds.b2, ref bounds.b4);
			bounds.b1 *= (float)(invscale);
			bounds.b2 *= (float)(invscale);
			bounds.b3 *= (float)(invscale);
			bounds.b4 *= (float)(invscale);

			return (float)(width * invscale);
		}

		public void TextBoxBounds(float x, float y, float breakRowWidth, StringSegment _string_, ref Bounds bounds)
		{
			NanoVGContextState state = GetState();
			float scale = (float)(__getFontScale(state) * _devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int i = 0;
			int oldAlign = (int)(state.TextAlign);
			int haling = (int)(state.TextAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state.TextAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);
			float rminy = (float)(0);
			float rmaxy = (float)(0);
			float minx = 0;
			float miny = 0;
			float maxx = 0;
			float maxy = 0;
			if ((state.FontId) == (-1))
			{
				bounds.b1 = (float)(bounds.b2 = (float)(bounds.b3 = (float)(bounds.b4 = (float)(0.0f))));
				return;
			}

			float ascender, descender;
			TextMetrics(out ascender, out descender, out lineh);
			state.TextAlign = (int)(NVG_ALIGN_LEFT | valign);
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			_fontSyst.SetSize((float)(state.FontSize * scale));
			_fontSyst.SetSpacing((float)(state.LetterSpacing * scale));
			_fontSyst.SetBlur((float)(state.FontBlur * scale));
			_fontSyst.SetAlign((int)(state.TextAlign));
			_fontSyst.SetFont((int)(state.FontId));
			_fontSyst.LineBounds((float)(0), ref rminy, ref rmaxy);
			rminy *= (float)(invscale);
			rmaxy *= (float)(invscale);
			while (true)
			{
				var nrows = TextBreakLines(_string_, (float)(breakRowWidth), _rows, out _string_);
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
						dx = (float)(breakRowWidth * 0.5f - row.Width * 0.5f);
					else if ((haling & NVG_ALIGN_RIGHT) != 0)
						dx = (float)(breakRowWidth - row.Width);
					rminx = (float)(x + row.MinX + dx);
					rmaxx = (float)(x + row.MaxX + dx);
					minx = (float)(__minf((float)(minx), (float)(rminx)));
					maxx = (float)(__maxf((float)(maxx), (float)(rmaxx)));
					miny = (float)(__minf((float)(miny), (float)(y + rminy)));
					maxy = (float)(__maxf((float)(maxy), (float)(y + rmaxy)));
					y += (float)(lineh * state.LineHeight);
				}
			}
			state.TextAlign = (int)(oldAlign);
			bounds.b1 = (float)(minx);
			bounds.b2 = (float)(miny);
			bounds.b3 = (float)(maxx);
			bounds.b4 = (float)(maxy);
		}

		public void TextMetrics(out float ascender, out float descender, out float lineh)
		{
			ascender = descender = lineh = 0;

			NanoVGContextState state = GetState();
			float scale = (float)(__getFontScale(state) * _devicePxRatio);
			float invscale = (float)(1.0f / scale);
			if ((state.FontId) == (-1))
				return;
			_fontSyst.SetSize((float)(state.FontSize * scale));
			_fontSyst.SetSpacing((float)(state.LetterSpacing * scale));
			_fontSyst.SetBlur((float)(state.FontBlur * scale));
			_fontSyst.SetAlign((int)(state.TextAlign));
			_fontSyst.SetFont((int)(state.FontId));
			_fontSyst.VertMetrics(out ascender, out descender, out lineh);
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

		private static float __modf(float a, float b)
		{
			return (float)(CRuntime.fmod((float)(a), (float)(b)));
		}

		public static int __mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int __maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}

		public static int __clampi(int a, int mn, int mx)
		{
			return (int)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		private static float __minf(float a, float b)
		{
			return (float)((a) < (b) ? a : b);
		}

		private static float __maxf(float a, float b)
		{
			return (float)((a) > (b) ? a : b);
		}

		private static float __absf(float a)
		{
			return (float)((a) >= (0.0f) ? a : -a);
		}

		private static float __signf(float a)
		{
			return (float)((a) >= (0.0f) ? 1.0f : -1.0f);
		}

		private static float __clampf(float a, float mn, float mx)
		{
			return (float)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		private static float __cross(float dx0, float dy0, float dx1, float dy1)
		{
			return (float)(dx1 * dy0 - dx0 * dy1);
		}

		private static float __normalize(float* x, float* y)
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

		private static float __hue(float h, float m1, float m2)
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

		public static Color HSLA(float h, float s, float l, byte a)
		{
			float m1 = 0;
			float m2 = 0;
			h = (float)(__modf((float)(h), (float)(1.0f)));
			if ((h) < (0.0f))
				h += (float)(1.0f);
			s = (float)(__clampf((float)(s), (float)(0.0f), (float)(1.0f)));
			l = (float)(__clampf((float)(l), (float)(0.0f), (float)(1.0f)));
			m2 = (float)(l <= 0.5f ? (l * (1 + s)) : (l + s - l * s));
			m1 = (float)(2 * l - m2);

			float fr = (float)(__clampf((float)(__hue((float)(h + 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			float fg = (float)(__clampf((float)(__hue((float)(h), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			float fb = (float)(__clampf((float)(__hue((float)(h - 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			float fa = (float)(a / 255.0f);

			return new Color(fr, fg, fb, fa);
		}

		public static float DegToRad(float deg)
		{
			return (float)(deg / 180.0f * 3.14159274);
		}

		public static float RadToDeg(float rad)
		{
			return (float)(rad / 3.14159274 * 180.0f);
		}

		private static void __setPaintColor(ref Paint p, Color color)
		{
			p.Zero();
			p.Transform.SetIdentity();
			p.Radius = 0.0f;
			p.Feather = 1.0f;
			p.InnerColor = color;
			p.OuterColor = color;
		}

		private static float __triarea2(float ax, float ay, float bx, float by, float cx, float cy)
		{
			float abx = bx - ax;
			float aby = (float)(by - ay);
			float acx = (float)(cx - ax);
			float acy = (float)(cy - ay);
			return (float)(acx * aby - abx * acy);
		}

		private static float __polyArea(NvgPoint* pts, int npts)
		{
			int i = 0;
			float area = (float)(0);
			for (i = (int)(2); (i) < (npts); i++)
			{
				NvgPoint* a = &pts[0];
				NvgPoint* b = &pts[i - 1];
				NvgPoint* c = &pts[i];
				area += (float)(__triarea2((float)(a->X), (float)(a->Y), (float)(b->X), (float)(b->Y), (float)(c->X), (float)(c->Y)));
			}
			return (float)(area * 0.5f);
		}

		internal static void __polyReverse(NvgPoint* pts, int npts)
		{
			NvgPoint tmp = new NvgPoint();
			int i = (int)(0);
			int j = (int)(npts - 1);
			while ((i) < (j))
			{
				tmp = (NvgPoint)(pts[i]);
				pts[i] = (NvgPoint)(pts[j]);
				pts[j] = (NvgPoint)(tmp);
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
			vtx.Position.X = (float)(x);
			vtx.Position.Y = (float)(y);
			vtx.TextureCoordinate.X = (float)(u);
			vtx.TextureCoordinate.Y = (float)(v);
		}

		private static void __isectRects(float* dst, float ax, float ay, float aw, float ah, float bx, float by, float bw, float bh)
		{
			float minx = (float)(__maxf((float)(ax), (float)(bx)));
			float miny = (float)(__maxf((float)(ay), (float)(by)));
			float maxx = (float)(__minf((float)(ax + aw), (float)(bx + bw)));
			float maxy = (float)(__minf((float)(ay + ah), (float)(by + bh)));
			dst[0] = (float)(minx);
			dst[1] = (float)(miny);
			dst[2] = (float)(__maxf((float)(0.0f), (float)(maxx - minx)));
			dst[3] = (float)(__maxf((float)(0.0f), (float)(maxy - miny)));
		}

		private static float __getAverageScale(ref Transform t)
		{
			float sx = (float)(Math.Sqrt((float)(t.T1 * t.T1 + t.T3 * t.T3)));
			float sy = (float)(Math.Sqrt((float)(t.T2 * t.T2 + t.T4 * t.T4)));
			return (float)((sx + sy) * 0.5f);
		}

		public static int __curveDivs(float r, float arc, float tol)
		{
			float da = (float)(acosf((float)(r / (r + tol))) * 2.0f);
			return (int)(__maxi((int)(2), (int)(ceilf((float)(arc / da)))));
		}

		private static void __chooseBevel(int bevel, NvgPoint* p0, NvgPoint* p1, float w, float* x0, float* y0, float* x1, float* y1)
		{
			if ((bevel) != 0)
			{
				*x0 = (float)(p1->X + p0->DeltaY * w);
				*y0 = (float)(p1->Y - p0->DeltaX * w);
				*x1 = (float)(p1->X + p1->DeltaY * w);
				*y1 = (float)(p1->Y - p1->DeltaX * w);
			}
			else
			{
				*x0 = (float)(p1->X + p1->dmx * w);
				*y0 = (float)(p1->Y + p1->dmy * w);
				*x1 = (float)(p1->X + p1->dmx * w);
				*y1 = (float)(p1->Y + p1->dmy * w);
			}
		}

		private static float __quantize(float a, float d)
		{
			return (float)(((int)(a / d + 0.5f)) * d);
		}

		private static float __getFontScale(NanoVGContextState state)
		{
			return (float)(__minf((float)(__quantize((float)(__getAverageScale(ref state.Transform)), (float)(0.01f))), (float)(4.0f)));
		}

		private static Vertex* __roundJoin(Vertex* dst, NvgPoint* p0, NvgPoint* p1, float lw, float rw, float lu, float ru, int ncap, float fringe)
		{
			int i = 0;
			int n = 0;
			float dlx0 = (float)(p0->DeltaY);
			float dly0 = (float)(-p0->DeltaX);
			float dlx1 = (float)(p1->DeltaY);
			float dly1 = (float)(-p1->DeltaX);
			if ((p1->flags & NVG_PT_LEFT) != 0)
			{
				float lx0 = 0;
				float ly0 = 0;
				float lx1 = 0;
				float ly1 = 0;
				float a0 = 0;
				float a1 = 0;
				__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1);
				a0 = (float)(atan2f((float)(-dly0), (float)(-dlx0)));
				a1 = (float)(atan2f((float)(-dly1), (float)(-dlx1)));
				if ((a1) > (a0))
					a1 -= (float)(3.14159274 * 2);
				__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(p1->X - dlx0 * rw), (float)(p1->Y - dly0 * rw), (float)(ru), (float)(1));
				dst++;
				n = (int)(__clampi((int)(ceilf((float)(((a0 - a1) / 3.14159274) * ncap))), (int)(2), (int)(ncap)));
				for (i = (int)(0); (i) < (n); i++)
				{
					float u = (float)(i / (float)(n - 1));
					float a = (float)(a0 + u * (a1 - a0));
					float rx = (float)(p1->X + cosf((float)(a)) * rw);
					float ry = (float)(p1->Y + sinf((float)(a)) * rw);
					__vset(dst, (float)(p1->X), (float)(p1->Y), (float)(0.5f), (float)(1));
					dst++;
					__vset(dst, (float)(rx), (float)(ry), (float)(ru), (float)(1));
					dst++;
				}
				__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(p1->X - dlx1 * rw), (float)(p1->Y - dly1 * rw), (float)(ru), (float)(1));
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
				__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1);
				a0 = (float)(atan2f((float)(dly0), (float)(dlx0)));
				a1 = (float)(atan2f((float)(dly1), (float)(dlx1)));
				if ((a1) < (a0))
					a1 += (float)(3.14159274 * 2);
				__vset(dst, (float)(p1->X + dlx0 * rw), (float)(p1->Y + dly0 * rw), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
				dst++;
				n = (int)(__clampi((int)(ceilf((float)(((a1 - a0) / 3.14159274) * ncap))), (int)(2), (int)(ncap)));
				for (i = (int)(0); (i) < (n); i++)
				{
					float u = (float)(i / (float)(n - 1));
					float a = (float)(a0 + u * (a1 - a0));
					float lx = (float)(p1->X + cosf((float)(a)) * lw);
					float ly = (float)(p1->Y + sinf((float)(a)) * lw);
					__vset(dst, (float)(lx), (float)(ly), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X), (float)(p1->Y), (float)(0.5f), (float)(1));
					dst++;
				}
				__vset(dst, (float)(p1->X + dlx1 * rw), (float)(p1->Y + dly1 * rw), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));
				dst++;
			}

			return dst;
		}

		private static Vertex* __bevelJoin(Vertex* dst, NvgPoint* p0, NvgPoint* p1, float lw, float rw, float lu, float ru, float fringe)
		{
			float rx0 = 0;
			float ry0 = 0;
			float rx1 = 0;
			float ry1 = 0;
			float lx0 = 0;
			float ly0 = 0;
			float lx1 = 0;
			float ly1 = 0;
			float dlx0 = (float)(p0->DeltaY);
			float dly0 = (float)(-p0->DeltaX);
			float dlx1 = (float)(p1->DeltaY);
			float dly1 = (float)(-p1->DeltaX);
			if ((p1->flags & NVG_PT_LEFT) != 0)
			{
				__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1);
				__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(p1->X - dlx0 * rw), (float)(p1->Y - dly0 * rw), (float)(ru), (float)(1));
				dst++;
				if ((p1->flags & NVG_PT_BEVEL) != 0)
				{
					__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X - dlx0 * rw), (float)(p1->Y - dly0 * rw), (float)(ru), (float)(1));
					dst++;
					__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X - dlx1 * rw), (float)(p1->Y - dly1 * rw), (float)(ru), (float)(1));
					dst++;
				}
				else
				{
					rx0 = (float)(p1->X - p1->dmx * rw);
					ry0 = (float)(p1->Y - p1->dmy * rw);
					__vset(dst, (float)(p1->X), (float)(p1->Y), (float)(0.5f), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X - dlx0 * rw), (float)(p1->Y - dly0 * rw), (float)(ru), (float)(1));
					dst++;
					__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
					dst++;
					__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X), (float)(p1->Y), (float)(0.5f), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X - dlx1 * rw), (float)(p1->Y - dly1 * rw), (float)(ru), (float)(1));
					dst++;
				}
				__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(p1->X - dlx1 * rw), (float)(p1->Y - dly1 * rw), (float)(ru), (float)(1));
				dst++;
			}
			else
			{
				__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1);
				__vset(dst, (float)(p1->X + dlx0 * lw), (float)(p1->Y + dly0 * lw), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
				dst++;
				if ((p1->flags & NVG_PT_BEVEL) != 0)
				{
					__vset(dst, (float)(p1->X + dlx0 * lw), (float)(p1->Y + dly0 * lw), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X + dlx1 * lw), (float)(p1->Y + dly1 * lw), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));
					dst++;
				}
				else
				{
					lx0 = (float)(p1->X + p1->dmx * lw);
					ly0 = (float)(p1->Y + p1->dmy * lw);
					__vset(dst, (float)(p1->X + dlx0 * lw), (float)(p1->Y + dly0 * lw), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X), (float)(p1->Y), (float)(0.5f), (float)(1));
					dst++;
					__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X + dlx1 * lw), (float)(p1->Y + dly1 * lw), (float)(lu), (float)(1));
					dst++;
					__vset(dst, (float)(p1->X), (float)(p1->Y), (float)(0.5f), (float)(1));
					dst++;
				}
				__vset(dst, (float)(p1->X + dlx1 * lw), (float)(p1->Y + dly1 * lw), (float)(lu), (float)(1));
				dst++;
				__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));
				dst++;
			}

			return dst;
		}

		private static Vertex* __buttCapStart(Vertex* dst, NvgPoint* p, float dx, float dy, float w, float d, float aa, float u0, float u1)
		{
			float px = (float)(p->X - dx * d);
			float py = (float)(p->Y - dy * d);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			__vset(dst, (float)(px + dlx * w - dx * aa), (float)(py + dly * w - dy * aa), (float)(u0), (float)(0));
			dst++;
			__vset(dst, (float)(px - dlx * w - dx * aa), (float)(py - dly * w - dy * aa), (float)(u1), (float)(0));
			dst++;
			__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			return dst;
		}

		private static Vertex* __buttCapEnd(Vertex* dst, NvgPoint* p, float dx, float dy, float w, float d, float aa, float u0, float u1)
		{
			float px = (float)(p->X + dx * d);
			float py = (float)(p->Y + dy * d);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			__vset(dst, (float)(px + dlx * w + dx * aa), (float)(py + dly * w + dy * aa), (float)(u0), (float)(0));
			dst++;
			__vset(dst, (float)(px - dlx * w + dx * aa), (float)(py - dly * w + dy * aa), (float)(u1), (float)(0));
			dst++;
			return dst;
		}

		private static Vertex* __roundCapStart(Vertex* dst, NvgPoint* p, float dx, float dy, float w, int ncap, float aa, float u0, float u1)
		{
			int i = 0;
			float px = (float)(p->X);
			float py = (float)(p->Y);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			for (i = (int)(0); (i) < (ncap); i++)
			{
				float a = (float)(i / (float)(ncap - 1) * 3.14159274);
				float ax = (float)(cosf((float)(a)) * w);
				float ay = (float)(sinf((float)(a)) * w);
				__vset(dst, (float)(px - dlx * ax - dx * ay), (float)(py - dly * ax - dy * ay), (float)(u0), (float)(1));
				dst++;
				__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1));
				dst++;
			}
			__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			return dst;
		}

		private static Vertex* __roundCapEnd(Vertex* dst, NvgPoint* p, float dx, float dy, float w, int ncap, float aa, float u0, float u1)
		{
			int i = 0;
			float px = (float)(p->X);
			float py = (float)(p->Y);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			for (i = (int)(0); (i) < (ncap); i++)
			{
				float a = (float)(i / (float)(ncap - 1) * 3.14159274);
				float ax = (float)(cosf((float)(a)) * w);
				float ay = (float)(sinf((float)(a)) * w);
				__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1));
				dst++;
				__vset(dst, (float)(px - dlx * ax + dx * ay), (float)(py - dly * ax + dy * ay), (float)(u0), (float)(1));
				dst++;
			}
			return dst;
		}

		public static int __ptEquals(float x1, float y1, float x2, float y2, float tol)
		{
			float dx = (float)(x2 - x1);
			float dy = (float)(y2 - y1);
			return (int)((dx * dx + dy * dy) < (tol * tol) ? 1 : 0);
		}

		private static float __distPtSeg(float x, float y, float px, float py, float qx, float qy)
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