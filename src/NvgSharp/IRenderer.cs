using System.Collections.Generic;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif STRIDE
using Stride.Graphics;
#else
using FontStashSharp.Interfaces;
using System.Numerics;
#endif

namespace NvgSharp
{
	public interface IRenderer
	{
#if MONOGAME || FNA || STRIDE
		GraphicsDevice GraphicsDevice { get; }
#else
		ITexture2DManager TextureManager { get; }
#endif

		void Draw(Vector2 viewportSize, float devicePixelRatio, IEnumerable<CallInfo> calls, Vertex[] vertexes);
	}
}
