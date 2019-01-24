using Microsoft.Xna.Framework;

namespace NanoVGSharp.Samples.Demo
{
	public class PerfGraph
	{
		public enum Style
		{
			GRAPH_RENDER_FPS,
			GRAPH_RENDER_MS,
			GRAPH_RENDER_PERCENT,
		};

		private Style _style;
		private string _name;
		private float[] _values = new float[100];
		private int _head;

		public PerfGraph(Style style, string name)
		{
			_style = style;
			_name = name;
		}

		public void Update(float frameTime)
		{
			_head = (_head + 1) % _values.Length;
			_values[_head] = frameTime;
		}

		public float GetAverage()
		{
			float avg = 0;
			for (var i = 0; i < _values.Length; i++)
			{
				avg += _values[i];
			}
			return avg / (float)_values.Length;
		}

		public void Render(NvgContext vg, float x, float y)
		{
			int i;
			float avg, w, h;
			string str;

			avg = GetAverage();

			w = 200;
			h = 35;

			vg.BeginPath();
			vg.Rect(x, y, w, h);
			vg.FillColor(new Color(0, 0, 0, 128));
			vg.Fill();

			vg.BeginPath();
			vg.MoveTo(x, y + h);
			if (_style == Style.GRAPH_RENDER_FPS)
			{
				for (i = 0; i < _values.Length; i++)
				{
					float v = 1.0f / (0.00001f + _values[(_head + i) % _values.Length]);
					float vx, vy;
					if (v > 80.0f)
						v = 80.0f;
					vx = x + ((float)i / (_values.Length - 1)) * w;
					vy = y + h - ((v / 80.0f) * h);
					vg.LineTo(vx, vy);
				}
			}
			else if (_style == Style.GRAPH_RENDER_PERCENT)
			{
				for (i = 0; i < _values.Length; i++)
				{
					float v = _values[(_head + i) % _values.Length] * 1.0f;
					float vx, vy;
					if (v > 100.0f)
						v = 100.0f;
					vx = x + ((float)i / (_values.Length - 1)) * w;
					vy = y + h - ((v / 100.0f) * h);
					vg.LineTo(vx, vy);
				}
			}
			else
			{
				for (i = 0; i < _values.Length; i++)
				{
					float v = _values[(_head + i) % _values.Length] * 1000.0f;
					float vx, vy;
					if (v > 20.0f)
						v = 20.0f;
					vx = x + ((float)i / (_values.Length - 1)) * w;
					vy = y + h - ((v / 20.0f) * h);
					vg.LineTo(vx, vy);
				}
			}
			vg.LineTo(x + w, y + h);
			vg.FillColor(new Color(255, 192, 0, 128));
			vg.Fill();

			vg.FontFace("sans");

			if (string.IsNullOrEmpty(_name))
			{
				vg.FontSize(14.0f);
				vg.TextAlign(NvgContext.NVG_ALIGN_LEFT | NvgContext.NVG_ALIGN_TOP);
				vg.FillColor(new Color(240, 240, 240, 192));
				vg.Text(x + 3, y + 1, _name);
			}

			if (_style == Style.GRAPH_RENDER_FPS)
			{
				vg.FontSize(18.0f);
				vg.TextAlign(NvgContext.NVG_ALIGN_RIGHT | NvgContext.NVG_ALIGN_TOP);
				vg.FillColor(new Color(240, 240, 240, 255));
				str = string.Format("{0:0.00} FPS", 1.0f / avg);
				vg.Text(x + w - 3, y + 1, str);

				vg.FontSize(15.0f);
				vg.TextAlign(NvgContext.NVG_ALIGN_RIGHT | NvgContext.NVG_ALIGN_BOTTOM);
				vg.FillColor(new Color(240, 240, 240, 160));
				str = string.Format("{0:0.00} ms", avg * 1000.0f);
				vg.Text(x + w - 3, y + h - 1, str);
			}
			else if (_style == Style.GRAPH_RENDER_PERCENT)
			{
				vg.FontSize(18.0f);
				vg.TextAlign(NvgContext.NVG_ALIGN_RIGHT | NvgContext.NVG_ALIGN_TOP);
				vg.FillColor(new Color(240, 240, 240, 255));
				str = string.Format("{0:0.00} %%", avg);
				vg.Text(x + w - 3, y + 1, str);
			}
			else
			{
				vg.FontSize(18.0f);
				vg.TextAlign(NvgContext.NVG_ALIGN_RIGHT | NvgContext.NVG_ALIGN_TOP);
				vg.FillColor(new Color(240, 240, 240, 255));
				str = string.Format("{0:0.00} ms", avg * 1000.0f);
				vg.Text(x + w - 3, y + 1, str);
			}
		}

	}
}
