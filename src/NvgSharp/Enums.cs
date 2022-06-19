namespace NvgSharp
{
	public enum Winding
	{
		CounterClockWise = 1,            // Winding for solid shapes
		ClockWise = 2,             // Winding for holes
	};

	public enum Solidity
	{
		Solid = 1,          // CCW
		Hole = 2,           // CW
	};

	public enum LineCap
	{
		Butt,
		Round,
		Square,
		Bevel,
		Miter,
	};

	public enum TextHorizontalAlignment
	{
		/// <summary>
		/// Default, align text horizontally to left
		/// </summary>
		Left,

		/// <summary>
		/// Align text horizontally to center
		/// </summary>
		Center,

		/// <summary>
		/// Align text horizontally to right
		/// </summary>
		Right
	}

	public enum TextVerticalAlignment
	{
		/// <summary>
		/// Default, Align text vertically to top
		/// </summary>
		Top,

		/// <summary>
		/// Align text vertically to middle
		/// </summary>
		Center,

		/// <summary>
		/// Align text vertically to bottom
		/// </summary>
		Bottom
	}

	public enum ImageFlags
	{
		GenerateMipMaps = 1 << 0,     // Generate mipmaps during creation of the image.
		RepeatX = 1 << 1,     // Repeat image in X direction.
		RepeatY = 1 << 2,     // Repeat image in Y direction.
		FlipY = 1 << 3,       // Flips (inverses) image in Y direction when rendered.
		Premultiplied = 1 << 4,       // Image data has premultiplied alpha.
		Nearest = 1 << 5,     // Image interpolation is Nearest instead Linear
	};

	internal enum CommandType
	{
		MoveTo = 0,
		LineTo = 1,
		BezierTo = 2,
		Close = 3,
		Winding = 4,
	};

	internal enum PointFlags
	{
		Corner = 0x01,
		Left = 0x02,
		Bevel = 0x04,
		InnerBevel = 0x08,
	};

	internal enum CodepointType
	{
		Space,
		Newline,
		Char,
		CjkChar,
	};
}