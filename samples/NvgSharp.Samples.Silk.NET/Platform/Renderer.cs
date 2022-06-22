using FontStashSharp;
using FontStashSharp.Interfaces;
using Silk.NET.OpenGL;
using System;
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
		private readonly ArrayBuffer<Vertex> _vertexArray = new ArrayBuffer<Vertex>(MAX_VERTICES);
		private readonly ArrayBuffer<PathInfo> _paths = new ArrayBuffer<PathInfo>(1024);
		private readonly ArrayBuffer<CallInfo> _calls = new ArrayBuffer<CallInfo>(1024);
		private float _width, _height, _devicePx;
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

		public void Begin()
		{
		}

		private void SetUniform(ref UniformInfo uniform, Texture texture)
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
			_shader.SetUniform("texType", uniform.texType);
			_shader.SetUniform("type", uniform.type);

			if (texture != null)
			{
				texture.Bind();
			}
			else
			{
				Env.Gl.BindTexture(TextureTarget.Texture2D, 0);
				GLUtility.CheckError();
			}

			_shader.SetUniform("tex", 0);

			if (_edgeAntiAlias)
			{
				_shader.SetUniform("strokeMult", uniform.strokeMult);
				_shader.SetUniform("strokeThr", uniform.strokeThr);
			}
		}

		private void ProcessFill(ref CallInfo call)
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

			SetUniform(ref call.UniformInfo, null);

			// set bindpoint for solid loc
			Env.Gl.StencilOpSeparate(StencilFaceDirection.Front, StencilOp.Keep, StencilOp.Keep, StencilOp.IncrWrap);
			GLUtility.CheckError();
			Env.Gl.StencilOpSeparate(StencilFaceDirection.Back, StencilOp.Keep, StencilOp.Keep, StencilOp.DecrWrap);
			GLUtility.CheckError();

			Env.Gl.Disable(EnableCap.CullFace);
			GLUtility.CheckError();

			for (var i = 0; i < call.PathCount; i++)
			{
				var path = _paths[call.PathOffset + i];

				Env.Gl.DrawArrays(PrimitiveType.TriangleFan, path.FillOffset, (uint)path.FillCount);
				GLUtility.CheckError();
			}

			Env.Gl.Enable(EnableCap.CullFace);
			GLUtility.CheckError();

			// Draw anti-aliased pixels
			Env.Gl.ColorMask(true, true, true, true);
			GLUtility.CheckError();

			SetUniform(ref call.UniformInfo2, call.Image);

			if (_edgeAntiAlias)
			{
				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				// Draw fringes
				for (var i = 0; i < call.PathCount; i++)
				{
					var path = _paths[call.PathOffset + i];

					Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, path.StrokeOffset, (uint)path.StrokeCount);
					GLUtility.CheckError();
				}
			}

			// Draw fill
			Env.Gl.StencilFunc(StencilFunction.Notequal, 0, 0xff);
			GLUtility.CheckError();
			Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
			GLUtility.CheckError();

			Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, call.TriangleOffset, (uint)call.TriangleCount);
			GLUtility.CheckError();

			Env.Gl.Disable(EnableCap.StencilTest);
			GLUtility.CheckError();
		}

		private void ProcessConvexFill(ref CallInfo call)
		{
			SetUniform(ref call.UniformInfo, call.Image);

			for (var i = 0; i < call.PathCount; i++)
			{
				var path = _paths[call.PathOffset + i];

				Env.Gl.DrawArrays(PrimitiveType.TriangleFan, path.FillOffset, (uint)path.FillCount);
				GLUtility.CheckError();

				if (path.StrokeCount > 0)
				{
					Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, path.StrokeOffset, (uint)path.StrokeCount);
					GLUtility.CheckError();
				}
			}
		}

		private void ProcessStroke(ref CallInfo call)
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

				SetUniform(ref call.UniformInfo2, call.Image);

				for (var i = 0; i < call.PathCount; i++)
				{
					var path = _paths[call.PathOffset + i];

					Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, path.StrokeOffset, (uint)path.StrokeCount);
					GLUtility.CheckError();
				}

				// Draw anti-aliased pixels.
				SetUniform(ref call.UniformInfo, call.Image);

				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				for (var i = 0; i < call.PathCount; i++)
				{
					var path = _paths[call.PathOffset + i];

					Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, path.StrokeOffset, (uint)path.StrokeCount);
					GLUtility.CheckError();
				}

				// Clear stencil buffer.
				Env.Gl.ColorMask(false, false, false, false);
				GLUtility.CheckError();

				Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
				GLUtility.CheckError();

				for (var i = 0; i < call.PathCount; i++)
				{
					var path = _paths[call.PathOffset + i];

					Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, path.StrokeOffset, (uint)path.StrokeCount);
					GLUtility.CheckError();
				}

				Env.Gl.ColorMask(true, true, true, true);
				GLUtility.CheckError();

				Env.Gl.Disable(EnableCap.StencilTest);
				GLUtility.CheckError();
			}
			else
			{
				SetUniform(ref call.UniformInfo, call.Image);

				for (var i = 0; i < call.PathCount; i++)
				{
					var path = _paths[call.PathOffset + i];

					Env.Gl.DrawArrays(PrimitiveType.TriangleStrip, path.StrokeOffset, (uint)path.StrokeCount);
					GLUtility.CheckError();
				}
			}
		}

		private void ProcessTriangles(ref CallInfo call)
		{
			SetUniform(ref call.UniformInfo, call.Image);

			Env.Gl.DrawArrays(PrimitiveType.Triangles, call.TriangleOffset, (uint)call.TriangleCount);
			GLUtility.CheckError();
		}

		public void End()
		{
			if (_calls.Count == 0)
			{
				return;
			}

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
			if (_vertexBuffer == null || _vertexBuffer.Size < _vertexArray.Capacity)
			{
				_vertexBuffer = new BufferObject<Vertex>(_vertexArray.Capacity, BufferTargetARB.ArrayBuffer, true);
			}

			_vertexBuffer.Bind();
			_vertexBuffer.SetData(_vertexArray.Array, 0, _vertexArray.Count);

			// Setup vao
			_vao.Bind();
			var location = _shader.GetAttribLocation("vertex");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 0);

			location = _shader.GetAttribLocation("tcoord");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 8);

			// Setup shader
			_shader.Use();
			_shader.SetUniform("tex", 0);
			var transform = Matrix4x4.CreateOrthographicOffCenter(0, _width, _height, 0, 0, -1);
			_shader.SetUniform("transformMat", transform);

			// Process calls
			for (var i = 0; i < _calls.Count; i++)
			{
				var call = _calls[i];

				Env.Gl.BlendFuncSeparate(call.BlendInfo.srcRgb, call.BlendInfo.destRgb, call.BlendInfo.srcAlpha, call.BlendInfo.destAlpha);

				switch (call.Type)
				{
					case CallType.Fill:
						ProcessFill(ref call);
						break;
					case CallType.ConvexFill:
						ProcessConvexFill(ref call);
						break;
					case CallType.Stroke:
						ProcessStroke(ref call);
						break;
					case CallType.Triangles:
						ProcessTriangles(ref call);
						break;
				}
			}

			_vertexArray.Clear();
			_paths.Clear();
			_calls.Clear();
		}

		public void Viewport(float width, float height, float devicePixelRatio)
		{
			_width = width;
			_height = height;
			_devicePx = devicePixelRatio;
		}

		private static void BuildUniform(ref Paint paint, ref Scissor scissor, float width, 
			float fringe, float strokeThr, ref UniformInfo uniform)
		{
			uniform.innerCol = paint.InnerColor.ToColorInfo().MakePremultiplied();
			uniform.outerCol = paint.OuterColor.ToColorInfo().MakePremultiplied();

			if (scissor.Extent.X < -0.5f || scissor.Extent.Y < -0.5f)
			{
				uniform.scissorMat.MakeZero();
				uniform.scissorExt.X = 1.0f;
				uniform.scissorExt.Y = 1.0f;
				uniform.scissorScale.X = 1.0f;
				uniform.scissorScale.Y = 1.0f;
			}
			else
			{
				uniform.scissorMat = scissor.Transform.BuildInverse().ToMatrix4x4();
				uniform.scissorExt.X = scissor.Extent.X;
				uniform.scissorExt.Y = scissor.Extent.Y;
				uniform.scissorScale.X = (float)Math.Sqrt(scissor.Transform.T1 * scissor.Transform.T1 + scissor.Transform.T3 * scissor.Transform.T3) / fringe;
				uniform.scissorScale.Y = (float)Math.Sqrt(scissor.Transform.T2 * scissor.Transform.T2 + scissor.Transform.T4 * scissor.Transform.T4) / fringe;
			}

			uniform.extent = paint.Extent;
			uniform.strokeMult = (width * 0.5f + fringe * 0.5f) / fringe;
			uniform.strokeThr = strokeThr;

			if (paint.Image != null)
			{
				uniform.type = (int)RenderType.FillImage;
				uniform.texType = 1;
			}
			else
			{
				uniform.type = (int)RenderType.FillGradient;
				uniform.radius = paint.Radius;
				uniform.feather = paint.Feather;
			}

			uniform.paintMat = paint.Transform.BuildInverse().ToMatrix4x4();
		}

		public void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			var call = new CallInfo
			{
				Type = CallType.Fill,
				TriangleCount = 4,
				PathOffset = _paths.Count,
				PathCount = paths.Count,
				Image = (Texture)paint.Image,
				BlendInfo = BlendInfo.Default
			};

			if (paths.Count == 1 && paths[0].Convex)
			{
				call.Type = CallType.ConvexFill;
				// Bounding box fill quad not needed for convex fill
				call.TriangleCount = 0;
			}

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths[i];

				var pathInfo = new PathInfo();
				if (path.Fill != null && path.Fill.Value.Count > 0)
				{
					pathInfo.FillOffset = _vertexArray.Count;
					pathInfo.FillCount = path.Fill.Value.Count;
					_vertexArray.Add(path.Fill.Value);
				}
				if (path.Stroke != null && path.Stroke.Value.Count > 0)
				{
					pathInfo.StrokeOffset = _vertexArray.Count;
					pathInfo.StrokeCount = path.Stroke.Value.Count;
					_vertexArray.Add(path.Stroke.Value);
				}

				_paths.Add(pathInfo);
			}

			// Setup uniforms for draw calls
			if (call.Type == CallType.Fill)
			{
				// Quad
				call.TriangleOffset = _vertexArray.Count;
				_vertexArray.Add(new Vertex(bounds.X2, bounds.Y2, 0.5f, 1.0f));
				_vertexArray.Add(new Vertex(bounds.X2, bounds.Y, 0.5f, 1.0f));
				_vertexArray.Add(new Vertex(bounds.X, bounds.Y2, 0.5f, 1.0f));
				_vertexArray.Add(new Vertex(bounds.X, bounds.Y, 0.5f, 1.0f));

				// Simple shader for stencil
				call.UniformInfo.strokeThr = -1.0f;
				call.UniformInfo.type = (int)RenderType.Simple;

				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref call.UniformInfo2);
			}
			else
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref call.UniformInfo);
			}

			_calls.Add(call);
		}

		public void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			var call = new CallInfo
			{
				Type = CallType.Stroke,
				PathOffset = _paths.Count,
				PathCount = paths.Count,
				Image = (Texture)paint.Image,
				BlendInfo = BlendInfo.Default
			};

			for (var i = 0; i < paths.Count; i++)
			{
				var path = paths[i];

				var pathInfo = new PathInfo();
				if (path.Stroke != null && path.Stroke.Value.Count > 0)
				{
					pathInfo.StrokeOffset = _vertexArray.Count;
					pathInfo.StrokeCount = path.Stroke.Value.Count;
					_vertexArray.Add(path.Stroke.Value);
				}

				_paths.Add(pathInfo);
			}

			// Setup uniforms for draw calls
			if (_stencilStrokes)
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref call.UniformInfo);
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, 1.0f - 0.5f / 255.0f, ref call.UniformInfo2);
			}
			else
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref call.UniformInfo);
			}

			_calls.Add(call);
		}

		public void RenderTriangles(ref Paint paint, ref Scissor scissor, float fringe, ArraySegment<Vertex> verts)
		{
			var call = new CallInfo
			{
				Type = CallType.Triangles,
				Image = (Texture)paint.Image,
				BlendInfo = BlendInfo.Default,
				TriangleOffset = _vertexArray.Count,
				TriangleCount = verts.Count
			};

			_vertexArray.Add(verts);

			// Fill shader
			BuildUniform(ref paint, ref scissor, 1.0f, fringe, -1.0f, ref call.UniformInfo);
			call.UniformInfo.type = (int)RenderType.Image;

			_calls.Add(call);
		}
	}
}
