using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using NvgSharp.Samples.Demo;
using NvgSharp.Platform;

namespace NvgSharp
{
	class Program
	{
		private static IWindow window;
		private static NvgContext nvgContext;
		private static Demo demo;

		private static void Main(string[] args)
		{
			var options = WindowOptions.Default;
			options.Size = new Vector2D<int>(1200, 800);
			options.Title = "FontStashSharp.Silk.NET";
			window = Window.Create(options);

			window.Load += OnLoad;
			window.Render += OnRender;
			window.Closing += OnClose;

			window.Run();
		}

		private static void OnLoad()
		{
			IInputContext input = window.CreateInput();
			for (int i = 0; i < input.Keyboards.Count; i++)
			{
				input.Keyboards[i].KeyDown += KeyDown;
			}

			Env.Gl = GL.GetApi(window);
			var renderer = new Renderer();
			nvgContext = new NvgContext(renderer);
			demo = new Demo(nvgContext);
		}

		private static unsafe void OnRender(double obj)
		{
			Env.Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

			nvgContext.BeginFrame(window.Size.X, window.Size.Y, 1.0f);

			demo.renderDemo(nvgContext, 0, 0, window.Size.X, window.Size.Y, 0.0f, false);

			nvgContext.EndFrame();
		}

		private static void OnClose()
		{
		}

		private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
		{
			if (arg2 == Key.Escape)
			{
				window.Close();
			}
		}
	}
}