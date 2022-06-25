using System.Runtime.CompilerServices;

#if MONOGAME
[assembly: InternalsVisibleTo("NvgSharp.Text.MonoGame", AllInternalsVisible = true)]
#elif FNA
[assembly: InternalsVisibleTo("NvgSharp.Text.FNA", AllInternalsVisible = true)]
#else
[assembly: InternalsVisibleTo("NvgSharp.Text", AllInternalsVisible = true)]
#endif
