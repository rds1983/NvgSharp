using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct NVGstate
	{
		public NVGcompositeOperationState compositeOperation;
		public int shapeAntiAlias;
		public NVGpaint fill;
		public NVGpaint stroke;
		public float strokeWidth;
		public float miterLimit;
		public int lineJoin;
		public int lineCap;
		public float alpha;
		public fixed float xform[6];
		public NVGscissor scissor;
		public float fontSize;
		public float letterSpacing;
		public float lineHeight;
		public float fontBlur;
		public int textAlign;
		public int fontId;
	}
}
