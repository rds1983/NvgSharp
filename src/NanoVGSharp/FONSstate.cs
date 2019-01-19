namespace NanoVGSharp
{
	public class FONSstate
	{
		public int font;
		public int align;
		public float size;
		public uint color;
		public float blur;
		public float spacing;

		public FONSstate Clone()
		{
			return new FONSstate
			{
				font = font,
				align = align,
				size = size,
				color = color,
				blur = blur,
				spacing = spacing
			};
		}
	}
}