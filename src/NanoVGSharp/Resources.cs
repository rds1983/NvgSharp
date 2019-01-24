using System.IO;

namespace NanoVGSharp
{
	internal static class Resources
	{
		private static byte[] _effectSource = null;

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
				var path = "NanoVGSharp.Resources.Effect.ogl.mgfxo";
#elif FNA
				var path = "NanoVGSharp.Resources.Effect.fxb";
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
	}
}
