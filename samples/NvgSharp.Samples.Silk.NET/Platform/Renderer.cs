using FontStashSharp;
using FontStashSharp.Interfaces;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;

namespace NvgSharp.Platform
{
	internal class Renderer : IRenderer
	{
		private const int MAX_VERTICES = 8192;

		private readonly Shader _shader;
		private readonly BufferObject<Vertex> _vertexBuffer = new BufferObject<Vertex>(MAX_VERTICES, BufferTargetARB.ArrayBuffer, true);
		private readonly VertexArrayObject _vao;
		private readonly ArrayBuffer<Vertex> _vertexArray = new ArrayBuffer<Vertex>(MAX_VERTICES);
		private readonly List<PathInfo> _paths = new List<PathInfo>(1024);
		private readonly List<CallInfo> _calls = new List<CallInfo>(1024);

		private readonly Texture2DManager _textureManager = new Texture2DManager();

		public ITexture2DManager TextureManager => _textureManager;

		public unsafe Renderer()
		{
			_shader = new Shader("shader.vert", "shader.frag");
			_shader.Use();

			_vao = new VertexArrayObject(sizeof(Vertex));
			_vao.Bind();

			var location = _shader.GetAttribLocation("vertex");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 0);

			location = _shader.GetAttribLocation("tcoord");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 8);
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
			Env.Gl.Disable(EnableCap.DepthTest);
			GLUtility.CheckError();
			Env.Gl.Enable(EnableCap.Blend);
			GLUtility.CheckError();
			Env.Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			GLUtility.CheckError();
			Env.Gl.Enable(EnableCap.CullFace);
			GLUtility.CheckError();

/*			_shader.Use();
			_shader.SetUniform("TextureSampler", 0);

			var transform = Matrix4x4.CreateOrthographicOffCenter(0, 1200, 800, 0, 0, -1);
			_shader.SetUniform("MatrixTransform", transform);

			_vao.Bind();
			_indexBuffer.Bind();
			_vertexBuffer.Bind();*/
		}


		public void End()
		{
		}

		public void Viewport(float width, float height, float devicePixelRatio)
		{
			// throw new System.NotImplementedException();
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
				uniform.scissorMat = scissor.Transform.BuildInverse().ToMatrix3x2();
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
				uniform.type = (float)RenderType.FillImage;
				uniform.texType = 1.0f;
			}
			else
			{
				uniform.type = (float)RenderType.FillGradient;
				uniform.radius = paint.Radius;
				uniform.feather = paint.Feather;
			}

			uniform.paintMat = paint.Transform.BuildInverse().ToMatrix3x2();
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
				call.UniformInfo.type = (float)RenderType.Simple;

				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref call.UniformInfo);
			}
			else
			{
				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref call.UniformInfo);
			}

			_calls.Add(call);

			// Draw shapes
			/*			Env.Gl.Enable(EnableCap.StencilTest);
						GLUtility.CheckError();
						Env.Gl.StencilMask(0xff);
						GLUtility.CheckError();
						Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xff);
						GLUtility.CheckError();
						Env.Gl.ColorMask(false, false, false, false);
						GLUtility.CheckError();

						// set bindpoint for solid loc
						Env.Gl.StencilOpSeparate(StencilFaceDirection.Front, StencilOp.Keep, StencilOp.Keep, StencilOp.IncrWrap);
						GLUtility.CheckError();
						Env.Gl.StencilOpSeparate(StencilFaceDirection.Back, StencilOp.Keep, StencilOp.Keep, StencilOp.DecrWrap);
						GLUtility.CheckError();

						Env.Gl.Disable(EnableCap.CullFace);
						GLUtility.CheckError();

						for (var i = 0; i < paths.Count; i++)
						{
							var path = paths[i];
							glDrawArrays(GL_TRIANGLE_FAN, paths[i].fillOffset, paths[i].fillCount);
							GLUtility.CheckError();
						}

						Env.Gl.Enable(EnableCap.CullFace);
						GLUtility.CheckError();

						// Draw anti-aliased pixels
						Env.Gl.ColorMask(true, true, true, true);
						GLUtility.CheckError();

						if (Env.Antialiasing)
						{
							Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
							GLUtility.CheckError();
							Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
							GLUtility.CheckError();

							// Draw fringes
							for (var i = 0; i < paths.Count; i++)
							{
								var path = paths[i];
								glDrawArrays(GL_TRIANGLE_STRIP, paths[i].strokeOffset, paths[i].strokeCount);
								GLUtility.CheckError();
							}
						}

						// Draw fill
						Env.Gl.StencilFunc(StencilFunction.Notequal, 0, 0xff);
						GLUtility.CheckError();
						Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
						GLUtility.CheckError();

						glDrawArrays(GL_TRIANGLE_STRIP, call->triangleOffset, call->triangleCount);

						Env.Gl.Disable(EnableCap.StencilTest);
						GLUtility.CheckError();*/
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
			if (Env.StencilStrokes)
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
			call.UniformInfo.type = (float)RenderType.Image;

			_calls.Add(call);
		}
	}
}
