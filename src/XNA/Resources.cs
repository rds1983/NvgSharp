using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace NvgSharp
{
	internal static class Resources
	{
		private static byte[] _effectSource = null, _effectWithAASource = null;

#if MONOGAME
		private static bool? _isOpenGL;
#endif

		public static byte[] GetNvgEffectSource(bool antiAliasing)
		{
			if (_effectSource != null && !antiAliasing)
			{
				return _effectSource;
			}

			if (_effectWithAASource != null && antiAliasing)
			{
				return _effectWithAASource;
			}

			var assembly = typeof(Resources).Assembly;

			var name = "Effect";
			if (antiAliasing)
			{
				name += "_AA";
			}

#if MONOGAME
				var path = IsOpenGL?"NvgSharp.Resources." + name + ".ogl.mgfxo":"NvgSharp.Resources." + name + ".dx11.mgfxo";
#elif FNA
			var path = "NvgSharp.Resources." + name + ".fxb";
#endif

			byte[] result;

			var ms = new MemoryStream();
			using (var stream = assembly.GetManifestResourceStream(path))
			{
				stream.CopyTo(ms);
				result = ms.ToArray();
			}

			if (antiAliasing)
			{
				_effectWithAASource = result;
			}
			else
			{
				_effectSource = result;
			}

			return result;
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
