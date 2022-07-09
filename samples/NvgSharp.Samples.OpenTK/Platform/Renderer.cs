using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace NvgSharp.Samples
{
	internal class Renderer : INvgRenderer
	{
		private const int MAX_VERTICES = 8192;

		private readonly Shader _shader;
		private BufferObject<Vertex> _vertexBuffer;
		private readonly VertexArrayObject _vao;
		private readonly bool _edgeAntiAlias, _stencilStrokes;
		private readonly int[] _viewPortValues = new int[4];

		public bool EdgeAntiAlias => _edgeAntiAlias;

		public unsafe Renderer(bool edgeAntiAlias = true, bool stencilStrokes = true)
		{
			_edgeAntiAlias = edgeAntiAlias;
			_stencilStrokes = stencilStrokes;

			var defines = new Dictionary<string, string>();
			if (edgeAntiAlias)
			{
				defines["EDGE_AA"] = "1";
			}

			_shader = new Shader("shader.vert", "shader.frag", defines);
			_vertexBuffer = new BufferObject<Vertex>(MAX_VERTICES, BufferTarget.ArrayBuffer, true);
			_vao = new VertexArrayObject(sizeof(Vertex));
		}

		~Renderer() => Dispose(false);

		public void Dispose() => Dispose(true);

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			_vao.Dispose();
			_vertexBuffer.Dispose();
			_shader.Dispose();
		}

		public object CreateTexture(int width, int height) => new Texture(width, height);

		public Point GetTextureSize(object texture)
		{
			var t = (Texture)texture;
			return new Point(t.Width, t.Height);
		}

		public void SetTextureData(object texture, Rectangle bounds, byte[] data)
		{
			var t = (Texture)texture;
			t.SetData(bounds, data);
		}

		private void SetUniform(ref UniformInfo uniform)
		{
			_shader.SetUniform("scissorMat", uniform.scissorMat);
			_shader.SetUniform("paintMat", uniform.paintMat);
			_shader.SetUniform("innerCol", uniform.innerCol);
			_shader.SetUniform("outerCol", uniform.outerCol);
			_shader.SetUniform("scissorExt", uniform.scissorExt);
			_shader.SetUniform("scissorScale", uniform.scissorScale);
			_shader.SetUniform("extent", uniform.extent);
			_shader.SetUniform("radius", uniform.radius);
			_shader.SetUniform("feather", uniform.feather);
			_shader.SetUniform("type", (int)uniform.type);

			if (uniform.Image != null)
			{
				var texture = (Texture)uniform.Image;
				texture.Bind();
			}
			else
			{
				GL.BindTexture(TextureTarget.Texture2D, 0);
				GLUtility.CheckError();
			}

			if (_edgeAntiAlias)
			{
				_shader.SetUniform("strokeMult", uniform.strokeMult);
				_shader.SetUniform("strokeThr", uniform.strokeThr);
			}
		}

		private void ProcessFill(CallInfo call)
		{
			// Draw shapes
			GL.Enable(EnableCap.StencilTest);
			GLUtility.CheckError();
			GL.StencilMask(0xff);
			GLUtility.CheckError();
			GL.StencilFunc(StencilFunction.Always, 0, 0xff);
			GLUtility.CheckError();
			GL.ColorMask(false, false, false, false);
			GLUtility.CheckError();

			SetUniform(ref call.UniformInfo);

			// set bindpoint for solid loc
			GL.StencilOpSeparate(StencilFace.Front, StencilOp.Keep, StencilOp.Keep, StencilOp.IncrWrap);
			GLUtility.CheckError();
			GL.StencilOpSeparate(StencilFace.Back, StencilOp.Keep, StencilOp.Keep, StencilOp.DecrWrap);
			GLUtility.CheckError();

			GL.Disable(EnableCap.CullFace);
			GLUtility.CheckError();

			for (var i = 0; i < call.FillStrokeInfos.Count; i++)
			{
				call.FillStrokeInfos[i].DrawFill(PrimitiveType.TriangleFan);
			}

			GL.Enable(EnableCap.CullFace);
			GLUtility.CheckError();

			// Draw anti-aliased pixels
			GL.ColorMask(true, true, true, true);
			GLUtility.CheckError();

			SetUniform(ref call.UniformInfo2);

			if (_edgeAntiAlias)
			{
				GL.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				// Draw fringes
				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}
			}

			// Draw fill
			GL.StencilFunc(StencilFunction.Notequal, 0, 0xff);
			GLUtility.CheckError();
			GL.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
			GLUtility.CheckError();

			call.DrawTriangles(PrimitiveType.TriangleStrip);

			GL.Disable(EnableCap.StencilTest);
			GLUtility.CheckError();
		}

		private void ProcessConvexFill(CallInfo call)
		{
			SetUniform(ref call.UniformInfo);

			for (var i = 0; i < call.FillStrokeInfos.Count; i++)
			{
				var fillStrokeInfo = call.FillStrokeInfos[i];

				fillStrokeInfo.DrawFill(PrimitiveType.TriangleFan);
				fillStrokeInfo.DrawStroke(PrimitiveType.TriangleStrip);
			}
		}

		private void ProcessStroke(CallInfo call)
		{
			if (_stencilStrokes)
			{
				// Fill the stroke base without overlap
				GL.Enable(EnableCap.StencilTest);
				GLUtility.CheckError();
				GL.StencilMask(0xff);
				GLUtility.CheckError();
				GL.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
				GLUtility.CheckError();

				SetUniform(ref call.UniformInfo2);

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}

				// Draw anti-aliased pixels.
				SetUniform(ref call.UniformInfo);

				GL.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}

				// Clear stencil buffer.
				GL.ColorMask(false, false, false, false);
				GLUtility.CheckError();

				GL.StencilFunc(StencilFunction.Always, 0, 0xff);
				GLUtility.CheckError();
				GL.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
				GLUtility.CheckError();

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}

				GL.ColorMask(true, true, true, true);
				GLUtility.CheckError();

				GL.Disable(EnableCap.StencilTest);
				GLUtility.CheckError();
			}
			else
			{
				SetUniform(ref call.UniformInfo);

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}
			}
		}

		private void ProcessTriangles(CallInfo call)
		{
			SetUniform(ref call.UniformInfo);

			call.DrawTriangles(PrimitiveType.Triangles);
		}

		public void Draw(float devicePixelRatio, IEnumerable<CallInfo> calls, Vertex[] vertexes)
		{
			// Setup required GL state
			GL.Enable(EnableCap.CullFace);
			GLUtility.CheckError();
			GL.CullFace(CullFaceMode.Back);
			GLUtility.CheckError();
			GL.FrontFace(FrontFaceDirection.Ccw);
			GLUtility.CheckError();
			GL.Enable(EnableCap.Blend);
			GLUtility.CheckError();
			GL.Disable(EnableCap.DepthTest);
			GLUtility.CheckError();
			GL.Disable(EnableCap.ScissorTest);
			GLUtility.CheckError();
			GL.ColorMask(true, true, true, true);
			GLUtility.CheckError();
			GL.StencilMask(0xffffffff);
			GLUtility.CheckError();
			GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			GLUtility.CheckError();
			GL.StencilFunc(StencilFunction.Always, 0, 0xffffffff);
			GLUtility.CheckError();
			GL.ActiveTexture(TextureUnit.Texture0);
			GLUtility.CheckError();
			GL.BindTexture(TextureTarget.Texture2D, 0);
			GLUtility.CheckError();

			// Bind and update vertex buffer
			if (_vertexBuffer.Size < vertexes.Length)
			{
				_vertexBuffer = new BufferObject<Vertex>(vertexes.Length, BufferTarget.ArrayBuffer, true);
			}

			_vertexBuffer.Bind();
			_vertexBuffer.SetData(vertexes, 0, vertexes.Length);

			// Setup vao
			_vao.Bind();
			var location = _shader.GetAttribLocation("vertex");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 0);

			location = _shader.GetAttribLocation("tcoord");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 8);

			// Setup shader
			_shader.Use();
			_shader.SetUniform("tex", 0);

			GL.GetInteger(GetPName.Viewport, _viewPortValues);
			GLUtility.CheckError();

			var transform = Matrix4x4.CreateOrthographicOffCenter(0, _viewPortValues[2], _viewPortValues[3], 0, 0, -1);
			_shader.SetUniform("transformMat", transform);

			GL.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
			GLUtility.CheckError();

			// Process calls
			foreach (var call in calls)
			{
				switch (call.Type)
				{
					case CallType.Fill:
						ProcessFill(call);
						break;
					case CallType.ConvexFill:
						ProcessConvexFill(call);
						break;
					case CallType.Stroke:
						ProcessStroke(call);
						break;
					case CallType.Triangles:
						ProcessTriangles(call);
						break;
				}
			}
		}
	}
}
