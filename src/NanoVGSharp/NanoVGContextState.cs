using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public class NanoVGContextState
	{
		public int shapeAntiAlias;
		public Paint fill;
		public Paint stroke;
		public float strokeWidth;
		public float miterLimit;
		public int lineJoin;
		public int lineCap;
		public float alpha;
		public Transform xform = new Transform();
		public Scissor scissor;
		public float fontSize;
		public float letterSpacing;
		public float lineHeight;
		public float fontBlur;
		public int textAlign;
		public int fontId;

		public NanoVGContextState Clone()
		{
			return new NanoVGContextState
			{
				shapeAntiAlias = shapeAntiAlias,
				fill = fill,
				stroke = stroke,
				strokeWidth = strokeWidth,
				miterLimit = miterLimit,
				lineJoin = lineJoin,
				lineCap = lineCap,
				alpha = alpha,
				xform = xform,
				scissor = scissor,
				fontSize = fontSize,
				letterSpacing = letterSpacing,
				lineHeight = lineHeight,
				fontBlur = fontBlur,
				textAlign = textAlign,
				fontId = fontId
			};
		}
	}
}
