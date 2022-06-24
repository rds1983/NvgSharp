using System.Runtime.InteropServices;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Numerics;
#endif

namespace NvgSharp
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Vertex
#if MONOGAME || FNA || STRIDE
		: IVertexType
#endif
	{
		public Vector2 Position;
		public Vector2 TextureCoordinate;

		public Vertex(float x, float y, float u, float v)
		{
			Position.X = x;
			Position.Y = y;
			TextureCoordinate.X = u;
			TextureCoordinate.Y = v;
		}

#if MONOGAME || FNA || STRIDE

		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexDeclaration;
			}
		}

		static Vertex()
		{
			VertexElement[] elements = new VertexElement[] {
				new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
				new VertexElement(8, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
			};
			VertexDeclaration declaration = new VertexDeclaration(elements);
			VertexDeclaration = declaration;
		}
#endif
	}
}