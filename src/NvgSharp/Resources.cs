using System.IO;

namespace NvgSharp
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
				var path = "NvgSharp.Resources.Effect.ogl.mgfxo";
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
	}
}
