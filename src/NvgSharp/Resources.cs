using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
{
	internal static class Resources
	{
		private static byte[] _effectSource = null;

#if MONOGAME		
		private static bool? _isOpenGL;
#endif

		public static byte[] NvgEffectSource
		{
			get
			{
				if (_effectSource != null)
				{
					return _effectSource;
				}

				var assembly = typeof(Resources).Assembly;

#if MONOGAME
				var path = IsOpenGL?"NvgSharp.Resources.Effect.ogl.mgfxo":"NvgSharp.Resources.Effect.dx11.mgfxo";
#elif FNA
				var path = "NvgSharp.Resources.Effect.fxb";
#endif

				var ms = new MemoryStream();
				using (var stream = assembly.GetManifestResourceStream(path))
				{
					stream.CopyTo(ms);
					_effectSource = ms.ToArray();
				}

				return _effectSource;
			}
		}

#if MONOGAME		
		public static bool IsOpenGL
		{
			get
			{
				if (_isOpenGL == null)
				{
					_isOpenGL = (from f in typeof(GraphicsDevice).GetFields(BindingFlags.NonPublic |
						 BindingFlags.Instance)
								 where f.Name == "glFramebuffer"
								 select f).FirstOrDefault() != null;
				}

				return _isOpenGL.Value;
			}
		}
#endif
	}
}
