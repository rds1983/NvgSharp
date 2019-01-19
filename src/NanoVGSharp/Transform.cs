namespace NanoVGSharp
{
	public struct Transform
	{
		public float t1, t2, t3, t4, t5, t6;

		public void Zero()
		{
			t1 = t2 = t3 = t4 = t5 = t6;
		}
	}
}
