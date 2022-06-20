namespace NvgSharp
{
	public enum Winding
	{
		/// <summary>
		/// Winding for solid shapes
		/// </summary>
		CounterClockWise = 1,

		/// <summary>
		/// Winding for holes
		/// </summary>
		ClockWise = 2,
	};

	public enum Solidity
	{
		/// <summary>
		/// CCW
		/// </summary>
		Solid = 1,

		/// <summary>
		/// CW
		/// </summary>
		Hole = 2,
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
		/// <summary>
		/// Generate mipmaps during creation of the image
		/// </summary>
		GenerateMipMaps = 1 << 0,

		/// <summary>
		/// Repeat image in X direction
		/// </summary>
		RepeatX = 1 << 1,

		/// <summary>
		/// Repeat image in Y direction
		/// </summary>
		RepeatY = 1 << 2,

		/// <summary>
		/// Flips (inverses) image in Y direction when rendered
		/// </summary>
		FlipY = 1 << 3,

		/// <summary>
		/// Image data has premultiplied alpha
		/// </summary>
		Premultiplied = 1 << 4,

		/// <summary>
		/// Image interpolation is Nearest instead Linear
		/// </summary>
		Nearest = 1 << 5,
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