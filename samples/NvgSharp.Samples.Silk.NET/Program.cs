using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using NvgSharp.Samples.Demo;
using NvgSharp.Platform;
using System;
using System.Diagnostics;

namespace NvgSharp
{
	class Program
	{
		private static IWindow window;
		private static IInputContext input;
		private static NvgContext nvgContext;
		private static PerfGraph _perfGraph;
		private static Demo demo;
		private static Stopwatch gameTimer;
		private static long startTicks;
		private static long previousTicks;

		private static void Main(string[] args)
		{
			gameTimer = Stopwatch.StartNew();
			startTicks = gameTimer.Elapsed.Ticks;

			var options = WindowOptions.Default;
			options.Size = new Vector2D<int>(1200, 800);
			options.PreferredDepthBufferBits = 24;
			options.PreferredStencilBufferBits = 8;
			options.Title = "FontStashSharp.Silk.NET";
			window = Window.Create(options);

			window.Load += OnLoad;
			window.Render += OnRender;
			window.Closing += OnClose;

			window.Run();
		}

		private static void OnLoad()
		{
			input = window.CreateInput();
			for (int i = 0; i < input.Keyboards.Count; i++)
			{
				input.Keyboards[i].KeyDown += KeyDown;
			}

			Env.Gl = GL.GetApi(window);
			var renderer = new Renderer(true, true);
			nvgContext = new NvgContext(renderer, renderer.EdgeAntiAlias);

			demo = new Demo(nvgContext);
			_perfGraph = new PerfGraph(PerfGraph.Style.GRAPH_RENDER_FPS, "Frame Time", demo.fontSystemNormal);
		}

		private static unsafe void OnRender(double obj)
		{
			Env.Gl.ClearColor(0.3f, 0.3f, 0.32f, 1.0f);
			Env.Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit));

			nvgContext.BeginFrame(window.Size.X, window.Size.Y, 1.0f);

			long currentTicks = gameTimer.Elapsed.Ticks;

			var elapsedTime = (float)TimeSpan.FromTicks(currentTicks - previousTicks).TotalSeconds;
			_perfGraph.Update(elapsedTime);

			previousTicks = currentTicks;

			var totalElapsedTime = (float)TimeSpan.FromTicks(currentTicks - startTicks).TotalSeconds;

			var mousePos = input.Mice[0].Position;
			demo.renderDemo(nvgContext, mousePos.X, mousePos.Y, window.Size.X, window.Size.Y, totalElapsedTime, false);
			_perfGraph.Render(nvgContext, 5, 5);

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