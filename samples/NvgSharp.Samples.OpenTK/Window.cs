using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Diagnostics;

namespace NvgSharp.Samples
{
	internal class Window : GameWindow
	{
		private NvgContext nvgContext;
		private PerfGraph _perfGraph;
		private Demo demo;
		private Stopwatch gameTimer;
		private long startTicks;
		private long previousTicks;

		public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
			: base(gameWindowSettings, nativeWindowSettings)
		{
			gameTimer = Stopwatch.StartNew();
			startTicks = gameTimer.Elapsed.Ticks;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			var renderer = new Renderer(true, true);
			nvgContext = new NvgContext(renderer, renderer.EdgeAntiAlias);

			demo = new Demo(nvgContext);
			_perfGraph = new PerfGraph(PerfGraph.Style.GRAPH_RENDER_FPS, "Frame Time", demo.fontSystemNormal);
		}

		protected override void OnRenderFrame(FrameEventArgs args)
		{
			base.OnRenderFrame(args);

			GL.ClearColor(0.3f, 0.3f, 0.32f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
			nvgContext.BeginFrame(Size.X, Size.Y, 1.0f);

			long currentTicks = gameTimer.Elapsed.Ticks;

			var elapsedTime = (float)TimeSpan.FromTicks(currentTicks - previousTicks).TotalSeconds;
			_perfGraph.Update(elapsedTime);

			previousTicks = currentTicks;

			var totalElapsedTime = (float)TimeSpan.FromTicks(currentTicks - startTicks).TotalSeconds;

			demo.renderDemo(nvgContext, MousePosition.X, MousePosition.Y, Size.X, Size.Y, totalElapsedTime, false);
			_perfGraph.Render(nvgContext, 5, 5);

			nvgContext.EndFrame();

			SwapBuffers();
		}

		protected override void OnResize(ResizeEventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(0, 0, Size.X, Size.Y);
		}
	}
}
