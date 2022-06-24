using System.Collections.Generic;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif STRIDE
using Stride.Graphics;
#else
using FontStashSharp.Interfaces;
using System.Numerics;
using Matrix = System.Numerics.Matrix4x4;
using Texture2D = System.Object;
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

	public struct UniformInfo
	{
		public Matrix scissorMat;
		public Matrix paintMat;
		public Vector4 innerCol;
		public Vector4 outerCol;
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

	public interface INvgRenderer
	{
#if MONOGAME || FNA || STRIDE
		GraphicsDevice GraphicsDevice { get; }
#else
		ITexture2DManager TextureManager { get; }
#endif

		void Draw(Vector2 viewportSize, float devicePixelRatio, IEnumerable<CallInfo> calls, Vertex[] vertexes);
	}
}
