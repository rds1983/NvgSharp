using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
{
	internal struct PathInfo
	{
		public int FillOffset;
		public int FillCount;
		public int StrokeOffset;
		public int StrokeCount;
	}

	internal enum CallType
	{
		None,
		Fill,
		ConvexFill,
		Stroke,
		Triangles
	}

	internal enum RenderType
	{
		FillGradient,
		FillImage,
		Simple,
		Image
	};

	internal struct ColorInfo
	{
		public float R, G, B, A;

		public Vector4 ToVector4() => new Vector4(R, G, B, A);
	}

	internal struct UniformInfo
	{
		public Matrix scissorMat;
		public Matrix paintMat;
		public ColorInfo innerCol;
		public ColorInfo outerCol;
		public Vector2 scissorExt;
		public Vector2 scissorScale;
		public Vector2 extent;
		public float radius;
		public float feather;
		public float strokeMult;
		public float strokeThr;
	}

	internal struct CallInfo
	{
		public CallType Type;
		public Texture2D Image;
		public int PathOffset;
		public int PathCount;
		public int TriangleOffset;
		public int TriangleCount;
		public BlendState BlendInfo;
		public UniformInfo UniformInfo, UniformInfo2;
	}
}