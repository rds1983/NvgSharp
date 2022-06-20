using System;
using System.Collections.Generic;
using System.Text;
using FontStashSharp;
using FontStashSharp.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
{
	public unsafe class NvgContext : IFontStashRenderer2
	{
		/// <summary>
		/// Length proportional to radius of a cubic bezier handle for 90deg arcs
		/// </summary>
		private const float NVG_KAPPA90 = 0.5522847493f;

		public const int MaxTextRows = 10;

		private readonly GraphicsDevice _device;
		private readonly IRenderer _renderer;
		private readonly PathCache _cache;
		private readonly List<Command> _commands = new List<Command>();
		private float _commandX;
		private float _commandY;
		private float _devicePxRatio;
		private float _distTol;
		private int _drawCallCount;
		private readonly bool _edgeAntiAlias;
		private int _fillTriCount;
		private float _fringeWidth;
		private readonly NvgContextState[] _states = new NvgContextState[32];
		private int _statesNumber;
		private int _strokeTriCount;
		private float _tessTol;
		private int _textTriCount;
		private Texture2D _lastTextTexture = null;

		public GraphicsDevice GraphicsDevice => _device;

		public NvgContext(GraphicsDevice device, bool edgeAntiAlias = true)
		{
			_device = device;
			_renderer = new Renderer(device);

			_edgeAntiAlias = edgeAntiAlias;
			_cache = new PathCache();
			Save();
			Reset();
			SetDevicePixelRatio(1.0f);
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
			state.LineCap = NvgSharp.LineCap.Butt;
			state.LineJoin = NvgSharp.LineCap.Miter;
			state.Alpha = 1.0f;
			state.Transform.SetIdentity();
			state.Scissor.Extent.X = -1.0f;
			state.Scissor.Extent.Y = -1.0f;
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

		public void LineCap(LineCap cap)
		{
			var state = GetState();
			state.LineCap = cap;
		}

		public void LineJoin(LineCap join)
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

		public Paint ImagePattern(float cx, float cy, float w, float h, float angle, Texture2D image, float alpha)
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
			_commands.Clear();
			__clearPathCache();
		}

		public void MoveTo(float x, float y) => AppendCommand(CommandType.MoveTo, x, y);

		public void LineTo(float x, float y) => AppendCommand(CommandType.LineTo, x, y);

		public void BezierTo(float c1x, float c1y, float c2x, float c2y, float x, float y) =>
			AppendCommand(c1x, c1y, c2x, c2y, x, y);

		public void QuadTo(float cx, float cy, float x, float y)
		{
			var x0 = _commandX;
			var y0 = _commandY;

			AppendCommand(x0 + 2.0f / 3.0f * (cx - x0), y0 + 2.0f / 3.0f * (cy - y0),
				x + 2.0f / 3.0f * (cx - x), y + 2.0f / 3.0f * (cy - y), x, y);
		}

		public void ArcTo(float x1, float y1, float x2, float y2, float radius)
		{
			var x0 = _commandX;
			var y0 = _commandY;

			if (_commands.Count == 0)
				return;

			if (__ptEquals(x0, y0, x1, y1, _distTol) != 0 || __ptEquals(x1, y1, x2, y2, _distTol) != 0 ||
				__distPtSeg(x1, y1, x0, y0, x2, y2) < _distTol * _distTol || radius < _distTol)
			{
				LineTo(x1, y1);
				return;
			}

			var dx0 = x0 - x1;
			var dy0 = y0 - y1;
			var dx1 = x2 - x1;
			var dy1 = y2 - y1;
			NvgUtility.__normalize(ref dx0, ref dy0);
			NvgUtility.__normalize(ref dx1, ref dy1);
			var a = NvgUtility.acosf(dx0 * dx1 + dy0 * dy1);
			var d = radius / NvgUtility.tanf(a / 2.0f);
			if (d > 10000.0f)
			{
				LineTo(x1, y1);
				return;
			}

			float cx, cy, a0, a1;
			Winding dir;
			if (NvgUtility.__cross(dx0, dy0, dx1, dy1) > 0.0f)
			{
				cx = x1 + dx0 * d + dy0 * radius;
				cy = y1 + dy0 * d + -dx0 * radius;
				a0 = NvgUtility.atan2f(dx0, -dy0);
				a1 = NvgUtility.atan2f(-dx1, dy1);
				dir = Winding.ClockWise;
			}
			else
			{
				cx = x1 + dx0 * d + -dy0 * radius;
				cy = y1 + dy0 * d + dx0 * radius;
				a0 = NvgUtility.atan2f(-dx0, dy0);
				a1 = NvgUtility.atan2f(dx1, -dy1);
				dir = Winding.CounterClockWise;
			}

			Arc(cx, cy, radius, a0, a1, dir);
		}

		public void ClosePath() => AppendCommand(CommandType.Close);

		public void PathWinding(Solidity dir) => AppendCommand(dir);

		public void Arc(float cx, float cy, float r, float a0, float a1, Winding dir)
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
			var move = _commands.Count > 0 ? CommandType.LineTo : CommandType.MoveTo;
			da = a1 - a0;
			if (dir == Winding.ClockWise)
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
			if (dir == Winding.CounterClockWise)
				kappa = -kappa;
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
					AppendCommand(move, x, y);
				}
				else
				{
					AppendCommand(px + ptanx, py + ptany, x - tanx, y - tany, x, y);
				}

				px = x;
				py = y;
				ptanx = tanx;
				ptany = tany;
			}
		}

		public void Rect(float x, float y, float w, float h)
		{
			AppendCommand(CommandType.MoveTo, x, y);
			AppendCommand(CommandType.LineTo, x, y + h);
			AppendCommand(CommandType.LineTo, x + w, y + h);
			AppendCommand(CommandType.LineTo, x + w, y);
			AppendCommand(CommandType.Close);
		}

		public void RoundedRect(float x, float y, float w, float h, float r) => RoundedRectVarying(x, y, w, h, r, r, r, r);

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
				AppendCommand(CommandType.MoveTo, x, y + ryTL);
				AppendCommand(CommandType.LineTo, x, y + h - ryBL);
				AppendCommand(x, y + h - ryBL * (1 - NVG_KAPPA90), x + rxBL * (1 - NVG_KAPPA90), y + h, x + rxBL, y + h);
				AppendCommand(CommandType.LineTo, x + w - rxBR, y + h);
				AppendCommand(x + w - rxBR * (1 - NVG_KAPPA90), y + h, x + w, y + h - ryBR * (1 - NVG_KAPPA90), x + w, y + h - ryBR);
				AppendCommand(CommandType.LineTo, x + w, y + ryTR);
				AppendCommand(x + w, y + ryTR * (1 - NVG_KAPPA90), x + w - rxTR * (1 - NVG_KAPPA90), y, x + w - rxTR, y);
				AppendCommand(CommandType.LineTo, x + rxTL, y);
				AppendCommand(x + rxTL * (1 - NVG_KAPPA90), y, x, y + ryTL * (1 - NVG_KAPPA90), x, y + ryTL);
				AppendCommand(CommandType.Close);
			}
		}

		public void Ellipse(float cx, float cy, float rx, float ry)
		{
			AppendCommand(CommandType.MoveTo, cx - rx, cy);
			AppendCommand(cx - rx, cy + ry * NVG_KAPPA90, cx - rx * NVG_KAPPA90, cy + ry, cx, cy + ry);
			AppendCommand(cx + rx * NVG_KAPPA90, cy + ry, cx + rx, cy + ry * NVG_KAPPA90, cx + rx, cy);
			AppendCommand(cx + rx, cy - ry * NVG_KAPPA90, cx + rx * NVG_KAPPA90, cy - ry, cx, cy - ry);
			AppendCommand(cx - rx * NVG_KAPPA90, cy - ry, cx - rx, cy - ry * NVG_KAPPA90, cx - rx, cy);
			AppendCommand(CommandType.Close);
		}

		public void Circle(float cx, float cy, float r) => Ellipse(cx, cy, r, r);

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
			if (_edgeAntiAlias && state.ShapeAntiAlias != 0)
				__expandFill(_fringeWidth, NvgSharp.LineCap.Miter, 2.4f);
			else
				__expandFill(0.0f, NvgSharp.LineCap.Miter, 2.4f);
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
			if (_edgeAntiAlias && state.ShapeAntiAlias != 0)
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


		void IFontStashRenderer2.DrawQuad(Texture2D texture,
			ref VertexPositionColorTexture topLeft, ref VertexPositionColorTexture topRight,
			ref VertexPositionColorTexture bottomLeft, ref VertexPositionColorTexture bottomRight)
		{
			if (_lastTextTexture != null && _lastTextTexture != texture)
			{
				FlushText();
			}

			var state = GetState();

			float px, py;
			state.Transform.TransformPoint(out px, out py, topLeft.Position.X, topLeft.Position.Y);
			var newTopLeft = new Vertex(px, py, topLeft.TextureCoordinate.X, topLeft.TextureCoordinate.Y);

			state.Transform.TransformPoint(out px, out py, topRight.Position.X, topRight.Position.Y);
			var newTopRight = new Vertex(px, py, topRight.TextureCoordinate.X, topRight.TextureCoordinate.Y);

			state.Transform.TransformPoint(out px, out py, bottomRight.Position.X, bottomRight.Position.Y);
			var newBottomRight = new Vertex(px, py, bottomRight.TextureCoordinate.X, bottomRight.TextureCoordinate.Y);

			state.Transform.TransformPoint(out px, out py, bottomLeft.Position.X, bottomLeft.Position.Y);
			var newBottomLeft = new Vertex(px, py, bottomLeft.TextureCoordinate.X, bottomLeft.TextureCoordinate.Y);

			var verts = _cache.Vertexes;
			verts.Add(newTopLeft);
			verts.Add(newBottomRight);
			verts.Add(newTopRight);
			verts.Add(newTopLeft);
			verts.Add(newBottomLeft);
			verts.Add(newBottomRight);

			_lastTextTexture = texture;
		}

		private void FlushText()
		{
			var verts = _cache.Vertexes;
			if (_lastTextTexture == null || verts.Count == 0)
			{
				return;
			}

			var state = GetState();
			var paint = state.Fill;
			paint.Image = _lastTextTexture;

			MultiplyAlpha(ref paint.InnerColor, state.Alpha);
			MultiplyAlpha(ref paint.OuterColor, state.Alpha);

			_renderer.RenderTriangles(ref paint, ref state.Scissor, verts.ToArraySegment());
			_drawCallCount++;
			_textTriCount += verts.Count / 3;

			_lastTextTexture = null;
			verts.Clear();
		}

		private void Text(SpriteFontBase font, TextSource text, float x, float y,
			TextHorizontalAlignment horizontalAlignment, TextVerticalAlignment verticalAlignment,
			float layerDepth, float characterSpacing, float lineSpacing)
		{
			if (text.IsNull)
			{
				return;
			}

			_cache.Vertexes.Clear();

			if (horizontalAlignment != TextHorizontalAlignment.Left)
			{
				Vector2 sz;
				if (text.StringText != null)
				{
					sz = font.MeasureString(text.StringText);
				}
				else
				{
					sz = font.MeasureString(text.StringBuilderText);
				}

				if (horizontalAlignment == TextHorizontalAlignment.Center)
				{
					x -= sz.X / 2.0f;
				}
				else if (horizontalAlignment == TextHorizontalAlignment.Right)
				{
					x -= sz.X;
				}
			}

			if (verticalAlignment == TextVerticalAlignment.Center)
			{
				y -= font.LineHeight / 2.0f;
			} else if (verticalAlignment == TextVerticalAlignment.Bottom)
			{
				y -= font.LineHeight;
			}

			if (text.StringText != null)
			{
				font.DrawText(this, text.StringText, new Vector2(x, y), Color.White,
					layerDepth: layerDepth, characterSpacing: characterSpacing, lineSpacing: lineSpacing);
			}
			else
			{
				font.DrawText(this, text.StringBuilderText, new Vector2(x, y), Color.White,
					layerDepth: layerDepth, characterSpacing: characterSpacing, lineSpacing: lineSpacing);
			}

			FlushText();
		}

		public void Text(SpriteFontBase font, string text, float x, float y,
			TextHorizontalAlignment horizontalAlignment = TextHorizontalAlignment.Left, TextVerticalAlignment verticalAlignment = TextVerticalAlignment.Top,
			float layerDepth = 0.0f, float characterSpacing = 0.0f, float lineSpacing = 0.0f) => 
			Text(font, new TextSource(text), x, y, horizontalAlignment, verticalAlignment, layerDepth, characterSpacing, lineSpacing);

		public void Text(SpriteFontBase font, StringBuilder text, float x, float y,
			TextHorizontalAlignment horizontalAlignment = TextHorizontalAlignment.Left, TextVerticalAlignment verticalAlignment = TextVerticalAlignment.Top,
			float layerDepth = 0.0f, float characterSpacing = 0.0f, float lineSpacing = 0.0f) =>
			Text(font, new TextSource(text), x, y, horizontalAlignment, verticalAlignment, layerDepth, characterSpacing, lineSpacing);

		private Bounds TextBounds(SpriteFontBase font, TextSource text, float x, float y, float characterSpacing, float lineSpacing)
		{
			Bounds result;
			if (text.StringText != null)
			{
				result = font.TextBounds(text.StringText, new Vector2(x, y), characterSpacing: characterSpacing, lineSpacing: lineSpacing);
			}
			else
			{
				result = font.TextBounds(text.StringBuilderText, new Vector2(x, y), characterSpacing: characterSpacing, lineSpacing: lineSpacing);
			}

			return result;
		}

		public Bounds TextBounds(SpriteFontBase font, string text, float x, float y, float characterSpacing = 0.0f, float lineSpacing = 0.0f) =>
			TextBounds(font, new TextSource(text), x, y, characterSpacing, lineSpacing);

		public Bounds TextBounds(SpriteFontBase font, StringBuilder text, float x, float y, float characterSpacing = 0.0f, float lineSpacing = 0.0f) =>
			TextBounds(font, new TextSource(text), x, y, characterSpacing, lineSpacing);

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

		private void AppendCommand(Command command)
		{
			var state = GetState();

			if (command.Type != CommandType.Close && command.Type != CommandType.Winding)
			{
				_commandX = command.P1;
				_commandY = command.P2;
			}

			switch (command.Type)
			{
				case CommandType.LineTo:
				case CommandType.MoveTo:
					state.Transform.TransformPoint(out command.P1, out command.P2, command.P1, command.P2);
					break;
				case CommandType.BezierTo:
					state.Transform.TransformPoint(out command.P1, out command.P2, command.P1, command.P2);
					state.Transform.TransformPoint(out command.P3, out command.P4, command.P3, command.P4);
					state.Transform.TransformPoint(out command.P5, out command.P6, command.P5, command.P6);
					break;
			}

			_commands.Add(command);
		}

		private void AppendCommand(CommandType type) => AppendCommand(new Command(type));
		private void AppendCommand(Solidity solidity) => AppendCommand(new Command(solidity));
		private void AppendCommand(CommandType type, float p1, float p2) => AppendCommand(new Command(type, p1, p2));
		private void AppendCommand(float p1, float p2, float p3, float p4, float p5, float p6) => 
			AppendCommand(new Command(p1, p2, p3, p4, p5, p6));

		private void __clearPathCache()
		{
			_cache.Paths.Clear();
		}

		private Path GetLastPath()
		{
			if (_cache.Paths.Count > 0)
				return _cache.Paths[_cache.Paths.Count - 1];
			return null;
		}

		private Path __addPath()
		{
			var newPath = new Path
			{
				Winding = Winding.CounterClockWise
			};

			_cache.Paths.Add(newPath);

			return newPath;
		}

		private void __addPoint(float x, float y, PointFlags flags)
		{
			var path = GetLastPath();
			if (path == null)
			{
				return;
			}

			NvgPoint pt;
			if (path.Points.Count > 0)
			{
				pt = path.LastPoint;
				if (__ptEquals(pt.X, pt.Y, x, y, _distTol) != 0)
				{
					pt.flags |= (byte)flags;
					return;
				}
			}

			pt = new NvgPoint();
			pt.Reset();
			pt.X = x;
			pt.Y = y;
			pt.flags = (byte)flags;
			path.Points.Add(pt);
		}

		private void __closePath()
		{
			var path = GetLastPath();
			if (path == null)
				return;
			path.Closed = true;
		}

		private void __pathWinding(Winding winding)
		{
			var path = GetLastPath();
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
			int level, PointFlags type)
		{
			if (level > 10)
				return;
			var x12 = (x1 + x2) * 0.5f;
			var y12 = (y1 + y2) * 0.5f;
			var x23 = (x2 + x3) * 0.5f;
			var y23 = (y2 + y3) * 0.5f;
			var x34 = (x3 + x4) * 0.5f;
			var y34 = (y3 + y4) * 0.5f;
			var x123 = (x12 + x23) * 0.5f;
			var y123 = (y12 + y23) * 0.5f;
			var dx = x4 - x1;
			var dy = y4 - y1;
			var d2 = NvgUtility.__absf((x2 - x4) * dy - (y2 - y4) * dx);
			var d3 = NvgUtility.__absf((x3 - x4) * dy - (y3 - y4) * dx);
			if ((d2 + d3) * (d2 + d3) < _tessTol * (dx * dx + dy * dy))
			{
				__addPoint(x4, y4, type);
				return;
			}

			var x234 = (x23 + x34) * 0.5f;
			var y234 = (y23 + y34) * 0.5f;
			var x1234 = (x123 + x234) * 0.5f;
			var y1234 = (y123 + y234) * 0.5f;
			__tesselateBezier(x1, y1, x12, y12, x123, y123, x1234, y1234, level + 1, 0);
			__tesselateBezier(x1234, y1234, x234, y234, x34, y34, x4, y4, level + 1, type);
		}

		private void __flattenPaths()
		{
			if (_cache.Paths.Count > 0)
				return;

			Path lastPath = null;
			for(var i = 0; i < _commands.Count; ++i)
			{
				switch (_commands[i].Type)
				{
					case CommandType.MoveTo:
						lastPath = __addPath();
						__addPoint(_commands[i].P1, _commands[i].P2, PointFlags.Corner);
						break;
					case CommandType.LineTo:
						__addPoint(_commands[i].P1, _commands[i].P2, PointFlags.Corner);
						break;
					case CommandType.BezierTo:
						if (lastPath != null && lastPath.Points.Count > 0)
						{
							var last = lastPath.LastPoint;
							__tesselateBezier(last.X, last.Y,
								_commands[i].P1, _commands[i].P2,
								_commands[i].P3, _commands[i].P4,
								_commands[i].P5, _commands[i].P6, 
								0, PointFlags.Corner);
						}

						break;
					case CommandType.Close:
						__closePath();
						break;
					case CommandType.Winding:
						__pathWinding((Winding)_commands[i].P1);
						break;
				}
			}

			_cache.Bounds.X = _cache.Bounds.Y = 1e6f;
			_cache.Bounds.X2 = _cache.Bounds.Y2 = -1e6f;
			for (var j = 0; j < _cache.Paths.Count; j++)
			{
				var path = _cache.Paths[j];

				var p0Index = path.Count - 1;
				var p1Index = 0;
				if (__ptEquals(path.LastPoint.X, path.LastPoint.Y, path.FirstPoint.X, path.FirstPoint.Y, _distTol) != 0)
				{
					path.Points.RemoveAt(path.Points.Count - 1);
					--p0Index;
					path.Closed = true;
				}

				if (path.Points.Count > 2)
				{
					var area = __polyArea(path.Points);
					if (path.Winding == Winding.CounterClockWise && area < 0.0f)
						__polyReverse(path.Points);
					if (path.Winding == Winding.ClockWise && area > 0.0f)
						__polyReverse(path.Points);
				}

				for (var i = 0; i < path.Points.Count; i++)
				{
					var p0 = path[p0Index];
					var p1 = path[p1Index];
					p0.DeltaX = p1.X - p0.X;
					p0.DeltaY = p1.Y - p0.Y;
					p0.Length = NvgUtility.__normalize(ref p0.DeltaX, ref p0.DeltaY);
					_cache.Bounds.X = NvgUtility.__minf(_cache.Bounds.X, p0.X);
					_cache.Bounds.Y = NvgUtility.__minf(_cache.Bounds.Y, p0.Y);
					_cache.Bounds.X2 = NvgUtility.__maxf(_cache.Bounds.X2, p0.X);
					_cache.Bounds.Y2 = NvgUtility.__maxf(_cache.Bounds.Y2, p0.Y);
					p0Index = p1Index++;
				}
			}
		}

		private void __calculateJoins(float w, LineCap lineJoin, float miterLimit)
		{
			var i = 0;
			var j = 0;
			var iw = 0.0f;
			if (w > 0.0f)
				iw = 1.0f / w;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				var p0Index = path.Count - 1;
				var p1Index = 0;
				var nleft = 0;
				path.BevelCount = 0;
				for (j = 0; j < path.Points.Count; j++)
				{
					var p0 = path[p0Index];
					var p1 = path[p1Index];

					var dlx0 = p0.DeltaY;
					var dly0 = -p0.DeltaX;
					var dlx1 = p1.DeltaY;
					var dly1 = -p1.DeltaX;
					p1.dmx = (dlx0 + dlx1) * 0.5f;
					p1.dmy = (dly0 + dly1) * 0.5f;
					var dmr2 = p1.dmx * p1.dmx + p1.dmy * p1.dmy;
					if (dmr2 > 0.000001f)
					{
						var scale = 1.0f / dmr2;
						if (scale > 600.0f)
							scale = 600.0f;
						p1.dmx *= scale;
						p1.dmy *= scale;
					}

					p1.flags = (byte)((p1.flags & (byte)PointFlags.Corner) != 0 ? PointFlags.Corner : 0);
					var cross = p1.DeltaX * p0.DeltaY - p0.DeltaX * p1.DeltaY;
					if (cross > 0.0f)
					{
						nleft++;
						p1.flags |= (byte)PointFlags.Left;
					}

					var limit = NvgUtility.__maxf(1.01f, NvgUtility.__minf(p0.Length, p1.Length) * iw);
					if (dmr2 * limit * limit < 1.0f)
						p1.flags |= (byte)PointFlags.InnerBevel;
					if ((p1.flags & (byte)PointFlags.Corner) != 0)
						if (dmr2 * miterLimit * miterLimit < 1.0f || lineJoin == NvgSharp.LineCap.Bevel || lineJoin == NvgSharp.LineCap.Round)
							p1.flags |= (byte)PointFlags.Bevel;
					if ((p1.flags & (byte)(PointFlags.Bevel | PointFlags.InnerBevel)) != 0)
						path.BevelCount++;
					p0Index = p1Index++;
				}

				path.Convex = nleft == path.Points.Count;
			}
		}

		private void __expandStroke(float w, float fringe, LineCap lineCap, LineCap lineJoin, float miterLimit)
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
				var loop = path.Closed;
				if (lineJoin == NvgSharp.LineCap.Round)
					cverts += (path.Points.Count + path.BevelCount * (ncap + 2) + 1) * 2;
				else
					cverts += (path.Points.Count + path.BevelCount * 5 + 1) * 2;
				if (!loop)
				{
					if (lineCap == NvgSharp.LineCap.Round)
						cverts += (ncap * 2 + 2) * 2;
					else
						cverts += (3 + 3) * 2;
				}
			}

			var verts = __allocTempVerts(cverts);
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				var s = 0;
				var e = 0;
				var loop = false;
				float dx = 0;
				float dy = 0;
				path.Fill = null;
				loop = path.Closed;
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					int p0Index, p1Index;
					if (loop)
					{
						p0Index = path.Count - 1;
						p1Index = 0;
						s = 0;
						e = path.Points.Count;
					}
					else
					{
						p0Index = 0;
						p1Index = 1;
						s = 1;
						e = path.Points.Count - 1;
					}

					var p0 = path[p0Index];
					var p1 = path[p1Index];
					if (!loop)
					{
						dx = p1.X - p0.X;
						dy = p1.Y - p0.Y;
						NvgUtility.__normalize(ref dx, ref dy);
						if (lineCap == NvgSharp.LineCap.Butt)
							dst = __buttCapStart(dst, p0, dx, dy, w, -aa * 0.5f, aa, u0, u1);
						else if (lineCap == NvgSharp.LineCap.Butt || lineCap == NvgSharp.LineCap.Square)
							dst = __buttCapStart(dst, p0, dx, dy, w, w - aa, aa, u0, u1);
						else if (lineCap == NvgSharp.LineCap.Round)
							dst = __roundCapStart(dst, p0, dx, dy, w, ncap, aa, u0, u1);
					}

					for (j = s; j < e; ++j)
					{
						p0 = path[p0Index];
						p1 = path[p1Index];
						if ((p1.flags & (byte)(PointFlags.Bevel | PointFlags.InnerBevel)) != 0)
						{
							if (lineJoin == NvgSharp.LineCap.Round)
								dst = __roundJoin(dst, p0, p1, w, w, u0, u1, ncap, aa);
							else
								dst = __bevelJoin(dst, p0, p1, w, w, u0, u1, aa);
						}
						else
						{
							__vset(dst, p1.X + p1.dmx * w, p1.Y + p1.dmy * w, u0, 1);
							dst++;
							__vset(dst, p1.X - p1.dmx * w, p1.Y - p1.dmy * w, u1, 1);
							dst++;
						}

						p0Index = p1Index++;
					}

					if (loop)
					{
						__vset(dst, verts.Array[verts.Offset].Position.X, verts.Array[verts.Offset].Position.Y, u0, 1);
						dst++;
						__vset(dst, verts.Array[verts.Offset + 1].Position.X, verts.Array[verts.Offset + 1].Position.Y,
							u1, 1);
						dst++;
					}
					else
					{
						p0 = path[p0Index];
						p1 = path[p1Index];

						dx = p1.X - p0.X;
						dy = p1.Y - p0.Y;
						NvgUtility.__normalize(ref dx, ref dy);
						if (lineCap == NvgSharp.LineCap.Butt)
							dst = __buttCapEnd(dst, p1, dx, dy, w, -aa * 0.5f, aa, u0, u1);
						else if (lineCap == NvgSharp.LineCap.Butt || lineCap == NvgSharp.LineCap.Square)
							dst = __buttCapEnd(dst, p1, dx, dy, w, w - aa, aa, u0, u1);
						else if (lineCap == NvgSharp.LineCap.Round)
							dst = __roundCapEnd(dst, p1, dx, dy, w, ncap, aa, u0, u1);
					}

					path.Stroke = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.Stroke.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}
			}
		}

		private void __expandFill(float w, LineCap lineJoin, float miterLimit)
		{
			var cverts = 0;
			var i = 0;
			var j = 0;
			var aa = _fringeWidth;
			var fringe = w > 0.0f;
			__calculateJoins(w, lineJoin, miterLimit);
			cverts = 0;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				cverts += path.Points.Count + path.BevelCount + 1;
				if (fringe)
					cverts += (path.Points.Count + path.BevelCount * 5 + 1) * 2;
			}

			var verts = __allocTempVerts(cverts);
			var convex = _cache.Paths.Count == 1 && _cache.Paths[0].Convex;
			for (i = 0; i < _cache.Paths.Count; i++)
			{
				var path = _cache.Paths[i];
				float rw = 0;
				float lw = 0;
				float woff = 0;
				float ru = 0;
				float lu = 0;
				woff = 0.5f * aa;
				fixed (Vertex* dst2 = &verts.Array[verts.Offset])
				{
					var dst = dst2;
					if (fringe)
					{
						var p0Index = path.Count - 1;
						var p1Index = 0;
						for (j = 0; j < path.Points.Count; ++j)
						{
							var p0 = path[p0Index];
							var p1 = path[p1Index];

							if ((p1.flags & (byte)PointFlags.Bevel) != 0)
							{
								var dlx0 = p0.DeltaY;
								var dly0 = -p0.DeltaX;
								var dlx1 = p1.DeltaY;
								var dly1 = -p1.DeltaX;
								if ((p1.flags & (byte)PointFlags.Left) != 0)
								{
									var lx = p1.X + p1.dmx * woff;
									var ly = p1.Y + p1.dmy * woff;
									__vset(dst, lx, ly, 0.5f, 1);
									dst++;
								}
								else
								{
									var lx0 = p1.X + dlx0 * woff;
									var ly0 = p1.Y + dly0 * woff;
									var lx1 = p1.X + dlx1 * woff;
									var ly1 = p1.Y + dly1 * woff;
									__vset(dst, lx0, ly0, 0.5f, 1);
									dst++;
									__vset(dst, lx1, ly1, 0.5f, 1);
									dst++;
								}
							}
							else
							{
								__vset(dst, p1.X + p1.dmx * woff, p1.Y + p1.dmy * woff, 0.5f, 1);
								dst++;
							}

							p0Index = p1Index++;
						}
					}
					else
					{
						for (j = 0; j < path.Count; ++j)
						{
							var p = path[j];
							__vset(dst, p.X, p.Y, 0.5f, 1);
							dst++;
						}
					}

					path.Fill = new ArraySegment<Vertex>(verts.Array, verts.Offset, (int)(dst - dst2));

					var newPos = verts.Offset + path.Fill.Value.Count;
					verts = new ArraySegment<Vertex>(verts.Array, newPos, verts.Array.Length - newPos);
				}

				if (fringe)
				{
					lw = w + woff;
					rw = w - woff;
					lu = 0;
					ru = 1;
					fixed (Vertex* dst2 = &verts.Array[verts.Offset])
					{
						var dst = dst2;
						if (convex)
						{
							lw = woff;
							lu = 0.5f;
						}

						var p0Index = path.Count - 1;
						var p1Index = 0;
						for (j = 0; j < path.Points.Count; ++j)
						{
							var p0 = path[p0Index];
							var p1 = path[p1Index];

							if ((p1.flags & (byte)(PointFlags.Bevel | PointFlags.InnerBevel)) != 0)
							{
								dst = __bevelJoin(dst, p0, p1, lw, rw, lu, ru, _fringeWidth);
							}
							else
							{
								__vset(dst, p1.X + p1.dmx * lw, p1.Y + p1.dmy * lw, lu, 1);
								dst++;
								__vset(dst, p1.X - p1.dmx * rw, p1.Y - p1.dmy * rw, ru, 1);
								dst++;
							}

							p0Index = p1Index++;
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
		}

		private static float __triarea2(float ax, float ay, float bx, float by, float cx, float cy)
		{
			var abx = bx - ax;
			var aby = by - ay;
			var acx = cx - ax;
			var acy = cy - ay;
			return acx * aby - abx * acy;
		}

		private static float __polyArea(List<NvgPoint> pts)
		{
			var i = 0;
			var area = (float)0;
			for (i = 2; i < pts.Count; i++)
			{
				var a = pts[0];
				var b = pts[i - 1];
				var c = pts[i];
				area += __triarea2(a.X, a.Y, b.X, b.Y, c.X, c.Y);
			}

			return area * 0.5f;
		}

		internal static void __polyReverse(List<NvgPoint> pts)
		{
			var i = 0;
			var j = pts.Count - 1;
			while (i < j)
			{
				var tmp = pts[i];
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

		private static void __chooseBevel(int bevel, NvgPoint p0, NvgPoint p1, float w, float* x0, float* y0,
			float* x1, float* y1)
		{
			if (bevel != 0)
			{
				*x0 = p1.X + p0.DeltaY * w;
				*y0 = p1.Y - p0.DeltaX * w;
				*x1 = p1.X + p1.DeltaY * w;
				*y1 = p1.Y - p1.DeltaX * w;
			}
			else
			{
				*x0 = p1.X + p1.dmx * w;
				*y0 = p1.Y + p1.dmy * w;
				*x1 = p1.X + p1.dmx * w;
				*y1 = p1.Y + p1.dmy * w;
			}
		}

		private static Vertex* __roundJoin(Vertex* dst, NvgPoint p0, NvgPoint p1, float lw, float rw, float lu,
			float ru, int ncap, float fringe)
		{
			var i = 0;
			var n = 0;
			var dlx0 = p0.DeltaY;
			var dly0 = -p0.DeltaX;
			var dlx1 = p1.DeltaY;
			var dly1 = -p1.DeltaX;
			if ((p1.flags & (byte)PointFlags.Left) != 0)
			{
				float lx0 = 0;
				float ly0 = 0;
				float lx1 = 0;
				float ly1 = 0;
				float a0 = 0;
				float a1 = 0;
				__chooseBevel(p1.flags & (byte)PointFlags.InnerBevel, p0, p1, lw, &lx0, &ly0, &lx1, &ly1);
				a0 = NvgUtility.atan2f(-dly0, -dlx0);
				a1 = NvgUtility.atan2f(-dly1, -dlx1);
				if (a1 > a0)
					a1 -= (float)(3.14159274 * 2);
				__vset(dst, lx0, ly0, lu, 1);
				dst++;
				__vset(dst, p1.X - dlx0 * rw, p1.Y - dly0 * rw, ru, 1);
				dst++;
				n = NvgUtility.__clampi((int)NvgUtility.ceilf((float)((a0 - a1) / 3.14159274 * ncap)), 2, ncap);
				for (i = 0; i < n; i++)
				{
					var u = i / (float)(n - 1);
					var a = a0 + u * (a1 - a0);
					var rx = p1.X + NvgUtility.cosf(a) * rw;
					var ry = p1.Y + NvgUtility.sinf(a) * rw;
					__vset(dst, p1.X, p1.Y, 0.5f, 1);
					dst++;
					__vset(dst, rx, ry, ru, 1);
					dst++;
				}

				__vset(dst, lx1, ly1, lu, 1);
				dst++;
				__vset(dst, p1.X - dlx1 * rw, p1.Y - dly1 * rw, ru, 1);
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
				__chooseBevel(p1.flags & (byte)PointFlags.InnerBevel, p0, p1, -rw, &rx0, &ry0, &rx1, &ry1);
				a0 = NvgUtility.atan2f(dly0, dlx0);
				a1 = NvgUtility.atan2f(dly1, dlx1);
				if (a1 < a0)
					a1 += (float)(3.14159274 * 2);
				__vset(dst, p1.X + dlx0 * rw, p1.Y + dly0 * rw, lu, 1);
				dst++;
				__vset(dst, rx0, ry0, ru, 1);
				dst++;
				n = NvgUtility.__clampi((int)NvgUtility.ceilf((float)((a1 - a0) / 3.14159274 * ncap)), 2, ncap);
				for (i = 0; i < n; i++)
				{
					var u = i / (float)(n - 1);
					var a = a0 + u * (a1 - a0);
					var lx = p1.X + NvgUtility.cosf(a) * lw;
					var ly = p1.Y + NvgUtility.sinf(a) * lw;
					__vset(dst, lx, ly, lu, 1);
					dst++;
					__vset(dst, p1.X, p1.Y, 0.5f, 1);
					dst++;
				}

				__vset(dst, p1.X + dlx1 * rw, p1.Y + dly1 * rw, lu, 1);
				dst++;
				__vset(dst, rx1, ry1, ru, 1);
				dst++;
			}

			return dst;
		}

		private static Vertex* __bevelJoin(Vertex* dst, NvgPoint p0, NvgPoint p1, float lw, float rw, float lu,
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
			var dlx0 = p0.DeltaY;
			var dly0 = -p0.DeltaX;
			var dlx1 = p1.DeltaY;
			var dly1 = -p1.DeltaX;
			if ((p1.flags & (byte)PointFlags.Left) != 0)
			{
				__chooseBevel(p1.flags & (byte)PointFlags.InnerBevel, p0, p1, lw, &lx0, &ly0, &lx1, &ly1);
				__vset(dst, lx0, ly0, lu, 1);
				dst++;
				__vset(dst, p1.X - dlx0 * rw, p1.Y - dly0 * rw, ru, 1);
				dst++;
				if ((p1.flags & (byte)PointFlags.Bevel) != 0)
				{
					__vset(dst, lx0, ly0, lu, 1);
					dst++;
					__vset(dst, p1.X - dlx0 * rw, p1.Y - dly0 * rw, ru, 1);
					dst++;
					__vset(dst, lx1, ly1, lu, 1);
					dst++;
					__vset(dst, p1.X - dlx1 * rw, p1.Y - dly1 * rw, ru, 1);
					dst++;
				}
				else
				{
					rx0 = p1.X - p1.dmx * rw;
					ry0 = p1.Y - p1.dmy * rw;
					__vset(dst, p1.X, p1.Y, 0.5f, 1);
					dst++;
					__vset(dst, p1.X - dlx0 * rw, p1.Y - dly0 * rw, ru, 1);
					dst++;
					__vset(dst, rx0, ry0, ru, 1);
					dst++;
					__vset(dst, rx0, ry0, ru, 1);
					dst++;
					__vset(dst, p1.X, p1.Y, 0.5f, 1);
					dst++;
					__vset(dst, p1.X - dlx1 * rw, p1.Y - dly1 * rw, ru, 1);
					dst++;
				}

				__vset(dst, lx1, ly1, lu, 1);
				dst++;
				__vset(dst, p1.X - dlx1 * rw, p1.Y - dly1 * rw, ru, 1);
				dst++;
			}
			else
			{
				__chooseBevel(p1.flags & (byte)PointFlags.InnerBevel, p0, p1, -rw, &rx0, &ry0, &rx1, &ry1);
				__vset(dst, p1.X + dlx0 * lw, p1.Y + dly0 * lw, lu, 1);
				dst++;
				__vset(dst, rx0, ry0, ru, 1);
				dst++;
				if ((p1.flags & (byte)PointFlags.Bevel) != 0)
				{
					__vset(dst, p1.X + dlx0 * lw, p1.Y + dly0 * lw, lu, 1);
					dst++;
					__vset(dst, rx0, ry0, ru, 1);
					dst++;
					__vset(dst, p1.X + dlx1 * lw, p1.Y + dly1 * lw, lu, 1);
					dst++;
					__vset(dst, rx1, ry1, ru, 1);
					dst++;
				}
				else
				{
					lx0 = p1.X + p1.dmx * lw;
					ly0 = p1.Y + p1.dmy * lw;
					__vset(dst, p1.X + dlx0 * lw, p1.Y + dly0 * lw, lu, 1);
					dst++;
					__vset(dst, p1.X, p1.Y, 0.5f, 1);
					dst++;
					__vset(dst, lx0, ly0, lu, 1);
					dst++;
					__vset(dst, lx0, ly0, lu, 1);
					dst++;
					__vset(dst, p1.X + dlx1 * lw, p1.Y + dly1 * lw, lu, 1);
					dst++;
					__vset(dst, p1.X, p1.Y, 0.5f, 1);
					dst++;
				}

				__vset(dst, p1.X + dlx1 * lw, p1.Y + dly1 * lw, lu, 1);
				dst++;
				__vset(dst, rx1, ry1, ru, 1);
				dst++;
			}

			return dst;
		}

		private static Vertex* __buttCapStart(Vertex* dst, NvgPoint p, float dx, float dy, float w, float d, float aa,
			float u0, float u1)
		{
			var px = p.X - dx * d;
			var py = p.Y - dy * d;
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

		private static Vertex* __buttCapEnd(Vertex* dst, NvgPoint p, float dx, float dy, float w, float d, float aa,
			float u0, float u1)
		{
			var px = p.X + dx * d;
			var py = p.Y + dy * d;
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

		private static Vertex* __roundCapStart(Vertex* dst, NvgPoint p, float dx, float dy, float w, int ncap,
			float aa, float u0, float u1)
		{
			var px = p.X;
			var py = p.Y;
			var dlx = dy;
			var dly = -dx;
			for (var i = 0; i < ncap; i++)
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

		private static Vertex* __roundCapEnd(Vertex* dst, NvgPoint p, float dx, float dy, float w, int ncap, float aa,
			float u0, float u1)
		{
			var i = 0;
			var px = p.X;
			var py = p.Y;
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