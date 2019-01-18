using System.Runtime.InteropServices;
using StbSharp;

namespace NanoVGSharp
{
	public unsafe class NanoVG
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

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGcolor
		{
			public float r;
			public float g;
			public float b;
			public float a;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGpaint
		{
			public fixed float xform[6];
			public fixed float extent[2];
			public float radius;
			public float feather;
			public NVGcolor innerColor;
			public NVGcolor outerColor;
			public int image;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGcompositeOperationState
		{
			public int srcRGB;
			public int dstRGB;
			public int srcAlpha;
			public int dstAlpha;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGglyphPosition
		{
			public sbyte* str;
			public float x;
			public float minx;
			public float maxx;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGtextRow
		{
			public sbyte* start;
			public sbyte* end;
			public sbyte* next;
			public float width;
			public float minx;
			public float maxx;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGscissor
		{
			public fixed float xform[6];
			public fixed float extent[2];
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGvertex
		{
			public float x;
			public float y;
			public float u;
			public float v;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGpath
		{
			public int first;
			public int count;
			public byte closed;
			public int nbevel;
			public NVGvertex* fill;
			public int nfill;
			public NVGvertex* stroke;
			public int nstroke;
			public int winding;
			public int convex;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSquad
		{
			public float x0;
			public float y0;
			public float s0;
			public float t0;
			public float x1;
			public float y1;
			public float s1;
			public float t1;
		}

		public class FONStextIter
		{
			public float x;
			public float y;
			public float nextx;
			public float nexty;
			public float scale;
			public float spacing;
			public uint codepoint;
			public short isize;
			public short iblur;
			public FONSfont font;
			public int prevGlyphIndex;
			public sbyte* str;
			public sbyte* next;
			public sbyte* end;
			public uint utf8state;
			public int bitmapOption;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSttFontImpl
		{
			public StbTrueType.stbtt_fontinfo font;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSglyph
		{
			public uint codepoint;
			public int index;
			public int next;
			public short size;
			public short blur;
			public short x0;
			public short y0;
			public short x1;
			public short y1;
			public short xadv;
			public short xoff;
			public short yoff;
		}

		public class FONSfont
		{
			public FONSttFontImpl font = new FONSttFontImpl();
			public sbyte[] name = new sbyte[64];
			public byte* data;
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

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSstate
		{
			public int font;
			public int align;
			public float size;
			public uint color;
			public float blur;
			public float spacing;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSatlasNode
		{
			public short x;
			public short y;
			public short width;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSatlas
		{
			public int width;
			public int height;
			public FONSatlasNode* nodes;
			public int nnodes;
			public int cnodes;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGstate
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

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGpoint
		{
			public float x;
			public float y;
			public float dx;
			public float dy;
			public float len;
			public float dmx;
			public float dmy;
			public byte flags;
		}

		public class NVGpathCache
		{
			public NVGpoint* points;
			public int npoints;
			public int cpoints;
			public NVGpath* paths;
			public int npaths;
			public int cpaths;
			public NVGvertex* verts;
			public int nverts;
			public int cverts;
			public float[] bounds = new float[4];
		}

		public class NVGcontext
		{
			public NVGparams _params_ = new NVGparams();
			public float* commands;
			public int ccommands;
			public int ncommands;
			public float commandx;
			public float commandy;
			public NVGstate[] states = new NVGstate[32];
			public int nstates;
			public NVGpathCache cache;
			public float tessTol;
			public float distTol;
			public float fringeWidth;
			public float devicePxRatio;
			public FONScontext fs;
			public int[] fontImages = new int[4];
			public int fontImageIdx;
			public int drawCallCount;
			public int fillTriCount;
			public int strokeTriCount;
			public int textTriCount;
		}

		public static int fons__tt_init(FONScontext context)
		{
			return (int)(1);
		}

		public static int fons__tt_done(FONScontext context)
		{
			return (int)(1);
		}

		public static int fons__tt_loadFont(FONScontext context, FONSttFontImpl* font, byte* data, int dataSize)
		{
			int stbError = 0;
			font->font.userdata = context;
			stbError = (int)(StbTrueType.stbtt_InitFont(&font->font, data, (int)(0)));
			return (int)(stbError);
		}

		public static void fons__tt_getFontVMetrics(FONSttFontImpl* font, int* ascent, int* descent, int* lineGap)
		{
			StbTrueType.stbtt_GetFontVMetrics(&font->font, ascent, descent, lineGap);
		}

		public static float fons__tt_getPixelHeightScale(FONSttFontImpl* font, float size)
		{
			return (float)(StbTrueType.stbtt_ScaleForPixelHeight(&font->font, (float)(size)));
		}

		public static int fons__tt_getGlyphIndex(FONSttFontImpl* font, int codepoint)
		{
			return (int)(StbTrueType.stbtt_FindGlyphIndex(&font->font, (int)(codepoint)));
		}

		public static int fons__tt_buildGlyphBitmap(FONSttFontImpl* font, int glyph, float size, float scale, int* advance, int* lsb, int* x0, int* y0, int* x1, int* y1)
		{
			StbTrueType.stbtt_GetGlyphHMetrics(&font->font, (int)(glyph), advance, lsb);
			StbTrueType.stbtt_GetGlyphBitmapBox(&font->font, (int)(glyph), (float)(scale), (float)(scale), x0, y0, x1, y1);
			return (int)(1);
		}

		public static void fons__tt_renderGlyphBitmap(FONSttFontImpl* font, byte* output, int outWidth, int outHeight, int outStride, float scaleX, float scaleY, int glyph)
		{
			StbTrueType.stbtt_MakeGlyphBitmap(&font->font, output, (int)(outWidth), (int)(outHeight), (int)(outStride), (float)(scaleX), (float)(scaleY), (int)(glyph));
		}

		public static int fons__tt_getGlyphKernAdvance(FONSttFontImpl* font, int glyph1, int glyph2)
		{
			return (int)(StbTrueType.stbtt_GetGlyphKernAdvance(&font->font, (int)(glyph1), (int)(glyph2)));
		}

		public static uint fons__hashint(uint a)
		{
			a += (uint)(~(a << 15));
			a ^= (uint)(a >> 10);
			a += (uint)(a << 3);
			a ^= (uint)(a >> 6);
			a += (uint)(~(a << 11));
			a ^= (uint)(a >> 16);
			return (uint)(a);
		}

		public static int fons__mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int fons__maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}

		public static void* fons__tmpalloc(ulong size, void* up)
		{
			byte* ptr;
			FONScontext stash = (FONScontext)(up);
			size = (ulong)((size + 0xf) & ~0xf);
			if ((stash.nscratch + (int)(size)) > (96000)) {
				if ((stash.handleError) != null) stash.handleError(stash.errorUptr, (int)(FONS_SCRATCH_FULL), (int)(stash.nscratch + (int)(size))); return null; }

			ptr = stash.scratch + stash.nscratch;
			stash.nscratch += ((int)(size));
			return ptr;
		}

		public static void fons__tmpfree(void* ptr, void* up)
		{
		}

		public static uint fons__decutf8(uint* state, uint* codep, uint byte)
		{
			byte* utf8d = stackalloc byte[364];
			utf8d[0] = (byte)(0);
			utf8d[1] = (byte)(0);
			utf8d[2] = (byte)(0);
			utf8d[3] = (byte)(0);
			utf8d[4] = (byte)(0);
			utf8d[5] = (byte)(0);
			utf8d[6] = (byte)(0);
			utf8d[7] = (byte)(0);
			utf8d[8] = (byte)(0);
			utf8d[9] = (byte)(0);
			utf8d[10] = (byte)(0);
			utf8d[11] = (byte)(0);
			utf8d[12] = (byte)(0);
			utf8d[13] = (byte)(0);
			utf8d[14] = (byte)(0);
			utf8d[15] = (byte)(0);
			utf8d[16] = (byte)(0);
			utf8d[17] = (byte)(0);
			utf8d[18] = (byte)(0);
			utf8d[19] = (byte)(0);
			utf8d[20] = (byte)(0);
			utf8d[21] = (byte)(0);
			utf8d[22] = (byte)(0);
			utf8d[23] = (byte)(0);
			utf8d[24] = (byte)(0);
			utf8d[25] = (byte)(0);
			utf8d[26] = (byte)(0);
			utf8d[27] = (byte)(0);
			utf8d[28] = (byte)(0);
			utf8d[29] = (byte)(0);
			utf8d[30] = (byte)(0);
			utf8d[31] = (byte)(0);
			utf8d[32] = (byte)(0);
			utf8d[33] = (byte)(0);
			utf8d[34] = (byte)(0);
			utf8d[35] = (byte)(0);
			utf8d[36] = (byte)(0);
			utf8d[37] = (byte)(0);
			utf8d[38] = (byte)(0);
			utf8d[39] = (byte)(0);
			utf8d[40] = (byte)(0);
			utf8d[41] = (byte)(0);
			utf8d[42] = (byte)(0);
			utf8d[43] = (byte)(0);
			utf8d[44] = (byte)(0);
			utf8d[45] = (byte)(0);
			utf8d[46] = (byte)(0);
			utf8d[47] = (byte)(0);
			utf8d[48] = (byte)(0);
			utf8d[49] = (byte)(0);
			utf8d[50] = (byte)(0);
			utf8d[51] = (byte)(0);
			utf8d[52] = (byte)(0);
			utf8d[53] = (byte)(0);
			utf8d[54] = (byte)(0);
			utf8d[55] = (byte)(0);
			utf8d[56] = (byte)(0);
			utf8d[57] = (byte)(0);
			utf8d[58] = (byte)(0);
			utf8d[59] = (byte)(0);
			utf8d[60] = (byte)(0);
			utf8d[61] = (byte)(0);
			utf8d[62] = (byte)(0);
			utf8d[63] = (byte)(0);
			utf8d[64] = (byte)(0);
			utf8d[65] = (byte)(0);
			utf8d[66] = (byte)(0);
			utf8d[67] = (byte)(0);
			utf8d[68] = (byte)(0);
			utf8d[69] = (byte)(0);
			utf8d[70] = (byte)(0);
			utf8d[71] = (byte)(0);
			utf8d[72] = (byte)(0);
			utf8d[73] = (byte)(0);
			utf8d[74] = (byte)(0);
			utf8d[75] = (byte)(0);
			utf8d[76] = (byte)(0);
			utf8d[77] = (byte)(0);
			utf8d[78] = (byte)(0);
			utf8d[79] = (byte)(0);
			utf8d[80] = (byte)(0);
			utf8d[81] = (byte)(0);
			utf8d[82] = (byte)(0);
			utf8d[83] = (byte)(0);
			utf8d[84] = (byte)(0);
			utf8d[85] = (byte)(0);
			utf8d[86] = (byte)(0);
			utf8d[87] = (byte)(0);
			utf8d[88] = (byte)(0);
			utf8d[89] = (byte)(0);
			utf8d[90] = (byte)(0);
			utf8d[91] = (byte)(0);
			utf8d[92] = (byte)(0);
			utf8d[93] = (byte)(0);
			utf8d[94] = (byte)(0);
			utf8d[95] = (byte)(0);
			utf8d[96] = (byte)(0);
			utf8d[97] = (byte)(0);
			utf8d[98] = (byte)(0);
			utf8d[99] = (byte)(0);
			utf8d[100] = (byte)(0);
			utf8d[101] = (byte)(0);
			utf8d[102] = (byte)(0);
			utf8d[103] = (byte)(0);
			utf8d[104] = (byte)(0);
			utf8d[105] = (byte)(0);
			utf8d[106] = (byte)(0);
			utf8d[107] = (byte)(0);
			utf8d[108] = (byte)(0);
			utf8d[109] = (byte)(0);
			utf8d[110] = (byte)(0);
			utf8d[111] = (byte)(0);
			utf8d[112] = (byte)(0);
			utf8d[113] = (byte)(0);
			utf8d[114] = (byte)(0);
			utf8d[115] = (byte)(0);
			utf8d[116] = (byte)(0);
			utf8d[117] = (byte)(0);
			utf8d[118] = (byte)(0);
			utf8d[119] = (byte)(0);
			utf8d[120] = (byte)(0);
			utf8d[121] = (byte)(0);
			utf8d[122] = (byte)(0);
			utf8d[123] = (byte)(0);
			utf8d[124] = (byte)(0);
			utf8d[125] = (byte)(0);
			utf8d[126] = (byte)(0);
			utf8d[127] = (byte)(0);
			utf8d[128] = (byte)(1);
			utf8d[129] = (byte)(1);
			utf8d[130] = (byte)(1);
			utf8d[131] = (byte)(1);
			utf8d[132] = (byte)(1);
			utf8d[133] = (byte)(1);
			utf8d[134] = (byte)(1);
			utf8d[135] = (byte)(1);
			utf8d[136] = (byte)(1);
			utf8d[137] = (byte)(1);
			utf8d[138] = (byte)(1);
			utf8d[139] = (byte)(1);
			utf8d[140] = (byte)(1);
			utf8d[141] = (byte)(1);
			utf8d[142] = (byte)(1);
			utf8d[143] = (byte)(1);
			utf8d[144] = (byte)(9);
			utf8d[145] = (byte)(9);
			utf8d[146] = (byte)(9);
			utf8d[147] = (byte)(9);
			utf8d[148] = (byte)(9);
			utf8d[149] = (byte)(9);
			utf8d[150] = (byte)(9);
			utf8d[151] = (byte)(9);
			utf8d[152] = (byte)(9);
			utf8d[153] = (byte)(9);
			utf8d[154] = (byte)(9);
			utf8d[155] = (byte)(9);
			utf8d[156] = (byte)(9);
			utf8d[157] = (byte)(9);
			utf8d[158] = (byte)(9);
			utf8d[159] = (byte)(9);
			utf8d[160] = (byte)(7);
			utf8d[161] = (byte)(7);
			utf8d[162] = (byte)(7);
			utf8d[163] = (byte)(7);
			utf8d[164] = (byte)(7);
			utf8d[165] = (byte)(7);
			utf8d[166] = (byte)(7);
			utf8d[167] = (byte)(7);
			utf8d[168] = (byte)(7);
			utf8d[169] = (byte)(7);
			utf8d[170] = (byte)(7);
			utf8d[171] = (byte)(7);
			utf8d[172] = (byte)(7);
			utf8d[173] = (byte)(7);
			utf8d[174] = (byte)(7);
			utf8d[175] = (byte)(7);
			utf8d[176] = (byte)(7);
			utf8d[177] = (byte)(7);
			utf8d[178] = (byte)(7);
			utf8d[179] = (byte)(7);
			utf8d[180] = (byte)(7);
			utf8d[181] = (byte)(7);
			utf8d[182] = (byte)(7);
			utf8d[183] = (byte)(7);
			utf8d[184] = (byte)(7);
			utf8d[185] = (byte)(7);
			utf8d[186] = (byte)(7);
			utf8d[187] = (byte)(7);
			utf8d[188] = (byte)(7);
			utf8d[189] = (byte)(7);
			utf8d[190] = (byte)(7);
			utf8d[191] = (byte)(7);
			utf8d[192] = (byte)(8);
			utf8d[193] = (byte)(8);
			utf8d[194] = (byte)(2);
			utf8d[195] = (byte)(2);
			utf8d[196] = (byte)(2);
			utf8d[197] = (byte)(2);
			utf8d[198] = (byte)(2);
			utf8d[199] = (byte)(2);
			utf8d[200] = (byte)(2);
			utf8d[201] = (byte)(2);
			utf8d[202] = (byte)(2);
			utf8d[203] = (byte)(2);
			utf8d[204] = (byte)(2);
			utf8d[205] = (byte)(2);
			utf8d[206] = (byte)(2);
			utf8d[207] = (byte)(2);
			utf8d[208] = (byte)(2);
			utf8d[209] = (byte)(2);
			utf8d[210] = (byte)(2);
			utf8d[211] = (byte)(2);
			utf8d[212] = (byte)(2);
			utf8d[213] = (byte)(2);
			utf8d[214] = (byte)(2);
			utf8d[215] = (byte)(2);
			utf8d[216] = (byte)(2);
			utf8d[217] = (byte)(2);
			utf8d[218] = (byte)(2);
			utf8d[219] = (byte)(2);
			utf8d[220] = (byte)(2);
			utf8d[221] = (byte)(2);
			utf8d[222] = (byte)(2);
			utf8d[223] = (byte)(2);
			utf8d[224] = (byte)(10);
			utf8d[225] = (byte)(3);
			utf8d[226] = (byte)(3);
			utf8d[227] = (byte)(3);
			utf8d[228] = (byte)(3);
			utf8d[229] = (byte)(3);
			utf8d[230] = (byte)(3);
			utf8d[231] = (byte)(3);
			utf8d[232] = (byte)(3);
			utf8d[233] = (byte)(3);
			utf8d[234] = (byte)(3);
			utf8d[235] = (byte)(3);
			utf8d[236] = (byte)(3);
			utf8d[237] = (byte)(4);
			utf8d[238] = (byte)(3);
			utf8d[239] = (byte)(3);
			utf8d[240] = (byte)(11);
			utf8d[241] = (byte)(6);
			utf8d[242] = (byte)(6);
			utf8d[243] = (byte)(6);
			utf8d[244] = (byte)(5);
			utf8d[245] = (byte)(8);
			utf8d[246] = (byte)(8);
			utf8d[247] = (byte)(8);
			utf8d[248] = (byte)(8);
			utf8d[249] = (byte)(8);
			utf8d[250] = (byte)(8);
			utf8d[251] = (byte)(8);
			utf8d[252] = (byte)(8);
			utf8d[253] = (byte)(8);
			utf8d[254] = (byte)(8);
			utf8d[255] = (byte)(8);
			utf8d[256] = (byte)(0);
			utf8d[257] = (byte)(12);
			utf8d[258] = (byte)(24);
			utf8d[259] = (byte)(36);
			utf8d[260] = (byte)(60);
			utf8d[261] = (byte)(96);
			utf8d[262] = (byte)(84);
			utf8d[263] = (byte)(12);
			utf8d[264] = (byte)(12);
			utf8d[265] = (byte)(12);
			utf8d[266] = (byte)(48);
			utf8d[267] = (byte)(72);
			utf8d[268] = (byte)(12);
			utf8d[269] = (byte)(12);
			utf8d[270] = (byte)(12);
			utf8d[271] = (byte)(12);
			utf8d[272] = (byte)(12);
			utf8d[273] = (byte)(12);
			utf8d[274] = (byte)(12);
			utf8d[275] = (byte)(12);
			utf8d[276] = (byte)(12);
			utf8d[277] = (byte)(12);
			utf8d[278] = (byte)(12);
			utf8d[279] = (byte)(12);
			utf8d[280] = (byte)(12);
			utf8d[281] = (byte)(0);
			utf8d[282] = (byte)(12);
			utf8d[283] = (byte)(12);
			utf8d[284] = (byte)(12);
			utf8d[285] = (byte)(12);
			utf8d[286] = (byte)(12);
			utf8d[287] = (byte)(0);
			utf8d[288] = (byte)(12);
			utf8d[289] = (byte)(0);
			utf8d[290] = (byte)(12);
			utf8d[291] = (byte)(12);
			utf8d[292] = (byte)(12);
			utf8d[293] = (byte)(24);
			utf8d[294] = (byte)(12);
			utf8d[295] = (byte)(12);
			utf8d[296] = (byte)(12);
			utf8d[297] = (byte)(12);
			utf8d[298] = (byte)(12);
			utf8d[299] = (byte)(24);
			utf8d[300] = (byte)(12);
			utf8d[301] = (byte)(24);
			utf8d[302] = (byte)(12);
			utf8d[303] = (byte)(12);
			utf8d[304] = (byte)(12);
			utf8d[305] = (byte)(12);
			utf8d[306] = (byte)(12);
			utf8d[307] = (byte)(12);
			utf8d[308] = (byte)(12);
			utf8d[309] = (byte)(12);
			utf8d[310] = (byte)(12);
			utf8d[311] = (byte)(24);
			utf8d[312] = (byte)(12);
			utf8d[313] = (byte)(12);
			utf8d[314] = (byte)(12);
			utf8d[315] = (byte)(12);
			utf8d[316] = (byte)(12);
			utf8d[317] = (byte)(24);
			utf8d[318] = (byte)(12);
			utf8d[319] = (byte)(12);
			utf8d[320] = (byte)(12);
			utf8d[321] = (byte)(12);
			utf8d[322] = (byte)(12);
			utf8d[323] = (byte)(12);
			utf8d[324] = (byte)(12);
			utf8d[325] = (byte)(24);
			utf8d[326] = (byte)(12);
			utf8d[327] = (byte)(12);
			utf8d[328] = (byte)(12);
			utf8d[329] = (byte)(12);
			utf8d[330] = (byte)(12);
			utf8d[331] = (byte)(12);
			utf8d[332] = (byte)(12);
			utf8d[333] = (byte)(12);
			utf8d[334] = (byte)(12);
			utf8d[335] = (byte)(36);
			utf8d[336] = (byte)(12);
			utf8d[337] = (byte)(36);
			utf8d[338] = (byte)(12);
			utf8d[339] = (byte)(12);
			utf8d[340] = (byte)(12);
			utf8d[341] = (byte)(36);
			utf8d[342] = (byte)(12);
			utf8d[343] = (byte)(12);
			utf8d[344] = (byte)(12);
			utf8d[345] = (byte)(12);
			utf8d[346] = (byte)(12);
			utf8d[347] = (byte)(36);
			utf8d[348] = (byte)(12);
			utf8d[349] = (byte)(36);
			utf8d[350] = (byte)(12);
			utf8d[351] = (byte)(12);
			utf8d[352] = (byte)(12);
			utf8d[353] = (byte)(36);
			utf8d[354] = (byte)(12);
			utf8d[355] = (byte)(12);
			utf8d[356] = (byte)(12);
			utf8d[357] = (byte)(12);
			utf8d[358] = (byte)(12);
			utf8d[359] = (byte)(12);
			utf8d[360] = (byte)(12);
			utf8d[361] = (byte)(12);
			utf8d[362] = (byte)(12);
			utf8d[363] = (byte)(12);

			uint type = (uint)(utf8d[byte]);
			*codep = (uint)((*state != 0) ? (byte & 0x3fu) | (*codep << 6) : (0xff >> type) & (byte));
			*state = (uint)(utf8d[256 + *state + type]);
			return (uint)(*state);
		}

		public static void fons__deleteAtlas(FONSatlas* atlas)
		{
			if ((atlas) == null) return;
			if (atlas->nodes != null) CRuntime.free(atlas->nodes);
			CRuntime.free(atlas);
		}

		public static FONSatlas* fons__allocAtlas(int w, int h, int nnodes)
		{
			FONSatlas* atlas = null;
			atlas = (FONSatlas*)(CRuntime.malloc((ulong)(sizeof(FONSatlas))));
			if ((atlas) == null) goto error;
			CRuntime.memset(atlas, (int)(0), (ulong)(sizeof(FONSatlas)));
			atlas->width = (int)(w);
			atlas->height = (int)(h);
			atlas->nodes = (FONSatlasNode*)(CRuntime.malloc((ulong)(sizeof(FONSatlasNode) * nnodes)));
			if ((atlas->nodes) == null) goto error;
			CRuntime.memset(atlas->nodes, (int)(0), (ulong)(sizeof(FONSatlasNode) * nnodes));
			atlas->nnodes = (int)(0);
			atlas->cnodes = (int)(nnodes);
			atlas->nodes[0].x = (short)(0);
			atlas->nodes[0].y = (short)(0);
			atlas->nodes[0].width = ((short)(w));
			atlas->nnodes++;
			return atlas;
		error:;
			if ((atlas) != null) fons__deleteAtlas(atlas);
			return null;
		}

		public static int fons__atlasInsertNode(FONSatlas* atlas, int idx, int x, int y, int w)
		{
			int i = 0;
			if ((atlas->nnodes + 1) > (atlas->cnodes)) {
				atlas->cnodes = (int)((atlas->cnodes) == (0) ? 8 : atlas->cnodes * 2); atlas->nodes = (FONSatlasNode*)(CRuntime.realloc(atlas->nodes, (ulong)(sizeof(FONSatlasNode) * atlas->cnodes))); if ((atlas->nodes) == null) return (int)(0); }

			for (i = (int)(atlas->nnodes); (i) > (idx); i--) { atlas->nodes[i] = (FONSatlasNode)(atlas->nodes[i - 1]); }
			atlas->nodes[idx].x = ((short)(x));
			atlas->nodes[idx].y = ((short)(y));
			atlas->nodes[idx].width = ((short)(w));
			atlas->nnodes++;
			return (int)(1);
		}

		public static void fons__atlasRemoveNode(FONSatlas* atlas, int idx)
		{
			int i = 0;
			if ((atlas->nnodes) == (0)) return;
			for (i = (int)(idx); (i) < (atlas->nnodes - 1); i++) { atlas->nodes[i] = (FONSatlasNode)(atlas->nodes[i + 1]); }
			atlas->nnodes--;
		}

		public static void fons__atlasExpand(FONSatlas* atlas, int w, int h)
		{
			if ((w) > (atlas->width)) fons__atlasInsertNode(atlas, (int)(atlas->nnodes), (int)(atlas->width), (int)(0), (int)(w - atlas->width));
			atlas->width = (int)(w);
			atlas->height = (int)(h);
		}

		public static void fons__atlasReset(FONSatlas* atlas, int w, int h)
		{
			atlas->width = (int)(w);
			atlas->height = (int)(h);
			atlas->nnodes = (int)(0);
			atlas->nodes[0].x = (short)(0);
			atlas->nodes[0].y = (short)(0);
			atlas->nodes[0].width = ((short)(w));
			atlas->nnodes++;
		}

		public static int fons__atlasAddSkylineLevel(FONSatlas* atlas, int idx, int x, int y, int w, int h)
		{
			int i = 0;
			if ((fons__atlasInsertNode(atlas, (int)(idx), (int)(x), (int)(y + h), (int)(w))) == (0)) return (int)(0);
			for (i = (int)(idx + 1); (i) < (atlas->nnodes); i++) {
				if ((atlas->nodes[i].x) < (atlas->nodes[i - 1].x + atlas->nodes[i - 1].width)) {
					int shrink = (int)(atlas->nodes[i - 1].x + atlas->nodes[i - 1].width - atlas->nodes[i].x); atlas->nodes[i].x += ((short)(shrink)); atlas->nodes[i].width -= ((short)(shrink)); if (atlas->nodes[i].width <= 0) {
						fons__atlasRemoveNode(atlas, (int)(i)); i--; }
					else {
						break; }
				}
				else {
					break; }
			}
			for (i = (int)(0); (i) < (atlas->nnodes - 1); i++) {
				if ((atlas->nodes[i].y) == (atlas->nodes[i + 1].y)) {
					atlas->nodes[i].width += (short)(atlas->nodes[i + 1].width); fons__atlasRemoveNode(atlas, (int)(i + 1)); i--; }
			}
			return (int)(1);
		}

		public static int fons__atlasRectFits(FONSatlas* atlas, int i, int w, int h)
		{
			int x = (int)(atlas->nodes[i].x);
			int y = (int)(atlas->nodes[i].y);
			int spaceLeft = 0;
			if ((x + w) > (atlas->width)) return (int)(-1);
			spaceLeft = (int)(w);
			while ((spaceLeft) > (0)) {
				if ((i) == (atlas->nnodes)) return (int)(-1); y = (int)(fons__maxi((int)(y), (int)(atlas->nodes[i].y))); if ((y + h) > (atlas->height)) return (int)(-1); spaceLeft -= (int)(atlas->nodes[i].width); ++i; }
			return (int)(y);
		}

		public static int fons__atlasAddRect(FONSatlas* atlas, int rw, int rh, int* rx, int* ry)
		{
			int besth = (int)(atlas->height); int bestw = (int)(atlas->width); int besti = (int)(-1);
			int bestx = (int)(-1); int besty = (int)(-1); int i = 0;
			for (i = (int)(0); (i) < (atlas->nnodes); i++) {
				int y = (int)(fons__atlasRectFits(atlas, (int)(i), (int)(rw), (int)(rh))); if (y != -1) {
					if (((y + rh) < (besth)) || (((y + rh) == (besth)) && ((atlas->nodes[i].width) < (bestw)))) {
						besti = (int)(i); bestw = (int)(atlas->nodes[i].width); besth = (int)(y + rh); bestx = (int)(atlas->nodes[i].x); besty = (int)(y); }
				}
			}
			if ((besti) == (-1)) return (int)(0);
			if ((fons__atlasAddSkylineLevel(atlas, (int)(besti), (int)(bestx), (int)(besty), (int)(rw), (int)(rh))) == (0)) return (int)(0);
			*rx = (int)(bestx);
			*ry = (int)(besty);
			return (int)(1);
		}

		public static void fons__addWhiteRect(FONScontext stash, int w, int h)
		{
			int x = 0; int y = 0; int gx = 0; int gy = 0;
			byte* dst;
			if ((fons__atlasAddRect(stash.atlas, (int)(w), (int)(h), &gx, &gy)) == (0)) return;
			dst = &stash.texData[gx + gy * stash._params_.width];
			for (y = (int)(0); (y) < (h); y++) {
				for (x = (int)(0); (x) < (w); x++) { dst[x] = (byte)(0xff); } dst += stash._params_.width; }
			stash.dirtyRect[0] = (int)(fons__mini((int)(stash.dirtyRect[0]), (int)(gx)));
			stash.dirtyRect[1] = (int)(fons__mini((int)(stash.dirtyRect[1]), (int)(gy)));
			stash.dirtyRect[2] = (int)(fons__maxi((int)(stash.dirtyRect[2]), (int)(gx + w)));
			stash.dirtyRect[3] = (int)(fons__maxi((int)(stash.dirtyRect[3]), (int)(gy + h)));
		}

		public static FONScontext fonsCreateInternal(FONSparams _params_)
		{
			FONScontext stash = new FONScontext();
			stash._params_ = (FONSparams)(_params_);
			stash.scratch = (byte*)(CRuntime.malloc((ulong)(96000)));
			if ((stash.scratch) == null) goto error;
			if (fons__tt_init(stash) == 0) goto error;
			if (stash._params_.renderCreate != null) {
				if ((stash._params_.renderCreate(stash._params_.userPtr, (int)(stash._params_.width), (int)(stash._params_.height))) == (0)) goto error; }

			stash.atlas = fons__allocAtlas((int)(stash._params_.width), (int)(stash._params_.height), (int)(256));
			if ((stash.atlas) == null) goto error;
			stash.fonts = new FONSfont[4];
			if ((stash.fonts) == null) goto error;
			stash.cfonts = (int)(4);
			stash.nfonts = (int)(0);
			stash.itw = (float)(1.0f / stash._params_.width);
			stash.ith = (float)(1.0f / stash._params_.height);
			stash.texData = (byte*)(CRuntime.malloc((ulong)(stash._params_.width * stash._params_.height)));
			if ((stash.texData) == null) goto error;
			CRuntime.memset(stash.texData, (int)(0), (ulong)(stash._params_.width * stash._params_.height));
			stash.dirtyRect[0] = (int)(stash._params_.width);
			stash.dirtyRect[1] = (int)(stash._params_.height);
			stash.dirtyRect[2] = (int)(0);
			stash.dirtyRect[3] = (int)(0);
			fons__addWhiteRect(stash, (int)(2), (int)(2));
			fonsPushState(stash);
			fonsClearState(stash);
			return stash;
		error:;
			fonsDeleteInternal(stash);
			return null;
		}

		public static FONSstate* fons__getState(FONScontext stash)
		{
			return &stash.states[stash.nstates - 1];
		}

		public static int fonsAddFallbackFont(FONScontext stash, int _base_, int fallback)
		{
			FONSfont baseFont = stash.fonts[_base_];
			if ((baseFont.nfallbacks) < (20)) {
				baseFont.fallbacks[baseFont.nfallbacks++] = (int)(fallback); return (int)(1); }

			return (int)(0);
		}

		public static void fonsSetSize(FONScontext stash, float size)
		{
			fons__getState(stash)->size = (float)(size);
		}

		public static void fonsSetColor(FONScontext stash, uint color)
		{
			fons__getState(stash)->color = (uint)(color);
		}

		public static void fonsSetSpacing(FONScontext stash, float spacing)
		{
			fons__getState(stash)->spacing = (float)(spacing);
		}

		public static void fonsSetBlur(FONScontext stash, float blur)
		{
			fons__getState(stash)->blur = (float)(blur);
		}

		public static void fonsSetAlign(FONScontext stash, int align)
		{
			fons__getState(stash)->align = (int)(align);
		}

		public static void fonsSetFont(FONScontext stash, int font)
		{
			fons__getState(stash)->font = (int)(font);
		}

		public static void fonsPushState(FONScontext stash)
		{
			if ((stash.nstates) >= (20)) {
				if ((stash.handleError) != null) stash.handleError(stash.errorUptr, (int)(FONS_STATES_OVERFLOW), (int)(0)); return; }

			if ((stash.nstates) > (0)) CRuntime.memcpy(&stash.states[stash.nstates], &stash.states[stash.nstates - 1], (ulong)(sizeof(FONSstate)));
			stash.nstates++;
		}

		public static void fonsPopState(FONScontext stash)
		{
			if (stash.nstates <= 1) {
				if ((stash.handleError) != null) stash.handleError(stash.errorUptr, (int)(FONS_STATES_UNDERFLOW), (int)(0)); return; }

			stash.nstates--;
		}

		public static void fonsClearState(FONScontext stash)
		{
			FONSstate* state = fons__getState(stash);
			state->size = (float)(12.0f);
			state->color = (uint)(0xffffffff);
			state->font = (int)(0);
			state->blur = (float)(0);
			state->spacing = (float)(0);
			state->align = (int)(FONS_ALIGN_LEFT | FONS_ALIGN_BASELINE);
		}

		public static void fons__freeFont(FONSfont font)
		{
			if ((font) == null) return;
			if ((font.glyphs) != null) CRuntime.free(font.glyphs);
			if (((font.freeData) != 0) && ((font.data) != null)) CRuntime.free(font.data);
		}

		public static int fons__allocFont(FONScontext stash)
		{
			FONSfont font = null;
			if ((stash.nfonts + 1) > (stash.cfonts)) {
				stash.cfonts = (int)((stash.cfonts) == (0) ? 8 : stash.cfonts * 2); stash.fonts = (FONSfont)(CRuntime.realloc(stash.fonts, (ulong)(sizeof(FONSfont) * stash.cfonts))); if ((stash.fonts) == null) return (int)(-1); }

			font = new FONSfont();
			if ((font) == null) goto error;
			font.glyphs = (FONSglyph*)(CRuntime.malloc((ulong)(sizeof(FONSglyph) * 256)));
			if ((font.glyphs) == null) goto error;
			font.cglyphs = (int)(256);
			font.nglyphs = (int)(0);
			stash.fonts[stash.nfonts++] = font;
			return (int)(stash.nfonts - 1);
		error:;
			fons__freeFont(font);
			return (int)(-1);
		}

		public static int fonsAddFontMem(FONScontext stash, sbyte* name, byte* data, int dataSize, int freeData)
		{
			int i = 0; int ascent = 0; int descent = 0; int fh = 0; int lineGap = 0;
			FONSfont font;
			int idx = (int)(fons__allocFont(stash));
			if ((idx) == (-1)) return (int)(-1);
			font = stash.fonts[idx];
			strncpy(font.name, name, (ulong)(sizeof((font.name))));
			font.name[sizeof((font.name)) - 1] = (sbyte)('');
			for (i = (int)(0); (i) < (256); ++i) { font.lut[i] = (int)(-1); }
			font.dataSize = (int)(dataSize);
			font.data = data;
			font.freeData = ((byte)(freeData));
			stash.nscratch = (int)(0);
			if (fons__tt_loadFont(stash, &font.font, data, (int)(dataSize)) == 0) goto error;
			fons__tt_getFontVMetrics(&font.font, &ascent, &descent, &lineGap);
			fh = (int)(ascent - descent);
			font.ascender = (float)((float)(ascent) / (float)(fh));
			font.descender = (float)((float)(descent) / (float)(fh));
			font.lineh = (float)((float)(fh + lineGap) / (float)(fh));
			return (int)(idx);
		error:;
			fons__freeFont(font);
			stash.nfonts--;
			return (int)(-1);
		}

		public static int fonsGetFontByName(FONScontext s, sbyte* name)
		{
			int i = 0;
			for (i = (int)(0); (i) < (s.nfonts); i++) {
				if ((strcmp(s.fonts[i].name, name)) == (0)) return (int)(i); }
			return (int)(-1);
		}

		public static FONSglyph* fons__allocGlyph(FONSfont font)
		{
			if ((font.nglyphs + 1) > (font.cglyphs)) {
				font.cglyphs = (int)((font.cglyphs) == (0) ? 8 : font.cglyphs * 2); font.glyphs = (FONSglyph*)(CRuntime.realloc(font.glyphs, (ulong)(sizeof(FONSglyph) * font.cglyphs))); if ((font.glyphs) == null) return null; }

			font.nglyphs++;
			return &font.glyphs[font.nglyphs - 1];
		}

		public static void fons__blurCols(byte* dst, int w, int h, int dstStride, int alpha)
		{
			int x = 0; int y = 0;
			for (y = (int)(0); (y) < (h); y++) {
				int z = (int)(0); for (x = (int)(1); (x) < (w); x++) {
					z += (int)((alpha * (((int)(dst[x]) << 7) - z)) >> 16); dst[x] = ((byte)(z >> 7)); } dst[w - 1] = (byte)(0); z = (int)(0); for (x = (int)(w - 2); (x) >= (0); x--) {
					z += (int)((alpha * (((int)(dst[x]) << 7) - z)) >> 16); dst[x] = ((byte)(z >> 7)); } dst[0] = (byte)(0); dst += dstStride; }
		}

		public static void fons__blurRows(byte* dst, int w, int h, int dstStride, int alpha)
		{
			int x = 0; int y = 0;
			for (x = (int)(0); (x) < (w); x++) {
				int z = (int)(0); for (y = (int)(dstStride); (y) < (h * dstStride); y += (int)(dstStride)) {
					z += (int)((alpha * (((int)(dst[y]) << 7) - z)) >> 16); dst[y] = ((byte)(z >> 7)); } dst[(h - 1) * dstStride] = (byte)(0); z = (int)(0); for (y = (int)((h - 2) * dstStride); (y) >= (0); y -= (int)(dstStride)) {
					z += (int)((alpha * (((int)(dst[y]) << 7) - z)) >> 16); dst[y] = ((byte)(z >> 7)); } dst[0] = (byte)(0); dst++; }
		}

		public static void fons__blur(FONScontext stash, byte* dst, int w, int h, int dstStride, int blur)
		{
			int alpha = 0;
			float sigma = 0;
			if ((blur) < (1)) return;
			sigma = (float)((float)(blur) * 0.57735f);
			alpha = ((int)((1 << 16) * (1.0f - expf((float)(-2.3f / (sigma + 1.0f))))));
			fons__blurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
		}

		public static FONSglyph* fons__getGlyph(FONScontext stash, FONSfont font, uint codepoint, short isize, short iblur, int bitmapOption)
		{
			int i = 0; int g = 0; int advance = 0; int lsb = 0; int x0 = 0; int y0 = 0; int x1 = 0; int y1 = 0; int gw = 0; int gh = 0; int gx = 0; int gy = 0; int x = 0; int y = 0;
			float scale = 0;
			FONSglyph* glyph = null;
			uint h = 0;
			float size = (float)(isize / 10.0f);
			int pad = 0; int added = 0;
			byte* bdst;
			byte* dst;
			FONSfont renderFont = font;
			if ((isize) < (2)) return null;
			if ((iblur) > (20)) iblur = (short)(20);
			pad = (int)(iblur + 2);
			stash.nscratch = (int)(0);
			h = (uint)(fons__hashint((uint)(codepoint)) & (256 - 1));
			i = (int)(font.lut[h]);
			while (i != -1) {
				if ((((font.glyphs[i].codepoint) == (codepoint)) && ((font.glyphs[i].size) == (isize))) && ((font.glyphs[i].blur) == (iblur))) {
					glyph = &font.glyphs[i]; if (((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL)) || (((glyph->x0) >= (0)) && ((glyph->y0) >= (0)))) {
						return glyph; }
					break; }
				i = (int)(font.glyphs[i].next); }
			g = (int)(fons__tt_getGlyphIndex(&font.font, (int)(codepoint)));
			if ((g) == (0)) {
				for (i = (int)(0); (i) < (font.nfallbacks); ++i) {
					FONSfont fallbackFont = stash.fonts[font.fallbacks[i]]; int fallbackIndex = (int)(fons__tt_getGlyphIndex(&fallbackFont.font, (int)(codepoint))); if (fallbackIndex != 0) {
						g = (int)(fallbackIndex); renderFont = fallbackFont; break; }
				} }

			scale = (float)(fons__tt_getPixelHeightScale(&renderFont.font, (float)(size)));
			fons__tt_buildGlyphBitmap(&renderFont.font, (int)(g), (float)(size), (float)(scale), &advance, &lsb, &x0, &y0, &x1, &y1);
			gw = (int)(x1 - x0 + pad * 2);
			gh = (int)(y1 - y0 + pad * 2);
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_REQUIRED)) {
				added = (int)(fons__atlasAddRect(stash.atlas, (int)(gw), (int)(gh), &gx, &gy)); if (((added) == (0)) && (stash.handleError != null)) {
					stash.handleError(stash.errorUptr, (int)(FONS_ATLAS_FULL), (int)(0)); added = (int)(fons__atlasAddRect(stash.atlas, (int)(gw), (int)(gh), &gx, &gy)); }
				if ((added) == (0)) return null; }
			else {
				gx = (int)(-1); gy = (int)(-1); }

			if ((glyph) == null) {
				glyph = fons__allocGlyph(font); glyph->codepoint = (uint)(codepoint); glyph->size = (short)(isize); glyph->blur = (short)(iblur); glyph->next = (int)(0); glyph->next = (int)(font.lut[h]); font.lut[h] = (int)(font.nglyphs - 1); }

			glyph->index = (int)(g);
			glyph->x0 = ((short)(gx));
			glyph->y0 = ((short)(gy));
			glyph->x1 = ((short)(glyph->x0 + gw));
			glyph->y1 = ((short)(glyph->y0 + gh));
			glyph->xadv = ((short)(scale * advance * 10.0f));
			glyph->xoff = ((short)(x0 - pad));
			glyph->yoff = ((short)(y0 - pad));
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL)) {
				return glyph; }

			dst = &stash.texData[(glyph->x0 + pad) + (glyph->y0 + pad) * stash._params_.width];
			fons__tt_renderGlyphBitmap(&renderFont.font, dst, (int)(gw - pad * 2), (int)(gh - pad * 2), (int)(stash._params_.width), (float)(scale), (float)(scale), (int)(g));
			dst = &stash.texData[glyph->x0 + glyph->y0 * stash._params_.width];
			for (y = (int)(0); (y) < (gh); y++) {
				dst[y * stash._params_.width] = (byte)(0); dst[gw - 1 + y * stash._params_.width] = (byte)(0); }
			for (x = (int)(0); (x) < (gw); x++) {
				dst[x] = (byte)(0); dst[x + (gh - 1) * stash._params_.width] = (byte)(0); }
			if ((iblur) > (0)) {
				stash.nscratch = (int)(0); bdst = &stash.texData[glyph->x0 + glyph->y0 * stash._params_.width]; fons__blur(stash, bdst, (int)(gw), (int)(gh), (int)(stash._params_.width), (int)(iblur)); }

			stash.dirtyRect[0] = (int)(fons__mini((int)(stash.dirtyRect[0]), (int)(glyph->x0)));
			stash.dirtyRect[1] = (int)(fons__mini((int)(stash.dirtyRect[1]), (int)(glyph->y0)));
			stash.dirtyRect[2] = (int)(fons__maxi((int)(stash.dirtyRect[2]), (int)(glyph->x1)));
			stash.dirtyRect[3] = (int)(fons__maxi((int)(stash.dirtyRect[3]), (int)(glyph->y1)));
			return glyph;
		}

		public static void fons__getQuad(FONScontext stash, FONSfont font, int prevGlyphIndex, FONSglyph* glyph, float scale, float spacing, float* x, float* y, FONSquad* q)
		{
			float rx = 0; float ry = 0; float xoff = 0; float yoff = 0; float x0 = 0; float y0 = 0; float x1 = 0; float y1 = 0;
			if (prevGlyphIndex != -1) {
				float adv = (float)(fons__tt_getGlyphKernAdvance(&font.font, (int)(prevGlyphIndex), (int)(glyph->index)) * scale); *x += (float)((int)(adv + spacing + 0.5f)); }

			xoff = (float)((short)(glyph->xoff + 1));
			yoff = (float)((short)(glyph->yoff + 1));
			x0 = ((float)(glyph->x0 + 1));
			y0 = ((float)(glyph->y0 + 1));
			x1 = ((float)(glyph->x1 - 1));
			y1 = ((float)(glyph->y1 - 1));
			if ((stash._params_.flags & FONS_ZERO_TOPLEFT) != 0) {
				rx = ((float)((int)(*x + xoff))); ry = ((float)((int)(*y + yoff))); q->x0 = (float)(rx); q->y0 = (float)(ry); q->x1 = (float)(rx + x1 - x0); q->y1 = (float)(ry + y1 - y0); q->s0 = (float)(x0 * stash.itw); q->t0 = (float)(y0 * stash.ith); q->s1 = (float)(x1 * stash.itw); q->t1 = (float)(y1 * stash.ith); }
			else {
				rx = ((float)((int)(*x + xoff))); ry = ((float)((int)(*y - yoff))); q->x0 = (float)(rx); q->y0 = (float)(ry); q->x1 = (float)(rx + x1 - x0); q->y1 = (float)(ry - y1 + y0); q->s0 = (float)(x0 * stash.itw); q->t0 = (float)(y0 * stash.ith); q->s1 = (float)(x1 * stash.itw); q->t1 = (float)(y1 * stash.ith); }

			*x += (float)((int)(glyph->xadv / 10.0f + 0.5f));
		}

		public static void fons__flush(FONScontext stash)
		{
			if (((stash.dirtyRect[0]) < (stash.dirtyRect[2])) && ((stash.dirtyRect[1]) < (stash.dirtyRect[3]))) {
				if (stash._params_.renderUpdate != null) stash._params_.renderUpdate(stash._params_.userPtr, stash.dirtyRect, stash.texData); stash.dirtyRect[0] = (int)(stash._params_.width); stash.dirtyRect[1] = (int)(stash._params_.height); stash.dirtyRect[2] = (int)(0); stash.dirtyRect[3] = (int)(0); }

			if ((stash.nverts) > (0)) {
				if (stash._params_.renderDraw != null) stash._params_.renderDraw(stash._params_.userPtr, stash.verts, stash.tcoords, stash.colors, (int)(stash.nverts)); stash.nverts = (int)(0); }

		}

		public static void fons__vertex(FONScontext stash, float x, float y, float s, float t, uint c)
		{
			stash.verts[stash.nverts * 2 + 0] = (float)(x);
			stash.verts[stash.nverts * 2 + 1] = (float)(y);
			stash.tcoords[stash.nverts * 2 + 0] = (float)(s);
			stash.tcoords[stash.nverts * 2 + 1] = (float)(t);
			stash.colors[stash.nverts] = (uint)(c);
			stash.nverts++;
		}

		public static float fons__getVertAlign(FONScontext stash, FONSfont font, int align, short isize)
		{
			if ((stash._params_.flags & FONS_ZERO_TOPLEFT) != 0) {
				if ((align & FONS_ALIGN_TOP) != 0) {
					return (float)(font.ascender * (float)(isize) / 10.0f); }
				else if ((align & FONS_ALIGN_MIDDLE) != 0) {
					return (float)((font.ascender + font.descender) / 2.0f * (float)(isize) / 10.0f); }
				else if ((align & FONS_ALIGN_BASELINE) != 0) {
					return (float)(0.0f); }
				else if ((align & FONS_ALIGN_BOTTOM) != 0) {
					return (float)(font.descender * (float)(isize) / 10.0f); }
			}
			else {
				if ((align & FONS_ALIGN_TOP) != 0) {
					return (float)(-font.ascender * (float)(isize) / 10.0f); }
				else if ((align & FONS_ALIGN_MIDDLE) != 0) {
					return (float)(-(font.ascender + font.descender) / 2.0f * (float)(isize) / 10.0f); }
				else if ((align & FONS_ALIGN_BASELINE) != 0) {
					return (float)(0.0f); }
				else if ((align & FONS_ALIGN_BOTTOM) != 0) {
					return (float)(-font.descender * (float)(isize) / 10.0f); }
			}

			return (float)(0.0);
		}

		public static float fonsDrawText(FONScontext stash, float x, float y, sbyte* str, sbyte* end)
		{
			FONSstate* state = fons__getState(stash);
			uint codepoint = 0;
			uint utf8state = (uint)(0);
			FONSglyph* glyph = null;
			FONSquad q = new FONSquad();
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state->size * 10.0f);
			short iblur = (short)(state->blur);
			float scale = 0;
			FONSfont font;
			float width = 0;
			if ((stash) == null) return (float)(x);
			if (((state->font) < (0)) || ((state->font) >= (stash.nfonts))) return (float)(x);
			font = stash.fonts[state->font];
			if ((font.data) == null) return (float)(x);
			scale = (float)(fons__tt_getPixelHeightScale(&font.font, (float)((float)(isize) / 10.0f)));
			if ((end) == null) end = str + CRuntime.strlen(str);
			if ((state->align & FONS_ALIGN_LEFT) != 0) {
			}
			else if ((state->align & FONS_ALIGN_RIGHT) != 0) {
				width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, null)); x -= (float)(width); }
			else if ((state->align & FONS_ALIGN_CENTER) != 0) {
				width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, null)); x -= (float)(width * 0.5f); }

			y += (float)(fons__getVertAlign(stash, font, (int)(state->align), (short)(isize)));
			for (; str != end; ++str) {
				if ((fons__decutf8(&utf8state, &codepoint, (uint)(*(byte*)(str)))) != 0) continue; glyph = fons__getGlyph(stash, font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_REQUIRED)); if (glyph != null) {
					fons__getQuad(stash, font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state->spacing), &x, &y, &q); if ((stash.nverts + 6) > (1024)) fons__flush(stash); fons__vertex(stash, (float)(q.x0), (float)(q.y0), (float)(q.s0), (float)(q.t0), (uint)(state->color)); fons__vertex(stash, (float)(q.x1), (float)(q.y1), (float)(q.s1), (float)(q.t1), (uint)(state->color)); fons__vertex(stash, (float)(q.x1), (float)(q.y0), (float)(q.s1), (float)(q.t0), (uint)(state->color)); fons__vertex(stash, (float)(q.x0), (float)(q.y0), (float)(q.s0), (float)(q.t0), (uint)(state->color)); fons__vertex(stash, (float)(q.x0), (float)(q.y1), (float)(q.s0), (float)(q.t1), (uint)(state->color)); fons__vertex(stash, (float)(q.x1), (float)(q.y1), (float)(q.s1), (float)(q.t1), (uint)(state->color)); }
				prevGlyphIndex = (int)(glyph != null ? glyph->index : -1); }
			fons__flush(stash);
			return (float)(x);
		}

		public static int fonsTextIterInit(FONScontext stash, FONStextIter iter, float x, float y, sbyte* str, sbyte* end, int bitmapOption)
		{
			FONSstate* state = fons__getState(stash);
			float width = 0;
			CRuntime.memset(iter, (int)(0), (ulong)(sizeof((iter))));
			if ((stash) == null) return (int)(0);
			if (((state->font) < (0)) || ((state->font) >= (stash.nfonts))) return (int)(0);
			iter.font = stash.fonts[state->font];
			if ((iter.font.data) == null) return (int)(0);
			iter.isize = ((short)(state->size * 10.0f));
			iter.iblur = ((short)(state->blur));
			iter.scale = (float)(fons__tt_getPixelHeightScale(&iter.font.font, (float)((float)(iter.isize) / 10.0f)));
			if ((state->align & FONS_ALIGN_LEFT) != 0) {
			}
			else if ((state->align & FONS_ALIGN_RIGHT) != 0) {
				width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, null)); x -= (float)(width); }
			else if ((state->align & FONS_ALIGN_CENTER) != 0) {
				width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, null)); x -= (float)(width * 0.5f); }

			y += (float)(fons__getVertAlign(stash, iter.font, (int)(state->align), (short)(iter.isize)));
			if ((end) == null) end = str + CRuntime.strlen(str);
			iter.x = (float)(iter.nextx = (float)(x));
			iter.y = (float)(iter.nexty = (float)(y));
			iter.spacing = (float)(state->spacing);
			iter.str = str;
			iter.next = str;
			iter.end = end;
			iter.codepoint = (uint)(0);
			iter.prevGlyphIndex = (int)(-1);
			iter.bitmapOption = (int)(bitmapOption);
			return (int)(1);
		}

		public static int fonsTextIterNext(FONScontext stash, FONStextIter iter, FONSquad* quad)
		{
			FONSglyph* glyph = null;
			sbyte* str = iter.next;
			iter.str = iter.next;
			if ((str) == (iter.end)) return (int)(0);
			for (; str != iter.end; str++) {
				if ((fons__decutf8(&iter.utf8state, &iter.codepoint, (uint)(*(byte*)(str)))) != 0) continue; str++; iter.x = (float)(iter.nextx); iter.y = (float)(iter.nexty); glyph = fons__getGlyph(stash, iter.font, (uint)(iter.codepoint), (short)(iter.isize), (short)(iter.iblur), (int)(iter.bitmapOption)); if (glyph != null) fons__getQuad(stash, iter.font, (int)(iter.prevGlyphIndex), glyph, (float)(iter.scale), (float)(iter.spacing), &iter.nextx, &iter.nexty, quad); iter.prevGlyphIndex = (int)(glyph != null ? glyph->index : -1); break; }
			iter.next = str;
			return (int)(1);
		}

		public static void fonsDrawDebug(FONScontext stash, float x, float y)
		{
			int i = 0;
			int w = (int)(stash._params_.width);
			int h = (int)(stash._params_.height);
			float u = (float)((w) == (0) ? 0 : (1.0f / w));
			float v = (float)((h) == (0) ? 0 : (1.0f / h));
			if ((stash.nverts + 6 + 6) > (1024)) fons__flush(stash);
			fons__vertex(stash, (float)(x + 0), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex(stash, (float)(x + w), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex(stash, (float)(x + w), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex(stash, (float)(x + 0), (float)(y + 0), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex(stash, (float)(x + 0), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex(stash, (float)(x + w), (float)(y + h), (float)(u), (float)(v), (uint)(0x0fffffff));
			fons__vertex(stash, (float)(x + 0), (float)(y + 0), (float)(0), (float)(0), (uint)(0xffffffff));
			fons__vertex(stash, (float)(x + w), (float)(y + h), (float)(1), (float)(1), (uint)(0xffffffff));
			fons__vertex(stash, (float)(x + w), (float)(y + 0), (float)(1), (float)(0), (uint)(0xffffffff));
			fons__vertex(stash, (float)(x + 0), (float)(y + 0), (float)(0), (float)(0), (uint)(0xffffffff));
			fons__vertex(stash, (float)(x + 0), (float)(y + h), (float)(0), (float)(1), (uint)(0xffffffff));
			fons__vertex(stash, (float)(x + w), (float)(y + h), (float)(1), (float)(1), (uint)(0xffffffff));
			for (i = (int)(0); (i) < (stash.atlas->nnodes); i++) {
				FONSatlasNode* n = &stash.atlas->nodes[i]; if ((stash.nverts + 6) > (1024)) fons__flush(stash); fons__vertex(stash, (float)(x + n->x + 0), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff)); fons__vertex(stash, (float)(x + n->x + n->width), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff)); fons__vertex(stash, (float)(x + n->x + n->width), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff)); fons__vertex(stash, (float)(x + n->x + 0), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff)); fons__vertex(stash, (float)(x + n->x + 0), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff)); fons__vertex(stash, (float)(x + n->x + n->width), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff)); }
			fons__flush(stash);
		}

		public static float fonsTextBounds(FONScontext stash, float x, float y, sbyte* str, sbyte* end, float* bounds)
		{
			FONSstate* state = fons__getState(stash);
			uint codepoint = 0;
			uint utf8state = (uint)(0);
			FONSquad q = new FONSquad();
			FONSglyph* glyph = null;
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state->size * 10.0f);
			short iblur = (short)(state->blur);
			float scale = 0;
			FONSfont font;
			float startx = 0; float advance = 0;
			float minx = 0; float miny = 0; float maxx = 0; float maxy = 0;
			if ((stash) == null) return (float)(0);
			if (((state->font) < (0)) || ((state->font) >= (stash.nfonts))) return (float)(0);
			font = stash.fonts[state->font];
			if ((font.data) == null) return (float)(0);
			scale = (float)(fons__tt_getPixelHeightScale(&font.font, (float)((float)(isize) / 10.0f)));
			y += (float)(fons__getVertAlign(stash, font, (int)(state->align), (short)(isize)));
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			startx = (float)(x);
			if ((end) == null) end = str + CRuntime.strlen(str);
			for (; str != end; ++str) {
				if ((fons__decutf8(&utf8state, &codepoint, (uint)(*(byte*)(str)))) != 0) continue; glyph = fons__getGlyph(stash, font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_OPTIONAL)); if (glyph != null) {
					fons__getQuad(stash, font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state->spacing), &x, &y, &q); if ((q.x0) < (minx)) minx = (float)(q.x0); if ((q.x1) > (maxx)) maxx = (float)(q.x1); if ((stash._params_.flags & FONS_ZERO_TOPLEFT) != 0) {
						if ((q.y0) < (miny)) miny = (float)(q.y0); if ((q.y1) > (maxy)) maxy = (float)(q.y1); }
					else {
						if ((q.y1) < (miny)) miny = (float)(q.y1); if ((q.y0) > (maxy)) maxy = (float)(q.y0); }
				}
				prevGlyphIndex = (int)(glyph != null ? glyph->index : -1); }
			advance = (float)(x - startx);
			if ((state->align & FONS_ALIGN_LEFT) != 0) {
			}
			else if ((state->align & FONS_ALIGN_RIGHT) != 0) {
				minx -= (float)(advance); maxx -= (float)(advance); }
			else if ((state->align & FONS_ALIGN_CENTER) != 0) {
				minx -= (float)(advance * 0.5f); maxx -= (float)(advance * 0.5f); }

			if ((bounds) != null) {
				bounds[0] = (float)(minx); bounds[1] = (float)(miny); bounds[2] = (float)(maxx); bounds[3] = (float)(maxy); }

			return (float)(advance);
		}

		public static void fonsVertMetrics(FONScontext stash, float* ascender, float* descender, float* lineh)
		{
			FONSfont font;
			FONSstate* state = fons__getState(stash);
			short isize = 0;
			if ((stash) == null) return;
			if (((state->font) < (0)) || ((state->font) >= (stash.nfonts))) return;
			font = stash.fonts[state->font];
			isize = ((short)(state->size * 10.0f));
			if ((font.data) == null) return;
			if ((ascender) != null) *ascender = (float)(font.ascender * isize / 10.0f);
			if ((descender) != null) *descender = (float)(font.descender * isize / 10.0f);
			if ((lineh) != null) *lineh = (float)(font.lineh * isize / 10.0f);
		}

		public static void fonsLineBounds(FONScontext stash, float y, float* miny, float* maxy)
		{
			FONSfont font;
			FONSstate* state = fons__getState(stash);
			short isize = 0;
			if ((stash) == null) return;
			if (((state->font) < (0)) || ((state->font) >= (stash.nfonts))) return;
			font = stash.fonts[state->font];
			isize = ((short)(state->size * 10.0f));
			if ((font.data) == null) return;
			y += (float)(fons__getVertAlign(stash, font, (int)(state->align), (short)(isize)));
			if ((stash._params_.flags & FONS_ZERO_TOPLEFT) != 0) {
				*miny = (float)(y - font.ascender * (float)(isize) / 10.0f); *maxy = (float)(*miny + font.lineh * isize / 10.0f); }
			else {
				*maxy = (float)(y + font.descender * (float)(isize) / 10.0f); *miny = (float)(*maxy - font.lineh * isize / 10.0f); }

		}

		public static byte* fonsGetTextureData(FONScontext stash, int* width, int* height)
		{
			if (width != null) *width = (int)(stash._params_.width);
			if (height != null) *height = (int)(stash._params_.height);
			return stash.texData;
		}

		public static int fonsValidateTexture(FONScontext stash, int* dirty)
		{
			if (((stash.dirtyRect[0]) < (stash.dirtyRect[2])) && ((stash.dirtyRect[1]) < (stash.dirtyRect[3]))) {
				dirty[0] = (int)(stash.dirtyRect[0]); dirty[1] = (int)(stash.dirtyRect[1]); dirty[2] = (int)(stash.dirtyRect[2]); dirty[3] = (int)(stash.dirtyRect[3]); stash.dirtyRect[0] = (int)(stash._params_.width); stash.dirtyRect[1] = (int)(stash._params_.height); stash.dirtyRect[2] = (int)(0); stash.dirtyRect[3] = (int)(0); return (int)(1); }

			return (int)(0);
		}

		public static void fonsDeleteInternal(FONScontext stash)
		{
			int i = 0;
			if ((stash) == null) return;
			if ((stash._params_.renderDelete) != null) stash._params_.renderDelete(stash._params_.userPtr);
			for (i = (int)(0); (i) < (stash.nfonts); ++i) { fons__freeFont(stash.fonts[i]); }
			if ((stash.atlas) != null) fons__deleteAtlas(stash.atlas);
			if ((stash.fonts) != null) CRuntime.free(stash.fonts);
			if ((stash.texData) != null) CRuntime.free(stash.texData);
			if ((stash.scratch) != null) CRuntime.free(stash.scratch);
			CRuntime.free(stash);
			fons__tt_done(stash);
		}

		public static void fonsSetErrorCallback(FONScontext stash, void (void*, int, int) * callback, void* uptr)
		{
			if ((stash) == null) return;
			stash.handleError = callback;
			stash.errorUptr = uptr;
		}

		public static void fonsGetAtlasSize(FONScontext stash, int* width, int* height)
		{
			if ((stash) == null) return;
			*width = (int)(stash._params_.width);
			*height = (int)(stash._params_.height);
		}

		public static int fonsExpandAtlas(FONScontext stash, int width, int height)
		{
			int i = 0; int maxy = (int)(0);
			byte* data = null;
			if ((stash) == null) return (int)(0);
			width = (int)(fons__maxi((int)(width), (int)(stash._params_.width)));
			height = (int)(fons__maxi((int)(height), (int)(stash._params_.height)));
			if (((width) == (stash._params_.width)) && ((height) == (stash._params_.height))) return (int)(1);
			fons__flush(stash);
			if (stash._params_.renderResize != null) {
				if ((stash._params_.renderResize(stash._params_.userPtr, (int)(width), (int)(height))) == (0)) return (int)(0); }

			data = (byte*)(CRuntime.malloc((ulong)(width * height)));
			if ((data) == null) return (int)(0);
			for (i = (int)(0); (i) < (stash._params_.height); i++) {
				byte* dst = &data[i * width]; byte* src = &stash.texData[i * stash._params_.width]; CRuntime.memcpy(dst, src, (ulong)(stash._params_.width)); if ((width) > (stash._params_.width)) CRuntime.memset(dst + stash._params_.width, (int)(0), (ulong)(width - stash._params_.width)); }
			if ((height) > (stash._params_.height)) CRuntime.memset(&data[stash._params_.height * width], (int)(0), (ulong)((height - stash._params_.height) * width));
			CRuntime.free(stash.texData);
			stash.texData = data;
			fons__atlasExpand(stash.atlas, (int)(width), (int)(height));
			for (i = (int)(0); (i) < (stash.atlas->nnodes); i++) { maxy = (int)(fons__maxi((int)(maxy), (int)(stash.atlas->nodes[i].y))); }
			stash.dirtyRect[0] = (int)(0);
			stash.dirtyRect[1] = (int)(0);
			stash.dirtyRect[2] = (int)(stash._params_.width);
			stash.dirtyRect[3] = (int)(maxy);
			stash._params_.width = (int)(width);
			stash._params_.height = (int)(height);
			stash.itw = (float)(1.0f / stash._params_.width);
			stash.ith = (float)(1.0f / stash._params_.height);
			return (int)(1);
		}

		public static int fonsResetAtlas(FONScontext stash, int width, int height)
		{
			int i = 0; int j = 0;
			if ((stash) == null) return (int)(0);
			fons__flush(stash);
			if (stash._params_.renderResize != null) {
				if ((stash._params_.renderResize(stash._params_.userPtr, (int)(width), (int)(height))) == (0)) return (int)(0); }

			fons__atlasReset(stash.atlas, (int)(width), (int)(height));
			stash.texData = (byte*)(CRuntime.realloc(stash.texData, (ulong)(width * height)));
			if ((stash.texData) == null) return (int)(0);
			CRuntime.memset(stash.texData, (int)(0), (ulong)(width * height));
			stash.dirtyRect[0] = (int)(width);
			stash.dirtyRect[1] = (int)(height);
			stash.dirtyRect[2] = (int)(0);
			stash.dirtyRect[3] = (int)(0);
			for (i = (int)(0); (i) < (stash.nfonts); i++) {
				FONSfont font = stash.fonts[i]; font.nglyphs = (int)(0); for (j = (int)(0); (j) < (256); j++) { font.lut[j] = (int)(-1); } }
			stash._params_.width = (int)(width);
			stash._params_.height = (int)(height);
			stash.itw = (float)(1.0f / stash._params_.width);
			stash.ith = (float)(1.0f / stash._params_.height);
			fons__addWhiteRect(stash, (int)(2), (int)(2));
			return (int)(1);
		}

		public static float nvg__sqrtf(float a)
		{
			return (float)(sqrtf((float)(a)));
		}

		public static float nvg__modf(float a, float b)
		{
			return (float)(fmodf((float)(a), (float)(b)));
		}

		public static float nvg__sinf(float a)
		{
			return (float)(sinf((float)(a)));
		}

		public static float nvg__cosf(float a)
		{
			return (float)(cosf((float)(a)));
		}

		public static float nvg__tanf(float a)
		{
			return (float)(tanf((float)(a)));
		}

		public static float nvg__atan2f(float a, float b)
		{
			return (float)(atan2f((float)(a), (float)(b)));
		}

		public static float nvg__acosf(float a)
		{
			return (float)(acosf((float)(a)));
		}

		public static int nvg__mini(int a, int b)
		{
			return (int)((a) < (b) ? a : b);
		}

		public static int nvg__maxi(int a, int b)
		{
			return (int)((a) > (b) ? a : b);
		}

		public static int nvg__clampi(int a, int mn, int mx)
		{
			return (int)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		public static float nvg__minf(float a, float b)
		{
			return (float)((a) < (b) ? a : b);
		}

		public static float nvg__maxf(float a, float b)
		{
			return (float)((a) > (b) ? a : b);
		}

		public static float nvg__absf(float a)
		{
			return (float)((a) >= (0.0f) ? a : -a);
		}

		public static float nvg__signf(float a)
		{
			return (float)((a) >= (0.0f) ? 1.0f : -1.0f);
		}

		public static float nvg__clampf(float a, float mn, float mx)
		{
			return (float)((a) < (mn) ? mn : ((a) > (mx) ? mx : a));
		}

		public static float nvg__cross(float dx0, float dy0, float dx1, float dy1)
		{
			return (float)(dx1 * dy0 - dx0 * dy1);
		}

		public static float nvg__normalize(float* x, float* y)
		{
			float d = (float)(nvg__sqrtf((float)((*x) * (*x) + (*y) * (*y))));
			if ((d) > (1e-6f)) {
				float id = (float)(1.0f / d); *x *= (float)(id); *y *= (float)(id); }

			return (float)(d);
		}

		public static void nvg__deletePathCache(NVGpathCache c)
		{
			if ((c) == null) return;
			if (c.points != null) CRuntime.free(c.points);
			if (c.paths != null) CRuntime.free(c.paths);
			if (c.verts != null) CRuntime.free(c.verts);
		}

		public static NVGpathCache nvg__allocPathCache()
		{
			NVGpathCache c = new NVGpathCache();
			if ((c) == null) goto error;
			c.points = (NVGpoint*)(CRuntime.malloc((ulong)(sizeof(NVGpoint) * 128)));
			if (c.points == null) goto error;
			c.npoints = (int)(0);
			c.cpoints = (int)(128);
			c.paths = (NVGpath*)(CRuntime.malloc((ulong)(sizeof(NVGpath) * 16)));
			if (c.paths == null) goto error;
			c.npaths = (int)(0);
			c.cpaths = (int)(16);
			c.verts = (NVGvertex*)(CRuntime.malloc((ulong)(sizeof(NVGvertex) * 256)));
			if (c.verts == null) goto error;
			c.nverts = (int)(0);
			c.cverts = (int)(256);
			return c;
		error:;
			nvg__deletePathCache(c);
			return null;
		}

		public static void nvg__setDevicePixelRatio(NVGcontext ctx, float ratio)
		{
			ctx.tessTol = (float)(0.25f / ratio);
			ctx.distTol = (float)(0.01f / ratio);
			ctx.fringeWidth = (float)(1.0f / ratio);
			ctx.devicePxRatio = (float)(ratio);
		}

		public static NVGcompositeOperationState nvg__compositeOperationState(int op)
		{
			int sfactor = 0; int dfactor = 0;
			if ((op) == (NVG_SOURCE_OVER)) {
				sfactor = (int)(NVG_ONE); dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA); }
			else if ((op) == (NVG_SOURCE_IN)) {
				sfactor = (int)(NVG_DST_ALPHA); dfactor = (int)(NVG_ZERO); }
			else if ((op) == (NVG_SOURCE_OUT)) {
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA); dfactor = (int)(NVG_ZERO); }
			else if ((op) == (NVG_ATOP)) {
				sfactor = (int)(NVG_DST_ALPHA); dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA); }
			else if ((op) == (NVG_DESTINATION_OVER)) {
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA); dfactor = (int)(NVG_ONE); }
			else if ((op) == (NVG_DESTINATION_IN)) {
				sfactor = (int)(NVG_ZERO); dfactor = (int)(NVG_SRC_ALPHA); }
			else if ((op) == (NVG_DESTINATION_OUT)) {
				sfactor = (int)(NVG_ZERO); dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA); }
			else if ((op) == (NVG_DESTINATION_ATOP)) {
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA); dfactor = (int)(NVG_SRC_ALPHA); }
			else if ((op) == (NVG_LIGHTER)) {
				sfactor = (int)(NVG_ONE); dfactor = (int)(NVG_ONE); }
			else if ((op) == (NVG_COPY)) {
				sfactor = (int)(NVG_ONE); dfactor = (int)(NVG_ZERO); }
			else if ((op) == (NVG_XOR)) {
				sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA); dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA); }
			else {
				sfactor = (int)(NVG_ONE); dfactor = (int)(NVG_ZERO); }

			NVGcompositeOperationState state = new NVGcompositeOperationState();
			state.srcRGB = (int)(sfactor);
			state.dstRGB = (int)(dfactor);
			state.srcAlpha = (int)(sfactor);
			state.dstAlpha = (int)(dfactor);
			return (NVGcompositeOperationState)(state);
		}

		public static NVGstate* nvg__getState(NVGcontext ctx)
		{
			return &ctx.states[ctx.nstates - 1];
		}

		public static NVGcontext nvgCreateInternal(NVGparams _params_)
		{
			FONSparams fontParams = new FONSparams();
			NVGcontext ctx = new NVGcontext();
			int i = 0;
			if ((ctx) == null) goto error;
			ctx._params_ = (NVGparams)(_params_);
			for (i = (int)(0); (i) < (4); i++) { ctx.fontImages[i] = (int)(0); }
			ctx.commands = (float*)(CRuntime.malloc((ulong)(sizeof(float) * 256)));
			if (ctx.commands == null) goto error;
			ctx.ncommands = (int)(0);
			ctx.ccommands = (int)(256);
			ctx.cache = nvg__allocPathCache();
			if ((ctx.cache) == null) goto error;
			nvgSave(ctx);
			nvgReset(ctx);
			nvg__setDevicePixelRatio(ctx, (float)(1.0f));
			if ((ctx._params_.renderCreate(ctx._params_.userPtr)) == (0)) goto error;
			fontParams.width = (int)(512);
			fontParams.height = (int)(512);
			fontParams.flags = (byte)(FONS_ZERO_TOPLEFT);
			fontParams.renderCreate = null;
			fontParams.renderUpdate = null;
			fontParams.renderDraw = null;
			fontParams.renderDelete = null;
			fontParams.userPtr = null;
			ctx.fs = fonsCreateInternal(fontParams);
			if ((ctx.fs) == null) goto error;
			ctx.fontImages[0] = (int)(ctx._params_.renderCreateTexture(ctx._params_.userPtr, (int)(NVG_TEXTURE_ALPHA), (int)(fontParams.width), (int)(fontParams.height), (int)(0), null));
			if ((ctx.fontImages[0]) == (0)) goto error;
			ctx.fontImageIdx = (int)(0);
			return ctx;
		error:;
			nvgDeleteInternal(ctx);
			return null;
		}

		public static NVGparams nvgInternalParams(NVGcontext ctx)
		{
			return ctx._params_;
		}

		public static void nvgDeleteInternal(NVGcontext ctx)
		{
			int i = 0;
			if ((ctx) == null) return;
			if (ctx.commands != null) CRuntime.free(ctx.commands);
			if (ctx.cache != null) nvg__deletePathCache(ctx.cache);
			if ((ctx.fs) != null) fonsDeleteInternal(ctx.fs);
			for (i = (int)(0); (i) < (4); i++) {
				if (ctx.fontImages[i] != 0) {
					nvgDeleteImage(ctx, (int)(ctx.fontImages[i])); ctx.fontImages[i] = (int)(0); }
			}
			if (ctx._params_.renderDelete != null) ctx._params_.renderDelete(ctx._params_.userPtr);
		}

		public static void nvgBeginFrame(NVGcontext ctx, float windowWidth, float windowHeight, float devicePixelRatio)
		{
			ctx.nstates = (int)(0);
			nvgSave(ctx);
			nvgReset(ctx);
			nvg__setDevicePixelRatio(ctx, (float)(devicePixelRatio));
			ctx._params_.renderViewport(ctx._params_.userPtr, (float)(windowWidth), (float)(windowHeight), (float)(devicePixelRatio));
			ctx.drawCallCount = (int)(0);
			ctx.fillTriCount = (int)(0);
			ctx.strokeTriCount = (int)(0);
			ctx.textTriCount = (int)(0);
		}

		public static void nvgCancelFrame(NVGcontext ctx)
		{
			ctx._params_.renderCancel(ctx._params_.userPtr);
		}

		public static void nvgEndFrame(NVGcontext ctx)
		{
			ctx._params_.renderFlush(ctx._params_.userPtr);
			if (ctx.fontImageIdx != 0) {
				int fontImage = (int)(ctx.fontImages[ctx.fontImageIdx]); int i = 0; int j = 0; int iw = 0; int ih = 0; if ((fontImage) == (0)) return; nvgImageSize(ctx, (int)(fontImage), &iw, &ih); for (i = (int)(j = (int)(0)); (i) < (ctx.fontImageIdx); i++) {
					if (ctx.fontImages[i] != 0) {
						int nw = 0; int nh = 0; nvgImageSize(ctx, (int)(ctx.fontImages[i]), &nw, &nh); if (((nw) < (iw)) || ((nh) < (ih))) nvgDeleteImage(ctx, (int)(ctx.fontImages[i])); else ctx.fontImages[j++] = (int)(ctx.fontImages[i]); }
				} ctx.fontImages[j++] = (int)(ctx.fontImages[0]); ctx.fontImages[0] = (int)(fontImage); ctx.fontImageIdx = (int)(0); for (i = (int)(j); (i) < (4); i++) { ctx.fontImages[i] = (int)(0); } }

		}

		public static NVGcolor nvgRGB(byte r, byte g, byte b)
		{
			return (NVGcolor)(nvgRGBA((byte)(r), (byte)(g), (byte)(b), (byte)(255)));
		}

		public static NVGcolor nvgRGBf(float r, float g, float b)
		{
			return (NVGcolor)(nvgRGBAf((float)(r), (float)(g), (float)(b), (float)(1.0f)));
		}

		public static NVGcolor nvgRGBA(byte r, byte g, byte b, byte a)
		{
			NVGcolor color = new NVGcolor();
			color.r = (float)(r / 255.0f);
			color.g = (float)(g / 255.0f);
			color.b = (float)(b / 255.0f);
			color.a = (float)(a / 255.0f);
			return (NVGcolor)(color);
		}

		public static NVGcolor nvgRGBAf(float r, float g, float b, float a)
		{
			NVGcolor color = new NVGcolor();
			color.r = (float)(r);
			color.g = (float)(g);
			color.b = (float)(b);
			color.a = (float)(a);
			return (NVGcolor)(color);
		}

		public static NVGcolor nvgTransRGBA(NVGcolor c, byte a)
		{
			c.a = (float)(a / 255.0f);
			return (NVGcolor)(c);
		}

		public static NVGcolor nvgTransRGBAf(NVGcolor c, float a)
		{
			c.a = (float)(a);
			return (NVGcolor)(c);
		}

		public static NVGcolor nvgLerpRGBA(NVGcolor c0, NVGcolor c1, float u)
		{
			int i = 0;
			float oneminu = 0;
			NVGcolor cint = new NVGcolor();
			u = (float)(nvg__clampf((float)(u), (float)(0.0f), (float)(1.0f)));
			oneminu = (float)(1.0f - u);
			for (i = (int)(0); (i) < (4); i++) {
				cint.r = (float)(c0.r * oneminu + c1.r * u); cint.g = (float)(c0.g * oneminu + c1.g * u); cint.b = (float)(c0.b * oneminu + c1.b * u); cint.a = (float)(c0.a * oneminu + c1.a * u); }
			return cint;
		}

		public static NVGcolor nvgHSL(float h, float s, float l)
		{
			return (NVGcolor)(nvgHSLA((float)(h), (float)(s), (float)(l), (byte)(255)));
		}

		public static float nvg__hue(float h, float m1, float m2)
		{
			if ((h) < (0)) h += (float)(1);
			if ((h) > (1)) h -= (float)(1);
			if ((h) < (1.0f / 6.0f)) return (float)(m1 + (m2 - m1) * h * 6.0f); else if ((h) < (3.0f / 6.0f)) return (float)(m2); else if ((h) < (4.0f / 6.0f)) return (float)(m1 + (m2 - m1) * (2.0f / 3.0f - h) * 6.0f);
			return (float)(m1);
		}

		public static NVGcolor nvgHSLA(float h, float s, float l, byte a)
		{
			float m1 = 0; float m2 = 0;
			NVGcolor col = new NVGcolor();
			h = (float)(nvg__modf((float)(h), (float)(1.0f)));
			if ((h) < (0.0f)) h += (float)(1.0f);
			s = (float)(nvg__clampf((float)(s), (float)(0.0f), (float)(1.0f)));
			l = (float)(nvg__clampf((float)(l), (float)(0.0f), (float)(1.0f)));
			m2 = (float)(l <= 0.5f ? (l * (1 + s)) : (l + s - l * s));
			m1 = (float)(2 * l - m2);
			col.r = (float)(nvg__clampf((float)(nvg__hue((float)(h + 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			col.g = (float)(nvg__clampf((float)(nvg__hue((float)(h), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			col.b = (float)(nvg__clampf((float)(nvg__hue((float)(h - 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			col.a = (float)(a / 255.0f);
			return (NVGcolor)(col);
		}

		public static void nvgTransformIdentity(float* t)
		{
			t[0] = (float)(1.0f);
			t[1] = (float)(0.0f);
			t[2] = (float)(0.0f);
			t[3] = (float)(1.0f);
			t[4] = (float)(0.0f);
			t[5] = (float)(0.0f);
		}

		public static void nvgTransformTranslate(float* t, float tx, float ty)
		{
			t[0] = (float)(1.0f);
			t[1] = (float)(0.0f);
			t[2] = (float)(0.0f);
			t[3] = (float)(1.0f);
			t[4] = (float)(tx);
			t[5] = (float)(ty);
		}

		public static void nvgTransformScale(float* t, float sx, float sy)
		{
			t[0] = (float)(sx);
			t[1] = (float)(0.0f);
			t[2] = (float)(0.0f);
			t[3] = (float)(sy);
			t[4] = (float)(0.0f);
			t[5] = (float)(0.0f);
		}

		public static void nvgTransformRotate(float* t, float a)
		{
			float cs = (float)(nvg__cosf((float)(a))); float sn = (float)(nvg__sinf((float)(a)));
			t[0] = (float)(cs);
			t[1] = (float)(sn);
			t[2] = (float)(-sn);
			t[3] = (float)(cs);
			t[4] = (float)(0.0f);
			t[5] = (float)(0.0f);
		}

		public static void nvgTransformSkewX(float* t, float a)
		{
			t[0] = (float)(1.0f);
			t[1] = (float)(0.0f);
			t[2] = (float)(nvg__tanf((float)(a)));
			t[3] = (float)(1.0f);
			t[4] = (float)(0.0f);
			t[5] = (float)(0.0f);
		}

		public static void nvgTransformSkewY(float* t, float a)
		{
			t[0] = (float)(1.0f);
			t[1] = (float)(nvg__tanf((float)(a)));
			t[2] = (float)(0.0f);
			t[3] = (float)(1.0f);
			t[4] = (float)(0.0f);
			t[5] = (float)(0.0f);
		}

		public static void nvgTransformMultiply(float* t, float* s)
		{
			float t0 = (float)(t[0] * s[0] + t[1] * s[2]);
			float t2 = (float)(t[2] * s[0] + t[3] * s[2]);
			float t4 = (float)(t[4] * s[0] + t[5] * s[2] + s[4]);
			t[1] = (float)(t[0] * s[1] + t[1] * s[3]);
			t[3] = (float)(t[2] * s[1] + t[3] * s[3]);
			t[5] = (float)(t[4] * s[1] + t[5] * s[3] + s[5]);
			t[0] = (float)(t0);
			t[2] = (float)(t2);
			t[4] = (float)(t4);
		}

		public static void nvgTransformPremultiply(float* t, float* s)
		{
			float* s2 = stackalloc float[6];
			CRuntime.memcpy(s2, s, (ulong)(sizeof(float) * 6));
			nvgTransformMultiply(s2, t);
			CRuntime.memcpy(t, s2, (ulong)(sizeof(float) * 6));
		}

		public static int nvgTransformInverse(float* inv, float* t)
		{
			double invdet = 0; double det = (double)((double)(t[0]) * t[3] - (double)(t[2]) * t[1]);
			if (((det) > (-1e-6)) && ((det) < (1e-6))) {
				nvgTransformIdentity(inv); return (int)(0); }

			invdet = (double)(1.0 / det);
			inv[0] = ((float)(t[3] * invdet));
			inv[2] = ((float)(-t[2] * invdet));
			inv[4] = ((float)(((double)(t[2]) * t[5] - (double)(t[3]) * t[4]) * invdet));
			inv[1] = ((float)(-t[1] * invdet));
			inv[3] = ((float)(t[0] * invdet));
			inv[5] = ((float)(((double)(t[1]) * t[4] - (double)(t[0]) * t[5]) * invdet));
			return (int)(1);
		}

		public static void nvgTransformPoint(float* dx, float* dy, float* t, float sx, float sy)
		{
			*dx = (float)(sx * t[0] + sy * t[2] + t[4]);
			*dy = (float)(sx * t[1] + sy * t[3] + t[5]);
		}

		public static float nvgDegToRad(float deg)
		{
			return (float)(deg / 180.0f * 3.14159274);
		}

		public static float nvgRadToDeg(float rad)
		{
			return (float)(rad / 3.14159274 * 180.0f);
		}

		public static void nvg__setPaintColor(NVGpaint* p, NVGcolor color)
		{
			CRuntime.memset(p, (int)(0), (ulong)(sizeof(NVGpaint)));
			nvgTransformIdentity(p->xform);
			p->radius = (float)(0.0f);
			p->feather = (float)(1.0f);
			p->innerColor = (NVGcolor)(color);
			p->outerColor = (NVGcolor)(color);
		}

		public static void nvgSave(NVGcontext ctx)
		{
			if ((ctx.nstates) >= (32)) return;
			if ((ctx.nstates) > (0)) CRuntime.memcpy(&ctx.states[ctx.nstates], &ctx.states[ctx.nstates - 1], (ulong)(sizeof(NVGstate)));
			ctx.nstates++;
		}

		public static void nvgRestore(NVGcontext ctx)
		{
			if (ctx.nstates <= 1) return;
			ctx.nstates--;
		}

		public static void nvgReset(NVGcontext ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			CRuntime.memset(state, (int)(0), (ulong)(sizeof(NVGstate)));
			nvg__setPaintColor(&state->fill, (NVGcolor)(nvgRGBA((byte)(255), (byte)(255), (byte)(255), (byte)(255))));
			nvg__setPaintColor(&state->stroke, (NVGcolor)(nvgRGBA((byte)(0), (byte)(0), (byte)(0), (byte)(255))));
			state->compositeOperation = (NVGcompositeOperationState)(nvg__compositeOperationState((int)(NVG_SOURCE_OVER)));
			state->shapeAntiAlias = (int)(1);
			state->strokeWidth = (float)(1.0f);
			state->miterLimit = (float)(10.0f);
			state->lineCap = (int)(NVG_BUTT);
			state->lineJoin = (int)(NVG_MITER);
			state->alpha = (float)(1.0f);
			nvgTransformIdentity(state->xform);
			state->scissor.extent[0] = (float)(-1.0f);
			state->scissor.extent[1] = (float)(-1.0f);
			state->fontSize = (float)(16.0f);
			state->letterSpacing = (float)(0.0f);
			state->lineHeight = (float)(1.0f);
			state->fontBlur = (float)(0.0f);
			state->textAlign = (int)(NVG_ALIGN_LEFT | NVG_ALIGN_BASELINE);
			state->fontId = (int)(0);
		}

		public static void nvgShapeAntiAlias(NVGcontext ctx, int enabled)
		{
			NVGstate* state = nvg__getState(ctx);
			state->shapeAntiAlias = (int)(enabled);
		}

		public static void nvgStrokeWidth(NVGcontext ctx, float width)
		{
			NVGstate* state = nvg__getState(ctx);
			state->strokeWidth = (float)(width);
		}

		public static void nvgMiterLimit(NVGcontext ctx, float limit)
		{
			NVGstate* state = nvg__getState(ctx);
			state->miterLimit = (float)(limit);
		}

		public static void nvgLineCap(NVGcontext ctx, int cap)
		{
			NVGstate* state = nvg__getState(ctx);
			state->lineCap = (int)(cap);
		}

		public static void nvgLineJoin(NVGcontext ctx, int join)
		{
			NVGstate* state = nvg__getState(ctx);
			state->lineJoin = (int)(join);
		}

		public static void nvgGlobalAlpha(NVGcontext ctx, float alpha)
		{
			NVGstate* state = nvg__getState(ctx);
			state->alpha = (float)(alpha);
		}

		public static void nvgTransform(NVGcontext ctx, float a, float b, float c, float d, float e, float f)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			t[0] = (float)(a);
			t[1] = (float)(b);
			t[2] = (float)(c);
			t[3] = (float)(d);
			t[4] = (float)(e);
			t[5] = (float)(f);

			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgResetTransform(NVGcontext ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			nvgTransformIdentity(state->xform);
		}

		public static void nvgTranslate(NVGcontext ctx, float x, float y)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformTranslate(t, (float)(x), (float)(y));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgRotate(NVGcontext ctx, float angle)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformRotate(t, (float)(angle));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgSkewX(NVGcontext ctx, float angle)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformSkewX(t, (float)(angle));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgSkewY(NVGcontext ctx, float angle)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformSkewY(t, (float)(angle));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgScale(NVGcontext ctx, float x, float y)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformScale(t, (float)(x), (float)(y));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgCurrentTransform(NVGcontext ctx, float* xform)
		{
			NVGstate* state = nvg__getState(ctx);
			if ((xform) == null) return;
			CRuntime.memcpy(xform, state->xform, (ulong)(sizeof(float) * 6));
		}

		public static void nvgStrokeColor(NVGcontext ctx, NVGcolor color)
		{
			NVGstate* state = nvg__getState(ctx);
			nvg__setPaintColor(&state->stroke, (NVGcolor)(color));
		}

		public static void nvgStrokePaint(NVGcontext ctx, NVGpaint paint)
		{
			NVGstate* state = nvg__getState(ctx);
			state->stroke = (NVGpaint)(paint);
			nvgTransformMultiply(state->stroke.xform, state->xform);
		}

		public static void nvgFillColor(NVGcontext ctx, NVGcolor color)
		{
			NVGstate* state = nvg__getState(ctx);
			nvg__setPaintColor(&state->fill, (NVGcolor)(color));
		}

		public static void nvgFillPaint(NVGcontext ctx, NVGpaint paint)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fill = (NVGpaint)(paint);
			nvgTransformMultiply(state->fill.xform, state->xform);
		}

		public static int nvgCreateImageMem(NVGcontext ctx, int imageFlags, byte* data, int ndata)
		{
			int w = 0; int h = 0; int n = 0; int image = 0;
			byte* img = StbImage.stbi_load_from_memory(data, (int)(ndata), &w, &h, &n, (int)(4));
			if ((img) == null) {
				return (int)(0); }

			image = (int)(nvgCreateImageRGBA(ctx, (int)(w), (int)(h), (int)(imageFlags), img));

			CRuntime.free(img);
			return (int)(image);
		}

		public static int nvgCreateImageRGBA(NVGcontext ctx, int w, int h, int imageFlags, byte* data)
		{
			return (int)(ctx._params_.renderCreateTexture(ctx._params_.userPtr, (int)(NVG_TEXTURE_RGBA), (int)(w), (int)(h), (int)(imageFlags), data));
		}

		public static void nvgUpdateImage(NVGcontext ctx, int image, byte* data)
		{
			int w = 0; int h = 0;
			ctx._params_.renderGetTextureSize(ctx._params_.userPtr, (int)(image), &w, &h);
			ctx._params_.renderUpdateTexture(ctx._params_.userPtr, (int)(image), (int)(0), (int)(0), (int)(w), (int)(h), data);
		}

		public static void nvgImageSize(NVGcontext ctx, int image, int* w, int* h)
		{
			ctx._params_.renderGetTextureSize(ctx._params_.userPtr, (int)(image), w, h);
		}

		public static void nvgDeleteImage(NVGcontext ctx, int image)
		{
			ctx._params_.renderDeleteTexture(ctx._params_.userPtr, (int)(image));
		}

		public static NVGpaint nvgLinearGradient(NVGcontext ctx, float sx, float sy, float ex, float ey, NVGcolor icol, NVGcolor ocol)
		{
			NVGpaint p = new NVGpaint();
			float dx = 0; float dy = 0; float d = 0;
			float large = (float)(1e5);
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof(NVGpaint)));
			dx = (float)(ex - sx);
			dy = (float)(ey - sy);
			d = (float)(sqrtf((float)(dx * dx + dy * dy)));
			if ((d) > (0.0001f)) {
				dx /= (float)(d); dy /= (float)(d); }
			else {
				dx = (float)(0); dy = (float)(1); }

			p.xform[0] = (float)(dy);
			p.xform[1] = (float)(-dx);
			p.xform[2] = (float)(dx);
			p.xform[3] = (float)(dy);
			p.xform[4] = (float)(sx - dx * large);
			p.xform[5] = (float)(sy - dy * large);
			p.extent[0] = (float)(large);
			p.extent[1] = (float)(large + d * 0.5f);
			p.radius = (float)(0.0f);
			p.feather = (float)(nvg__maxf((float)(1.0f), (float)(d)));
			p.innerColor = (NVGcolor)(icol);
			p.outerColor = (NVGcolor)(ocol);
			return (NVGpaint)(p);
		}

		public static NVGpaint nvgRadialGradient(NVGcontext ctx, float cx, float cy, float inr, float outr, NVGcolor icol, NVGcolor ocol)
		{
			NVGpaint p = new NVGpaint();
			float r = (float)((inr + outr) * 0.5f);
			float f = (float)(outr - inr);
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof(NVGpaint)));
			nvgTransformIdentity(p.xform);
			p.xform[4] = (float)(cx);
			p.xform[5] = (float)(cy);
			p.extent[0] = (float)(r);
			p.extent[1] = (float)(r);
			p.radius = (float)(r);
			p.feather = (float)(nvg__maxf((float)(1.0f), (float)(f)));
			p.innerColor = (NVGcolor)(icol);
			p.outerColor = (NVGcolor)(ocol);
			return (NVGpaint)(p);
		}

		public static NVGpaint nvgBoxGradient(NVGcontext ctx, float x, float y, float w, float h, float r, float f, NVGcolor icol, NVGcolor ocol)
		{
			NVGpaint p = new NVGpaint();
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof(NVGpaint)));
			nvgTransformIdentity(p.xform);
			p.xform[4] = (float)(x + w * 0.5f);
			p.xform[5] = (float)(y + h * 0.5f);
			p.extent[0] = (float)(w * 0.5f);
			p.extent[1] = (float)(h * 0.5f);
			p.radius = (float)(r);
			p.feather = (float)(nvg__maxf((float)(1.0f), (float)(f)));
			p.innerColor = (NVGcolor)(icol);
			p.outerColor = (NVGcolor)(ocol);
			return (NVGpaint)(p);
		}

		public static NVGpaint nvgImagePattern(NVGcontext ctx, float cx, float cy, float w, float h, float angle, int image, float alpha)
		{
			NVGpaint p = new NVGpaint();
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof(NVGpaint)));
			nvgTransformRotate(p.xform, (float)(angle));
			p.xform[4] = (float)(cx);
			p.xform[5] = (float)(cy);
			p.extent[0] = (float)(w);
			p.extent[1] = (float)(h);
			p.image = (int)(image);
			p.innerColor = (NVGcolor)(p.outerColor = (NVGcolor)(nvgRGBAf((float)(1), (float)(1), (float)(1), (float)(alpha))));
			return (NVGpaint)(p);
		}

		public static void nvgScissor(NVGcontext ctx, float x, float y, float w, float h)
		{
			NVGstate* state = nvg__getState(ctx);
			w = (float)(nvg__maxf((float)(0.0f), (float)(w)));
			h = (float)(nvg__maxf((float)(0.0f), (float)(h)));
			nvgTransformIdentity(state->scissor.xform);
			state->scissor.xform[4] = (float)(x + w * 0.5f);
			state->scissor.xform[5] = (float)(y + h * 0.5f);
			nvgTransformMultiply(state->scissor.xform, state->xform);
			state->scissor.extent[0] = (float)(w * 0.5f);
			state->scissor.extent[1] = (float)(h * 0.5f);
		}

		public static void nvg__isectRects(float* dst, float ax, float ay, float aw, float ah, float bx, float by, float bw, float bh)
		{
			float minx = (float)(nvg__maxf((float)(ax), (float)(bx)));
			float miny = (float)(nvg__maxf((float)(ay), (float)(by)));
			float maxx = (float)(nvg__minf((float)(ax + aw), (float)(bx + bw)));
			float maxy = (float)(nvg__minf((float)(ay + ah), (float)(by + bh)));
			dst[0] = (float)(minx);
			dst[1] = (float)(miny);
			dst[2] = (float)(nvg__maxf((float)(0.0f), (float)(maxx - minx)));
			dst[3] = (float)(nvg__maxf((float)(0.0f), (float)(maxy - miny)));
		}

		public static void nvgIntersectScissor(NVGcontext ctx, float x, float y, float w, float h)
		{
			NVGstate* state = nvg__getState(ctx);
			float* pxform = stackalloc float[6]; float* invxorm = stackalloc float[6];
			float* rect = stackalloc float[4];
			float ex = 0; float ey = 0; float tex = 0; float tey = 0;
			if ((state->scissor.extent[0]) < (0)) {
				nvgScissor(ctx, (float)(x), (float)(y), (float)(w), (float)(h)); return; }

			CRuntime.memcpy(pxform, state->scissor.xform, (ulong)(sizeof(float) * 6));
			ex = (float)(state->scissor.extent[0]);
			ey = (float)(state->scissor.extent[1]);
			nvgTransformInverse(invxorm, state->xform);
			nvgTransformMultiply(pxform, invxorm);
			tex = (float)(ex * nvg__absf((float)(pxform[0])) + ey * nvg__absf((float)(pxform[2])));
			tey = (float)(ex * nvg__absf((float)(pxform[1])) + ey * nvg__absf((float)(pxform[3])));
			nvg__isectRects(rect, (float)(pxform[4] - tex), (float)(pxform[5] - tey), (float)(tex * 2), (float)(tey * 2), (float)(x), (float)(y), (float)(w), (float)(h));
			nvgScissor(ctx, (float)(rect[0]), (float)(rect[1]), (float)(rect[2]), (float)(rect[3]));
		}

		public static void nvgResetScissor(NVGcontext ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			CRuntime.memset(state->scissor.xform, (int)(0), (ulong)(sizeof((state->scissor.xform))));
			state->scissor.extent[0] = (float)(-1.0f);
			state->scissor.extent[1] = (float)(-1.0f);
		}

		public static void nvgGlobalCompositeOperation(NVGcontext ctx, int op)
		{
			NVGstate* state = nvg__getState(ctx);
			state->compositeOperation = (NVGcompositeOperationState)(nvg__compositeOperationState((int)(op)));
		}

		public static void nvgGlobalCompositeBlendFunc(NVGcontext ctx, int sfactor, int dfactor)
		{
			nvgGlobalCompositeBlendFuncSeparate(ctx, (int)(sfactor), (int)(dfactor), (int)(sfactor), (int)(dfactor));
		}

		public static void nvgGlobalCompositeBlendFuncSeparate(NVGcontext ctx, int srcRGB, int dstRGB, int srcAlpha, int dstAlpha)
		{
			NVGcompositeOperationState op = new NVGcompositeOperationState();
			op.srcRGB = (int)(srcRGB);
			op.dstRGB = (int)(dstRGB);
			op.srcAlpha = (int)(srcAlpha);
			op.dstAlpha = (int)(dstAlpha);
			NVGstate* state = nvg__getState(ctx);
			state->compositeOperation = (NVGcompositeOperationState)(op);
		}

		public static int nvg__ptEquals(float x1, float y1, float x2, float y2, float tol)
		{
			float dx = (float)(x2 - x1);
			float dy = (float)(y2 - y1);
			return (int)((dx * dx + dy * dy) < (tol * tol) ? 1 : 0);
		}

		public static float nvg__distPtSeg(float x, float y, float px, float py, float qx, float qy)
		{
			float pqx = 0; float pqy = 0; float dx = 0; float dy = 0; float d = 0; float t = 0;
			pqx = (float)(qx - px);
			pqy = (float)(qy - py);
			dx = (float)(x - px);
			dy = (float)(y - py);
			d = (float)(pqx * pqx + pqy * pqy);
			t = (float)(pqx * dx + pqy * dy);
			if ((d) > (0)) t /= (float)(d);
			if ((t) < (0)) t = (float)(0); else if ((t) > (1)) t = (float)(1);
			dx = (float)(px + t * pqx - x);
			dy = (float)(py + t * pqy - y);
			return (float)(dx * dx + dy * dy);
		}

		public static void nvg__appendCommands(NVGcontext ctx, float* vals, int nvals)
		{
			NVGstate* state = nvg__getState(ctx);
			int i = 0;
			if ((ctx.ncommands + nvals) > (ctx.ccommands))
			{
				float* commands;
				int ccommands = (int)(ctx.ncommands + nvals + ctx.ccommands / 2);
				commands = (float*)(CRuntime.realloc(ctx.commands, (ulong)(sizeof(float) * ccommands)));
				if ((commands) == null)
					return;
				ctx.commands = commands;
				ctx.ccommands = (int)(ccommands);
			}

			if (((int)(vals[0]) != NVG_CLOSE) && ((int)(vals[0]) != NVG_WINDING)) {
				ctx.commandx = (float)(vals[nvals - 2]); ctx.commandy = (float)(vals[nvals - 1]); }

			i = (int)(0);
			while ((i) < (nvals)) {
				int cmd = (int)(vals[i]); switch (cmd) {
					case NVG_MOVETO: nvgTransformPoint(&vals[i + 1], &vals[i + 2], state->xform, (float)(vals[i + 1]), (float)(vals[i + 2])); i += (int)(3); break; case NVG_LINETO: nvgTransformPoint(&vals[i + 1], &vals[i + 2], state->xform, (float)(vals[i + 1]), (float)(vals[i + 2])); i += (int)(3); break; case NVG_BEZIERTO: nvgTransformPoint(&vals[i + 1], &vals[i + 2], state->xform, (float)(vals[i + 1]), (float)(vals[i + 2])); nvgTransformPoint(&vals[i + 3], &vals[i + 4], state->xform, (float)(vals[i + 3]), (float)(vals[i + 4])); nvgTransformPoint(&vals[i + 5], &vals[i + 6], state->xform, (float)(vals[i + 5]), (float)(vals[i + 6])); i += (int)(7); break; case NVG_CLOSE: i++; break; case NVG_WINDING: i += (int)(2); break; default: i++; }
			}
			CRuntime.memcpy(&ctx.commands[ctx.ncommands], vals, (ulong)(nvals * sizeof(float)));
			ctx.ncommands += (int)(nvals);
		}

		public static void nvg__clearPathCache(NVGcontext ctx)
		{
			ctx.cache.npoints = (int)(0);
			ctx.cache.npaths = (int)(0);
		}

		public static NVGpath* nvg__lastPath(NVGcontext ctx)
		{
			if ((ctx.cache.npaths) > (0)) return &ctx.cache.paths[ctx.cache.npaths - 1];
			return null;
		}

		public static void nvg__addPath(NVGcontext ctx)
		{
			NVGpath* path;
			if ((ctx.cache.npaths + 1) > (ctx.cache.cpaths)) {
				NVGpath* paths; int cpaths = (int)(ctx.cache.npaths + 1 + ctx.cache.cpaths / 2); paths = (NVGpath*)(CRuntime.realloc(ctx.cache.paths, (ulong)(sizeof(NVGpath) * cpaths))); if ((paths) == null) return; ctx.cache.paths = paths; ctx.cache.cpaths = (int)(cpaths); }

			path = &ctx.cache.paths[ctx.cache.npaths];
			CRuntime.memset(path, (int)(0), (ulong)(sizeof(NVGpath)));
			path->first = (int)(ctx.cache.npoints);
			path->winding = (int)(NVG_CCW);
			ctx.cache.npaths++;
		}

		public static NVGpoint* nvg__lastPoint(NVGcontext ctx)
		{
			if ((ctx.cache.npoints) > (0)) return &ctx.cache.points[ctx.cache.npoints - 1];
			return null;
		}

		public static void nvg__addPoint(NVGcontext ctx, float x, float y, int flags)
		{
			NVGpath* path = nvg__lastPath(ctx);
			NVGpoint* pt;
			if ((path) == null) return;
			if (((path->count) > (0)) && ((ctx.cache.npoints) > (0))) {
				pt = nvg__lastPoint(ctx); if ((nvg__ptEquals((float)(pt->x), (float)(pt->y), (float)(x), (float)(y), (float)(ctx.distTol))) != 0) {
					pt->flags |= (byte)(flags); return; }
			}

			if ((ctx.cache.npoints + 1) > (ctx.cache.cpoints)) {
				NVGpoint* points; int cpoints = (int)(ctx.cache.npoints + 1 + ctx.cache.cpoints / 2); points = (NVGpoint*)(CRuntime.realloc(ctx.cache.points, (ulong)(sizeof(NVGpoint) * cpoints))); if ((points) == null) return; ctx.cache.points = points; ctx.cache.cpoints = (int)(cpoints); }

			pt = &ctx.cache.points[ctx.cache.npoints];
			CRuntime.memset(pt, (int)(0), (ulong)(sizeof(NVGpoint)));
			pt->x = (float)(x);
			pt->y = (float)(y);
			pt->flags = ((byte)(flags));
			ctx.cache.npoints++;
			path->count++;
		}

		public static void nvg__closePath(NVGcontext ctx)
		{
			NVGpath* path = nvg__lastPath(ctx);
			if ((path) == null) return;
			path->closed = (byte)(1);
		}

		public static void nvg__pathWinding(NVGcontext ctx, int winding)
		{
			NVGpath* path = nvg__lastPath(ctx);
			if ((path) == null) return;
			path->winding = (int)(winding);
		}

		public static float nvg__getAverageScale(float* t)
		{
			float sx = (float)(sqrtf((float)(t[0] * t[0] + t[2] * t[2])));
			float sy = (float)(sqrtf((float)(t[1] * t[1] + t[3] * t[3])));
			return (float)((sx + sy) * 0.5f);
		}

		public static NVGvertex* nvg__allocTempVerts(NVGcontext ctx, int nverts)
		{
			if ((nverts) > (ctx.cache.cverts)) {
				NVGvertex* verts; int cverts = (int)((nverts + 0xff) & ~0xff); verts = (NVGvertex*)(CRuntime.realloc(ctx.cache.verts, (ulong)(sizeof(NVGvertex) * cverts))); if ((verts) == null) return null; ctx.cache.verts = verts; ctx.cache.cverts = (int)(cverts); }

			return ctx.cache.verts;
		}

		public static float nvg__triarea2(float ax, float ay, float bx, float by, float cx, float cy)
		{
			float abx = (float)(bx - ax);
			float aby = (float)(by - ay);
			float acx = (float)(cx - ax);
			float acy = (float)(cy - ay);
			return (float)(acx * aby - abx * acy);
		}

		public static float nvg__polyArea(NVGpoint* pts, int npts)
		{
			int i = 0;
			float area = (float)(0);
			for (i = (int)(2); (i) < (npts); i++) {
				NVGpoint* a = &pts[0]; NVGpoint* b = &pts[i - 1]; NVGpoint* c = &pts[i]; area += (float)(nvg__triarea2((float)(a->x), (float)(a->y), (float)(b->x), (float)(b->y), (float)(c->x), (float)(c->y))); }
			return (float)(area * 0.5f);
		}

		public static void nvg__polyReverse(NVGpoint* pts, int npts)
		{
			NVGpoint tmp = new NVGpoint();
			int i = (int)(0); int j = (int)(npts - 1);
			while ((i) < (j)) {
				tmp = (NVGpoint)(pts[i]); pts[i] = (NVGpoint)(pts[j]); pts[j] = (NVGpoint)(tmp); i++; j--; }
		}

		public static void nvg__vset(NVGvertex* vtx, float x, float y, float u, float v)
		{
			vtx->x = (float)(x);
			vtx->y = (float)(y);
			vtx->u = (float)(u);
			vtx->v = (float)(v);
		}

		public static void nvg__tesselateBezier(NVGcontext ctx, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, int level, int type)
		{
			float x12 = 0; float y12 = 0; float x23 = 0; float y23 = 0; float x34 = 0; float y34 = 0; float x123 = 0; float y123 = 0; float x234 = 0; float y234 = 0; float x1234 = 0; float y1234 = 0;
			float dx = 0; float dy = 0; float d2 = 0; float d3 = 0;
			if ((level) > (10)) return;
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
			d2 = (float)(nvg__absf((float)((x2 - x4) * dy - (y2 - y4) * dx)));
			d3 = (float)(nvg__absf((float)((x3 - x4) * dy - (y3 - y4) * dx)));
			if (((d2 + d3) * (d2 + d3)) < (ctx.tessTol * (dx * dx + dy * dy))) {
				nvg__addPoint(ctx, (float)(x4), (float)(y4), (int)(type)); return; }

			x234 = (float)((x23 + x34) * 0.5f);
			y234 = (float)((y23 + y34) * 0.5f);
			x1234 = (float)((x123 + x234) * 0.5f);
			y1234 = (float)((y123 + y234) * 0.5f);
			nvg__tesselateBezier(ctx, (float)(x1), (float)(y1), (float)(x12), (float)(y12), (float)(x123), (float)(y123), (float)(x1234), (float)(y1234), (int)(level + 1), (int)(0));
			nvg__tesselateBezier(ctx, (float)(x1234), (float)(y1234), (float)(x234), (float)(y234), (float)(x34), (float)(y34), (float)(x4), (float)(y4), (int)(level + 1), (int)(type));
		}

		public static void nvg__flattenPaths(NVGcontext ctx)
		{
			NVGpathCache cache = ctx.cache;
			NVGpoint* last;
			NVGpoint* p0;
			NVGpoint* p1;
			NVGpoint* pts;
			NVGpath* path;
			int i = 0; int j = 0;
			float* cp1;
			float* cp2;
			float* p;
			float area = 0;
			if ((cache.npaths) > (0)) return;
			i = (int)(0);
			while ((i) < (ctx.ncommands)) {
				int cmd = (int)(ctx.commands[i]); switch (cmd) {
					case NVG_MOVETO: nvg__addPath(ctx); p = &ctx.commands[i + 1]; nvg__addPoint(ctx, (float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER)); i += (int)(3); break; case NVG_LINETO: p = &ctx.commands[i + 1]; nvg__addPoint(ctx, (float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER)); i += (int)(3); break; case NVG_BEZIERTO: last = nvg__lastPoint(ctx); if (last != null) {
							cp1 = &ctx.commands[i + 1]; cp2 = &ctx.commands[i + 3]; p = &ctx.commands[i + 5]; nvg__tesselateBezier(ctx, (float)(last->x), (float)(last->y), (float)(cp1[0]), (float)(cp1[1]), (float)(cp2[0]), (float)(cp2[1]), (float)(p[0]), (float)(p[1]), (int)(0), (int)(NVG_PT_CORNER)); }
						i += (int)(7); break; case NVG_CLOSE: nvg__closePath(ctx); i++; break; case NVG_WINDING: nvg__pathWinding(ctx, (int)(ctx.commands[i + 1])); i += (int)(2); break; default: i++; }
			}
			cache.bounds[0] = (float)(cache.bounds[1] = (float)(1e6f));
			cache.bounds[2] = (float)(cache.bounds[3] = (float)(-1e6f));
			for (j = (int)(0); (j) < (cache.npaths); j++) {
				path = &cache.paths[j]; pts = &cache.points[path->first]; p0 = &pts[path->count - 1]; p1 = &pts[0]; if ((nvg__ptEquals((float)(p0->x), (float)(p0->y), (float)(p1->x), (float)(p1->y), (float)(ctx.distTol))) != 0) {
					path->count--; p0 = &pts[path->count - 1]; path->closed = (byte)(1); }
				if ((path->count) > (2)) {
					area = (float)(nvg__polyArea(pts, (int)(path->count))); if (((path->winding) == (NVG_CCW)) && ((area) < (0.0f))) nvg__polyReverse(pts, (int)(path->count)); if (((path->winding) == (NVG_CW)) && ((area) > (0.0f))) nvg__polyReverse(pts, (int)(path->count)); }
				for (i = (int)(0); (i) < (path->count); i++) {
					p0->dx = (float)(p1->x - p0->x); p0->dy = (float)(p1->y - p0->y); p0->len = (float)(nvg__normalize(&p0->dx, &p0->dy)); cache.bounds[0] = (float)(nvg__minf((float)(cache.bounds[0]), (float)(p0->x))); cache.bounds[1] = (float)(nvg__minf((float)(cache.bounds[1]), (float)(p0->y))); cache.bounds[2] = (float)(nvg__maxf((float)(cache.bounds[2]), (float)(p0->x))); cache.bounds[3] = (float)(nvg__maxf((float)(cache.bounds[3]), (float)(p0->y))); p0 = p1++; } }
		}

		public static int nvg__curveDivs(float r, float arc, float tol)
		{
			float da = (float)(acosf((float)(r / (r + tol))) * 2.0f);
			return (int)(nvg__maxi((int)(2), (int)(ceilf((float)(arc / da)))));
		}

		public static void nvg__chooseBevel(int bevel, NVGpoint* p0, NVGpoint* p1, float w, float* x0, float* y0, float* x1, float* y1)
		{
			if ((bevel) != 0) {
				*x0 = (float)(p1->x + p0->dy * w); *y0 = (float)(p1->y - p0->dx * w); *x1 = (float)(p1->x + p1->dy * w); *y1 = (float)(p1->y - p1->dx * w); }
			else {
				*x0 = (float)(p1->x + p1->dmx * w); *y0 = (float)(p1->y + p1->dmy * w); *x1 = (float)(p1->x + p1->dmx * w); *y1 = (float)(p1->y + p1->dmy * w); }

		}

		public static NVGvertex* nvg__roundJoin(NVGvertex* dst, NVGpoint* p0, NVGpoint* p1, float lw, float rw, float lu, float ru, int ncap, float fringe)
		{
			int i = 0; int n = 0;
			float dlx0 = (float)(p0->dy);
			float dly0 = (float)(-p0->dx);
			float dlx1 = (float)(p1->dy);
			float dly1 = (float)(-p1->dx);
			if ((p1->flags & NVG_PT_LEFT) != 0) {
				float lx0 = 0; float ly0 = 0; float lx1 = 0; float ly1 = 0; float a0 = 0; float a1 = 0; nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1); a0 = (float)(atan2f((float)(-dly0), (float)(-dlx0))); a1 = (float)(atan2f((float)(-dly1), (float)(-dlx1))); if ((a1) > (a0)) a1 -= (float)(3.14159274 * 2); nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1)); dst++; n = (int)(nvg__clampi((int)(ceilf((float)(((a0 - a1) / 3.14159274) * ncap))), (int)(2), (int)(ncap))); for (i = (int)(0); (i) < (n); i++) {
					float u = (float)(i / (float)(n - 1)); float a = (float)(a0 + u * (a1 - a0)); float rx = (float)(p1->x + cosf((float)(a)) * rw); float ry = (float)(p1->y + sinf((float)(a)) * rw); nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1)); dst++; nvg__vset(dst, (float)(rx), (float)(ry), (float)(ru), (float)(1)); dst++; } nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1)); dst++; }
			else {
				float rx0 = 0; float ry0 = 0; float rx1 = 0; float ry1 = 0; float a0 = 0; float a1 = 0; nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1); a0 = (float)(atan2f((float)(dly0), (float)(dlx0))); a1 = (float)(atan2f((float)(dly1), (float)(dlx1))); if ((a1) < (a0)) a1 += (float)(3.14159274 * 2); nvg__vset(dst, (float)(p1->x + dlx0 * rw), (float)(p1->y + dly0 * rw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1)); dst++; n = (int)(nvg__clampi((int)(ceilf((float)(((a1 - a0) / 3.14159274) * ncap))), (int)(2), (int)(ncap))); for (i = (int)(0); (i) < (n); i++) {
					float u = (float)(i / (float)(n - 1)); float a = (float)(a0 + u * (a1 - a0)); float lx = (float)(p1->x + cosf((float)(a)) * lw); float ly = (float)(p1->y + sinf((float)(a)) * lw); nvg__vset(dst, (float)(lx), (float)(ly), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1)); dst++; } nvg__vset(dst, (float)(p1->x + dlx1 * rw), (float)(p1->y + dly1 * rw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1)); dst++; }

			return dst;
		}

		public static NVGvertex* nvg__bevelJoin(NVGvertex* dst, NVGpoint* p0, NVGpoint* p1, float lw, float rw, float lu, float ru, float fringe)
		{
			float rx0 = 0; float ry0 = 0; float rx1 = 0; float ry1 = 0;
			float lx0 = 0; float ly0 = 0; float lx1 = 0; float ly1 = 0;
			float dlx0 = (float)(p0->dy);
			float dly0 = (float)(-p0->dx);
			float dlx1 = (float)(p1->dy);
			float dly1 = (float)(-p1->dx);
			if ((p1->flags & NVG_PT_LEFT) != 0) {
				nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1); nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1)); dst++; if ((p1->flags & NVG_PT_BEVEL) != 0) {
					nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1)); dst++; nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1)); dst++; }
				else {
					rx0 = (float)(p1->x - p1->dmx * rw); ry0 = (float)(p1->y - p1->dmy * rw); nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1)); dst++; nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1)); dst++; nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1)); dst++; }
				nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1)); dst++; }
			else {
				nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1); nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1)); dst++; if ((p1->flags & NVG_PT_BEVEL) != 0) {
					nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1)); dst++; }
				else {
					lx0 = (float)(p1->x + p1->dmx * lw); ly0 = (float)(p1->y + p1->dmy * lw); nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1)); dst++; nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1)); dst++; }
				nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1)); dst++; }

			return dst;
		}

		public static NVGvertex* nvg__buttCapStart(NVGvertex* dst, NVGpoint* p, float dx, float dy, float w, float d, float aa, float u0, float u1)
		{
			float px = (float)(p->x - dx * d);
			float py = (float)(p->y - dy * d);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			nvg__vset(dst, (float)(px + dlx * w - dx * aa), (float)(py + dly * w - dy * aa), (float)(u0), (float)(0));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w - dx * aa), (float)(py - dly * w - dy * aa), (float)(u1), (float)(0));
			dst++;
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			return dst;
		}

		public static NVGvertex* nvg__buttCapEnd(NVGvertex* dst, NVGpoint* p, float dx, float dy, float w, float d, float aa, float u0, float u1)
		{
			float px = (float)(p->x + dx * d);
			float py = (float)(p->y + dy * d);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px + dlx * w + dx * aa), (float)(py + dly * w + dy * aa), (float)(u0), (float)(0));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w + dx * aa), (float)(py - dly * w + dy * aa), (float)(u1), (float)(0));
			dst++;
			return dst;
		}

		public static NVGvertex* nvg__roundCapStart(NVGvertex* dst, NVGpoint* p, float dx, float dy, float w, int ncap, float aa, float u0, float u1)
		{
			int i = 0;
			float px = (float)(p->x);
			float py = (float)(p->y);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			for (i = (int)(0); (i) < (ncap); i++) {
				float a = (float)(i / (float)(ncap - 1) * 3.14159274); float ax = (float)(cosf((float)(a)) * w); float ay = (float)(sinf((float)(a)) * w); nvg__vset(dst, (float)(px - dlx * ax - dx * ay), (float)(py - dly * ax - dy * ay), (float)(u0), (float)(1)); dst++; nvg__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1)); dst++; }
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			return dst;
		}

		public static NVGvertex* nvg__roundCapEnd(NVGvertex* dst, NVGpoint* p, float dx, float dy, float w, int ncap, float aa, float u0, float u1)
		{
			int i = 0;
			float px = (float)(p->x);
			float py = (float)(p->y);
			float dlx = (float)(dy);
			float dly = (float)(-dx);
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			for (i = (int)(0); (i) < (ncap); i++) {
				float a = (float)(i / (float)(ncap - 1) * 3.14159274); float ax = (float)(cosf((float)(a)) * w); float ay = (float)(sinf((float)(a)) * w); nvg__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1)); dst++; nvg__vset(dst, (float)(px - dlx * ax + dx * ay), (float)(py - dly * ax + dy * ay), (float)(u0), (float)(1)); dst++; }
			return dst;
		}

		public static void nvg__calculateJoins(NVGcontext ctx, float w, int lineJoin, float miterLimit)
		{
			NVGpathCache cache = ctx.cache;
			int i = 0; int j = 0;
			float iw = (float)(0.0f);
			if ((w) > (0.0f)) iw = (float)(1.0f / w);
			for (i = (int)(0); (i) < (cache.npaths); i++) {
				NVGpath* path = &cache.paths[i]; NVGpoint* pts = &cache.points[path->first]; NVGpoint* p0 = &pts[path->count - 1]; NVGpoint* p1 = &pts[0]; int nleft = (int)(0); path->nbevel = (int)(0); for (j = (int)(0); (j) < (path->count); j++) {
					float dlx0 = 0; float dly0 = 0; float dlx1 = 0; float dly1 = 0; float dmr2 = 0; float cross = 0; float limit = 0; dlx0 = (float)(p0->dy); dly0 = (float)(-p0->dx); dlx1 = (float)(p1->dy); dly1 = (float)(-p1->dx); p1->dmx = (float)((dlx0 + dlx1) * 0.5f); p1->dmy = (float)((dly0 + dly1) * 0.5f); dmr2 = (float)(p1->dmx * p1->dmx + p1->dmy * p1->dmy); if ((dmr2) > (0.000001f)) {
						float scale = (float)(1.0f / dmr2); if ((scale) > (600.0f)) {
							scale = (float)(600.0f); }
						p1->dmx *= (float)(scale); p1->dmy *= (float)(scale); }
					p1->flags = (byte)((p1->flags & NVG_PT_CORNER) ? NVG_PT_CORNER : 0); cross = (float)(p1->dx * p0->dy - p0->dx * p1->dy); if ((cross) > (0.0f)) {
						nleft++; p1->flags |= (byte)(NVG_PT_LEFT); }
					limit = (float)(nvg__maxf((float)(1.01f), (float)(nvg__minf((float)(p0->len), (float)(p1->len)) * iw))); if ((dmr2 * limit * limit) < (1.0f)) p1->flags |= (byte)(NVG_PR_INNERBEVEL); if ((p1->flags & NVG_PT_CORNER) != 0) {
						if ((((dmr2 * miterLimit * miterLimit) < (1.0f)) || ((lineJoin) == (NVG_BEVEL))) || ((lineJoin) == (NVG_ROUND))) {
							p1->flags |= (byte)(NVG_PT_BEVEL); }
					}
					if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0) path->nbevel++; p0 = p1++; } path->convex = (int)(((nleft) == (path->count)) ? 1 : 0); }
		}

		public static int nvg__expandStroke(NVGcontext ctx, float w, float fringe, int lineCap, int lineJoin, float miterLimit)
		{
			NVGpathCache cache = ctx.cache;
			NVGvertex* verts;
			NVGvertex* dst;
			int cverts = 0; int i = 0; int j = 0;
			float aa = (float)(fringe);
			float u0 = (float)(0.0f); float u1 = (float)(1.0f);
			int ncap = (int)(nvg__curveDivs((float)(w), (float)(3.14159274), (float)(ctx.tessTol)));
			w += (float)(aa * 0.5f);
			if ((aa) == (0.0f)) {
				u0 = (float)(0.5f); u1 = (float)(0.5f); }

			nvg__calculateJoins(ctx, (float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (cache.npaths); i++) {
				NVGpath* path = &cache.paths[i]; int loop = (int)(((path->closed) == (0)) ? 0 : 1); if ((lineJoin) == (NVG_ROUND)) cverts += (int)((path->count + path->nbevel * (ncap + 2) + 1) * 2); else cverts += (int)((path->count + path->nbevel * 5 + 1) * 2); if ((loop) == (0)) {
					if ((lineCap) == (NVG_ROUND)) {
						cverts += (int)((ncap * 2 + 2) * 2); }
					else {
						cverts += (int)((3 + 3) * 2); }
				}
			}
			verts = nvg__allocTempVerts(ctx, (int)(cverts));
			if ((verts) == null) return (int)(0);
			for (i = (int)(0); (i) < (cache.npaths); i++) {
				NVGpath* path = &cache.paths[i]; NVGpoint* pts = &cache.points[path->first]; NVGpoint* p0; NVGpoint* p1; int s = 0; int e = 0; int loop = 0; float dx = 0; float dy = 0; path->fill = null; path->nfill = (int)(0); loop = (int)(((path->closed) == (0)) ? 0 : 1); dst = verts; path->stroke = dst; if ((loop) != 0) {
					p0 = &pts[path->count - 1]; p1 = &pts[0]; s = (int)(0); e = (int)(path->count); }
				else {
					p0 = &pts[0]; p1 = &pts[1]; s = (int)(1); e = (int)(path->count - 1); }
				if ((loop) == (0)) {
					dx = (float)(p1->x - p0->x); dy = (float)(p1->y - p0->y); nvg__normalize(&dx, &dy); if ((lineCap) == (NVG_BUTT)) dst = nvg__buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1)); else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE))) dst = nvg__buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1)); else if ((lineCap) == (NVG_ROUND)) dst = nvg__roundCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1)); }
				for (j = (int)(s); (j) < (e); ++j) {
					if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0) {
						if ((lineJoin) == (NVG_ROUND)) {
							dst = nvg__roundJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (int)(ncap), (float)(aa)); }
						else {
							dst = nvg__bevelJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (float)(aa)); }
					}
					else {
						nvg__vset(dst, (float)(p1->x + (p1->dmx * w)), (float)(p1->y + (p1->dmy * w)), (float)(u0), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - (p1->dmx * w)), (float)(p1->y - (p1->dmy * w)), (float)(u1), (float)(1)); dst++; }
					p0 = p1++; } if ((loop) != 0) {
					nvg__vset(dst, (float)(verts[0].x), (float)(verts[0].y), (float)(u0), (float)(1)); dst++; nvg__vset(dst, (float)(verts[1].x), (float)(verts[1].y), (float)(u1), (float)(1)); dst++; }
				else {
					dx = (float)(p1->x - p0->x); dy = (float)(p1->y - p0->y); nvg__normalize(&dx, &dy); if ((lineCap) == (NVG_BUTT)) dst = nvg__buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1)); else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE))) dst = nvg__buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1)); else if ((lineCap) == (NVG_ROUND)) dst = nvg__roundCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1)); }
				path->nstroke = ((int)(dst - verts)); verts = dst; }
			return (int)(1);
		}

		public static int nvg__expandFill(NVGcontext ctx, float w, int lineJoin, float miterLimit)
		{
			NVGpathCache cache = ctx.cache;
			NVGvertex* verts;
			NVGvertex* dst;
			int cverts = 0; int convex = 0; int i = 0; int j = 0;
			float aa = (float)(ctx.fringeWidth);
			int fringe = (int)((w) > (0.0f) ? 1 : 0);
			nvg__calculateJoins(ctx, (float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (cache.npaths); i++) {
				NVGpath* path = &cache.paths[i]; cverts += (int)(path->count + path->nbevel + 1); if ((fringe) != 0) cverts += (int)((path->count + path->nbevel * 5 + 1) * 2); }
			verts = nvg__allocTempVerts(ctx, (int)(cverts));
			if ((verts) == null) return (int)(0);
			convex = (int)(((cache.npaths) == (1)) && ((cache.paths[0].convex) != 0) ? 1 : 0);
			for (i = (int)(0); (i) < (cache.npaths); i++) {
				NVGpath* path = &cache.paths[i]; NVGpoint* pts = &cache.points[path->first]; NVGpoint* p0; NVGpoint* p1; float rw = 0; float lw = 0; float woff = 0; float ru = 0; float lu = 0; woff = (float)(0.5f * aa); dst = verts; path->fill = dst; if ((fringe) != 0) {
					p0 = &pts[path->count - 1]; p1 = &pts[0]; for (j = (int)(0); (j) < (path->count); ++j) {
						if ((p1->flags & NVG_PT_BEVEL) != 0) {
							float dlx0 = (float)(p0->dy); float dly0 = (float)(-p0->dx); float dlx1 = (float)(p1->dy); float dly1 = (float)(-p1->dx); if ((p1->flags & NVG_PT_LEFT) != 0) {
								float lx = (float)(p1->x + p1->dmx * woff); float ly = (float)(p1->y + p1->dmy * woff); nvg__vset(dst, (float)(lx), (float)(ly), (float)(0.5f), (float)(1)); dst++; }
							else {
								float lx0 = (float)(p1->x + dlx0 * woff); float ly0 = (float)(p1->y + dly0 * woff); float lx1 = (float)(p1->x + dlx1 * woff); float ly1 = (float)(p1->y + dly1 * woff); nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(0.5f), (float)(1)); dst++; nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(0.5f), (float)(1)); dst++; }
						}
						else {
							nvg__vset(dst, (float)(p1->x + (p1->dmx * woff)), (float)(p1->y + (p1->dmy * woff)), (float)(0.5f), (float)(1)); dst++; }
						p0 = p1++; } }
				else {
					for (j = (int)(0); (j) < (path->count); ++j) {
						nvg__vset(dst, (float)(pts[j].x), (float)(pts[j].y), (float)(0.5f), (float)(1)); dst++; } }
				path->nfill = ((int)(dst - verts)); verts = dst; if ((fringe) != 0) {
					lw = (float)(w + woff); rw = (float)(w - woff); lu = (float)(0); ru = (float)(1); dst = verts; path->stroke = dst; if ((convex) != 0) {
						lw = (float)(woff); lu = (float)(0.5f); }
					p0 = &pts[path->count - 1]; p1 = &pts[0]; for (j = (int)(0); (j) < (path->count); ++j) {
						if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0) {
							dst = nvg__bevelJoin(dst, p0, p1, (float)(lw), (float)(rw), (float)(lu), (float)(ru), (float)(ctx.fringeWidth)); }
						else {
							nvg__vset(dst, (float)(p1->x + (p1->dmx * lw)), (float)(p1->y + (p1->dmy * lw)), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(p1->x - (p1->dmx * rw)), (float)(p1->y - (p1->dmy * rw)), (float)(ru), (float)(1)); dst++; }
						p0 = p1++; } nvg__vset(dst, (float)(verts[0].x), (float)(verts[0].y), (float)(lu), (float)(1)); dst++; nvg__vset(dst, (float)(verts[1].x), (float)(verts[1].y), (float)(ru), (float)(1)); dst++; path->nstroke = ((int)(dst - verts)); verts = dst; }
				else {
					path->stroke = null; path->nstroke = (int)(0); }
			}
			return (int)(1);
		}

		public static void nvgBeginPath(NVGcontext ctx)
		{
			ctx.ncommands = (int)(0);
			nvg__clearPathCache(ctx);
		}

		public static void nvgMoveTo(NVGcontext ctx, float x, float y)
		{
			float* vals = stackalloc float[3];
			vals[0] = (float)(NVG_MOVETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);

			nvg__appendCommands(ctx, vals, 3);
		}

		public static void nvgLineTo(NVGcontext ctx, float x, float y)
		{
			float* vals = stackalloc float[3];
			vals[0] = (float)(NVG_LINETO);
			vals[1] = (float)(x);
			vals[2] = (float)(y);

			nvg__appendCommands(ctx, vals, 3);
		}

		public static void nvgBezierTo(NVGcontext ctx, float c1x, float c1y, float c2x, float c2y, float x, float y)
		{
			float* vals = stackalloc float[7];
			vals[0] = (float)(NVG_BEZIERTO);
			vals[1] = (float)(c1x);
			vals[2] = (float)(c1y);
			vals[3] = (float)(c2x);
			vals[4] = (float)(c2y);
			vals[5] = (float)(x);
			vals[6] = (float)(y);

			nvg__appendCommands(ctx, vals, 7);
		}

		public static void nvgQuadTo(NVGcontext ctx, float cx, float cy, float x, float y)
		{
			float x0 = (float)(ctx.commandx);
			float y0 = (float)(ctx.commandy);
			float* vals = stackalloc float[7];
			vals[0] = (float)(NVG_BEZIERTO);
			vals[1] = (float)(x0 + 2.0f / 3.0f * (cx - x0));
			vals[2] = (float)(y0 + 2.0f / 3.0f * (cy - y0));
			vals[3] = (float)(x + 2.0f / 3.0f * (cx - x));
			vals[4] = (float)(y + 2.0f / 3.0f * (cy - y));
			vals[5] = (float)(x);
			vals[6] = (float)(y);

			nvg__appendCommands(ctx, vals, 7);
		}

		public static void nvgArcTo(NVGcontext ctx, float x1, float y1, float x2, float y2, float radius)
		{
			float x0 = (float)(ctx.commandx);
			float y0 = (float)(ctx.commandy);
			float dx0 = 0; float dy0 = 0; float dx1 = 0; float dy1 = 0; float a = 0; float d = 0; float cx = 0; float cy = 0; float a0 = 0; float a1 = 0;
			int dir = 0;
			if ((ctx.ncommands) == (0)) {
				return; }

			if (((((nvg__ptEquals((float)(x0), (float)(y0), (float)(x1), (float)(y1), (float)(ctx.distTol))) != 0) || ((nvg__ptEquals((float)(x1), (float)(y1), (float)(x2), (float)(y2), (float)(ctx.distTol))) != 0)) || ((nvg__distPtSeg((float)(x1), (float)(y1), (float)(x0), (float)(y0), (float)(x2), (float)(y2))) < (ctx.distTol * ctx.distTol))) || ((radius) < (ctx.distTol))) {
				nvgLineTo(ctx, (float)(x1), (float)(y1)); return; }

			dx0 = (float)(x0 - x1);
			dy0 = (float)(y0 - y1);
			dx1 = (float)(x2 - x1);
			dy1 = (float)(y2 - y1);
			nvg__normalize(&dx0, &dy0);
			nvg__normalize(&dx1, &dy1);
			a = (float)(nvg__acosf((float)(dx0 * dx1 + dy0 * dy1)));
			d = (float)(radius / nvg__tanf((float)(a / 2.0f)));
			if ((d) > (10000.0f)) {
				nvgLineTo(ctx, (float)(x1), (float)(y1)); return; }

			if ((nvg__cross((float)(dx0), (float)(dy0), (float)(dx1), (float)(dy1))) > (0.0f)) {
				cx = (float)(x1 + dx0 * d + dy0 * radius); cy = (float)(y1 + dy0 * d + -dx0 * radius); a0 = (float)(nvg__atan2f((float)(dx0), (float)(-dy0))); a1 = (float)(nvg__atan2f((float)(-dx1), (float)(dy1))); dir = (int)(NVG_CW); }
			else {
				cx = (float)(x1 + dx0 * d + -dy0 * radius); cy = (float)(y1 + dy0 * d + dx0 * radius); a0 = (float)(nvg__atan2f((float)(-dx0), (float)(dy0))); a1 = (float)(nvg__atan2f((float)(dx1), (float)(-dy1))); dir = (int)(NVG_CCW); }

			nvgArc(ctx, (float)(cx), (float)(cy), (float)(radius), (float)(a0), (float)(a1), (int)(dir));
		}

		public static void nvgClosePath(NVGcontext ctx)
		{
			float* vals = stackalloc float[1];
			vals[0] = (float)(NVG_CLOSE);

			nvg__appendCommands(ctx, vals, 1);
		}

		public static void nvgPathWinding(NVGcontext ctx, int dir)
		{
			float* vals = stackalloc float[2];
			vals[0] = (float)(NVG_WINDING);
			vals[1] = (float)(dir);

			nvg__appendCommands(ctx, vals, 2);
		}

		public static void nvgArc(NVGcontext ctx, float cx, float cy, float r, float a0, float a1, int dir)
		{
			float a = (float)(0); float da = (float)(0); float hda = (float)(0); float kappa = (float)(0);
			float dx = (float)(0); float dy = (float)(0); float x = (float)(0); float y = (float)(0); float tanx = (float)(0); float tany = (float)(0);
			float px = (float)(0); float py = (float)(0); float ptanx = (float)(0); float ptany = (float)(0);
			float* vals = stackalloc float[3 + 5 * 7 + 100];
			int i = 0; int ndivs = 0; int nvals = 0;
			int move = (int)((ctx.ncommands) > (0) ? NVG_LINETO : NVG_MOVETO);
			da = (float)(a1 - a0);
			if ((dir) == (NVG_CW)) {
				if ((nvg__absf((float)(da))) >= (3.14159274 * 2)) {
					da = (float)(3.14159274 * 2); }
				else {
					while ((da) < (0.0f)) { da += (float)(3.14159274 * 2); } }
			}
			else {
				if ((nvg__absf((float)(da))) >= (3.14159274 * 2)) {
					da = (float)(-3.14159274 * 2); }
				else {
					while ((da) > (0.0f)) { da -= (float)(3.14159274 * 2); } }
			}

			ndivs = (int)(nvg__maxi((int)(1), (int)(nvg__mini((int)(nvg__absf((float)(da)) / (3.14159274 * 0.5f) + 0.5f), (int)(5)))));
			hda = (float)((da / (float)(ndivs)) / 2.0f);
			kappa = (float)(nvg__absf((float)(4.0f / 3.0f * (1.0f - nvg__cosf((float)(hda))) / nvg__sinf((float)(hda)))));
			if ((dir) == (NVG_CCW)) kappa = (float)(-kappa);
			nvals = (int)(0);
			for (i = (int)(0); i <= ndivs; i++) {
				a = (float)(a0 + da * (i / (float)(ndivs))); dx = (float)(nvg__cosf((float)(a))); dy = (float)(nvg__sinf((float)(a))); x = (float)(cx + dx * r); y = (float)(cy + dy * r); tanx = (float)(-dy * r * kappa); tany = (float)(dx * r * kappa); if ((i) == (0)) {
					vals[nvals++] = ((float)(move)); vals[nvals++] = (float)(x); vals[nvals++] = (float)(y); }
				else {
					vals[nvals++] = (float)(NVG_BEZIERTO); vals[nvals++] = (float)(px + ptanx); vals[nvals++] = (float)(py + ptany); vals[nvals++] = (float)(x - tanx); vals[nvals++] = (float)(y - tany); vals[nvals++] = (float)(x); vals[nvals++] = (float)(y); }
				px = (float)(x); py = (float)(y); ptanx = (float)(tanx); ptany = (float)(tany); }
			nvg__appendCommands(ctx, vals, (int)(nvals));
		}

		public static void nvgRect(NVGcontext ctx, float x, float y, float w, float h)
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

			nvg__appendCommands(ctx, vals, 13);
		}

		public static void nvgRoundedRect(NVGcontext ctx, float x, float y, float w, float h, float r)
		{
			nvgRoundedRectVarying(ctx, (float)(x), (float)(y), (float)(w), (float)(h), (float)(r), (float)(r), (float)(r), (float)(r));
		}

		public static void nvgRoundedRectVarying(NVGcontext ctx, float x, float y, float w, float h, float radTopLeft, float radTopRight, float radBottomRight, float radBottomLeft)
		{
			if (((((radTopLeft) < (0.1f)) && ((radTopRight) < (0.1f))) && ((radBottomRight) < (0.1f))) && ((radBottomLeft) < (0.1f))) {
				nvgRect(ctx, (float)(x), (float)(y), (float)(w), (float)(h)); return; }
			else {
				float halfw = (float)(nvg__absf((float)(w)) * 0.5f); float halfh = (float)(nvg__absf((float)(h)) * 0.5f); float rxBL = (float)(nvg__minf((float)(radBottomLeft), (float)(halfw)) * nvg__signf((float)(w))); float ryBL = (float)(nvg__minf((float)(radBottomLeft), (float)(halfh)) * nvg__signf((float)(h))); float rxBR = (float)(nvg__minf((float)(radBottomRight), (float)(halfw)) * nvg__signf((float)(w))); float ryBR = (float)(nvg__minf((float)(radBottomRight), (float)(halfh)) * nvg__signf((float)(h))); float rxTR = (float)(nvg__minf((float)(radTopRight), (float)(halfw)) * nvg__signf((float)(w))); float ryTR = (float)(nvg__minf((float)(radTopRight), (float)(halfh)) * nvg__signf((float)(h))); float rxTL = (float)(nvg__minf((float)(radTopLeft), (float)(halfw)) * nvg__signf((float)(w))); float ryTL = (float)(nvg__minf((float)(radTopLeft), (float)(halfh)) * nvg__signf((float)(h))); float* vals = stackalloc float[44];
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
				nvg__appendCommands(ctx, vals, 44); }

		}

		public static void nvgEllipse(NVGcontext ctx, float cx, float cy, float rx, float ry)
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

			nvg__appendCommands(ctx, vals, 32);
		}

		public static void nvgCircle(NVGcontext ctx, float cx, float cy, float r)
		{
			nvgEllipse(ctx, (float)(cx), (float)(cy), (float)(r), (float)(r));
		}

		public static void nvgDebugDumpPathCache(NVGcontext ctx)
		{
			NVGpath* path;
			int i = 0; int j = 0;
			printf("Dumping %d cached paths\n", (int)(ctx.cache.npaths));
			for (i = (int)(0); (i) < (ctx.cache.npaths); i++) {
				path = &ctx.cache.paths[i]; printf(" - Path %d\n", (int)(i)); if ((path->nfill) != 0) {
					printf("   - fill: %d\n", (int)(path->nfill)); for (j = (int)(0); (j) < (path->nfill); j++) { printf("%f\t%f\n", (double)(path->fill[j].x), (double)(path->fill[j].y)); } }
				if ((path->nstroke) != 0) {
					printf("   - stroke: %d\n", (int)(path->nstroke)); for (j = (int)(0); (j) < (path->nstroke); j++) { printf("%f\t%f\n", (double)(path->stroke[j].x), (double)(path->stroke[j].y)); } }
			}
		}

		public static void nvgFill(NVGcontext ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGpath* path;
			NVGpaint fillPaint = (NVGpaint)(state->fill);
			int i = 0;
			nvg__flattenPaths(ctx);
			if (((ctx._params_.edgeAntiAlias) != 0) && ((state->shapeAntiAlias) != 0)) nvg__expandFill(ctx, (float)(ctx.fringeWidth), (int)(NVG_MITER), (float)(2.4f)); else nvg__expandFill(ctx, (float)(0.0f), (int)(NVG_MITER), (float)(2.4f));
			fillPaint.innerColor.a *= (float)(state->alpha);
			fillPaint.outerColor.a *= (float)(state->alpha);
			ctx._params_.renderFill(ctx._params_.userPtr, &fillPaint, (NVGcompositeOperationState)(state->compositeOperation), &state->scissor, (float)(ctx.fringeWidth), ctx.cache.bounds, ctx.cache.paths, (int)(ctx.cache.npaths));
			for (i = (int)(0); (i) < (ctx.cache.npaths); i++) {
				path = &ctx.cache.paths[i]; ctx.fillTriCount += (int)(path->nfill - 2); ctx.fillTriCount += (int)(path->nstroke - 2); ctx.drawCallCount += (int)(2); }
		}

		public static void nvgStroke(NVGcontext ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getAverageScale(state->xform));
			float strokeWidth = (float)(nvg__clampf((float)(state->strokeWidth * scale), (float)(0.0f), (float)(200.0f)));
			NVGpaint strokePaint = (NVGpaint)(state->stroke);
			NVGpath* path;
			int i = 0;
			if ((strokeWidth) < (ctx.fringeWidth)) {
				float alpha = (float)(nvg__clampf((float)(strokeWidth / ctx.fringeWidth), (float)(0.0f), (float)(1.0f))); strokePaint.innerColor.a *= (float)(alpha * alpha); strokePaint.outerColor.a *= (float)(alpha * alpha); strokeWidth = (float)(ctx.fringeWidth); }

			strokePaint.innerColor.a *= (float)(state->alpha);
			strokePaint.outerColor.a *= (float)(state->alpha);
			nvg__flattenPaths(ctx);
			if (((ctx._params_.edgeAntiAlias) != 0) && ((state->shapeAntiAlias) != 0)) nvg__expandStroke(ctx, (float)(strokeWidth * 0.5f), (float)(ctx.fringeWidth), (int)(state->lineCap), (int)(state->lineJoin), (float)(state->miterLimit)); else nvg__expandStroke(ctx, (float)(strokeWidth * 0.5f), (float)(0.0f), (int)(state->lineCap), (int)(state->lineJoin), (float)(state->miterLimit));
			ctx._params_.renderStroke(ctx._params_.userPtr, &strokePaint, (NVGcompositeOperationState)(state->compositeOperation), &state->scissor, (float)(ctx.fringeWidth), (float)(strokeWidth), ctx.cache.paths, (int)(ctx.cache.npaths));
			for (i = (int)(0); (i) < (ctx.cache.npaths); i++) {
				path = &ctx.cache.paths[i]; ctx.strokeTriCount += (int)(path->nstroke - 2); ctx.drawCallCount++; }
		}

		public static int nvgCreateFontMem(NVGcontext ctx, sbyte* name, byte* data, int ndata, int freeData)
		{
			return (int)(fonsAddFontMem(ctx.fs, name, data, (int)(ndata), (int)(freeData)));
		}

		public static int nvgFindFont(NVGcontext ctx, sbyte* name)
		{
			if ((name) == null) return (int)(-1);
			return (int)(fonsGetFontByName(ctx.fs, name));
		}

		public static int nvgAddFallbackFontId(NVGcontext ctx, int baseFont, int fallbackFont)
		{
			if (((baseFont) == (-1)) || ((fallbackFont) == (-1))) return (int)(0);
			return (int)(fonsAddFallbackFont(ctx.fs, (int)(baseFont), (int)(fallbackFont)));
		}

		public static int nvgAddFallbackFont(NVGcontext ctx, sbyte* baseFont, sbyte* fallbackFont)
		{
			return (int)(nvgAddFallbackFontId(ctx, (int)(nvgFindFont(ctx, baseFont)), (int)(nvgFindFont(ctx, fallbackFont))));
		}

		public static void nvgFontSize(NVGcontext ctx, float size)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontSize = (float)(size);
		}

		public static void nvgFontBlur(NVGcontext ctx, float blur)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontBlur = (float)(blur);
		}

		public static void nvgTextLetterSpacing(NVGcontext ctx, float spacing)
		{
			NVGstate* state = nvg__getState(ctx);
			state->letterSpacing = (float)(spacing);
		}

		public static void nvgTextLineHeight(NVGcontext ctx, float lineHeight)
		{
			NVGstate* state = nvg__getState(ctx);
			state->lineHeight = (float)(lineHeight);
		}

		public static void nvgTextAlign(NVGcontext ctx, int align)
		{
			NVGstate* state = nvg__getState(ctx);
			state->textAlign = (int)(align);
		}

		public static void nvgFontFaceId(NVGcontext ctx, int font)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontId = (int)(font);
		}

		public static void nvgFontFace(NVGcontext ctx, sbyte* font)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontId = (int)(fonsGetFontByName(ctx.fs, font));
		}

		public static float nvg__quantize(float a, float d)
		{
			return (float)(((int)(a / d + 0.5f)) * d);
		}

		public static float nvg__getFontScale(NVGstate* state)
		{
			return (float)(nvg__minf((float)(nvg__quantize((float)(nvg__getAverageScale(state->xform)), (float)(0.01f))), (float)(4.0f)));
		}

		public static void nvg__flushTextTexture(NVGcontext ctx)
		{
			int* dirty = stackalloc int[4];
			if ((fonsValidateTexture(ctx.fs, dirty)) != 0) {
				int fontImage = (int)(ctx.fontImages[ctx.fontImageIdx]); if (fontImage != 0) {
					int iw = 0; int ih = 0; byte* data = fonsGetTextureData(ctx.fs, &iw, &ih); int x = (int)(dirty[0]); int y = (int)(dirty[1]); int w = (int)(dirty[2] - dirty[0]); int h = (int)(dirty[3] - dirty[1]); ctx._params_.renderUpdateTexture(ctx._params_.userPtr, (int)(fontImage), (int)(x), (int)(y), (int)(w), (int)(h), data); }
			}

		}

		public static int nvg__allocTextAtlas(NVGcontext ctx)
		{
			int iw = 0; int ih = 0;
			nvg__flushTextTexture(ctx);
			if ((ctx.fontImageIdx) >= (4 - 1)) return (int)(0);
			if (ctx.fontImages[ctx.fontImageIdx + 1] != 0) nvgImageSize(ctx, (int)(ctx.fontImages[ctx.fontImageIdx + 1]), &iw, &ih); else {
				nvgImageSize(ctx, (int)(ctx.fontImages[ctx.fontImageIdx]), &iw, &ih); if ((iw) > (ih)) ih *= (int)(2); else iw *= (int)(2); if (((iw) > (2048)) || ((ih) > (2048))) iw = (int)(ih = (int)(2048)); ctx.fontImages[ctx.fontImageIdx + 1] = (int)(ctx._params_.renderCreateTexture(ctx._params_.userPtr, (int)(NVG_TEXTURE_ALPHA), (int)(iw), (int)(ih), (int)(0), null)); }

			++ctx.fontImageIdx;
			fonsResetAtlas(ctx.fs, (int)(iw), (int)(ih));
			return (int)(1);
		}

		public static void nvg__renderText(NVGcontext ctx, NVGvertex* verts, int nverts)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGpaint paint = (NVGpaint)(state->fill);
			paint.image = (int)(ctx.fontImages[ctx.fontImageIdx]);
			paint.innerColor.a *= (float)(state->alpha);
			paint.outerColor.a *= (float)(state->alpha);
			ctx._params_.renderTriangles(ctx._params_.userPtr, &paint, (NVGcompositeOperationState)(state->compositeOperation), &state->scissor, verts, (int)(nverts));
			ctx.drawCallCount++;
			ctx.textTriCount += (int)(nverts / 3);
		}

		public static float nvgText(NVGcontext ctx, float x, float y, sbyte* _string_, sbyte* end)
		{
			NVGstate* state = nvg__getState(ctx);
			FONStextIter iter = new FONStextIter(); FONStextIter prevIter = new FONStextIter();
			FONSquad q = new FONSquad();
			NVGvertex* verts;
			float scale = (float)(nvg__getFontScale(state) * ctx.devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int cverts = (int)(0);
			int nverts = (int)(0);
			if ((end) == null) end = _string_ + CRuntime.strlen(_string_);
			if ((state->fontId) == (-1)) return (float)(x);
			fonsSetSize(ctx.fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx.fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx.fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx.fs, (int)(state->textAlign));
			fonsSetFont(ctx.fs, (int)(state->fontId));
			cverts = (int)(nvg__maxi((int)(2), (int)(end - _string_)) * 6);
			verts = nvg__allocTempVerts(ctx, (int)(cverts));
			if ((verts) == null) return (float)(x);
			fonsTextIterInit(ctx.fs, iter, (float)(x * scale), (float)(y * scale), _string_, end, (int)(FONS_GLYPH_BITMAP_REQUIRED));
			prevIter = (FONStextIter)(iter);
			while ((fonsTextIterNext(ctx.fs, iter, &q)) != 0) {
				float* c = stackalloc float[4 * 2]; if ((iter.prevGlyphIndex) == (-1)) {
					if (nverts != 0) {
						nvg__renderText(ctx, verts, (int)(nverts)); nverts = (int)(0); }
					if (nvg__allocTextAtlas(ctx) == 0) break; iter = (FONStextIter)(prevIter); fonsTextIterNext(ctx.fs, iter, &q); if ((iter.prevGlyphIndex) == (-1)) break; }
				prevIter = (FONStextIter)(iter); nvgTransformPoint(&c[0], &c[1], state->xform, (float)(q.x0 * invscale), (float)(q.y0 * invscale)); nvgTransformPoint(&c[2], &c[3], state->xform, (float)(q.x1 * invscale), (float)(q.y0 * invscale)); nvgTransformPoint(&c[4], &c[5], state->xform, (float)(q.x1 * invscale), (float)(q.y1 * invscale)); nvgTransformPoint(&c[6], &c[7], state->xform, (float)(q.x0 * invscale), (float)(q.y1 * invscale)); if (nverts + 6 <= cverts) {
					nvg__vset(&verts[nverts], (float)(c[0]), (float)(c[1]), (float)(q.s0), (float)(q.t0)); nverts++; nvg__vset(&verts[nverts], (float)(c[4]), (float)(c[5]), (float)(q.s1), (float)(q.t1)); nverts++; nvg__vset(&verts[nverts], (float)(c[2]), (float)(c[3]), (float)(q.s1), (float)(q.t0)); nverts++; nvg__vset(&verts[nverts], (float)(c[0]), (float)(c[1]), (float)(q.s0), (float)(q.t0)); nverts++; nvg__vset(&verts[nverts], (float)(c[6]), (float)(c[7]), (float)(q.s0), (float)(q.t1)); nverts++; nvg__vset(&verts[nverts], (float)(c[4]), (float)(c[5]), (float)(q.s1), (float)(q.t1)); nverts++; }
			}
			nvg__flushTextTexture(ctx);
			nvg__renderText(ctx, verts, (int)(nverts));
			return (float)(iter.nextx / scale);
		}

		public static void nvgTextBox(NVGcontext ctx, float x, float y, float breakRowWidth, sbyte* _string_, sbyte* end)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGtextRow* rows = stackalloc NVGtextRow[2];
			int nrows = (int)(0); int i = 0;
			int oldAlign = (int)(state->textAlign);
			int haling = (int)(state->textAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state->textAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);
			if ((state->fontId) == (-1)) return;
			nvgTextMetrics(ctx, null, null, &lineh);
			state->textAlign = (int)(NVG_ALIGN_LEFT | valign);
			while ((nrows = (int)(nvgTextBreakLines(ctx, _string_, end, (float)(breakRowWidth), rows, (int)(2))))) {
				for (i = (int)(0); (i) < (nrows); i++) {
					NVGtextRow* row = &rows[i]; if ((haling & NVG_ALIGN_LEFT) != 0) nvgText(ctx, (float)(x), (float)(y), row->start, row->end); else if ((haling & NVG_ALIGN_CENTER) != 0) nvgText(ctx, (float)(x + breakRowWidth * 0.5f - row->width * 0.5f), (float)(y), row->start, row->end); else if ((haling & NVG_ALIGN_RIGHT) != 0) nvgText(ctx, (float)(x + breakRowWidth - row->width), (float)(y), row->start, row->end); y += (float)(lineh * state->lineHeight); } _string_ = rows[nrows - 1].next; }
			state->textAlign = (int)(oldAlign);
		}

		public static int nvgTextGlyphPositions(NVGcontext ctx, float x, float y, sbyte* _string_, sbyte* end, NVGglyphPosition* positions, int maxPositions)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx.devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FONStextIter iter = new FONStextIter(); FONStextIter prevIter = new FONStextIter();
			FONSquad q = new FONSquad();
			int npos = (int)(0);
			if ((state->fontId) == (-1)) return (int)(0);
			if ((end) == null) end = _string_ + CRuntime.strlen(_string_);
			if ((_string_) == (end)) return (int)(0);
			fonsSetSize(ctx.fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx.fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx.fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx.fs, (int)(state->textAlign));
			fonsSetFont(ctx.fs, (int)(state->fontId));
			fonsTextIterInit(ctx.fs, iter, (float)(x * scale), (float)(y * scale), _string_, end, (int)(FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FONStextIter)(iter);
			while ((fonsTextIterNext(ctx.fs, iter, &q)) != 0) {
				if (((iter.prevGlyphIndex) < (0)) && ((nvg__allocTextAtlas(ctx)) != 0)) {
					iter = (FONStextIter)(prevIter); fonsTextIterNext(ctx.fs, iter, &q); }
				prevIter = (FONStextIter)(iter); positions[npos].str = iter.str; positions[npos].x = (float)(iter.x * invscale); positions[npos].minx = (float)(nvg__minf((float)(iter.x), (float)(q.x0)) * invscale); positions[npos].maxx = (float)(nvg__maxf((float)(iter.nextx), (float)(q.x1)) * invscale); npos++; if ((npos) >= (maxPositions)) break; }
			return (int)(npos);
		}

		public static int nvgTextBreakLines(NVGcontext ctx, sbyte* _string_, sbyte* end, float breakRowWidth, NVGtextRow* rows, int maxRows)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx.devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FONStextIter iter = new FONStextIter(); FONStextIter prevIter = new FONStextIter();
			FONSquad q = new FONSquad();
			int nrows = (int)(0);
			float rowStartX = (float)(0);
			float rowWidth = (float)(0);
			float rowMinX = (float)(0);
			float rowMaxX = (float)(0);
			sbyte* rowStart = null;
			sbyte* rowEnd = null;
			sbyte* wordStart = null;
			float wordStartX = (float)(0);
			float wordMinX = (float)(0);
			sbyte* breakEnd = null;
			float breakWidth = (float)(0);
			float breakMaxX = (float)(0);
			int type = (int)(NVG_SPACE); int ptype = (int)(NVG_SPACE);
			uint pcodepoint = (uint)(0);
			if ((maxRows) == (0)) return (int)(0);
			if ((state->fontId) == (-1)) return (int)(0);
			if ((end) == null) end = _string_ + CRuntime.strlen(_string_);
			if ((_string_) == (end)) return (int)(0);
			fonsSetSize(ctx.fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx.fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx.fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx.fs, (int)(state->textAlign));
			fonsSetFont(ctx.fs, (int)(state->fontId));
			breakRowWidth *= (float)(scale);
			fonsTextIterInit(ctx.fs, iter, (float)(0), (float)(0), _string_, end, (int)(FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FONStextIter)(iter);
			while ((fonsTextIterNext(ctx.fs, iter, &q)) != 0) {
				if (((iter.prevGlyphIndex) < (0)) && ((nvg__allocTextAtlas(ctx)) != 0)) {
					iter = (FONStextIter)(prevIter); fonsTextIterNext(ctx.fs, iter, &q); }
				prevIter = (FONStextIter)(iter); switch (iter.codepoint) {
					case 9: case 11: case 12: case 32: case 0x00a0: type = (int)(NVG_SPACE); break; case 10: type = (int)((pcodepoint) == (13) ? NVG_SPACE : NVG_NEWLINE); break; case 13: type = (int)((pcodepoint) == (10) ? NVG_SPACE : NVG_NEWLINE); break; case 0x0085: type = (int)(NVG_NEWLINE); break; default: if ((((((((iter.codepoint) >= (0x4E00)) && (iter.codepoint <= 0x9FFF)) || (((iter.codepoint) >= (0x3000)) && (iter.codepoint <= 0x30FF))) || (((iter.codepoint) >= (0xFF00)) && (iter.codepoint <= 0xFFEF))) || (((iter.codepoint) >= (0x1100)) && (iter.codepoint <= 0x11FF))) || (((iter.codepoint) >= (0x3130)) && (iter.codepoint <= 0x318F))) || (((iter.codepoint) >= (0xAC00)) && (iter.codepoint <= 0xD7AF))) type = (int)(NVG_CJK_CHAR); else type = (int)(NVG_CHAR); break; }
				if ((type) == (NVG_NEWLINE)) {
					rows[nrows].start = rowStart != null ? rowStart : iter.str; rows[nrows].end = rowEnd != null ? rowEnd : iter.str; rows[nrows].width = (float)(rowWidth * invscale); rows[nrows].minx = (float)(rowMinX * invscale); rows[nrows].maxx = (float)(rowMaxX * invscale); rows[nrows].next = iter.next; nrows++; if ((nrows) >= (maxRows)) return (int)(nrows); breakEnd = rowStart; breakWidth = (float)(0.0); breakMaxX = (float)(0.0); rowStart = null; rowEnd = null; rowWidth = (float)(0); rowMinX = (float)(rowMaxX = (float)(0)); }
				else {
					if ((rowStart) == null) {
						if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) {
							rowStartX = (float)(iter.x); rowStart = iter.str; rowEnd = iter.next; rowWidth = (float)(iter.nextx - rowStartX); rowMinX = (float)(q.x0 - rowStartX); rowMaxX = (float)(q.x1 - rowStartX); wordStart = iter.str; wordStartX = (float)(iter.x); wordMinX = (float)(q.x0 - rowStartX); breakEnd = rowStart; breakWidth = (float)(0.0); breakMaxX = (float)(0.0); }
					}
					else {
						float nextWidth = (float)(iter.nextx - rowStartX); if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) {
							rowEnd = iter.next; rowWidth = (float)(iter.nextx - rowStartX); rowMaxX = (float)(q.x1 - rowStartX); }
						if (((((ptype) == (NVG_CHAR)) || ((ptype) == (NVG_CJK_CHAR))) && ((type) == (NVG_SPACE))) || ((type) == (NVG_CJK_CHAR))) {
							breakEnd = iter.str; breakWidth = (float)(rowWidth); breakMaxX = (float)(rowMaxX); }
						if ((((ptype) == (NVG_SPACE)) && (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))) || ((type) == (NVG_CJK_CHAR))) {
							wordStart = iter.str; wordStartX = (float)(iter.x); wordMinX = (float)(q.x0 - rowStartX); }
						if ((((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) && ((nextWidth) > (breakRowWidth))) {
							if ((breakEnd) == (rowStart)) {
								rows[nrows].start = rowStart; rows[nrows].end = iter.str; rows[nrows].width = (float)(rowWidth * invscale); rows[nrows].minx = (float)(rowMinX * invscale); rows[nrows].maxx = (float)(rowMaxX * invscale); rows[nrows].next = iter.str; nrows++; if ((nrows) >= (maxRows)) return (int)(nrows); rowStartX = (float)(iter.x); rowStart = iter.str; rowEnd = iter.next; rowWidth = (float)(iter.nextx - rowStartX); rowMinX = (float)(q.x0 - rowStartX); rowMaxX = (float)(q.x1 - rowStartX); wordStart = iter.str; wordStartX = (float)(iter.x); wordMinX = (float)(q.x0 - rowStartX); }
							else {
								rows[nrows].start = rowStart; rows[nrows].end = breakEnd; rows[nrows].width = (float)(breakWidth * invscale); rows[nrows].minx = (float)(rowMinX * invscale); rows[nrows].maxx = (float)(breakMaxX * invscale); rows[nrows].next = wordStart; nrows++; if ((nrows) >= (maxRows)) return (int)(nrows); rowStartX = (float)(wordStartX); rowStart = wordStart; rowEnd = iter.next; rowWidth = (float)(iter.nextx - rowStartX); rowMinX = (float)(wordMinX); rowMaxX = (float)(q.x1 - rowStartX); }
							breakEnd = rowStart; breakWidth = (float)(0.0); breakMaxX = (float)(0.0); }
					}
				}
				pcodepoint = (uint)(iter.codepoint); ptype = (int)(type); }
			if (rowStart != null) {
				rows[nrows].start = rowStart; rows[nrows].end = rowEnd; rows[nrows].width = (float)(rowWidth * invscale); rows[nrows].minx = (float)(rowMinX * invscale); rows[nrows].maxx = (float)(rowMaxX * invscale); rows[nrows].next = end; nrows++; }

			return (int)(nrows);
		}

		public static float nvgTextBounds(NVGcontext ctx, float x, float y, sbyte* _string_, sbyte* end, float* bounds)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx.devicePxRatio);
			float invscale = (float)(1.0f / scale);
			float width = 0;
			if ((state->fontId) == (-1)) return (float)(0);
			fonsSetSize(ctx.fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx.fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx.fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx.fs, (int)(state->textAlign));
			fonsSetFont(ctx.fs, (int)(state->fontId));
			width = (float)(fonsTextBounds(ctx.fs, (float)(x * scale), (float)(y * scale), _string_, end, bounds));
			if (bounds != null) {
				fonsLineBounds(ctx.fs, (float)(y * scale), &bounds[1], &bounds[3]); bounds[0] *= (float)(invscale); bounds[1] *= (float)(invscale); bounds[2] *= (float)(invscale); bounds[3] *= (float)(invscale); }

			return (float)(width * invscale);
		}

		public static void nvgTextBoxBounds(NVGcontext ctx, float x, float y, float breakRowWidth, sbyte* _string_, sbyte* end, float* bounds)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGtextRow* rows = stackalloc NVGtextRow[2];
			float scale = (float)(nvg__getFontScale(state) * ctx.devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int nrows = (int)(0); int i = 0;
			int oldAlign = (int)(state->textAlign);
			int haling = (int)(state->textAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state->textAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0); float rminy = (float)(0); float rmaxy = (float)(0);
			float minx = 0; float miny = 0; float maxx = 0; float maxy = 0;
			if ((state->fontId) == (-1)) {
				if (bounds != null) bounds[0] = (float)(bounds[1] = (float)(bounds[2] = (float)(bounds[3] = (float)(0.0f)))); return; }

			nvgTextMetrics(ctx, null, null, &lineh);
			state->textAlign = (int)(NVG_ALIGN_LEFT | valign);
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			fonsSetSize(ctx.fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx.fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx.fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx.fs, (int)(state->textAlign));
			fonsSetFont(ctx.fs, (int)(state->fontId));
			fonsLineBounds(ctx.fs, (float)(0), &rminy, &rmaxy);
			rminy *= (float)(invscale);
			rmaxy *= (float)(invscale);
			while ((nrows = (int)(nvgTextBreakLines(ctx, _string_, end, (float)(breakRowWidth), rows, (int)(2))))) {
				for (i = (int)(0); (i) < (nrows); i++) {
					NVGtextRow* row = &rows[i]; float rminx = 0; float rmaxx = 0; float dx = (float)(0); if ((haling & NVG_ALIGN_LEFT) != 0) dx = (float)(0); else if ((haling & NVG_ALIGN_CENTER) != 0) dx = (float)(breakRowWidth * 0.5f - row->width * 0.5f); else if ((haling & NVG_ALIGN_RIGHT) != 0) dx = (float)(breakRowWidth - row->width); rminx = (float)(x + row->minx + dx); rmaxx = (float)(x + row->maxx + dx); minx = (float)(nvg__minf((float)(minx), (float)(rminx))); maxx = (float)(nvg__maxf((float)(maxx), (float)(rmaxx))); miny = (float)(nvg__minf((float)(miny), (float)(y + rminy))); maxy = (float)(nvg__maxf((float)(maxy), (float)(y + rmaxy))); y += (float)(lineh * state->lineHeight); } _string_ = rows[nrows - 1].next; }
			state->textAlign = (int)(oldAlign);
			if (bounds != null) {
				bounds[0] = (float)(minx); bounds[1] = (float)(miny); bounds[2] = (float)(maxx); bounds[3] = (float)(maxy); }

		}

		public static void nvgTextMetrics(NVGcontext ctx, float* ascender, float* descender, float* lineh)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx.devicePxRatio);
			float invscale = (float)(1.0f / scale);
			if ((state->fontId) == (-1)) return;
			fonsSetSize(ctx.fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx.fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx.fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx.fs, (int)(state->textAlign));
			fonsSetFont(ctx.fs, (int)(state->fontId));
			fonsVertMetrics(ctx.fs, ascender, descender, lineh);
			if (ascender != null) *ascender *= (float)(invscale);
			if (descender != null) *descender *= (float)(invscale);
			if (lineh != null) *lineh *= (float)(invscale);
		}

	}
}