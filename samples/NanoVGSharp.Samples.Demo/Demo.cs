using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace NanoVGSharp.Samples.Demo
{
	public class Demo
	{
		int fontNormal, fontBold, fontIcons;
		int[] images = new int[12];

		//static float minf(float a, float b) { return a < b ? a : b; }
		static float maxf(float a, float b)
		{
			return a > b ? a : b;
		}
		//static float absf(float a) { return a >= 0.0f ? a : -a; }
		static float clampf(float a, float mn, float mx)
		{
			return a < mn ? mn : (a > mx ? mx : a);
		}

		// Returns 1 if col.rgba is 0.0f,0.0f,0.0f,0.0f, 0 otherwise
		public static bool isBlack(Color col)
		{
			return col == Color.Transparent;
		}

		static int mini(int a, int b)
		{
			return a < b ? a : b;
		}


		public static void drawWindow(NanoVGContext vg, string title, float x, float y, float w, float h)
		{
			float cornerRadius = 3.0f;
			Paint shadowPaint;
			Paint headerPaint;

			vg.nvgSave();
			//	nvgClearState(vg);

			// Window
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x, y, w, h, cornerRadius);
			vg.nvgFillColor(new Color(28, 30, 34, 192));
			//	vg.nvgFillColor(new Color(0,0,0,128));
			vg.nvgFill();

			// Drop shadow
			shadowPaint = vg.nvgBoxGradient(x, y + 2, w, h, cornerRadius * 2, 10, new Color(0, 0, 0, 128), new Color(0, 0, 0, 0));
			vg.nvgBeginPath();
			vg.nvgRect(x - 10, y - 10, w + 20, h + 30);
			vg.nvgRoundedRect(x, y, w, h, cornerRadius);
			vg.nvgPathWinding(NanoVGContext.NVG_HOLE);
			vg.nvgFillPaint(shadowPaint);
			vg.nvgFill();

			// Header
			headerPaint = vg.nvgLinearGradient(x, y, x, y + 15, new Color(255, 255, 255, 8), new Color(0, 0, 0, 16));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 1, y + 1, w - 2, 30, cornerRadius - 1);
			vg.nvgFillPaint(headerPaint);
			vg.nvgFill();
			vg.nvgBeginPath();
			vg.nvgMoveTo(x + 0.5f, y + 0.5f + 30);
			vg.nvgLineTo(x + 0.5f + w - 1, y + 0.5f + 30);
			vg.nvgStrokeColor(new Color(0, 0, 0, 32));
			vg.nvgStroke();

			vg.nvgFontSize(18.0f);
			vg.nvgFontFace("sans-bold");
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_CENTER | NanoVGContext.NVG_ALIGN_MIDDLE);

			vg.nvgFontBlur(2);
			vg.nvgFillColor(new Color(0, 0, 0, 128));
			vg.nvgText(x + w / 2, y + 16 + 1, title);

			vg.nvgFontBlur(0);
			vg.nvgFillColor(new Color(220, 220, 220, 160));
			vg.nvgText(x + w / 2, y + 16, title);

			vg.nvgRestore();
		}

		public static void drawSearchBox(NanoVGContext vg, string text, float x, float y, float w, float h)
		{
			Paint bg;
			float cornerRadius = h / 2 - 1;

			// Edit
			bg = vg.nvgBoxGradient(x, y + 1.5f, w, h, h / 2, 5, new Color(0, 0, 0, 16), new Color(0, 0, 0, 92));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x, y, w, h, cornerRadius);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			/*	vg.nvgBeginPath();
				vg.nvgRoundedRect(x+0.5f,y+0.5f, w-1,h-1, cornerRadius-0.5f);
				vg.nvgStrokeColor(new Color(0,0,0,48));
				vg.nvgStroke();*/

			vg.nvgFontSize(h * 1.3f);
			vg.nvgFontFace("icons");
			vg.nvgFillColor(new Color(255, 255, 255, 64));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_CENTER | NanoVGContext.NVG_ALIGN_MIDDLE);

			vg.nvgFontSize(20.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 32));

			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x + h * 1.05f, y + h * 0.5f, text);

			vg.nvgFontSize(h * 1.3f);
			vg.nvgFontFace("icons");
			vg.nvgFillColor(new Color(255, 255, 255, 32));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_CENTER | NanoVGContext.NVG_ALIGN_MIDDLE);
		}

		public static void drawDropDown(NanoVGContext vg, string text, float x, float y, float w, float h)
		{
			Paint bg;
			float cornerRadius = 4.0f;

			bg = vg.nvgLinearGradient(x, y, x, y + h, new Color(255, 255, 255, 16), new Color(0, 0, 0, 16));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 1, y + 1, w - 2, h - 2, cornerRadius - 1);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 0.5f, y + 0.5f, w - 1, h - 1, cornerRadius - 0.5f);
			vg.nvgStrokeColor(new Color(0, 0, 0, 48));
			vg.nvgStroke();

			vg.nvgFontSize(20.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 160));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x + h * 0.3f, y + h * 0.5f, text);

			vg.nvgFontSize(h * 1.3f);
			vg.nvgFontFace("icons");
			vg.nvgFillColor(new Color(255, 255, 255, 64));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_CENTER | NanoVGContext.NVG_ALIGN_MIDDLE);
		}

		public static void drawLabel(NanoVGContext vg, string text, float x, float y, float w, float h)
		{
			vg.nvgFontSize(18.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 128));

			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x, y + h * 0.5f, text);
		}

		public static void drawEditBoxBase(NanoVGContext vg, float x, float y, float w, float h)
		{
			Paint bg;
			// Edit
			bg = vg.nvgBoxGradient(x + 1, y + 1 + 1.5f, w - 2, h - 2, 3, 4, new Color(255, 255, 255, 32), new Color(32, 32, 32, 32));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 1, y + 1, w - 2, h - 2, 4 - 1);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 0.5f, y + 0.5f, w - 1, h - 1, 4 - 0.5f);
			vg.nvgStrokeColor(new Color(0, 0, 0, 48));
			vg.nvgStroke();
		}

		public static void drawEditBox(NanoVGContext vg, string text, float x, float y, float w, float h)
		{

			drawEditBoxBase(vg, x, y, w, h);

			vg.nvgFontSize(20.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 64));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x + h * 0.3f, y + h * 0.5f, text);
		}

		public static void drawEditBoxNum(NanoVGContext vg,
							string text, string units, float x, float y, float w, float h)
		{
			float uw;

			drawEditBoxBase(vg, x, y, w, h);

			uw = vg.nvgTextBounds(0, 0, units, new Bounds());

			vg.nvgFontSize(18.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 64));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_RIGHT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x + w - h * 0.3f, y + h * 0.5f, units);

			vg.nvgFontSize(20.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 128));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_RIGHT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x + w - uw - h * 0.5f, y + h * 0.5f, text);
		}

		public static void drawCheckBox(NanoVGContext vg, string text, float x, float y, float w, float h)
		{
			Paint bg;

			vg.nvgFontSize(18.0f);
			vg.nvgFontFace("sans");
			vg.nvgFillColor(new Color(255, 255, 255, 160));

			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgText(x + 28, y + h * 0.5f, text);

			bg = vg.nvgBoxGradient(x + 1, y + (int)(h * 0.5f) - 9 + 1, 18, 18, 3, 3, new Color(0, 0, 0, 32), new Color(0, 0, 0, 92));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 1, y + (int)(h * 0.5f) - 9, 18, 18, 3);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			vg.nvgFontSize(40);
			vg.nvgFontFace("icons");
			vg.nvgFillColor(new Color(255, 255, 255, 128));
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_CENTER | NanoVGContext.NVG_ALIGN_MIDDLE);
		}

		public static void drawButton(NanoVGContext vg, int preicon, string text, float x, float y, float w, float h, Color col)
		{
			Paint bg;
			float cornerRadius = 4.0f;
			float tw = 0, iw = 0;

			bg = vg.nvgLinearGradient(x, y, x, y + h, new Color(255, 255, 255, isBlack(col) ? 16 : 32), new Color(0, 0, 0, isBlack(col) ? 16 : 32));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 1, y + 1, w - 2, h - 2, cornerRadius - 1);
			if (!isBlack(col))
			{
				vg.nvgFillColor(col);
				vg.nvgFill();
			}
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + 0.5f, y + 0.5f, w - 1, h - 1, cornerRadius - 0.5f);
			vg.nvgStrokeColor(new Color(0, 0, 0, 48));
			vg.nvgStroke();

			vg.nvgFontSize(20.0f);
			vg.nvgFontFace("sans-bold");
			tw = vg.nvgTextBounds(0, 0, text, new Bounds());
			if (preicon != 0)
			{
				vg.nvgFontSize(h * 1.3f);
				vg.nvgFontFace("icons");
				iw += h * 0.15f;
			}

			if (preicon != 0)
			{
				vg.nvgFontSize(h * 1.3f);
				vg.nvgFontFace("icons");
				vg.nvgFillColor(new Color(255, 255, 255, 96));
				vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			}

			vg.nvgFontSize(20.0f);
			vg.nvgFontFace("sans-bold");
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_MIDDLE);
			vg.nvgFillColor(new Color(0, 0, 0, 160));
			vg.nvgText(x + w * 0.5f - tw * 0.5f + iw * 0.25f, y + h * 0.5f - 1, text);
			vg.nvgFillColor(new Color(255, 255, 255, 160));
			vg.nvgText(x + w * 0.5f - tw * 0.5f + iw * 0.25f, y + h * 0.5f, text);
		}

		public static void drawSlider(NanoVGContext vg, float pos, float x, float y, float w, float h)
		{
			Paint bg, knob;
			float cy = y + (int)(h * 0.5f);
			float kr = (int)(h * 0.25f);

			vg.nvgSave();
			//	nvgClearState(vg);

			// Slot
			bg = vg.nvgBoxGradient(x, cy - 2 + 1, w, 4, 2, 2, new Color(0, 0, 0, 32), new Color(0, 0, 0, 128));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x, cy - 2, w, 4, 2);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			// Knob Shadow
			bg = vg.nvgRadialGradient(x + (int)(pos * w), cy + 1, kr - 3, kr + 3, new Color(0, 0, 0, 64), new Color(0, 0, 0, 0));
			vg.nvgBeginPath();
			vg.nvgRect(x + (int)(pos * w) - kr - 5, cy - kr - 5, kr * 2 + 5 + 5, kr * 2 + 5 + 5 + 3);
			vg.nvgCircle(x + (int)(pos * w), cy, kr);
			vg.nvgPathWinding(NanoVGContext.NVG_HOLE);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			// Knob
			knob = vg.nvgLinearGradient(x, cy - kr, x, cy + kr, new Color(255, 255, 255, 16), new Color(0, 0, 0, 16));
			vg.nvgBeginPath();
			vg.nvgCircle(x + (int)(pos * w), cy, kr - 1);
			vg.nvgFillColor(new Color(40, 43, 48, 255));
			vg.nvgFill();
			vg.nvgFillPaint(knob);
			vg.nvgFill();

			vg.nvgBeginPath();
			vg.nvgCircle(x + (int)(pos * w), cy, kr - 0.5f);
			vg.nvgStrokeColor(new Color(0, 0, 0, 92));
			vg.nvgStroke();

			vg.nvgRestore();
		}

		public static void drawEyes(NanoVGContext vg, float x, float y, float w, float h, float mx, float my, float t)
		{
			Paint gloss, bg;
			float ex = w * 0.23f;
			float ey = h * 0.5f;
			float lx = x + ex;
			float ly = y + ey;
			float rx = x + w - ex;
			float ry = y + ey;
			float dx, dy, d;
			float br = (ex < ey ? ex : ey) * 0.5f;
			float blink = 1 - (float)(Math.Pow(Math.Sin(t * 0.5f), 200) * 0.8f);

			bg = vg.nvgLinearGradient(x, y + h * 0.5f, x + w * 0.1f, y + h, new Color(0, 0, 0, 32), new Color(0, 0, 0, 16));
			vg.nvgBeginPath();
			vg.nvgEllipse(lx + 3.0f, ly + 16.0f, ex, ey);
			vg.nvgEllipse(rx + 3.0f, ry + 16.0f, ex, ey);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			bg = vg.nvgLinearGradient(x, y + h * 0.25f, x + w * 0.1f, y + h, new Color(220, 220, 220, 255), new Color(128, 128, 128, 255));
			vg.nvgBeginPath();
			vg.nvgEllipse(lx, ly, ex, ey);
			vg.nvgEllipse(rx, ry, ex, ey);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			dx = (mx - rx) / (ex * 10);
			dy = (my - ry) / (ey * 10);
			d = (float)Math.Sqrt(dx * dx + dy * dy);
			if (d > 1.0f)
			{
				dx /= d;
				dy /= d;
			}
			dx *= ex * 0.4f;
			dy *= ey * 0.5f;
			vg.nvgBeginPath();
			vg.nvgEllipse(lx + dx, ly + dy + ey * 0.25f * (1 - blink), br, br * blink);
			vg.nvgFillColor(new Color(32, 32, 32, 255));
			vg.nvgFill();

			dx = (mx - rx) / (ex * 10);
			dy = (my - ry) / (ey * 10);
			d = (float)Math.Sqrt(dx * dx + dy * dy);
			if (d > 1.0f)
			{
				dx /= d;
				dy /= d;
			}
			dx *= ex * 0.4f;
			dy *= ey * 0.5f;
			vg.nvgBeginPath();
			vg.nvgEllipse(rx + dx, ry + dy + ey * 0.25f * (1 - blink), br, br * blink);
			vg.nvgFillColor(new Color(32, 32, 32, 255));
			vg.nvgFill();

			gloss = vg.nvgRadialGradient(lx - ex * 0.25f, ly - ey * 0.5f, ex * 0.1f, ex * 0.75f, new Color(255, 255, 255, 128), new Color(255, 255, 255, 0));
			vg.nvgBeginPath();
			vg.nvgEllipse(lx, ly, ex, ey);
			vg.nvgFillPaint(gloss);
			vg.nvgFill();

			gloss = vg.nvgRadialGradient(rx - ex * 0.25f, ry - ey * 0.5f, ex * 0.1f, ex * 0.75f, new Color(255, 255, 255, 128), new Color(255, 255, 255, 0));
			vg.nvgBeginPath();
			vg.nvgEllipse(rx, ry, ex, ey);
			vg.nvgFillPaint(gloss);
			vg.nvgFill();
		}

		public static void drawGraph(NanoVGContext vg, float x, float y, float w, float h, float t)
		{
			Paint bg;
			float[] samples = new float[6];
			float[] sx = new float[6], sy = new float[6];
			float dx = w / 5.0f;
			int i;

			samples[0] = (1 + (float)Math.Sin(t * 1.2345f + (float)Math.Cos(t * 0.33457f) * 0.44f)) * 0.5f;
			samples[1] = (1 + (float)Math.Sin(t * 0.68363f + (float)Math.Cos(t * 1.3f) * 1.55f)) * 0.5f;
			samples[2] = (1 + (float)Math.Sin(t * 1.1642f + (float)Math.Cos(t * 0.33457) * 1.24f)) * 0.5f;
			samples[3] = (1 + (float)Math.Sin(t * 0.56345f + (float)Math.Cos(t * 1.63f) * 0.14f)) * 0.5f;
			samples[4] = (1 + (float)Math.Sin(t * 1.6245f + (float)Math.Cos(t * 0.254f) * 0.3f)) * 0.5f;
			samples[5] = (1 + (float)Math.Sin(t * 0.345f + (float)Math.Cos(t * 0.03f) * 0.6f)) * 0.5f;

			for (i = 0; i < 6; i++)
			{
				sx[i] = x + i * dx;
				sy[i] = y + h * samples[i] * 0.8f;
			}

			// Graph background
			bg = vg.nvgLinearGradient(x, y, x, y + h, new Color(0, 160, 192, 0), new Color(0, 160, 192, 64));
			vg.nvgBeginPath();
			vg.nvgMoveTo(sx[0], sy[0]);
			for (i = 1; i < 6; i++)
				vg.nvgBezierTo(sx[i - 1] + dx * 0.5f, sy[i - 1], sx[i] - dx * 0.5f, sy[i], sx[i], sy[i]);
			vg.nvgLineTo(x + w, y + h);
			vg.nvgLineTo(x, y + h);
			vg.nvgFillPaint(bg);
			vg.nvgFill();

			// Graph line
			vg.nvgBeginPath();
			vg.nvgMoveTo(sx[0], sy[0] + 2);
			for (i = 1; i < 6; i++)
				vg.nvgBezierTo(sx[i - 1] + dx * 0.5f, sy[i - 1] + 2, sx[i] - dx * 0.5f, sy[i] + 2, sx[i], sy[i] + 2);
			vg.nvgStrokeColor(new Color(0, 0, 0, 32));
			vg.nvgStrokeWidth(3.0f);
			vg.nvgStroke();

			vg.nvgBeginPath();
			vg.nvgMoveTo(sx[0], sy[0]);
			for (i = 1; i < 6; i++)
				vg.nvgBezierTo(sx[i - 1] + dx * 0.5f, sy[i - 1], sx[i] - dx * 0.5f, sy[i], sx[i], sy[i]);
			vg.nvgStrokeColor(new Color(0, 160, 192, 255));
			vg.nvgStrokeWidth(3.0f);
			vg.nvgStroke();

			// Graph sample pos
			for (i = 0; i < 6; i++)
			{
				bg = vg.nvgRadialGradient(sx[i], sy[i] + 2, 3.0f, 8.0f, new Color(0, 0, 0, 32), new Color(0, 0, 0, 0));
				vg.nvgBeginPath();
				vg.nvgRect(sx[i] - 10, sy[i] - 10 + 2, 20, 20);
				vg.nvgFillPaint(bg);
				vg.nvgFill();
			}

			vg.nvgBeginPath();
			for (i = 0; i < 6; i++)
				vg.nvgCircle(sx[i], sy[i], 4.0f);
			vg.nvgFillColor(new Color(0, 160, 192, 255));
			vg.nvgFill();
			vg.nvgBeginPath();
			for (i = 0; i < 6; i++)
				vg.nvgCircle(sx[i], sy[i], 2.0f);
			vg.nvgFillColor(new Color(220, 220, 220, 255));
			vg.nvgFill();

			vg.nvgStrokeWidth(1.0f);
		}

		public static void drawSpinner(NanoVGContext vg, float cx, float cy, float r, float t)
		{
			float a0 = 0.0f + t * 6;
			float a1 = (float)Math.PI + t * 6;
			float r0 = r;
			float r1 = r * 0.75f;
			float ax, ay, bx, by;
			Paint paint;

			vg.nvgSave();

			vg.nvgBeginPath();
			vg.nvgArc(cx, cy, r0, a0, a1, NanoVGContext.NVG_CW);
			vg.nvgArc(cx, cy, r1, a1, a0, NanoVGContext.NVG_CCW);
			vg.nvgClosePath();
			ax = cx + (float)Math.Cos(a0) * (r0 + r1) * 0.5f;
			ay = cy + (float)Math.Sin(a0) * (r0 + r1) * 0.5f;
			bx = cx + (float)Math.Cos(a1) * (r0 + r1) * 0.5f;
			by = cy + (float)Math.Sin(a1) * (r0 + r1) * 0.5f;
			paint = vg.nvgLinearGradient(ax, ay, bx, by, new Color(0, 0, 0, 0), new Color(0, 0, 0, 128));
			vg.nvgFillPaint(paint);
			vg.nvgFill();

			vg.nvgRestore();
		}

		public static void drawThumbnails(NanoVGContext vg, float x, float y, float w, float h, int[] images, float t)
		{
			float cornerRadius = 3.0f;
			Paint shadowPaint, imgPaint, fadePaint;
			float ix, iy, iw, ih;
			float thumb = 60.0f;
			float arry = 30.5f;
			int imgw, imgh;
			float stackh = (images.Length / 2) * (thumb + 10) + 10;
			int i;
			float u = (1 + (float)Math.Cos(t * 0.5f)) * 0.5f;
			float u2 = (1 - (float)Math.Cos(t * 0.2f)) * 0.5f;
			float scrollh, dv;

			vg.nvgSave();
			//	nvgClearState(vg);

			// Drop shadow
			shadowPaint = vg.nvgBoxGradient(x, y + 4, w, h, cornerRadius * 2, 20, new Color(0, 0, 0, 128), new Color(0, 0, 0, 0));
			vg.nvgBeginPath();
			vg.nvgRect(x - 10, y - 10, w + 20, h + 30);
			vg.nvgRoundedRect(x, y, w, h, cornerRadius);
			vg.nvgPathWinding(NanoVGContext.NVG_HOLE);
			vg.nvgFillPaint(shadowPaint);
			vg.nvgFill();

			// Window
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x, y, w, h, cornerRadius);
			vg.nvgMoveTo(x - 10, y + arry);
			vg.nvgLineTo(x + 1, y + arry - 11);
			vg.nvgLineTo(x + 1, y + arry + 11);
			vg.nvgFillColor(new Color(200, 200, 200, 255));
			vg.nvgFill();

			vg.nvgSave();
			vg.nvgScissor(x, y, w, h);
			vg.nvgTranslate(0, -(stackh - h) * u);

			dv = 1.0f / (float)(images.Length - 1);

			for (i = 0; i < images.Length; i++)
			{
				float tx, ty, v, a;
				tx = x + 10;
				ty = y + 10;
				tx += (i % 2) * (thumb + 10);
				ty += (i / 2) * (thumb + 10);
				vg.nvgImageSize(images[i], out imgw, out imgh);
				if (imgw < imgh)
				{
					iw = thumb;
					ih = iw * (float)imgh / (float)imgw;
					ix = 0;
					iy = -(ih - thumb) * 0.5f;
				}
				else
				{
					ih = thumb;
					iw = ih * (float)imgw / (float)imgh;
					ix = -(iw - thumb) * 0.5f;
					iy = 0;
				}

				v = i * dv;
				a = clampf((u2 - v) / dv, 0, 1);

				if (a < 1.0f)
					drawSpinner(vg, tx + thumb / 2, ty + thumb / 2, thumb * 0.25f, t);

				imgPaint = vg.nvgImagePattern(tx + ix, ty + iy, iw, ih, 0.0f / 180.0f * (float)Math.PI, images[i], a);
				vg.nvgBeginPath();
				vg.nvgRoundedRect(tx, ty, thumb, thumb, 5);
				vg.nvgFillPaint(imgPaint);
				vg.nvgFill();

				shadowPaint = vg.nvgBoxGradient(tx - 1, ty, thumb + 2, thumb + 2, 5, 3, new Color(0, 0, 0, 128), new Color(0, 0, 0, 0));
				vg.nvgBeginPath();
				vg.nvgRect(tx - 5, ty - 5, thumb + 10, thumb + 10);
				vg.nvgRoundedRect(tx, ty, thumb, thumb, 6);
				vg.nvgPathWinding(NanoVGContext.NVG_HOLE);
				vg.nvgFillPaint(shadowPaint);
				vg.nvgFill();

				vg.nvgBeginPath();
				vg.nvgRoundedRect(tx + 0.5f, ty + 0.5f, thumb - 1, thumb - 1, 4 - 0.5f);
				vg.nvgStrokeWidth(1.0f);
				vg.nvgStrokeColor(new Color(255, 255, 255, 192));
				vg.nvgStroke();
			}
			vg.nvgRestore();

			// Hide fades
			fadePaint = vg.nvgLinearGradient(x, y, x, y + 6, new Color(200, 200, 200, 255), new Color(200, 200, 200, 0));
			vg.nvgBeginPath();
			vg.nvgRect(x + 4, y, w - 8, 6);
			vg.nvgFillPaint(fadePaint);
			vg.nvgFill();

			fadePaint = vg.nvgLinearGradient(x, y + h, x, y + h - 6, new Color(200, 200, 200, 255), new Color(200, 200, 200, 0));
			vg.nvgBeginPath();
			vg.nvgRect(x + 4, y + h - 6, w - 8, 6);
			vg.nvgFillPaint(fadePaint);
			vg.nvgFill();

			// Scroll bar
			shadowPaint = vg.nvgBoxGradient(x + w - 12 + 1, y + 4 + 1, 8, h - 8, 3, 4, new Color(0, 0, 0, 32), new Color(0, 0, 0, 92));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + w - 12, y + 4, 8, h - 8, 3);
			vg.nvgFillPaint(shadowPaint);
			//	vg.nvgFillColor(new Color(255,0,0,128));
			vg.nvgFill();

			scrollh = (h / stackh) * (h - 8);
			shadowPaint = vg.nvgBoxGradient(x + w - 12 - 1, y + 4 + (h - 8 - scrollh) * u - 1, 8, scrollh, 3, 4, new Color(220, 220, 220, 255), new Color(128, 128, 128, 255));
			vg.nvgBeginPath();
			vg.nvgRoundedRect(x + w - 12 + 1, y + 4 + 1 + (h - 8 - scrollh) * u, 8 - 2, scrollh - 2, 2);
			vg.nvgFillPaint(shadowPaint);
			//	vg.nvgFillColor(new Color(0,0,0,128));
			vg.nvgFill();

			vg.nvgRestore();
		}

		public static void drawColorwheel(NanoVGContext vg, float x, float y, float w, float h, float t)
		{
			int i;
			float r0, r1, ax, ay, bx, by, cx, cy, aeps, r;
			float hue = (float)Math.Sin(t * 0.12f);
			Paint paint;

			vg.nvgSave();

			/*	vg.nvgBeginPath();
				vg.nvgRect(x,y,w,h);
				vg.nvgFillColor(new Color(255,0,0,128));
				vg.nvgFill();*/

			cx = x + w * 0.5f;
			cy = y + h * 0.5f;
			r1 = (w < h ? w : h) * 0.5f - 5.0f;
			r0 = r1 - 20.0f;
			aeps = 0.5f / r1;   // half a pixel arc length in radians (2pi cancels out).

			for (i = 0; i < 6; i++)
			{
				float a0 = (float)i / 6.0f * (float)Math.PI * 2.0f - aeps;
				float a1 = (float)(i + 1.0f) / 6.0f * (float)Math.PI * 2.0f + aeps;
				vg.nvgBeginPath();
				vg.nvgArc(cx, cy, r0, a0, a1, NanoVGContext.NVG_CW);
				vg.nvgArc(cx, cy, r1, a1, a0, NanoVGContext.NVG_CCW);
				vg.nvgClosePath();
				ax = cx + (float)Math.Cos(a0) * (r0 + r1) * 0.5f;
				ay = cy + (float)Math.Sin(a0) * (r0 + r1) * 0.5f;
				bx = cx + (float)Math.Cos(a1) * (r0 + r1) * 0.5f;
				by = cy + (float)Math.Sin(a1) * (r0 + r1) * 0.5f;
				paint = vg.nvgLinearGradient(ax, ay, bx, by, NanoVGContext.nvgHSLA(a0 / ((float)Math.PI * 2), 1.0f, 0.55f, 255), NanoVGContext.nvgHSLA(a1 / ((float)Math.PI * 2), 1.0f, 0.55f, 255));
				vg.nvgFillPaint(paint);
				vg.nvgFill();
			}

			vg.nvgBeginPath();
			vg.nvgCircle(cx, cy, r0 - 0.5f);
			vg.nvgCircle(cx, cy, r1 + 0.5f);
			vg.nvgStrokeColor(new Color(0, 0, 0, 64));
			vg.nvgStrokeWidth(1.0f);
			vg.nvgStroke();

			// Selector
			vg.nvgSave();
			vg.nvgTranslate(cx, cy);
			vg.nvgRotate(hue * (float)Math.PI * 2);

			// Marker on
			vg.nvgStrokeWidth(2.0f);
			vg.nvgBeginPath();
			vg.nvgRect(r0 - 1, -3, r1 - r0 + 2, 6);
			vg.nvgStrokeColor(new Color(255, 255, 255, 192));
			vg.nvgStroke();

			paint = vg.nvgBoxGradient(r0 - 3, -5, r1 - r0 + 6, 10, 2, 4, new Color(0, 0, 0, 128), new Color(0, 0, 0, 0));
			vg.nvgBeginPath();
			vg.nvgRect(r0 - 2 - 10, -4 - 10, r1 - r0 + 4 + 20, 8 + 20);
			vg.nvgRect(r0 - 2, -4, r1 - r0 + 4, 8);
			vg.nvgPathWinding(NanoVGContext.NVG_HOLE);
			vg.nvgFillPaint(paint);
			vg.nvgFill();

			// Center triangle
			r = r0 - 6;
			ax = (float)Math.Cos(120.0f / 180.0f * (float)Math.PI) * r;
			ay = (float)Math.Sin(120.0f / 180.0f * (float)Math.PI) * r;
			bx = (float)Math.Cos(-120.0f / 180.0f * (float)Math.PI) * r;
			by = (float)Math.Sin(-120.0f / 180.0f * (float)Math.PI) * r;
			vg.nvgBeginPath();
			vg.nvgMoveTo(r, 0);
			vg.nvgLineTo(ax, ay);
			vg.nvgLineTo(bx, by);
			vg.nvgClosePath();
			paint = vg.nvgLinearGradient(r, 0, ax, ay, NanoVGContext.nvgHSLA(hue, 1.0f, 0.5f, 255), new Color(255, 255, 255, 255));
			vg.nvgFillPaint(paint);
			vg.nvgFill();
			paint = vg.nvgLinearGradient((r + ax) * 0.5f, (0 + ay) * 0.5f, bx, by, new Color(0, 0, 0, 0), new Color(0, 0, 0, 255));
			vg.nvgFillPaint(paint);
			vg.nvgFill();
			vg.nvgStrokeColor(new Color(0, 0, 0, 64));
			vg.nvgStroke();

			// Select circle on triangle
			ax = (float)Math.Cos(120.0f / 180.0f * (float)Math.PI) * r * 0.3f;
			ay = (float)Math.Sin(120.0f / 180.0f * (float)Math.PI) * r * 0.4f;
			vg.nvgStrokeWidth(2.0f);
			vg.nvgBeginPath();
			vg.nvgCircle(ax, ay, 5);
			vg.nvgStrokeColor(new Color(255, 255, 255, 192));
			vg.nvgStroke();

			paint = vg.nvgRadialGradient(ax, ay, 7, 9, new Color(0, 0, 0, 64), new Color(0, 0, 0, 0));
			vg.nvgBeginPath();
			vg.nvgRect(ax - 20, ay - 20, 40, 40);
			vg.nvgCircle(ax, ay, 7);
			vg.nvgPathWinding(NanoVGContext.NVG_HOLE);
			vg.nvgFillPaint(paint);
			vg.nvgFill();

			vg.nvgRestore();

			vg.nvgRestore();
		}

		public static void drawLines(NanoVGContext vg, float x, float y, float w, float h, float t)
		{
			int i, j;
			float pad = 5.0f, s = w / 9.0f - pad * 2;
			float[] pts = new float[4 * 2];
			float fx, fy;
			int[] joins = new int[] { NanoVGContext.NVG_MITER, NanoVGContext.NVG_ROUND, NanoVGContext.NVG_BEVEL };
			int[] caps = new int[] { NanoVGContext.NVG_BUTT, NanoVGContext.NVG_ROUND, NanoVGContext.NVG_SQUARE };

			vg.nvgSave();
			pts[0] = -s * 0.25f + (float)Math.Cos(t * 0.3f) * s * 0.5f;
			pts[1] = (float)Math.Sin(t * 0.3f) * s * 0.5f;
			pts[2] = -s * 0.25f;
			pts[3] = 0;
			pts[4] = s * 0.25f;
			pts[5] = 0;
			pts[6] = s * 0.25f + (float)Math.Cos(-t * 0.3f) * s * 0.5f;
			pts[7] = (float)Math.Sin(-t * 0.3f) * s * 0.5f;

			for (i = 0; i < 3; i++)
			{
				for (j = 0; j < 3; j++)
				{
					fx = x + s * 0.5f + (i * 3 + j) / 9.0f * w + pad;
					fy = y - s * 0.5f + pad;

					vg.nvgLineCap(caps[i]);
					vg.nvgLineJoin(joins[j]);

					vg.nvgStrokeWidth(s * 0.3f);
					vg.nvgStrokeColor(new Color(0, 0, 0, 160));
					vg.nvgBeginPath();
					vg.nvgMoveTo(fx + pts[0], fy + pts[1]);
					vg.nvgLineTo(fx + pts[2], fy + pts[3]);
					vg.nvgLineTo(fx + pts[4], fy + pts[5]);
					vg.nvgLineTo(fx + pts[6], fy + pts[7]);
					vg.nvgStroke();

					vg.nvgLineCap(NanoVGContext.NVG_BUTT);
					vg.nvgLineJoin(NanoVGContext.NVG_BEVEL);

					vg.nvgStrokeWidth(1.0f);
					vg.nvgStrokeColor(new Color(0, 192, 255, 255));
					vg.nvgBeginPath();
					vg.nvgMoveTo(fx + pts[0], fy + pts[1]);
					vg.nvgLineTo(fx + pts[2], fy + pts[3]);
					vg.nvgLineTo(fx + pts[4], fy + pts[5]);
					vg.nvgLineTo(fx + pts[6], fy + pts[7]);
					vg.nvgStroke();
				}
			}

			vg.nvgRestore();
		}

		private static int LoadFont(NanoVGContext vg, string name, string path)
		{
			byte[] data;
			var ms = new MemoryStream();
			using (var stream = TitleContainer.OpenStream(path))
			{
				stream.CopyTo(ms);

				data = ms.ToArray();
			}

			return vg.nvgCreateFontMem(name, data, 0);
		}

		public int loadDemoData(GraphicsDevice device, NanoVGContext vg)
		{
			int i;

			if (vg == null)
				return -1;

			for (i = 0; i < 12; i++)
			{
				var path = "Assets/images/image" + (i + 1).ToString() + ".jpg";

				int width, height;
				byte[] data;
				using (var stream = File.OpenRead(path))
				{
					var texture = Texture2D.FromStream(device, stream);

					width = texture.Width;
					height = texture.Height;
					data = new byte[texture.Width * texture.Height * 4];
					texture.GetData(data);
				}

				images[i] = vg.nvgCreateImageRGBA(width, height, 0, data);
			}

			fontIcons = LoadFont(vg, "icons", "Assets/entypo.ttf");
			fontNormal = LoadFont(vg, "sans", "Assets/Roboto-Regular.ttf");
			fontBold = LoadFont(vg, "sans-bold", "Assets/Roboto-Bold.ttf");

			return 0;
		}

		public static void drawParagraph(NanoVGContext vg, float x, float y, float width, float height, float mx, float my)
		{
			TextRow[] rows = new TextRow[3];
			GlyphPosition[] glyphs = new GlyphPosition[100];
			string text = "This is longer chunk of text.\n  \n  Would have used lorem ipsum but she    was busy jumping over the lazy dog with the fox and all the men who came to the aid of the party.";
			StringSegment start;
			int nrows, i, nglyphs, j, lnum = 0;
			float lineh;
			float caretx, px;
			Bounds bounds = new Bounds();
			float a;
			float gx = 0, gy = 0;
			int gutter = 0;

			for (i = 0; i < rows.Length; ++i)
			{
				rows[i] = new TextRow();
			}

			vg.nvgSave();

			vg.nvgFontSize(18.0f);
			vg.nvgFontFace("sans");
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_TOP);

			float ascender, descender;
			vg.nvgTextMetrics(out ascender, out descender, out lineh);

			// The text break API can be used to fill a large buffer of rows,
			// or to iterate over the text just few lines (or just one) at a time.
			// The "next" variable of the last returned item tells where to continue.
			start = text;
			while (true)
			{
				nrows = vg.nvgTextBreakLines(start, width, rows, out start);

				if (nrows <= 0)
				{
					break;
				}
				for (i = 0; i < nrows; i++)
				{
					TextRow row = rows[i];
					var hit = mx > x && mx < (x + width) && my >= y && my < (y + lineh);

					vg.nvgBeginPath();
					vg.nvgFillColor(new Color(255, 255, 255, hit ? 64 : 16));
					vg.nvgRect(x, y, row.width, lineh);
					vg.nvgFill();

					vg.nvgFillColor(new Color(255, 255, 255, 255));
					vg.nvgText(x, y, row.str);

					if (hit)
					{
						caretx = (mx < x + row.width / 2) ? x : x + row.width;
						px = x;
						nglyphs = vg.nvgTextGlyphPositions(x, y, row.str, glyphs);
						for (j = 0; j < nglyphs; j++)
						{
							float x0 = glyphs[j].x;
							float x1 = (j + 1 < nglyphs) ? glyphs[j + 1].x : x + row.width;
							float gx2 = x0 * 0.3f + x1 * 0.7f;
							if (mx >= px && mx < gx2)
								caretx = glyphs[j].x;
							px = gx2;
						}
						vg.nvgBeginPath();
						vg.nvgFillColor(new Color(255, 192, 0, 255));
						vg.nvgRect(caretx, y, 1, lineh);
						vg.nvgFill();

						gutter = lnum + 1;
						gx = x - 10;
						gy = y + lineh / 2;
					}
					lnum++;
					y += lineh;
				}
			}

			if (gutter > 0)
			{
				string txt = gutter.ToString();
				vg.nvgFontSize(13.0f);
				vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_RIGHT | NanoVGContext.NVG_ALIGN_MIDDLE);

				vg.nvgTextBounds(gx, gy, txt, bounds);

				vg.nvgBeginPath();
				vg.nvgFillColor(new Color(255, 192, 0, 255));
				vg.nvgRoundedRect((int)bounds.b1 - 4, (int)bounds.b2 - 2, 
					(int)(bounds.b3 - bounds.b1) + 8, 
					(int)(bounds.b4 - bounds.b2) + 4, 
					((int)(bounds.b4 - bounds.b2) + 4) / 2 - 1);
				vg.nvgFill();

				vg.nvgFillColor(new Color(32, 32, 32, 255));
				vg.nvgText(gx, gy, txt);
			}

			y += 20.0f;

			vg.nvgFontSize(13.0f);
			vg.nvgTextAlign(NanoVGContext.NVG_ALIGN_LEFT | NanoVGContext.NVG_ALIGN_TOP);
			vg.nvgTextLineHeight(1.2f);

			vg.nvgTextBoxBounds(x, y, 150, "Hover your mouse over the text to see calculated caret position.", bounds);

			// Fade the tooltip out when close to it.
			gx = (float)Math.Abs((mx - (bounds.b1 + bounds.b3) * 0.5f) / (bounds.b1 - bounds.b3));
			gy = (float)Math.Abs((my - (bounds.b2 + bounds.b4) * 0.5f) / (bounds.b2 - bounds.b4));
			a = maxf(gx, gy) - 0.5f;
			a = clampf(a, 0, 1);
			vg.nvgGlobalAlpha(a);

			vg.nvgBeginPath();
			vg.nvgFillColor(new Color(220, 220, 220, 255));
			vg.nvgRoundedRect(bounds.b1 - 2, bounds.b2 - 2, (int)(bounds.b3 - bounds.b1) + 4, (int)(bounds.b4 - bounds.b2) + 4, 3);
			px = (int)((bounds.b3 + bounds.b1) / 2);
			vg.nvgMoveTo(px, bounds.b2 - 10);
			vg.nvgLineTo(px + 7, bounds.b2 + 1);
			vg.nvgLineTo(px - 7, bounds.b2 + 1);
			vg.nvgFill();

			vg.nvgFillColor(new Color(0, 0, 0, 220));
			vg.nvgTextBox(x, y, 150, "Hover your mouse over the text to see calculated caret position.");

			vg.nvgRestore();
		}

		public static void drawWidths(NanoVGContext vg, float x, float y, float width)
		{
			int i;

			vg.nvgSave();

			vg.nvgStrokeColor(new Color(0, 0, 0, 255));

			for (i = 0; i < 20; i++)
			{
				float w = (i + 0.5f) * 0.1f;
				vg.nvgStrokeWidth(w);
				vg.nvgBeginPath();
				vg.nvgMoveTo(x, y);
				vg.nvgLineTo(x + width, y + width * 0.3f);
				vg.nvgStroke();
				y += 10;
			}

			vg.nvgRestore();
		}

		public static void drawCaps(NanoVGContext vg, float x, float y, float width)
		{
			int i;
			int[] caps = new[] { NanoVGContext.NVG_BUTT, NanoVGContext.NVG_ROUND, NanoVGContext.NVG_SQUARE };
			float lineWidth = 8.0f;

			vg.nvgSave();

			vg.nvgBeginPath();
			vg.nvgRect(x - lineWidth / 2, y, width + lineWidth, 40);
			vg.nvgFillColor(new Color(255, 255, 255, 32));
			vg.nvgFill();

			vg.nvgBeginPath();
			vg.nvgRect(x, y, width, 40);
			vg.nvgFillColor(new Color(255, 255, 255, 32));
			vg.nvgFill();

			vg.nvgStrokeWidth(lineWidth);
			for (i = 0; i < 3; i++)
			{
				vg.nvgLineCap(caps[i]);
				vg.nvgStrokeColor(new Color(0, 0, 0, 255));
				vg.nvgBeginPath();
				vg.nvgMoveTo(x, y + i * 10 + 5);
				vg.nvgLineTo(x + width, y + i * 10 + 5);
				vg.nvgStroke();
			}

			vg.nvgRestore();
		}

		public static void drawScissor(NanoVGContext vg, float x, float y, float t)
		{
			vg.nvgSave();

			// Draw first rect and set scissor to it's area.
			vg.nvgTranslate(x, y);
			vg.nvgRotate(NanoVGContext.nvgDegToRad(5));
			vg.nvgBeginPath();
			vg.nvgRect(-20, -20, 60, 40);
			vg.nvgFillColor(new Color(255, 0, 0, 255));
			vg.nvgFill();
			vg.nvgScissor(-20, -20, 60, 40);

			// Draw second rectangle with offset and rotation.
			vg.nvgTranslate(40, 0);
			vg.nvgRotate(t);

			// Draw the intended second rectangle without any scissoring.
			vg.nvgSave();
			vg.nvgResetScissor();
			vg.nvgBeginPath();
			vg.nvgRect(-20, -10, 60, 30);
			vg.nvgFillColor(new Color(255, 128, 0, 64));
			vg.nvgFill();
			vg.nvgRestore();

			// Draw second rectangle with combined scissoring.
			vg.nvgIntersectScissor(-20, -10, 60, 30);
			vg.nvgBeginPath();
			vg.nvgRect(-20, -10, 60, 30);
			vg.nvgFillColor(new Color(255, 128, 0, 255));
			vg.nvgFill();

			vg.nvgRestore();
		}

		public void renderDemo(NanoVGContext vg, float mx, float my, float width, float height,
						float t, bool blowup)
		{
			float x, y, popy;

			drawEyes(vg, width - 250, 50, 150, 100, mx, my, t);

			drawParagraph(vg, width - 450, 50, 150, 100, mx, my);
			drawGraph(vg, 0, height / 2, width, height / 2, t);
			drawColorwheel(vg, width - 300, height - 300, 250.0f, 250.0f, t);

			// Line joints
			drawLines(vg, 120, height - 50, 600, 50, t);

			// Line caps
			drawWidths(vg, 10, 50, 30);

			// Line caps
			drawCaps(vg, 10, 300, 30);

			drawScissor(vg, 50, height - 80, t);

			vg.nvgSave();
			if (blowup)
			{
				vg.nvgRotate((float)Math.Sin(t * 0.3f) * 5.0f / 180.0f * (float)Math.PI);
				vg.nvgScale(2.0f, 2.0f);
			}

			// Widgets
			drawWindow(vg, "Widgets `n Stuff", 50, 50, 300, 400);
			x = 60;
			y = 95;
			drawSearchBox(vg, "Search", x, y, 280, 25);
			y += 40;
			drawDropDown(vg, "Effects", x, y, 280, 28);
			popy = y + 14;
			y += 45;

			// Form
			drawLabel(vg, "Login", x, y, 280, 20);
			y += 25;
			drawEditBox(vg, "Email", x, y, 280, 28);
			y += 35;
			drawEditBox(vg, "Password", x, y, 280, 28);
			y += 38;
			drawCheckBox(vg, "Remember me", x, y, 140, 28);
			y += 45;

			// Slider
			drawLabel(vg, "Diameter", x, y, 280, 20);
			y += 25;
			drawEditBoxNum(vg, "123.00", "px", x + 180, y, 100, 28);
			drawSlider(vg, 0.4f, x, y, 170, 28);
			y += 55;

			drawButton(vg, 0, "Cancel", x + 170, y, 110, 28, new Color(0, 0, 0, 0));

			// Thumbnails box
			drawThumbnails(vg, 365, popy - 30, 160, 300, images, t);

			vg.nvgRestore();
		}
	}
}