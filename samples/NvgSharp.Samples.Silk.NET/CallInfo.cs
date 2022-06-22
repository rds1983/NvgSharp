using Silk.NET.OpenGL;
using System.Numerics;

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

	internal struct BlendInfo
	{
		public static BlendInfo Default = new()
		{
			srcRgb = BlendingFactor.One,
			destRgb = BlendingFactor.OneMinusSrcAlpha,
			srcAlpha = BlendingFactor.One,
			destAlpha = BlendingFactor.OneMinusSrcAlpha,
		};

		public BlendingFactor srcRgb;
		public BlendingFactor destRgb;
		public BlendingFactor srcAlpha;
		public BlendingFactor destAlpha;
	}

	internal struct ColorInfo
	{
		public float R, G, B, A;

		public Vector4 ToVector4() => new Vector4(R, G, B, A);
	}

	internal struct UniformInfo
	{
		public Matrix4x4 scissorMat;
		public Matrix4x4 paintMat;
		public ColorInfo innerCol;
		public ColorInfo outerCol;
		public Vector2 scissorExt;
		public Vector2 scissorScale;
		public Vector2 extent;
		public float radius;
		public float feather;
		public float strokeMult;
		public float strokeThr;
		public int texType;
		public int type;
	}

	internal struct CallInfo
	{
		public CallType Type;
		public Texture Image;
		public int PathOffset;
		public int PathCount;
		public int TriangleOffset;
		public int TriangleCount;
		public BlendInfo BlendInfo;
		public UniformInfo UniformInfo, UniformInfo2;
	}
}