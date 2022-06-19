namespace NvgSharp
{
	internal struct Command
	{
		public CommandType Type;
		public float P1, P2, P3, P4, P5, P6;

		public Command(CommandType type)
		{
			Type = type;
			P1 = P2 = P3 = P4 = P5 = P6 = 0;
		}

		public Command(Solidity solidity)
		{
			Type = CommandType.Winding;
			P1 = (int)solidity;
			P2 = P3 = P4 = P5 = P6 = 0;
		}

		public Command(CommandType type, float p1, float p2)
		{
			Type = type;
			P1 = p1;
			P2 = p2;
			P3 = P4 = P5 = P6 = 0;
		}

		public Command(float p1, float p2, float p3, float p4, float p5, float p6)
		{
			Type = CommandType.BezierTo;
			P1 = p1;
			P2 = p2;
			P3 = p3;
			P4 = p4;
			P5 = p5;
			P6 = p6;
		}
	}
}
