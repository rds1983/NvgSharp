using System;
using System.IO;

#if MONOGAME
using MonoGame.Framework.Utilities;
#endif

namespace NvgSharp
{
	internal static class Resources
	{
		private static byte[] _effectSource = null, _effectWithAASource = null;

		public static byte[] GetNvgEffectSource(bool edgeAntiAlias)
		{
			if (_effectSource != null && !edgeAntiAlias)
			{
				return _effectSource;
			}

			if (_effectWithAASource != null && edgeAntiAlias)
			{
				return _effectWithAASource;
			}

			var name = "Effect";
			if (edgeAntiAlias)
			{
				name += "_EDGE_AA";
			}

#if MONOGAME
			string path;

			switch(PlatformInfo.GraphicsBackend)
			{
				case GraphicsBackend.OpenGL:
					path = "NvgSharp.Resources.MonoGameOGL.bin." + name + ".efb";
					break;

				case GraphicsBackend.DirectX:
					path = "NvgSharp.Resources.MonoGameDX11.bin." + name + ".efb";
					break;

				default:
					throw new NotSupportedException($"Graphics backend {PlatformInfo.GraphicsBackend} is not supported.");
			}

#elif FNA
			var path = "NvgSharp.Resources.FNA.bin." + name + ".efb";
#endif

			byte[] result;



			var ms = new MemoryStream();

			var assembly = typeof(Resources).Assembly;
			using (var stream = assembly.GetManifestResourceStream(path))
			{
				stream.CopyTo(ms);
				result = ms.ToArray();
			}

			if (edgeAntiAlias)
			{
				_effectWithAASource = result;
			}
			else
			{
				_effectSource = result;
			}

			return result;
		}
	}
}
