using System.IO;

namespace NanoVGSharp
{
	internal static class Resources
	{
		private static byte[] _nvgEffectSource = null;

		public static byte[] NvgEffectSource
		{
			get
			{
				if (_nvgEffectSource != null)
				{
					return _nvgEffectSource;
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
					_nvgEffectSource = ms.ToArray();
				}

				return _nvgEffectSource;
			}
		}
	}
}
