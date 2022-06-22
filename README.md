# NvgSharp
[![NuGet](https://img.shields.io/nuget/v/NvgSharp.MonoGame.svg)](https://www.nuget.org/packages/NvgSharp.MonoGame/) [![Build status](https://ci.appveyor.com/api/projects/status/r4cd8vcao5i84xo7?svg=true)](https://ci.appveyor.com/project/RomanShapiro/nvgsharp)
[![Chat](https://img.shields.io/discord/628186029488340992.svg)](https://discord.gg/ZeHxhCY)

C# port of https://github.com/memononen/nanovg for MonoGame and FNA.

# Adding Reference
There are two ways of referencing NvgSharp in the project:
1. Through nuget: `install-package NvgSharp.MonoGame` for MonoGame(or `install-package NvgSharp.FNA` for FNA)
2. As submodule:
    
    a. `git submodule add https://github.com/rds1983/NvgSharp.git`
    
    b. Copy SolutionDefines.targets from NvgSharp/build/MonoGame(or NvgSharp/build/FNA) to your solution folder.

      * If FNA is used, SolutionDefines.targets needs to be edited and FNAProj variable should be updated to the location of FNA.csproj next to the NvgSharp location.
    
    c. Add NvgSharp/src/NvgSharp/NvgSharp.csproj to the solution.

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
Add following code to Draw method to render a red circle:

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
