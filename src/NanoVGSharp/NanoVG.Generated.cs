using System;
using System.Runtime.InteropServices;

namespace SqliteSharp
{
	unsafe partial class Sqlite
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
		public const int STBTT_vmove = 1;
		public const int STBTT_vline = 2;
		public const int STBTT_vcurve = 3;
		public const int STBTT_PLATFORM_ID_UNICODE = 0;
		public const int STBTT_PLATFORM_ID_MAC = 1;
		public const int STBTT_PLATFORM_ID_ISO = 2;
		public const int STBTT_PLATFORM_ID_MICROSOFT = 3;
		public const int STBTT_UNICODE_EID_UNICODE_1_0 = 0;
		public const int STBTT_UNICODE_EID_UNICODE_1_1 = 1;
		public const int STBTT_UNICODE_EID_ISO_10646 = 2;
		public const int STBTT_UNICODE_EID_UNICODE_2_0_BMP = 3;
		public const int STBTT_UNICODE_EID_UNICODE_2_0_FULL = 4;
		public const int STBTT_MS_EID_SYMBOL = 0;
		public const int STBTT_MS_EID_UNICODE_BMP = 1;
		public const int STBTT_MS_EID_SHIFTJIS = 2;
		public const int STBTT_MS_EID_UNICODE_FULL = 10;
		public const int STBTT_MAC_EID_ROMAN = 0;
		public const int STBTT_MAC_EID_ARABIC = 4;
		public const int STBTT_MAC_EID_JAPANESE = 1;
		public const int STBTT_MAC_EID_HEBREW = 5;
		public const int STBTT_MAC_EID_CHINESE_TRAD = 2;
		public const int STBTT_MAC_EID_GREEK = 6;
		public const int STBTT_MAC_EID_KOREAN = 3;
		public const int STBTT_MAC_EID_RUSSIAN = 7;
		public const int STBTT_MS_LANG_ENGLISH = 0x0409;
		public const int STBTT_MS_LANG_ITALIAN = 0x0410;
		public const int STBTT_MS_LANG_CHINESE = 0x0804;
		public const int STBTT_MS_LANG_JAPANESE = 0x0411;
		public const int STBTT_MS_LANG_DUTCH = 0x0413;
		public const int STBTT_MS_LANG_KOREAN = 0x0412;
		public const int STBTT_MS_LANG_FRENCH = 0x040c;
		public const int STBTT_MS_LANG_RUSSIAN = 0x0419;
		public const int STBTT_MS_LANG_GERMAN = 0x0407;
		public const int STBTT_MS_LANG_SPANISH = 0x0409;
		public const int STBTT_MS_LANG_HEBREW = 0x040d;
		public const int STBTT_MS_LANG_SWEDISH = 0x041D;
		public const int STBTT_MAC_LANG_ENGLISH = 0;
		public const int STBTT_MAC_LANG_JAPANESE = 11;
		public const int STBTT_MAC_LANG_ARABIC = 12;
		public const int STBTT_MAC_LANG_KOREAN = 23;
		public const int STBTT_MAC_LANG_DUTCH = 4;
		public const int STBTT_MAC_LANG_RUSSIAN = 32;
		public const int STBTT_MAC_LANG_FRENCH = 1;
		public const int STBTT_MAC_LANG_SPANISH = 6;
		public const int STBTT_MAC_LANG_GERMAN = 2;
		public const int STBTT_MAC_LANG_SWEDISH = 5;
		public const int STBTT_MAC_LANG_HEBREW = 10;
		public const int STBTT_MAC_LANG_CHINESE_SIMPLIFIED = 33;
		public const int STBTT_MAC_LANG_ITALIAN = 3;
		public const int STBTT_MAC_LANG_CHINESE_TRAD = 19;
		public const int STBI_default = 0;
		public const int STBI_grey = 1;
		public const int STBI_grey_alpha = 2;
		public const int STBI_rgb = 3;
		public const int STBI_rgb_alpha = 4;
		public const int STBI__SCAN_load = 0;
		public const int STBI__SCAN_type = 1;
		public const int STBI__SCAN_header = 2;
		public const int STBI__F_none = 0;
		public const int STBI__F_sub = 1;
		public const int STBI__F_up = 2;
		public const int STBI__F_avg = 3;
		public const int STBI__F_paeth = 4;
		public const int STBI__F_avg_first = 5;
		public const int STBI__F_paeth_first = 6;
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
		public static float stbi__h2l_gamma_i = (float)(1.0f / 2.2f);
		public static float stbi__h2l_scale_i = (float)(1.0f);
		public static FakePtr<uint> stbi__bmask = new PinnedArray<uint>( new uint[] { 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767, 65535 });
		public static FakePtr<int> stbi__jbias = new PinnedArray<int>( new int[] { 0, -1, -3, -7, -15, -31, -63, -127, -255, -511, -1023, -2047, -4095, -8191, -16383, -32767 });
		public static FakePtr<byte> stbi__jpeg_dezigzag = new PinnedArray<byte>( new byte[] { 0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12, 19, 26, 33, 40, 48, 41, 34, 27, 20, 13, 6, 7, 14, 21, 28, 35, 42, 49, 56, 57, 50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51, 58, 59, 52, 45, 38, 31, 39, 46, 53, 60, 61, 54, 47, 55, 62, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63 });
		public static FakePtr<int> stbi__zlength_base = new PinnedArray<int>( new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 0, 0 });
		public static FakePtr<int> stbi__zlength_extra = new PinnedArray<int>( new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0 });
		public static FakePtr<int> stbi__zdist_base = new PinnedArray<int>( new int[] { 1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577, 0, 0 });
		public static FakePtr<int> stbi__zdist_extra = new PinnedArray<int>( new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 });
		public static FakePtr<byte> stbi__zdefault_length = FakePtr<byte>.CreateWithSize(288);
		public static FakePtr<byte> stbi__zdefault_distance = FakePtr<byte>.CreateWithSize(32);
		public static FakePtr<byte> first_row_filter = new PinnedArray<byte>( new byte[] { STBI__F_none, STBI__F_sub, STBI__F_none, STBI__F_avg_first, STBI__F_paeth_first });
		public static FakePtr<byte> stbi__depth_scale_table = new PinnedArray<byte>( new byte[] { 0, 0xff, 0x55, 0, 0x11, 0, 0, 0, 0x01 });
		public static int stbi__unpremultiply_on_load = (int)(0);
		public static int stbi__de_iphone_flag = (int)(0);
		[StructLayout(LayoutKind.Sequential)]
		public struct NVGcolor
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct _
	{
		public fixed float rgba[4];
		[StructLayout(LayoutKind.Sequential)]
		public struct _
	{
		public float r;
		public float g;
		public float b;
		public float a;
		}

		}

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
		public struct NVGparams
	{
		public void * userPtr;
		public int edgeAntiAlias;
		public int (void *)* renderCreate;
		public int (void *, int, int, int, int, const unsigned char *)* renderCreateTexture;
		public int (void *, int)* renderDeleteTexture;
		public int (void *, int, int, int, int, int, const unsigned char *)* renderUpdateTexture;
		public int (void *, int, int *, int *)* renderGetTextureSize;
		public void (void *, float, float, float)* renderViewport;
		public void (void *)* renderCancel;
		public void (void *)* renderFlush;
		public void (void *, NVGpaint *, NVGcompositeOperationState, NVGscissor *, float, const float *, const NVGpath *, int)* renderFill;
		public void (void *, NVGpaint *, NVGcompositeOperationState, NVGscissor *, float, float, const NVGpath *, int)* renderStroke;
		public void (void *, NVGpaint *, NVGcompositeOperationState, NVGscissor *, const NVGvertex *, int)* renderTriangles;
		public void (void *)* renderDelete;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSparams
	{
		public int width;
		public int height;
		public byte flags;
		public void * userPtr;
		public int (void *, int, int)* renderCreate;
		public int (void *, int, int)* renderResize;
		public void (void *, int *, const unsigned char *)* renderUpdate;
		public void (void *, const float *, const float *, const unsigned int *, int)* renderDraw;
		public void (void *)* renderDelete;
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

		[StructLayout(LayoutKind.Sequential)]
		public struct FONStextIter
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
		public FONSfont* font;
		public int prevGlyphIndex;
		public sbyte* str;
		public sbyte* next;
		public sbyte* end;
		public uint utf8state;
		public int bitmapOption;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_bakedchar
	{
		public ushort x0;
		public ushort y0;
		public ushort x1;
		public ushort y1;
		public float xoff;
		public float yoff;
		public float xadvance;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_aligned_quad
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

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_packedchar
	{
		public ushort x0;
		public ushort y0;
		public ushort x1;
		public ushort y1;
		public float xoff;
		public float yoff;
		public float xadvance;
		public float xoff2;
		public float yoff2;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_pack_range
	{
		public float font_size;
		public int first_unicode_codepoint_in_range;
		public int* array_of_unicode_codepoints;
		public int num_chars;
		public stbtt_packedchar* chardata_for_range;
		public byte h_oversample;
		public byte v_oversample;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_pack_context
	{
		public void * user_allocator_context;
		public void * pack_info;
		public int width;
		public int height;
		public int stride_in_bytes;
		public int padding;
		public uint h_oversample;
		public uint v_oversample;
		public byte* pixels;
		public void * nodes;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_fontinfo
	{
		public void * userdata;
		public byte* data;
		public int fontstart;
		public int numGlyphs;
		public int loca;
		public int head;
		public int glyf;
		public int hhea;
		public int hmtx;
		public int kern;
		public int index_map;
		public int indexToLocFormat;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt_vertex
	{
		public short x;
		public short y;
		public short cx;
		public short cy;
		public byte type;
		public byte padding;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__bitmap
	{
		public int w;
		public int h;
		public int stride;
		public byte* pixels;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__hheap_chunk
	{
		public stbtt__hheap_chunk* next;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__hheap
	{
		public stbtt__hheap_chunk* head;
		public void * first_free;
		public int num_remaining_in_head_chunk;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__edge
	{
		public float x0;
		public float y0;
		public float x1;
		public float y1;
		public int invert;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__active_edge
	{
		public stbtt__active_edge* next;
		public float fx;
		public float fdx;
		public float fdy;
		public float direction;
		public float sy;
		public float ey;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__point
	{
		public float x;
		public float y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbrp_context
	{
		public int width;
		public int height;
		public int x;
		public int y;
		public int bottom_y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbrp_node
	{
		public byte x;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbrp_rect
	{
		public int x;
		public int y;
		public int id;
		public int w;
		public int h;
		public int was_packed;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSttFontImpl
	{
		public stbtt_fontinfo font;
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

		[StructLayout(LayoutKind.Sequential)]
		public struct FONSfont
	{
		public FONSttFontImpl font;
		public fixed sbyte name[64];
		public byte* data;
		public int dataSize;
		public byte freeData;
		public float ascender;
		public float descender;
		public float lineh;
		public FONSglyph* glyphs;
		public int cglyphs;
		public int nglyphs;
		public fixed int lut[256];
		public fixed int fallbacks[20];
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
		public struct FONScontext
	{
		public FONSparams params;
		public float itw;
		public float ith;
		public byte* texData;
		public fixed int dirtyRect[4];
		public FONSfont** fonts;
		public FONSatlas* atlas;
		public int cfonts;
		public int nfonts;
		public fixed float verts[1024 * 2];
		public fixed float tcoords[1024 * 2];
		public fixed uint colors[1024];
		public int nverts;
		public byte* scratch;
		public int nscratch;
		public FakePtr<FONSstate> states;
		public int nstates;
		public void (void *, int, int)* handleError;
		public void * errorUptr;
		}

		public class stbi__context
	{
		public uint img_x;
		public uint img_y;
		public int img_n;
		public int img_out_n;
		public stbi_io_callbacks io = new stbi_io_callbacks();
		public void * io_user_data;
		public int read_from_callbacks;
		public int buflen;
		public fixed byte buffer_start[128] = new FakePtr<byte>(128);
		public byte* img_buffer;
		public byte* img_buffer_end;
		public byte* img_buffer_original;
		public byte* img_buffer_original_end;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbi__huffman
	{
		public fixed byte fast[1 << 9];
		public fixed ushort code[256];
		public fixed byte values[256];
		public fixed byte size[257];
		public fixed uint maxcode[18];
		public fixed int delta[17];
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbi__zhuffman
	{
		public fixed ushort fast[1 << 9];
		public fixed ushort firstcode[16];
		public fixed int maxcode[17];
		public fixed ushort firstsymbol[16];
		public fixed byte size[288];
		public fixed ushort value[288];
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbi__zbuf
	{
		public byte* zbuffer;
		public byte* zbuffer_end;
		public int num_bits;
		public uint code_buffer;
		public sbyte* zout;
		public sbyte* zout_start;
		public sbyte* zout_end;
		public int z_expandable;
		public stbi__zhuffman z_length;
		public stbi__zhuffman z_distance;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbi__pngchunk
	{
		public uint length;
		public uint type;
		}

		public class stbi__png
	{
		public stbi__context s;
		public byte* idata;
		public byte* expanded;
		public byte* _out_;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct stbi__bmp_data
	{
		public int bpp;
		public int offset;
		public int hsz;
		public uint mr;
		public uint mg;
		public uint mb;
		public uint ma;
		public uint all_a;
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

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGpathCache
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
		public fixed float bounds[4];
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NVGcontext
	{
		public NVGparams params;
		public float* commands;
		public int ccommands;
		public int ncommands;
		public float commandx;
		public float commandy;
		public FakePtr<NVGstate> states;
		public int nstates;
		public NVGpathCache* cache;
		public float tessTol;
		public float distTol;
		public float fringeWidth;
		public float devicePxRatio;
		public FONScontext* fs;
		public fixed int fontImages[4];
		public int fontImageIdx;
		public int drawCallCount;
		public int fillTriCount;
		public int strokeTriCount;
		public int textTriCount;
		}

		public static ushort ttUSHORT(byte* p)
		{
			return (ushort)(p[0] * 256 + p[1]);
		}

		public static short ttSHORT(byte* p)
		{
			return (short)(p[0] * 256 + p[1]);
		}

		public static uint ttULONG(byte* p)
		{
			return (uint)((p[0] << 24) + (p[1] << 16) + (p[2] << 8) + p[3]);
		}

		public static int ttLONG(byte* p)
		{
			return (int)((p[0] << 24) + (p[1] << 16) + (p[2] << 8) + p[3]);
		}

		public static int stbtt__isfont(byte* font)
		{
			if (((((((font)[0]) == ('1')) && (((font)[1]) == (0))) && (((font)[2]) == (0))) && (((font)[3]) == (0)))) return (int)(1);
			if (((((((font)[0]) == ("typ1"[0])) && (((font)[1]) == ("typ1"[1]))) && (((font)[2]) == ("typ1"[2]))) && (((font)[3]) == ("typ1"[3])))) return (int)(1);
			if (((((((font)[0]) == ("OTTO"[0])) && (((font)[1]) == ("OTTO"[1]))) && (((font)[2]) == ("OTTO"[2]))) && (((font)[3]) == ("OTTO"[3])))) return (int)(1);
			if (((((((font)[0]) == (0)) && (((font)[1]) == (1))) && (((font)[2]) == (0))) && (((font)[3]) == (0)))) return (int)(1);
			return (int)(0);
		}

		public static uint stbtt__find_table(byte* data, uint fontstart, sbyte* tag)
		{
			int num_tables = (int)(ttUSHORT(data + fontstart + 4));
			uint tabledir = (uint)(fontstart + 12);
			int i = 0;
			for (i = (int)(0); (i) < (num_tables); ++i) {
uint loc = (uint)(tabledir + 16 * i);if (((((((data + loc + 0)[0]) == (tag[0])) && (((data + loc + 0)[1]) == (tag[1]))) && (((data + loc + 0)[2]) == (tag[2]))) && (((data + loc + 0)[3]) == (tag[3])))) return (uint)(ttULONG(data + loc + 8));}
			return (uint)(0);
		}

		public static int stbtt_GetFontOffsetForIndex(byte* font_collection, int index)
		{
			if ((stbtt__isfont(font_collection)) != 0) return (int)((index) == (0)?0:-1);
			if (((((((font_collection)[0]) == ("ttcf"[0])) && (((font_collection)[1]) == ("ttcf"[1]))) && (((font_collection)[2]) == ("ttcf"[2]))) && (((font_collection)[3]) == ("ttcf"[3])))) {
if (((ttULONG(font_collection + 4)) == (0x00010000)) || ((ttULONG(font_collection + 4)) == (0x00020000))) {
int n = (int)(ttLONG(font_collection + 8));if ((index) >= (n)) return (int)(-1);return (int)(ttULONG(font_collection + 12 + index * 4));}
}

			return (int)(-1);
		}

		public static int stbtt_InitFont(stbtt_fontinfo* info, byte* data2, int fontstart)
		{
			byte* data = data2;
			uint cmap = 0;uint t = 0;
			int i = 0;int numTables = 0;
			info->data = data;
			info->fontstart = (int)(fontstart);
			cmap = (uint)(stbtt__find_table(data, (uint)(fontstart), "cmap"));
			info->loca = (int)(stbtt__find_table(data, (uint)(fontstart), "loca"));
			info->head = (int)(stbtt__find_table(data, (uint)(fontstart), "head"));
			info->glyf = (int)(stbtt__find_table(data, (uint)(fontstart), "glyf"));
			info->hhea = (int)(stbtt__find_table(data, (uint)(fontstart), "hhea"));
			info->hmtx = (int)(stbtt__find_table(data, (uint)(fontstart), "hmtx"));
			info->kern = (int)(stbtt__find_table(data, (uint)(fontstart), "kern"));
			if ((((((cmap== 0) || (info->loca== 0)) || (info->head== 0)) || (info->glyf== 0)) || (info->hhea== 0)) || (info->hmtx== 0)) return (int)(0);
			t = (uint)(stbtt__find_table(data, (uint)(fontstart), "maxp"));
			if ((t) != 0) info->numGlyphs = (int)(ttUSHORT(data + t + 4)); else info->numGlyphs = (int)(0xffff);
			numTables = (int)(ttUSHORT(data + cmap + 2));
			info->index_map = (int)(0);
			for (i = (int)(0); (i) < (numTables); ++i) {
uint encoding_record = (uint)(cmap + 4 + 8 * i);switch (ttUSHORT(data + encoding_record)){
case STBTT_PLATFORM_ID_MICROSOFT:switch (ttUSHORT(data + encoding_record + 2)){
case STBTT_MS_EID_UNICODE_BMP:case STBTT_MS_EID_UNICODE_FULL:info->index_map = (int)(cmap + ttULONG(data + encoding_record + 4));break;}
break;case STBTT_PLATFORM_ID_UNICODE:info->index_map = (int)(cmap + ttULONG(data + encoding_record + 4));break;}
}
			if ((info->index_map) == (0)) return (int)(0);
			info->indexToLocFormat = (int)(ttUSHORT(data + info->head + 50));
			return (int)(1);
		}

		public static int stbtt_FindGlyphIndex(stbtt_fontinfo* info, int unicode_codepoint)
		{
			byte* data = info->data;
			uint index_map = (uint)(info->index_map);
			ushort format = (ushort)(ttUSHORT(data + index_map + 0));
			if ((format) == (0)) {
int bytes = (int)(ttUSHORT(data + index_map + 2));if ((unicode_codepoint) < (bytes - 6)) return (int)(*(data + index_map + 6 + unicode_codepoint));return (int)(0);}
 else if ((format) == (6)) {
uint first = (uint)(ttUSHORT(data + index_map + 6));uint count = (uint)(ttUSHORT(data + index_map + 8));if ((((uint)(unicode_codepoint)) >= (first)) && (((uint)(unicode_codepoint)) < (first + count))) return (int)(ttUSHORT(data + index_map + 10 + (unicode_codepoint - first) * 2));return (int)(0);}
 else if ((format) == (2)) {
(void)((!!(0)) || (_wassert("0", "nanovg/stb_truetype.h", (uint)(1094)) , 0));return (int)(0);}
 else if ((format) == (4)) {
ushort segcount = (ushort)(ttUSHORT(data + index_map + 6) >> 1);ushort searchRange = (ushort)(ttUSHORT(data + index_map + 8) >> 1);ushort entrySelector = (ushort)(ttUSHORT(data + index_map + 10));ushort rangeShift = (ushort)(ttUSHORT(data + index_map + 12) >> 1);uint endCount = (uint)(index_map + 14);uint search = (uint)(endCount);if ((unicode_codepoint) > (0xffff)) return (int)(0);if ((unicode_codepoint) >= (ttUSHORT(data + search + rangeShift * 2))) search += (uint)(rangeShift * 2);search -= (uint)(2);while ((entrySelector) != 0) {
ushort end = 0;searchRange >>= 1;end = (ushort)(ttUSHORT(data + search + searchRange * 2));if ((unicode_codepoint) > (end)) search += (uint)(searchRange * 2);--entrySelector;}search += (uint)(2);{
ushort offset = 0;ushort start = 0;ushort item = (ushort)((search - endCount) >> 1);(void)((!!(unicode_codepoint <= ttUSHORT(data + endCount + 2 * item))) || (_wassert("unicode_codepoint <= ttUSHORT(data + endCount + 2*item)", "nanovg/stb_truetype.h", (uint)(1130)) , 0));start = (ushort)(ttUSHORT(data + index_map + 14 + segcount * 2 + 2 + 2 * item));if ((unicode_codepoint) < (start)) return (int)(0);offset = (ushort)(ttUSHORT(data + index_map + 14 + segcount * 6 + 2 + 2 * item));if ((offset) == (0)) return (int)((ushort)(unicode_codepoint + ttSHORT(data + index_map + 14 + segcount * 4 + 2 + 2 * item)));return (int)(ttUSHORT(data + offset + (unicode_codepoint - start) * 2 + index_map + 14 + segcount * 6 + 2 + 2 * item));}
}
 else if (((format) == (12)) || ((format) == (13))) {
uint ngroups = (uint)(ttULONG(data + index_map + 12));int low = 0;int high = 0;low = (int)(0);high = ((int)(ngroups));while ((low) < (high)) {
int mid = (int)(low + ((high - low) >> 1));uint start_char = (uint)(ttULONG(data + index_map + 16 + mid * 12));uint end_char = (uint)(ttULONG(data + index_map + 16 + mid * 12 + 4));if (((uint)(unicode_codepoint)) < (start_char)) high = (int)(mid); else if (((uint)(unicode_codepoint)) > (end_char)) low = (int)(mid + 1); else {
uint start_glyph = (uint)(ttULONG(data + index_map + 16 + mid * 12 + 8));if ((format) == (12)) return (int)(start_glyph + unicode_codepoint - start_char); else return (int)(start_glyph);}
}return (int)(0);}

			(void)((!!(0)) || (_wassert("0", "nanovg/stb_truetype.h", (uint)(1165)) , 0));
			return (int)(0);
		}

		public static int stbtt_GetCodepointShape(stbtt_fontinfo* info, int unicode_codepoint, stbtt_vertex** vertices)
		{
			return (int)(stbtt_GetGlyphShape(info, (int)(stbtt_FindGlyphIndex(info, (int)(unicode_codepoint))), vertices));
		}

		public static void stbtt_setvertex(stbtt_vertex* v, byte type, int x, int y, int cx, int cy)
		{
			v->type = (byte)(type);
			v->x = ((short)(x));
			v->y = ((short)(y));
			v->cx = ((short)(cx));
			v->cy = ((short)(cy));
		}

		public static int stbtt__GetGlyfOffset(stbtt_fontinfo* info, int glyph_index)
		{
			int g1 = 0;int g2 = 0;
			if ((glyph_index) >= (info->numGlyphs)) return (int)(-1);
			if ((info->indexToLocFormat) >= (2)) return (int)(-1);
			if ((info->indexToLocFormat) == (0)) {
g1 = (int)(info->glyf + ttUSHORT(info->data + info->loca + glyph_index * 2) * 2);g2 = (int)(info->glyf + ttUSHORT(info->data + info->loca + glyph_index * 2 + 2) * 2);}
 else {
g1 = (int)(info->glyf + ttULONG(info->data + info->loca + glyph_index * 4));g2 = (int)(info->glyf + ttULONG(info->data + info->loca + glyph_index * 4 + 4));}

			return (int)((g1) == (g2)?-1:g1);
		}

		public static int stbtt_GetGlyphBox(stbtt_fontinfo* info, int glyph_index, int* x0, int* y0, int* x1, int* y1)
		{
			int g = (int)(stbtt__GetGlyfOffset(info, (int)(glyph_index)));
			if ((g) < (0)) return (int)(0);
			if ((x0) != null) *x0 = (int)(ttSHORT(info->data + g + 2));
			if ((y0) != null) *y0 = (int)(ttSHORT(info->data + g + 4));
			if ((x1) != null) *x1 = (int)(ttSHORT(info->data + g + 6));
			if ((y1) != null) *y1 = (int)(ttSHORT(info->data + g + 8));
			return (int)(1);
		}

		public static int stbtt_GetCodepointBox(stbtt_fontinfo* info, int codepoint, int* x0, int* y0, int* x1, int* y1)
		{
			return (int)(stbtt_GetGlyphBox(info, (int)(stbtt_FindGlyphIndex(info, (int)(codepoint))), x0, y0, x1, y1));
		}

		public static int stbtt_IsGlyphEmpty(stbtt_fontinfo* info, int glyph_index)
		{
			short numberOfContours = 0;
			int g = (int)(stbtt__GetGlyfOffset(info, (int)(glyph_index)));
			if ((g) < (0)) return (int)(1);
			numberOfContours = (short)(ttSHORT(info->data + g));
			return (int)((numberOfContours) == (0)?1:0);
		}

		public static int stbtt__close_shape(stbtt_vertex* vertices, int num_vertices, int was_off, int start_off, int sx, int sy, int scx, int scy, int cx, int cy)
		{
			if ((start_off) != 0) {
if ((was_off) != 0) stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vcurve), (int)((cx + scx) >> 1), (int)((cy + scy) >> 1), (int)(cx), (int)(cy));stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vcurve), (int)(sx), (int)(sy), (int)(scx), (int)(scy));}
 else {
if ((was_off) != 0) stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vcurve), (int)(sx), (int)(sy), (int)(cx), (int)(cy)); else stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vline), (int)(sx), (int)(sy), (int)(0), (int)(0));}

			return (int)(num_vertices);
		}

		public static int stbtt_GetGlyphShape(stbtt_fontinfo* info, int glyph_index, stbtt_vertex** pvertices)
		{
			short numberOfContours = 0;
			byte* endPtsOfContours;
			byte* data = info->data;
			stbtt_vertex* vertices = null;
			int num_vertices = (int)(0);
			int g = (int)(stbtt__GetGlyfOffset(info, (int)(glyph_index)));
			*pvertices = (null);
			if ((g) < (0)) return (int)(0);
			numberOfContours = (short)(ttSHORT(data + g));
			if ((numberOfContours) > (0)) {
byte flags = (byte)(0);byte flagcount = 0;int ins = 0;int i = 0;int j = (int)(0);int m = 0;int n = 0;int next_move = 0;int was_off = (int)(0);int off = 0;int start_off = (int)(0);int x = 0;int y = 0;int cx = 0;int cy = 0;int sx = 0;int sy = 0;int scx = 0;int scy = 0;byte* points;endPtsOfContours = (data + g + 10);ins = (int)(ttUSHORT(data + g + 10 + numberOfContours * 2));points = data + g + 10 + numberOfContours * 2 + 2 + ins;n = (int)(1 + ttUSHORT(endPtsOfContours + numberOfContours * 2 - 2));m = (int)(n + 2 * numberOfContours);vertices = (stbtt_vertex*)(fons__tmpalloc((ulong)(m * sizeof((vertices[0]))), info->userdata));if ((vertices) == (null)) return (int)(0);next_move = (int)(0);flagcount = (byte)(0);off = (int)(m - n);for (i = (int)(0); (i) < (n); ++i) {
if ((flagcount) == (0)) {
flags = (byte)(*points++);if ((flags & 8) != 0) flagcount = (byte)(*points++);}
 else --flagcount;vertices[off + i].type = (byte)(flags);}x = (int)(0);for (i = (int)(0); (i) < (n); ++i) {
flags = (byte)(vertices[off + i].type);if ((flags & 2) != 0) {
short dx = (short)(*points++);x += (int)((flags & 16)?dx:-dx);}
 else {
if ((flags & 16)== 0) {
x = (int)(x + (short)(points[0] * 256 + points[1]));points += 2;}
}
vertices[off + i].x = ((short)(x));}y = (int)(0);for (i = (int)(0); (i) < (n); ++i) {
flags = (byte)(vertices[off + i].type);if ((flags & 4) != 0) {
short dy = (short)(*points++);y += (int)((flags & 32)?dy:-dy);}
 else {
if ((flags & 32)== 0) {
y = (int)(y + (short)(points[0] * 256 + points[1]));points += 2;}
}
vertices[off + i].y = ((short)(y));}num_vertices = (int)(0);sx = (int)(sy = (int)(cx = (int)(cy = (int)(scx = (int)(scy = (int)(0))))));for (i = (int)(0); (i) < (n); ++i) {
flags = (byte)(vertices[off + i].type);x = (int)(vertices[off + i].x);y = (int)(vertices[off + i].y);if ((next_move) == (i)) {
if (i != 0) num_vertices = (int)(stbtt__close_shape(vertices, (int)(num_vertices), (int)(was_off), (int)(start_off), (int)(sx), (int)(sy), (int)(scx), (int)(scy), (int)(cx), (int)(cy)));start_off = (int)(!(flags & 1));if ((start_off) != 0) {
scx = (int)(x);scy = (int)(y);if ((vertices[off + i + 1].type & 1)== 0) {
sx = (int)((x + (int)(vertices[off + i + 1].x)) >> 1);sy = (int)((y + (int)(vertices[off + i + 1].y)) >> 1);}
 else {
sx = ((int)(vertices[off + i + 1].x));sy = ((int)(vertices[off + i + 1].y));++i;}
}
 else {
sx = (int)(x);sy = (int)(y);}
stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vmove), (int)(sx), (int)(sy), (int)(0), (int)(0));was_off = (int)(0);next_move = (int)(1 + ttUSHORT(endPtsOfContours + j * 2));++j;}
 else {
if ((flags & 1)== 0) {
if ((was_off) != 0) stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vcurve), (int)((cx + x) >> 1), (int)((cy + y) >> 1), (int)(cx), (int)(cy));cx = (int)(x);cy = (int)(y);was_off = (int)(1);}
 else {
if ((was_off) != 0) stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vcurve), (int)(x), (int)(y), (int)(cx), (int)(cy)); else stbtt_setvertex(&vertices[num_vertices++], (byte)(STBTT_vline), (int)(x), (int)(y), (int)(0), (int)(0));was_off = (int)(0);}
}
}num_vertices = (int)(stbtt__close_shape(vertices, (int)(num_vertices), (int)(was_off), (int)(start_off), (int)(sx), (int)(sy), (int)(scx), (int)(scy), (int)(cx), (int)(cy)));}
 else if ((numberOfContours) == (-1)) {
int more = (int)(1);byte* comp = data + g + 10;num_vertices = (int)(0);vertices = null;while ((more) != 0) {
ushort flags = 0;ushort gidx = 0;int comp_num_verts = (int)(0);int i = 0;stbtt_vertex* comp_verts = null;stbtt_vertex* tmp = null;float* mtx = stackalloc float[6];
mtx[0] = (float)(1);
mtx[1] = (float)(0);
mtx[2] = (float)(0);
mtx[3] = (float)(1);
mtx[4] = (float)(0);
mtx[5] = (float)(0);
float m = 0;float n = 0;flags = (ushort)(ttSHORT(comp));comp += 2;gidx = (ushort)(ttSHORT(comp));comp += 2;if ((flags & 2) != 0) {
if ((flags & 1) != 0) {
mtx[4] = (float)(ttSHORT(comp));comp += 2;mtx[5] = (float)(ttSHORT(comp));comp += 2;}
 else {
mtx[4] = (float)(*(sbyte*)(comp));comp += 1;mtx[5] = (float)(*(sbyte*)(comp));comp += 1;}
}
 else {
(void)((!!(0)) || (_wassert("0", "nanovg/stb_truetype.h", (uint)(1407)) , 0));}
if ((flags & (1 << 3)) != 0) {
mtx[0] = (float)(mtx[3] = (float)(ttSHORT(comp) / 16384.0f));comp += 2;mtx[1] = (float)(mtx[2] = (float)(0));}
 else if ((flags & (1 << 6)) != 0) {
mtx[0] = (float)(ttSHORT(comp) / 16384.0f);comp += 2;mtx[1] = (float)(mtx[2] = (float)(0));mtx[3] = (float)(ttSHORT(comp) / 16384.0f);comp += 2;}
 else if ((flags & (1 << 7)) != 0) {
mtx[0] = (float)(ttSHORT(comp) / 16384.0f);comp += 2;mtx[1] = (float)(ttSHORT(comp) / 16384.0f);comp += 2;mtx[2] = (float)(ttSHORT(comp) / 16384.0f);comp += 2;mtx[3] = (float)(ttSHORT(comp) / 16384.0f);comp += 2;}
m = ((float)(CRuntime.sqrt((double)(mtx[0] * mtx[0] + mtx[1] * mtx[1]))));n = ((float)(CRuntime.sqrt((double)(mtx[2] * mtx[2] + mtx[3] * mtx[3]))));comp_num_verts = (int)(stbtt_GetGlyphShape(info, (int)(gidx), &comp_verts));if ((comp_num_verts) > (0)) {
for (i = (int)(0); (i) < (comp_num_verts); ++i) {
stbtt_vertex* v = &comp_verts[i];short x = 0;short y = 0;x = (short)(v->x);y = (short)(v->y);v->x = ((short)(m * (mtx[0] * x + mtx[2] * y + mtx[4])));v->y = ((short)(n * (mtx[1] * x + mtx[3] * y + mtx[5])));x = (short)(v->cx);y = (short)(v->cy);v->cx = ((short)(m * (mtx[0] * x + mtx[2] * y + mtx[4])));v->cy = ((short)(n * (mtx[1] * x + mtx[3] * y + mtx[5])));}tmp = (stbtt_vertex*)(fons__tmpalloc((ulong)((num_vertices + comp_num_verts) * sizeof(stbtt_vertex)), info->userdata));if (tmp== null) {
if ((vertices) != null) fons__tmpfree(vertices, info->userdata);if ((comp_verts) != null) fons__tmpfree(comp_verts, info->userdata);return (int)(0);}
if ((num_vertices) > (0)) CRuntime.memcpy(tmp, vertices, (ulong)(num_vertices * sizeof(stbtt_vertex)));CRuntime.memcpy(tmp + num_vertices, comp_verts, (ulong)(comp_num_verts * sizeof(stbtt_vertex)));if ((vertices) != null) fons__tmpfree(vertices, info->userdata);vertices = tmp;fons__tmpfree(comp_verts, info->userdata);num_vertices += (int)(comp_num_verts);}
more = (int)(flags & (1 << 5));}}
 else if ((numberOfContours) < (0)) {
(void)((!!(0)) || (_wassert("0", "nanovg/stb_truetype.h", (uint)(1460)) , 0));}
 else {
}

			*pvertices = vertices;
			return (int)(num_vertices);
		}

		public static void stbtt_GetGlyphHMetrics(stbtt_fontinfo* info, int glyph_index, int* advanceWidth, int* leftSideBearing)
		{
			ushort numOfLongHorMetrics = (ushort)(ttUSHORT(info->data + info->hhea + 34));
			if ((glyph_index) < (numOfLongHorMetrics)) {
if ((advanceWidth) != null) *advanceWidth = (int)(ttSHORT(info->data + info->hmtx + 4 * glyph_index));if ((leftSideBearing) != null) *leftSideBearing = (int)(ttSHORT(info->data + info->hmtx + 4 * glyph_index + 2));}
 else {
if ((advanceWidth) != null) *advanceWidth = (int)(ttSHORT(info->data + info->hmtx + 4 * (numOfLongHorMetrics - 1)));if ((leftSideBearing) != null) *leftSideBearing = (int)(ttSHORT(info->data + info->hmtx + 4 * numOfLongHorMetrics + 2 * (glyph_index - numOfLongHorMetrics)));}

		}

		public static int stbtt_GetGlyphKernAdvance(stbtt_fontinfo* info, int glyph1, int glyph2)
		{
			byte* data = info->data + info->kern;
			uint needle = 0;uint straw = 0;
			int l = 0;int r = 0;int m = 0;
			if (info->kern== 0) return (int)(0);
			if ((ttUSHORT(data + 2)) < (1)) return (int)(0);
			if (ttUSHORT(data + 8) != 1) return (int)(0);
			l = (int)(0);
			r = (int)(ttUSHORT(data + 10) - 1);
			needle = (uint)(glyph1 << 16 | glyph2);
			while (l <= r) {
m = (int)((l + r) >> 1);straw = (uint)(ttULONG(data + 18 + (m * 6)));if ((needle) < (straw)) r = (int)(m - 1); else if ((needle) > (straw)) l = (int)(m + 1); else return (int)(ttSHORT(data + 22 + (m * 6)));}
			return (int)(0);
		}

		public static int stbtt_GetCodepointKernAdvance(stbtt_fontinfo* info, int ch1, int ch2)
		{
			if (info->kern== 0) return (int)(0);
			return (int)(stbtt_GetGlyphKernAdvance(info, (int)(stbtt_FindGlyphIndex(info, (int)(ch1))), (int)(stbtt_FindGlyphIndex(info, (int)(ch2)))));
		}

		public static void stbtt_GetCodepointHMetrics(stbtt_fontinfo* info, int codepoint, int* advanceWidth, int* leftSideBearing)
		{
			stbtt_GetGlyphHMetrics(info, (int)(stbtt_FindGlyphIndex(info, (int)(codepoint))), advanceWidth, leftSideBearing);
		}

		public static void stbtt_GetFontVMetrics(stbtt_fontinfo* info, int* ascent, int* descent, int* lineGap)
		{
			if ((ascent) != null) *ascent = (int)(ttSHORT(info->data + info->hhea + 4));
			if ((descent) != null) *descent = (int)(ttSHORT(info->data + info->hhea + 6));
			if ((lineGap) != null) *lineGap = (int)(ttSHORT(info->data + info->hhea + 8));
		}

		public static void stbtt_GetFontBoundingBox(stbtt_fontinfo* info, int* x0, int* y0, int* x1, int* y1)
		{
			*x0 = (int)(ttSHORT(info->data + info->head + 36));
			*y0 = (int)(ttSHORT(info->data + info->head + 38));
			*x1 = (int)(ttSHORT(info->data + info->head + 40));
			*y1 = (int)(ttSHORT(info->data + info->head + 42));
		}

		public static float stbtt_ScaleForPixelHeight(stbtt_fontinfo* info, float height)
		{
			int fheight = (int)(ttSHORT(info->data + info->hhea + 4) - ttSHORT(info->data + info->hhea + 6));
			return (float)(height / fheight);
		}

		public static float stbtt_ScaleForMappingEmToPixels(stbtt_fontinfo* info, float pixels)
		{
			int unitsPerEm = (int)(ttUSHORT(info->data + info->head + 18));
			return (float)(pixels / unitsPerEm);
		}

		public static void stbtt_FreeShape(stbtt_fontinfo* info, stbtt_vertex* v)
		{
			fons__tmpfree(v, info->userdata);
		}

		public static void stbtt_GetGlyphBitmapBoxSubpixel(stbtt_fontinfo* font, int glyph, float scale_x, float scale_y, float shift_x, float shift_y, int* ix0, int* iy0, int* ix1, int* iy1)
		{
			int x0 = (int)(0);int y0 = (int)(0);int x1 = 0;int y1 = 0;
			if (stbtt_GetGlyphBox(font, (int)(glyph), &x0, &y0, &x1, &y1)== 0) {
if ((ix0) != null) *ix0 = (int)(0);if ((iy0) != null) *iy0 = (int)(0);if ((ix1) != null) *ix1 = (int)(0);if ((iy1) != null) *iy1 = (int)(0);}
 else {
if ((ix0) != null) *ix0 = ((int)(CRuntime.floor((double)(x0 * scale_x + shift_x))));if ((iy0) != null) *iy0 = ((int)(CRuntime.floor((double)(-y1 * scale_y + shift_y))));if ((ix1) != null) *ix1 = ((int)(CRuntime.ceil((double)(x1 * scale_x + shift_x))));if ((iy1) != null) *iy1 = ((int)(CRuntime.ceil((double)(-y0 * scale_y + shift_y))));}

		}

		public static void stbtt_GetGlyphBitmapBox(stbtt_fontinfo* font, int glyph, float scale_x, float scale_y, int* ix0, int* iy0, int* ix1, int* iy1)
		{
			stbtt_GetGlyphBitmapBoxSubpixel(font, (int)(glyph), (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), ix0, iy0, ix1, iy1);
		}

		public static void stbtt_GetCodepointBitmapBoxSubpixel(stbtt_fontinfo* font, int codepoint, float scale_x, float scale_y, float shift_x, float shift_y, int* ix0, int* iy0, int* ix1, int* iy1)
		{
			stbtt_GetGlyphBitmapBoxSubpixel(font, (int)(stbtt_FindGlyphIndex(font, (int)(codepoint))), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), ix0, iy0, ix1, iy1);
		}

		public static void stbtt_GetCodepointBitmapBox(stbtt_fontinfo* font, int codepoint, float scale_x, float scale_y, int* ix0, int* iy0, int* ix1, int* iy1)
		{
			stbtt_GetCodepointBitmapBoxSubpixel(font, (int)(codepoint), (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), ix0, iy0, ix1, iy1);
		}

		public static void * stbtt__hheap_alloc(stbtt__hheap* hh, ulong size, void * userdata)
		{
			if ((hh->first_free) != null) {
void * p = hh->first_free;hh->first_free = *(void **)(p);return p;}
 else {
if ((hh->num_remaining_in_head_chunk) == (0)) {
int count = (int)((size) < (32)?2000:(size) < (128)?800:100);stbtt__hheap_chunk* c = (stbtt__hheap_chunk*)(fons__tmpalloc((ulong)(sizeof(stbtt__hheap_chunk) + size * count), userdata));if ((c) == (null)) return (null);c->next = hh->head;hh->head = c;hh->num_remaining_in_head_chunk = (int)(count);}
--hh->num_remaining_in_head_chunk;return (sbyte*)(hh->head) + size * hh->num_remaining_in_head_chunk;}

		}

		public static void stbtt__hheap_free(stbtt__hheap* hh, void * p)
		{
			*(void **)(p) = hh->first_free;
			hh->first_free = p;
		}

		public static void stbtt__hheap_cleanup(stbtt__hheap* hh, void * userdata)
		{
			stbtt__hheap_chunk* c = hh->head;
			while ((c) != null) {
stbtt__hheap_chunk* n = c->next;fons__tmpfree(c, userdata);c = n;}
		}

		public static stbtt__active_edge* stbtt__new_active(stbtt__hheap* hh, stbtt__edge* e, int off_x, float start_point, void * userdata)
		{
			stbtt__active_edge* z = (stbtt__active_edge*)(stbtt__hheap_alloc(hh, (ulong)(sizeof((*z))), userdata));
			float dxdy = (float)((e->x1 - e->x0) / (e->y1 - e->y0));
			(void)((!!(z != (null))) || (_wassert("z != ((void *)0)", "nanovg/stb_truetype.h", (uint)(1700)) , 0));
			if (z== null) return z;
			z->fdx = (float)(dxdy);
			z->fdy = (float)(dxdy != 0.0f?(1.0f / dxdy):0.0f);
			z->fx = (float)(e->x0 + dxdy * (start_point - e->y0));
			z->fx -= (float)(off_x);
			z->direction = (float)((e->invert) != 0?1.0f:-1.0f);
			z->sy = (float)(e->y0);
			z->ey = (float)(e->y1);
			z->next = null;
			return z;
		}

		public static void stbtt__handle_clipped_edge(float* scanline, int x, stbtt__active_edge* e, float x0, float y0, float x1, float y1)
		{
			if ((y0) == (y1)) return;
			(void)((!!((y0) < (y1))) || (_wassert("y0 < y1", "nanovg/stb_truetype.h", (uint)(1870)) , 0));
			(void)((!!(e->sy <= e->ey)) || (_wassert("e->sy <= e->ey", "nanovg/stb_truetype.h", (uint)(1871)) , 0));
			if ((y0) > (e->ey)) return;
			if ((y1) < (e->sy)) return;
			if ((y0) < (e->sy)) {
x0 += (float)((x1 - x0) * (e->sy - y0) / (y1 - y0));y0 = (float)(e->sy);}

			if ((y1) > (e->ey)) {
x1 += (float)((x1 - x0) * (e->ey - y1) / (y1 - y0));y1 = (float)(e->ey);}

			if ((x0) == (x)) (void)((!!(x1 <= x + 1)) || (_wassert("x1 <= x+1", "nanovg/stb_truetype.h", (uint)(1884)) , 0)); else if ((x0) == (x + 1)) (void)((!!((x1) >= (x))) || (_wassert("x1 >= x", "nanovg/stb_truetype.h", (uint)(1886)) , 0)); else if (x0 <= x) (void)((!!(x1 <= x)) || (_wassert("x1 <= x", "nanovg/stb_truetype.h", (uint)(1888)) , 0)); else if ((x0) >= (x + 1)) (void)((!!((x1) >= (x + 1))) || (_wassert("x1 >= x+1", "nanovg/stb_truetype.h", (uint)(1890)) , 0)); else (void)((!!(((x1) >= (x)) && (x1 <= x + 1))) || (_wassert("x1 >= x && x1 <= x+1", "nanovg/stb_truetype.h", (uint)(1892)) , 0));
			if ((x0 <= x) && (x1 <= x)) scanline[x] += (float)(e->direction * (y1 - y0)); else if (((x0) >= (x + 1)) && ((x1) >= (x + 1)))  else {
(void)((!!(((((x0) >= (x)) && (x0 <= x + 1)) && ((x1) >= (x))) && (x1 <= x + 1))) || (_wassert("x0 >= x && x0 <= x+1 && x1 >= x && x1 <= x+1", "nanovg/stb_truetype.h", (uint)(1899)) , 0));scanline[x] += (float)(e->direction * (y1 - y0) * (1 - ((x0 - x) + (x1 - x)) / 2));}

		}

		public static void stbtt__fill_active_edges_new(float* scanline, float* scanline_fill, int len, stbtt__active_edge* e, float y_top)
		{
			float y_bottom = (float)(y_top + 1);
			while ((e) != null) {
(void)((!!((e->ey) >= (y_top))) || (_wassert("e->ey >= y_top", "nanovg/stb_truetype.h", (uint)(1912)) , 0));if ((e->fdx) == (0)) {
float x0 = (float)(e->fx);if ((x0) < (len)) {
if ((x0) >= (0)) {
stbtt__handle_clipped_edge(scanline, (int)(x0), e, (float)(x0), (float)(y_top), (float)(x0), (float)(y_bottom));stbtt__handle_clipped_edge(scanline_fill - 1, (int)((int)(x0) + 1), e, (float)(x0), (float)(y_top), (float)(x0), (float)(y_bottom));}
 else {
stbtt__handle_clipped_edge(scanline_fill - 1, (int)(0), e, (float)(x0), (float)(y_top), (float)(x0), (float)(y_bottom));}
}
}
 else {
float x0 = (float)(e->fx);float dx = (float)(e->fdx);float xb = (float)(x0 + dx);float x_top = 0;float x_bottom = 0;float sy0 = 0;float sy1 = 0;float dy = (float)(e->fdy);(void)((!!((e->sy <= y_bottom) && ((e->ey) >= (y_top)))) || (_wassert("e->sy <= y_bottom && e->ey >= y_top", "nanovg/stb_truetype.h", (uint)(1931)) , 0));if ((e->sy) > (y_top)) {
x_top = (float)(x0 + dx * (e->sy - y_top));sy0 = (float)(e->sy);}
 else {
x_top = (float)(x0);sy0 = (float)(y_top);}
if ((e->ey) < (y_bottom)) {
x_bottom = (float)(x0 + dx * (e->ey - y_top));sy1 = (float)(e->ey);}
 else {
x_bottom = (float)(xb);sy1 = (float)(y_bottom);}
if (((((x_top) >= (0)) && ((x_bottom) >= (0))) && ((x_top) < (len))) && ((x_bottom) < (len))) {
if (((int)(x_top)) == ((int)(x_bottom))) {
float height = 0;int x = (int)(x_top);height = (float)(sy1 - sy0);(void)((!!(((x) >= (0)) && ((x) < (len)))) || (_wassert("x >= 0 && x < len", "nanovg/stb_truetype.h", (uint)(1959)) , 0));scanline[x] += (float)(e->direction * (1 - ((x_top - x) + (x_bottom - x)) / 2) * height);scanline_fill[x] += (float)(e->direction * height);}
 else {
int x = 0;int x1 = 0;int x2 = 0;float y_crossing = 0;float step = 0;float sign = 0;float area = 0;if ((x_top) > (x_bottom)) {
float t = 0;sy0 = (float)(y_bottom - (sy0 - y_top));sy1 = (float)(y_bottom - (sy1 - y_top));t = (float)(sy0) , sy0 = (float)(sy1) , sy1 = (float)(t);t = (float)(x_bottom) , x_bottom = (float)(x_top) , x_top = (float)(t);dx = (float)(-dx);dy = (float)(-dy);t = (float)(x0) , x0 = (float)(xb) , xb = (float)(t);}
x1 = ((int)(x_top));x2 = ((int)(x_bottom));y_crossing = (float)((x1 + 1 - x0) * dy + y_top);sign = (float)(e->direction);area = (float)(sign * (y_crossing - sy0));scanline[x1] += (float)(area * (1 - ((x_top - x1) + (x1 + 1 - x1)) / 2));step = (float)(sign * dy);for (x = (int)(x1 + 1); (x) < (x2); ++x) {
scanline[x] += (float)(area + step / 2);area += (float)(step);}y_crossing += (float)(dy * (x2 - (x1 + 1)));(void)((!!(CRuntime.fabs((double)(area)) <= 1.01f)) || (_wassert("fabs(area) <= 1.01f", "nanovg/stb_truetype.h", (uint)(1996)) , 0));scanline[x2] += (float)(area + sign * (1 - ((x2 - x2) + (x_bottom - x2)) / 2) * (sy1 - y_crossing));scanline_fill[x2] += (float)(sign * (sy1 - sy0));}
}
 else {
int x = 0;for (x = (int)(0); (x) < (len); ++x) {
float y0 = (float)(y_top);float x1 = (float)(x);float x2 = (float)(x + 1);float x3 = (float)(xb);float y3 = (float)(y_bottom);float y1 = 0;float y2 = 0;y1 = (float)((x - x0) / dx + y_top);y2 = (float)((x + 1 - x0) / dx + y_top);if (((x0) < (x1)) && ((x3) > (x2))) {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x1), (float)(y1));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x2), (float)(y2));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x3), (float)(y3));}
 else if (((x3) < (x1)) && ((x0) > (x2))) {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x2), (float)(y2));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x1), (float)(y1));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x3), (float)(y3));}
 else if (((x0) < (x1)) && ((x3) > (x1))) {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x1), (float)(y1));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x3), (float)(y3));}
 else if (((x3) < (x1)) && ((x0) > (x1))) {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x1), (float)(y1));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x3), (float)(y3));}
 else if (((x0) < (x2)) && ((x3) > (x2))) {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x2), (float)(y2));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x3), (float)(y3));}
 else if (((x3) < (x2)) && ((x0) > (x2))) {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x2), (float)(y2));stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x3), (float)(y3));}
 else {
stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x3), (float)(y3));}
}}
}
e = e->next;}
		}

		public static void stbtt__rasterize_sorted_edges(stbtt__bitmap* result, stbtt__edge* e, int n, int vsubsample, int off_x, int off_y, void * userdata)
		{
			stbtt__hheap hh = (stbtt__hheap)({ null, null, 0 });
			stbtt__active_edge* active = (null);
			int y = 0;int j = (int)(0);int i = 0;
			float* scanline_data = stackalloc float[129];float* scanline;float* scanline2;
			if ((result->w) > (64)) scanline = (float*)(fons__tmpalloc((ulong)((result->w * 2 + 1) * sizeof(float)), userdata)); else scanline = scanline_data;
			scanline2 = scanline + result->w;
			y = (int)(off_y);
			e[n].y0 = (float)((float)(off_y + result->h) + 1);
			while ((j) < (result->h)) {
float scan_y_top = (float)(y + 0.0f);float scan_y_bottom = (float)(y + 1.0f);stbtt__active_edge** step = &active;CRuntime.memset(scanline, (int)(0), (ulong)(result->w * sizeof((scanline[0]))));CRuntime.memset(scanline2, (int)(0), (ulong)((result->w + 1) * sizeof((scanline[0]))));while ((*step) != null) {
stbtt__active_edge* z = *step;if (z->ey <= scan_y_top) {
*step = z->next;(void)((!!(z->direction)) || (_wassert("z->direction", "nanovg/stb_truetype.h", (uint)(2099)) , 0));z->direction = (float)(0);stbtt__hheap_free(&hh, z);}
 else {
step = &((*step)->next);}
}while (e->y0 <= scan_y_bottom) {
if (e->y0 != e->y1) {
stbtt__active_edge* z = stbtt__new_active(&hh, e, (int)(off_x), (float)(scan_y_top), userdata);if (z != (null)) {
(void)((!!((z->ey) >= (scan_y_top))) || (_wassert("z->ey >= scan_y_top", "nanovg/stb_truetype.h", (uint)(2112)) , 0));z->next = active;active = z;}
}
++e;}if ((active) != null) stbtt__fill_active_edges_new(scanline, scanline2 + 1, (int)(result->w), active, (float)(scan_y_top));{
float sum = (float)(0);for (i = (int)(0); (i) < (result->w); ++i) {
float k = 0;int m = 0;sum += (float)(scanline2[i]);k = (float)(scanline[i] + sum);k = (float)((float)(CRuntime.fabs((double)(k))) * 255 + 0.5f);m = ((int)(k));if ((m) > (255)) m = (int)(255);result->pixels[j * result->stride + i] = ((byte)(m));}}
step = &active;while ((*step) != null) {
stbtt__active_edge* z = *step;z->fx += (float)(z->fdx);step = &((*step)->next);}++y;++j;}
			stbtt__hheap_cleanup(&hh, userdata);
			if (scanline != scanline_data) fons__tmpfree(scanline, userdata);
		}

		public static void stbtt__sort_edges_ins_sort(stbtt__edge* p, int n)
		{
			int i = 0;int j = 0;
			for (i = (int)(1); (i) < (n); ++i) {
stbtt__edge t = (stbtt__edge)(p[i]);stbtt__edge* a = &t;j = (int)(i);while ((j) > (0)) {
stbtt__edge* b = &p[j - 1];int c = (int)(((a)->y0) < ((b)->y0));if (c== 0) break;p[j] = (stbtt__edge)(p[j - 1]);--j;}if (i != j) p[j] = (stbtt__edge)(t);}
		}

		public static void stbtt__sort_edges_quicksort(stbtt__edge* p, int n)
		{
			while ((n) > (12)) {
stbtt__edge t =  new stbtt__edge();int c01 = 0;int c12 = 0;int c = 0;int m = 0;int i = 0;int j = 0;m = (int)(n >> 1);c01 = (int)(((&p[0])->y0) < ((&p[m])->y0)?1:0);c12 = (int)(((&p[m])->y0) < ((&p[n - 1])->y0)?1:0);if (c01 != c12) {
int z = 0;c = (int)(((&p[0])->y0) < ((&p[n - 1])->y0)?1:0);z = (int)(((c) == (c12))?0:n - 1);t = (stbtt__edge)(p[z]);p[z] = (stbtt__edge)(p[m]);p[m] = (stbtt__edge)(t);}
t = (stbtt__edge)(p[0]);p[0] = (stbtt__edge)(p[m]);p[m] = (stbtt__edge)(t);i = (int)(1);j = (int)(n - 1);for (; ; ) {
for (++i; {
if (!(((&p[i])->y0) < ((&p[0])->y0))) break;}
; ) {}for (--j; {
if (!(((&p[0])->y0) < ((&p[j])->y0))) break;}
; ) {}if ((i) >= (j)) break;t = (stbtt__edge)(p[i]);p[i] = (stbtt__edge)(p[j]);p[j] = (stbtt__edge)(t);++i;--j;}if ((j) < (n - i)) {
stbtt__sort_edges_quicksort(p, (int)(j));p = p + i;n = (int)(n - i);}
 else {
stbtt__sort_edges_quicksort(p + i, (int)(n - i));n = (int)(j);}
}
		}

		public static void stbtt__sort_edges(stbtt__edge* p, int n)
		{
			stbtt__sort_edges_quicksort(p, (int)(n));
			stbtt__sort_edges_ins_sort(p, (int)(n));
		}

		public static void stbtt__rasterize(stbtt__bitmap* result, stbtt__point* pts, int* wcount, int windings, float scale_x, float scale_y, float shift_x, float shift_y, int off_x, int off_y, int invert, void * userdata)
		{
			float y_scale_inv = (float)((invert) != 0?-scale_y:scale_y);
			stbtt__edge* e;
			int n = 0;int i = 0;int j = 0;int k = 0;int m = 0;
			int vsubsample = (int)(1);
			n = (int)(0);
			for (i = (int)(0); (i) < (windings); ++i) {n += (int)(wcount[i]);}
			e = (stbtt__edge*)(fons__tmpalloc((ulong)(sizeof((*e)) * (n + 1)), userdata));
			if ((e) == (null)) return;
			n = (int)(0);
			m = (int)(0);
			for (i = (int)(0); (i) < (windings); ++i) {
stbtt__point* p = pts + m;m += (int)(wcount[i]);j = (int)(wcount[i] - 1);for (k = (int)(0); (k) < (wcount[i]); j = (int)(k++)) {
int a = (int)(k);int b = (int)(j);if ((p[j].y) == (p[k].y)) continue;e[n].invert = (int)(0);if (((invert) != 0?(p[j].y) > (p[k].y):(p[j].y) < (p[k].y)) != 0) {
e[n].invert = (int)(1);a = (int)(j) , b = (int)(k);}
e[n].x0 = (float)(p[a].x * scale_x + shift_x);e[n].y0 = (float)((p[a].y * y_scale_inv + shift_y) * vsubsample);e[n].x1 = (float)(p[b].x * scale_x + shift_x);e[n].y1 = (float)((p[b].y * y_scale_inv + shift_y) * vsubsample);++n;}}
			stbtt__sort_edges(e, (int)(n));
			stbtt__rasterize_sorted_edges(result, e, (int)(n), (int)(vsubsample), (int)(off_x), (int)(off_y), userdata);
			fons__tmpfree(e, userdata);
		}

		public static void stbtt__add_point(stbtt__point* points, int n, float x, float y)
		{
			if (points== null) return;
			points[n].x = (float)(x);
			points[n].y = (float)(y);
		}

		public static int stbtt__tesselate_curve(stbtt__point* points, int* num_points, float x0, float y0, float x1, float y1, float x2, float y2, float objspace_flatness_squared, int n)
		{
			float mx = (float)((x0 + 2 * x1 + x2) / 4);
			float my = (float)((y0 + 2 * y1 + y2) / 4);
			float dx = (float)((x0 + x2) / 2 - mx);
			float dy = (float)((y0 + y2) / 2 - my);
			if ((n) > (16)) return (int)(1);
			if ((dx * dx + dy * dy) > (objspace_flatness_squared)) {
stbtt__tesselate_curve(points, num_points, (float)(x0), (float)(y0), (float)((x0 + x1) / 2.0f), (float)((y0 + y1) / 2.0f), (float)(mx), (float)(my), (float)(objspace_flatness_squared), (int)(n + 1));stbtt__tesselate_curve(points, num_points, (float)(mx), (float)(my), (float)((x1 + x2) / 2.0f), (float)((y1 + y2) / 2.0f), (float)(x2), (float)(y2), (float)(objspace_flatness_squared), (int)(n + 1));}
 else {
stbtt__add_point(points, (int)(*num_points), (float)(x2), (float)(y2));*num_points = (int)(*num_points + 1);}

			return (int)(1);
		}

		public static stbtt__point* stbtt_FlattenCurves(stbtt_vertex* vertices, int num_verts, float objspace_flatness, int** contour_lengths, int* num_contours, void * userdata)
		{
			stbtt__point* points = null;
			int num_points = (int)(0);
			float objspace_flatness_squared = (float)(objspace_flatness * objspace_flatness);
			int i = 0;int n = (int)(0);int start = (int)(0);int pass = 0;
			for (i = (int)(0); (i) < (num_verts); ++i) {if ((vertices[i].type) == (STBTT_vmove)) ++n;}
			*num_contours = (int)(n);
			if ((n) == (0)) return null;
			*contour_lengths = (int*)(fons__tmpalloc((ulong)(sizeof((**contour_lengths)) * n), userdata));
			if ((*contour_lengths) == (null)) {
*num_contours = (int)(0);return null;}

			for (pass = (int)(0); (pass) < (2); ++pass) {
float x = (float)(0);float y = (float)(0);if ((pass) == (1)) {
points = (stbtt__point*)(fons__tmpalloc((ulong)(num_points * sizeof((points[0]))), userdata));if ((points) == (null)) goto error;}
num_points = (int)(0);n = (int)(-1);for (i = (int)(0); (i) < (num_verts); ++i) {
switch (vertices[i].type){
case STBTT_vmove:if ((n) >= (0)) (*contour_lengths)[n] = (int)(num_points - start);++n;start = (int)(num_points);x = (float)(vertices[i].x) , y = (float)(vertices[i].y);stbtt__add_point(points, (int)(num_points++), (float)(x), (float)(y));break;case STBTT_vline:x = (float)(vertices[i].x) , y = (float)(vertices[i].y);stbtt__add_point(points, (int)(num_points++), (float)(x), (float)(y));break;case STBTT_vcurve:stbtt__tesselate_curve(points, &num_points, (float)(x), (float)(y), (float)(vertices[i].cx), (float)(vertices[i].cy), (float)(vertices[i].x), (float)(vertices[i].y), (float)(objspace_flatness_squared), (int)(0));x = (float)(vertices[i].x) , y = (float)(vertices[i].y);break;}
}(*contour_lengths)[n] = (int)(num_points - start);}
			return points;
			error:;
fons__tmpfree(points, userdata);
			fons__tmpfree(*contour_lengths, userdata);
			*contour_lengths = null;
			*num_contours = (int)(0);
			return (null);
		}

		public static void stbtt_Rasterize(stbtt__bitmap* result, float flatness_in_pixels, stbtt_vertex* vertices, int num_verts, float scale_x, float scale_y, float shift_x, float shift_y, int x_off, int y_off, int invert, void * userdata)
		{
			float scale = (float)((scale_x) > (scale_y)?scale_y:scale_x);
			int winding_count = 0;int* winding_lengths;
			stbtt__point* windings = stbtt_FlattenCurves(vertices, (int)(num_verts), (float)(flatness_in_pixels / scale), &winding_lengths, &winding_count, userdata);
			if ((windings) != null) {
stbtt__rasterize(result, windings, winding_lengths, (int)(winding_count), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(x_off), (int)(y_off), (int)(invert), userdata);fons__tmpfree(winding_lengths, userdata);fons__tmpfree(windings, userdata);}

		}

		public static void stbtt_FreeBitmap(byte* bitmap, void * userdata)
		{
			fons__tmpfree(bitmap, userdata);
		}

		public static byte* stbtt_GetGlyphBitmapSubpixel(stbtt_fontinfo* info, float scale_x, float scale_y, float shift_x, float shift_y, int glyph, int* width, int* height, int* xoff, int* yoff)
		{
			int ix0 = 0;int iy0 = 0;int ix1 = 0;int iy1 = 0;
			stbtt__bitmap gbm =  new stbtt__bitmap();
			stbtt_vertex* vertices;
			int num_verts = (int)(stbtt_GetGlyphShape(info, (int)(glyph), &vertices));
			if ((scale_x) == (0)) scale_x = (float)(scale_y);
			if ((scale_y) == (0)) {
if ((scale_x) == (0)) return (null);scale_y = (float)(scale_x);}

			stbtt_GetGlyphBitmapBoxSubpixel(info, (int)(glyph), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), &ix0, &iy0, &ix1, &iy1);
			gbm.w = (int)(ix1 - ix0);
			gbm.h = (int)(iy1 - iy0);
			gbm.pixels = (null);
			if ((width) != null) *width = (int)(gbm.w);
			if ((height) != null) *height = (int)(gbm.h);
			if ((xoff) != null) *xoff = (int)(ix0);
			if ((yoff) != null) *yoff = (int)(iy0);
			if (((gbm.w) != 0) && ((gbm.h) != 0)) {
gbm.pixels = (byte*)(fons__tmpalloc((ulong)(gbm.w * gbm.h), info->userdata));if ((gbm.pixels) != null) {
gbm.stride = (int)(gbm.w);stbtt_Rasterize(&gbm, (float)(0.35f), vertices, (int)(num_verts), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(ix0), (int)(iy0), (int)(1), info->userdata);}
}

			fons__tmpfree(vertices, info->userdata);
			return gbm.pixels;
		}

		public static byte* stbtt_GetGlyphBitmap(stbtt_fontinfo* info, float scale_x, float scale_y, int glyph, int* width, int* height, int* xoff, int* yoff)
		{
			return stbtt_GetGlyphBitmapSubpixel(info, (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), (int)(glyph), width, height, xoff, yoff);
		}

		public static void stbtt_MakeGlyphBitmapSubpixel(stbtt_fontinfo* info, byte* output, int out_w, int out_h, int out_stride, float scale_x, float scale_y, float shift_x, float shift_y, int glyph)
		{
			int ix0 = 0;int iy0 = 0;
			stbtt_vertex* vertices;
			int num_verts = (int)(stbtt_GetGlyphShape(info, (int)(glyph), &vertices));
			stbtt__bitmap gbm =  new stbtt__bitmap();
			stbtt_GetGlyphBitmapBoxSubpixel(info, (int)(glyph), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), &ix0, &iy0, null, null);
			gbm.pixels = output;
			gbm.w = (int)(out_w);
			gbm.h = (int)(out_h);
			gbm.stride = (int)(out_stride);
			if (((gbm.w) != 0) && ((gbm.h) != 0)) stbtt_Rasterize(&gbm, (float)(0.35f), vertices, (int)(num_verts), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(ix0), (int)(iy0), (int)(1), info->userdata);
			fons__tmpfree(vertices, info->userdata);
		}

		public static void stbtt_MakeGlyphBitmap(stbtt_fontinfo* info, byte* output, int out_w, int out_h, int out_stride, float scale_x, float scale_y, int glyph)
		{
			stbtt_MakeGlyphBitmapSubpixel(info, output, (int)(out_w), (int)(out_h), (int)(out_stride), (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), (int)(glyph));
		}

		public static byte* stbtt_GetCodepointBitmapSubpixel(stbtt_fontinfo* info, float scale_x, float scale_y, float shift_x, float shift_y, int codepoint, int* width, int* height, int* xoff, int* yoff)
		{
			return stbtt_GetGlyphBitmapSubpixel(info, (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(stbtt_FindGlyphIndex(info, (int)(codepoint))), width, height, xoff, yoff);
		}

		public static void stbtt_MakeCodepointBitmapSubpixel(stbtt_fontinfo* info, byte* output, int out_w, int out_h, int out_stride, float scale_x, float scale_y, float shift_x, float shift_y, int codepoint)
		{
			stbtt_MakeGlyphBitmapSubpixel(info, output, (int)(out_w), (int)(out_h), (int)(out_stride), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(stbtt_FindGlyphIndex(info, (int)(codepoint))));
		}

		public static byte* stbtt_GetCodepointBitmap(stbtt_fontinfo* info, float scale_x, float scale_y, int codepoint, int* width, int* height, int* xoff, int* yoff)
		{
			return stbtt_GetCodepointBitmapSubpixel(info, (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), (int)(codepoint), width, height, xoff, yoff);
		}

		public static void stbtt_MakeCodepointBitmap(stbtt_fontinfo* info, byte* output, int out_w, int out_h, int out_stride, float scale_x, float scale_y, int codepoint)
		{
			stbtt_MakeCodepointBitmapSubpixel(info, output, (int)(out_w), (int)(out_h), (int)(out_stride), (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), (int)(codepoint));
		}

		public static int stbtt_BakeFontBitmap(byte* data, int offset, float pixel_height, byte* pixels, int pw, int ph, int first_char, int num_chars, stbtt_bakedchar* chardata)
		{
			float scale = 0;
			int x = 0;int y = 0;int bottom_y = 0;int i = 0;
			stbtt_fontinfo f =  new stbtt_fontinfo();
			f.userdata = (null);
			if (stbtt_InitFont(&f, data, (int)(offset))== 0) return (int)(-1);
			CRuntime.memset(pixels, (int)(0), (ulong)(pw * ph));
			x = (int)(y = (int)(1));
			bottom_y = (int)(1);
			scale = (float)(stbtt_ScaleForPixelHeight(&f, (float)(pixel_height)));
			for (i = (int)(0); (i) < (num_chars); ++i) {
int advance = 0;int lsb = 0;int x0 = 0;int y0 = 0;int x1 = 0;int y1 = 0;int gw = 0;int gh = 0;int g = (int)(stbtt_FindGlyphIndex(&f, (int)(first_char + i)));stbtt_GetGlyphHMetrics(&f, (int)(g), &advance, &lsb);stbtt_GetGlyphBitmapBox(&f, (int)(g), (float)(scale), (float)(scale), &x0, &y0, &x1, &y1);gw = (int)(x1 - x0);gh = (int)(y1 - y0);if ((x + gw + 1) >= (pw)) y = (int)(bottom_y) , x = (int)(1);if ((y + gh + 1) >= (ph)) return (int)(-i);(void)((!!((x + gw) < (pw))) || (_wassert("x+gw < pw", "nanovg/stb_truetype.h", (uint)(2545)) , 0));(void)((!!((y + gh) < (ph))) || (_wassert("y+gh < ph", "nanovg/stb_truetype.h", (uint)(2546)) , 0));stbtt_MakeGlyphBitmap(&f, pixels + x + y * pw, (int)(gw), (int)(gh), (int)(pw), (float)(scale), (float)(scale), (int)(g));chardata[i].x0 = (ushort)((short)(x));chardata[i].y0 = (ushort)((short)(y));chardata[i].x1 = (ushort)((short)(x + gw));chardata[i].y1 = (ushort)((short)(y + gh));chardata[i].xadvance = (float)(scale * advance);chardata[i].xoff = ((float)(x0));chardata[i].yoff = ((float)(y0));x = (int)(x + gw + 1);if ((y + gh + 1) > (bottom_y)) bottom_y = (int)(y + gh + 1);}
			return (int)(bottom_y);
		}

		public static void stbtt_GetBakedQuad(stbtt_bakedchar* chardata, int pw, int ph, int char_index, float* xpos, float* ypos, stbtt_aligned_quad* q, int opengl_fillrule)
		{
			float d3d_bias = (float)((opengl_fillrule) != 0?0:-0.5f);
			float ipw = (float)(1.0f / pw);float iph = (float)(1.0f / ph);
			stbtt_bakedchar* b = chardata + char_index;
			int round_x = ((int)(CRuntime.floor((double)((*xpos + b->xoff) + 0.5f))));
			int round_y = ((int)(CRuntime.floor((double)((*ypos + b->yoff) + 0.5f))));
			q->x0 = (float)(round_x + d3d_bias);
			q->y0 = (float)(round_y + d3d_bias);
			q->x1 = (float)(round_x + b->x1 - b->x0 + d3d_bias);
			q->y1 = (float)(round_y + b->y1 - b->y0 + d3d_bias);
			q->s0 = (float)(b->x0 * ipw);
			q->t0 = (float)(b->y0 * iph);
			q->s1 = (float)(b->x1 * ipw);
			q->t1 = (float)(b->y1 * iph);
			*xpos += (float)(b->xadvance);
		}

		public static void stbrp_init_target(stbrp_context* con, int pw, int ph, stbrp_node* nodes, int num_nodes)
		{
			con->width = (int)(pw);
			con->height = (int)(ph);
			con->x = (int)(0);
			con->y = (int)(0);
			con->bottom_y = (int)(0);
			(void)(nodes);
			(void)(num_nodes);
		}

		public static void stbrp_pack_rects(stbrp_context* con, stbrp_rect* rects, int num_rects)
		{
			int i = 0;
			for (i = (int)(0); (i) < (num_rects); ++i) {
if ((con->x + rects[i].w) > (con->width)) {
con->x = (int)(0);con->y = (int)(con->bottom_y);}
if ((con->y + rects[i].h) > (con->height)) break;rects[i].x = (int)(con->x);rects[i].y = (int)(con->y);rects[i].was_packed = (int)(1);con->x += (int)(rects[i].w);if ((con->y + rects[i].h) > (con->bottom_y)) con->bottom_y = (int)(con->y + rects[i].h);}
			for (; (i) < (num_rects); ++i) {rects[i].was_packed = (int)(0);}
		}

		public static int stbtt_PackBegin(stbtt_pack_context* spc, byte* pixels, int pw, int ph, int stride_in_bytes, int padding, void * alloc_context)
		{
			stbrp_context* context = (stbrp_context*)(fons__tmpalloc((ulong)(sizeof((*context))), alloc_context));
			int num_nodes = (int)(pw - padding);
			stbrp_node* nodes = (stbrp_node*)(fons__tmpalloc((ulong)(sizeof((*nodes)) * num_nodes), alloc_context));
			if (((context) == (null)) || ((nodes) == (null))) {
if (context != (null)) fons__tmpfree(context, alloc_context);if (nodes != (null)) fons__tmpfree(nodes, alloc_context);return (int)(0);}

			spc->user_allocator_context = alloc_context;
			spc->width = (int)(pw);
			spc->height = (int)(ph);
			spc->pixels = pixels;
			spc->pack_info = context;
			spc->nodes = nodes;
			spc->padding = (int)(padding);
			spc->stride_in_bytes = (int)(stride_in_bytes != 0?stride_in_bytes:pw);
			spc->h_oversample = (uint)(1);
			spc->v_oversample = (uint)(1);
			stbrp_init_target(context, (int)(pw - padding), (int)(ph - padding), nodes, (int)(num_nodes));
			if ((pixels) != null) CRuntime.memset(pixels, (int)(0), (ulong)(pw * ph));
			return (int)(1);
		}

		public static void stbtt_PackEnd(stbtt_pack_context* spc)
		{
			fons__tmpfree(spc->nodes, spc->user_allocator_context);
			fons__tmpfree(spc->pack_info, spc->user_allocator_context);
		}

		public static void stbtt_PackSetOversampling(stbtt_pack_context* spc, uint h_oversample, uint v_oversample)
		{
			(void)((!!(h_oversample <= 8)) || (_wassert("h_oversample <= 8", "nanovg/stb_truetype.h", (uint)(2704)) , 0));
			(void)((!!(v_oversample <= 8)) || (_wassert("v_oversample <= 8", "nanovg/stb_truetype.h", (uint)(2705)) , 0));
			if (h_oversample <= 8) spc->h_oversample = (uint)(h_oversample);
			if (v_oversample <= 8) spc->v_oversample = (uint)(v_oversample);
		}

		public static void stbtt__h_prefilter(byte* pixels, int w, int h, int stride_in_bytes, uint kernel_width)
		{
			byte* buffer = stackalloc byte[8];
			int safe_w = (int)(w - kernel_width);
			int j = 0;
			CRuntime.memset(buffer, (int)(0), (ulong)(8));
			for (j = (int)(0); (j) < (h); ++j) {
int i = 0;uint total = 0;CRuntime.memset(buffer, (int)(0), (ulong)(kernel_width));total = (uint)(0);switch (kernel_width){
case 2:for (i = (int)(0); i <= safe_w; ++i) {
total += (uint)(pixels[i] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i]);pixels[i] = ((byte)(total / 2));}break;case 3:for (i = (int)(0); i <= safe_w; ++i) {
total += (uint)(pixels[i] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i]);pixels[i] = ((byte)(total / 3));}break;case 4:for (i = (int)(0); i <= safe_w; ++i) {
total += (uint)(pixels[i] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i]);pixels[i] = ((byte)(total / 4));}break;case 5:for (i = (int)(0); i <= safe_w; ++i) {
total += (uint)(pixels[i] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i]);pixels[i] = ((byte)(total / 5));}break;default: for (i = (int)(0); i <= safe_w; ++i) {
total += (uint)(pixels[i] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i]);pixels[i] = ((byte)(total / kernel_width));}break;}
for (; (i) < (w); ++i) {
(void)((!!((pixels[i]) == (0))) || (_wassert("pixels[i] == 0", "nanovg/stb_truetype.h", (uint)(2767)) , 0));total -= (uint)(buffer[i & (8 - 1)]);pixels[i] = ((byte)(total / kernel_width));}pixels += stride_in_bytes;}
		}

		public static void stbtt__v_prefilter(byte* pixels, int w, int h, int stride_in_bytes, uint kernel_width)
		{
			byte* buffer = stackalloc byte[8];
			int safe_h = (int)(h - kernel_width);
			int j = 0;
			CRuntime.memset(buffer, (int)(0), (ulong)(8));
			for (j = (int)(0); (j) < (w); ++j) {
int i = 0;uint total = 0;CRuntime.memset(buffer, (int)(0), (ulong)(kernel_width));total = (uint)(0);switch (kernel_width){
case 2:for (i = (int)(0); i <= safe_h; ++i) {
total += (uint)(pixels[i * stride_in_bytes] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i * stride_in_bytes]);pixels[i * stride_in_bytes] = ((byte)(total / 2));}break;case 3:for (i = (int)(0); i <= safe_h; ++i) {
total += (uint)(pixels[i * stride_in_bytes] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i * stride_in_bytes]);pixels[i * stride_in_bytes] = ((byte)(total / 3));}break;case 4:for (i = (int)(0); i <= safe_h; ++i) {
total += (uint)(pixels[i * stride_in_bytes] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i * stride_in_bytes]);pixels[i * stride_in_bytes] = ((byte)(total / 4));}break;case 5:for (i = (int)(0); i <= safe_h; ++i) {
total += (uint)(pixels[i * stride_in_bytes] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i * stride_in_bytes]);pixels[i * stride_in_bytes] = ((byte)(total / 5));}break;default: for (i = (int)(0); i <= safe_h; ++i) {
total += (uint)(pixels[i * stride_in_bytes] - buffer[i & (8 - 1)]);buffer[(i + kernel_width) & (8 - 1)] = (byte)(pixels[i * stride_in_bytes]);pixels[i * stride_in_bytes] = ((byte)(total / kernel_width));}break;}
for (; (i) < (h); ++i) {
(void)((!!((pixels[i * stride_in_bytes]) == (0))) || (_wassert("pixels[i*stride_in_bytes] == 0", "nanovg/stb_truetype.h", (uint)(2829)) , 0));total -= (uint)(buffer[i & (8 - 1)]);pixels[i * stride_in_bytes] = ((byte)(total / kernel_width));}pixels += 1;}
		}

		public static float stbtt__oversample_shift(int oversample)
		{
			if (oversample== 0) return (float)(0.0f);
			return (float)((float)(-(oversample - 1)) / (2.0f * (float)(oversample)));
		}

		public static int stbtt_PackFontRangesGatherRects(stbtt_pack_context* spc, stbtt_fontinfo* info, stbtt_pack_range* ranges, int num_ranges, stbrp_rect* rects)
		{
			int i = 0;int j = 0;int k = 0;
			k = (int)(0);
			for (i = (int)(0); (i) < (num_ranges); ++i) {
float fh = (float)(ranges[i].font_size);float scale = (float)((fh) > (0)?stbtt_ScaleForPixelHeight(info, (float)(fh)):stbtt_ScaleForMappingEmToPixels(info, (float)(-fh)));ranges[i].h_oversample = ((byte)(spc->h_oversample));ranges[i].v_oversample = ((byte)(spc->v_oversample));for (j = (int)(0); (j) < (ranges[i].num_chars); ++j) {
int x0 = 0;int y0 = 0;int x1 = 0;int y1 = 0;int codepoint = (int)((ranges[i].array_of_unicode_codepoints) == (null)?ranges[i].first_unicode_codepoint_in_range + j:ranges[i].array_of_unicode_codepoints[j]);int glyph = (int)(stbtt_FindGlyphIndex(info, (int)(codepoint)));stbtt_GetGlyphBitmapBoxSubpixel(info, (int)(glyph), (float)(scale * spc->h_oversample), (float)(scale * spc->v_oversample), (float)(0), (float)(0), &x0, &y0, &x1, &y1);rects[k].w = ((int)(x1 - x0 + spc->padding + spc->h_oversample - 1));rects[k].h = ((int)(y1 - y0 + spc->padding + spc->v_oversample - 1));++k;}}
			return (int)(k);
		}

		public static int stbtt_PackFontRangesRenderIntoRects(stbtt_pack_context* spc, stbtt_fontinfo* info, stbtt_pack_range* ranges, int num_ranges, stbrp_rect* rects)
		{
			int i = 0;int j = 0;int k = 0;int return_value = (int)(1);
			int old_h_over = (int)(spc->h_oversample);
			int old_v_over = (int)(spc->v_oversample);
			k = (int)(0);
			for (i = (int)(0); (i) < (num_ranges); ++i) {
float fh = (float)(ranges[i].font_size);float scale = (float)((fh) > (0)?stbtt_ScaleForPixelHeight(info, (float)(fh)):stbtt_ScaleForMappingEmToPixels(info, (float)(-fh)));float recip_h = 0;float recip_v = 0;float sub_x = 0;float sub_y = 0;spc->h_oversample = (uint)(ranges[i].h_oversample);spc->v_oversample = (uint)(ranges[i].v_oversample);recip_h = (float)(1.0f / spc->h_oversample);recip_v = (float)(1.0f / spc->v_oversample);sub_x = (float)(stbtt__oversample_shift((int)(spc->h_oversample)));sub_y = (float)(stbtt__oversample_shift((int)(spc->v_oversample)));for (j = (int)(0); (j) < (ranges[i].num_chars); ++j) {
stbrp_rect* r = &rects[k];if ((r->was_packed) != 0) {
stbtt_packedchar* bc = &ranges[i].chardata_for_range[j];int advance = 0;int lsb = 0;int x0 = 0;int y0 = 0;int x1 = 0;int y1 = 0;int codepoint = (int)((ranges[i].array_of_unicode_codepoints) == (null)?ranges[i].first_unicode_codepoint_in_range + j:ranges[i].array_of_unicode_codepoints[j]);int glyph = (int)(stbtt_FindGlyphIndex(info, (int)(codepoint)));int pad = (int)(spc->padding);r->x += (int)(pad);r->y += (int)(pad);r->w -= (int)(pad);r->h -= (int)(pad);stbtt_GetGlyphHMetrics(info, (int)(glyph), &advance, &lsb);stbtt_GetGlyphBitmapBox(info, (int)(glyph), (float)(scale * spc->h_oversample), (float)(scale * spc->v_oversample), &x0, &y0, &x1, &y1);stbtt_MakeGlyphBitmapSubpixel(info, spc->pixels + r->x + r->y * spc->stride_in_bytes, (int)(r->w - spc->h_oversample + 1), (int)(r->h - spc->v_oversample + 1), (int)(spc->stride_in_bytes), (float)(scale * spc->h_oversample), (float)(scale * spc->v_oversample), (float)(0), (float)(0), (int)(glyph));if ((spc->h_oversample) > (1)) stbtt__h_prefilter(spc->pixels + r->x + r->y * spc->stride_in_bytes, (int)(r->w), (int)(r->h), (int)(spc->stride_in_bytes), (uint)(spc->h_oversample));if ((spc->v_oversample) > (1)) stbtt__v_prefilter(spc->pixels + r->x + r->y * spc->stride_in_bytes, (int)(r->w), (int)(r->h), (int)(spc->stride_in_bytes), (uint)(spc->v_oversample));bc->x0 = (ushort)((short)(r->x));bc->y0 = (ushort)((short)(r->y));bc->x1 = (ushort)((short)(r->x + r->w));bc->y1 = (ushort)((short)(r->y + r->h));bc->xadvance = (float)(scale * advance);bc->xoff = (float)((float)(x0) * recip_h + sub_x);bc->yoff = (float)((float)(y0) * recip_v + sub_y);bc->xoff2 = (float)((x0 + r->w) * recip_h + sub_x);bc->yoff2 = (float)((y0 + r->h) * recip_v + sub_y);}
 else {
return_value = (int)(0);}
++k;}}
			spc->h_oversample = (uint)(old_h_over);
			spc->v_oversample = (uint)(old_v_over);
			return (int)(return_value);
		}

		public static void stbtt_PackFontRangesPackRects(stbtt_pack_context* spc, stbrp_rect* rects, int num_rects)
		{
			stbrp_pack_rects((stbrp_context*)(spc->pack_info), rects, (int)(num_rects));
		}

		public static int stbtt_PackFontRanges(stbtt_pack_context* spc, byte* fontdata, int font_index, stbtt_pack_range* ranges, int num_ranges)
		{
			stbtt_fontinfo info =  new stbtt_fontinfo();
			int i = 0;int j = 0;int n = 0;int return_value = (int)(1);
			stbrp_rect* rects;
			for (i = (int)(0); (i) < (num_ranges); ++i) {for (j = (int)(0); (j) < (ranges[i].num_chars); ++j) {ranges[i].chardata_for_range[j].x0 = (ushort)(ranges[i].chardata_for_range[j].y0 = (ushort)(ranges[i].chardata_for_range[j].x1 = (ushort)(ranges[i].chardata_for_range[j].y1 = (ushort)(0))));}}
			n = (int)(0);
			for (i = (int)(0); (i) < (num_ranges); ++i) {n += (int)(ranges[i].num_chars);}
			rects = (stbrp_rect*)(fons__tmpalloc((ulong)(sizeof((*rects)) * n), spc->user_allocator_context));
			if ((rects) == (null)) return (int)(0);
			info.userdata = spc->user_allocator_context;
			stbtt_InitFont(&info, fontdata, (int)(stbtt_GetFontOffsetForIndex(fontdata, (int)(font_index))));
			n = (int)(stbtt_PackFontRangesGatherRects(spc, &info, ranges, (int)(num_ranges), rects));
			stbtt_PackFontRangesPackRects(spc, rects, (int)(n));
			return_value = (int)(stbtt_PackFontRangesRenderIntoRects(spc, &info, ranges, (int)(num_ranges), rects));
			fons__tmpfree(rects, spc->user_allocator_context);
			return (int)(return_value);
		}

		public static int stbtt_PackFontRange(stbtt_pack_context* spc, byte* fontdata, int font_index, float font_size, int first_unicode_codepoint_in_range, int num_chars_in_range, stbtt_packedchar* chardata_for_range)
		{
			stbtt_pack_range range =  new stbtt_pack_range();
			range.first_unicode_codepoint_in_range = (int)(first_unicode_codepoint_in_range);
			range.array_of_unicode_codepoints = (null);
			range.num_chars = (int)(num_chars_in_range);
			range.chardata_for_range = chardata_for_range;
			range.font_size = (float)(font_size);
			return (int)(stbtt_PackFontRanges(spc, fontdata, (int)(font_index), &range, (int)(1)));
		}

		public static void stbtt_GetPackedQuad(stbtt_packedchar* chardata, int pw, int ph, int char_index, float* xpos, float* ypos, stbtt_aligned_quad* q, int align_to_integer)
		{
			float ipw = (float)(1.0f / pw);float iph = (float)(1.0f / ph);
			stbtt_packedchar* b = chardata + char_index;
			if ((align_to_integer) != 0) {
float x = (float)((int)(CRuntime.floor((double)((*xpos + b->xoff) + 0.5f))));float y = (float)((int)(CRuntime.floor((double)((*ypos + b->yoff) + 0.5f))));q->x0 = (float)(x);q->y0 = (float)(y);q->x1 = (float)(x + b->xoff2 - b->xoff);q->y1 = (float)(y + b->yoff2 - b->yoff);}
 else {
q->x0 = (float)(*xpos + b->xoff);q->y0 = (float)(*ypos + b->yoff);q->x1 = (float)(*xpos + b->xoff2);q->y1 = (float)(*ypos + b->yoff2);}

			q->s0 = (float)(b->x0 * ipw);
			q->t0 = (float)(b->y0 * iph);
			q->s1 = (float)(b->x1 * ipw);
			q->t1 = (float)(b->y1 * iph);
			*xpos += (float)(b->xadvance);
		}

		public static int stbtt__CompareUTF8toUTF16_bigendian_prefix(byte* s1, int len1, byte* s2, int len2)
		{
			int i = (int)(0);
			while ((len2) != 0) {
ushort ch = (ushort)(s2[0] * 256 + s2[1]);if ((ch) < (0x80)) {
if ((i) >= (len1)) return (int)(-1);if (s1[i++] != ch) return (int)(-1);}
 else if ((ch) < (0x800)) {
if ((i + 1) >= (len1)) return (int)(-1);if (s1[i++] != 0xc0 + (ch >> 6)) return (int)(-1);if (s1[i++] != 0x80 + (ch & 0x3f)) return (int)(-1);}
 else if (((ch) >= (0xd800)) && ((ch) < (0xdc00))) {
uint c = 0;ushort ch2 = (ushort)(s2[2] * 256 + s2[3]);if ((i + 3) >= (len1)) return (int)(-1);c = (uint)(((ch - 0xd800) << 10) + (ch2 - 0xdc00) + 0x10000);if (s1[i++] != 0xf0 + (c >> 18)) return (int)(-1);if (s1[i++] != 0x80 + ((c >> 12) & 0x3f)) return (int)(-1);if (s1[i++] != 0x80 + ((c >> 6) & 0x3f)) return (int)(-1);if (s1[i++] != 0x80 + ((c) & 0x3f)) return (int)(-1);s2 += 2;len2 -= (int)(2);}
 else if (((ch) >= (0xdc00)) && ((ch) < (0xe000))) {
return (int)(-1);}
 else {
if ((i + 2) >= (len1)) return (int)(-1);if (s1[i++] != 0xe0 + (ch >> 12)) return (int)(-1);if (s1[i++] != 0x80 + ((ch >> 6) & 0x3f)) return (int)(-1);if (s1[i++] != 0x80 + ((ch) & 0x3f)) return (int)(-1);}
s2 += 2;len2 -= (int)(2);}
			return (int)(i);
		}

		public static int stbtt_CompareUTF8toUTF16_bigendian(sbyte* s1, int len1, sbyte* s2, int len2)
		{
			return (int)((len1) == (stbtt__CompareUTF8toUTF16_bigendian_prefix((byte*)(s1), (int)(len1), (byte*)(s2), (int)(len2)))?1:0);
		}

		public static sbyte* stbtt_GetFontNameString(stbtt_fontinfo* font, int* length, int platformID, int encodingID, int languageID, int nameID)
		{
			int i = 0;int count = 0;int stringOffset = 0;
			byte* fc = font->data;
			uint offset = (uint)(font->fontstart);
			uint nm = (uint)(stbtt__find_table(fc, (uint)(offset), "name"));
			if (nm== 0) return (null);
			count = (int)(ttUSHORT(fc + nm + 2));
			stringOffset = (int)(nm + ttUSHORT(fc + nm + 4));
			for (i = (int)(0); (i) < (count); ++i) {
uint loc = (uint)(nm + 6 + 12 * i);if (((((platformID) == (ttUSHORT(fc + loc + 0))) && ((encodingID) == (ttUSHORT(fc + loc + 2)))) && ((languageID) == (ttUSHORT(fc + loc + 4)))) && ((nameID) == (ttUSHORT(fc + loc + 6)))) {
*length = (int)(ttUSHORT(fc + loc + 8));return (sbyte*)(fc + stringOffset + ttUSHORT(fc + loc + 10));}
}
			return (null);
		}

		public static int stbtt__matchpair(byte* fc, uint nm, byte* name, int nlen, int target_id, int next_id)
		{
			int i = 0;
			int count = (int)(ttUSHORT(fc + nm + 2));
			int stringOffset = (int)(nm + ttUSHORT(fc + nm + 4));
			for (i = (int)(0); (i) < (count); ++i) {
uint loc = (uint)(nm + 6 + 12 * i);int id = (int)(ttUSHORT(fc + loc + 6));if ((id) == (target_id)) {
int platform = (int)(ttUSHORT(fc + loc + 0));int encoding = (int)(ttUSHORT(fc + loc + 2));int language = (int)(ttUSHORT(fc + loc + 4));if ((((platform) == (0)) || (((platform) == (3)) && ((encoding) == (1)))) || (((platform) == (3)) && ((encoding) == (10)))) {
int slen = (int)(ttUSHORT(fc + loc + 8));int off = (int)(ttUSHORT(fc + loc + 10));int matchlen = (int)(stbtt__CompareUTF8toUTF16_bigendian_prefix(name, (int)(nlen), fc + stringOffset + off, (int)(slen)));if ((matchlen) >= (0)) {
if ((((((i + 1) < (count)) && ((ttUSHORT(fc + loc + 12 + 6)) == (next_id))) && ((ttUSHORT(fc + loc + 12)) == (platform))) && ((ttUSHORT(fc + loc + 12 + 2)) == (encoding))) && ((ttUSHORT(fc + loc + 12 + 4)) == (language))) {
slen = (int)(ttUSHORT(fc + loc + 12 + 8));off = (int)(ttUSHORT(fc + loc + 12 + 10));if ((slen) == (0)) {
if ((matchlen) == (nlen)) return (int)(1);}
 else if (((matchlen) < (nlen)) && ((name[matchlen]) == (' '))) {
++matchlen;if ((stbtt_CompareUTF8toUTF16_bigendian((sbyte*)(name + matchlen), (int)(nlen - matchlen), (sbyte*)(fc + stringOffset + off), (int)(slen))) != 0) return (int)(1);}
}
 else {
if ((matchlen) == (nlen)) return (int)(1);}
}
}
}
}
			return (int)(0);
		}

		public static int stbtt__matches(byte* fc, uint offset, byte* name, int flags)
		{
			int nlen = (int)(CRuntime.strlen((sbyte*)(name)));
			uint nm = 0;uint hd = 0;
			if (stbtt__isfont(fc + offset)== 0) return (int)(0);
			if ((flags) != 0) {
hd = (uint)(stbtt__find_table(fc, (uint)(offset), "head"));if ((ttUSHORT(fc + hd + 44) & 7) != (flags & 7)) return (int)(0);}

			nm = (uint)(stbtt__find_table(fc, (uint)(offset), "name"));
			if (nm== 0) return (int)(0);
			if ((flags) != 0) {
if ((stbtt__matchpair(fc, (uint)(nm), name, (int)(nlen), (int)(16), (int)(-1))) != 0) return (int)(1);if ((stbtt__matchpair(fc, (uint)(nm), name, (int)(nlen), (int)(1), (int)(-1))) != 0) return (int)(1);if ((stbtt__matchpair(fc, (uint)(nm), name, (int)(nlen), (int)(3), (int)(-1))) != 0) return (int)(1);}
 else {
if ((stbtt__matchpair(fc, (uint)(nm), name, (int)(nlen), (int)(16), (int)(17))) != 0) return (int)(1);if ((stbtt__matchpair(fc, (uint)(nm), name, (int)(nlen), (int)(1), (int)(2))) != 0) return (int)(1);if ((stbtt__matchpair(fc, (uint)(nm), name, (int)(nlen), (int)(3), (int)(-1))) != 0) return (int)(1);}

			return (int)(0);
		}

		public static int stbtt_FindMatchingFont(byte* font_collection, sbyte* name_utf8, int flags)
		{
			int i = 0;
			for (i = (int)(0); ; ++i) {
int off = (int)(stbtt_GetFontOffsetForIndex(font_collection, (int)(i)));if ((off) < (0)) return (int)(off);if ((stbtt__matches(font_collection, (uint)(off), (byte*)(name_utf8), (int)(flags))) != 0) return (int)(off);}
		}

		public static int fons__tt_init(FONScontext* context)
		{
			(void)(sizeof((context)));
			return (int)(1);
		}

		public static int fons__tt_done(FONScontext* context)
		{
			(void)(sizeof((context)));
			return (int)(1);
		}

		public static int fons__tt_loadFont(FONScontext* context, FONSttFontImpl* font, byte* data, int dataSize)
		{
			int stbError = 0;
			(void)(sizeof((dataSize)));
			font->font.userdata = context;
			stbError = (int)(stbtt_InitFont(&font->font, data, (int)(0)));
			return (int)(stbError);
		}

		public static void fons__tt_getFontVMetrics(FONSttFontImpl* font, int* ascent, int* descent, int* lineGap)
		{
			stbtt_GetFontVMetrics(&font->font, ascent, descent, lineGap);
		}

		public static float fons__tt_getPixelHeightScale(FONSttFontImpl* font, float size)
		{
			return (float)(stbtt_ScaleForPixelHeight(&font->font, (float)(size)));
		}

		public static int fons__tt_getGlyphIndex(FONSttFontImpl* font, int codepoint)
		{
			return (int)(stbtt_FindGlyphIndex(&font->font, (int)(codepoint)));
		}

		public static int fons__tt_buildGlyphBitmap(FONSttFontImpl* font, int glyph, float size, float scale, int* advance, int* lsb, int* x0, int* y0, int* x1, int* y1)
		{
			(void)(sizeof((size)));
			stbtt_GetGlyphHMetrics(&font->font, (int)(glyph), advance, lsb);
			stbtt_GetGlyphBitmapBox(&font->font, (int)(glyph), (float)(scale), (float)(scale), x0, y0, x1, y1);
			return (int)(1);
		}

		public static void fons__tt_renderGlyphBitmap(FONSttFontImpl* font, byte* output, int outWidth, int outHeight, int outStride, float scaleX, float scaleY, int glyph)
		{
			stbtt_MakeGlyphBitmap(&font->font, output, (int)(outWidth), (int)(outHeight), (int)(outStride), (float)(scaleX), (float)(scaleY), (int)(glyph));
		}

		public static int fons__tt_getGlyphKernAdvance(FONSttFontImpl* font, int glyph1, int glyph2)
		{
			return (int)(stbtt_GetGlyphKernAdvance(&font->font, (int)(glyph1), (int)(glyph2)));
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
			return (int)((a) < (b)?a:b);
		}

		public static int fons__maxi(int a, int b)
		{
			return (int)((a) > (b)?a:b);
		}

		public static void * fons__tmpalloc(ulong size, void * up)
		{
			byte* ptr;
			FONScontext* stash = (FONScontext*)(up);
			size = (ulong)((size + 0xf) & ~0xf);
			if ((stash->nscratch + (int)(size)) > (96000)) {
if ((stash->handleError) != null) stash->handleError(stash->errorUptr, (int)(FONS_SCRATCH_FULL), (int)(stash->nscratch + (int)(size)));return (null);}

			ptr = stash->scratch + stash->nscratch;
			stash->nscratch += ((int)(size));
			return ptr;
		}

		public static void fons__tmpfree(void * ptr, void * up)
		{
			(void)(ptr);
			(void)(up);
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
			*codep = (uint)((*state != 0)?(byte & 0x3fu) | (*codep << 6):(0xff >> type) & (byte));
			*state = (uint)(utf8d[256 + *state + type]);
			return (uint)(*state);
		}

		public static void fons__deleteAtlas(FONSatlas* atlas)
		{
			if ((atlas) == (null)) return;
			if (atlas->nodes != (null)) CRuntime.free(atlas->nodes);
			CRuntime.free(atlas);
		}

		public static FONSatlas* fons__allocAtlas(int w, int h, int nnodes)
		{
			FONSatlas* atlas = (null);
			atlas = (FONSatlas*)(CRuntime.malloc((ulong)(sizeof(FONSatlas))));
			if ((atlas) == (null)) goto error;
			CRuntime.memset(atlas, (int)(0), (ulong)(sizeof(FONSatlas)));
			atlas->width = (int)(w);
			atlas->height = (int)(h);
			atlas->nodes = (FONSatlasNode*)(CRuntime.malloc((ulong)(sizeof(FONSatlasNode) * nnodes)));
			if ((atlas->nodes) == (null)) goto error;
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
			return (null);
		}

		public static int fons__atlasInsertNode(FONSatlas* atlas, int idx, int x, int y, int w)
		{
			int i = 0;
			if ((atlas->nnodes + 1) > (atlas->cnodes)) {
atlas->cnodes = (int)((atlas->cnodes) == (0)?8:atlas->cnodes * 2);atlas->nodes = (FONSatlasNode*)(CRuntime.realloc(atlas->nodes, (ulong)(sizeof(FONSatlasNode) * atlas->cnodes)));if ((atlas->nodes) == (null)) return (int)(0);}

			for (i = (int)(atlas->nnodes); (i) > (idx); i--) {atlas->nodes[i] = (FONSatlasNode)(atlas->nodes[i - 1]);}
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
			for (i = (int)(idx); (i) < (atlas->nnodes - 1); i++) {atlas->nodes[i] = (FONSatlasNode)(atlas->nodes[i + 1]);}
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
int shrink = (int)(atlas->nodes[i - 1].x + atlas->nodes[i - 1].width - atlas->nodes[i].x);atlas->nodes[i].x += ((short)(shrink));atlas->nodes[i].width -= ((short)(shrink));if (atlas->nodes[i].width <= 0) {
fons__atlasRemoveNode(atlas, (int)(i));i--;}
 else {
break;}
}
 else {
break;}
}
			for (i = (int)(0); (i) < (atlas->nnodes - 1); i++) {
if ((atlas->nodes[i].y) == (atlas->nodes[i + 1].y)) {
atlas->nodes[i].width += (short)(atlas->nodes[i + 1].width);fons__atlasRemoveNode(atlas, (int)(i + 1));i--;}
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
if ((i) == (atlas->nnodes)) return (int)(-1);y = (int)(fons__maxi((int)(y), (int)(atlas->nodes[i].y)));if ((y + h) > (atlas->height)) return (int)(-1);spaceLeft -= (int)(atlas->nodes[i].width);++i;}
			return (int)(y);
		}

		public static int fons__atlasAddRect(FONSatlas* atlas, int rw, int rh, int* rx, int* ry)
		{
			int besth = (int)(atlas->height);int bestw = (int)(atlas->width);int besti = (int)(-1);
			int bestx = (int)(-1);int besty = (int)(-1);int i = 0;
			for (i = (int)(0); (i) < (atlas->nnodes); i++) {
int y = (int)(fons__atlasRectFits(atlas, (int)(i), (int)(rw), (int)(rh)));if (y != -1) {
if (((y + rh) < (besth)) || (((y + rh) == (besth)) && ((atlas->nodes[i].width) < (bestw)))) {
besti = (int)(i);bestw = (int)(atlas->nodes[i].width);besth = (int)(y + rh);bestx = (int)(atlas->nodes[i].x);besty = (int)(y);}
}
}
			if ((besti) == (-1)) return (int)(0);
			if ((fons__atlasAddSkylineLevel(atlas, (int)(besti), (int)(bestx), (int)(besty), (int)(rw), (int)(rh))) == (0)) return (int)(0);
			*rx = (int)(bestx);
			*ry = (int)(besty);
			return (int)(1);
		}

		public static void fons__addWhiteRect(FONScontext* stash, int w, int h)
		{
			int x = 0;int y = 0;int gx = 0;int gy = 0;
			byte* dst;
			if ((fons__atlasAddRect(stash->atlas, (int)(w), (int)(h), &gx, &gy)) == (0)) return;
			dst = &stash->texData[gx + gy * stash->params.width];
			for (y = (int)(0); (y) < (h); y++) {
for (x = (int)(0); (x) < (w); x++) {dst[x] = (byte)(0xff);}dst += stash->params.width;}
			stash->dirtyRect[0] = (int)(fons__mini((int)(stash->dirtyRect[0]), (int)(gx)));
			stash->dirtyRect[1] = (int)(fons__mini((int)(stash->dirtyRect[1]), (int)(gy)));
			stash->dirtyRect[2] = (int)(fons__maxi((int)(stash->dirtyRect[2]), (int)(gx + w)));
			stash->dirtyRect[3] = (int)(fons__maxi((int)(stash->dirtyRect[3]), (int)(gy + h)));
		}

		public static FONScontext* fonsCreateInternal(FONSparams* params)
		{
			FONScontext* stash = (null);
			stash = (FONScontext*)(CRuntime.malloc((ulong)(sizeof(FONScontext))));
			if ((stash) == (null)) goto error;
			CRuntime.memset(stash, (int)(0), (ulong)(sizeof(FONScontext)));
			stash->params = (FONSparams)(*params);
			stash->scratch = (byte*)(CRuntime.malloc((ulong)(96000)));
			if ((stash->scratch) == (null)) goto error;
			if (fons__tt_init(stash)== 0) goto error;
			if (stash->params.renderCreate != (null)) {
if ((stash->params.renderCreate(stash->params.userPtr, (int)(stash->params.width), (int)(stash->params.height))) == (0)) goto error;}

			stash->atlas = fons__allocAtlas((int)(stash->params.width), (int)(stash->params.height), (int)(256));
			if ((stash->atlas) == (null)) goto error;
			stash->fonts = (FONSfont**)(CRuntime.malloc((ulong)(sizeof(FONSfont) * 4)));
			if ((stash->fonts) == (null)) goto error;
			CRuntime.memset(stash->fonts, (int)(0), (ulong)(sizeof(FONSfont) * 4));
			stash->cfonts = (int)(4);
			stash->nfonts = (int)(0);
			stash->itw = (float)(1.0f / stash->params.width);
			stash->ith = (float)(1.0f / stash->params.height);
			stash->texData = (byte*)(CRuntime.malloc((ulong)(stash->params.width * stash->params.height)));
			if ((stash->texData) == (null)) goto error;
			CRuntime.memset(stash->texData, (int)(0), (ulong)(stash->params.width * stash->params.height));
			stash->dirtyRect[0] = (int)(stash->params.width);
			stash->dirtyRect[1] = (int)(stash->params.height);
			stash->dirtyRect[2] = (int)(0);
			stash->dirtyRect[3] = (int)(0);
			fons__addWhiteRect(stash, (int)(2), (int)(2));
			fonsPushState(stash);
			fonsClearState(stash);
			return stash;
			error:;
fonsDeleteInternal(stash);
			return (null);
		}

		public static FONSstate* fons__getState(FONScontext* stash)
		{
			return &stash->states[stash->nstates - 1];
		}

		public static int fonsAddFallbackFont(FONScontext* stash, int _base_, int fallback)
		{
			FONSfont* baseFont = stash->fonts[_base_];
			if ((baseFont->nfallbacks) < (20)) {
baseFont->fallbacks[baseFont->nfallbacks++] = (int)(fallback);return (int)(1);}

			return (int)(0);
		}

		public static void fonsSetSize(FONScontext* stash, float size)
		{
			fons__getState(stash)->size = (float)(size);
		}

		public static void fonsSetColor(FONScontext* stash, uint color)
		{
			fons__getState(stash)->color = (uint)(color);
		}

		public static void fonsSetSpacing(FONScontext* stash, float spacing)
		{
			fons__getState(stash)->spacing = (float)(spacing);
		}

		public static void fonsSetBlur(FONScontext* stash, float blur)
		{
			fons__getState(stash)->blur = (float)(blur);
		}

		public static void fonsSetAlign(FONScontext* stash, int align)
		{
			fons__getState(stash)->align = (int)(align);
		}

		public static void fonsSetFont(FONScontext* stash, int font)
		{
			fons__getState(stash)->font = (int)(font);
		}

		public static void fonsPushState(FONScontext* stash)
		{
			if ((stash->nstates) >= (20)) {
if ((stash->handleError) != null) stash->handleError(stash->errorUptr, (int)(FONS_STATES_OVERFLOW), (int)(0));return;}

			if ((stash->nstates) > (0)) CRuntime.memcpy(&stash->states[stash->nstates], &stash->states[stash->nstates - 1], (ulong)(sizeof(FONSstate)));
			stash->nstates++;
		}

		public static void fonsPopState(FONScontext* stash)
		{
			if (stash->nstates <= 1) {
if ((stash->handleError) != null) stash->handleError(stash->errorUptr, (int)(FONS_STATES_UNDERFLOW), (int)(0));return;}

			stash->nstates--;
		}

		public static void fonsClearState(FONScontext* stash)
		{
			FONSstate* state = fons__getState(stash);
			state->size = (float)(12.0f);
			state->color = (uint)(0xffffffff);
			state->font = (int)(0);
			state->blur = (float)(0);
			state->spacing = (float)(0);
			state->align = (int)(FONS_ALIGN_LEFT | FONS_ALIGN_BASELINE);
		}

		public static void fons__freeFont(FONSfont* font)
		{
			if ((font) == (null)) return;
			if ((font->glyphs) != null) CRuntime.free(font->glyphs);
			if (((font->freeData) != 0) && ((font->data) != null)) CRuntime.free(font->data);
			CRuntime.free(font);
		}

		public static int fons__allocFont(FONScontext* stash)
		{
			FONSfont* font = (null);
			if ((stash->nfonts + 1) > (stash->cfonts)) {
stash->cfonts = (int)((stash->cfonts) == (0)?8:stash->cfonts * 2);stash->fonts = (FONSfont**)(CRuntime.realloc(stash->fonts, (ulong)(sizeof(FONSfont) * stash->cfonts)));if ((stash->fonts) == (null)) return (int)(-1);}

			font = (FONSfont*)(CRuntime.malloc((ulong)(sizeof(FONSfont))));
			if ((font) == (null)) goto error;
			CRuntime.memset(font, (int)(0), (ulong)(sizeof(FONSfont)));
			font->glyphs = (FONSglyph*)(CRuntime.malloc((ulong)(sizeof(FONSglyph) * 256)));
			if ((font->glyphs) == (null)) goto error;
			font->cglyphs = (int)(256);
			font->nglyphs = (int)(0);
			stash->fonts[stash->nfonts++] = font;
			return (int)(stash->nfonts - 1);
			error:;
fons__freeFont(font);
			return (int)(-1);
		}

		public static int fonsAddFont(FONScontext* stash, sbyte* name, sbyte* path)
		{
			_iobuf* fp = null;
			int dataSize = (int)(0);
			ulong readed = 0;
			byte* data = (null);
			fp = fopen(path, "rb");
			if ((fp) == (null)) goto error;
			fseek(fp, (int)(0), (int)(2));
			dataSize = (int)(ftell(fp));
			fseek(fp, (int)(0), (int)(0));
			data = (byte*)(CRuntime.malloc((ulong)(dataSize)));
			if ((data) == (null)) goto error;
			readed = (ulong)(fread(data, (ulong)(1), (ulong)(dataSize), fp));
			fclose(fp);
			fp = null;
			if (readed != dataSize) goto error;
			return (int)(fonsAddFontMem(stash, name, data, (int)(dataSize), (int)(1)));
			error:;
if ((data) != null) CRuntime.free(data);
			if ((fp) != null) fclose(fp);
			return (int)(-1);
		}

		public static int fonsAddFontMem(FONScontext* stash, sbyte* name, byte* data, int dataSize, int freeData)
		{
			int i = 0;int ascent = 0;int descent = 0;int fh = 0;int lineGap = 0;
			FONSfont* font;
			int idx = (int)(fons__allocFont(stash));
			if ((idx) == (-1)) return (int)(-1);
			font = stash->fonts[idx];
			strncpy(font->name, name, (ulong)(sizeof((font->name))));
			font->name[sizeof((font->name)) - 1] = (sbyte)('');
			for (i = (int)(0); (i) < (256); ++i) {font->lut[i] = (int)(-1);}
			font->dataSize = (int)(dataSize);
			font->data = data;
			font->freeData = ((byte)(freeData));
			stash->nscratch = (int)(0);
			if (fons__tt_loadFont(stash, &font->font, data, (int)(dataSize))== 0) goto error;
			fons__tt_getFontVMetrics(&font->font, &ascent, &descent, &lineGap);
			fh = (int)(ascent - descent);
			font->ascender = (float)((float)(ascent) / (float)(fh));
			font->descender = (float)((float)(descent) / (float)(fh));
			font->lineh = (float)((float)(fh + lineGap) / (float)(fh));
			return (int)(idx);
			error:;
fons__freeFont(font);
			stash->nfonts--;
			return (int)(-1);
		}

		public static int fonsGetFontByName(FONScontext* s, sbyte* name)
		{
			int i = 0;
			for (i = (int)(0); (i) < (s->nfonts); i++) {
if ((strcmp(s->fonts[i]->name, name)) == (0)) return (int)(i);}
			return (int)(-1);
		}

		public static FONSglyph* fons__allocGlyph(FONSfont* font)
		{
			if ((font->nglyphs + 1) > (font->cglyphs)) {
font->cglyphs = (int)((font->cglyphs) == (0)?8:font->cglyphs * 2);font->glyphs = (FONSglyph*)(CRuntime.realloc(font->glyphs, (ulong)(sizeof(FONSglyph) * font->cglyphs)));if ((font->glyphs) == (null)) return (null);}

			font->nglyphs++;
			return &font->glyphs[font->nglyphs - 1];
		}

		public static void fons__blurCols(byte* dst, int w, int h, int dstStride, int alpha)
		{
			int x = 0;int y = 0;
			for (y = (int)(0); (y) < (h); y++) {
int z = (int)(0);for (x = (int)(1); (x) < (w); x++) {
z += (int)((alpha * (((int)(dst[x]) << 7) - z)) >> 16);dst[x] = ((byte)(z >> 7));}dst[w - 1] = (byte)(0);z = (int)(0);for (x = (int)(w - 2); (x) >= (0); x--) {
z += (int)((alpha * (((int)(dst[x]) << 7) - z)) >> 16);dst[x] = ((byte)(z >> 7));}dst[0] = (byte)(0);dst += dstStride;}
		}

		public static void fons__blurRows(byte* dst, int w, int h, int dstStride, int alpha)
		{
			int x = 0;int y = 0;
			for (x = (int)(0); (x) < (w); x++) {
int z = (int)(0);for (y = (int)(dstStride); (y) < (h * dstStride); y += (int)(dstStride)) {
z += (int)((alpha * (((int)(dst[y]) << 7) - z)) >> 16);dst[y] = ((byte)(z >> 7));}dst[(h - 1) * dstStride] = (byte)(0);z = (int)(0);for (y = (int)((h - 2) * dstStride); (y) >= (0); y -= (int)(dstStride)) {
z += (int)((alpha * (((int)(dst[y]) << 7) - z)) >> 16);dst[y] = ((byte)(z >> 7));}dst[0] = (byte)(0);dst++;}
		}

		public static void fons__blur(FONScontext* stash, byte* dst, int w, int h, int dstStride, int blur)
		{
			int alpha = 0;
			float sigma = 0;
			(void)(stash);
			if ((blur) < (1)) return;
			sigma = (float)((float)(blur) * 0.57735f);
			alpha = ((int)((1 << 16) * (1.0f - expf((float)(-2.3f / (sigma + 1.0f))))));
			fons__blurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurRows(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
			fons__blurCols(dst, (int)(w), (int)(h), (int)(dstStride), (int)(alpha));
		}

		public static FONSglyph* fons__getGlyph(FONScontext* stash, FONSfont* font, uint codepoint, short isize, short iblur, int bitmapOption)
		{
			int i = 0;int g = 0;int advance = 0;int lsb = 0;int x0 = 0;int y0 = 0;int x1 = 0;int y1 = 0;int gw = 0;int gh = 0;int gx = 0;int gy = 0;int x = 0;int y = 0;
			float scale = 0;
			FONSglyph* glyph = (null);
			uint h = 0;
			float size = (float)(isize / 10.0f);
			int pad = 0;int added = 0;
			byte* bdst;
			byte* dst;
			FONSfont* renderFont = font;
			if ((isize) < (2)) return (null);
			if ((iblur) > (20)) iblur = (short)(20);
			pad = (int)(iblur + 2);
			stash->nscratch = (int)(0);
			h = (uint)(fons__hashint((uint)(codepoint)) & (256 - 1));
			i = (int)(font->lut[h]);
			while (i != -1) {
if ((((font->glyphs[i].codepoint) == (codepoint)) && ((font->glyphs[i].size) == (isize))) && ((font->glyphs[i].blur) == (iblur))) {
glyph = &font->glyphs[i];if (((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL)) || (((glyph->x0) >= (0)) && ((glyph->y0) >= (0)))) {
return glyph;}
break;}
i = (int)(font->glyphs[i].next);}
			g = (int)(fons__tt_getGlyphIndex(&font->font, (int)(codepoint)));
			if ((g) == (0)) {
for (i = (int)(0); (i) < (font->nfallbacks); ++i) {
FONSfont* fallbackFont = stash->fonts[font->fallbacks[i]];int fallbackIndex = (int)(fons__tt_getGlyphIndex(&fallbackFont->font, (int)(codepoint)));if (fallbackIndex != 0) {
g = (int)(fallbackIndex);renderFont = fallbackFont;break;}
}}

			scale = (float)(fons__tt_getPixelHeightScale(&renderFont->font, (float)(size)));
			fons__tt_buildGlyphBitmap(&renderFont->font, (int)(g), (float)(size), (float)(scale), &advance, &lsb, &x0, &y0, &x1, &y1);
			gw = (int)(x1 - x0 + pad * 2);
			gh = (int)(y1 - y0 + pad * 2);
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_REQUIRED)) {
added = (int)(fons__atlasAddRect(stash->atlas, (int)(gw), (int)(gh), &gx, &gy));if (((added) == (0)) && (stash->handleError != (null))) {
stash->handleError(stash->errorUptr, (int)(FONS_ATLAS_FULL), (int)(0));added = (int)(fons__atlasAddRect(stash->atlas, (int)(gw), (int)(gh), &gx, &gy));}
if ((added) == (0)) return (null);}
 else {
gx = (int)(-1);gy = (int)(-1);}

			if ((glyph) == (null)) {
glyph = fons__allocGlyph(font);glyph->codepoint = (uint)(codepoint);glyph->size = (short)(isize);glyph->blur = (short)(iblur);glyph->next = (int)(0);glyph->next = (int)(font->lut[h]);font->lut[h] = (int)(font->nglyphs - 1);}

			glyph->index = (int)(g);
			glyph->x0 = ((short)(gx));
			glyph->y0 = ((short)(gy));
			glyph->x1 = ((short)(glyph->x0 + gw));
			glyph->y1 = ((short)(glyph->y0 + gh));
			glyph->xadv = ((short)(scale * advance * 10.0f));
			glyph->xoff = ((short)(x0 - pad));
			glyph->yoff = ((short)(y0 - pad));
			if ((bitmapOption) == (FONS_GLYPH_BITMAP_OPTIONAL)) {
return glyph;}

			dst = &stash->texData[(glyph->x0 + pad) + (glyph->y0 + pad) * stash->params.width];
			fons__tt_renderGlyphBitmap(&renderFont->font, dst, (int)(gw - pad * 2), (int)(gh - pad * 2), (int)(stash->params.width), (float)(scale), (float)(scale), (int)(g));
			dst = &stash->texData[glyph->x0 + glyph->y0 * stash->params.width];
			for (y = (int)(0); (y) < (gh); y++) {
dst[y * stash->params.width] = (byte)(0);dst[gw - 1 + y * stash->params.width] = (byte)(0);}
			for (x = (int)(0); (x) < (gw); x++) {
dst[x] = (byte)(0);dst[x + (gh - 1) * stash->params.width] = (byte)(0);}
			if ((iblur) > (0)) {
stash->nscratch = (int)(0);bdst = &stash->texData[glyph->x0 + glyph->y0 * stash->params.width];fons__blur(stash, bdst, (int)(gw), (int)(gh), (int)(stash->params.width), (int)(iblur));}

			stash->dirtyRect[0] = (int)(fons__mini((int)(stash->dirtyRect[0]), (int)(glyph->x0)));
			stash->dirtyRect[1] = (int)(fons__mini((int)(stash->dirtyRect[1]), (int)(glyph->y0)));
			stash->dirtyRect[2] = (int)(fons__maxi((int)(stash->dirtyRect[2]), (int)(glyph->x1)));
			stash->dirtyRect[3] = (int)(fons__maxi((int)(stash->dirtyRect[3]), (int)(glyph->y1)));
			return glyph;
		}

		public static void fons__getQuad(FONScontext* stash, FONSfont* font, int prevGlyphIndex, FONSglyph* glyph, float scale, float spacing, float* x, float* y, FONSquad* q)
		{
			float rx = 0;float ry = 0;float xoff = 0;float yoff = 0;float x0 = 0;float y0 = 0;float x1 = 0;float y1 = 0;
			if (prevGlyphIndex != -1) {
float adv = (float)(fons__tt_getGlyphKernAdvance(&font->font, (int)(prevGlyphIndex), (int)(glyph->index)) * scale);*x += (float)((int)(adv + spacing + 0.5f));}

			xoff = (float)((short)(glyph->xoff + 1));
			yoff = (float)((short)(glyph->yoff + 1));
			x0 = ((float)(glyph->x0 + 1));
			y0 = ((float)(glyph->y0 + 1));
			x1 = ((float)(glyph->x1 - 1));
			y1 = ((float)(glyph->y1 - 1));
			if ((stash->params.flags & FONS_ZERO_TOPLEFT) != 0) {
rx = ((float)((int)(*x + xoff)));ry = ((float)((int)(*y + yoff)));q->x0 = (float)(rx);q->y0 = (float)(ry);q->x1 = (float)(rx + x1 - x0);q->y1 = (float)(ry + y1 - y0);q->s0 = (float)(x0 * stash->itw);q->t0 = (float)(y0 * stash->ith);q->s1 = (float)(x1 * stash->itw);q->t1 = (float)(y1 * stash->ith);}
 else {
rx = ((float)((int)(*x + xoff)));ry = ((float)((int)(*y - yoff)));q->x0 = (float)(rx);q->y0 = (float)(ry);q->x1 = (float)(rx + x1 - x0);q->y1 = (float)(ry - y1 + y0);q->s0 = (float)(x0 * stash->itw);q->t0 = (float)(y0 * stash->ith);q->s1 = (float)(x1 * stash->itw);q->t1 = (float)(y1 * stash->ith);}

			*x += (float)((int)(glyph->xadv / 10.0f + 0.5f));
		}

		public static void fons__flush(FONScontext* stash)
		{
			if (((stash->dirtyRect[0]) < (stash->dirtyRect[2])) && ((stash->dirtyRect[1]) < (stash->dirtyRect[3]))) {
if (stash->params.renderUpdate != (null)) stash->params.renderUpdate(stash->params.userPtr, stash->dirtyRect, stash->texData);stash->dirtyRect[0] = (int)(stash->params.width);stash->dirtyRect[1] = (int)(stash->params.height);stash->dirtyRect[2] = (int)(0);stash->dirtyRect[3] = (int)(0);}

			if ((stash->nverts) > (0)) {
if (stash->params.renderDraw != (null)) stash->params.renderDraw(stash->params.userPtr, stash->verts, stash->tcoords, stash->colors, (int)(stash->nverts));stash->nverts = (int)(0);}

		}

		public static void fons__vertex(FONScontext* stash, float x, float y, float s, float t, uint c)
		{
			stash->verts[stash->nverts * 2 + 0] = (float)(x);
			stash->verts[stash->nverts * 2 + 1] = (float)(y);
			stash->tcoords[stash->nverts * 2 + 0] = (float)(s);
			stash->tcoords[stash->nverts * 2 + 1] = (float)(t);
			stash->colors[stash->nverts] = (uint)(c);
			stash->nverts++;
		}

		public static float fons__getVertAlign(FONScontext* stash, FONSfont* font, int align, short isize)
		{
			if ((stash->params.flags & FONS_ZERO_TOPLEFT) != 0) {
if ((align & FONS_ALIGN_TOP) != 0) {
return (float)(font->ascender * (float)(isize) / 10.0f);}
 else if ((align & FONS_ALIGN_MIDDLE) != 0) {
return (float)((font->ascender + font->descender) / 2.0f * (float)(isize) / 10.0f);}
 else if ((align & FONS_ALIGN_BASELINE) != 0) {
return (float)(0.0f);}
 else if ((align & FONS_ALIGN_BOTTOM) != 0) {
return (float)(font->descender * (float)(isize) / 10.0f);}
}
 else {
if ((align & FONS_ALIGN_TOP) != 0) {
return (float)(-font->ascender * (float)(isize) / 10.0f);}
 else if ((align & FONS_ALIGN_MIDDLE) != 0) {
return (float)(-(font->ascender + font->descender) / 2.0f * (float)(isize) / 10.0f);}
 else if ((align & FONS_ALIGN_BASELINE) != 0) {
return (float)(0.0f);}
 else if ((align & FONS_ALIGN_BOTTOM) != 0) {
return (float)(-font->descender * (float)(isize) / 10.0f);}
}

			return (float)(0.0);
		}

		public static float fonsDrawText(FONScontext* stash, float x, float y, sbyte* str, sbyte* end)
		{
			FONSstate* state = fons__getState(stash);
			uint codepoint = 0;
			uint utf8state = (uint)(0);
			FONSglyph* glyph = (null);
			FONSquad q =  new FONSquad();
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state->size * 10.0f);
			short iblur = (short)(state->blur);
			float scale = 0;
			FONSfont* font;
			float width = 0;
			if ((stash) == (null)) return (float)(x);
			if (((state->font) < (0)) || ((state->font) >= (stash->nfonts))) return (float)(x);
			font = stash->fonts[state->font];
			if ((font->data) == (null)) return (float)(x);
			scale = (float)(fons__tt_getPixelHeightScale(&font->font, (float)((float)(isize) / 10.0f)));
			if ((end) == (null)) end = str + CRuntime.strlen(str);
			if ((state->align & FONS_ALIGN_LEFT) != 0) {
}
 else if ((state->align & FONS_ALIGN_RIGHT) != 0) {
width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, (null)));x -= (float)(width);}
 else if ((state->align & FONS_ALIGN_CENTER) != 0) {
width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, (null)));x -= (float)(width * 0.5f);}

			y += (float)(fons__getVertAlign(stash, font, (int)(state->align), (short)(isize)));
			for (; str != end; ++str) {
if ((fons__decutf8(&utf8state, &codepoint, (uint)(*(byte*)(str)))) != 0) continue;glyph = fons__getGlyph(stash, font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_REQUIRED));if (glyph != (null)) {
fons__getQuad(stash, font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state->spacing), &x, &y, &q);if ((stash->nverts + 6) > (1024)) fons__flush(stash);fons__vertex(stash, (float)(q.x0), (float)(q.y0), (float)(q.s0), (float)(q.t0), (uint)(state->color));fons__vertex(stash, (float)(q.x1), (float)(q.y1), (float)(q.s1), (float)(q.t1), (uint)(state->color));fons__vertex(stash, (float)(q.x1), (float)(q.y0), (float)(q.s1), (float)(q.t0), (uint)(state->color));fons__vertex(stash, (float)(q.x0), (float)(q.y0), (float)(q.s0), (float)(q.t0), (uint)(state->color));fons__vertex(stash, (float)(q.x0), (float)(q.y1), (float)(q.s0), (float)(q.t1), (uint)(state->color));fons__vertex(stash, (float)(q.x1), (float)(q.y1), (float)(q.s1), (float)(q.t1), (uint)(state->color));}
prevGlyphIndex = (int)(glyph != (null)?glyph->index:-1);}
			fons__flush(stash);
			return (float)(x);
		}

		public static int fonsTextIterInit(FONScontext* stash, FONStextIter* iter, float x, float y, sbyte* str, sbyte* end, int bitmapOption)
		{
			FONSstate* state = fons__getState(stash);
			float width = 0;
			CRuntime.memset(iter, (int)(0), (ulong)(sizeof((*iter))));
			if ((stash) == (null)) return (int)(0);
			if (((state->font) < (0)) || ((state->font) >= (stash->nfonts))) return (int)(0);
			iter->font = stash->fonts[state->font];
			if ((iter->font->data) == (null)) return (int)(0);
			iter->isize = ((short)(state->size * 10.0f));
			iter->iblur = ((short)(state->blur));
			iter->scale = (float)(fons__tt_getPixelHeightScale(&iter->font->font, (float)((float)(iter->isize) / 10.0f)));
			if ((state->align & FONS_ALIGN_LEFT) != 0) {
}
 else if ((state->align & FONS_ALIGN_RIGHT) != 0) {
width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, (null)));x -= (float)(width);}
 else if ((state->align & FONS_ALIGN_CENTER) != 0) {
width = (float)(fonsTextBounds(stash, (float)(x), (float)(y), str, end, (null)));x -= (float)(width * 0.5f);}

			y += (float)(fons__getVertAlign(stash, iter->font, (int)(state->align), (short)(iter->isize)));
			if ((end) == (null)) end = str + CRuntime.strlen(str);
			iter->x = (float)(iter->nextx = (float)(x));
			iter->y = (float)(iter->nexty = (float)(y));
			iter->spacing = (float)(state->spacing);
			iter->str = str;
			iter->next = str;
			iter->end = end;
			iter->codepoint = (uint)(0);
			iter->prevGlyphIndex = (int)(-1);
			iter->bitmapOption = (int)(bitmapOption);
			return (int)(1);
		}

		public static int fonsTextIterNext(FONScontext* stash, FONStextIter* iter, FONSquad* quad)
		{
			FONSglyph* glyph = (null);
			sbyte* str = iter->next;
			iter->str = iter->next;
			if ((str) == (iter->end)) return (int)(0);
			for (; str != iter->end; str++) {
if ((fons__decutf8(&iter->utf8state, &iter->codepoint, (uint)(*(byte*)(str)))) != 0) continue;str++;iter->x = (float)(iter->nextx);iter->y = (float)(iter->nexty);glyph = fons__getGlyph(stash, iter->font, (uint)(iter->codepoint), (short)(iter->isize), (short)(iter->iblur), (int)(iter->bitmapOption));if (glyph != (null)) fons__getQuad(stash, iter->font, (int)(iter->prevGlyphIndex), glyph, (float)(iter->scale), (float)(iter->spacing), &iter->nextx, &iter->nexty, quad);iter->prevGlyphIndex = (int)(glyph != (null)?glyph->index:-1);break;}
			iter->next = str;
			return (int)(1);
		}

		public static void fonsDrawDebug(FONScontext* stash, float x, float y)
		{
			int i = 0;
			int w = (int)(stash->params.width);
			int h = (int)(stash->params.height);
			float u = (float)((w) == (0)?0:(1.0f / w));
			float v = (float)((h) == (0)?0:(1.0f / h));
			if ((stash->nverts + 6 + 6) > (1024)) fons__flush(stash);
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
			for (i = (int)(0); (i) < (stash->atlas->nnodes); i++) {
FONSatlasNode* n = &stash->atlas->nodes[i];if ((stash->nverts + 6) > (1024)) fons__flush(stash);fons__vertex(stash, (float)(x + n->x + 0), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));fons__vertex(stash, (float)(x + n->x + n->width), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));fons__vertex(stash, (float)(x + n->x + n->width), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));fons__vertex(stash, (float)(x + n->x + 0), (float)(y + n->y + 0), (float)(u), (float)(v), (uint)(0xc00000ff));fons__vertex(stash, (float)(x + n->x + 0), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));fons__vertex(stash, (float)(x + n->x + n->width), (float)(y + n->y + 1), (float)(u), (float)(v), (uint)(0xc00000ff));}
			fons__flush(stash);
		}

		public static float fonsTextBounds(FONScontext* stash, float x, float y, sbyte* str, sbyte* end, float* bounds)
		{
			FONSstate* state = fons__getState(stash);
			uint codepoint = 0;
			uint utf8state = (uint)(0);
			FONSquad q =  new FONSquad();
			FONSglyph* glyph = (null);
			int prevGlyphIndex = (int)(-1);
			short isize = (short)(state->size * 10.0f);
			short iblur = (short)(state->blur);
			float scale = 0;
			FONSfont* font;
			float startx = 0;float advance = 0;
			float minx = 0;float miny = 0;float maxx = 0;float maxy = 0;
			if ((stash) == (null)) return (float)(0);
			if (((state->font) < (0)) || ((state->font) >= (stash->nfonts))) return (float)(0);
			font = stash->fonts[state->font];
			if ((font->data) == (null)) return (float)(0);
			scale = (float)(fons__tt_getPixelHeightScale(&font->font, (float)((float)(isize) / 10.0f)));
			y += (float)(fons__getVertAlign(stash, font, (int)(state->align), (short)(isize)));
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			startx = (float)(x);
			if ((end) == (null)) end = str + CRuntime.strlen(str);
			for (; str != end; ++str) {
if ((fons__decutf8(&utf8state, &codepoint, (uint)(*(byte*)(str)))) != 0) continue;glyph = fons__getGlyph(stash, font, (uint)(codepoint), (short)(isize), (short)(iblur), (int)(FONS_GLYPH_BITMAP_OPTIONAL));if (glyph != (null)) {
fons__getQuad(stash, font, (int)(prevGlyphIndex), glyph, (float)(scale), (float)(state->spacing), &x, &y, &q);if ((q.x0) < (minx)) minx = (float)(q.x0);if ((q.x1) > (maxx)) maxx = (float)(q.x1);if ((stash->params.flags & FONS_ZERO_TOPLEFT) != 0) {
if ((q.y0) < (miny)) miny = (float)(q.y0);if ((q.y1) > (maxy)) maxy = (float)(q.y1);}
 else {
if ((q.y1) < (miny)) miny = (float)(q.y1);if ((q.y0) > (maxy)) maxy = (float)(q.y0);}
}
prevGlyphIndex = (int)(glyph != (null)?glyph->index:-1);}
			advance = (float)(x - startx);
			if ((state->align & FONS_ALIGN_LEFT) != 0) {
}
 else if ((state->align & FONS_ALIGN_RIGHT) != 0) {
minx -= (float)(advance);maxx -= (float)(advance);}
 else if ((state->align & FONS_ALIGN_CENTER) != 0) {
minx -= (float)(advance * 0.5f);maxx -= (float)(advance * 0.5f);}

			if ((bounds) != null) {
bounds[0] = (float)(minx);bounds[1] = (float)(miny);bounds[2] = (float)(maxx);bounds[3] = (float)(maxy);}

			return (float)(advance);
		}

		public static void fonsVertMetrics(FONScontext* stash, float* ascender, float* descender, float* lineh)
		{
			FONSfont* font;
			FONSstate* state = fons__getState(stash);
			short isize = 0;
			if ((stash) == (null)) return;
			if (((state->font) < (0)) || ((state->font) >= (stash->nfonts))) return;
			font = stash->fonts[state->font];
			isize = ((short)(state->size * 10.0f));
			if ((font->data) == (null)) return;
			if ((ascender) != null) *ascender = (float)(font->ascender * isize / 10.0f);
			if ((descender) != null) *descender = (float)(font->descender * isize / 10.0f);
			if ((lineh) != null) *lineh = (float)(font->lineh * isize / 10.0f);
		}

		public static void fonsLineBounds(FONScontext* stash, float y, float* miny, float* maxy)
		{
			FONSfont* font;
			FONSstate* state = fons__getState(stash);
			short isize = 0;
			if ((stash) == (null)) return;
			if (((state->font) < (0)) || ((state->font) >= (stash->nfonts))) return;
			font = stash->fonts[state->font];
			isize = ((short)(state->size * 10.0f));
			if ((font->data) == (null)) return;
			y += (float)(fons__getVertAlign(stash, font, (int)(state->align), (short)(isize)));
			if ((stash->params.flags & FONS_ZERO_TOPLEFT) != 0) {
*miny = (float)(y - font->ascender * (float)(isize) / 10.0f);*maxy = (float)(*miny + font->lineh * isize / 10.0f);}
 else {
*maxy = (float)(y + font->descender * (float)(isize) / 10.0f);*miny = (float)(*maxy - font->lineh * isize / 10.0f);}

		}

		public static byte* fonsGetTextureData(FONScontext* stash, int* width, int* height)
		{
			if (width != (null)) *width = (int)(stash->params.width);
			if (height != (null)) *height = (int)(stash->params.height);
			return stash->texData;
		}

		public static int fonsValidateTexture(FONScontext* stash, int* dirty)
		{
			if (((stash->dirtyRect[0]) < (stash->dirtyRect[2])) && ((stash->dirtyRect[1]) < (stash->dirtyRect[3]))) {
dirty[0] = (int)(stash->dirtyRect[0]);dirty[1] = (int)(stash->dirtyRect[1]);dirty[2] = (int)(stash->dirtyRect[2]);dirty[3] = (int)(stash->dirtyRect[3]);stash->dirtyRect[0] = (int)(stash->params.width);stash->dirtyRect[1] = (int)(stash->params.height);stash->dirtyRect[2] = (int)(0);stash->dirtyRect[3] = (int)(0);return (int)(1);}

			return (int)(0);
		}

		public static void fonsDeleteInternal(FONScontext* stash)
		{
			int i = 0;
			if ((stash) == (null)) return;
			if ((stash->params.renderDelete) != null) stash->params.renderDelete(stash->params.userPtr);
			for (i = (int)(0); (i) < (stash->nfonts); ++i) {fons__freeFont(stash->fonts[i]);}
			if ((stash->atlas) != null) fons__deleteAtlas(stash->atlas);
			if ((stash->fonts) != null) CRuntime.free(stash->fonts);
			if ((stash->texData) != null) CRuntime.free(stash->texData);
			if ((stash->scratch) != null) CRuntime.free(stash->scratch);
			CRuntime.free(stash);
			fons__tt_done(stash);
		}

		public static void fonsSetErrorCallback(FONScontext* stash, void (void *, int, int)* callback, void * uptr)
		{
			if ((stash) == (null)) return;
			stash->handleError = callback;
			stash->errorUptr = uptr;
		}

		public static void fonsGetAtlasSize(FONScontext* stash, int* width, int* height)
		{
			if ((stash) == (null)) return;
			*width = (int)(stash->params.width);
			*height = (int)(stash->params.height);
		}

		public static int fonsExpandAtlas(FONScontext* stash, int width, int height)
		{
			int i = 0;int maxy = (int)(0);
			byte* data = (null);
			if ((stash) == (null)) return (int)(0);
			width = (int)(fons__maxi((int)(width), (int)(stash->params.width)));
			height = (int)(fons__maxi((int)(height), (int)(stash->params.height)));
			if (((width) == (stash->params.width)) && ((height) == (stash->params.height))) return (int)(1);
			fons__flush(stash);
			if (stash->params.renderResize != (null)) {
if ((stash->params.renderResize(stash->params.userPtr, (int)(width), (int)(height))) == (0)) return (int)(0);}

			data = (byte*)(CRuntime.malloc((ulong)(width * height)));
			if ((data) == (null)) return (int)(0);
			for (i = (int)(0); (i) < (stash->params.height); i++) {
byte* dst = &data[i * width];byte* src = &stash->texData[i * stash->params.width];CRuntime.memcpy(dst, src, (ulong)(stash->params.width));if ((width) > (stash->params.width)) CRuntime.memset(dst + stash->params.width, (int)(0), (ulong)(width - stash->params.width));}
			if ((height) > (stash->params.height)) CRuntime.memset(&data[stash->params.height * width], (int)(0), (ulong)((height - stash->params.height) * width));
			CRuntime.free(stash->texData);
			stash->texData = data;
			fons__atlasExpand(stash->atlas, (int)(width), (int)(height));
			for (i = (int)(0); (i) < (stash->atlas->nnodes); i++) {maxy = (int)(fons__maxi((int)(maxy), (int)(stash->atlas->nodes[i].y)));}
			stash->dirtyRect[0] = (int)(0);
			stash->dirtyRect[1] = (int)(0);
			stash->dirtyRect[2] = (int)(stash->params.width);
			stash->dirtyRect[3] = (int)(maxy);
			stash->params.width = (int)(width);
			stash->params.height = (int)(height);
			stash->itw = (float)(1.0f / stash->params.width);
			stash->ith = (float)(1.0f / stash->params.height);
			return (int)(1);
		}

		public static int fonsResetAtlas(FONScontext* stash, int width, int height)
		{
			int i = 0;int j = 0;
			if ((stash) == (null)) return (int)(0);
			fons__flush(stash);
			if (stash->params.renderResize != (null)) {
if ((stash->params.renderResize(stash->params.userPtr, (int)(width), (int)(height))) == (0)) return (int)(0);}

			fons__atlasReset(stash->atlas, (int)(width), (int)(height));
			stash->texData = (byte*)(CRuntime.realloc(stash->texData, (ulong)(width * height)));
			if ((stash->texData) == (null)) return (int)(0);
			CRuntime.memset(stash->texData, (int)(0), (ulong)(width * height));
			stash->dirtyRect[0] = (int)(width);
			stash->dirtyRect[1] = (int)(height);
			stash->dirtyRect[2] = (int)(0);
			stash->dirtyRect[3] = (int)(0);
			for (i = (int)(0); (i) < (stash->nfonts); i++) {
FONSfont* font = stash->fonts[i];font->nglyphs = (int)(0);for (j = (int)(0); (j) < (256); j++) {font->lut[j] = (int)(-1);}}
			stash->params.width = (int)(width);
			stash->params.height = (int)(height);
			stash->itw = (float)(1.0f / stash->params.width);
			stash->ith = (float)(1.0f / stash->params.height);
			fons__addWhiteRect(stash, (int)(2), (int)(2));
			return (int)(1);
		}

		public static void stbi__start_mem(stbi__context s, byte* buffer, int len)
		{
			s.io.read = (null);
			s.read_from_callbacks = (int)(0);
			s.img_buffer = s.img_buffer_original = buffer;
			s.img_buffer_end = s.img_buffer_original_end = buffer + len;
		}

		public static void stbi__start_callbacks(stbi__context s, stbi_io_callbacks c, void * user)
		{
			s.io = (stbi_io_callbacks)(c);
			s.io_user_data = user;
			s.buflen = (int)(s.buffer_start.Size);
			s.read_from_callbacks = (int)(1);
			s.img_buffer_original = s.buffer_start;
			stbi__refill_buffer(s);
			s.img_buffer_original_end = s.img_buffer_end;
		}

		public static void stbi__rewind(stbi__context s)
		{
			s.img_buffer = s.img_buffer_original;
			s.img_buffer_end = s.img_buffer_original_end;
		}

		public static void stbi_set_flip_vertically_on_load(int flag_true_if_should_flip)
		{
			stbi__vertically_flip_on_load = (int)(flag_true_if_should_flip);
		}

		public static byte* stbi__load_main(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			if ((stbi__jpeg_test(s)) != 0) return stbi__jpeg_load(s, x, y, comp, (int)(req_comp));
			if ((stbi__png_test(s)) != 0) return stbi__png_load(s, x, y, comp, (int)(req_comp));
			if ((stbi__bmp_test(s)) != 0) return stbi__bmp_load(s, x, y, comp, (int)(req_comp));
			if ((stbi__gif_test(s)) != 0) return stbi__gif_load(s, x, y, comp, (int)(req_comp));
			if ((stbi__psd_test(s)) != 0) return stbi__psd_load(s, x, y, comp, (int)(req_comp));
			if ((stbi__tga_test(s)) != 0) return stbi__tga_load(s, x, y, comp, (int)(req_comp));
			return ((byte*)((ulong)((stbi__err("unknown image type")) != 0?((byte *)null):(null))));
		}

		public static byte* stbi__load_flip(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			byte* result = stbi__load_main(s, x, y, comp, (int)(req_comp));
			if (((stbi__vertically_flip_on_load) != 0) && (result != (null))) {
int w = (int)(*x);int h = (int)(*y);int depth = (int)((req_comp) != 0?req_comp:*comp);int row = 0;int col = 0;int z = 0;byte temp = 0;for (row = (int)(0); (row) < (h >> 1); row++) {
for (col = (int)(0); (col) < (w); col++) {
for (z = (int)(0); (z) < (depth); z++) {
temp = (byte)(result[(row * w + col) * depth + z]);result[(row * w + col) * depth + z] = (byte)(result[((h - row - 1) * w + col) * depth + z]);result[((h - row - 1) * w + col) * depth + z] = (byte)(temp);}}}}

			return result;
		}

		public static byte* stbi_load_from_memory(byte* buffer, int len, int* x, int* y, int* comp, int req_comp)
		{
			stbi__context s =  new stbi__context();
			stbi__start_mem(s, buffer, (int)(len));
			return stbi__load_flip(s, x, y, comp, (int)(req_comp));
		}

		public static byte* stbi_load_from_callbacks(stbi_io_callbacks clbk, void * user, int* x, int* y, int* comp, int req_comp)
		{
			stbi__context s =  new stbi__context();
			stbi__start_callbacks(s, clbk, user);
			return stbi__load_flip(s, x, y, comp, (int)(req_comp));
		}

		public static void stbi_hdr_to_ldr_gamma(float gamma)
		{
			stbi__h2l_gamma_i = (float)(1 / gamma);
		}

		public static void stbi_hdr_to_ldr_scale(float scale)
		{
			stbi__h2l_scale_i = (float)(1 / scale);
		}

		public static void stbi__refill_buffer(stbi__context s)
		{
			int n = (int)(s.io.read(s.io_user_data, (sbyte*)(s.buffer_start), (int)(s.buflen)));
			if ((n) == (0)) {
s.read_from_callbacks = (int)(0);s.img_buffer = s.buffer_start;s.img_buffer_end = s.buffer_start; s.img_buffer_end++;*s.img_buffer = (byte)(0);}
 else {
s.img_buffer = s.buffer_start;s.img_buffer_end = s.buffer_start; s.img_buffer_end += n;}

		}

		public static byte stbi__get8(stbi__context s)
		{
			if ((s.img_buffer) < (s.img_buffer_end)) return (byte)(*s.img_buffer++);
			if ((s.read_from_callbacks) != 0) {
stbi__refill_buffer(s);return (byte)(*s.img_buffer++);}

			return (byte)(0);
		}

		public static int stbi__at_eof(stbi__context s)
		{
			if ((s.io.read) != null) {
if (s.io.eof(s.io_user_data)== 0) return (int)(0);if ((s.read_from_callbacks) == (0)) return (int)(1);}

			return (int)((s.img_buffer) >= (s.img_buffer_end)?1:0);
		}

		public static void stbi__skip(stbi__context s, int n)
		{
			if ((n) < (0)) {
s.img_buffer = s.img_buffer_end;return;}

			if ((s.io.read) != null) {
int blen = (int)(s.img_buffer_end - s.img_buffer);if ((blen) < (n)) {
s.img_buffer = s.img_buffer_end;s.io.skip(s.io_user_data, (int)(n - blen));return;}
}

			s.img_buffer += n;
		}

		public static int stbi__getn(stbi__context s, byte* buffer, int n)
		{
			if ((s.io.read) != null) {
int blen = (int)(s.img_buffer_end - s.img_buffer);if ((blen) < (n)) {
int res = 0;int count = 0;CRuntime.memcpy(buffer, s.img_buffer, (ulong)(blen));count = (int)(s.io.read(s.io_user_data, (sbyte*)(buffer) + blen, (int)(n - blen)));res = (int)((count) == (n - blen)?1:0);s.img_buffer = s.img_buffer_end;return (int)(res);}
}

			if (s.img_buffer + n <= s.img_buffer_end) {
CRuntime.memcpy(buffer, s.img_buffer, (ulong)(n));s.img_buffer += n;return (int)(1);}
 else return (int)(0);
		}

		public static int stbi__get16be(stbi__context s)
		{
			int z = (int)(stbi__get8(s));
			return (int)((z << 8) + stbi__get8(s));
		}

		public static uint stbi__get32be(stbi__context s)
		{
			uint z = (uint)(stbi__get16be(s));
			return (uint)((z << 16) + stbi__get16be(s));
		}

		public static int stbi__get16le(stbi__context s)
		{
			int z = (int)(stbi__get8(s));
			return (int)(z + (stbi__get8(s) << 8));
		}

		public static uint stbi__get32le(stbi__context s)
		{
			uint z = (uint)(stbi__get16le(s));
			return (uint)(z + (stbi__get16le(s) << 16));
		}

		public static byte stbi__compute_y(int r, int g, int b)
		{
			return (byte)(((r * 77) + (g * 150) + (29 * b)) >> 8);
		}

		public static byte* stbi__convert_format(byte* data, int img_n, int req_comp, uint x, uint y)
		{
			int i = 0;int j = 0;
			byte* good;
			if ((req_comp) == (img_n)) return data;
			(void)((!!(((req_comp) >= (1)) && (req_comp <= 4))) || (_wassert("req_comp >= 1 && req_comp <= 4", "nanovg/stb_image.h", (uint)(1346)) , 0));
			good = (byte*)(stbi__malloc((ulong)(req_comp * x * y)));
			if ((good) == (null)) {
CRuntime.free(data);return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));}

			for (j = (int)(0); (j) < ((int)(y)); ++j) {
byte* src = data + j * x * img_n;byte* dest = good + j * x * req_comp;switch (((img_n) * 8 + (req_comp))){
case ((1) * 8 + (2)):for (i = (int)(x - 1); (i) >= (0); --i , src += 1 , dest += 2) {dest[0] = (byte)(src[0]);dest[1] = (byte)(255);}break;case ((1) * 8 + (3)):for (i = (int)(x - 1); (i) >= (0); --i , src += 1 , dest += 3) {dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));}break;case ((1) * 8 + (4)):for (i = (int)(x - 1); (i) >= (0); --i , src += 1 , dest += 4) {dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));dest[3] = (byte)(255);}break;case ((2) * 8 + (1)):for (i = (int)(x - 1); (i) >= (0); --i , src += 2 , dest += 1) {dest[0] = (byte)(src[0]);}break;case ((2) * 8 + (3)):for (i = (int)(x - 1); (i) >= (0); --i , src += 2 , dest += 3) {dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));}break;case ((2) * 8 + (4)):for (i = (int)(x - 1); (i) >= (0); --i , src += 2 , dest += 4) {dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));dest[3] = (byte)(src[1]);}break;case ((3) * 8 + (4)):for (i = (int)(x - 1); (i) >= (0); --i , src += 3 , dest += 4) {dest[0] = (byte)(src[0]);dest[1] = (byte)(src[1]);dest[2] = (byte)(src[2]);dest[3] = (byte)(255);}break;case ((3) * 8 + (1)):for (i = (int)(x - 1); (i) >= (0); --i , src += 3 , dest += 1) {dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));}break;case ((3) * 8 + (2)):for (i = (int)(x - 1); (i) >= (0); --i , src += 3 , dest += 2) {dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));dest[1] = (byte)(255);}break;case ((4) * 8 + (1)):for (i = (int)(x - 1); (i) >= (0); --i , src += 4 , dest += 1) {dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));}break;case ((4) * 8 + (2)):for (i = (int)(x - 1); (i) >= (0); --i , src += 4 , dest += 2) {dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));dest[1] = (byte)(src[3]);}break;case ((4) * 8 + (3)):for (i = (int)(x - 1); (i) >= (0); --i , src += 4 , dest += 3) {dest[0] = (byte)(src[0]);dest[1] = (byte)(src[1]);dest[2] = (byte)(src[2]);}break;default: (void)((!!(0)) || (_wassert("0", "nanovg/stb_image.h", (uint)(1375)) , 0));}
}
			CRuntime.free(data);
			return good;
		}

		public static int stbi__build_huffman(stbi__huffman* h, int* count)
		{
			int i = 0;int j = 0;int k = (int)(0);int code = 0;
			for (i = (int)(0); (i) < (16); ++i) {for (j = (int)(0); (j) < (count[i]); ++j) {h->size[k++] = ((byte)(i + 1));}}
			h->size[k] = (byte)(0);
			code = (int)(0);
			k = (int)(0);
			for (j = (int)(1); j <= 16; ++j) {
h->delta[j] = (int)(k - code);if ((h->size[k]) == (j)) {
while ((h->size[k]) == (j)) {h->code[k++] = ((ushort)(code++));}if ((code - 1) >= (1 << j)) return (int)(stbi__err("bad code lengths"));}
h->maxcode[j] = (uint)(code << (16 - j));code <<= 1;}
			h->maxcode[j] = (uint)(0xffffffff);
			CRuntime.memset(h->fast, (int)(255), (ulong)(1 << 9));
			for (i = (int)(0); (i) < (k); ++i) {
int s = (int)(h->size[i]);if (s <= 9) {
int c = (int)(h->code[i] << (9 - s));int m = (int)(1 << (9 - s));for (j = (int)(0); (j) < (m); ++j) {
h->fast[c + j] = ((byte)(i));}}
}
			return (int)(1);
		}

		public static void stbi__build_fast_ac(short* fast_ac, stbi__huffman* h)
		{
			int i = 0;
			for (i = (int)(0); (i) < (1 << 9); ++i) {
byte fast = (byte)(h->fast[i]);fast_ac[i] = (short)(0);if ((fast) < (255)) {
int rs = (int)(h->values[fast]);int run = (int)((rs >> 4) & 15);int magbits = (int)(rs & 15);int len = (int)(h->size[fast]);if (((magbits) != 0) && (len + magbits <= 9)) {
int k = (int)(((i << len) & ((1 << 9) - 1)) >> (9 - magbits));int m = (int)(1 << (magbits - 1));if ((k) < (m)) k += (int)((-1 << magbits) + 1);if (((k) >= (-128)) && (k <= 127)) fast_ac[i] = ((short)((k << 8) + (run << 4) + (len + magbits)));}
}
}
		}

		public static void stbi__grow_buffer_unsafe(stbi__jpeg j)
		{
			do {
int b = (int)((j.nomore) != 0?0:stbi__get8(j.s));if ((b) == (0xff)) {
int c = (int)(stbi__get8(j.s));if (c != 0) {
j.marker = ((byte)(c));j.nomore = (int)(1);return;}
}
j.code_buffer |= (uint)(b << (24 - j.code_bits));j.code_bits += (int)(8);}
 while (j.code_bits <= 24);
		}

		public static int stbi__jpeg_huff_decode(stbi__jpeg j, stbi__huffman* h)
		{
			uint temp = 0;
			int c = 0;int k = 0;
			if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
			c = (int)((j.code_buffer >> (32 - 9)) & ((1 << 9) - 1));
			k = (int)(h->fast[c]);
			if ((k) < (255)) {
int s = (int)(h->size[k]);if ((s) > (j.code_bits)) return (int)(-1);j.code_buffer <<= s;j.code_bits -= (int)(s);return (int)(h->values[k]);}

			temp = (uint)(j.code_buffer >> 16);
			for (k = (int)(9 + 1); ; ++k) {if ((temp) < (h->maxcode[k])) break;}
			if ((k) == (17)) {
j.code_bits -= (int)(16);return (int)(-1);}

			if ((k) > (j.code_bits)) return (int)(-1);
			c = (int)(((j.code_buffer >> (32 - k)) & stbi__bmask[k]) + h->delta[k]);
			(void)((!!((((j.code_buffer) >> (32 - h->size[c])) & stbi__bmask[h->size[c]]) == (h->code[c]))) || (_wassert("(((j->code_buffer) >> (32 - h->size[c])) & stbi__bmask[h->size[c]]) == h->code[c]", "nanovg/stb_image.h", (uint)(1649)) , 0));
			j.code_bits -= (int)(k);
			j.code_buffer <<= k;
			return (int)(h->values[c]);
		}

		public static int stbi__extend_receive(stbi__jpeg j, int n)
		{
			uint k = 0;
			int sgn = 0;
			if ((j.code_bits) < (n)) stbi__grow_buffer_unsafe(j);
			sgn = (int)((int)j.code_buffer >> 31);
			k = (uint)(CRuntime._lrotl(j.code_buffer, (int)(n)));
			(void)((!!(((n) >= (0)) && ((n) < ((int)(sizeof((stbi__bmask)) / sizeof((*stbi__bmask))))))) || (_wassert("n >= 0 && n < (int) (sizeof(stbi__bmask)/sizeof(*stbi__bmask))", "nanovg/stb_image.h", (uint)(1670)) , 0));
			j.code_buffer = (uint)(k & ~stbi__bmask[n]);
			k &= (uint)(stbi__bmask[n]);
			j.code_bits -= (int)(n);
			return (int)(k + (stbi__jbias[n] & ~sgn));
		}

		public static int stbi__jpeg_get_bits(stbi__jpeg j, int n)
		{
			uint k = 0;
			if ((j.code_bits) < (n)) stbi__grow_buffer_unsafe(j);
			k = (uint)(CRuntime._lrotl(j.code_buffer, (int)(n)));
			j.code_buffer = (uint)(k & ~stbi__bmask[n]);
			k &= (uint)(stbi__bmask[n]);
			j.code_bits -= (int)(n);
			return (int)(k);
		}

		public static int stbi__jpeg_get_bit(stbi__jpeg j)
		{
			uint k = 0;
			if ((j.code_bits) < (1)) stbi__grow_buffer_unsafe(j);
			k = (uint)(j.code_buffer);
			j.code_buffer <<= 1;
			--j.code_bits;
			return (int)(k & 0x80000000);
		}

		public static int stbi__jpeg_decode_block(stbi__jpeg j, short* data, stbi__huffman* hdc, stbi__huffman* hac, short* fac, int b, byte* dequant)
		{
			int diff = 0;int dc = 0;int k = 0;
			int t = 0;
			if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
			t = (int)(stbi__jpeg_huff_decode(j, hdc));
			if ((t) < (0)) return (int)(stbi__err("bad huffman code"));
			CRuntime.memset(data, (int)(0), (ulong)(64 * sizeof(short)));
			diff = (int)((t) != 0?stbi__extend_receive(j, (int)(t)):0);
			dc = (int)(j.img_comp[b].dc_pred + diff);
			j.img_comp[b].dc_pred = (int)(dc);
			data[0] = ((short)(dc * dequant[0]));
			k = (int)(1);
			do {
uint zig = 0;int c = 0;int r = 0;int s = 0;if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);c = (int)((j.code_buffer >> (32 - 9)) & ((1 << 9) - 1));r = (int)(fac[c]);if ((r) != 0) {
k += (int)((r >> 4) & 15);s = (int)(r & 15);j.code_buffer <<= s;j.code_bits -= (int)(s);zig = (uint)(stbi__jpeg_dezigzag[k++]);data[zig] = ((short)((r >> 8) * dequant[zig]));}
 else {
int rs = (int)(stbi__jpeg_huff_decode(j, hac));if ((rs) < (0)) return (int)(stbi__err("bad huffman code"));s = (int)(rs & 15);r = (int)(rs >> 4);if ((s) == (0)) {
if (rs != 0xf0) break;k += (int)(16);}
 else {
k += (int)(r);zig = (uint)(stbi__jpeg_dezigzag[k++]);data[zig] = ((short)(stbi__extend_receive(j, (int)(s)) * dequant[zig]));}
}
}
 while ((k) < (64));
			return (int)(1);
		}

		public static int stbi__jpeg_decode_block_prog_dc(stbi__jpeg j, short* data, stbi__huffman* hdc, int b)
		{
			int diff = 0;int dc = 0;
			int t = 0;
			if (j.spec_end != 0) return (int)(stbi__err("can't merge dc and ac"));
			if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
			if ((j.succ_high) == (0)) {
CRuntime.memset(data, (int)(0), (ulong)(64 * sizeof(short)));t = (int)(stbi__jpeg_huff_decode(j, hdc));diff = (int)((t) != 0?stbi__extend_receive(j, (int)(t)):0);dc = (int)(j.img_comp[b].dc_pred + diff);j.img_comp[b].dc_pred = (int)(dc);data[0] = ((short)(dc << j.succ_low));}
 else {
if ((stbi__jpeg_get_bit(j)) != 0) data[0] += ((short)(1 << j.succ_low));}

			return (int)(1);
		}

		public static int stbi__jpeg_decode_block_prog_ac(stbi__jpeg j, short* data, stbi__huffman* hac, short* fac)
		{
			int k = 0;
			if ((j.spec_start) == (0)) return (int)(stbi__err("can't merge dc and ac"));
			if ((j.succ_high) == (0)) {
int shift = (int)(j.succ_low);if ((j.eob_run) != 0) {
--j.eob_run;return (int)(1);}
k = (int)(j.spec_start);do {
uint zig = 0;int c = 0;int r = 0;int s = 0;if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);c = (int)((j.code_buffer >> (32 - 9)) & ((1 << 9) - 1));r = (int)(fac[c]);if ((r) != 0) {
k += (int)((r >> 4) & 15);s = (int)(r & 15);j.code_buffer <<= s;j.code_bits -= (int)(s);zig = (uint)(stbi__jpeg_dezigzag[k++]);data[zig] = ((short)((r >> 8) << shift));}
 else {
int rs = (int)(stbi__jpeg_huff_decode(j, hac));if ((rs) < (0)) return (int)(stbi__err("bad huffman code"));s = (int)(rs & 15);r = (int)(rs >> 4);if ((s) == (0)) {
if ((r) < (15)) {
j.eob_run = (int)(1 << r);if ((r) != 0) j.eob_run += (int)(stbi__jpeg_get_bits(j, (int)(r)));--j.eob_run;break;}
k += (int)(16);}
 else {
k += (int)(r);zig = (uint)(stbi__jpeg_dezigzag[k++]);data[zig] = ((short)(stbi__extend_receive(j, (int)(s)) << shift));}
}
}
 while (k <= j.spec_end);}
 else {
short bit = (short)(1 << j.succ_low);if ((j.eob_run) != 0) {
--j.eob_run;for (k = (int)(j.spec_start); k <= j.spec_end; ++k) {
short* p = &data[stbi__jpeg_dezigzag[k]];if (*p != 0) if ((stbi__jpeg_get_bit(j)) != 0) if ((*p & bit) == (0)) {
if ((*p) > (0)) *p += (short)(bit); else *p -= (short)(bit);}
}}
 else {
k = (int)(j.spec_start);do {
int r = 0;int s = 0;int rs = (int)(stbi__jpeg_huff_decode(j, hac));if ((rs) < (0)) return (int)(stbi__err("bad huffman code"));s = (int)(rs & 15);r = (int)(rs >> 4);if ((s) == (0)) {
if ((r) < (15)) {
j.eob_run = (int)((1 << r) - 1);if ((r) != 0) j.eob_run += (int)(stbi__jpeg_get_bits(j, (int)(r)));r = (int)(64);}
 else {
}
}
 else {
if (s != 1) return (int)(stbi__err("bad huffman code"));if ((stbi__jpeg_get_bit(j)) != 0) s = (int)(bit); else s = (int)(-bit);}
while (k <= j.spec_end) {
short* p = &data[stbi__jpeg_dezigzag[k++]];if (*p != 0) {
if ((stbi__jpeg_get_bit(j)) != 0) if ((*p & bit) == (0)) {
if ((*p) > (0)) *p += (short)(bit); else *p -= (short)(bit);}
}
 else {
if ((r) == (0)) {
*p = ((short)(s));break;}
--r;}
}}
 while (k <= j.spec_end);}
}

			return (int)(1);
		}

		public static byte stbi__clamp(int x)
		{
			if (((uint)(x)) > (255)) {
if ((x) < (0)) return (byte)(0);if ((x) > (255)) return (byte)(255);}

			return (byte)(x);
		}

		public static void stbi__idct_block(byte* _out_, int out_stride, short* data)
		{
			int i = 0;int* val = stackalloc int[64];int* v = val;
			byte* o;
			short* d = ((short*)data);
			for (i = (int)(0); (i) < (8); ++i , ++d , ++v) {
if ((((((((d[8]) == (0)) && ((d[16]) == (0))) && ((d[24]) == (0))) && ((d[32]) == (0))) && ((d[40]) == (0))) && ((d[48]) == (0))) && ((d[56]) == (0))) {
int dcterm = (int)(d[0] << 2);v[0] = (int)(v[8] = (int)(v[16] = (int)(v[24] = (int)(v[32] = (int)(v[40] = (int)(v[48] = (int)(v[56] = (int)(dcterm))))))));}
 else {
int t0 = 0;int t1 = 0;int t2 = 0;int t3 = 0;int p1 = 0;int p2 = 0;int p3 = 0;int p4 = 0;int p5 = 0;int x0 = 0;int x1 = 0;int x2 = 0;int x3 = 0;p2 = (int)(d[16]);p3 = (int)(d[48]);p1 = (int)((p2 + p3) * ((int)((0.5411961f) * 4096 + 0.5)));t2 = (int)(p1 + p3 * ((int)((-1.847759065f) * 4096 + 0.5)));t3 = (int)(p1 + p2 * ((int)((0.765366865f) * 4096 + 0.5)));p2 = (int)(d[0]);p3 = (int)(d[32]);t0 = (int)((p2 + p3) << 12);t1 = (int)((p2 - p3) << 12);x0 = (int)(t0 + t3);x3 = (int)(t0 - t3);x1 = (int)(t1 + t2);x2 = (int)(t1 - t2);t0 = (int)(d[56]);t1 = (int)(d[40]);t2 = (int)(d[24]);t3 = (int)(d[8]);p3 = (int)(t0 + t2);p4 = (int)(t1 + t3);p1 = (int)(t0 + t3);p2 = (int)(t1 + t2);p5 = (int)((p3 + p4) * ((int)((1.175875602f) * 4096 + 0.5)));t0 = (int)(t0 * ((int)((0.298631336f) * 4096 + 0.5)));t1 = (int)(t1 * ((int)((2.053119869f) * 4096 + 0.5)));t2 = (int)(t2 * ((int)((3.072711026f) * 4096 + 0.5)));t3 = (int)(t3 * ((int)((1.501321110f) * 4096 + 0.5)));p1 = (int)(p5 + p1 * ((int)((-0.899976223f) * 4096 + 0.5)));p2 = (int)(p5 + p2 * ((int)((-2.562915447f) * 4096 + 0.5)));p3 = (int)(p3 * ((int)((-1.961570560f) * 4096 + 0.5)));p4 = (int)(p4 * ((int)((-0.390180644f) * 4096 + 0.5)));t3 += (int)(p1 + p4);t2 += (int)(p2 + p3);t1 += (int)(p2 + p4);t0 += (int)(p1 + p3);x0 += (int)(512);x1 += (int)(512);x2 += (int)(512);x3 += (int)(512);v[0] = (int)((x0 + t3) >> 10);v[56] = (int)((x0 - t3) >> 10);v[8] = (int)((x1 + t2) >> 10);v[48] = (int)((x1 - t2) >> 10);v[16] = (int)((x2 + t1) >> 10);v[40] = (int)((x2 - t1) >> 10);v[24] = (int)((x3 + t0) >> 10);v[32] = (int)((x3 - t0) >> 10);}
}
			for (i = (int)(0) , v = val , o = _out_; (i) < (8); ++i , v += 8 , o += out_stride) {
int t0 = 0;int t1 = 0;int t2 = 0;int t3 = 0;int p1 = 0;int p2 = 0;int p3 = 0;int p4 = 0;int p5 = 0;int x0 = 0;int x1 = 0;int x2 = 0;int x3 = 0;p2 = (int)(v[2]);p3 = (int)(v[6]);p1 = (int)((p2 + p3) * ((int)((0.5411961f) * 4096 + 0.5)));t2 = (int)(p1 + p3 * ((int)((-1.847759065f) * 4096 + 0.5)));t3 = (int)(p1 + p2 * ((int)((0.765366865f) * 4096 + 0.5)));p2 = (int)(v[0]);p3 = (int)(v[4]);t0 = (int)((p2 + p3) << 12);t1 = (int)((p2 - p3) << 12);x0 = (int)(t0 + t3);x3 = (int)(t0 - t3);x1 = (int)(t1 + t2);x2 = (int)(t1 - t2);t0 = (int)(v[7]);t1 = (int)(v[5]);t2 = (int)(v[3]);t3 = (int)(v[1]);p3 = (int)(t0 + t2);p4 = (int)(t1 + t3);p1 = (int)(t0 + t3);p2 = (int)(t1 + t2);p5 = (int)((p3 + p4) * ((int)((1.175875602f) * 4096 + 0.5)));t0 = (int)(t0 * ((int)((0.298631336f) * 4096 + 0.5)));t1 = (int)(t1 * ((int)((2.053119869f) * 4096 + 0.5)));t2 = (int)(t2 * ((int)((3.072711026f) * 4096 + 0.5)));t3 = (int)(t3 * ((int)((1.501321110f) * 4096 + 0.5)));p1 = (int)(p5 + p1 * ((int)((-0.899976223f) * 4096 + 0.5)));p2 = (int)(p5 + p2 * ((int)((-2.562915447f) * 4096 + 0.5)));p3 = (int)(p3 * ((int)((-1.961570560f) * 4096 + 0.5)));p4 = (int)(p4 * ((int)((-0.390180644f) * 4096 + 0.5)));t3 += (int)(p1 + p4);t2 += (int)(p2 + p3);t1 += (int)(p2 + p4);t0 += (int)(p1 + p3);x0 += (int)(65536 + (128 << 17));x1 += (int)(65536 + (128 << 17));x2 += (int)(65536 + (128 << 17));x3 += (int)(65536 + (128 << 17));o[0] = (byte)(stbi__clamp((int)((x0 + t3) >> 17)));o[7] = (byte)(stbi__clamp((int)((x0 - t3) >> 17)));o[1] = (byte)(stbi__clamp((int)((x1 + t2) >> 17)));o[6] = (byte)(stbi__clamp((int)((x1 - t2) >> 17)));o[2] = (byte)(stbi__clamp((int)((x2 + t1) >> 17)));o[5] = (byte)(stbi__clamp((int)((x2 - t1) >> 17)));o[3] = (byte)(stbi__clamp((int)((x3 + t0) >> 17)));o[4] = (byte)(stbi__clamp((int)((x3 - t0) >> 17)));}
		}

		public static byte stbi__get_marker(stbi__jpeg j)
		{
			byte x = 0;
			if (j.marker != 0xff) {
x = (byte)(j.marker);j.marker = (byte)(0xff);return (byte)(x);}

			x = (byte)(stbi__get8(j.s));
			if (x != 0xff) return (byte)(0xff);
			while ((x) == (0xff)) {x = (byte)(stbi__get8(j.s));}
			return (byte)(x);
		}

		public static void stbi__jpeg_reset(stbi__jpeg j)
		{
			j.code_bits = (int)(0);
			j.code_buffer = (uint)(0);
			j.nomore = (int)(0);
			j.img_comp[0].dc_pred = (int)(j.img_comp[1].dc_pred = (int)(j.img_comp[2].dc_pred = (int)(0)));
			j.marker = (byte)(0xff);
			j.todo = (int)((j.restart_interval) != 0?j.restart_interval:0x7fffffff);
			j.eob_run = (int)(0);
		}

		public static int stbi__parse_entropy_coded_data(stbi__jpeg z)
		{
			stbi__jpeg_reset(z);
			if (z.progressive== 0) {
if ((z.scan_n) == (1)) {
int i = 0;int j = 0;short* data = stackalloc short[64];int n = (int)(z.order[0]);int w = (int)((z.img_comp[n].x + 7) >> 3);int h = (int)((z.img_comp[n].y + 7) >> 3);for (j = (int)(0); (j) < (h); ++j) {
for (i = (int)(0); (i) < (w); ++i) {
int ha = (int)(z.img_comp[n].ha);if (stbi__jpeg_decode_block(z, data, (stbi__huffman *)z.huff_dc + z.img_comp[n].hd, (stbi__huffman *)z.huff_ac + ha, z.fast_ac[ha], (int)(n), (ushort *)z.dequant[z.img_comp[n].tq])== 0) return (int)(0);z.idct_block_kernel(z.img_comp[n].data + z.img_comp[n].w2 * j * 8 + i * 8, (int)(z.img_comp[n].w2), data);if (--z.todo <= 0) {
if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);stbi__jpeg_reset(z);}
}}return (int)(1);}
 else {
int i = 0;int j = 0;int k = 0;int x = 0;int y = 0;short* data = stackalloc short[64];for (j = (int)(0); (j) < (z.img_mcu_y); ++j) {
for (i = (int)(0); (i) < (z.img_mcu_x); ++i) {
for (k = (int)(0); (k) < (z.scan_n); ++k) {
int n = (int)(z.order[k]);for (y = (int)(0); (y) < (z.img_comp[n].v); ++y) {
for (x = (int)(0); (x) < (z.img_comp[n].h); ++x) {
int x2 = (int)((i * z.img_comp[n].h + x) * 8);int y2 = (int)((j * z.img_comp[n].v + y) * 8);int ha = (int)(z.img_comp[n].ha);if (stbi__jpeg_decode_block(z, data, (stbi__huffman *)z.huff_dc + z.img_comp[n].hd, (stbi__huffman *)z.huff_ac + ha, z.fast_ac[ha], (int)(n), (ushort *)z.dequant[z.img_comp[n].tq])== 0) return (int)(0);z.idct_block_kernel(z.img_comp[n].data + z.img_comp[n].w2 * y2 + x2, (int)(z.img_comp[n].w2), data);}}}if (--z.todo <= 0) {
if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);stbi__jpeg_reset(z);}
}}return (int)(1);}
}
 else {
if ((z.scan_n) == (1)) {
int i = 0;int j = 0;int n = (int)(z.order[0]);int w = (int)((z.img_comp[n].x + 7) >> 3);int h = (int)((z.img_comp[n].y + 7) >> 3);for (j = (int)(0); (j) < (h); ++j) {
for (i = (int)(0); (i) < (w); ++i) {
short* data = z.img_comp[n].coeff + 64 * (i + j * z.img_comp[n].coeff_w);if ((z.spec_start) == (0)) {
if (stbi__jpeg_decode_block_prog_dc(z, data, (stbi__huffman*)z.huff_dc + z.img_comp[n].hd, (int)(n))== 0) return (int)(0);}
 else {
int ha = (int)(z.img_comp[n].ha);if (stbi__jpeg_decode_block_prog_ac(z, data, (stbi__huffman*)z.huff_ac + ha, z.fast_ac[ha])== 0) return (int)(0);}
if (--z.todo <= 0) {
if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);stbi__jpeg_reset(z);}
}}return (int)(1);}
 else {
int i = 0;int j = 0;int k = 0;int x = 0;int y = 0;for (j = (int)(0); (j) < (z.img_mcu_y); ++j) {
for (i = (int)(0); (i) < (z.img_mcu_x); ++i) {
for (k = (int)(0); (k) < (z.scan_n); ++k) {
int n = (int)(z.order[k]);for (y = (int)(0); (y) < (z.img_comp[n].v); ++y) {
for (x = (int)(0); (x) < (z.img_comp[n].h); ++x) {
int x2 = (int)(i * z.img_comp[n].h + x);int y2 = (int)(j * z.img_comp[n].v + y);short* data = z.img_comp[n].coeff + 64 * (x2 + y2 * z.img_comp[n].coeff_w);if (stbi__jpeg_decode_block_prog_dc(z, data, (stbi__huffman*)z.huff_dc + z.img_comp[n].hd, (int)(n))== 0) return (int)(0);}}}if (--z.todo <= 0) {
if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);stbi__jpeg_reset(z);}
}}return (int)(1);}
}

		}

		public static void stbi__jpeg_dequantize(short* data, byte* dequant)
		{
			int i = 0;
			for (i = (int)(0); (i) < (64); ++i) {data[i] *= (short)(dequant[i]);}
		}

		public static void stbi__jpeg_finish(stbi__jpeg z)
		{
			if ((z.progressive) != 0) {
int i = 0;int j = 0;int n = 0;for (n = (int)(0); (n) < (z.s.img_n); ++n) {
int w = (int)((z.img_comp[n].x + 7) >> 3);int h = (int)((z.img_comp[n].y + 7) >> 3);for (j = (int)(0); (j) < (h); ++j) {
for (i = (int)(0); (i) < (w); ++i) {
short* data = z.img_comp[n].coeff + 64 * (i + j * z.img_comp[n].coeff_w);stbi__jpeg_dequantize(data, (ushort *)z.dequant[z.img_comp[n].tq]);z.idct_block_kernel(z.img_comp[n].data + z.img_comp[n].w2 * j * 8 + i * 8, (int)(z.img_comp[n].w2), data);}}}}

		}

		public static int stbi__process_marker(stbi__jpeg z, int m)
		{
			int L = 0;
			switch (m){
case 0xff:return (int)(stbi__err("expected marker"));case 0xDD:if (stbi__get16be(z.s) != 4) return (int)(stbi__err("bad DRI len"));z.restart_interval = (int)(stbi__get16be(z.s));return (int)(1);case 0xDB:L = (int)(stbi__get16be(z.s) - 2);while ((L) > (0)) {
int q = (int)(stbi__get8(z.s));int p = (int)(q >> 4);int t = (int)(q & 15);int i = 0;if (p != 0) return (int)(stbi__err("bad DQT type"));if ((t) > (3)) return (int)(stbi__err("bad DQT table"));for (i = (int)(0); (i) < (64); ++i) {z.dequant[t][stbi__jpeg_dezigzag[i]] = (byte)(stbi__get8(z.s));}L -= (int)(65);}return (int)((L) == (0)?1:0);case 0xC4:L = (int)(stbi__get16be(z.s) - 2);while ((L) > (0)) {
byte* v;int* sizes = stackalloc int[16];int i = 0;int n = (int)(0);int q = (int)(stbi__get8(z.s));int tc = (int)(q >> 4);int th = (int)(q & 15);if (((tc) > (1)) || ((th) > (3))) return (int)(stbi__err("bad DHT header"));for (i = (int)(0); (i) < (16); ++i) {
sizes[i] = (int)(stbi__get8(z.s));n += (int)(sizes[i]);}L -= (int)(17);if ((tc) == (0)) {
if (stbi__build_huffman((stbi__huffman *)z.huff_dc + th, sizes)== 0) return (int)(0);v = z.huff_dc[th].values;}
 else {
if (stbi__build_huffman((stbi__huffman *)z.huff_ac + th, sizes)== 0) return (int)(0);v = z.huff_ac[th].values;}
for (i = (int)(0); (i) < (n); ++i) {v[i] = (byte)(stbi__get8(z.s));}if (tc != 0) stbi__build_fast_ac(z.fast_ac[th], (stbi__huffman *)z.huff_ac + th);L -= (int)(n);}return (int)((L) == (0)?1:0);}

			if ((((m) >= (0xE0)) && (m <= 0xEF)) || ((m) == (0xFE))) {
stbi__skip(z.s, (int)(stbi__get16be(z.s) - 2));return (int)(1);}

			return (int)(0);
		}

		public static int stbi__process_scan_header(stbi__jpeg z)
		{
			int i = 0;
			int Ls = (int)(stbi__get16be(z.s));
			z.scan_n = (int)(stbi__get8(z.s));
			if ((((z.scan_n) < (1)) || ((z.scan_n) > (4))) || ((z.scan_n) > (z.s.img_n))) return (int)(stbi__err("bad SOS component count"));
			if (Ls != 6 + 2 * z.scan_n) return (int)(stbi__err("bad SOS len"));
			for (i = (int)(0); (i) < (z.scan_n); ++i) {
int id = (int)(stbi__get8(z.s));int which = 0;int q = (int)(stbi__get8(z.s));for (which = (int)(0); (which) < (z.s.img_n); ++which) {if ((z.img_comp[which].id) == (id)) break;}if ((which) == (z.s.img_n)) return (int)(0);z.img_comp[which].hd = (int)(q >> 4);if ((z.img_comp[which].hd) > (3)) return (int)(stbi__err("bad DC huff"));z.img_comp[which].ha = (int)(q & 15);if ((z.img_comp[which].ha) > (3)) return (int)(stbi__err("bad AC huff"));z.order[i] = (int)(which);}
			{
int aa = 0;z.spec_start = (int)(stbi__get8(z.s));z.spec_end = (int)(stbi__get8(z.s));aa = (int)(stbi__get8(z.s));z.succ_high = (int)(aa >> 4);z.succ_low = (int)(aa & 15);if ((z.progressive) != 0) {
if ((((((z.spec_start) > (63)) || ((z.spec_end) > (63))) || ((z.spec_start) > (z.spec_end))) || ((z.succ_high) > (13))) || ((z.succ_low) > (13))) return (int)(stbi__err("bad SOS"));}
 else {
if (z.spec_start != 0) return (int)(stbi__err("bad SOS"));if ((z.succ_high != 0) || (z.succ_low != 0)) return (int)(stbi__err("bad SOS"));z.spec_end = (int)(63);}
}

			return (int)(1);
		}

		public static int stbi__process_frame_header(stbi__jpeg z, int scan)
		{
			stbi__context s = z.s;
			int Lf = 0;int p = 0;int i = 0;int q = 0;int h_max = (int)(1);int v_max = (int)(1);int c = 0;
			Lf = (int)(stbi__get16be(s));
			if ((Lf) < (11)) return (int)(stbi__err("bad SOF len"));
			p = (int)(stbi__get8(s));
			if (p != 8) return (int)(stbi__err("only 8-bit"));
			s.img_y = (uint)(stbi__get16be(s));
			if ((s.img_y) == (0)) return (int)(stbi__err("no header height"));
			s.img_x = (uint)(stbi__get16be(s));
			if ((s.img_x) == (0)) return (int)(stbi__err("0 width"));
			c = (int)(stbi__get8(s));
			if ((c != 3) && (c != 1)) return (int)(stbi__err("bad component count"));
			s.img_n = (int)(c);
			for (i = (int)(0); (i) < (c); ++i) {
z.img_comp[i].data = (null);z.img_comp[i].linebuf = (null);}
			if (Lf != 8 + 3 * s.img_n) return (int)(stbi__err("bad SOF len"));
			for (i = (int)(0); (i) < (s.img_n); ++i) {
z.img_comp[i].id = (int)(stbi__get8(s));if (z.img_comp[i].id != i + 1) if (z.img_comp[i].id != i) return (int)(stbi__err("bad component ID"));q = (int)(stbi__get8(s));z.img_comp[i].h = (int)(q >> 4);if ((z.img_comp[i].h== 0) || ((z.img_comp[i].h) > (4))) return (int)(stbi__err("bad H"));z.img_comp[i].v = (int)(q & 15);if ((z.img_comp[i].v== 0) || ((z.img_comp[i].v) > (4))) return (int)(stbi__err("bad V"));z.img_comp[i].tq = (int)(stbi__get8(s));if ((z.img_comp[i].tq) > (3)) return (int)(stbi__err("bad TQ"));}
			if (scan != STBI__SCAN_load) return (int)(1);
			if (((1 << 30) / s.img_x / s.img_n) < (s.img_y)) return (int)(stbi__err("too large"));
			for (i = (int)(0); (i) < (s.img_n); ++i) {
if ((z.img_comp[i].h) > (h_max)) h_max = (int)(z.img_comp[i].h);if ((z.img_comp[i].v) > (v_max)) v_max = (int)(z.img_comp[i].v);}
			z.img_h_max = (int)(h_max);
			z.img_v_max = (int)(v_max);
			z.img_mcu_w = (int)(h_max * 8);
			z.img_mcu_h = (int)(v_max * 8);
			z.img_mcu_x = (int)((s.img_x + z.img_mcu_w - 1) / z.img_mcu_w);
			z.img_mcu_y = (int)((s.img_y + z.img_mcu_h - 1) / z.img_mcu_h);
			for (i = (int)(0); (i) < (s.img_n); ++i) {
z.img_comp[i].x = (int)((s.img_x * z.img_comp[i].h + h_max - 1) / h_max);z.img_comp[i].y = (int)((s.img_y * z.img_comp[i].v + v_max - 1) / v_max);z.img_comp[i].w2 = (int)(z.img_mcu_x * z.img_comp[i].h * 8);z.img_comp[i].h2 = (int)(z.img_mcu_y * z.img_comp[i].v * 8);z.img_comp[i].raw_data = stbi__malloc((ulong)(z.img_comp[i].w2 * z.img_comp[i].h2 + 15));if ((z.img_comp[i].raw_data) == (null)) {
for (--i; (i) >= (0); --i) {
CRuntime.free(z.img_comp[i].raw_data);z.img_comp[i].raw_data = (null);}return (int)(stbi__err("outofmem"));}
z.img_comp[i].data = (byte*)((((long)z.img_comp[i].raw_data + 15) & ~15));z.img_comp[i].linebuf = (null);if ((z.progressive) != 0) {
z.img_comp[i].coeff_w = (int)((z.img_comp[i].w2 + 7) >> 3);z.img_comp[i].coeff_h = (int)((z.img_comp[i].h2 + 7) >> 3);z.img_comp[i].raw_coeff = CRuntime.malloc((ulong)(z.img_comp[i].coeff_w * z.img_comp[i].coeff_h * 64 * sizeof(short) + 15));z.img_comp[i].coeff = (short*)((((long)z.img_comp[i].raw_coeff + 15) & ~15));}
 else {
z.img_comp[i].coeff = null;z.img_comp[i].raw_coeff = null;}
}
			return (int)(1);
		}

		public static int stbi__decode_jpeg_header(stbi__jpeg z, int scan)
		{
			int m = 0;
			z.marker = (byte)(0xff);
			m = (int)(stbi__get_marker(z));
			if (!((m) == (0xd8))) return (int)(stbi__err("no SOI"));
			if ((scan) == (STBI__SCAN_type)) return (int)(1);
			m = (int)(stbi__get_marker(z));
			while (!((((m) == (0xc0)) || ((m) == (0xc1))) || ((m) == (0xc2)))) {
if (stbi__process_marker(z, (int)(m))== 0) return (int)(0);m = (int)(stbi__get_marker(z));while ((m) == (0xff)) {
if ((stbi__at_eof(z.s)) != 0) return (int)(stbi__err("no SOF"));m = (int)(stbi__get_marker(z));}}
			z.progressive = (int)((m) == (0xc2)?1:0);
			if (stbi__process_frame_header(z, (int)(scan))== 0) return (int)(0);
			return (int)(1);
		}

		public static int stbi__decode_jpeg_image(stbi__jpeg j)
		{
			int m = 0;
			for (m = (int)(0); (m) < (4); m++) {
j.img_comp[m].raw_data = (null);j.img_comp[m].raw_coeff = (null);}
			j.restart_interval = (int)(0);
			if (stbi__decode_jpeg_header(j, (int)(STBI__SCAN_load))== 0) return (int)(0);
			m = (int)(stbi__get_marker(j));
			while (!((m) == (0xd9))) {
if (((m) == (0xda))) {
if (stbi__process_scan_header(j)== 0) return (int)(0);if (stbi__parse_entropy_coded_data(j)== 0) return (int)(0);if ((j.marker) == (0xff)) {
while (stbi__at_eof(j.s)== 0) {
int x = (int)(stbi__get8(j.s));if ((x) == (255)) {
j.marker = (byte)(stbi__get8(j.s));break;}
 else if (x != 0) {
return (int)(stbi__err("junk before marker"));}
}}
}
 else {
if (stbi__process_marker(j, (int)(m))== 0) return (int)(0);}
m = (int)(stbi__get_marker(j));}
			if ((j.progressive) != 0) stbi__jpeg_finish(j);
			return (int)(1);
		}

		public static byte* resample_row_1(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			(void)(_out_);
			(void)(in_far);
			(void)(w);
			(void)(hs);
			return in_near;
		}

		public static byte* stbi__resample_row_v_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			int i = 0;
			(void)(hs);
			for (i = (int)(0); (i) < (w); ++i) {_out_[i] = ((byte)((3 * in_near[i] + in_far[i] + 2) >> 2));}
			return _out_;
		}

		public static byte* stbi__resample_row_h_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			int i = 0;
			byte* input = in_near;
			if ((w) == (1)) {
_out_[0] = (byte)(_out_[1] = (byte)(input[0]));return _out_;}

			_out_[0] = (byte)(input[0]);
			_out_[1] = ((byte)((input[0] * 3 + input[1] + 2) >> 2));
			for (i = (int)(1); (i) < (w - 1); ++i) {
int n = (int)(3 * input[i] + 2);_out_[i * 2 + 0] = ((byte)((n + input[i - 1]) >> 2));_out_[i * 2 + 1] = ((byte)((n + input[i + 1]) >> 2));}
			_out_[i * 2 + 0] = ((byte)((input[w - 2] * 3 + input[w - 1] + 2) >> 2));
			_out_[i * 2 + 1] = (byte)(input[w - 1]);
			(void)(in_far);
			(void)(hs);
			return _out_;
		}

		public static byte* stbi__resample_row_hv_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			int i = 0;int t0 = 0;int t1 = 0;
			if ((w) == (1)) {
_out_[0] = (byte)(_out_[1] = ((byte)((3 * in_near[0] + in_far[0] + 2) >> 2)));return _out_;}

			t1 = (int)(3 * in_near[0] + in_far[0]);
			_out_[0] = ((byte)((t1 + 2) >> 2));
			for (i = (int)(1); (i) < (w); ++i) {
t0 = (int)(t1);t1 = (int)(3 * in_near[i] + in_far[i]);_out_[i * 2 - 1] = ((byte)((3 * t0 + t1 + 8) >> 4));_out_[i * 2] = ((byte)((3 * t1 + t0 + 8) >> 4));}
			_out_[w * 2 - 1] = ((byte)((t1 + 2) >> 2));
			(void)(hs);
			return _out_;
		}

		public static byte* stbi__resample_row_generic(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			int i = 0;int j = 0;
			(void)(in_far);
			for (i = (int)(0); (i) < (w); ++i) {for (j = (int)(0); (j) < (hs); ++j) {_out_[i * hs + j] = (byte)(in_near[i]);}}
			return _out_;
		}

		public static void stbi__YCbCr_to_RGB_row(byte* _out_, byte* y, byte* pcb, byte* pcr, int count, int step)
		{
			int i = 0;
			for (i = (int)(0); (i) < (count); ++i) {
int y_fixed = (int)((y[i] << 20) + (1 << 19));int r = 0;int g = 0;int b = 0;int cr = (int)(pcr[i] - 128);int cb = (int)(pcb[i] - 128);r = (int)(y_fixed + cr * (((int)((1.40200f) * 4096.0f + 0.5f)) << 8));g = (int)(y_fixed + (cr * -(((int)((0.71414f) * 4096.0f + 0.5f)) << 8)) + ((cb * -(((int)((0.34414f) * 4096.0f + 0.5f)) << 8)) & 0xffff0000));b = (int)(y_fixed + cb * (((int)((1.77200f) * 4096.0f + 0.5f)) << 8));r >>= 20;g >>= 20;b >>= 20;if (((uint)(r)) > (255)) {
if ((r) < (0)) r = (int)(0); else r = (int)(255);}
if (((uint)(g)) > (255)) {
if ((g) < (0)) g = (int)(0); else g = (int)(255);}
if (((uint)(b)) > (255)) {
if ((b) < (0)) b = (int)(0); else b = (int)(255);}
_out_[0] = ((byte)(r));_out_[1] = ((byte)(g));_out_[2] = ((byte)(b));_out_[3] = (byte)(255);_out_ += step;}
		}

		public static void stbi__setup_jpeg(stbi__jpeg j)
		{
			j.idct_block_kernel = stbi__idct_block;
			j.YCbCr_to_RGB_kernel = stbi__YCbCr_to_RGB_row;
			j.resample_row_hv_2_kernel = stbi__resample_row_hv_2;
		}

		public static void stbi__cleanup_jpeg(stbi__jpeg j)
		{
			int i = 0;
			for (i = (int)(0); (i) < (j.s.img_n); ++i) {
if ((j.img_comp[i].raw_data) != null) {
CRuntime.free(j.img_comp[i].raw_data);j.img_comp[i].raw_data = (null);j.img_comp[i].data = (null);}
if ((j.img_comp[i].raw_coeff) != null) {
CRuntime.free(j.img_comp[i].raw_coeff);j.img_comp[i].raw_coeff = null;j.img_comp[i].coeff = null;}
if ((j.img_comp[i].linebuf) != null) {
CRuntime.free(j.img_comp[i].linebuf);j.img_comp[i].linebuf = (null);}
}
		}

		public static byte* load_jpeg_image(stbi__jpeg z, int* out_x, int* out_y, int* comp, int req_comp)
		{
			int n = 0;int decode_n = 0;
			z.s.img_n = (int)(0);
			if (((req_comp) < (0)) || ((req_comp) > (4))) return ((byte*)((ulong)((stbi__err("bad req_comp")) != 0?((byte *)null):(null))));
			if (stbi__decode_jpeg_image(z)== 0) {
stbi__cleanup_jpeg(z);return (null);}

			n = (int)((req_comp) != 0?req_comp:z.s.img_n);
			if (((z.s.img_n) == (3)) && ((n) < (3))) decode_n = (int)(1); else decode_n = (int)(z.s.img_n);
			{
int k = 0;uint i = 0;uint j = 0;byte* output;byte** coutput = stackalloc byte *[4];stbi__resample res_comp = FakePtr<stbi__resample>.CreateWithSize(4);for (k = (int)(0); (k) < (decode_n); ++k) {
stbi__resample r = res_comp[k];z.img_comp[k].linebuf = (byte*)(stbi__malloc((ulong)(z.s.img_x + 3)));if (z.img_comp[k].linebuf== null) {
stbi__cleanup_jpeg(z);return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));}
r.hs = (int)(z.img_h_max / z.img_comp[k].h);r.vs = (int)(z.img_v_max / z.img_comp[k].v);r.ystep = (int)(r.vs >> 1);r.w_lores = (int)((z.s.img_x + r.hs - 1) / r.hs);r.ypos = (int)(0);r.line0 = r.line1 = z.img_comp[k].data;if (((r.hs) == (1)) && ((r.vs) == (1))) r.resample = resample_row_1; else if (((r.hs) == (1)) && ((r.vs) == (2))) r.resample = stbi__resample_row_v_2; else if (((r.hs) == (2)) && ((r.vs) == (1))) r.resample = stbi__resample_row_h_2; else if (((r.hs) == (2)) && ((r.vs) == (2))) r.resample = z.resample_row_hv_2_kernel; else r.resample = stbi__resample_row_generic;}output = (byte*)(stbi__malloc((ulong)(n * z.s.img_x * z.s.img_y + 1)));if (output== null) {
stbi__cleanup_jpeg(z);return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));}
for (j = (uint)(0); (j) < (z.s.img_y); ++j) {
byte* _out_ = output + n * z.s.img_x * j;for (k = (int)(0); (k) < (decode_n); ++k) {
stbi__resample r = res_comp[k];int y_bot = (int)((r.ystep) >= (r.vs >> 1)?1:0);coutput[k] = r.resample(z.img_comp[k].linebuf, (y_bot) != 0?r.line1:r.line0, (y_bot) != 0?r.line0:r.line1, (int)(r.w_lores), (int)(r.hs));if ((++r.ystep) >= (r.vs)) {
r.ystep = (int)(0);r.line0 = r.line1;if ((++r.ypos) < (z.img_comp[k].y)) r.line1 += z.img_comp[k].w2;}
}if ((n) >= (3)) {
byte* y = coutput[0];if ((z.s.img_n) == (3)) {
z.YCbCr_to_RGB_kernel(_out_, y, coutput[1], coutput[2], (int)(z.s.img_x), (int)(n));}
 else for (i = (uint)(0); (i) < (z.s.img_x); ++i) {
_out_[0] = (byte)(_out_[1] = (byte)(_out_[2] = (byte)(y[i])));_out_[3] = (byte)(255);_out_ += n;}}
 else {
byte* y = coutput[0];if ((n) == (1)) for (i = (uint)(0); (i) < (z.s.img_x); ++i) {_out_[i] = (byte)(y[i]);} else for (i = (uint)(0); (i) < (z.s.img_x); ++i) {*_out_++ = (byte)(y[i]);*_out_++ = (byte)(255);}}
}stbi__cleanup_jpeg(z);*out_x = (int)(z.s.img_x);*out_y = (int)(z.s.img_y);if ((comp) != null) *comp = (int)(z.s.img_n);return output;}

		}

		public static byte* stbi__jpeg_load(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			stbi__jpeg j =  new stbi__jpeg();
			j.s = s;
			stbi__setup_jpeg(j);
			return load_jpeg_image(j, x, y, comp, (int)(req_comp));
		}

		public static int stbi__jpeg_test(stbi__context s)
		{
			int r = 0;
			stbi__jpeg j =  new stbi__jpeg();
			j.s = s;
			stbi__setup_jpeg(j);
			r = (int)(stbi__decode_jpeg_header(j, (int)(STBI__SCAN_type)));
			stbi__rewind(s);
			return (int)(r);
		}

		public static int stbi__jpeg_info_raw(stbi__jpeg j, int* x, int* y, int* comp)
		{
			if (stbi__decode_jpeg_header(j, (int)(STBI__SCAN_header))== 0) {
stbi__rewind(j.s);return (int)(0);}

			if ((x) != null) *x = (int)(j.s.img_x);
			if ((y) != null) *y = (int)(j.s.img_y);
			if ((comp) != null) *comp = (int)(j.s.img_n);
			return (int)(1);
		}

		public static int stbi__jpeg_info(stbi__context s, int* x, int* y, int* comp)
		{
			stbi__jpeg j =  new stbi__jpeg();
			j.s = s;
			return (int)(stbi__jpeg_info_raw(j, x, y, comp));
		}

		public static int stbi__bitreverse16(int n)
		{
			n = (int)(((n & 0xAAAA) >> 1) | ((n & 0x5555) << 1));
			n = (int)(((n & 0xCCCC) >> 2) | ((n & 0x3333) << 2));
			n = (int)(((n & 0xF0F0) >> 4) | ((n & 0x0F0F) << 4));
			n = (int)(((n & 0xFF00) >> 8) | ((n & 0x00FF) << 8));
			return (int)(n);
		}

		public static int stbi__bit_reverse(int v, int bits)
		{
			(void)((!!(bits <= 16)) || (_wassert("bits <= 16", "nanovg/stb_image.h", (uint)(3484)) , 0));
			return (int)(stbi__bitreverse16((int)(v)) >> (16 - bits));
		}

		public static int stbi__zbuild_huffman(stbi__zhuffman* z, byte* sizelist, int num)
		{
			int i = 0;int k = (int)(0);
			int code = 0;int* next_code = stackalloc int[16];int* sizes = stackalloc int[17];
			CRuntime.memset(sizes, (int)(0), (ulong)(sizeof(int)));
			CRuntime.memset(((ushort*)(z->fast)), (int)(0), (ulong)((1 << 9) * sizeof(ushort)));
			for (i = (int)(0); (i) < (num); ++i) {++sizes[sizelist[i]];}
			sizes[0] = (int)(0);
			for (i = (int)(1); (i) < (16); ++i) {if ((sizes[i]) > (1 << i)) return (int)(stbi__err("bad sizes"));}
			code = (int)(0);
			for (i = (int)(1); (i) < (16); ++i) {
next_code[i] = (int)(code);z->firstcode[i] = ((ushort)(code));z->firstsymbol[i] = ((ushort)(k));code = (int)(code + sizes[i]);if ((sizes[i]) != 0) if ((code - 1) >= (1 << i)) return (int)(stbi__err("bad codelengths"));z->maxcode[i] = (int)(code << (16 - i));code <<= 1;k += (int)(sizes[i]);}
			z->maxcode[16] = (int)(0x10000);
			for (i = (int)(0); (i) < (num); ++i) {
int s = (int)(sizelist[i]);if ((s) != 0) {
int c = (int)(next_code[s] - z->firstcode[s] + z->firstsymbol[s]);ushort fastv = (ushort)((s << 9) | i);z->size[c] = ((byte)(s));z->value[c] = ((ushort)(i));if (s <= 9) {
int j = (int)(stbi__bit_reverse((int)(next_code[s]), (int)(s)));while ((j) < (1 << 9)) {
z->fast[j] = (ushort)(fastv);j += (int)(1 << s);}}
++next_code[s];}
}
			return (int)(1);
		}

		public static byte stbi__zget8(stbi__zbuf* z)
		{
			if ((z->zbuffer) >= (z->zbuffer_end)) return (byte)(0);
			return (byte)(*z->zbuffer++);
		}

		public static void stbi__fill_bits(stbi__zbuf* z)
		{
			do {
(void)((!!((z->code_buffer) < (1U << z->num_bits))) || (_wassert("z->code_buffer < (1U << z->num_bits)", "nanovg/stb_image.h", (uint)(3566)) , 0));z->code_buffer |= (uint)((uint)(stbi__zget8(z)) << z->num_bits);z->num_bits += (int)(8);}
 while (z->num_bits <= 24);
		}

		public static uint stbi__zreceive(stbi__zbuf* z, int n)
		{
			uint k = 0;
			if ((z->num_bits) < (n)) stbi__fill_bits(z);
			k = (uint)(z->code_buffer & ((1 << n) - 1));
			z->code_buffer >>= n;
			z->num_bits -= (int)(n);
			return (uint)(k);
		}

		public static int stbi__zhuffman_decode_slowpath(stbi__zbuf* a, stbi__zhuffman* z)
		{
			int b = 0;int s = 0;int k = 0;
			k = (int)(stbi__bit_reverse((int)(a->code_buffer), (int)(16)));
			for (s = (int)(9 + 1); ; ++s) {if ((k) < (z->maxcode[s])) break;}
			if ((s) == (16)) return (int)(-1);
			b = (int)((k >> (16 - s)) - z->firstcode[s] + z->firstsymbol[s]);
			(void)((!!((z->size[b]) == (s))) || (_wassert("z->size[b] == s", "nanovg/stb_image.h", (uint)(3594)) , 0));
			a->code_buffer >>= s;
			a->num_bits -= (int)(s);
			return (int)(z->value[b]);
		}

		public static int stbi__zhuffman_decode(stbi__zbuf* a, stbi__zhuffman* z)
		{
			int b = 0;int s = 0;
			if ((a->num_bits) < (16)) stbi__fill_bits(a);
			b = (int)(z->fast[a->code_buffer & ((1 << 9) - 1)]);
			if ((b) != 0) {
s = (int)(b >> 9);a->code_buffer >>= s;a->num_bits -= (int)(s);return (int)(b & 511);}

			return (int)(stbi__zhuffman_decode_slowpath(a, z));
		}

		public static int stbi__zexpand(stbi__zbuf* z, sbyte* zout, int n)
		{
			sbyte* q;
			int cur = 0;int limit = 0;int old_limit = 0;
			z->zout = zout;
			if (z->z_expandable== 0) return (int)(stbi__err("output buffer limit"));
			cur = ((int)(z->zout - z->zout_start));
			limit = (int)(old_limit = ((int)(z->zout_end - z->zout_start)));
			while ((cur + n) > (limit)) {limit *= (int)(2);}
			q = (sbyte*)(CRuntime.realloc(z->zout_start, (ulong)(limit)));
			(void)(old_limit);
			if ((q) == (null)) return (int)(stbi__err("outofmem"));
			z->zout_start = q;
			z->zout = q + cur;
			z->zout_end = q + limit;
			return (int)(1);
		}

		public static int stbi__parse_huffman_block(stbi__zbuf* a)
		{
			sbyte* zout = a->zout;
			for (; ; ) {
int z = (int)(stbi__zhuffman_decode(a, &a->z_length));if ((z) < (256)) {
if ((z) < (0)) return (int)(stbi__err("bad huffman code"));if ((zout) >= (a->zout_end)) {
if (stbi__zexpand(a, zout, (int)(1))== 0) return (int)(0);zout = a->zout;}
*zout++ = ((sbyte)(z));}
 else {
byte* p;int len = 0;int dist = 0;if ((z) == (256)) {
a->zout = zout;return (int)(1);}
z -= (int)(257);len = (int)(stbi__zlength_base[z]);if ((stbi__zlength_extra[z]) != 0) len += (int)(stbi__zreceive(a, (int)(stbi__zlength_extra[z])));z = (int)(stbi__zhuffman_decode(a, &a->z_distance));if ((z) < (0)) return (int)(stbi__err("bad huffman code"));dist = (int)(stbi__zdist_base[z]);if ((stbi__zdist_extra[z]) != 0) dist += (int)(stbi__zreceive(a, (int)(stbi__zdist_extra[z])));if ((zout - a->zout_start) < (dist)) return (int)(stbi__err("bad dist"));if ((zout + len) > (a->zout_end)) {
if (stbi__zexpand(a, zout, (int)(len))== 0) return (int)(0);zout = a->zout;}
p = (byte*)(zout - dist);if ((dist) == (1)) {
byte v = (byte)(*p);if ((len) != 0) {
do *zout++ = (sbyte)(v); while ((--len) != 0);}
}
 else {
if ((len) != 0) {
do *zout++ = (sbyte)(*p++); while ((--len) != 0);}
}
}
}
		}

		public static int stbi__compute_huffman_codes(stbi__zbuf* a)
		{
			byte* length_dezigzag = stackalloc byte[19];
length_dezigzag[0] = (byte)(16);
length_dezigzag[1] = (byte)(17);
length_dezigzag[2] = (byte)(18);
length_dezigzag[3] = (byte)(0);
length_dezigzag[4] = (byte)(8);
length_dezigzag[5] = (byte)(7);
length_dezigzag[6] = (byte)(9);
length_dezigzag[7] = (byte)(6);
length_dezigzag[8] = (byte)(10);
length_dezigzag[9] = (byte)(5);
length_dezigzag[10] = (byte)(11);
length_dezigzag[11] = (byte)(4);
length_dezigzag[12] = (byte)(12);
length_dezigzag[13] = (byte)(3);
length_dezigzag[14] = (byte)(13);
length_dezigzag[15] = (byte)(2);
length_dezigzag[16] = (byte)(14);
length_dezigzag[17] = (byte)(1);
length_dezigzag[18] = (byte)(15);

			stbi__zhuffman z_codelength =  new stbi__zhuffman();
			byte* lencodes = stackalloc byte[286 + 32 + 137];
			byte* codelength_sizes = stackalloc byte[19];
			int i = 0;int n = 0;
			int hlit = (int)(stbi__zreceive(a, (int)(5)) + 257);
			int hdist = (int)(stbi__zreceive(a, (int)(5)) + 1);
			int hclen = (int)(stbi__zreceive(a, (int)(4)) + 4);
			CRuntime.memset(((byte*)(codelength_sizes)), (int)(0), (ulong)(19 * sizeof(byte)));
			for (i = (int)(0); (i) < (hclen); ++i) {
int s = (int)(stbi__zreceive(a, (int)(3)));codelength_sizes[length_dezigzag[i]] = ((byte)(s));}
			if (stbi__zbuild_huffman(&z_codelength, codelength_sizes, (int)(19))== 0) return (int)(0);
			n = (int)(0);
			while ((n) < (hlit + hdist)) {
int c = (int)(stbi__zhuffman_decode(a, &z_codelength));if (((c) < (0)) || ((c) >= (19))) return (int)(stbi__err("bad codelengths"));if ((c) < (16)) lencodes[n++] = ((byte)(c)); else if ((c) == (16)) {
c = (int)(stbi__zreceive(a, (int)(2)) + 3);CRuntime.memset(lencodes + n, (int)(lencodes[n - 1]), (ulong)(c));n += (int)(c);}
 else if ((c) == (17)) {
c = (int)(stbi__zreceive(a, (int)(3)) + 3);CRuntime.memset(lencodes + n, (int)(0), (ulong)(c));n += (int)(c);}
 else {
(void)((!!((c) == (18))) || (_wassert("c == 18", "nanovg/stb_image.h", (uint)(3723)) , 0));c = (int)(stbi__zreceive(a, (int)(7)) + 11);CRuntime.memset(lencodes + n, (int)(0), (ulong)(c));n += (int)(c);}
}
			if (n != hlit + hdist) return (int)(stbi__err("bad codelengths"));
			if (stbi__zbuild_huffman(&a->z_length, lencodes, (int)(hlit))== 0) return (int)(0);
			if (stbi__zbuild_huffman(&a->z_distance, lencodes + hlit, (int)(hdist))== 0) return (int)(0);
			return (int)(1);
		}

		public static int stbi__parse_uncomperssed_block(stbi__zbuf* a)
		{
			byte* header = stackalloc byte[4];
			int len = 0;int nlen = 0;int k = 0;
			if ((a->num_bits & 7) != 0) stbi__zreceive(a, (int)(a->num_bits & 7));
			k = (int)(0);
			while ((a->num_bits) > (0)) {
header[k++] = ((byte)(a->code_buffer & 255));a->code_buffer >>= 8;a->num_bits -= (int)(8);}
			(void)((!!((a->num_bits) == (0))) || (_wassert("a->num_bits == 0", "nanovg/stb_image.h", (uint)(3748)) , 0));
			while ((k) < (4)) {header[k++] = (byte)(stbi__zget8(a));}
			len = (int)(header[1] * 256 + header[0]);
			nlen = (int)(header[3] * 256 + header[2]);
			if (nlen != (len ^ 0xffff)) return (int)(stbi__err("zlib corrupt"));
			if ((a->zbuffer + len) > (a->zbuffer_end)) return (int)(stbi__err("read past buffer"));
			if ((a->zout + len) > (a->zout_end)) if (stbi__zexpand(a, a->zout, (int)(len))== 0) return (int)(0);
			CRuntime.memcpy(a->zout, a->zbuffer, (ulong)(len));
			a->zbuffer += len;
			a->zout += len;
			return (int)(1);
		}

		public static int stbi__parse_zlib_header(stbi__zbuf* a)
		{
			int cmf = (int)(stbi__zget8(a));
			int cm = (int)(cmf & 15);
			int flg = (int)(stbi__zget8(a));
			if ((cmf * 256 + flg) % 31 != 0) return (int)(stbi__err("bad zlib header"));
			if ((flg & 32) != 0) return (int)(stbi__err("no preset dict"));
			if (cm != 8) return (int)(stbi__err("bad compression"));
			return (int)(1);
		}

		public static void stbi__init_zdefaults()
		{
			int i = 0;
			for (i = (int)(0); i <= 143; ++i) {stbi__zdefault_length[i] = (byte)(8);}
			for (; i <= 255; ++i) {stbi__zdefault_length[i] = (byte)(9);}
			for (; i <= 279; ++i) {stbi__zdefault_length[i] = (byte)(7);}
			for (; i <= 287; ++i) {stbi__zdefault_length[i] = (byte)(8);}
			for (i = (int)(0); i <= 31; ++i) {stbi__zdefault_distance[i] = (byte)(5);}
		}

		public static int stbi__parse_zlib(stbi__zbuf* a, int parse_header)
		{
			int final = 0;int type = 0;
			if ((parse_header) != 0) if (stbi__parse_zlib_header(a)== 0) return (int)(0);
			a->num_bits = (int)(0);
			a->code_buffer = (uint)(0);
			do {
final = (int)(stbi__zreceive(a, (int)(1)));type = (int)(stbi__zreceive(a, (int)(2)));if ((type) == (0)) {
if (stbi__parse_uncomperssed_block(a)== 0) return (int)(0);}
 else if ((type) == (3)) {
return (int)(0);}
 else {
if ((type) == (1)) {
if (stbi__zdefault_distance[31]== 0) stbi__init_zdefaults();fixed (byte* b = stbi__zdefault_length) {if (stbi__zbuild_huffman(&a->z_length, b, (int) (288)) == 0) return (int) (0);}fixed (byte* b = stbi__zdefault_distance) {if (stbi__zbuild_huffman(&a->z_distance, b, (int) (32)) == 0) return (int) (0);}}
 else {
if (stbi__compute_huffman_codes(a)== 0) return (int)(0);}
if (stbi__parse_huffman_block(a)== 0) return (int)(0);}
}
 while (final== 0);
			return (int)(1);
		}

		public static int stbi__do_zlib(stbi__zbuf* a, sbyte* obuf, int olen, int exp, int parse_header)
		{
			a->zout_start = obuf;
			a->zout = obuf;
			a->zout_end = obuf + olen;
			a->z_expandable = (int)(exp);
			return (int)(stbi__parse_zlib(a, (int)(parse_header)));
		}

		public static sbyte* stbi_zlib_decode_malloc_guesssize(sbyte* buffer, int len, int initial_size, int* outlen)
		{
			stbi__zbuf a =  new stbi__zbuf();
			sbyte* p = (sbyte*)(stbi__malloc((ulong)(initial_size)));
			if ((p) == (null)) return (null);
			a.zbuffer = (byte*)(buffer);
			a.zbuffer_end = (byte*)(buffer) + len;
			if ((stbi__do_zlib(&a, p, (int)(initial_size), (int)(1), (int)(1))) != 0) {
if ((outlen) != null) *outlen = ((int)(a.zout - a.zout_start));return a.zout_start;}
 else {
CRuntime.free(a.zout_start);return (null);}

		}

		public static sbyte* stbi_zlib_decode_malloc(sbyte* buffer, int len, int* outlen)
		{
			return stbi_zlib_decode_malloc_guesssize(buffer, (int)(len), (int)(16384), outlen);
		}

		public static sbyte* stbi_zlib_decode_malloc_guesssize_headerflag(sbyte* buffer, int len, int initial_size, int* outlen, int parse_header)
		{
			stbi__zbuf a =  new stbi__zbuf();
			sbyte* p = (sbyte*)(stbi__malloc((ulong)(initial_size)));
			if ((p) == (null)) return (null);
			a.zbuffer = (byte*)(buffer);
			a.zbuffer_end = (byte*)(buffer) + len;
			if ((stbi__do_zlib(&a, p, (int)(initial_size), (int)(1), (int)(parse_header))) != 0) {
if ((outlen) != null) *outlen = ((int)(a.zout - a.zout_start));return a.zout_start;}
 else {
CRuntime.free(a.zout_start);return (null);}

		}

		public static int stbi_zlib_decode_buffer(sbyte* obuffer, int olen, sbyte* ibuffer, int ilen)
		{
			stbi__zbuf a =  new stbi__zbuf();
			a.zbuffer = (byte*)(ibuffer);
			a.zbuffer_end = (byte*)(ibuffer) + ilen;
			if ((stbi__do_zlib(&a, obuffer, (int)(olen), (int)(0), (int)(1))) != 0) return (int)(a.zout - a.zout_start); else return (int)(-1);
		}

		public static sbyte* stbi_zlib_decode_noheader_malloc(sbyte* buffer, int len, int* outlen)
		{
			stbi__zbuf a =  new stbi__zbuf();
			sbyte* p = (sbyte*)(stbi__malloc((ulong)(16384)));
			if ((p) == (null)) return (null);
			a.zbuffer = (byte*)(buffer);
			a.zbuffer_end = (byte*)(buffer) + len;
			if ((stbi__do_zlib(&a, p, (int)(16384), (int)(1), (int)(0))) != 0) {
if ((outlen) != null) *outlen = ((int)(a.zout - a.zout_start));return a.zout_start;}
 else {
CRuntime.free(a.zout_start);return (null);}

		}

		public static int stbi_zlib_decode_noheader_buffer(sbyte* obuffer, int olen, sbyte* ibuffer, int ilen)
		{
			stbi__zbuf a =  new stbi__zbuf();
			a.zbuffer = (byte*)(ibuffer);
			a.zbuffer_end = (byte*)(ibuffer) + ilen;
			if ((stbi__do_zlib(&a, obuffer, (int)(olen), (int)(0), (int)(0))) != 0) return (int)(a.zout - a.zout_start); else return (int)(-1);
		}

		public static stbi__pngchunk stbi__get_chunk_header(stbi__context s)
		{
			stbi__pngchunk c =  new stbi__pngchunk();
			c.length = (uint)(stbi__get32be(s));
			c.type = (uint)(stbi__get32be(s));
			return (stbi__pngchunk)(c);
		}

		public static int stbi__check_png_header(stbi__context s)
		{
			byte* png_sig = stackalloc byte[8];
png_sig[0] = (byte)(137);
png_sig[1] = (byte)(80);
png_sig[2] = (byte)(78);
png_sig[3] = (byte)(71);
png_sig[4] = (byte)(13);
png_sig[5] = (byte)(10);
png_sig[6] = (byte)(26);
png_sig[7] = (byte)(10);

			int i = 0;
			for (i = (int)(0); (i) < (8); ++i) {if (stbi__get8(s) != png_sig[i]) return (int)(stbi__err("bad png sig"));}
			return (int)(1);
		}

		public static int stbi__paeth(int a, int b, int c)
		{
			int p = (int)(a + b - c);
			int pa = (int)(CRuntime.abs((int)(p - a)));
			int pb = (int)(CRuntime.abs((int)(p - b)));
			int pc = (int)(CRuntime.abs((int)(p - c)));
			if ((pa <= pb) && (pa <= pc)) return (int)(a);
			if (pb <= pc) return (int)(b);
			return (int)(c);
		}

		public static int stbi__create_png_image_raw(stbi__png a, byte* raw, uint raw_len, int out_n, uint x, uint y, int depth, int color)
		{
			stbi__context s = a.s;
			uint i = 0;uint j = 0;uint stride = (uint)(x * out_n);
			uint img_len = 0;uint img_width_bytes = 0;
			int k = 0;
			int img_n = (int)(s.img_n);
			(void)((!!(((out_n) == (s.img_n)) || ((out_n) == (s.img_n + 1)))) || (_wassert("out_n == s->img_n || out_n == s->img_n+1", "nanovg/stb_image.h", (uint)(3988)) , 0));
			a._out_ = (byte*)(stbi__malloc((ulong)(x * y * out_n)));
			if (a._out_== null) return (int)(stbi__err("outofmem"));
			img_width_bytes = (uint)(((img_n * x * depth) + 7) >> 3);
			img_len = (uint)((img_width_bytes + 1) * y);
			if (((s.img_x) == (x)) && ((s.img_y) == (y))) {
if (raw_len != img_len) return (int)(stbi__err("not enough pixels"));}
 else {
if ((raw_len) < (img_len)) return (int)(stbi__err("not enough pixels"));}

			for (j = (uint)(0); (j) < (y); ++j) {
byte* cur = a._out_ + stride * j;byte* prior = cur - stride;int filter = (int)(*raw++);int filter_bytes = (int)(img_n);int width = (int)(x);if ((filter) > (4)) return (int)(stbi__err("invalid filter"));if ((depth) < (8)) {
(void)((!!(img_width_bytes <= x)) || (_wassert("img_width_bytes <= x", "nanovg/stb_image.h", (uint)(4010)) , 0));cur += x * out_n - img_width_bytes;filter_bytes = (int)(1);width = (int)(img_width_bytes);}
if ((j) == (0)) filter = (int)(first_row_filter[filter]);for (k = (int)(0); (k) < (filter_bytes); ++k) {
switch (filter){
case STBI__F_none:cur[k] = (byte)(raw[k]);break;case STBI__F_sub:cur[k] = (byte)(raw[k]);break;case STBI__F_up:cur[k] = ((byte)((raw[k] + prior[k]) & 255));break;case STBI__F_avg:cur[k] = ((byte)((raw[k] + (prior[k] >> 1)) & 255));break;case STBI__F_paeth:cur[k] = ((byte)((raw[k] + stbi__paeth((int)(0), (int)(prior[k]), (int)(0))) & 255));break;case STBI__F_avg_first:cur[k] = (byte)(raw[k]);break;case STBI__F_paeth_first:cur[k] = (byte)(raw[k]);break;}
}if ((depth) == (8)) {
if (img_n != out_n) cur[img_n] = (byte)(255);raw += img_n;cur += out_n;prior += out_n;}
 else {
raw += 1;cur += 1;prior += 1;}
if (((depth) < (8)) || ((img_n) == (out_n))) {
int nk = (int)((width - 1) * img_n);switch (filter){
case STBI__F_none:CRuntime.memcpy(cur, raw, (ulong)(nk));break;case STBI__F_sub:for (k = (int)(0); (k) < (nk); ++k) {cur[k] = ((byte)((raw[k] + cur[k - filter_bytes]) & 255));}break;case STBI__F_up:for (k = (int)(0); (k) < (nk); ++k) {cur[k] = ((byte)((raw[k] + prior[k]) & 255));}break;case STBI__F_avg:for (k = (int)(0); (k) < (nk); ++k) {cur[k] = ((byte)((raw[k] + ((prior[k] + cur[k - filter_bytes]) >> 1)) & 255));}break;case STBI__F_paeth:for (k = (int)(0); (k) < (nk); ++k) {cur[k] = ((byte)((raw[k] + stbi__paeth((int)(cur[k - filter_bytes]), (int)(prior[k]), (int)(prior[k - filter_bytes]))) & 255));}break;case STBI__F_avg_first:for (k = (int)(0); (k) < (nk); ++k) {cur[k] = ((byte)((raw[k] + (cur[k - filter_bytes] >> 1)) & 255));}break;case STBI__F_paeth_first:for (k = (int)(0); (k) < (nk); ++k) {cur[k] = ((byte)((raw[k] + stbi__paeth((int)(cur[k - filter_bytes]), (int)(0), (int)(0))) & 255));}break;}
raw += nk;}
 else {
(void)((!!((img_n + 1) == (out_n))) || (_wassert("img_n+1 == out_n", "nanovg/stb_image.h", (uint)(4063)) , 0));switch (filter){
case STBI__F_none:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = (byte)(raw[k]);}}break;case STBI__F_sub:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = ((byte)((raw[k] + cur[k - out_n]) & 255));}}break;case STBI__F_up:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = ((byte)((raw[k] + prior[k]) & 255));}}break;case STBI__F_avg:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = ((byte)((raw[k] + ((prior[k] + cur[k - out_n]) >> 1)) & 255));}}break;case STBI__F_paeth:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = ((byte)((raw[k] + stbi__paeth((int)(cur[k - out_n]), (int)(prior[k]), (int)(prior[k - out_n]))) & 255));}}break;case STBI__F_avg_first:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = ((byte)((raw[k] + (cur[k - out_n] >> 1)) & 255));}}break;case STBI__F_paeth_first:for (i = (uint)(x - 1); (i) >= (1); --i , cur[img_n] = (byte)(255) , raw += img_n , cur += out_n , prior += out_n) {for (k = (int)(0); (k) < (img_n); ++k) {cur[k] = ((byte)((raw[k] + stbi__paeth((int)(cur[k - out_n]), (int)(0), (int)(0))) & 255));}}break;}
}
}
			if ((depth) < (8)) {
for (j = (uint)(0); (j) < (y); ++j) {
byte* cur = a._out_ + stride * j;byte* _in_ = a._out_ + stride * j + x * out_n - img_width_bytes;byte scale = (byte)(((color) == (0))?stbi__depth_scale_table[depth]:1);if ((depth) == (4)) {
for (k = (int)(x * img_n); (k) >= (2); k -= (int)(2) , ++_in_) {
*cur++ = (byte)(scale * (*_in_ >> 4));*cur++ = (byte)(scale * ((*_in_) & 0x0f));}if ((k) > (0)) *cur++ = (byte)(scale * (*_in_ >> 4));}
 else if ((depth) == (2)) {
for (k = (int)(x * img_n); (k) >= (4); k -= (int)(4) , ++_in_) {
*cur++ = (byte)(scale * (*_in_ >> 6));*cur++ = (byte)(scale * ((*_in_ >> 4) & 0x03));*cur++ = (byte)(scale * ((*_in_ >> 2) & 0x03));*cur++ = (byte)(scale * ((*_in_) & 0x03));}if ((k) > (0)) *cur++ = (byte)(scale * (*_in_ >> 6));if ((k) > (1)) *cur++ = (byte)(scale * ((*_in_ >> 4) & 0x03));if ((k) > (2)) *cur++ = (byte)(scale * ((*_in_ >> 2) & 0x03));}
 else if ((depth) == (1)) {
for (k = (int)(x * img_n); (k) >= (8); k -= (int)(8) , ++_in_) {
*cur++ = (byte)(scale * (*_in_ >> 7));*cur++ = (byte)(scale * ((*_in_ >> 6) & 0x01));*cur++ = (byte)(scale * ((*_in_ >> 5) & 0x01));*cur++ = (byte)(scale * ((*_in_ >> 4) & 0x01));*cur++ = (byte)(scale * ((*_in_ >> 3) & 0x01));*cur++ = (byte)(scale * ((*_in_ >> 2) & 0x01));*cur++ = (byte)(scale * ((*_in_ >> 1) & 0x01));*cur++ = (byte)(scale * ((*_in_) & 0x01));}if ((k) > (0)) *cur++ = (byte)(scale * (*_in_ >> 7));if ((k) > (1)) *cur++ = (byte)(scale * ((*_in_ >> 6) & 0x01));if ((k) > (2)) *cur++ = (byte)(scale * ((*_in_ >> 5) & 0x01));if ((k) > (3)) *cur++ = (byte)(scale * ((*_in_ >> 4) & 0x01));if ((k) > (4)) *cur++ = (byte)(scale * ((*_in_ >> 3) & 0x01));if ((k) > (5)) *cur++ = (byte)(scale * ((*_in_ >> 2) & 0x01));if ((k) > (6)) *cur++ = (byte)(scale * ((*_in_ >> 1) & 0x01));}
if (img_n != out_n) {
int q = 0;cur = a._out_ + stride * j;if ((img_n) == (1)) {
for (q = (int)(x - 1); (q) >= (0); --q) {
cur[q * 2 + 1] = (byte)(255);cur[q * 2 + 0] = (byte)(cur[q]);}}
 else {
(void)((!!((img_n) == (3))) || (_wassert("img_n == 3", "nanovg/stb_image.h", (uint)(4143)) , 0));for (q = (int)(x - 1); (q) >= (0); --q) {
cur[q * 4 + 3] = (byte)(255);cur[q * 4 + 2] = (byte)(cur[q * 3 + 2]);cur[q * 4 + 1] = (byte)(cur[q * 3 + 1]);cur[q * 4 + 0] = (byte)(cur[q * 3 + 0]);}}
}
}}

			return (int)(1);
		}

		public static int stbi__create_png_image(stbi__png a, byte* image_data, uint image_data_len, int out_n, int depth, int color, int interlaced)
		{
			byte* final;
			int p = 0;
			if (interlaced== 0) return (int)(stbi__create_png_image_raw(a, image_data, (uint)(image_data_len), (int)(out_n), (uint)(a.s.img_x), (uint)(a.s.img_y), (int)(depth), (int)(color)));
			final = (byte*)(stbi__malloc((ulong)(a.s.img_x * a.s.img_y * out_n)));
			for (p = (int)(0); (p) < (7); ++p) {
int* xorig = stackalloc int[7];
xorig[0] = (int)(0);
xorig[1] = (int)(4);
xorig[2] = (int)(0);
xorig[3] = (int)(2);
xorig[4] = (int)(0);
xorig[5] = (int)(1);
xorig[6] = (int)(0);
int* yorig = stackalloc int[7];
yorig[0] = (int)(0);
yorig[1] = (int)(0);
yorig[2] = (int)(4);
yorig[3] = (int)(0);
yorig[4] = (int)(2);
yorig[5] = (int)(0);
yorig[6] = (int)(1);
int* xspc = stackalloc int[7];
xspc[0] = (int)(8);
xspc[1] = (int)(8);
xspc[2] = (int)(4);
xspc[3] = (int)(4);
xspc[4] = (int)(2);
xspc[5] = (int)(2);
xspc[6] = (int)(1);
int* yspc = stackalloc int[7];
yspc[0] = (int)(8);
yspc[1] = (int)(8);
yspc[2] = (int)(8);
yspc[3] = (int)(4);
yspc[4] = (int)(4);
yspc[5] = (int)(2);
yspc[6] = (int)(2);
int i = 0;int j = 0;int x = 0;int y = 0;x = (int)((a.s.img_x - xorig[p] + xspc[p] - 1) / xspc[p]);y = (int)((a.s.img_y - yorig[p] + yspc[p] - 1) / yspc[p]);if (((x) != 0) && ((y) != 0)) {
uint img_len = (uint)(((((a.s.img_n * x * depth) + 7) >> 3) + 1) * y);if (stbi__create_png_image_raw(a, image_data, (uint)(image_data_len), (int)(out_n), (uint)(x), (uint)(y), (int)(depth), (int)(color))== 0) {
CRuntime.free(final);return (int)(0);}
for (j = (int)(0); (j) < (y); ++j) {
for (i = (int)(0); (i) < (x); ++i) {
int out_y = (int)(j * yspc[p] + yorig[p]);int out_x = (int)(i * xspc[p] + xorig[p]);CRuntime.memcpy(final + out_y * a.s.img_x * out_n + out_x * out_n, a._out_ + (j * x + i) * out_n, (ulong)(out_n));}}CRuntime.free(a._out_);image_data += img_len;image_data_len -= (uint)(img_len);}
}
			a._out_ = final;
			return (int)(1);
		}

		public static int stbi__compute_transparency(stbi__png z, byte* tc, int out_n)
		{
			stbi__context s = z.s;
			uint i = 0;uint pixel_count = (uint)(s.img_x * s.img_y);
			byte* p = z._out_;
			(void)((!!(((out_n) == (2)) || ((out_n) == (4)))) || (_wassert("out_n == 2 || out_n == 4", "nanovg/stb_image.h", (uint)(4208)) , 0));
			if ((out_n) == (2)) {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
p[1] = (byte)((p[0]) == (tc[0])?0:255);p += 2;}}
 else {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
if ((((p[0]) == (tc[0])) && ((p[1]) == (tc[1]))) && ((p[2]) == (tc[2]))) p[3] = (byte)(0);p += 4;}}

			return (int)(1);
		}

		public static int stbi__expand_png_palette(stbi__png a, byte* palette, int len, int pal_img_n)
		{
			uint i = 0;uint pixel_count = (uint)(a.s.img_x * a.s.img_y);
			byte* p;byte* temp_out;byte* orig = a._out_;
			p = (byte*)(stbi__malloc((ulong)(pixel_count * pal_img_n)));
			if ((p) == (null)) return (int)(stbi__err("outofmem"));
			temp_out = p;
			if ((pal_img_n) == (3)) {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
int n = (int)(orig[i] * 4);p[0] = (byte)(palette[n]);p[1] = (byte)(palette[n + 1]);p[2] = (byte)(palette[n + 2]);p += 3;}}
 else {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
int n = (int)(orig[i] * 4);p[0] = (byte)(palette[n]);p[1] = (byte)(palette[n + 1]);p[2] = (byte)(palette[n + 2]);p[3] = (byte)(palette[n + 3]);p += 4;}}

			CRuntime.free(a._out_);
			a._out_ = temp_out;
			(void)(len);
			return (int)(1);
		}

		public static void stbi_set_unpremultiply_on_load(int flag_true_if_should_unpremultiply)
		{
			stbi__unpremultiply_on_load = (int)(flag_true_if_should_unpremultiply);
		}

		public static void stbi_convert_iphone_png_to_rgb(int flag_true_if_should_convert)
		{
			stbi__de_iphone_flag = (int)(flag_true_if_should_convert);
		}

		public static void stbi__de_iphone(stbi__png z)
		{
			stbi__context s = z.s;
			uint i = 0;uint pixel_count = (uint)(s.img_x * s.img_y);
			byte* p = z._out_;
			if ((s.img_out_n) == (3)) {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
byte t = (byte)(p[0]);p[0] = (byte)(p[2]);p[2] = (byte)(t);p += 3;}}
 else {
(void)((!!((s.img_out_n) == (4))) || (_wassert("s->img_out_n == 4", "nanovg/stb_image.h", (uint)(4289)) , 0));if ((stbi__unpremultiply_on_load) != 0) {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
byte a = (byte)(p[3]);byte t = (byte)(p[0]);if ((a) != 0) {
p[0] = (byte)(p[2] * 255 / a);p[1] = (byte)(p[1] * 255 / a);p[2] = (byte)(t * 255 / a);}
 else {
p[0] = (byte)(p[2]);p[2] = (byte)(t);}
p += 4;}}
 else {
for (i = (uint)(0); (i) < (pixel_count); ++i) {
byte t = (byte)(p[0]);p[0] = (byte)(p[2]);p[2] = (byte)(t);p += 4;}}
}

		}

		public static int stbi__parse_png_file(stbi__png z, int scan, int req_comp)
		{
			byte* palette = stackalloc byte[1024];byte pal_img_n = (byte)(0);
			byte has_trans = (byte)(0);byte* tc = stackalloc byte[3];
			uint ioff = (uint)(0);uint idata_limit = (uint)(0);uint i = 0;uint pal_len = (uint)(0);
			int first = (int)(1);int k = 0;int interlace = (int)(0);int color = (int)(0);int depth = (int)(0);int is_iphone = (int)(0);
			stbi__context s = z.s;
			z.expanded = (null);
			z.idata = (null);
			z._out_ = (null);
			if (stbi__check_png_header(s)== 0) return (int)(0);
			if ((scan) == (STBI__SCAN_type)) return (int)(1);
			for (; ; ) {
stbi__pngchunk c = (stbi__pngchunk)(stbi__get_chunk_header(s));switch (c.type){
case ((('C') << 24) + (('g') << 16) + (('B') << 8) + ('I')):is_iphone = (int)(1);stbi__skip(s, (int)(c.length));break;case ((('I') << 24) + (('H') << 16) + (('D') << 8) + ('R')):{
int comp = 0;int filter = 0;if (first== 0) return (int)(stbi__err("multiple IHDR"));first = (int)(0);if (c.length != 13) return (int)(stbi__err("bad IHDR len"));s.img_x = (uint)(stbi__get32be(s));if ((s.img_x) > (1 << 24)) return (int)(stbi__err("too large"));s.img_y = (uint)(stbi__get32be(s));if ((s.img_y) > (1 << 24)) return (int)(stbi__err("too large"));depth = (int)(stbi__get8(s));if ((((depth != 1) && (depth != 2)) && (depth != 4)) && (depth != 8)) return (int)(stbi__err("1/2/4/8-bit only"));color = (int)(stbi__get8(s));if ((color) > (6)) return (int)(stbi__err("bad ctype"));if ((color) == (3)) pal_img_n = (byte)(3); else if ((color & 1) != 0) return (int)(stbi__err("bad ctype"));comp = (int)(stbi__get8(s));if ((comp) != 0) return (int)(stbi__err("bad comp method"));filter = (int)(stbi__get8(s));if ((filter) != 0) return (int)(stbi__err("bad filter method"));interlace = (int)(stbi__get8(s));if ((interlace) > (1)) return (int)(stbi__err("bad interlace method"));if ((s.img_x== 0) || (s.img_y== 0)) return (int)(stbi__err("0-pixel image"));if (pal_img_n== 0) {
s.img_n = (int)(((color & 2) != 0?3:1) + ((color & 4) != 0?1:0));if (((1 << 30) / s.img_x / s.img_n) < (s.img_y)) return (int)(stbi__err("too large"));if ((scan) == (STBI__SCAN_header)) return (int)(1);}
 else {
s.img_n = (int)(1);if (((1 << 30) / s.img_x / 4) < (s.img_y)) return (int)(stbi__err("too large"));}
break;}
case ((('P') << 24) + (('L') << 16) + (('T') << 8) + ('E')):{
if ((first) != 0) return (int)(stbi__err("first not IHDR"));if ((c.length) > (256 * 3)) return (int)(stbi__err("invalid PLTE"));pal_len = (uint)(c.length / 3);if (pal_len * 3 != c.length) return (int)(stbi__err("invalid PLTE"));for (i = (uint)(0); (i) < (pal_len); ++i) {
palette[i * 4 + 0] = (byte)(stbi__get8(s));palette[i * 4 + 1] = (byte)(stbi__get8(s));palette[i * 4 + 2] = (byte)(stbi__get8(s));palette[i * 4 + 3] = (byte)(255);}break;}
case ((('t') << 24) + (('R') << 16) + (('N') << 8) + ('S')):{
if ((first) != 0) return (int)(stbi__err("first not IHDR"));if ((z.idata) != null) return (int)(stbi__err("tRNS after IDAT"));if ((pal_img_n) != 0) {
if ((scan) == (STBI__SCAN_header)) {
s.img_n = (int)(4);return (int)(1);}
if ((pal_len) == (0)) return (int)(stbi__err("tRNS before PLTE"));if ((c.length) > (pal_len)) return (int)(stbi__err("bad tRNS len"));pal_img_n = (byte)(4);for (i = (uint)(0); (i) < (c.length); ++i) {palette[i * 4 + 3] = (byte)(stbi__get8(s));}}
 else {
if ((s.img_n & 1)== 0) return (int)(stbi__err("tRNS with alpha"));if (c.length != (uint)(s.img_n) * 2) return (int)(stbi__err("bad tRNS len"));has_trans = (byte)(1);for (k = (int)(0); (k) < (s.img_n); ++k) {tc[k] = (byte)((byte)(stbi__get16be(s) & 255) * stbi__depth_scale_table[depth]);}}
break;}
case ((('I') << 24) + (('D') << 16) + (('A') << 8) + ('T')):{
if ((first) != 0) return (int)(stbi__err("first not IHDR"));if (((pal_img_n) != 0) && (pal_len== 0)) return (int)(stbi__err("no PLTE"));if ((scan) == (STBI__SCAN_header)) {
s.img_n = (int)(pal_img_n);return (int)(1);}
if (((int)(ioff + c.length)) < ((int)(ioff))) return (int)(0);if ((ioff + c.length) > (idata_limit)) {
uint idata_limit_old = (uint)(idata_limit);byte* p;if ((idata_limit) == (0)) idata_limit = (uint)((c.length) > (4096)?c.length:4096);while ((ioff + c.length) > (idata_limit)) {idata_limit *= (uint)(2);}(void)(idata_limit_old);p = (byte*)(CRuntime.realloc(z.idata, (ulong)(idata_limit)));if ((p) == (null)) return (int)(stbi__err("outofmem"));z.idata = p;}
if (stbi__getn(s, z.idata + ioff, (int)(c.length))== 0) return (int)(stbi__err("outofdata"));ioff += (uint)(c.length);break;}
case ((('I') << 24) + (('E') << 16) + (('N') << 8) + ('D')):{
uint raw_len = 0;uint bpl = 0;if ((first) != 0) return (int)(stbi__err("first not IHDR"));if (scan != STBI__SCAN_load) return (int)(1);if ((z.idata) == (null)) return (int)(stbi__err("no IDAT"));bpl = (uint)((s.img_x * depth + 7) / 8);raw_len = (uint)(bpl * s.img_y * s.img_n + s.img_y);z.expanded = (byte*)(stbi_zlib_decode_malloc_guesssize_headerflag((sbyte*)(z.idata), (int)(ioff), (int)(raw_len), (int*)(&raw_len), is_iphone!=0?0:1));if ((z.expanded) == (null)) return (int)(0);CRuntime.free(z.idata);z.idata = (null);if (((((req_comp) == (s.img_n + 1)) && (req_comp != 3)) && (pal_img_n== 0)) || ((has_trans) != 0)) s.img_out_n = (int)(s.img_n + 1); else s.img_out_n = (int)(s.img_n);if (stbi__create_png_image(z, z.expanded, (uint)(raw_len), (int)(s.img_out_n), (int)(depth), (int)(color), (int)(interlace))== 0) return (int)(0);if ((has_trans) != 0) if (stbi__compute_transparency(z, tc, (int)(s.img_out_n))== 0) return (int)(0);if ((((is_iphone) != 0) && ((stbi__de_iphone_flag) != 0)) && ((s.img_out_n) > (2))) stbi__de_iphone(z);if ((pal_img_n) != 0) {
s.img_n = (int)(pal_img_n);s.img_out_n = (int)(pal_img_n);if ((req_comp) >= (3)) s.img_out_n = (int)(req_comp);if (stbi__expand_png_palette(z, palette, (int)(pal_len), (int)(s.img_out_n))== 0) return (int)(0);}
CRuntime.free(z.expanded);z.expanded = (null);return (int)(1);}
default: if ((first) != 0) return (int)(stbi__err("first not IHDR"));if ((c.type & (1 << 29)) == (0)) {
string invalid_chunk = "XXXX PNG chunk not known";invalid_chunk[0] = (sbyte)((byte)((c.type >> 24) & 255));invalid_chunk[1] = (sbyte)((byte)((c.type >> 16) & 255));invalid_chunk[2] = (sbyte)((byte)((c.type >> 8) & 255));invalid_chunk[3] = (sbyte)((byte)((c.type >> 0) & 255));return (int)(stbi__err(invalid_chunk));}
stbi__skip(s, (int)(c.length));break;}
stbi__get32be(s);}
		}

		public static byte* stbi__do_png(stbi__png p, int* x, int* y, int* n, int req_comp)
		{
			byte* result = (null);
			if (((req_comp) < (0)) || ((req_comp) > (4))) return ((byte*)((ulong)((stbi__err("bad req_comp")) != 0?((byte *)null):(null))));
			if ((stbi__parse_png_file(p, (int)(STBI__SCAN_load), (int)(req_comp))) != 0) {
result = p._out_;p._out_ = (null);if (((req_comp) != 0) && (req_comp != p.s.img_out_n)) {
result = stbi__convert_format(result, (int)(p.s.img_out_n), (int)(req_comp), (uint)(p.s.img_x), (uint)(p.s.img_y));p.s.img_out_n = (int)(req_comp);if ((result) == (null)) return result;}
*x = (int)(p.s.img_x);*y = (int)(p.s.img_y);if ((n) != null) *n = (int)(p.s.img_out_n);}

			CRuntime.free(p._out_);
			p._out_ = (null);
			CRuntime.free(p.expanded);
			p.expanded = (null);
			CRuntime.free(p.idata);
			p.idata = (null);
			return result;
		}

		public static byte* stbi__png_load(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			stbi__png p =  new stbi__png();
			p.s = s;
			return stbi__do_png(p, x, y, comp, (int)(req_comp));
		}

		public static int stbi__png_test(stbi__context s)
		{
			int r = 0;
			r = (int)(stbi__check_png_header(s));
			stbi__rewind(s);
			return (int)(r);
		}

		public static int stbi__png_info_raw(stbi__png p, int* x, int* y, int* comp)
		{
			if (stbi__parse_png_file(p, (int)(STBI__SCAN_header), (int)(0))== 0) {
stbi__rewind(p.s);return (int)(0);}

			if ((x) != null) *x = (int)(p.s.img_x);
			if ((y) != null) *y = (int)(p.s.img_y);
			if ((comp) != null) *comp = (int)(p.s.img_n);
			return (int)(1);
		}

		public static int stbi__png_info(stbi__context s, int* x, int* y, int* comp)
		{
			stbi__png p =  new stbi__png();
			p.s = s;
			return (int)(stbi__png_info_raw(p, x, y, comp));
		}

		public static int stbi__bmp_test_raw(stbi__context s)
		{
			int r = 0;
			int sz = 0;
			if (stbi__get8(s) != 'B') return (int)(0);
			if (stbi__get8(s) != 'M') return (int)(0);
			stbi__get32le(s);
			stbi__get16le(s);
			stbi__get16le(s);
			stbi__get32le(s);
			sz = (int)(stbi__get32le(s));
			r = (int)((((((sz) == (12)) || ((sz) == (40))) || ((sz) == (56))) || ((sz) == (108))) || ((sz) == (124))?1:0);
			return (int)(r);
		}

		public static int stbi__bmp_test(stbi__context s)
		{
			int r = (int)(stbi__bmp_test_raw(s));
			stbi__rewind(s);
			return (int)(r);
		}

		public static int stbi__high_bit(uint z)
		{
			int n = (int)(0);
			if ((z) == (0)) return (int)(-1);
			if ((z) >= (0x10000)) n += (int)(16) , z >>= 16;
			if ((z) >= (0x00100)) n += (int)(8) , z >>= 8;
			if ((z) >= (0x00010)) n += (int)(4) , z >>= 4;
			if ((z) >= (0x00004)) n += (int)(2) , z >>= 2;
			if ((z) >= (0x00002)) n += (int)(1) , z >>= 1;
			return (int)(n);
		}

		public static int stbi__bitcount(uint a)
		{
			a = (uint)((a & 0x55555555) + ((a >> 1) & 0x55555555));
			a = (uint)((a & 0x33333333) + ((a >> 2) & 0x33333333));
			a = (uint)((a + (a >> 4)) & 0x0f0f0f0f);
			a = (uint)(a + (a >> 8));
			a = (uint)(a + (a >> 16));
			return (int)(a & 0xff);
		}

		public static int stbi__shiftsigned(int v, int shift, int bits)
		{
			int result = 0;
			int z = (int)(0);
			if ((shift) < (0)) v <<= -shift; else v >>= shift;
			result = (int)(v);
			z = (int)(bits);
			while ((z) < (8)) {
result += (int)(v >> z);z += (int)(bits);}
			return (int)(result);
		}

		public static void * stbi__bmp_parse_header(stbi__context s, stbi__bmp_data* info)
		{
			int hsz = 0;
			if ((stbi__get8(s) != 'B') || (stbi__get8(s) != 'M')) return ((byte*)((ulong)((stbi__err("not BMP")) != 0?((byte *)null):(null))));
			stbi__get32le(s);
			stbi__get16le(s);
			stbi__get16le(s);
			info->offset = (int)(stbi__get32le(s));
			info->hsz = (int)(hsz = (int)(stbi__get32le(s)));
			if (((((hsz != 12) && (hsz != 40)) && (hsz != 56)) && (hsz != 108)) && (hsz != 124)) return ((byte*)((ulong)((stbi__err("unknown BMP")) != 0?((byte *)null):(null))));
			if ((hsz) == (12)) {
s.img_x = (uint)(stbi__get16le(s));s.img_y = (uint)(stbi__get16le(s));}
 else {
s.img_x = (uint)(stbi__get32le(s));s.img_y = (uint)(stbi__get32le(s));}

			if (stbi__get16le(s) != 1) return ((byte*)((ulong)((stbi__err("bad BMP")) != 0?((byte *)null):(null))));
			info->bpp = (int)(stbi__get16le(s));
			if ((info->bpp) == (1)) return ((byte*)((ulong)((stbi__err("monochrome")) != 0?((byte *)null):(null))));
			if (hsz != 12) {
int compress = (int)(stbi__get32le(s));if (((compress) == (1)) || ((compress) == (2))) return ((byte*)((ulong)((stbi__err("BMP RLE")) != 0?((byte *)null):(null))));stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);if (((hsz) == (40)) || ((hsz) == (56))) {
if ((hsz) == (56)) {
stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);}
if (((info->bpp) == (16)) || ((info->bpp) == (32))) {
info->mr = (uint)(info->mg = (uint)(info->mb = (uint)(0)));if ((compress) == (0)) {
if ((info->bpp) == (32)) {
info->mr = (uint)(0xffu << 16);info->mg = (uint)(0xffu << 8);info->mb = (uint)(0xffu << 0);info->ma = (uint)(0xffu << 24);info->all_a = (uint)(0);}
 else {
info->mr = (uint)(31u << 10);info->mg = (uint)(31u << 5);info->mb = (uint)(31u << 0);}
}
 else if ((compress) == (3)) {
info->mr = (uint)(stbi__get32le(s));info->mg = (uint)(stbi__get32le(s));info->mb = (uint)(stbi__get32le(s));if (((info->mr) == (info->mg)) && ((info->mg) == (info->mb))) {
return ((byte*)((ulong)((stbi__err("bad BMP")) != 0?((byte *)null):(null))));}
}
 else return ((byte*)((ulong)((stbi__err("bad BMP")) != 0?((byte *)null):(null))));}
}
 else {
int i = 0;if ((hsz != 108) && (hsz != 124)) return ((byte*)((ulong)((stbi__err("bad BMP")) != 0?((byte *)null):(null))));info->mr = (uint)(stbi__get32le(s));info->mg = (uint)(stbi__get32le(s));info->mb = (uint)(stbi__get32le(s));info->ma = (uint)(stbi__get32le(s));stbi__get32le(s);for (i = (int)(0); (i) < (12); ++i) {stbi__get32le(s);}if ((hsz) == (124)) {
stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);stbi__get32le(s);}
}
}

			return (void *)(1);
		}

		public static byte* stbi__bmp_load(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			byte* _out_;
			uint mr = (uint)(0);uint mg = (uint)(0);uint mb = (uint)(0);uint ma = (uint)(0);uint all_a = 0;
			byte* pal = stackalloc byte[256 * 4];
			int psize = (int)(0);int i = 0;int j = 0;int width = 0;
			int flip_vertically = 0;int pad = 0;int target = 0;
			stbi__bmp_data info =  new stbi__bmp_data();
			info.all_a = (uint)(255);
			if ((stbi__bmp_parse_header(s, &info)) == (null)) return (null);
			flip_vertically = (int)(((int)(s.img_y)) > (0)?1:0);
			s.img_y = (uint)(CRuntime.abs((int)(s.img_y)));
			mr = (uint)(info.mr);
			mg = (uint)(info.mg);
			mb = (uint)(info.mb);
			ma = (uint)(info.ma);
			all_a = (uint)(info.all_a);
			if ((info.hsz) == (12)) {
if ((info.bpp) < (24)) psize = (int)((info.offset - 14 - 24) / 3);}
 else {
if ((info.bpp) < (16)) psize = (int)((info.offset - 14 - info.hsz) >> 2);}

			s.img_n = (int)((ma) != 0?4:3);
			if (((req_comp) != 0) && ((req_comp) >= (3))) target = (int)(req_comp); else target = (int)(s.img_n);
			_out_ = (byte*)(stbi__malloc((ulong)(target * s.img_x * s.img_y)));
			if (_out_== null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));
			if ((info.bpp) < (16)) {
int z = (int)(0);if (((psize) == (0)) || ((psize) > (256))) {
CRuntime.free(_out_);return ((byte*)((ulong)((stbi__err("invalid")) != 0?((byte *)null):(null))));}
for (i = (int)(0); (i) < (psize); ++i) {
pal[i * 4 +2] = (byte)(stbi__get8(s));pal[i * 4 +1] = (byte)(stbi__get8(s));pal[i * 4 +0] = (byte)(stbi__get8(s));if (info.hsz != 12) stbi__get8(s);pal[i * 4 +3] = (byte)(255);}stbi__skip(s, (int)(info.offset - 14 - info.hsz - psize * ((info.hsz) == (12)?3:4)));if ((info.bpp) == (4)) width = (int)((s.img_x + 1) >> 1); else if ((info.bpp) == (8)) width = (int)(s.img_x); else {
CRuntime.free(_out_);return ((byte*)((ulong)((stbi__err("bad bpp")) != 0?((byte *)null):(null))));}
pad = (int)((-width) & 3);for (j = (int)(0); (j) < ((int)(s.img_y)); ++j) {
for (i = (int)(0); (i) < ((int)(s.img_x)); i += (int)(2)) {
int v = (int)(stbi__get8(s));int v2 = (int)(0);if ((info.bpp) == (4)) {
v2 = (int)(v & 15);v >>= 4;}
_out_[z++] = (byte)(pal[v * 4 +0]);_out_[z++] = (byte)(pal[v * 4 +1]);_out_[z++] = (byte)(pal[v * 4 +2]);if ((target) == (4)) _out_[z++] = (byte)(255);if ((i + 1) == ((int)(s.img_x))) break;v = (int)(((info.bpp) == (8))?stbi__get8(s):v2);_out_[z++] = (byte)(pal[v * 4 +0]);_out_[z++] = (byte)(pal[v * 4 +1]);_out_[z++] = (byte)(pal[v * 4 +2]);if ((target) == (4)) _out_[z++] = (byte)(255);}stbi__skip(s, (int)(pad));}}
 else {
int rshift = (int)(0);int gshift = (int)(0);int bshift = (int)(0);int ashift = (int)(0);int rcount = (int)(0);int gcount = (int)(0);int bcount = (int)(0);int acount = (int)(0);int z = (int)(0);int easy = (int)(0);stbi__skip(s, (int)(info.offset - 14 - info.hsz));if ((info.bpp) == (24)) width = (int)(3 * s.img_x); else if ((info.bpp) == (16)) width = (int)(2 * s.img_x); else width = (int)(0);pad = (int)((-width) & 3);if ((info.bpp) == (24)) {
easy = (int)(1);}
 else if ((info.bpp) == (32)) {
if (((((mb) == (0xff)) && ((mg) == (0xff00))) && ((mr) == (0x00ff0000))) && ((ma) == (0xff000000))) easy = (int)(2);}
if (easy== 0) {
if (((mr== 0) || (mg== 0)) || (mb== 0)) {
CRuntime.free(_out_);return ((byte*)((ulong)((stbi__err("bad masks")) != 0?((byte *)null):(null))));}
rshift = (int)(stbi__high_bit((uint)(mr)) - 7);rcount = (int)(stbi__bitcount((uint)(mr)));gshift = (int)(stbi__high_bit((uint)(mg)) - 7);gcount = (int)(stbi__bitcount((uint)(mg)));bshift = (int)(stbi__high_bit((uint)(mb)) - 7);bcount = (int)(stbi__bitcount((uint)(mb)));ashift = (int)(stbi__high_bit((uint)(ma)) - 7);acount = (int)(stbi__bitcount((uint)(ma)));}
for (j = (int)(0); (j) < ((int)(s.img_y)); ++j) {
if ((easy) != 0) {
for (i = (int)(0); (i) < ((int)(s.img_x)); ++i) {
byte a = 0;_out_[z + 2] = (byte)(stbi__get8(s));_out_[z + 1] = (byte)(stbi__get8(s));_out_[z + 0] = (byte)(stbi__get8(s));z += (int)(3);a = (byte)((easy) == (2)?stbi__get8(s):255);all_a |= (uint)(a);if ((target) == (4)) _out_[z++] = (byte)(a);}}
 else {
int bpp = (int)(info.bpp);for (i = (int)(0); (i) < ((int)(s.img_x)); ++i) {
uint v = (uint)((bpp) == (16)?(uint)(stbi__get16le(s)):stbi__get32le(s));int a = 0;_out_[z++] = ((byte)((stbi__shiftsigned((int)(v & mr), (int)(rshift), (int)(rcount))) & 255));_out_[z++] = ((byte)((stbi__shiftsigned((int)(v & mg), (int)(gshift), (int)(gcount))) & 255));_out_[z++] = ((byte)((stbi__shiftsigned((int)(v & mb), (int)(bshift), (int)(bcount))) & 255));a = (int)((ma) != 0?stbi__shiftsigned((int)(v & ma), (int)(ashift), (int)(acount)):255);all_a |= (uint)(a);if ((target) == (4)) _out_[z++] = ((byte)((a) & 255));}}
stbi__skip(s, (int)(pad));}}

			if (((target) == (4)) && ((all_a) == (0))) for (i = (int)(4 * s.img_x * s.img_y - 1); (i) >= (0); i -= (int)(4)) {_out_[i] = (byte)(255);}
			if ((flip_vertically) != 0) {
byte t = 0;for (j = (int)(0); (j) < ((int)(s.img_y) >> 1); ++j) {
byte* p1 = _out_ + j * s.img_x * target;byte* p2 = _out_ + (s.img_y - 1 - j) * s.img_x * target;for (i = (int)(0); (i) < ((int)(s.img_x) * target); ++i) {
t = (byte)(p1[i]) , p1[i] = (byte)(p2[i]) , p2[i] = (byte)(t);}}}

			if (((req_comp) != 0) && (req_comp != target)) {
_out_ = stbi__convert_format(_out_, (int)(target), (int)(req_comp), (uint)(s.img_x), (uint)(s.img_y));if ((_out_) == (null)) return _out_;}

			*x = (int)(s.img_x);
			*y = (int)(s.img_y);
			if ((comp) != null) *comp = (int)(s.img_n);
			return _out_;
		}

		public static int stbi__tga_get_comp(int bits_per_pixel, int is_grey, int* is_rgb16)
		{
			if ((is_rgb16) != null) *is_rgb16 = (int)(0);
			switch (bits_per_pixel){
case 8:return (int)(STBI_grey);case 16:if ((is_grey) != 0) return (int)(STBI_grey_alpha);case 15:if ((is_rgb16) != null) *is_rgb16 = (int)(1);return (int)(STBI_rgb);case 24:case 32:return (int)(bits_per_pixel / 8);default: return (int)(0);}

		}

		public static int stbi__tga_info(stbi__context s, int* x, int* y, int* comp)
		{
			int tga_w = 0;int tga_h = 0;int tga_comp = 0;int tga_image_type = 0;int tga_bits_per_pixel = 0;int tga_colormap_bpp = 0;
			int sz = 0;int tga_colormap_type = 0;
			stbi__get8(s);
			tga_colormap_type = (int)(stbi__get8(s));
			if ((tga_colormap_type) > (1)) {
stbi__rewind(s);return (int)(0);}

			tga_image_type = (int)(stbi__get8(s));
			if ((tga_colormap_type) == (1)) {
if ((tga_image_type != 1) && (tga_image_type != 9)) {
stbi__rewind(s);return (int)(0);}
stbi__skip(s, (int)(4));sz = (int)(stbi__get8(s));if (((((sz != 8) && (sz != 15)) && (sz != 16)) && (sz != 24)) && (sz != 32)) {
stbi__rewind(s);return (int)(0);}
stbi__skip(s, (int)(4));tga_colormap_bpp = (int)(sz);}
 else {
if ((((tga_image_type != 2) && (tga_image_type != 3)) && (tga_image_type != 10)) && (tga_image_type != 11)) {
stbi__rewind(s);return (int)(0);}
stbi__skip(s, (int)(9));tga_colormap_bpp = (int)(0);}

			tga_w = (int)(stbi__get16le(s));
			if ((tga_w) < (1)) {
stbi__rewind(s);return (int)(0);}

			tga_h = (int)(stbi__get16le(s));
			if ((tga_h) < (1)) {
stbi__rewind(s);return (int)(0);}

			tga_bits_per_pixel = (int)(stbi__get8(s));
			stbi__get8(s);
			if (tga_colormap_bpp != 0) {
if ((tga_bits_per_pixel != 8) && (tga_bits_per_pixel != 16)) {
stbi__rewind(s);return (int)(0);}
tga_comp = (int)(stbi__tga_get_comp((int)(tga_colormap_bpp), (int)(0), (null)));}
 else {
tga_comp = (int)(stbi__tga_get_comp((int)(tga_bits_per_pixel), (((tga_image_type) == (3))) || (((tga_image_type) == (11)))?1:0, (null)));}

			if (tga_comp== 0) {
stbi__rewind(s);return (int)(0);}

			if ((x) != null) *x = (int)(tga_w);
			if ((y) != null) *y = (int)(tga_h);
			if ((comp) != null) *comp = (int)(tga_comp);
			return (int)(1);
		}

		public static int stbi__tga_test(stbi__context s)
		{
			int res = (int)(0);
			int sz = 0;int tga_color_type = 0;
			stbi__get8(s);
			tga_color_type = (int)(stbi__get8(s));
			if ((tga_color_type) > (1)) goto errorEnd;
			sz = (int)(stbi__get8(s));
			if ((tga_color_type) == (1)) {
if ((sz != 1) && (sz != 9)) goto errorEnd;stbi__skip(s, (int)(4));sz = (int)(stbi__get8(s));if (((((sz != 8) && (sz != 15)) && (sz != 16)) && (sz != 24)) && (sz != 32)) goto errorEnd;stbi__skip(s, (int)(4));}
 else {
if ((((sz != 2) && (sz != 3)) && (sz != 10)) && (sz != 11)) goto errorEnd;stbi__skip(s, (int)(9));}

			if ((stbi__get16le(s)) < (1)) goto errorEnd;
			if ((stbi__get16le(s)) < (1)) goto errorEnd;
			sz = (int)(stbi__get8(s));
			if ((((tga_color_type) == (1)) && (sz != 8)) && (sz != 16)) goto errorEnd;
			if (((((sz != 8) && (sz != 15)) && (sz != 16)) && (sz != 24)) && (sz != 32)) goto errorEnd;
			res = (int)(1);
			errorEnd:;
stbi__rewind(s);
			return (int)(res);
		}

		public static void stbi__tga_read_rgb16(stbi__context s, byte* _out_)
		{
			ushort px = (ushort)(stbi__get16le(s));
			ushort fiveBitMask = (ushort)(31);
			int r = (int)((px >> 10) & fiveBitMask);
			int g = (int)((px >> 5) & fiveBitMask);
			int b = (int)(px & fiveBitMask);
			_out_[0] = (byte)((r * 255) / 31);
			_out_[1] = (byte)((g * 255) / 31);
			_out_[2] = (byte)((b * 255) / 31);
		}

		public static byte* stbi__tga_load(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			int tga_offset = (int)(stbi__get8(s));
			int tga_indexed = (int)(stbi__get8(s));
			int tga_image_type = (int)(stbi__get8(s));
			int tga_is_RLE = (int)(0);
			int tga_palette_start = (int)(stbi__get16le(s));
			int tga_palette_len = (int)(stbi__get16le(s));
			int tga_palette_bits = (int)(stbi__get8(s));
			int tga_x_origin = (int)(stbi__get16le(s));
			int tga_y_origin = (int)(stbi__get16le(s));
			int tga_width = (int)(stbi__get16le(s));
			int tga_height = (int)(stbi__get16le(s));
			int tga_bits_per_pixel = (int)(stbi__get8(s));
			int tga_comp = 0;int tga_rgb16 = (int)(0);
			int tga_inverted = (int)(stbi__get8(s));
			byte* tga_data;
			byte* tga_palette = (null);
			int i = 0;int j = 0;
			byte* raw_data = stackalloc byte[4];
			int RLE_count = (int)(0);
			int RLE_repeating = (int)(0);
			int read_next_pixel = (int)(1);
			if ((tga_image_type) >= (8)) {
tga_image_type -= (int)(8);tga_is_RLE = (int)(1);}

			tga_inverted = (int)(1 - ((tga_inverted >> 5) & 1));
			if ((tga_indexed) != 0) tga_comp = (int)(stbi__tga_get_comp((int)(tga_palette_bits), (int)(0), &tga_rgb16)); else tga_comp = (int)(stbi__tga_get_comp((int)(tga_bits_per_pixel), (tga_image_type) == (3)?1:0, &tga_rgb16));
			if (tga_comp== 0) return ((byte*)((ulong)((stbi__err("bad format")) != 0?((byte *)null):(null))));
			*x = (int)(tga_width);
			*y = (int)(tga_height);
			if ((comp) != null) *comp = (int)(tga_comp);
			tga_data = (byte*)(stbi__malloc((ulong)((ulong)(tga_width) * tga_height * tga_comp)));
			if (tga_data== null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));
			stbi__skip(s, (int)(tga_offset));
			if (((tga_indexed== 0) && (tga_is_RLE== 0)) && (tga_rgb16== 0)) {
for (i = (int)(0); (i) < (tga_height); ++i) {
int row = (int)((tga_inverted) != 0?tga_height - i - 1:i);byte* tga_row = tga_data + row * tga_width * tga_comp;stbi__getn(s, tga_row, (int)(tga_width * tga_comp));}}
 else {
if ((tga_indexed) != 0) {
stbi__skip(s, (int)(tga_palette_start));tga_palette = (byte*)(stbi__malloc((ulong)(tga_palette_len * tga_comp)));if (tga_palette== null) {
CRuntime.free(tga_data);return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));}
if ((tga_rgb16) != 0) {
byte* pal_entry = tga_palette;(void)((!!((tga_comp) == (STBI_rgb))) || (_wassert("tga_comp == STBI_rgb", "nanovg/stb_image.h", (uint)(5055)) , 0));for (i = (int)(0); (i) < (tga_palette_len); ++i) {
stbi__tga_read_rgb16(s, pal_entry);pal_entry += tga_comp;}}
 else if (stbi__getn(s, tga_palette, (int)(tga_palette_len * tga_comp))== 0) {
CRuntime.free(tga_data);CRuntime.free(tga_palette);return ((byte*)((ulong)((stbi__err("bad palette")) != 0?((byte *)null):(null))));}
}
for (i = (int)(0); (i) < (tga_width * tga_height); ++i) {
if ((tga_is_RLE) != 0) {
if ((RLE_count) == (0)) {
int RLE_cmd = (int)(stbi__get8(s));RLE_count = (int)(1 + (RLE_cmd & 127));RLE_repeating = (int)(RLE_cmd >> 7);read_next_pixel = (int)(1);}
 else if (RLE_repeating== 0) {
read_next_pixel = (int)(1);}
}
 else {
read_next_pixel = (int)(1);}
if ((read_next_pixel) != 0) {
if ((tga_indexed) != 0) {
int pal_idx = (int)(((tga_bits_per_pixel) == (8))?stbi__get8(s):stbi__get16le(s));if ((pal_idx) >= (tga_palette_len)) {
pal_idx = (int)(0);}
pal_idx *= (int)(tga_comp);for (j = (int)(0); (j) < (tga_comp); ++j) {
raw_data[j] = (byte)(tga_palette[pal_idx + j]);}}
 else if ((tga_rgb16) != 0) {
(void)((!!((tga_comp) == (STBI_rgb))) || (_wassert("tga_comp == STBI_rgb", "nanovg/stb_image.h", (uint)(5104)) , 0));stbi__tga_read_rgb16(s, raw_data);}
 else {
for (j = (int)(0); (j) < (tga_comp); ++j) {
raw_data[j] = (byte)(stbi__get8(s));}}
read_next_pixel = (int)(0);}
for (j = (int)(0); (j) < (tga_comp); ++j) {tga_data[i * tga_comp + j] = (byte)(raw_data[j]);}--RLE_count;}if ((tga_inverted) != 0) {
for (j = (int)(0); (j * 2) < (tga_height); ++j) {
int index1 = (int)(j * tga_width * tga_comp);int index2 = (int)((tga_height - 1 - j) * tga_width * tga_comp);for (i = (int)(tga_width * tga_comp); (i) > (0); --i) {
byte temp = (byte)(tga_data[index1]);tga_data[index1] = (byte)(tga_data[index2]);tga_data[index2] = (byte)(temp);++index1;++index2;}}}
if (tga_palette != (null)) {
CRuntime.free(tga_palette);}
}

			if (((tga_comp) >= (3)) && (tga_rgb16== 0)) {
byte* tga_pixel = tga_data;for (i = (int)(0); (i) < (tga_width * tga_height); ++i) {
byte temp = (byte)(tga_pixel[0]);tga_pixel[0] = (byte)(tga_pixel[2]);tga_pixel[2] = (byte)(temp);tga_pixel += tga_comp;}}

			if (((req_comp) != 0) && (req_comp != tga_comp)) tga_data = stbi__convert_format(tga_data, (int)(tga_comp), (int)(req_comp), (uint)(tga_width), (uint)(tga_height));
			tga_palette_start = (int)(tga_palette_len = (int)(tga_palette_bits = (int)(tga_x_origin = (int)(tga_y_origin = (int)(0)))));
			return tga_data;
		}

		public static int stbi__psd_test(stbi__context s)
		{
			int r = (((stbi__get32be(s)) == (0x38425053)))?1:0;
			stbi__rewind(s);
			return (int)(r);
		}

		public static byte* stbi__psd_load(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			int pixelCount = 0;
			int channelCount = 0;int compression = 0;
			int channel = 0;int i = 0;int count = 0;int len = 0;
			int bitdepth = 0;
			int w = 0;int h = 0;
			byte* _out_;
			if (stbi__get32be(s) != 0x38425053) return ((byte*)((ulong)((stbi__err("not PSD")) != 0?((byte *)null):(null))));
			if (stbi__get16be(s) != 1) return ((byte*)((ulong)((stbi__err("wrong version")) != 0?((byte *)null):(null))));
			stbi__skip(s, (int)(6));
			channelCount = (int)(stbi__get16be(s));
			if (((channelCount) < (0)) || ((channelCount) > (16))) return ((byte*)((ulong)((stbi__err("wrong channel count")) != 0?((byte *)null):(null))));
			h = (int)(stbi__get32be(s));
			w = (int)(stbi__get32be(s));
			bitdepth = (int)(stbi__get16be(s));
			if ((bitdepth != 8) && (bitdepth != 16)) return ((byte*)((ulong)((stbi__err("unsupported bit depth")) != 0?((byte *)null):(null))));
			if (stbi__get16be(s) != 3) return ((byte*)((ulong)((stbi__err("wrong color format")) != 0?((byte *)null):(null))));
			stbi__skip(s, (int)(stbi__get32be(s)));
			stbi__skip(s, (int)(stbi__get32be(s)));
			stbi__skip(s, (int)(stbi__get32be(s)));
			compression = (int)(stbi__get16be(s));
			if ((compression) > (1)) return ((byte*)((ulong)((stbi__err("bad compression")) != 0?((byte *)null):(null))));
			_out_ = (byte*)(stbi__malloc((ulong)(4 * w * h)));
			if (_out_== null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));
			pixelCount = (int)(w * h);
			if ((compression) != 0) {
stbi__skip(s, (int)(h * channelCount * 2));for (channel = (int)(0); (channel) < (4); channel++) {
byte* p;p = _out_ + channel;if ((channel) >= (channelCount)) {
for (i = (int)(0); (i) < (pixelCount); i++ , p += 4) {*p = (byte)((channel) == (3)?255:0);}}
 else {
count = (int)(0);while ((count) < (pixelCount)) {
len = (int)(stbi__get8(s));if ((len) == (128)) {
}
 else if ((len) < (128)) {
len++;count += (int)(len);while ((len) != 0) {
*p = (byte)(stbi__get8(s));p += 4;len--;}}
 else if ((len) > (128)) {
byte val = 0;len ^= (int)(0x0FF);len += (int)(2);val = (byte)(stbi__get8(s));count += (int)(len);while ((len) != 0) {
*p = (byte)(val);p += 4;len--;}}
}}
}}
 else {
for (channel = (int)(0); (channel) < (4); channel++) {
byte* p;p = _out_ + channel;if ((channel) >= (channelCount)) {
byte val = (byte)((channel) == (3)?255:0);for (i = (int)(0); (i) < (pixelCount); i++ , p += 4) {*p = (byte)(val);}}
 else {
if ((bitdepth) == (16)) {
for (i = (int)(0); (i) < (pixelCount); i++ , p += 4) {*p = ((byte)(stbi__get16be(s) >> 8));}}
 else {
for (i = (int)(0); (i) < (pixelCount); i++ , p += 4) {*p = (byte)(stbi__get8(s));}}
}
}}

			if (((req_comp) != 0) && (req_comp != 4)) {
_out_ = stbi__convert_format(_out_, (int)(4), (int)(req_comp), (uint)(w), (uint)(h));if ((_out_) == (null)) return _out_;}

			if ((comp) != null) *comp = (int)(4);
			*y = (int)(h);
			*x = (int)(w);
			return _out_;
		}

		public static int stbi__gif_test_raw(stbi__context s)
		{
			int sz = 0;
			if ((((stbi__get8(s) != 'G') || (stbi__get8(s) != 'I')) || (stbi__get8(s) != 'F')) || (stbi__get8(s) != '8')) return (int)(0);
			sz = (int)(stbi__get8(s));
			if ((sz != '9') && (sz != '7')) return (int)(0);
			if (stbi__get8(s) != 'a') return (int)(0);
			return (int)(1);
		}

		public static int stbi__gif_test(stbi__context s)
		{
			int r = (int)(stbi__gif_test_raw(s));
			stbi__rewind(s);
			return (int)(r);
		}

		public static int stbi__gif_header(stbi__context s, stbi__gif g, int* comp, int is_info)
		{
			byte version = 0;
			if ((((stbi__get8(s) != 'G') || (stbi__get8(s) != 'I')) || (stbi__get8(s) != 'F')) || (stbi__get8(s) != '8')) return (int)(stbi__err("not GIF"));
			version = (byte)(stbi__get8(s));
			if ((version != '7') && (version != '9')) return (int)(stbi__err("not GIF"));
			if (stbi__get8(s) != 'a') return (int)(stbi__err("not GIF"));
			stbi__g_failure_reason = "";
			g.w = (int)(stbi__get16le(s));
			g.h = (int)(stbi__get16le(s));
			g.flags = (int)(stbi__get8(s));
			g.bgindex = (int)(stbi__get8(s));
			g.ratio = (int)(stbi__get8(s));
			g.transparent = (int)(-1);
			if (comp != null) *comp = (int)(4);
			if ((is_info) != 0) return (int)(1);
			if ((g.flags & 0x80) != 0) stbi__gif_parse_colortable(s, g.pal, (int)(2 << (g.flags & 7)), (int)(-1));
			return (int)(1);
		}

		public static int stbi__gif_info_raw(stbi__context s, int* x, int* y, int* comp)
		{
			stbi__gif g =  new stbi__gif();
			if (stbi__gif_header(s, g, comp, (int)(1))== 0) {
stbi__rewind(s);return (int)(0);}

			if ((x) != null) *x = (int)(g.w);
			if ((y) != null) *y = (int)(g.h);
			return (int)(1);
		}

		public static void stbi__out_gif_code(stbi__gif g, ushort code)
		{
			byte* p;byte* c;
			if ((g.codes[code].prefix) >= (0)) stbi__out_gif_code(g, (ushort)(g.codes[code].prefix));
			if ((g.cur_y) >= (g.max_y)) return;
			p = &g._out_[g.cur_x + g.cur_y];
			c = &g.color_table[g.codes[code].suffix * 4];
			if ((c[3]) >= (128)) {
p[0] = (byte)(c[2]);p[1] = (byte)(c[1]);p[2] = (byte)(c[0]);p[3] = (byte)(c[3]);}

			g.cur_x += (int)(4);
			if ((g.cur_x) >= (g.max_x)) {
g.cur_x = (int)(g.start_x);g.cur_y += (int)(g.step);while (((g.cur_y) >= (g.max_y)) && ((g.parse) > (0))) {
g.step = (int)((1 << g.parse) * g.line_size);g.cur_y = (int)(g.start_y + (g.step >> 1));--g.parse;}}

		}

		public static byte* stbi__process_gif_raster(stbi__context s, stbi__gif g)
		{
			byte lzw_cs = 0;
			int len = 0;int init_code = 0;
			uint first = 0;
			int codesize = 0;int codemask = 0;int avail = 0;int oldcode = 0;int bits = 0;int valid_bits = 0;int clear = 0;
			stbi__gif_lzw* p;
			lzw_cs = (byte)(stbi__get8(s));
			if ((lzw_cs) > (12)) return (null);
			clear = (int)(1 << lzw_cs);
			first = (uint)(1);
			codesize = (int)(lzw_cs + 1);
			codemask = (int)((1 << codesize) - 1);
			bits = (int)(0);
			valid_bits = (int)(0);
			for (init_code = (int)(0); (init_code) < (clear); init_code++) {
((stbi__gif_lzw*) (g.codes))[init_code].prefix = (short)(-1);((stbi__gif_lzw*) (g.codes))[init_code].first = ((byte)(init_code));((stbi__gif_lzw*) (g.codes))[init_code].suffix = ((byte)(init_code));}
			avail = (int)(clear + 2);
			oldcode = (int)(-1);
			len = (int)(0);
			for (; ; ) {
if ((valid_bits) < (codesize)) {
if ((len) == (0)) {
len = (int)(stbi__get8(s));if ((len) == (0)) return g._out_;}
--len;bits |= (int)((int)(stbi__get8(s)) << valid_bits);valid_bits += (int)(8);}
 else {
int code = (int)(bits & codemask);bits >>= codesize;valid_bits -= (int)(codesize);if ((code) == (clear)) {
codesize = (int)(lzw_cs + 1);codemask = (int)((1 << codesize) - 1);avail = (int)(clear + 2);oldcode = (int)(-1);first = (uint)(0);}
 else if ((code) == (clear + 1)) {
stbi__skip(s, (int)(len));while ((len = (int)(stbi__get8(s))) > (0)) {stbi__skip(s, (int)(len));}return g._out_;}
 else if (code <= avail) {
if ((first) != 0) return ((byte*)((ulong)((stbi__err("no clear code")) != 0?((byte *)null):(null))));if ((oldcode) >= (0)) {
p = (stbi__gif_lzw*)g.codes + avail++;if ((avail) > (4096)) return ((byte*)((ulong)((stbi__err("too many codes")) != 0?((byte *)null):(null))));p->prefix = ((short)(oldcode));p->first = (byte)(g.codes[oldcode].first);p->suffix = (byte)(((code) == (avail))?p->first:g.codes[code].first);}
 else if ((code) == (avail)) return ((byte*)((ulong)((stbi__err("illegal code in raster")) != 0?((byte *)null):(null))));stbi__out_gif_code(g, (ushort)(code));if (((avail & codemask) == (0)) && (avail <= 0x0FFF)) {
codesize++;codemask = (int)((1 << codesize) - 1);}
oldcode = (int)(code);}
 else {
return ((byte*)((ulong)((stbi__err("illegal code in raster")) != 0?((byte *)null):(null))));}
}
}
		}

		public static void stbi__fill_gif_background(stbi__gif g, int x0, int y0, int x1, int y1)
		{
			int x = 0;int y = 0;
			byte* c = (byte *)g.pal + g.bgindex;
			for (y = (int)(y0); (y) < (y1); y += (int)(4 * g.w)) {
for (x = (int)(x0); (x) < (x1); x += (int)(4)) {
byte* p = &g._out_[y + x];p[0] = (byte)(c[2]);p[1] = (byte)(c[1]);p[2] = (byte)(c[0]);p[3] = (byte)(0);}}
		}

		public static byte* stbi__gif_load_next(stbi__context s, stbi__gif g, int* comp, int req_comp)
		{
			int i = 0;
			byte* prev_out = null;
			if (((g._out_) == (null)) && (stbi__gif_header(s, g, comp, (int)(0))== 0)) return null;
			prev_out = g._out_;
			g._out_ = (byte*)(stbi__malloc((ulong)(4 * g.w * g.h)));
			if ((g._out_) == (null)) return ((byte*)((ulong)((stbi__err("outofmem")) != 0?((byte *)null):(null))));
			switch ((g.eflags & 0x1C) >> 2){
case 0:stbi__fill_gif_background(g, (int)(0), (int)(0), (int)(4 * g.w), (int)(4 * g.w * g.h));break;case 1:if ((prev_out) != null) CRuntime.memcpy(g._out_, prev_out, (ulong)(4 * g.w * g.h));g.old_out = prev_out;break;case 2:if ((prev_out) != null) CRuntime.memcpy(g._out_, prev_out, (ulong)(4 * g.w * g.h));stbi__fill_gif_background(g, (int)(g.start_x), (int)(g.start_y), (int)(g.max_x), (int)(g.max_y));break;case 3:if ((g.old_out) != null) {
for (i = (int)(g.start_y); (i) < (g.max_y); i += (int)(4 * g.w)) {CRuntime.memcpy(&g._out_[i + g.start_x], &g.old_out[i + g.start_x], (ulong)(g.max_x - g.start_x));}}
break;}

			for (; ; ) {
switch (stbi__get8(s)){
case 0x2C:{
int prev_trans = (int)(-1);int x = 0;int y = 0;int w = 0;int h = 0;byte* o;x = (int)(stbi__get16le(s));y = (int)(stbi__get16le(s));w = (int)(stbi__get16le(s));h = (int)(stbi__get16le(s));if (((x + w) > (g.w)) || ((y + h) > (g.h))) return ((byte*)((ulong)((stbi__err("bad Image Descriptor")) != 0?((byte *)null):(null))));g.line_size = (int)(g.w * 4);g.start_x = (int)(x * 4);g.start_y = (int)(y * g.line_size);g.max_x = (int)(g.start_x + w * 4);g.max_y = (int)(g.start_y + h * g.line_size);g.cur_x = (int)(g.start_x);g.cur_y = (int)(g.start_y);g.lflags = (int)(stbi__get8(s));if ((g.lflags & 0x40) != 0) {
g.step = (int)(8 * g.line_size);g.parse = (int)(3);}
 else {
g.step = (int)(g.line_size);g.parse = (int)(0);}
if ((g.lflags & 0x80) != 0) {
stbi__gif_parse_colortable(s, g.lpal, (int)(2 << (g.lflags & 7)), (int)((g.eflags & 0x01) != 0?g.transparent:-1));g.color_table = (byte*)(g.lpal);}
 else if ((g.flags & 0x80) != 0) {
if (((g.transparent) >= (0)) && ((g.eflags & 0x01)!= 0)) {
prev_trans = (int)(g.pal[g.transparent * 4 +3]);g.pal[g.transparent * 4 +3] = (byte)(0);}
g.color_table = (byte*)(g.pal);}
 else return ((byte*)((ulong)((stbi__err("missing color table")) != 0?((byte *)null):(null))));o = stbi__process_gif_raster(s, g);if ((o) == (null)) return (null);if (prev_trans != -1) g.pal[g.transparent * 4 +3] = ((byte)(prev_trans));return o;}
case 0x21:{
int len = 0;if ((stbi__get8(s)) == (0xF9)) {
len = (int)(stbi__get8(s));if ((len) == (4)) {
g.eflags = (int)(stbi__get8(s));g.delay = (int)(stbi__get16le(s));g.transparent = (int)(stbi__get8(s));}
 else {
stbi__skip(s, (int)(len));break;}
}
while ((len = (int)(stbi__get8(s))) != 0) {stbi__skip(s, (int)(len));}break;}
case 0x3B:return null;default: return ((byte*)((ulong)((stbi__err("unknown code")) != 0?((byte *)null):(null))));}
}
			(void)(req_comp);
		}

		public static byte* stbi__gif_load(stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			byte* u = null;
			stbi__gif g =  new stbi__gif();
			
			u = stbi__gif_load_next(s, g, comp, (int)(req_comp));
			if ((u) == ((byte*)(s))) u = null;
			if ((u) != null) {
*x = (int)(g.w);*y = (int)(g.h);if (((req_comp) != 0) && (req_comp != 4)) u = stbi__convert_format(u, (int)(4), (int)(req_comp), (uint)(g.w), (uint)(g.h));}
 else if ((g._out_) != null) CRuntime.free(g._out_);
			return u;
		}

		public static int stbi__gif_info(stbi__context s, int* x, int* y, int* comp)
		{
			return (int)(stbi__gif_info_raw(s, x, y, comp));
		}

		public static int stbi__bmp_info(stbi__context s, int* x, int* y, int* comp)
		{
			void * p;
			stbi__bmp_data info =  new stbi__bmp_data();
			info.all_a = (uint)(255);
			p = stbi__bmp_parse_header(s, &info);
			stbi__rewind(s);
			if ((p) == (null)) return (int)(0);
			*x = (int)(s.img_x);
			*y = (int)(s.img_y);
			*comp = (int)((info.ma) != 0?4:3);
			return (int)(1);
		}

		public static int stbi__psd_info(stbi__context s, int* x, int* y, int* comp)
		{
			int channelCount = 0;
			if (stbi__get32be(s) != 0x38425053) {
stbi__rewind(s);return (int)(0);}

			if (stbi__get16be(s) != 1) {
stbi__rewind(s);return (int)(0);}

			stbi__skip(s, (int)(6));
			channelCount = (int)(stbi__get16be(s));
			if (((channelCount) < (0)) || ((channelCount) > (16))) {
stbi__rewind(s);return (int)(0);}

			*y = (int)(stbi__get32be(s));
			*x = (int)(stbi__get32be(s));
			if (stbi__get16be(s) != 8) {
stbi__rewind(s);return (int)(0);}

			if (stbi__get16be(s) != 3) {
stbi__rewind(s);return (int)(0);}

			*comp = (int)(4);
			return (int)(1);
		}

		public static int stbi__info_main(stbi__context s, int* x, int* y, int* comp)
		{
			if ((stbi__jpeg_info(s, x, y, comp)) != 0) return (int)(1);
			if ((stbi__png_info(s, x, y, comp)) != 0) return (int)(1);
			if ((stbi__gif_info(s, x, y, comp)) != 0) return (int)(1);
			if ((stbi__bmp_info(s, x, y, comp)) != 0) return (int)(1);
			if ((stbi__psd_info(s, x, y, comp)) != 0) return (int)(1);
			if ((stbi__tga_info(s, x, y, comp)) != 0) return (int)(1);
			return (int)(stbi__err("unknown image type"));
		}

		public static int stbi_info_from_memory(byte* buffer, int len, int* x, int* y, int* comp)
		{
			stbi__context s =  new stbi__context();
			stbi__start_mem(s, buffer, (int)(len));
			return (int)(stbi__info_main(s, x, y, comp));
		}

		public static int stbi_info_from_callbacks(stbi_io_callbacks c, void * user, int* x, int* y, int* comp)
		{
			stbi__context s =  new stbi__context();
			stbi__start_callbacks(s, c, user);
			return (int)(stbi__info_main(s, x, y, comp));
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
			return (int)((a) < (b)?a:b);
		}

		public static int nvg__maxi(int a, int b)
		{
			return (int)((a) > (b)?a:b);
		}

		public static int nvg__clampi(int a, int mn, int mx)
		{
			return (int)((a) < (mn)?mn:((a) > (mx)?mx:a));
		}

		public static float nvg__minf(float a, float b)
		{
			return (float)((a) < (b)?a:b);
		}

		public static float nvg__maxf(float a, float b)
		{
			return (float)((a) > (b)?a:b);
		}

		public static float nvg__absf(float a)
		{
			return (float)((a) >= (0.0f)?a:-a);
		}

		public static float nvg__signf(float a)
		{
			return (float)((a) >= (0.0f)?1.0f:-1.0f);
		}

		public static float nvg__clampf(float a, float mn, float mx)
		{
			return (float)((a) < (mn)?mn:((a) > (mx)?mx:a));
		}

		public static float nvg__cross(float dx0, float dy0, float dx1, float dy1)
		{
			return (float)(dx1 * dy0 - dx0 * dy1);
		}

		public static float nvg__normalize(float* x, float* y)
		{
			float d = (float)(nvg__sqrtf((float)((*x) * (*x) + (*y) * (*y))));
			if ((d) > (1e-6f)) {
float id = (float)(1.0f / d);*x *= (float)(id);*y *= (float)(id);}

			return (float)(d);
		}

		public static void nvg__deletePathCache(NVGpathCache* c)
		{
			if ((c) == (null)) return;
			if (c->points != (null)) CRuntime.free(c->points);
			if (c->paths != (null)) CRuntime.free(c->paths);
			if (c->verts != (null)) CRuntime.free(c->verts);
			CRuntime.free(c);
		}

		public static NVGpathCache* nvg__allocPathCache()
		{
			NVGpathCache* c = (NVGpathCache*)(CRuntime.malloc((ulong)(sizeof(NVGpathCache))));
			if ((c) == (null)) goto error;
			CRuntime.memset(c, (int)(0), (ulong)(sizeof(NVGpathCache)));
			c->points = (NVGpoint*)(CRuntime.malloc((ulong)(sizeof(NVGpoint) * 128)));
			if (c->points== null) goto error;
			c->npoints = (int)(0);
			c->cpoints = (int)(128);
			c->paths = (NVGpath*)(CRuntime.malloc((ulong)(sizeof(NVGpath) * 16)));
			if (c->paths== null) goto error;
			c->npaths = (int)(0);
			c->cpaths = (int)(16);
			c->verts = (NVGvertex*)(CRuntime.malloc((ulong)(sizeof(NVGvertex) * 256)));
			if (c->verts== null) goto error;
			c->nverts = (int)(0);
			c->cverts = (int)(256);
			return c;
			error:;
nvg__deletePathCache(c);
			return (null);
		}

		public static void nvg__setDevicePixelRatio(NVGcontext* ctx, float ratio)
		{
			ctx->tessTol = (float)(0.25f / ratio);
			ctx->distTol = (float)(0.01f / ratio);
			ctx->fringeWidth = (float)(1.0f / ratio);
			ctx->devicePxRatio = (float)(ratio);
		}

		public static NVGcompositeOperationState nvg__compositeOperationState(int op)
		{
			int sfactor = 0;int dfactor = 0;
			if ((op) == (NVG_SOURCE_OVER)) {
sfactor = (int)(NVG_ONE);dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);}
 else if ((op) == (NVG_SOURCE_IN)) {
sfactor = (int)(NVG_DST_ALPHA);dfactor = (int)(NVG_ZERO);}
 else if ((op) == (NVG_SOURCE_OUT)) {
sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);dfactor = (int)(NVG_ZERO);}
 else if ((op) == (NVG_ATOP)) {
sfactor = (int)(NVG_DST_ALPHA);dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);}
 else if ((op) == (NVG_DESTINATION_OVER)) {
sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);dfactor = (int)(NVG_ONE);}
 else if ((op) == (NVG_DESTINATION_IN)) {
sfactor = (int)(NVG_ZERO);dfactor = (int)(NVG_SRC_ALPHA);}
 else if ((op) == (NVG_DESTINATION_OUT)) {
sfactor = (int)(NVG_ZERO);dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);}
 else if ((op) == (NVG_DESTINATION_ATOP)) {
sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);dfactor = (int)(NVG_SRC_ALPHA);}
 else if ((op) == (NVG_LIGHTER)) {
sfactor = (int)(NVG_ONE);dfactor = (int)(NVG_ONE);}
 else if ((op) == (NVG_COPY)) {
sfactor = (int)(NVG_ONE);dfactor = (int)(NVG_ZERO);}
 else if ((op) == (NVG_XOR)) {
sfactor = (int)(NVG_ONE_MINUS_DST_ALPHA);dfactor = (int)(NVG_ONE_MINUS_SRC_ALPHA);}
 else {
sfactor = (int)(NVG_ONE);dfactor = (int)(NVG_ZERO);}

			NVGcompositeOperationState state =  new NVGcompositeOperationState();
			state.srcRGB = (int)(sfactor);
			state.dstRGB = (int)(dfactor);
			state.srcAlpha = (int)(sfactor);
			state.dstAlpha = (int)(dfactor);
			return (NVGcompositeOperationState)(state);
		}

		public static NVGstate* nvg__getState(NVGcontext* ctx)
		{
			return &ctx->states[ctx->nstates - 1];
		}

		public static NVGcontext* nvgCreateInternal(NVGparams* params)
		{
			FONSparams fontParams =  new FONSparams();
			NVGcontext* ctx = (NVGcontext*)(CRuntime.malloc((ulong)(sizeof(NVGcontext))));
			int i = 0;
			if ((ctx) == (null)) goto error;
			CRuntime.memset(ctx, (int)(0), (ulong)(sizeof(NVGcontext)));
			ctx->params = (NVGparams)(*params);
			for (i = (int)(0); (i) < (4); i++) {ctx->fontImages[i] = (int)(0);}
			ctx->commands = (float*)(CRuntime.malloc((ulong)(sizeof(float) * 256)));
			if (ctx->commands== null) goto error;
			ctx->ncommands = (int)(0);
			ctx->ccommands = (int)(256);
			ctx->cache = nvg__allocPathCache();
			if ((ctx->cache) == (null)) goto error;
			nvgSave(ctx);
			nvgReset(ctx);
			nvg__setDevicePixelRatio(ctx, (float)(1.0f));
			if ((ctx->params.renderCreate(ctx->params.userPtr)) == (0)) goto error;
			CRuntime.memset(&fontParams, (int)(0), (ulong)(sizeof((fontParams))));
			fontParams.width = (int)(512);
			fontParams.height = (int)(512);
			fontParams.flags = (byte)(FONS_ZERO_TOPLEFT);
			fontParams.renderCreate = (null);
			fontParams.renderUpdate = (null);
			fontParams.renderDraw = (null);
			fontParams.renderDelete = (null);
			fontParams.userPtr = (null);
			ctx->fs = fonsCreateInternal(&fontParams);
			if ((ctx->fs) == (null)) goto error;
			ctx->fontImages[0] = (int)(ctx->params.renderCreateTexture(ctx->params.userPtr, (int)(NVG_TEXTURE_ALPHA), (int)(fontParams.width), (int)(fontParams.height), (int)(0), (null)));
			if ((ctx->fontImages[0]) == (0)) goto error;
			ctx->fontImageIdx = (int)(0);
			return ctx;
			error:;
nvgDeleteInternal(ctx);
			return null;
		}

		public static NVGparams* nvgInternalParams(NVGcontext* ctx)
		{
			return &ctx->params;
		}

		public static void nvgDeleteInternal(NVGcontext* ctx)
		{
			int i = 0;
			if ((ctx) == (null)) return;
			if (ctx->commands != (null)) CRuntime.free(ctx->commands);
			if (ctx->cache != (null)) nvg__deletePathCache(ctx->cache);
			if ((ctx->fs) != null) fonsDeleteInternal(ctx->fs);
			for (i = (int)(0); (i) < (4); i++) {
if (ctx->fontImages[i] != 0) {
nvgDeleteImage(ctx, (int)(ctx->fontImages[i]));ctx->fontImages[i] = (int)(0);}
}
			if (ctx->params.renderDelete != (null)) ctx->params.renderDelete(ctx->params.userPtr);
			CRuntime.free(ctx);
		}

		public static void nvgBeginFrame(NVGcontext* ctx, float windowWidth, float windowHeight, float devicePixelRatio)
		{
			ctx->nstates = (int)(0);
			nvgSave(ctx);
			nvgReset(ctx);
			nvg__setDevicePixelRatio(ctx, (float)(devicePixelRatio));
			ctx->params.renderViewport(ctx->params.userPtr, (float)(windowWidth), (float)(windowHeight), (float)(devicePixelRatio));
			ctx->drawCallCount = (int)(0);
			ctx->fillTriCount = (int)(0);
			ctx->strokeTriCount = (int)(0);
			ctx->textTriCount = (int)(0);
		}

		public static void nvgCancelFrame(NVGcontext* ctx)
		{
			ctx->params.renderCancel(ctx->params.userPtr);
		}

		public static void nvgEndFrame(NVGcontext* ctx)
		{
			ctx->params.renderFlush(ctx->params.userPtr);
			if (ctx->fontImageIdx != 0) {
int fontImage = (int)(ctx->fontImages[ctx->fontImageIdx]);int i = 0;int j = 0;int iw = 0;int ih = 0;if ((fontImage) == (0)) return;nvgImageSize(ctx, (int)(fontImage), &iw, &ih);for (i = (int)(j = (int)(0)); (i) < (ctx->fontImageIdx); i++) {
if (ctx->fontImages[i] != 0) {
int nw = 0;int nh = 0;nvgImageSize(ctx, (int)(ctx->fontImages[i]), &nw, &nh);if (((nw) < (iw)) || ((nh) < (ih))) nvgDeleteImage(ctx, (int)(ctx->fontImages[i])); else ctx->fontImages[j++] = (int)(ctx->fontImages[i]);}
}ctx->fontImages[j++] = (int)(ctx->fontImages[0]);ctx->fontImages[0] = (int)(fontImage);ctx->fontImageIdx = (int)(0);for (i = (int)(j); (i) < (4); i++) {ctx->fontImages[i] = (int)(0);}}

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
			NVGcolor color =  new NVGcolor();
			color..r = (float)(r / 255.0f);
			color..g = (float)(g / 255.0f);
			color..b = (float)(b / 255.0f);
			color..a = (float)(a / 255.0f);
			return (NVGcolor)(color);
		}

		public static NVGcolor nvgRGBAf(float r, float g, float b, float a)
		{
			NVGcolor color =  new NVGcolor();
			color..r = (float)(r);
			color..g = (float)(g);
			color..b = (float)(b);
			color..a = (float)(a);
			return (NVGcolor)(color);
		}

		public static NVGcolor nvgTransRGBA(NVGcolor c, byte a)
		{
			c..a = (float)(a / 255.0f);
			return (NVGcolor)(c);
		}

		public static NVGcolor nvgTransRGBAf(NVGcolor c, float a)
		{
			c..a = (float)(a);
			return (NVGcolor)(c);
		}

		public static NVGcolor nvgLerpRGBA(NVGcolor c0, NVGcolor c1, float u)
		{
			int i = 0;
			float oneminu = 0;
			NVGcolor cint = (NVGcolor)({ { { 0 } } });
			u = (float)(nvg__clampf((float)(u), (float)(0.0f), (float)(1.0f)));
			oneminu = (float)(1.0f - u);
			for (i = (int)(0); (i) < (4); i++) {
cint.rgba[i] = (float)(c0.rgba[i] * oneminu + c1.rgba[i] * u);}
			return (NVGcolor)(cint);
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
			float m1 = 0;float m2 = 0;
			NVGcolor col =  new NVGcolor();
			h = (float)(nvg__modf((float)(h), (float)(1.0f)));
			if ((h) < (0.0f)) h += (float)(1.0f);
			s = (float)(nvg__clampf((float)(s), (float)(0.0f), (float)(1.0f)));
			l = (float)(nvg__clampf((float)(l), (float)(0.0f), (float)(1.0f)));
			m2 = (float)(l <= 0.5f?(l * (1 + s)):(l + s - l * s));
			m1 = (float)(2 * l - m2);
			col..r = (float)(nvg__clampf((float)(nvg__hue((float)(h + 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			col..g = (float)(nvg__clampf((float)(nvg__hue((float)(h), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			col..b = (float)(nvg__clampf((float)(nvg__hue((float)(h - 1.0f / 3.0f), (float)(m1), (float)(m2))), (float)(0.0f), (float)(1.0f)));
			col..a = (float)(a / 255.0f);
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
			float cs = (float)(nvg__cosf((float)(a)));float sn = (float)(nvg__sinf((float)(a)));
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
			double invdet = 0;double det = (double)((double)(t[0]) * t[3] - (double)(t[2]) * t[1]);
			if (((det) > (-1e-6)) && ((det) < (1e-6))) {
nvgTransformIdentity(inv);return (int)(0);}

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
			CRuntime.memset(p, (int)(0), (ulong)(sizeof((*p))));
			nvgTransformIdentity(p->xform);
			p->radius = (float)(0.0f);
			p->feather = (float)(1.0f);
			p->innerColor = (NVGcolor)(color);
			p->outerColor = (NVGcolor)(color);
		}

		public static void nvgSave(NVGcontext* ctx)
		{
			if ((ctx->nstates) >= (32)) return;
			if ((ctx->nstates) > (0)) CRuntime.memcpy(&ctx->states[ctx->nstates], &ctx->states[ctx->nstates - 1], (ulong)(sizeof(NVGstate)));
			ctx->nstates++;
		}

		public static void nvgRestore(NVGcontext* ctx)
		{
			if (ctx->nstates <= 1) return;
			ctx->nstates--;
		}

		public static void nvgReset(NVGcontext* ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			CRuntime.memset(state, (int)(0), (ulong)(sizeof((*state))));
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

		public static void nvgShapeAntiAlias(NVGcontext* ctx, int enabled)
		{
			NVGstate* state = nvg__getState(ctx);
			state->shapeAntiAlias = (int)(enabled);
		}

		public static void nvgStrokeWidth(NVGcontext* ctx, float width)
		{
			NVGstate* state = nvg__getState(ctx);
			state->strokeWidth = (float)(width);
		}

		public static void nvgMiterLimit(NVGcontext* ctx, float limit)
		{
			NVGstate* state = nvg__getState(ctx);
			state->miterLimit = (float)(limit);
		}

		public static void nvgLineCap(NVGcontext* ctx, int cap)
		{
			NVGstate* state = nvg__getState(ctx);
			state->lineCap = (int)(cap);
		}

		public static void nvgLineJoin(NVGcontext* ctx, int join)
		{
			NVGstate* state = nvg__getState(ctx);
			state->lineJoin = (int)(join);
		}

		public static void nvgGlobalAlpha(NVGcontext* ctx, float alpha)
		{
			NVGstate* state = nvg__getState(ctx);
			state->alpha = (float)(alpha);
		}

		public static void nvgTransform(NVGcontext* ctx, float a, float b, float c, float d, float e, float f)
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

		public static void nvgResetTransform(NVGcontext* ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			nvgTransformIdentity(state->xform);
		}

		public static void nvgTranslate(NVGcontext* ctx, float x, float y)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformTranslate(t, (float)(x), (float)(y));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgRotate(NVGcontext* ctx, float angle)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformRotate(t, (float)(angle));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgSkewX(NVGcontext* ctx, float angle)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformSkewX(t, (float)(angle));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgSkewY(NVGcontext* ctx, float angle)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformSkewY(t, (float)(angle));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgScale(NVGcontext* ctx, float x, float y)
		{
			NVGstate* state = nvg__getState(ctx);
			float* t = stackalloc float[6];
			nvgTransformScale(t, (float)(x), (float)(y));
			nvgTransformPremultiply(state->xform, t);
		}

		public static void nvgCurrentTransform(NVGcontext* ctx, float* xform)
		{
			NVGstate* state = nvg__getState(ctx);
			if ((xform) == (null)) return;
			CRuntime.memcpy(xform, state->xform, (ulong)(sizeof(float) * 6));
		}

		public static void nvgStrokeColor(NVGcontext* ctx, NVGcolor color)
		{
			NVGstate* state = nvg__getState(ctx);
			nvg__setPaintColor(&state->stroke, (NVGcolor)(color));
		}

		public static void nvgStrokePaint(NVGcontext* ctx, NVGpaint paint)
		{
			NVGstate* state = nvg__getState(ctx);
			state->stroke = (NVGpaint)(paint);
			nvgTransformMultiply(state->stroke.xform, state->xform);
		}

		public static void nvgFillColor(NVGcontext* ctx, NVGcolor color)
		{
			NVGstate* state = nvg__getState(ctx);
			nvg__setPaintColor(&state->fill, (NVGcolor)(color));
		}

		public static void nvgFillPaint(NVGcontext* ctx, NVGpaint paint)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fill = (NVGpaint)(paint);
			nvgTransformMultiply(state->fill.xform, state->xform);
		}

		public static int nvgCreateImage(NVGcontext* ctx, sbyte* filename, int imageFlags)
		{
			int w = 0;int h = 0;int n = 0;int image = 0;
			byte* img;
			stbi_set_unpremultiply_on_load((int)(1));
			stbi_convert_iphone_png_to_rgb((int)(1));
			img = stbi_load(filename, &w, &h, &n, (int)(4));
			if ((img) == (null)) {
return (int)(0);}

			image = (int)(nvgCreateImageRGBA(ctx, (int)(w), (int)(h), (int)(imageFlags), img));
			stbi_image_free(img);
			return (int)(image);
		}

		public static int nvgCreateImageMem(NVGcontext* ctx, int imageFlags, byte* data, int ndata)
		{
			int w = 0;int h = 0;int n = 0;int image = 0;
			byte* img = stbi_load_from_memory(data, (int)(ndata), &w, &h, &n, (int)(4));
			if ((img) == (null)) {
return (int)(0);}

			image = (int)(nvgCreateImageRGBA(ctx, (int)(w), (int)(h), (int)(imageFlags), img));
			stbi_image_free(img);
			return (int)(image);
		}

		public static int nvgCreateImageRGBA(NVGcontext* ctx, int w, int h, int imageFlags, byte* data)
		{
			return (int)(ctx->params.renderCreateTexture(ctx->params.userPtr, (int)(NVG_TEXTURE_RGBA), (int)(w), (int)(h), (int)(imageFlags), data));
		}

		public static void nvgUpdateImage(NVGcontext* ctx, int image, byte* data)
		{
			int w = 0;int h = 0;
			ctx->params.renderGetTextureSize(ctx->params.userPtr, (int)(image), &w, &h);
			ctx->params.renderUpdateTexture(ctx->params.userPtr, (int)(image), (int)(0), (int)(0), (int)(w), (int)(h), data);
		}

		public static void nvgImageSize(NVGcontext* ctx, int image, int* w, int* h)
		{
			ctx->params.renderGetTextureSize(ctx->params.userPtr, (int)(image), w, h);
		}

		public static void nvgDeleteImage(NVGcontext* ctx, int image)
		{
			ctx->params.renderDeleteTexture(ctx->params.userPtr, (int)(image));
		}

		public static NVGpaint nvgLinearGradient(NVGcontext* ctx, float sx, float sy, float ex, float ey, NVGcolor icol, NVGcolor ocol)
		{
			NVGpaint p =  new NVGpaint();
			float dx = 0;float dy = 0;float d = 0;
			float large = (float)(1e5);
			for (; ; ) {
((1) != 0?(void)(0):((void)(ctx)));break;}
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof((p))));
			dx = (float)(ex - sx);
			dy = (float)(ey - sy);
			d = (float)(sqrtf((float)(dx * dx + dy * dy)));
			if ((d) > (0.0001f)) {
dx /= (float)(d);dy /= (float)(d);}
 else {
dx = (float)(0);dy = (float)(1);}

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

		public static NVGpaint nvgRadialGradient(NVGcontext* ctx, float cx, float cy, float inr, float outr, NVGcolor icol, NVGcolor ocol)
		{
			NVGpaint p =  new NVGpaint();
			float r = (float)((inr + outr) * 0.5f);
			float f = (float)(outr - inr);
			for (; ; ) {
((1) != 0?(void)(0):((void)(ctx)));break;}
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof((p))));
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

		public static NVGpaint nvgBoxGradient(NVGcontext* ctx, float x, float y, float w, float h, float r, float f, NVGcolor icol, NVGcolor ocol)
		{
			NVGpaint p =  new NVGpaint();
			for (; ; ) {
((1) != 0?(void)(0):((void)(ctx)));break;}
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof((p))));
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

		public static NVGpaint nvgImagePattern(NVGcontext* ctx, float cx, float cy, float w, float h, float angle, int image, float alpha)
		{
			NVGpaint p =  new NVGpaint();
			for (; ; ) {
((1) != 0?(void)(0):((void)(ctx)));break;}
			CRuntime.memset(&p, (int)(0), (ulong)(sizeof((p))));
			nvgTransformRotate(p.xform, (float)(angle));
			p.xform[4] = (float)(cx);
			p.xform[5] = (float)(cy);
			p.extent[0] = (float)(w);
			p.extent[1] = (float)(h);
			p.image = (int)(image);
			p.innerColor = (NVGcolor)(p.outerColor = (NVGcolor)(nvgRGBAf((float)(1), (float)(1), (float)(1), (float)(alpha))));
			return (NVGpaint)(p);
		}

		public static void nvgScissor(NVGcontext* ctx, float x, float y, float w, float h)
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

		public static void nvgIntersectScissor(NVGcontext* ctx, float x, float y, float w, float h)
		{
			NVGstate* state = nvg__getState(ctx);
			float* pxform = stackalloc float[6];float* invxorm = stackalloc float[6];
			float* rect = stackalloc float[4];
			float ex = 0;float ey = 0;float tex = 0;float tey = 0;
			if ((state->scissor.extent[0]) < (0)) {
nvgScissor(ctx, (float)(x), (float)(y), (float)(w), (float)(h));return;}

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

		public static void nvgResetScissor(NVGcontext* ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			CRuntime.memset(state->scissor.xform, (int)(0), (ulong)(sizeof((state->scissor.xform))));
			state->scissor.extent[0] = (float)(-1.0f);
			state->scissor.extent[1] = (float)(-1.0f);
		}

		public static void nvgGlobalCompositeOperation(NVGcontext* ctx, int op)
		{
			NVGstate* state = nvg__getState(ctx);
			state->compositeOperation = (NVGcompositeOperationState)(nvg__compositeOperationState((int)(op)));
		}

		public static void nvgGlobalCompositeBlendFunc(NVGcontext* ctx, int sfactor, int dfactor)
		{
			nvgGlobalCompositeBlendFuncSeparate(ctx, (int)(sfactor), (int)(dfactor), (int)(sfactor), (int)(dfactor));
		}

		public static void nvgGlobalCompositeBlendFuncSeparate(NVGcontext* ctx, int srcRGB, int dstRGB, int srcAlpha, int dstAlpha)
		{
			NVGcompositeOperationState op =  new NVGcompositeOperationState();
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
			return (int)((dx * dx + dy * dy) < (tol * tol)?1:0);
		}

		public static float nvg__distPtSeg(float x, float y, float px, float py, float qx, float qy)
		{
			float pqx = 0;float pqy = 0;float dx = 0;float dy = 0;float d = 0;float t = 0;
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

		public static void nvg__appendCommands(NVGcontext* ctx, float* vals, int nvals)
		{
			NVGstate* state = nvg__getState(ctx);
			int i = 0;
			if ((ctx->ncommands + nvals) > (ctx->ccommands)) {
float* commands;int ccommands = (int)(ctx->ncommands + nvals + ctx->ccommands / 2);commands = (float*)(CRuntime.realloc(ctx->commands, (ulong)(sizeof(float) * ccommands)));if ((commands) == (null)) return;ctx->commands = commands;ctx->ccommands = (int)(ccommands);}

			if (((int)(vals[0]) != NVG_CLOSE) && ((int)(vals[0]) != NVG_WINDING)) {
ctx->commandx = (float)(vals[nvals - 2]);ctx->commandy = (float)(vals[nvals - 1]);}

			i = (int)(0);
			while ((i) < (nvals)) {
int cmd = (int)(vals[i]);switch (cmd){
case NVG_MOVETO:nvgTransformPoint(&vals[i + 1], &vals[i + 2], state->xform, (float)(vals[i + 1]), (float)(vals[i + 2]));i += (int)(3);break;case NVG_LINETO:nvgTransformPoint(&vals[i + 1], &vals[i + 2], state->xform, (float)(vals[i + 1]), (float)(vals[i + 2]));i += (int)(3);break;case NVG_BEZIERTO:nvgTransformPoint(&vals[i + 1], &vals[i + 2], state->xform, (float)(vals[i + 1]), (float)(vals[i + 2]));nvgTransformPoint(&vals[i + 3], &vals[i + 4], state->xform, (float)(vals[i + 3]), (float)(vals[i + 4]));nvgTransformPoint(&vals[i + 5], &vals[i + 6], state->xform, (float)(vals[i + 5]), (float)(vals[i + 6]));i += (int)(7);break;case NVG_CLOSE:i++;break;case NVG_WINDING:i += (int)(2);break;default: i++;}
}
			CRuntime.memcpy(&ctx->commands[ctx->ncommands], vals, (ulong)(nvals * sizeof(float)));
			ctx->ncommands += (int)(nvals);
		}

		public static void nvg__clearPathCache(NVGcontext* ctx)
		{
			ctx->cache->npoints = (int)(0);
			ctx->cache->npaths = (int)(0);
		}

		public static NVGpath* nvg__lastPath(NVGcontext* ctx)
		{
			if ((ctx->cache->npaths) > (0)) return &ctx->cache->paths[ctx->cache->npaths - 1];
			return (null);
		}

		public static void nvg__addPath(NVGcontext* ctx)
		{
			NVGpath* path;
			if ((ctx->cache->npaths + 1) > (ctx->cache->cpaths)) {
NVGpath* paths;int cpaths = (int)(ctx->cache->npaths + 1 + ctx->cache->cpaths / 2);paths = (NVGpath*)(CRuntime.realloc(ctx->cache->paths, (ulong)(sizeof(NVGpath) * cpaths)));if ((paths) == (null)) return;ctx->cache->paths = paths;ctx->cache->cpaths = (int)(cpaths);}

			path = &ctx->cache->paths[ctx->cache->npaths];
			CRuntime.memset(path, (int)(0), (ulong)(sizeof((*path))));
			path->first = (int)(ctx->cache->npoints);
			path->winding = (int)(NVG_CCW);
			ctx->cache->npaths++;
		}

		public static NVGpoint* nvg__lastPoint(NVGcontext* ctx)
		{
			if ((ctx->cache->npoints) > (0)) return &ctx->cache->points[ctx->cache->npoints - 1];
			return (null);
		}

		public static void nvg__addPoint(NVGcontext* ctx, float x, float y, int flags)
		{
			NVGpath* path = nvg__lastPath(ctx);
			NVGpoint* pt;
			if ((path) == (null)) return;
			if (((path->count) > (0)) && ((ctx->cache->npoints) > (0))) {
pt = nvg__lastPoint(ctx);if ((nvg__ptEquals((float)(pt->x), (float)(pt->y), (float)(x), (float)(y), (float)(ctx->distTol))) != 0) {
pt->flags |= (byte)(flags);return;}
}

			if ((ctx->cache->npoints + 1) > (ctx->cache->cpoints)) {
NVGpoint* points;int cpoints = (int)(ctx->cache->npoints + 1 + ctx->cache->cpoints / 2);points = (NVGpoint*)(CRuntime.realloc(ctx->cache->points, (ulong)(sizeof(NVGpoint) * cpoints)));if ((points) == (null)) return;ctx->cache->points = points;ctx->cache->cpoints = (int)(cpoints);}

			pt = &ctx->cache->points[ctx->cache->npoints];
			CRuntime.memset(pt, (int)(0), (ulong)(sizeof((*pt))));
			pt->x = (float)(x);
			pt->y = (float)(y);
			pt->flags = ((byte)(flags));
			ctx->cache->npoints++;
			path->count++;
		}

		public static void nvg__closePath(NVGcontext* ctx)
		{
			NVGpath* path = nvg__lastPath(ctx);
			if ((path) == (null)) return;
			path->closed = (byte)(1);
		}

		public static void nvg__pathWinding(NVGcontext* ctx, int winding)
		{
			NVGpath* path = nvg__lastPath(ctx);
			if ((path) == (null)) return;
			path->winding = (int)(winding);
		}

		public static float nvg__getAverageScale(float* t)
		{
			float sx = (float)(sqrtf((float)(t[0] * t[0] + t[2] * t[2])));
			float sy = (float)(sqrtf((float)(t[1] * t[1] + t[3] * t[3])));
			return (float)((sx + sy) * 0.5f);
		}

		public static NVGvertex* nvg__allocTempVerts(NVGcontext* ctx, int nverts)
		{
			if ((nverts) > (ctx->cache->cverts)) {
NVGvertex* verts;int cverts = (int)((nverts + 0xff) & ~0xff);verts = (NVGvertex*)(CRuntime.realloc(ctx->cache->verts, (ulong)(sizeof(NVGvertex) * cverts)));if ((verts) == (null)) return (null);ctx->cache->verts = verts;ctx->cache->cverts = (int)(cverts);}

			return ctx->cache->verts;
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
NVGpoint* a = &pts[0];NVGpoint* b = &pts[i - 1];NVGpoint* c = &pts[i];area += (float)(nvg__triarea2((float)(a->x), (float)(a->y), (float)(b->x), (float)(b->y), (float)(c->x), (float)(c->y)));}
			return (float)(area * 0.5f);
		}

		public static void nvg__polyReverse(NVGpoint* pts, int npts)
		{
			NVGpoint tmp =  new NVGpoint();
			int i = (int)(0);int j = (int)(npts - 1);
			while ((i) < (j)) {
tmp = (NVGpoint)(pts[i]);pts[i] = (NVGpoint)(pts[j]);pts[j] = (NVGpoint)(tmp);i++;j--;}
		}

		public static void nvg__vset(NVGvertex* vtx, float x, float y, float u, float v)
		{
			vtx->x = (float)(x);
			vtx->y = (float)(y);
			vtx->u = (float)(u);
			vtx->v = (float)(v);
		}

		public static void nvg__tesselateBezier(NVGcontext* ctx, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, int level, int type)
		{
			float x12 = 0;float y12 = 0;float x23 = 0;float y23 = 0;float x34 = 0;float y34 = 0;float x123 = 0;float y123 = 0;float x234 = 0;float y234 = 0;float x1234 = 0;float y1234 = 0;
			float dx = 0;float dy = 0;float d2 = 0;float d3 = 0;
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
			if (((d2 + d3) * (d2 + d3)) < (ctx->tessTol * (dx * dx + dy * dy))) {
nvg__addPoint(ctx, (float)(x4), (float)(y4), (int)(type));return;}

			x234 = (float)((x23 + x34) * 0.5f);
			y234 = (float)((y23 + y34) * 0.5f);
			x1234 = (float)((x123 + x234) * 0.5f);
			y1234 = (float)((y123 + y234) * 0.5f);
			nvg__tesselateBezier(ctx, (float)(x1), (float)(y1), (float)(x12), (float)(y12), (float)(x123), (float)(y123), (float)(x1234), (float)(y1234), (int)(level + 1), (int)(0));
			nvg__tesselateBezier(ctx, (float)(x1234), (float)(y1234), (float)(x234), (float)(y234), (float)(x34), (float)(y34), (float)(x4), (float)(y4), (int)(level + 1), (int)(type));
		}

		public static void nvg__flattenPaths(NVGcontext* ctx)
		{
			NVGpathCache* cache = ctx->cache;
			NVGpoint* last;
			NVGpoint* p0;
			NVGpoint* p1;
			NVGpoint* pts;
			NVGpath* path;
			int i = 0;int j = 0;
			float* cp1;
			float* cp2;
			float* p;
			float area = 0;
			if ((cache->npaths) > (0)) return;
			i = (int)(0);
			while ((i) < (ctx->ncommands)) {
int cmd = (int)(ctx->commands[i]);switch (cmd){
case NVG_MOVETO:nvg__addPath(ctx);p = &ctx->commands[i + 1];nvg__addPoint(ctx, (float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER));i += (int)(3);break;case NVG_LINETO:p = &ctx->commands[i + 1];nvg__addPoint(ctx, (float)(p[0]), (float)(p[1]), (int)(NVG_PT_CORNER));i += (int)(3);break;case NVG_BEZIERTO:last = nvg__lastPoint(ctx);if (last != (null)) {
cp1 = &ctx->commands[i + 1];cp2 = &ctx->commands[i + 3];p = &ctx->commands[i + 5];nvg__tesselateBezier(ctx, (float)(last->x), (float)(last->y), (float)(cp1[0]), (float)(cp1[1]), (float)(cp2[0]), (float)(cp2[1]), (float)(p[0]), (float)(p[1]), (int)(0), (int)(NVG_PT_CORNER));}
i += (int)(7);break;case NVG_CLOSE:nvg__closePath(ctx);i++;break;case NVG_WINDING:nvg__pathWinding(ctx, (int)(ctx->commands[i + 1]));i += (int)(2);break;default: i++;}
}
			cache->bounds[0] = (float)(cache->bounds[1] = (float)(1e6f));
			cache->bounds[2] = (float)(cache->bounds[3] = (float)(-1e6f));
			for (j = (int)(0); (j) < (cache->npaths); j++) {
path = &cache->paths[j];pts = &cache->points[path->first];p0 = &pts[path->count - 1];p1 = &pts[0];if ((nvg__ptEquals((float)(p0->x), (float)(p0->y), (float)(p1->x), (float)(p1->y), (float)(ctx->distTol))) != 0) {
path->count--;p0 = &pts[path->count - 1];path->closed = (byte)(1);}
if ((path->count) > (2)) {
area = (float)(nvg__polyArea(pts, (int)(path->count)));if (((path->winding) == (NVG_CCW)) && ((area) < (0.0f))) nvg__polyReverse(pts, (int)(path->count));if (((path->winding) == (NVG_CW)) && ((area) > (0.0f))) nvg__polyReverse(pts, (int)(path->count));}
for (i = (int)(0); (i) < (path->count); i++) {
p0->dx = (float)(p1->x - p0->x);p0->dy = (float)(p1->y - p0->y);p0->len = (float)(nvg__normalize(&p0->dx, &p0->dy));cache->bounds[0] = (float)(nvg__minf((float)(cache->bounds[0]), (float)(p0->x)));cache->bounds[1] = (float)(nvg__minf((float)(cache->bounds[1]), (float)(p0->y)));cache->bounds[2] = (float)(nvg__maxf((float)(cache->bounds[2]), (float)(p0->x)));cache->bounds[3] = (float)(nvg__maxf((float)(cache->bounds[3]), (float)(p0->y)));p0 = p1++;}}
		}

		public static int nvg__curveDivs(float r, float arc, float tol)
		{
			float da = (float)(acosf((float)(r / (r + tol))) * 2.0f);
			return (int)(nvg__maxi((int)(2), (int)(ceilf((float)(arc / da)))));
		}

		public static void nvg__chooseBevel(int bevel, NVGpoint* p0, NVGpoint* p1, float w, float* x0, float* y0, float* x1, float* y1)
		{
			if ((bevel) != 0) {
*x0 = (float)(p1->x + p0->dy * w);*y0 = (float)(p1->y - p0->dx * w);*x1 = (float)(p1->x + p1->dy * w);*y1 = (float)(p1->y - p1->dx * w);}
 else {
*x0 = (float)(p1->x + p1->dmx * w);*y0 = (float)(p1->y + p1->dmy * w);*x1 = (float)(p1->x + p1->dmx * w);*y1 = (float)(p1->y + p1->dmy * w);}

		}

		public static NVGvertex* nvg__roundJoin(NVGvertex* dst, NVGpoint* p0, NVGpoint* p1, float lw, float rw, float lu, float ru, int ncap, float fringe)
		{
			int i = 0;int n = 0;
			float dlx0 = (float)(p0->dy);
			float dly0 = (float)(-p0->dx);
			float dlx1 = (float)(p1->dy);
			float dly1 = (float)(-p1->dx);
			for (; ; ) {
((1) != 0?(void)(0):((void)(fringe)));break;}
			if ((p1->flags & NVG_PT_LEFT) != 0) {
float lx0 = 0;float ly0 = 0;float lx1 = 0;float ly1 = 0;float a0 = 0;float a1 = 0;nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1);a0 = (float)(atan2f((float)(-dly0), (float)(-dlx0)));a1 = (float)(atan2f((float)(-dly1), (float)(-dlx1)));if ((a1) > (a0)) a1 -= (float)(3.14159274 * 2);nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));dst++;n = (int)(nvg__clampi((int)(ceilf((float)(((a0 - a1) / 3.14159274) * ncap))), (int)(2), (int)(ncap)));for (i = (int)(0); (i) < (n); i++) {
float u = (float)(i / (float)(n - 1));float a = (float)(a0 + u * (a1 - a0));float rx = (float)(p1->x + cosf((float)(a)) * rw);float ry = (float)(p1->y + sinf((float)(a)) * rw);nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));dst++;nvg__vset(dst, (float)(rx), (float)(ry), (float)(ru), (float)(1));dst++;}nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));dst++;}
 else {
float rx0 = 0;float ry0 = 0;float rx1 = 0;float ry1 = 0;float a0 = 0;float a1 = 0;nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1);a0 = (float)(atan2f((float)(dly0), (float)(dlx0)));a1 = (float)(atan2f((float)(dly1), (float)(dlx1)));if ((a1) < (a0)) a1 += (float)(3.14159274 * 2);nvg__vset(dst, (float)(p1->x + dlx0 * rw), (float)(p1->y + dly0 * rw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));dst++;n = (int)(nvg__clampi((int)(ceilf((float)(((a1 - a0) / 3.14159274) * ncap))), (int)(2), (int)(ncap)));for (i = (int)(0); (i) < (n); i++) {
float u = (float)(i / (float)(n - 1));float a = (float)(a0 + u * (a1 - a0));float lx = (float)(p1->x + cosf((float)(a)) * lw);float ly = (float)(p1->y + sinf((float)(a)) * lw);nvg__vset(dst, (float)(lx), (float)(ly), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));dst++;}nvg__vset(dst, (float)(p1->x + dlx1 * rw), (float)(p1->y + dly1 * rw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));dst++;}

			return dst;
		}

		public static NVGvertex* nvg__bevelJoin(NVGvertex* dst, NVGpoint* p0, NVGpoint* p1, float lw, float rw, float lu, float ru, float fringe)
		{
			float rx0 = 0;float ry0 = 0;float rx1 = 0;float ry1 = 0;
			float lx0 = 0;float ly0 = 0;float lx1 = 0;float ly1 = 0;
			float dlx0 = (float)(p0->dy);
			float dly0 = (float)(-p0->dx);
			float dlx1 = (float)(p1->dy);
			float dly1 = (float)(-p1->dx);
			for (; ; ) {
((1) != 0?(void)(0):((void)(fringe)));break;}
			if ((p1->flags & NVG_PT_LEFT) != 0) {
nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(lw), &lx0, &ly0, &lx1, &ly1);nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));dst++;if ((p1->flags & NVG_PT_BEVEL) != 0) {
nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));dst++;nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));dst++;}
 else {
rx0 = (float)(p1->x - p1->dmx * rw);ry0 = (float)(p1->y - p1->dmy * rw);nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx0 * rw), (float)(p1->y - dly0 * rw), (float)(ru), (float)(1));dst++;nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));dst++;nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));dst++;nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));dst++;}
nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - dlx1 * rw), (float)(p1->y - dly1 * rw), (float)(ru), (float)(1));dst++;}
 else {
nvg__chooseBevel((int)(p1->flags & NVG_PR_INNERBEVEL), p0, p1, (float)(-rw), &rx0, &ry0, &rx1, &ry1);nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));dst++;if ((p1->flags & NVG_PT_BEVEL) != 0) {
nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(rx0), (float)(ry0), (float)(ru), (float)(1));dst++;nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));dst++;}
 else {
lx0 = (float)(p1->x + p1->dmx * lw);ly0 = (float)(p1->y + p1->dmy * lw);nvg__vset(dst, (float)(p1->x + dlx0 * lw), (float)(p1->y + dly0 * lw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));dst++;nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x), (float)(p1->y), (float)(0.5f), (float)(1));dst++;}
nvg__vset(dst, (float)(p1->x + dlx1 * lw), (float)(p1->y + dly1 * lw), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(rx1), (float)(ry1), (float)(ru), (float)(1));dst++;}

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
			for (; ; ) {
((1) != 0?(void)(0):((void)(aa)));break;}
			for (i = (int)(0); (i) < (ncap); i++) {
float a = (float)(i / (float)(ncap - 1) * 3.14159274);float ax = (float)(cosf((float)(a)) * w);float ay = (float)(sinf((float)(a)) * w);nvg__vset(dst, (float)(px - dlx * ax - dx * ay), (float)(py - dly * ax - dy * ay), (float)(u0), (float)(1));dst++;nvg__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1));dst++;}
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
			for (; ; ) {
((1) != 0?(void)(0):((void)(aa)));break;}
			nvg__vset(dst, (float)(px + dlx * w), (float)(py + dly * w), (float)(u0), (float)(1));
			dst++;
			nvg__vset(dst, (float)(px - dlx * w), (float)(py - dly * w), (float)(u1), (float)(1));
			dst++;
			for (i = (int)(0); (i) < (ncap); i++) {
float a = (float)(i / (float)(ncap - 1) * 3.14159274);float ax = (float)(cosf((float)(a)) * w);float ay = (float)(sinf((float)(a)) * w);nvg__vset(dst, (float)(px), (float)(py), (float)(0.5f), (float)(1));dst++;nvg__vset(dst, (float)(px - dlx * ax + dx * ay), (float)(py - dly * ax + dy * ay), (float)(u0), (float)(1));dst++;}
			return dst;
		}

		public static void nvg__calculateJoins(NVGcontext* ctx, float w, int lineJoin, float miterLimit)
		{
			NVGpathCache* cache = ctx->cache;
			int i = 0;int j = 0;
			float iw = (float)(0.0f);
			if ((w) > (0.0f)) iw = (float)(1.0f / w);
			for (i = (int)(0); (i) < (cache->npaths); i++) {
NVGpath* path = &cache->paths[i];NVGpoint* pts = &cache->points[path->first];NVGpoint* p0 = &pts[path->count - 1];NVGpoint* p1 = &pts[0];int nleft = (int)(0);path->nbevel = (int)(0);for (j = (int)(0); (j) < (path->count); j++) {
float dlx0 = 0;float dly0 = 0;float dlx1 = 0;float dly1 = 0;float dmr2 = 0;float cross = 0;float limit = 0;dlx0 = (float)(p0->dy);dly0 = (float)(-p0->dx);dlx1 = (float)(p1->dy);dly1 = (float)(-p1->dx);p1->dmx = (float)((dlx0 + dlx1) * 0.5f);p1->dmy = (float)((dly0 + dly1) * 0.5f);dmr2 = (float)(p1->dmx * p1->dmx + p1->dmy * p1->dmy);if ((dmr2) > (0.000001f)) {
float scale = (float)(1.0f / dmr2);if ((scale) > (600.0f)) {
scale = (float)(600.0f);}
p1->dmx *= (float)(scale);p1->dmy *= (float)(scale);}
p1->flags = (byte)((p1->flags & NVG_PT_CORNER)?NVG_PT_CORNER:0);cross = (float)(p1->dx * p0->dy - p0->dx * p1->dy);if ((cross) > (0.0f)) {
nleft++;p1->flags |= (byte)(NVG_PT_LEFT);}
limit = (float)(nvg__maxf((float)(1.01f), (float)(nvg__minf((float)(p0->len), (float)(p1->len)) * iw)));if ((dmr2 * limit * limit) < (1.0f)) p1->flags |= (byte)(NVG_PR_INNERBEVEL);if ((p1->flags & NVG_PT_CORNER) != 0) {
if ((((dmr2 * miterLimit * miterLimit) < (1.0f)) || ((lineJoin) == (NVG_BEVEL))) || ((lineJoin) == (NVG_ROUND))) {
p1->flags |= (byte)(NVG_PT_BEVEL);}
}
if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0) path->nbevel++;p0 = p1++;}path->convex = (int)(((nleft) == (path->count))?1:0);}
		}

		public static int nvg__expandStroke(NVGcontext* ctx, float w, float fringe, int lineCap, int lineJoin, float miterLimit)
		{
			NVGpathCache* cache = ctx->cache;
			NVGvertex* verts;
			NVGvertex* dst;
			int cverts = 0;int i = 0;int j = 0;
			float aa = (float)(fringe);
			float u0 = (float)(0.0f);float u1 = (float)(1.0f);
			int ncap = (int)(nvg__curveDivs((float)(w), (float)(3.14159274), (float)(ctx->tessTol)));
			w += (float)(aa * 0.5f);
			if ((aa) == (0.0f)) {
u0 = (float)(0.5f);u1 = (float)(0.5f);}

			nvg__calculateJoins(ctx, (float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (cache->npaths); i++) {
NVGpath* path = &cache->paths[i];int loop = (int)(((path->closed) == (0))?0:1);if ((lineJoin) == (NVG_ROUND)) cverts += (int)((path->count + path->nbevel * (ncap + 2) + 1) * 2); else cverts += (int)((path->count + path->nbevel * 5 + 1) * 2);if ((loop) == (0)) {
if ((lineCap) == (NVG_ROUND)) {
cverts += (int)((ncap * 2 + 2) * 2);}
 else {
cverts += (int)((3 + 3) * 2);}
}
}
			verts = nvg__allocTempVerts(ctx, (int)(cverts));
			if ((verts) == (null)) return (int)(0);
			for (i = (int)(0); (i) < (cache->npaths); i++) {
NVGpath* path = &cache->paths[i];NVGpoint* pts = &cache->points[path->first];NVGpoint* p0;NVGpoint* p1;int s = 0;int e = 0;int loop = 0;float dx = 0;float dy = 0;path->fill = null;path->nfill = (int)(0);loop = (int)(((path->closed) == (0))?0:1);dst = verts;path->stroke = dst;if ((loop) != 0) {
p0 = &pts[path->count - 1];p1 = &pts[0];s = (int)(0);e = (int)(path->count);}
 else {
p0 = &pts[0];p1 = &pts[1];s = (int)(1);e = (int)(path->count - 1);}
if ((loop) == (0)) {
dx = (float)(p1->x - p0->x);dy = (float)(p1->y - p0->y);nvg__normalize(&dx, &dy);if ((lineCap) == (NVG_BUTT)) dst = nvg__buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1)); else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE))) dst = nvg__buttCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1)); else if ((lineCap) == (NVG_ROUND)) dst = nvg__roundCapStart(dst, p0, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1));}
for (j = (int)(s); (j) < (e); ++j) {
if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0) {
if ((lineJoin) == (NVG_ROUND)) {
dst = nvg__roundJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (int)(ncap), (float)(aa));}
 else {
dst = nvg__bevelJoin(dst, p0, p1, (float)(w), (float)(w), (float)(u0), (float)(u1), (float)(aa));}
}
 else {
nvg__vset(dst, (float)(p1->x + (p1->dmx * w)), (float)(p1->y + (p1->dmy * w)), (float)(u0), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - (p1->dmx * w)), (float)(p1->y - (p1->dmy * w)), (float)(u1), (float)(1));dst++;}
p0 = p1++;}if ((loop) != 0) {
nvg__vset(dst, (float)(verts[0].x), (float)(verts[0].y), (float)(u0), (float)(1));dst++;nvg__vset(dst, (float)(verts[1].x), (float)(verts[1].y), (float)(u1), (float)(1));dst++;}
 else {
dx = (float)(p1->x - p0->x);dy = (float)(p1->y - p0->y);nvg__normalize(&dx, &dy);if ((lineCap) == (NVG_BUTT)) dst = nvg__buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(-aa * 0.5f), (float)(aa), (float)(u0), (float)(u1)); else if (((lineCap) == (NVG_BUTT)) || ((lineCap) == (NVG_SQUARE))) dst = nvg__buttCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (float)(w - aa), (float)(aa), (float)(u0), (float)(u1)); else if ((lineCap) == (NVG_ROUND)) dst = nvg__roundCapEnd(dst, p1, (float)(dx), (float)(dy), (float)(w), (int)(ncap), (float)(aa), (float)(u0), (float)(u1));}
path->nstroke = ((int)(dst - verts));verts = dst;}
			return (int)(1);
		}

		public static int nvg__expandFill(NVGcontext* ctx, float w, int lineJoin, float miterLimit)
		{
			NVGpathCache* cache = ctx->cache;
			NVGvertex* verts;
			NVGvertex* dst;
			int cverts = 0;int convex = 0;int i = 0;int j = 0;
			float aa = (float)(ctx->fringeWidth);
			int fringe = (int)((w) > (0.0f)?1:0);
			nvg__calculateJoins(ctx, (float)(w), (int)(lineJoin), (float)(miterLimit));
			cverts = (int)(0);
			for (i = (int)(0); (i) < (cache->npaths); i++) {
NVGpath* path = &cache->paths[i];cverts += (int)(path->count + path->nbevel + 1);if ((fringe) != 0) cverts += (int)((path->count + path->nbevel * 5 + 1) * 2);}
			verts = nvg__allocTempVerts(ctx, (int)(cverts));
			if ((verts) == (null)) return (int)(0);
			convex = (int)(((cache->npaths) == (1)) && ((cache->paths[0].convex) != 0)?1:0);
			for (i = (int)(0); (i) < (cache->npaths); i++) {
NVGpath* path = &cache->paths[i];NVGpoint* pts = &cache->points[path->first];NVGpoint* p0;NVGpoint* p1;float rw = 0;float lw = 0;float woff = 0;float ru = 0;float lu = 0;woff = (float)(0.5f * aa);dst = verts;path->fill = dst;if ((fringe) != 0) {
p0 = &pts[path->count - 1];p1 = &pts[0];for (j = (int)(0); (j) < (path->count); ++j) {
if ((p1->flags & NVG_PT_BEVEL) != 0) {
float dlx0 = (float)(p0->dy);float dly0 = (float)(-p0->dx);float dlx1 = (float)(p1->dy);float dly1 = (float)(-p1->dx);if ((p1->flags & NVG_PT_LEFT) != 0) {
float lx = (float)(p1->x + p1->dmx * woff);float ly = (float)(p1->y + p1->dmy * woff);nvg__vset(dst, (float)(lx), (float)(ly), (float)(0.5f), (float)(1));dst++;}
 else {
float lx0 = (float)(p1->x + dlx0 * woff);float ly0 = (float)(p1->y + dly0 * woff);float lx1 = (float)(p1->x + dlx1 * woff);float ly1 = (float)(p1->y + dly1 * woff);nvg__vset(dst, (float)(lx0), (float)(ly0), (float)(0.5f), (float)(1));dst++;nvg__vset(dst, (float)(lx1), (float)(ly1), (float)(0.5f), (float)(1));dst++;}
}
 else {
nvg__vset(dst, (float)(p1->x + (p1->dmx * woff)), (float)(p1->y + (p1->dmy * woff)), (float)(0.5f), (float)(1));dst++;}
p0 = p1++;}}
 else {
for (j = (int)(0); (j) < (path->count); ++j) {
nvg__vset(dst, (float)(pts[j].x), (float)(pts[j].y), (float)(0.5f), (float)(1));dst++;}}
path->nfill = ((int)(dst - verts));verts = dst;if ((fringe) != 0) {
lw = (float)(w + woff);rw = (float)(w - woff);lu = (float)(0);ru = (float)(1);dst = verts;path->stroke = dst;if ((convex) != 0) {
lw = (float)(woff);lu = (float)(0.5f);}
p0 = &pts[path->count - 1];p1 = &pts[0];for (j = (int)(0); (j) < (path->count); ++j) {
if ((p1->flags & (NVG_PT_BEVEL | NVG_PR_INNERBEVEL)) != 0) {
dst = nvg__bevelJoin(dst, p0, p1, (float)(lw), (float)(rw), (float)(lu), (float)(ru), (float)(ctx->fringeWidth));}
 else {
nvg__vset(dst, (float)(p1->x + (p1->dmx * lw)), (float)(p1->y + (p1->dmy * lw)), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(p1->x - (p1->dmx * rw)), (float)(p1->y - (p1->dmy * rw)), (float)(ru), (float)(1));dst++;}
p0 = p1++;}nvg__vset(dst, (float)(verts[0].x), (float)(verts[0].y), (float)(lu), (float)(1));dst++;nvg__vset(dst, (float)(verts[1].x), (float)(verts[1].y), (float)(ru), (float)(1));dst++;path->nstroke = ((int)(dst - verts));verts = dst;}
 else {
path->stroke = (null);path->nstroke = (int)(0);}
}
			return (int)(1);
		}

		public static void nvgBeginPath(NVGcontext* ctx)
		{
			ctx->ncommands = (int)(0);
			nvg__clearPathCache(ctx);
		}

		public static void nvgMoveTo(NVGcontext* ctx, float x, float y)
		{
			float* vals = stackalloc float[3];
vals[0] = (float)(NVG_MOVETO);
vals[1] = (float)(x);
vals[2] = (float)(y);

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgLineTo(NVGcontext* ctx, float x, float y)
		{
			float* vals = stackalloc float[3];
vals[0] = (float)(NVG_LINETO);
vals[1] = (float)(x);
vals[2] = (float)(y);

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgBezierTo(NVGcontext* ctx, float c1x, float c1y, float c2x, float c2y, float x, float y)
		{
			float* vals = stackalloc float[7];
vals[0] = (float)(NVG_BEZIERTO);
vals[1] = (float)(c1x);
vals[2] = (float)(c1y);
vals[3] = (float)(c2x);
vals[4] = (float)(c2y);
vals[5] = (float)(x);
vals[6] = (float)(y);

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgQuadTo(NVGcontext* ctx, float cx, float cy, float x, float y)
		{
			float x0 = (float)(ctx->commandx);
			float y0 = (float)(ctx->commandy);
			float* vals = stackalloc float[7];
vals[0] = (float)(NVG_BEZIERTO);
vals[1] = (float)(x0 + 2.0f / 3.0f * (cx - x0));
vals[2] = (float)(y0 + 2.0f / 3.0f * (cy - y0));
vals[3] = (float)(x + 2.0f / 3.0f * (cx - x));
vals[4] = (float)(y + 2.0f / 3.0f * (cy - y));
vals[5] = (float)(x);
vals[6] = (float)(y);

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgArcTo(NVGcontext* ctx, float x1, float y1, float x2, float y2, float radius)
		{
			float x0 = (float)(ctx->commandx);
			float y0 = (float)(ctx->commandy);
			float dx0 = 0;float dy0 = 0;float dx1 = 0;float dy1 = 0;float a = 0;float d = 0;float cx = 0;float cy = 0;float a0 = 0;float a1 = 0;
			int dir = 0;
			if ((ctx->ncommands) == (0)) {
return;}

			if (((((nvg__ptEquals((float)(x0), (float)(y0), (float)(x1), (float)(y1), (float)(ctx->distTol))) != 0) || ((nvg__ptEquals((float)(x1), (float)(y1), (float)(x2), (float)(y2), (float)(ctx->distTol))) != 0)) || ((nvg__distPtSeg((float)(x1), (float)(y1), (float)(x0), (float)(y0), (float)(x2), (float)(y2))) < (ctx->distTol * ctx->distTol))) || ((radius) < (ctx->distTol))) {
nvgLineTo(ctx, (float)(x1), (float)(y1));return;}

			dx0 = (float)(x0 - x1);
			dy0 = (float)(y0 - y1);
			dx1 = (float)(x2 - x1);
			dy1 = (float)(y2 - y1);
			nvg__normalize(&dx0, &dy0);
			nvg__normalize(&dx1, &dy1);
			a = (float)(nvg__acosf((float)(dx0 * dx1 + dy0 * dy1)));
			d = (float)(radius / nvg__tanf((float)(a / 2.0f)));
			if ((d) > (10000.0f)) {
nvgLineTo(ctx, (float)(x1), (float)(y1));return;}

			if ((nvg__cross((float)(dx0), (float)(dy0), (float)(dx1), (float)(dy1))) > (0.0f)) {
cx = (float)(x1 + dx0 * d + dy0 * radius);cy = (float)(y1 + dy0 * d + -dx0 * radius);a0 = (float)(nvg__atan2f((float)(dx0), (float)(-dy0)));a1 = (float)(nvg__atan2f((float)(-dx1), (float)(dy1)));dir = (int)(NVG_CW);}
 else {
cx = (float)(x1 + dx0 * d + -dy0 * radius);cy = (float)(y1 + dy0 * d + dx0 * radius);a0 = (float)(nvg__atan2f((float)(-dx0), (float)(dy0)));a1 = (float)(nvg__atan2f((float)(dx1), (float)(-dy1)));dir = (int)(NVG_CCW);}

			nvgArc(ctx, (float)(cx), (float)(cy), (float)(radius), (float)(a0), (float)(a1), (int)(dir));
		}

		public static void nvgClosePath(NVGcontext* ctx)
		{
			float* vals = stackalloc float[1];
vals[0] = (float)(NVG_CLOSE);

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgPathWinding(NVGcontext* ctx, int dir)
		{
			float* vals = stackalloc float[2];
vals[0] = (float)(NVG_WINDING);
vals[1] = (float)(dir);

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgArc(NVGcontext* ctx, float cx, float cy, float r, float a0, float a1, int dir)
		{
			float a = (float)(0);float da = (float)(0);float hda = (float)(0);float kappa = (float)(0);
			float dx = (float)(0);float dy = (float)(0);float x = (float)(0);float y = (float)(0);float tanx = (float)(0);float tany = (float)(0);
			float px = (float)(0);float py = (float)(0);float ptanx = (float)(0);float ptany = (float)(0);
			float* vals = stackalloc float[3 + 5 * 7 + 100];
			int i = 0;int ndivs = 0;int nvals = 0;
			int move = (int)((ctx->ncommands) > (0)?NVG_LINETO:NVG_MOVETO);
			da = (float)(a1 - a0);
			if ((dir) == (NVG_CW)) {
if ((nvg__absf((float)(da))) >= (3.14159274 * 2)) {
da = (float)(3.14159274 * 2);}
 else {
while ((da) < (0.0f)) {da += (float)(3.14159274 * 2);}}
}
 else {
if ((nvg__absf((float)(da))) >= (3.14159274 * 2)) {
da = (float)(-3.14159274 * 2);}
 else {
while ((da) > (0.0f)) {da -= (float)(3.14159274 * 2);}}
}

			ndivs = (int)(nvg__maxi((int)(1), (int)(nvg__mini((int)(nvg__absf((float)(da)) / (3.14159274 * 0.5f) + 0.5f), (int)(5)))));
			hda = (float)((da / (float)(ndivs)) / 2.0f);
			kappa = (float)(nvg__absf((float)(4.0f / 3.0f * (1.0f - nvg__cosf((float)(hda))) / nvg__sinf((float)(hda)))));
			if ((dir) == (NVG_CCW)) kappa = (float)(-kappa);
			nvals = (int)(0);
			for (i = (int)(0); i <= ndivs; i++) {
a = (float)(a0 + da * (i / (float)(ndivs)));dx = (float)(nvg__cosf((float)(a)));dy = (float)(nvg__sinf((float)(a)));x = (float)(cx + dx * r);y = (float)(cy + dy * r);tanx = (float)(-dy * r * kappa);tany = (float)(dx * r * kappa);if ((i) == (0)) {
vals[nvals++] = ((float)(move));vals[nvals++] = (float)(x);vals[nvals++] = (float)(y);}
 else {
vals[nvals++] = (float)(NVG_BEZIERTO);vals[nvals++] = (float)(px + ptanx);vals[nvals++] = (float)(py + ptany);vals[nvals++] = (float)(x - tanx);vals[nvals++] = (float)(y - tany);vals[nvals++] = (float)(x);vals[nvals++] = (float)(y);}
px = (float)(x);py = (float)(y);ptanx = (float)(tanx);ptany = (float)(tany);}
			nvg__appendCommands(ctx, vals, (int)(nvals));
		}

		public static void nvgRect(NVGcontext* ctx, float x, float y, float w, float h)
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

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgRoundedRect(NVGcontext* ctx, float x, float y, float w, float h, float r)
		{
			nvgRoundedRectVarying(ctx, (float)(x), (float)(y), (float)(w), (float)(h), (float)(r), (float)(r), (float)(r), (float)(r));
		}

		public static void nvgRoundedRectVarying(NVGcontext* ctx, float x, float y, float w, float h, float radTopLeft, float radTopRight, float radBottomRight, float radBottomLeft)
		{
			if (((((radTopLeft) < (0.1f)) && ((radTopRight) < (0.1f))) && ((radBottomRight) < (0.1f))) && ((radBottomLeft) < (0.1f))) {
nvgRect(ctx, (float)(x), (float)(y), (float)(w), (float)(h));return;}
 else {
float halfw = (float)(nvg__absf((float)(w)) * 0.5f);float halfh = (float)(nvg__absf((float)(h)) * 0.5f);float rxBL = (float)(nvg__minf((float)(radBottomLeft), (float)(halfw)) * nvg__signf((float)(w)));float ryBL = (float)(nvg__minf((float)(radBottomLeft), (float)(halfh)) * nvg__signf((float)(h)));float rxBR = (float)(nvg__minf((float)(radBottomRight), (float)(halfw)) * nvg__signf((float)(w)));float ryBR = (float)(nvg__minf((float)(radBottomRight), (float)(halfh)) * nvg__signf((float)(h)));float rxTR = (float)(nvg__minf((float)(radTopRight), (float)(halfw)) * nvg__signf((float)(w)));float ryTR = (float)(nvg__minf((float)(radTopRight), (float)(halfh)) * nvg__signf((float)(h)));float rxTL = (float)(nvg__minf((float)(radTopLeft), (float)(halfw)) * nvg__signf((float)(w)));float ryTL = (float)(nvg__minf((float)(radTopLeft), (float)(halfh)) * nvg__signf((float)(h)));float* vals = stackalloc float[44];
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
nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));}

		}

		public static void nvgEllipse(NVGcontext* ctx, float cx, float cy, float rx, float ry)
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

			nvg__appendCommands(ctx, vals, (int)(sizeof((vals)) / sizeof((0[vals]))));
		}

		public static void nvgCircle(NVGcontext* ctx, float cx, float cy, float r)
		{
			nvgEllipse(ctx, (float)(cx), (float)(cy), (float)(r), (float)(r));
		}

		public static void nvgDebugDumpPathCache(NVGcontext* ctx)
		{
			NVGpath* path;
			int i = 0;int j = 0;
			printf("Dumping %d cached paths\n", (int)(ctx->cache->npaths));
			for (i = (int)(0); (i) < (ctx->cache->npaths); i++) {
path = &ctx->cache->paths[i];printf(" - Path %d\n", (int)(i));if ((path->nfill) != 0) {
printf("   - fill: %d\n", (int)(path->nfill));for (j = (int)(0); (j) < (path->nfill); j++) {printf("%f\t%f\n", (double)(path->fill[j].x), (double)(path->fill[j].y));}}
if ((path->nstroke) != 0) {
printf("   - stroke: %d\n", (int)(path->nstroke));for (j = (int)(0); (j) < (path->nstroke); j++) {printf("%f\t%f\n", (double)(path->stroke[j].x), (double)(path->stroke[j].y));}}
}
		}

		public static void nvgFill(NVGcontext* ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGpath* path;
			NVGpaint fillPaint = (NVGpaint)(state->fill);
			int i = 0;
			nvg__flattenPaths(ctx);
			if (((ctx->params.edgeAntiAlias) != 0) && ((state->shapeAntiAlias) != 0)) nvg__expandFill(ctx, (float)(ctx->fringeWidth), (int)(NVG_MITER), (float)(2.4f)); else nvg__expandFill(ctx, (float)(0.0f), (int)(NVG_MITER), (float)(2.4f));
			fillPaint.innerColor..a *= (float)(state->alpha);
			fillPaint.outerColor..a *= (float)(state->alpha);
			ctx->params.renderFill(ctx->params.userPtr, &fillPaint, (NVGcompositeOperationState)(state->compositeOperation), &state->scissor, (float)(ctx->fringeWidth), ctx->cache->bounds, ctx->cache->paths, (int)(ctx->cache->npaths));
			for (i = (int)(0); (i) < (ctx->cache->npaths); i++) {
path = &ctx->cache->paths[i];ctx->fillTriCount += (int)(path->nfill - 2);ctx->fillTriCount += (int)(path->nstroke - 2);ctx->drawCallCount += (int)(2);}
		}

		public static void nvgStroke(NVGcontext* ctx)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getAverageScale(state->xform));
			float strokeWidth = (float)(nvg__clampf((float)(state->strokeWidth * scale), (float)(0.0f), (float)(200.0f)));
			NVGpaint strokePaint = (NVGpaint)(state->stroke);
			NVGpath* path;
			int i = 0;
			if ((strokeWidth) < (ctx->fringeWidth)) {
float alpha = (float)(nvg__clampf((float)(strokeWidth / ctx->fringeWidth), (float)(0.0f), (float)(1.0f)));strokePaint.innerColor..a *= (float)(alpha * alpha);strokePaint.outerColor..a *= (float)(alpha * alpha);strokeWidth = (float)(ctx->fringeWidth);}

			strokePaint.innerColor..a *= (float)(state->alpha);
			strokePaint.outerColor..a *= (float)(state->alpha);
			nvg__flattenPaths(ctx);
			if (((ctx->params.edgeAntiAlias) != 0) && ((state->shapeAntiAlias) != 0)) nvg__expandStroke(ctx, (float)(strokeWidth * 0.5f), (float)(ctx->fringeWidth), (int)(state->lineCap), (int)(state->lineJoin), (float)(state->miterLimit)); else nvg__expandStroke(ctx, (float)(strokeWidth * 0.5f), (float)(0.0f), (int)(state->lineCap), (int)(state->lineJoin), (float)(state->miterLimit));
			ctx->params.renderStroke(ctx->params.userPtr, &strokePaint, (NVGcompositeOperationState)(state->compositeOperation), &state->scissor, (float)(ctx->fringeWidth), (float)(strokeWidth), ctx->cache->paths, (int)(ctx->cache->npaths));
			for (i = (int)(0); (i) < (ctx->cache->npaths); i++) {
path = &ctx->cache->paths[i];ctx->strokeTriCount += (int)(path->nstroke - 2);ctx->drawCallCount++;}
		}

		public static int nvgCreateFont(NVGcontext* ctx, sbyte* name, sbyte* path)
		{
			return (int)(fonsAddFont(ctx->fs, name, path));
		}

		public static int nvgCreateFontMem(NVGcontext* ctx, sbyte* name, byte* data, int ndata, int freeData)
		{
			return (int)(fonsAddFontMem(ctx->fs, name, data, (int)(ndata), (int)(freeData)));
		}

		public static int nvgFindFont(NVGcontext* ctx, sbyte* name)
		{
			if ((name) == (null)) return (int)(-1);
			return (int)(fonsGetFontByName(ctx->fs, name));
		}

		public static int nvgAddFallbackFontId(NVGcontext* ctx, int baseFont, int fallbackFont)
		{
			if (((baseFont) == (-1)) || ((fallbackFont) == (-1))) return (int)(0);
			return (int)(fonsAddFallbackFont(ctx->fs, (int)(baseFont), (int)(fallbackFont)));
		}

		public static int nvgAddFallbackFont(NVGcontext* ctx, sbyte* baseFont, sbyte* fallbackFont)
		{
			return (int)(nvgAddFallbackFontId(ctx, (int)(nvgFindFont(ctx, baseFont)), (int)(nvgFindFont(ctx, fallbackFont))));
		}

		public static void nvgFontSize(NVGcontext* ctx, float size)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontSize = (float)(size);
		}

		public static void nvgFontBlur(NVGcontext* ctx, float blur)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontBlur = (float)(blur);
		}

		public static void nvgTextLetterSpacing(NVGcontext* ctx, float spacing)
		{
			NVGstate* state = nvg__getState(ctx);
			state->letterSpacing = (float)(spacing);
		}

		public static void nvgTextLineHeight(NVGcontext* ctx, float lineHeight)
		{
			NVGstate* state = nvg__getState(ctx);
			state->lineHeight = (float)(lineHeight);
		}

		public static void nvgTextAlign(NVGcontext* ctx, int align)
		{
			NVGstate* state = nvg__getState(ctx);
			state->textAlign = (int)(align);
		}

		public static void nvgFontFaceId(NVGcontext* ctx, int font)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontId = (int)(font);
		}

		public static void nvgFontFace(NVGcontext* ctx, sbyte* font)
		{
			NVGstate* state = nvg__getState(ctx);
			state->fontId = (int)(fonsGetFontByName(ctx->fs, font));
		}

		public static float nvg__quantize(float a, float d)
		{
			return (float)(((int)(a / d + 0.5f)) * d);
		}

		public static float nvg__getFontScale(NVGstate* state)
		{
			return (float)(nvg__minf((float)(nvg__quantize((float)(nvg__getAverageScale(state->xform)), (float)(0.01f))), (float)(4.0f)));
		}

		public static void nvg__flushTextTexture(NVGcontext* ctx)
		{
			int* dirty = stackalloc int[4];
			if ((fonsValidateTexture(ctx->fs, dirty)) != 0) {
int fontImage = (int)(ctx->fontImages[ctx->fontImageIdx]);if (fontImage != 0) {
int iw = 0;int ih = 0;byte* data = fonsGetTextureData(ctx->fs, &iw, &ih);int x = (int)(dirty[0]);int y = (int)(dirty[1]);int w = (int)(dirty[2] - dirty[0]);int h = (int)(dirty[3] - dirty[1]);ctx->params.renderUpdateTexture(ctx->params.userPtr, (int)(fontImage), (int)(x), (int)(y), (int)(w), (int)(h), data);}
}

		}

		public static int nvg__allocTextAtlas(NVGcontext* ctx)
		{
			int iw = 0;int ih = 0;
			nvg__flushTextTexture(ctx);
			if ((ctx->fontImageIdx) >= (4 - 1)) return (int)(0);
			if (ctx->fontImages[ctx->fontImageIdx + 1] != 0) nvgImageSize(ctx, (int)(ctx->fontImages[ctx->fontImageIdx + 1]), &iw, &ih); else {
nvgImageSize(ctx, (int)(ctx->fontImages[ctx->fontImageIdx]), &iw, &ih);if ((iw) > (ih)) ih *= (int)(2); else iw *= (int)(2);if (((iw) > (2048)) || ((ih) > (2048))) iw = (int)(ih = (int)(2048));ctx->fontImages[ctx->fontImageIdx + 1] = (int)(ctx->params.renderCreateTexture(ctx->params.userPtr, (int)(NVG_TEXTURE_ALPHA), (int)(iw), (int)(ih), (int)(0), (null)));}

			++ctx->fontImageIdx;
			fonsResetAtlas(ctx->fs, (int)(iw), (int)(ih));
			return (int)(1);
		}

		public static void nvg__renderText(NVGcontext* ctx, NVGvertex* verts, int nverts)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGpaint paint = (NVGpaint)(state->fill);
			paint.image = (int)(ctx->fontImages[ctx->fontImageIdx]);
			paint.innerColor..a *= (float)(state->alpha);
			paint.outerColor..a *= (float)(state->alpha);
			ctx->params.renderTriangles(ctx->params.userPtr, &paint, (NVGcompositeOperationState)(state->compositeOperation), &state->scissor, verts, (int)(nverts));
			ctx->drawCallCount++;
			ctx->textTriCount += (int)(nverts / 3);
		}

		public static float nvgText(NVGcontext* ctx, float x, float y, sbyte* _string_, sbyte* end)
		{
			NVGstate* state = nvg__getState(ctx);
			FONStextIter iter =  new FONStextIter();FONStextIter prevIter =  new FONStextIter();
			FONSquad q =  new FONSquad();
			NVGvertex* verts;
			float scale = (float)(nvg__getFontScale(state) * ctx->devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int cverts = (int)(0);
			int nverts = (int)(0);
			if ((end) == (null)) end = _string_ + CRuntime.strlen(_string_);
			if ((state->fontId) == (-1)) return (float)(x);
			fonsSetSize(ctx->fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx->fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx->fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx->fs, (int)(state->textAlign));
			fonsSetFont(ctx->fs, (int)(state->fontId));
			cverts = (int)(nvg__maxi((int)(2), (int)(end - _string_)) * 6);
			verts = nvg__allocTempVerts(ctx, (int)(cverts));
			if ((verts) == (null)) return (float)(x);
			fonsTextIterInit(ctx->fs, &iter, (float)(x * scale), (float)(y * scale), _string_, end, (int)(FONS_GLYPH_BITMAP_REQUIRED));
			prevIter = (FONStextIter)(iter);
			while ((fonsTextIterNext(ctx->fs, &iter, &q)) != 0) {
float* c = stackalloc float[4 * 2];if ((iter.prevGlyphIndex) == (-1)) {
if (nverts != 0) {
nvg__renderText(ctx, verts, (int)(nverts));nverts = (int)(0);}
if (nvg__allocTextAtlas(ctx)== 0) break;iter = (FONStextIter)(prevIter);fonsTextIterNext(ctx->fs, &iter, &q);if ((iter.prevGlyphIndex) == (-1)) break;}
prevIter = (FONStextIter)(iter);nvgTransformPoint(&c[0], &c[1], state->xform, (float)(q.x0 * invscale), (float)(q.y0 * invscale));nvgTransformPoint(&c[2], &c[3], state->xform, (float)(q.x1 * invscale), (float)(q.y0 * invscale));nvgTransformPoint(&c[4], &c[5], state->xform, (float)(q.x1 * invscale), (float)(q.y1 * invscale));nvgTransformPoint(&c[6], &c[7], state->xform, (float)(q.x0 * invscale), (float)(q.y1 * invscale));if (nverts + 6 <= cverts) {
nvg__vset(&verts[nverts], (float)(c[0]), (float)(c[1]), (float)(q.s0), (float)(q.t0));nverts++;nvg__vset(&verts[nverts], (float)(c[4]), (float)(c[5]), (float)(q.s1), (float)(q.t1));nverts++;nvg__vset(&verts[nverts], (float)(c[2]), (float)(c[3]), (float)(q.s1), (float)(q.t0));nverts++;nvg__vset(&verts[nverts], (float)(c[0]), (float)(c[1]), (float)(q.s0), (float)(q.t0));nverts++;nvg__vset(&verts[nverts], (float)(c[6]), (float)(c[7]), (float)(q.s0), (float)(q.t1));nverts++;nvg__vset(&verts[nverts], (float)(c[4]), (float)(c[5]), (float)(q.s1), (float)(q.t1));nverts++;}
}
			nvg__flushTextTexture(ctx);
			nvg__renderText(ctx, verts, (int)(nverts));
			return (float)(iter.nextx / scale);
		}

		public static void nvgTextBox(NVGcontext* ctx, float x, float y, float breakRowWidth, sbyte* _string_, sbyte* end)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGtextRow* rows = stackalloc NVGtextRow[2];
			int nrows = (int)(0);int i = 0;
			int oldAlign = (int)(state->textAlign);
			int haling = (int)(state->textAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state->textAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);
			if ((state->fontId) == (-1)) return;
			nvgTextMetrics(ctx, (null), (null), &lineh);
			state->textAlign = (int)(NVG_ALIGN_LEFT | valign);
			while ((nrows = (int)(nvgTextBreakLines(ctx, _string_, end, (float)(breakRowWidth), rows, (int)(2))))) {
for (i = (int)(0); (i) < (nrows); i++) {
NVGtextRow* row = &rows[i];if ((haling & NVG_ALIGN_LEFT) != 0) nvgText(ctx, (float)(x), (float)(y), row->start, row->end); else if ((haling & NVG_ALIGN_CENTER) != 0) nvgText(ctx, (float)(x + breakRowWidth * 0.5f - row->width * 0.5f), (float)(y), row->start, row->end); else if ((haling & NVG_ALIGN_RIGHT) != 0) nvgText(ctx, (float)(x + breakRowWidth - row->width), (float)(y), row->start, row->end);y += (float)(lineh * state->lineHeight);}_string_ = rows[nrows - 1].next;}
			state->textAlign = (int)(oldAlign);
		}

		public static int nvgTextGlyphPositions(NVGcontext* ctx, float x, float y, sbyte* _string_, sbyte* end, NVGglyphPosition* positions, int maxPositions)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx->devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FONStextIter iter =  new FONStextIter();FONStextIter prevIter =  new FONStextIter();
			FONSquad q =  new FONSquad();
			int npos = (int)(0);
			if ((state->fontId) == (-1)) return (int)(0);
			if ((end) == (null)) end = _string_ + CRuntime.strlen(_string_);
			if ((_string_) == (end)) return (int)(0);
			fonsSetSize(ctx->fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx->fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx->fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx->fs, (int)(state->textAlign));
			fonsSetFont(ctx->fs, (int)(state->fontId));
			fonsTextIterInit(ctx->fs, &iter, (float)(x * scale), (float)(y * scale), _string_, end, (int)(FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FONStextIter)(iter);
			while ((fonsTextIterNext(ctx->fs, &iter, &q)) != 0) {
if (((iter.prevGlyphIndex) < (0)) && ((nvg__allocTextAtlas(ctx)) != 0)) {
iter = (FONStextIter)(prevIter);fonsTextIterNext(ctx->fs, &iter, &q);}
prevIter = (FONStextIter)(iter);positions[npos].str = iter.str;positions[npos].x = (float)(iter.x * invscale);positions[npos].minx = (float)(nvg__minf((float)(iter.x), (float)(q.x0)) * invscale);positions[npos].maxx = (float)(nvg__maxf((float)(iter.nextx), (float)(q.x1)) * invscale);npos++;if ((npos) >= (maxPositions)) break;}
			return (int)(npos);
		}

		public static int nvgTextBreakLines(NVGcontext* ctx, sbyte* _string_, sbyte* end, float breakRowWidth, NVGtextRow* rows, int maxRows)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx->devicePxRatio);
			float invscale = (float)(1.0f / scale);
			FONStextIter iter =  new FONStextIter();FONStextIter prevIter =  new FONStextIter();
			FONSquad q =  new FONSquad();
			int nrows = (int)(0);
			float rowStartX = (float)(0);
			float rowWidth = (float)(0);
			float rowMinX = (float)(0);
			float rowMaxX = (float)(0);
			sbyte* rowStart = (null);
			sbyte* rowEnd = (null);
			sbyte* wordStart = (null);
			float wordStartX = (float)(0);
			float wordMinX = (float)(0);
			sbyte* breakEnd = (null);
			float breakWidth = (float)(0);
			float breakMaxX = (float)(0);
			int type = (int)(NVG_SPACE);int ptype = (int)(NVG_SPACE);
			uint pcodepoint = (uint)(0);
			if ((maxRows) == (0)) return (int)(0);
			if ((state->fontId) == (-1)) return (int)(0);
			if ((end) == (null)) end = _string_ + CRuntime.strlen(_string_);
			if ((_string_) == (end)) return (int)(0);
			fonsSetSize(ctx->fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx->fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx->fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx->fs, (int)(state->textAlign));
			fonsSetFont(ctx->fs, (int)(state->fontId));
			breakRowWidth *= (float)(scale);
			fonsTextIterInit(ctx->fs, &iter, (float)(0), (float)(0), _string_, end, (int)(FONS_GLYPH_BITMAP_OPTIONAL));
			prevIter = (FONStextIter)(iter);
			while ((fonsTextIterNext(ctx->fs, &iter, &q)) != 0) {
if (((iter.prevGlyphIndex) < (0)) && ((nvg__allocTextAtlas(ctx)) != 0)) {
iter = (FONStextIter)(prevIter);fonsTextIterNext(ctx->fs, &iter, &q);}
prevIter = (FONStextIter)(iter);switch (iter.codepoint){
case 9:case 11:case 12:case 32:case 0x00a0:type = (int)(NVG_SPACE);break;case 10:type = (int)((pcodepoint) == (13)?NVG_SPACE:NVG_NEWLINE);break;case 13:type = (int)((pcodepoint) == (10)?NVG_SPACE:NVG_NEWLINE);break;case 0x0085:type = (int)(NVG_NEWLINE);break;default: if ((((((((iter.codepoint) >= (0x4E00)) && (iter.codepoint <= 0x9FFF)) || (((iter.codepoint) >= (0x3000)) && (iter.codepoint <= 0x30FF))) || (((iter.codepoint) >= (0xFF00)) && (iter.codepoint <= 0xFFEF))) || (((iter.codepoint) >= (0x1100)) && (iter.codepoint <= 0x11FF))) || (((iter.codepoint) >= (0x3130)) && (iter.codepoint <= 0x318F))) || (((iter.codepoint) >= (0xAC00)) && (iter.codepoint <= 0xD7AF))) type = (int)(NVG_CJK_CHAR); else type = (int)(NVG_CHAR);break;}
if ((type) == (NVG_NEWLINE)) {
rows[nrows].start = rowStart != (null)?rowStart:iter.str;rows[nrows].end = rowEnd != (null)?rowEnd:iter.str;rows[nrows].width = (float)(rowWidth * invscale);rows[nrows].minx = (float)(rowMinX * invscale);rows[nrows].maxx = (float)(rowMaxX * invscale);rows[nrows].next = iter.next;nrows++;if ((nrows) >= (maxRows)) return (int)(nrows);breakEnd = rowStart;breakWidth = (float)(0.0);breakMaxX = (float)(0.0);rowStart = (null);rowEnd = (null);rowWidth = (float)(0);rowMinX = (float)(rowMaxX = (float)(0));}
 else {
if ((rowStart) == (null)) {
if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) {
rowStartX = (float)(iter.x);rowStart = iter.str;rowEnd = iter.next;rowWidth = (float)(iter.nextx - rowStartX);rowMinX = (float)(q.x0 - rowStartX);rowMaxX = (float)(q.x1 - rowStartX);wordStart = iter.str;wordStartX = (float)(iter.x);wordMinX = (float)(q.x0 - rowStartX);breakEnd = rowStart;breakWidth = (float)(0.0);breakMaxX = (float)(0.0);}
}
 else {
float nextWidth = (float)(iter.nextx - rowStartX);if (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) {
rowEnd = iter.next;rowWidth = (float)(iter.nextx - rowStartX);rowMaxX = (float)(q.x1 - rowStartX);}
if (((((ptype) == (NVG_CHAR)) || ((ptype) == (NVG_CJK_CHAR))) && ((type) == (NVG_SPACE))) || ((type) == (NVG_CJK_CHAR))) {
breakEnd = iter.str;breakWidth = (float)(rowWidth);breakMaxX = (float)(rowMaxX);}
if ((((ptype) == (NVG_SPACE)) && (((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR)))) || ((type) == (NVG_CJK_CHAR))) {
wordStart = iter.str;wordStartX = (float)(iter.x);wordMinX = (float)(q.x0 - rowStartX);}
if ((((type) == (NVG_CHAR)) || ((type) == (NVG_CJK_CHAR))) && ((nextWidth) > (breakRowWidth))) {
if ((breakEnd) == (rowStart)) {
rows[nrows].start = rowStart;rows[nrows].end = iter.str;rows[nrows].width = (float)(rowWidth * invscale);rows[nrows].minx = (float)(rowMinX * invscale);rows[nrows].maxx = (float)(rowMaxX * invscale);rows[nrows].next = iter.str;nrows++;if ((nrows) >= (maxRows)) return (int)(nrows);rowStartX = (float)(iter.x);rowStart = iter.str;rowEnd = iter.next;rowWidth = (float)(iter.nextx - rowStartX);rowMinX = (float)(q.x0 - rowStartX);rowMaxX = (float)(q.x1 - rowStartX);wordStart = iter.str;wordStartX = (float)(iter.x);wordMinX = (float)(q.x0 - rowStartX);}
 else {
rows[nrows].start = rowStart;rows[nrows].end = breakEnd;rows[nrows].width = (float)(breakWidth * invscale);rows[nrows].minx = (float)(rowMinX * invscale);rows[nrows].maxx = (float)(breakMaxX * invscale);rows[nrows].next = wordStart;nrows++;if ((nrows) >= (maxRows)) return (int)(nrows);rowStartX = (float)(wordStartX);rowStart = wordStart;rowEnd = iter.next;rowWidth = (float)(iter.nextx - rowStartX);rowMinX = (float)(wordMinX);rowMaxX = (float)(q.x1 - rowStartX);}
breakEnd = rowStart;breakWidth = (float)(0.0);breakMaxX = (float)(0.0);}
}
}
pcodepoint = (uint)(iter.codepoint);ptype = (int)(type);}
			if (rowStart != (null)) {
rows[nrows].start = rowStart;rows[nrows].end = rowEnd;rows[nrows].width = (float)(rowWidth * invscale);rows[nrows].minx = (float)(rowMinX * invscale);rows[nrows].maxx = (float)(rowMaxX * invscale);rows[nrows].next = end;nrows++;}

			return (int)(nrows);
		}

		public static float nvgTextBounds(NVGcontext* ctx, float x, float y, sbyte* _string_, sbyte* end, float* bounds)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx->devicePxRatio);
			float invscale = (float)(1.0f / scale);
			float width = 0;
			if ((state->fontId) == (-1)) return (float)(0);
			fonsSetSize(ctx->fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx->fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx->fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx->fs, (int)(state->textAlign));
			fonsSetFont(ctx->fs, (int)(state->fontId));
			width = (float)(fonsTextBounds(ctx->fs, (float)(x * scale), (float)(y * scale), _string_, end, bounds));
			if (bounds != (null)) {
fonsLineBounds(ctx->fs, (float)(y * scale), &bounds[1], &bounds[3]);bounds[0] *= (float)(invscale);bounds[1] *= (float)(invscale);bounds[2] *= (float)(invscale);bounds[3] *= (float)(invscale);}

			return (float)(width * invscale);
		}

		public static void nvgTextBoxBounds(NVGcontext* ctx, float x, float y, float breakRowWidth, sbyte* _string_, sbyte* end, float* bounds)
		{
			NVGstate* state = nvg__getState(ctx);
			NVGtextRow* rows = stackalloc NVGtextRow[2];
			float scale = (float)(nvg__getFontScale(state) * ctx->devicePxRatio);
			float invscale = (float)(1.0f / scale);
			int nrows = (int)(0);int i = 0;
			int oldAlign = (int)(state->textAlign);
			int haling = (int)(state->textAlign & (NVG_ALIGN_LEFT | NVG_ALIGN_CENTER | NVG_ALIGN_RIGHT));
			int valign = (int)(state->textAlign & (NVG_ALIGN_TOP | NVG_ALIGN_MIDDLE | NVG_ALIGN_BOTTOM | NVG_ALIGN_BASELINE));
			float lineh = (float)(0);float rminy = (float)(0);float rmaxy = (float)(0);
			float minx = 0;float miny = 0;float maxx = 0;float maxy = 0;
			if ((state->fontId) == (-1)) {
if (bounds != (null)) bounds[0] = (float)(bounds[1] = (float)(bounds[2] = (float)(bounds[3] = (float)(0.0f))));return;}

			nvgTextMetrics(ctx, (null), (null), &lineh);
			state->textAlign = (int)(NVG_ALIGN_LEFT | valign);
			minx = (float)(maxx = (float)(x));
			miny = (float)(maxy = (float)(y));
			fonsSetSize(ctx->fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx->fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx->fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx->fs, (int)(state->textAlign));
			fonsSetFont(ctx->fs, (int)(state->fontId));
			fonsLineBounds(ctx->fs, (float)(0), &rminy, &rmaxy);
			rminy *= (float)(invscale);
			rmaxy *= (float)(invscale);
			while ((nrows = (int)(nvgTextBreakLines(ctx, _string_, end, (float)(breakRowWidth), rows, (int)(2))))) {
for (i = (int)(0); (i) < (nrows); i++) {
NVGtextRow* row = &rows[i];float rminx = 0;float rmaxx = 0;float dx = (float)(0);if ((haling & NVG_ALIGN_LEFT) != 0) dx = (float)(0); else if ((haling & NVG_ALIGN_CENTER) != 0) dx = (float)(breakRowWidth * 0.5f - row->width * 0.5f); else if ((haling & NVG_ALIGN_RIGHT) != 0) dx = (float)(breakRowWidth - row->width);rminx = (float)(x + row->minx + dx);rmaxx = (float)(x + row->maxx + dx);minx = (float)(nvg__minf((float)(minx), (float)(rminx)));maxx = (float)(nvg__maxf((float)(maxx), (float)(rmaxx)));miny = (float)(nvg__minf((float)(miny), (float)(y + rminy)));maxy = (float)(nvg__maxf((float)(maxy), (float)(y + rmaxy)));y += (float)(lineh * state->lineHeight);}_string_ = rows[nrows - 1].next;}
			state->textAlign = (int)(oldAlign);
			if (bounds != (null)) {
bounds[0] = (float)(minx);bounds[1] = (float)(miny);bounds[2] = (float)(maxx);bounds[3] = (float)(maxy);}

		}

		public static void nvgTextMetrics(NVGcontext* ctx, float* ascender, float* descender, float* lineh)
		{
			NVGstate* state = nvg__getState(ctx);
			float scale = (float)(nvg__getFontScale(state) * ctx->devicePxRatio);
			float invscale = (float)(1.0f / scale);
			if ((state->fontId) == (-1)) return;
			fonsSetSize(ctx->fs, (float)(state->fontSize * scale));
			fonsSetSpacing(ctx->fs, (float)(state->letterSpacing * scale));
			fonsSetBlur(ctx->fs, (float)(state->fontBlur * scale));
			fonsSetAlign(ctx->fs, (int)(state->textAlign));
			fonsSetFont(ctx->fs, (int)(state->fontId));
			fonsVertMetrics(ctx->fs, ascender, descender, lineh);
			if (ascender != (null)) *ascender *= (float)(invscale);
			if (descender != (null)) *descender *= (float)(invscale);
			if (lineh != (null)) *lineh *= (float)(invscale);
		}

	}
}
