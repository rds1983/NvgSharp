using System.Collections.Generic;

namespace NvgSharp
{
	internal class Path
	{
		public bool Closed;
		public int BevelCount;
		public int FillOffset, FillCount;
		public int StrokeOffset, StrokeCount;
		public Winding Winding;
		public bool Convex;
		public readonly List<NvgPoint> Points = new List<NvgPoint>();

		public NvgPoint this[int index]
		{
			get => Points[index];
			set => Points[index] = value;
		}

		public int Count => Points.Count;

		public NvgPoint FirstPoint => Points[0];
		public NvgPoint LastPoint => Points[Points.Count - 1];
	}
}
