# NvgSharp
[![NuGet](https://img.shields.io/nuget/v/NvgSharp.MonoGame.svg)](https://www.nuget.org/packages/NvgSharp.MonoGame/) [![Build status](https://ci.appveyor.com/api/projects/status/r4cd8vcao5i84xo7?svg=true)](https://ci.appveyor.com/project/RomanShapiro/nvgsharp)

C# port of https://github.com/memononen/nanovg for MonoGame and FNA.

# Adding Reference
`Install-Package NvgSharp.MonoGame` (or `Install-Package NvgSharp.FNA` for FNA)

# Stencil
NvgSharp requires Game to be created with stencil.
That could be archived by setting PreferredDepthStencilFormat to DepthFormat.Depth24Stencil8 in the GraphicsDeviceManager creation.
I.e.
```c#
public Game1()
{
	_graphics = new GraphicsDeviceManager(this)
	{
		PreferredBackBufferWidth = 1000,
		PreferredBackBufferHeight = 600,
		PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
	};
}
```

# Context Creation
Add following field to the Game class:
```c#
	private NvgContext _context;
```

And following code to the LoadContent method:
```c#
	_context = new NvgContext(GraphicsDevice, 0);
```

# Drawing
Following code renders a simple circle:

```c#
	_context.BeginFrame(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, 1.0f);

	_context.FillColor(Color.Red);
	_context.Circle(100, 100, 20);
	_context.Fill();

	_context.EndFrame();
```

# Demo
This [sample](https://github.com/rds1983/NvgSharp/tree/master/samples/NvgSharp.Samples.Demo) demonstrates rendering of the following:
![](/images/nanovg.gif)

## Credits
* [nanovg](https://github.com/memononen/nanovg)
* [MonoGame](http://www.monogame.net/)
* [FNA](https://github.com/FNA-XNA/FNA)
* [stb](https://github.com/nothings/stb)
