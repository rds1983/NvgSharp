using System.Collections.Generic;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Numerics;
using System.Drawing;
using Texture2D = System.Object;
using Matrix = System.Numerics.Matrix4x4;
#endif

namespace NvgSharp
{
	public enum CallType
	{
		Fill,
		ConvexFill,
		Stroke,
		Triangles
	}

	public enum RenderType
	{
		FillGradient,
		FillImage,
		Simple,
		Image
	};

	public struct ColorInfo
	{
		public static readonly ColorInfo Transparent = new ColorInfo(0, 0, 0, 0);

		public float R, G, B, A;

		public ColorInfo(float r, float g, float b, float a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public ColorInfo(Color c)
		{
			R = c.R / 255.0f;
			G = c.G / 255.0f;
			B = c.B / 255.0f;
			A = c.A / 255.0f;
		}

		public void MakePremultiplied()
		{
			R *= A;
			G *= A;
			B *= A;
		}

		public Vector4 ToVector4() => new Vector4(R, G, B, A);
	}

	public struct UniformInfo
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
		public Texture2D Image;
		public RenderType type;
	}

	public struct FillStrokeInfo
	{
		public int FillOffset;
		public int FillCount;
		public int StrokeOffset;
		public int StrokeCount;
	}

	public class CallInfo
	{
		public CallType Type;
		public UniformInfo UniformInfo, UniformInfo2;
		public readonly List<FillStrokeInfo> FillStrokeInfos = new List<FillStrokeInfo>();
		public int TriangleOffset;
		public int TriangleCount;
	}
}