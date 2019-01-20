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

				var ms = new MemoryStream();
				using (var stream = assembly.GetManifestResourceStream("NanoVGSharp.Resources.Effect.ogl.mgfxo"))
				{
					stream.CopyTo(ms);
					_nvgEffectSource = ms.ToArray();
				}

				return _nvgEffectSource;
			}
		}
	}
}
