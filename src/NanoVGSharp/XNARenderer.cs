using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NanoVGSharp
{
	public class XNARenderer: IRenderer
	{
		private readonly GraphicsDevice _device;
		private DynamicVertexBuffer _VertexBuffer;
		private DynamicIndexBuffer _IndexBuffer;
		private BasicEffect basicEffect;
		public readonly List<Texture2D> _textures = new List<Texture2D>();
		private readonly RasterizerState _rasterizerState = new RasterizerState
		{
			CullMode = CullMode.None,
			ScissorTestEnable = true
		};

		private BlendState _oldBlendState;
		private RasterizerState _oldRasterizerState;
		private SamplerState _oldSamplerState;
		private DepthStencilState _oldDepthStencilState;
		private readonly Buffer<Vertex> _sourceVertexes = new Buffer<Vertex>(1024);
		private readonly Buffer<VertexPositionColorTexture> _vertexes = new Buffer<VertexPositionColorTexture>(1024);
		private readonly Buffer<ushort> _indexes = new Buffer<ushort>(2048);
		Transform scissorTransform;
		Vector2 scissorExt, scissorScale;

		public bool AntiAliasingOn
		{
			get; set;
		}

		private enum RenderingType
		{
			FILLING,
			IMAGE,
			TRIS
		}

		public XNARenderer(GraphicsDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}

			_device = device;

			_VertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 2000, BufferUsage.WriteOnly);
			_IndexBuffer = new DynamicIndexBuffer(device, typeof(ushort), 6000, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(device);
		}

		private Texture2D GetTextureById(int id)
		{
			return _textures[id - 1];
		}

		public int renderCreateTexture(int type, int w, int h, int imageFlags, byte[] d)
		{
			var texture = new Texture2D(_device, w, h);

			if (d != null)
			{
				texture.SetData(d);
			}

			_textures.Add(texture);

			return _textures.Count;
		}

		public void renderDeleteTexture(int image)
		{
			throw new NotImplementedException();
		}

		public void renderUpdateTexture(int image, int x, int y, int w, int h, byte[] d)
		{
			var texture = GetTextureById(image);

			if (d != null)
			{
				var sz = w * h;
				var b = new byte[texture.Width * texture.Height * 4];

				texture.GetData<byte>(b);
				for (var xx = x; xx < x + w; ++xx)
				{
					for (var yy = y; yy < y + h; ++yy)
					{
						var destPos = yy * texture.Width + xx;

						for (var k = 0; k < 3; ++k)
						{
							b[destPos * 4 + k] = 255;
						}

						b[destPos * 4 + 3] = d[destPos];
					}
				}

				texture.SetData(b);
			}
		}

		public void renderGetTextureSize(int image, out int w, out int h)
		{
			var texture = GetTextureById(image);

			w = texture.Width;
			h = texture.Height;
		}

		public void renderViewport(float width, float height, float devicePixelRatio)
		{
			// TO DO
		}

		private float scissorMask(Vector2 p)
		{
			scissorTransform.TransformVector(out p, p);
			Vector2 sc = new Vector2(Math.Abs(p.X), Math.Abs(p.Y)) - scissorExt;
			sc = new Vector2(0.5f, 0.5f) - sc * scissorScale;
			return clamp(sc.X) * clamp(sc.Y);
		}

		private static float sdroundrect(Vector2 pt, Vector2 ext, float rad)
		{
			var ext2 = ext - new Vector2(rad, rad);
			var d = new Vector2(Math.Abs(pt.X), Math.Abs(pt.Y)) - ext2;
			var m = new Vector2(Math.Max(d.X, 0.0f), Math.Max(d.Y, 0.0f));

			return Math.Min(Math.Max(d.X, d.Y), 0.0f) + m.Length() - rad;
		}

		private static float clamp(float v)
		{
			if (v < 0.0f)
			{
				v = 0.0f;
			}

			if (v > 1.0f)
			{
				v = 1.0f;
			}

			return v;
		}

		private static Color mix(Color x, Color y, float a)
		{
			x *= (1 - a);
			y *= a;

			return new Color(x.R + y.R,
				x.G + y.G,
				x.B + y.B,
				x.A + y.A);
		}

		private void renderTriangles(ref Paint paint, 
			CompositeOperationState compositeOperation, 
			ref Scissor scissor,
			float width, float fringe, float strokeThr, 
			PrimitiveType primitiveType, 
			ArraySegment<Vertex> verts,
			RenderingType type)
		{
			if (verts.Count <= 0 || _indexes.Count <= 0)
			{
				return;
			}

			if (scissor.extent1 < -0.5f || scissor.extent2 < -0.5f)
			{
				scissorTransform.Zero();

				scissorExt.X = 1.0f;
				scissorExt.Y = 1.0f;
				scissorScale.X = 1.0f;
				scissorScale.Y = 1.0f;
			}
			else
			{
				scissorTransform = scissor.xform.BuildInverse();
				scissorExt.X = scissor.extent1;
				scissorExt.Y = scissor.extent2;
				scissorScale.X = (float)Math.Sqrt(scissor.xform.t1 * scissor.xform.t1 + scissor.xform.t3 * scissor.xform.t3) / fringe;
				scissorScale.Y = (float)Math.Sqrt(scissor.xform.t2 * scissor.xform.t2 + scissor.xform.t4 * scissor.xform.t4) / fringe;
			}

			_vertexes.Clear();
			var transform = paint.xform.BuildInverse();

			for (var i = 0; i < verts.Count; ++i)
			{
				var sourceVert = verts.Array[verts.Offset + i];
				var vert = new VertexPositionColorTexture();

				vert.Position.X = sourceVert.X;
				vert.Position.Y = sourceVert.Y;

				var sf = scissorMask(new Vector2(sourceVert.X ,sourceVert.Y));

				var strokeAlpha = 1.0f;
				if (AntiAliasingOn)
				{
					// TODO
				}

				switch (type)
				{
					case RenderingType.FILLING:
					{
						Vector2 pt;
						transform.TransformVector(out pt, new Vector2(sourceVert.X, sourceVert.Y));

						var d = clamp(sdroundrect(pt, new Vector2(paint.extent1, paint.extent2), paint.radius) + paint.feather * 0.5f);
						var col = mix(paint.innerColor, paint.outerColor, d);

						col *= strokeAlpha * sf;

						vert.Color = col;
					}
					break;
					case RenderingType.IMAGE:
					{
						Vector2 pt;
						transform.TransformVector(out pt, new Vector2(sourceVert.X, sourceVert.Y));
						pt.X /= paint.extent1;
						pt.Y /= paint.extent2;

						vert.TextureCoordinate.X = pt.X;
						vert.TextureCoordinate.Y = pt.Y;
						vert.Color = paint.innerColor * strokeAlpha * sf;
					}
					break;
					case RenderingType.TRIS:
					{

						vert.TextureCoordinate.X = sourceVert.U;
						vert.TextureCoordinate.Y = sourceVert.V;
						vert.Color = paint.innerColor * sf;
					}

					break;
				}

				_vertexes.Add(vert);
			}

			if (_VertexBuffer.VertexCount < _vertexes.Count)
			{
				// Resize vertex ArraySegment if data doesnt fit
				_VertexBuffer = new DynamicVertexBuffer(_device, VertexPositionColorTexture.VertexDeclaration, verts.Count * 2,
					BufferUsage.WriteOnly);
			}
			_VertexBuffer.SetData(_vertexes.Array, 0, _vertexes.Count);

			if (_IndexBuffer.IndexCount < _indexes.Count)
			{
				// Resize index ArraySegment if data doesnt fit
				_IndexBuffer = new DynamicIndexBuffer(_device, typeof(ushort), _indexes.Count * 2, BufferUsage.WriteOnly);
			}

			_IndexBuffer.SetData(_indexes.Array, 0, _indexes.Count);

			if (paint.image > 0)
			{
				var texture = GetTextureById(paint.image);
				basicEffect.TextureEnabled = true;
				basicEffect.Texture = texture;
			}
			else
			{
				basicEffect.TextureEnabled = false;
			}

			foreach (var pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, verts.Count, 0, _indexes.Count / 3);
			}
		}

		private void DrawBuffers(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor, 
			float width, float fringe, float strokeThr, PrimitiveType primitiveType)
		{
			// Update indexes
			_indexes.Clear();

			for (var j = 2; j < _sourceVertexes.Count; ++j)
			{
				_indexes.Add(0);
				_indexes.Add((ushort)(j - 1));
				_indexes.Add((ushort)(j));
			}

			renderTriangles(ref paint, compositeOperation, ref scissor,
				width, fringe, strokeThr,
				primitiveType,
				_sourceVertexes.ToArraySegment(),
				paint.image != 0 ? RenderingType.IMAGE : RenderingType.FILLING);

			_sourceVertexes.Clear();
		}

		public void renderFill(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor, 
			float fringe, Bounds bounds, ArraySegment<Path> paths)
		{
			var isConvex = paths.Count == 1 && paths.Array[paths.Offset].convex == 1;

			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.fill != null)
				{
					var fill = path.fill.Value;
					for (var j = 0; j < fill.Count; ++j)
					{
						_sourceVertexes.Add(fill.Array[fill.Offset + j]);
					}

					DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, PrimitiveType.TriangleList);
				}

				if (path.stroke != null && AntiAliasingOn)
				{
					var stroke = path.stroke.Value;

					for (var j = 0; j < stroke.Count; ++j)
					{
						_sourceVertexes.Add(stroke.Array[stroke.Offset + j]);
					}

					DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, PrimitiveType.TriangleList);
				}
			}

			if (isConvex)
			{
				return;
			}

/*			if (!isConvex)
			{
				_sourceVertexes.Add(new Vertex(bounds.b3, bounds.b4, 0.5f, 1.0f));
				_sourceVertexes.Add(new Vertex(bounds.b3, bounds.b2, 0.5f, 1.0f));
				_sourceVertexes.Add(new Vertex(bounds.b1, bounds.b4, 0.5f, 1.0f));
				_sourceVertexes.Add(new Vertex(bounds.b1, bounds.b2, 0.5f, 1.0f));

				DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, PrimitiveType.TriangleList);
			}*/
		}

		public void renderStroke(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor, 
			float fringe, float strokeWidth, ArraySegment<Path> paths)
		{
			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths.Array[paths.Offset + i];

				if (path.stroke != null && AntiAliasingOn)
				{
					var stroke = path.stroke.Value;

					for (var j = 0; j < stroke.Count; ++j)
					{
						_sourceVertexes.Add(stroke.Array[stroke.Offset + j]);
					}

					DrawBuffers(ref paint, compositeOperation, ref scissor, fringe, fringe, -1.0f, PrimitiveType.TriangleList);
				}
			}
		}

		public void renderTriangles(ref Paint paint, CompositeOperationState compositeOperation, 
			ref Scissor scissor, ArraySegment<Vertex> verts)
		{
			if (verts.Count <= 0)
			{
				return;
			}

			_indexes.Clear();
			for (var i = 0; i < verts.Count; ++i)
			{
				_indexes.Add((ushort)i);
			}

			renderTriangles(ref paint, compositeOperation, ref scissor, 
				1.0f, 1.0f, -1.0f,
				PrimitiveType.TriangleList, verts, RenderingType.TRIS);
		}

		private static void GetProjectionMatrix(int width, int height, out Matrix mtx)
		{
			const float L = 0.5f;
			var R = width + 0.5f;
			const float T = 0.5f;
			var B = height + 0.5f;
			mtx = new Matrix(2.0f / (R - L), 0.0f, 0.0f, 0.0f, 0.0f, 2.0f / (T - B), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
				(R + L) / (L - R), (T + B) / (B - T), 0.0f, 1.0f);
		}

		public void Begin()
		{
			basicEffect.World = Matrix.Identity;
			Matrix projection;
			GetProjectionMatrix(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height,
				out projection);
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			basicEffect.TextureEnabled = true;
			basicEffect.LightingEnabled = false;
			_device.SetVertexBuffer(_VertexBuffer);
			_device.Indices = _IndexBuffer;

			_oldSamplerState = _device.SamplerStates[0];
			_oldBlendState = _device.BlendState;
			_oldDepthStencilState = _device.DepthStencilState;
			_oldRasterizerState = _device.RasterizerState;

			_device.SamplerStates[0] = SamplerState.LinearClamp;
			_device.BlendState = BlendState.NonPremultiplied;
			_device.DepthStencilState = DepthStencilState.None;
			_device.RasterizerState = _rasterizerState;
		}

		public void End()
		{
			_device.SamplerStates[0] = _oldSamplerState;
			_device.BlendState = _oldBlendState;
			_device.DepthStencilState = _oldDepthStencilState;
			_device.RasterizerState = _oldRasterizerState;
		}
	}
}
