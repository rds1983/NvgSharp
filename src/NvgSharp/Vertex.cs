using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace NvgSharp
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct Vertex : IVertexType
	{
		public Vector2 Position;
		public Vector2 TextureCoordinate;

		public static readonly VertexDeclaration VertexDeclaration;
		public Vertex(float x, float y, float u, float v)
		{
			Position.X = x;
			Position.Y = y;
			TextureCoordinate.X = u;
			TextureCoordinate.Y = v;
		}

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
	}
}