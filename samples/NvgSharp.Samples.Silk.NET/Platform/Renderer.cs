using FontStashSharp;
using FontStashSharp.Interfaces;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace NvgSharp.Platform
{
	internal class Renderer : IRenderer
	{
		private const int MAX_VERTICES = 8192;

		private readonly Shader _shader;
		private float _width, _height, _devicePx;
		private readonly bool _antiAlias, _stencilStrokes;
		private BufferObject<Vertex> _vertexBuffer;
		private readonly VertexArrayObject _vao;

		private readonly Texture2DManager _textureManager = new Texture2DManager();
		private int _locationVertex, _locationTexCoord;

		public bool AntiAlias => _antiAlias;

		public ITexture2DManager TextureManager => _textureManager;

		public unsafe Renderer(bool antiAlias = true, bool stencilStrokes = true)
		{
			_antiAlias = antiAlias;
			_stencilStrokes = stencilStrokes;

			var defines = new Dictionary<string, string>();
			if (antiAlias)
			{
				defines["EDGE_AA"] = "1";
			}

			_vao = new VertexArrayObject(sizeof(Vertex));
			_vao.Bind();

			_vertexBuffer = new BufferObject<Vertex>(MAX_VERTICES, BufferTargetARB.ArrayBuffer, true);

			_shader = new Shader("shader.vert", "shader.frag", defines);
			_shader.Use();
		}

		~Renderer() => Dispose(false);

		public void Dispose() => Dispose(true);

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			_shader.Dispose();
			_vao.Dispose();
		}

		public void Begin()
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

			// Setup shader
			_shader.Use();
			_shader.SetUniform("tex", 0);
			var transform = Matrix4x4.CreateOrthographicOffCenter(0, _width, _height, 0, 0, -1);
			_shader.SetUniform("transformMat", transform);

			_vao.Bind();
			var location = _shader.GetAttribLocation("vertex");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 0);

			location = _shader.GetAttribLocation("tcoord");
			_vao.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 8);

			Env.Gl.BlendFuncSeparate(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha, BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
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

			if (_antiAlias)
			{
				_shader.SetUniform("strokeMult", uniform.strokeMult);
				_shader.SetUniform("strokeThr", uniform.strokeThr);
			}
		}

		public void End()
		{
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

		private void DrawVertexes(PrimitiveType primitiveType, ArraySegment<Vertex> vertexData)
		{
			// Copy vertex data to gpu mem
			_vertexBuffer.SetData(vertexData.Array, vertexData.Offset, vertexData.Count);

			// Draw
			Env.Gl.DrawArrays(primitiveType, 0, (uint)vertexData.Count);
			GLUtility.CheckError();
		}

		private ArrayBuffer<Vertex> _tempVerts = new ArrayBuffer<Vertex>(4);

		public void RenderFill(ref Paint paint, ref Scissor scissor, float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			if (paths.Count == 1 && paths[0].Convex)
			{
				var uniformInfo = new UniformInfo();
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref uniformInfo);
				SetUniform(ref uniformInfo, (Texture)paint.Image);

				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths[i];

					if (path.Fill != null)
					{
						DrawVertexes(PrimitiveType.TriangleFan, path.Fill.Value);
					}

					if (path.Stroke != null)
					{
						DrawVertexes(PrimitiveType.TriangleStrip, path.Stroke.Value);
					}
				}
			}
			else
			{
				// Setup uniforms for draw calls
				var uniformInfo = new UniformInfo();
				var uniformInfo2 = new UniformInfo();

				_tempVerts.Clear();
				_tempVerts.Add(new Vertex(bounds.X2, bounds.Y2, 0.5f, 1.0f));
				_tempVerts.Add(new Vertex(bounds.X2, bounds.Y, 0.5f, 1.0f));
				_tempVerts.Add(new Vertex(bounds.X, bounds.Y2, 0.5f, 1.0f));
				_tempVerts.Add(new Vertex(bounds.X, bounds.Y, 0.5f, 1.0f));

				// Simple shader for stencil
				uniformInfo.strokeThr = -1.0f;
				uniformInfo.type = (int)RenderType.Simple;

				// Fill shader
				BuildUniform(ref paint, ref scissor, fringe, fringe, -1.0f, ref uniformInfo2);

				// Draw shapes
				Env.Gl.Enable(EnableCap.StencilTest);
				GLUtility.CheckError();
				Env.Gl.StencilMask(0xff);
				GLUtility.CheckError();
				Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.ColorMask(false, false, false, false);
				GLUtility.CheckError();

				SetUniform(ref uniformInfo, null);

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

					if (path.Fill != null)
					{
						DrawVertexes(PrimitiveType.TriangleFan, path.Fill.Value);
					}
				}

				Env.Gl.Enable(EnableCap.CullFace);
				GLUtility.CheckError();

				// Draw anti-aliased pixels
				Env.Gl.ColorMask(true, true, true, true);
				GLUtility.CheckError();

				SetUniform(ref uniformInfo2, (Texture)paint.Image);

				if (_antiAlias)
				{
					Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
					GLUtility.CheckError();
					Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
					GLUtility.CheckError();

					// Draw fringes
					for (var i = 0; i < paths.Count; i++)
					{
						var path = paths[i];

						if (path.Stroke != null)
						{
							DrawVertexes(PrimitiveType.TriangleStrip, path.Stroke.Value);
						}
					}
				}

				// Draw fill
				Env.Gl.StencilFunc(StencilFunction.Notequal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
				GLUtility.CheckError();

				DrawVertexes(PrimitiveType.TriangleStrip, _tempVerts.ToArraySegment());
				GLUtility.CheckError();

				Env.Gl.Disable(EnableCap.StencilTest);
				GLUtility.CheckError();
			}
		}

		public void RenderStroke(ref Paint paint, ref Scissor scissor, float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			if (_stencilStrokes)
			{
				var uniformInfo = new UniformInfo();
				var uniformInfo2 = new UniformInfo();

				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref uniformInfo);
				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, 1.0f - 0.5f / 255.0f, ref uniformInfo2);

				// Fill the stroke base without overlap
				Env.Gl.Enable(EnableCap.StencilTest);
				GLUtility.CheckError();
				Env.Gl.StencilMask(0xff);
				GLUtility.CheckError();
				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
				GLUtility.CheckError();

				SetUniform(ref uniformInfo2, (Texture)paint.Image);

				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths[i];

					if (path.Stroke != null)
					{
						DrawVertexes(PrimitiveType.TriangleStrip, path.Stroke.Value);
					}
				}

				// Draw anti-aliased pixels.
				SetUniform(ref uniformInfo, (Texture)paint.Image);

				Env.Gl.StencilFunc(StencilFunction.Equal, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
				GLUtility.CheckError();

				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths[i];

					if (path.Stroke != null)
					{
						DrawVertexes(PrimitiveType.TriangleStrip, path.Stroke.Value);
					}
				}

				// Clear stencil buffer.
				Env.Gl.ColorMask(false, false, false, false);
				GLUtility.CheckError();

				Env.Gl.StencilFunc(StencilFunction.Always, 0, 0xff);
				GLUtility.CheckError();
				Env.Gl.StencilOp(StencilOp.Zero, StencilOp.Zero, StencilOp.Zero);
				GLUtility.CheckError();

				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths[i];

					if (path.Stroke != null)
					{
						DrawVertexes(PrimitiveType.TriangleStrip, path.Stroke.Value);
					}
				}

				Env.Gl.ColorMask(true, true, true, true);
				GLUtility.CheckError();

				Env.Gl.Disable(EnableCap.StencilTest);
				GLUtility.CheckError();
			}
			else
			{
				var uniformInfo = new UniformInfo();

				BuildUniform(ref paint, ref scissor, strokeWidth, fringe, -1.0f, ref uniformInfo);
				SetUniform(ref uniformInfo, (Texture)paint.Image);

				for (var i = 0; i < paths.Count; i++)
				{
					var path = paths[i];

					if (path.Stroke != null)
					{
						DrawVertexes(PrimitiveType.TriangleStrip, path.Stroke.Value);
					}
				}
			}
		}

		public void RenderTriangles(ref Paint paint, ref Scissor scissor, float fringe, ArraySegment<Vertex> verts)
		{

			var uniformInfo = new UniformInfo();
			BuildUniform(ref paint, ref scissor, 1.0f, fringe, -1.0f, ref uniformInfo);
			uniformInfo.type = (int)RenderType.Image;
			SetUniform(ref uniformInfo, (Texture)paint.Image);

			DrawVertexes(PrimitiveType.Triangles, verts);

		}
	}
}
