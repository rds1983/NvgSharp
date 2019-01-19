using StbSharp;

namespace NanoVGSharp
{
	public unsafe class FONSfont
	{
		public StbTrueType.stbtt_fontinfo font = new StbTrueType.stbtt_fontinfo();
		public string name;
		public byte[] data;
		public int dataSize;
		public byte freeData;
		public float ascender;
		public float descender;
		public float lineh;
		public FONSglyph* glyphs;
		public int cglyphs;
		public int nglyphs;
		public int[] lut = new int[256];
		public int[] fallbacks = new int[20];
		public int nfallbacks;
	}
}
