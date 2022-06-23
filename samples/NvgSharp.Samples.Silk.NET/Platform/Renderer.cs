using FontStashSharp.Interfaces;
using Silk.NET.OpenGL;
using System.Collections.Generic;
using System.Numerics;

namespace NvgSharp.Platform
{
	internal class Renderer : IRenderer
	{
		private const int MAX_VERTICES = 8192;

		private readonly Shader _shader;
		private BufferObject<Vertex> _vertexBuffer;
		private readonly VertexArrayObject _vao;
		private readonly bool _edgeAntiAlias, _stencilStrokes;

		private readonly Texture2DManager _textureManager = new Texture2DManager();

		public bool EdgeAntiAlias => _edgeAntiAlias;

		public ITexture2DManager TextureManager => _textureManager;

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
			_shader.Use();

			_vertexBuffer = new BufferObject<Vertex>(MAX_VERTICES, BufferTargetARB.ArrayBuffer, true);

			_vao = new VertexArrayObject(sizeof(Vertex));
			_vao.Bind();
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

		private void SetUniform(ref UniformInfo uniform)
		{
			_shader.SetUniform("scissorMat", uniform.scissorMat);
			_shader.SetUniform("paintMat", uniform.paintMat);
			_shader.SetUniform("innerCol", uniform.innerCol.ToVector4());
			_shader.SetUniform("outerCol", uniform.outerCol.ToVector4());
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
				Env.Gl.BindTexture(TextureTarget.Texture2D, 0);
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
			Env.Gl.Enable(EnableCap.StencilTest);
			GLUtility.CheckError();
			Env.Gl.StencilMask(0xff);
			GLUtility.CheckError();
			Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xff);
			GLUtility.CheckError();
			Env.Gl.ColorMask(false, false, false, false);
			GLUtility.CheckError();

			SetUniform(ref call.UniformInfo);

			// set bindpoint for solid loc
			Env.Gl.StencilOpSeparate(StencilFaceDirection.Front, StencilOp.Keep, StencilOp.Keep, StencilOp.IncrWrap);
			GLUtility.CheckError();
			Env.Gl.StencilOpSeparate(StencilFaceDirection.Back, StencilOp.Keep, StencilOp.Keep, StencilOp.DecrWrap);
			GLUtility.CheckError();

			Env.Gl.Disable(EnableCap.CullFace);
			GLUtility.CheckError();

			for (var i = 0; i < call.FillStrokeInfos.Count; i++)
			{
				call.FillStrokeInfos[i].DrawFill(PrimitiveType.TriangleFan);
			}

			Env.Gl.Enable(EnableCap.CullFace);
			GLUtility.CheckError();

			// Draw anti-aliased pixels
			Env.Gl.ColorMask(true, true, true, true);
			GLUtility.CheckError();

			SetUniform(ref call.UniformInfo2);

			if (_edgeAntiAlias)
			{
				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				// Draw fringes
				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}
			}

			// Draw fill
			Env.Gl.StencilFunc(StencilFunction.Notequal, 0, 0xff);
			GLUtility.CheckError();
			Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
			GLUtility.CheckError();

			call.DrawTriangles(PrimitiveType.TriangleStrip);

			Env.Gl.Disable(EnableCap.StencilTest);
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
				Env.Gl.Enable(EnableCap.StencilTest);
				GLUtility.CheckError();
				Env.Gl.StencilMask(0xff);
				GLUtility.CheckError();
				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
				GLUtility.CheckError();

				SetUniform(ref call.UniformInfo2);

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}

				// Draw anti-aliased pixels.
				SetUniform(ref call.UniformInfo);

				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}

				// Clear stencil buffer.
				Env.Gl.ColorMask(false, false, false, false);
				GLUtility.CheckError();

				Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
				GLUtility.CheckError();

				for (var i = 0; i < call.FillStrokeInfos.Count; i++)
				{
					call.FillStrokeInfos[i].DrawStroke(PrimitiveType.TriangleStrip);
				}

				Env.Gl.ColorMask(true, true, true, true);
				GLUtility.CheckError();

				Env.Gl.Disable(EnableCap.StencilTest);
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

		public void Draw(Vector2 viewportSize, float devicePixelRatio, IEnumerable<CallInfo> calls, Vertex[] vertexes)
		{
			// Setup required GL state
			Env.Gl.Enable(EnableCap.CullFace);
			GLUtility.CheckError();
			Env.Gl.CullFace(CullFaceMode.Back);
			GLUtility.CheckError();
			Env.Gl.FrontFace(FrontFaceDirection.Ccw);
			GLUtility.CheckError();
			Env.Gl.Enable(EnableCap.Blend);
			GLUtility.CheckError();
			Env.Gl.Disable(EnableCap.DepthTest);
			GLUtility.CheckError();
			Env.Gl.Disable(EnableCap.ScissorTest);
			GLUtility.CheckError();
			Env.Gl.ColorMask(true, true, true, true);
			GLUtility.CheckError();
			Env.Gl.StencilMask(0xffffffff);
			GLUtility.CheckError();
			Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			GLUtility.CheckError();
			Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xffffffff);
			GLUtility.CheckError();
			Env.Gl.ActiveTexture(TextureUnit.Texture0);
			GLUtility.CheckError();
			Env.Gl.BindTexture(TextureTarget.Texture2D, 0);
			GLUtility.CheckError();

			// Bind and update vertex buffer
			if (_vertexBuffer.Size < vertexes.Length)
			{
				_vertexBuffer = new BufferObject<Vertex>(vertexes.Length, BufferTargetARB.ArrayBuffer, true);
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
			var transform = Matrix4x4.CreateOrthographicOffCenter(0, viewportSize.X, viewportSize.Y, 0, 0, -1);
			_shader.SetUniform("transformMat", transform);

			Env.Gl.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
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
