namespace NanoVGSharp
{
	public struct StringLocation
	{
		public string String;
		public int Location;

		public int Remaining
		{
			get
			{
				if (String == null)
				{
					return 0;
				}

				if (Location >= String.Length)
				{
					return 0;
				}

				return String.Length - Location;
			}
		}

		public char this[int index]
		{
			get
			{
				return String[Location + index];
			}
		}

		public bool IsNullOrEmpty
		{
			get
			{
				if (String == null)
				{
					return true;
				}

				return Location >= String.Length;
			}
		}

		public static implicit operator StringLocation(string value)
		{
			return new StringLocation
			{
				String = value,
				Location = 0
			};
		}

		public static StringLocation operator ++(StringLocation a)
		{
			return new StringLocation
			{
				String = a.String,
				Location = a.Location
			};
		}

		public static bool operator ==(StringLocation a, StringLocation b)
		{
			return object.ReferenceEquals(a.String, b.String) &&
				a.Location == b.Location;
		}

		public static bool operator !=(StringLocation a, StringLocation b)
		{
			return !(a == b);
		}

		public void Reset()
		{
			String = null;
			Location = 0;
		}
	}
}
