using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NanoVGSharp
{
	public class XNARenderer: IRenderer
	{
		private readonly GraphicsDevice _device;
		private DynamicVertexBuffer _vertexBuffer;
		private DynamicIndexBuffer _indexBuffer;
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

		public XNARenderer(GraphicsDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}

			_device = device;

			_vertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 2000, BufferUsage.WriteOnly);
			_indexBuffer = new DynamicIndexBuffer(device, typeof(ushort), 6000, BufferUsage.WriteOnly);
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

						for (var k = 0; k < 4; ++k)
						{
							b[destPos * 4 + k] = d[destPos];
						}
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

		private void renderTriangles(ref Paint paint, 
			CompositeOperationState compositeOperation, 
			ref Scissor scissor,
			ArraySegment<VertexPositionColorTexture> verts, 
			ushort[] indexes)
		{
			if (verts.Count <= 0 || indexes.Length <= 0)
			{
				return;
			}

			for (var i = 0; i < verts.Count; ++i)
			{
				verts.Array[verts.Offset + i].Color = paint.innerColor;
			}

			if (_vertexBuffer.VertexCount < verts.Count)
			{
				// Resize vertex buffer if data doesnt fit
				_vertexBuffer = new DynamicVertexBuffer(_device, VertexPositionColorTexture.VertexDeclaration, verts.Count * 2,
					BufferUsage.WriteOnly);
			}
			_vertexBuffer.SetData(verts.Array, verts.Offset, verts.Count);

			if (_indexBuffer.IndexCount < indexes.Length)
			{
				// Resize index buffer if data doesnt fit
				_indexBuffer = new DynamicIndexBuffer(_device, typeof(ushort), indexes.Length * 2, BufferUsage.WriteOnly);
			}

			_indexBuffer.SetData(indexes, 0, indexes.Length);

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
				_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, verts.Count, 0, indexes.Length / 3);
			}
		}

		public void renderFill(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor, 
			float fringe, Bounds bounds, Buffer<Path> paths)
		{
			var vertexes = new List<VertexPositionColorTexture>();
			var indexes = new List<ushort>();

			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths[i];

				if (path.fill != null)
				{
					var index = vertexes.Count;

					var fill = path.fill.Value;
					for (var j = 0; j < fill.Count; ++j)
					{
						var v = fill.Array[fill.Offset + j];
						vertexes.Add(v);
					}

					for (var j = 2; j < fill.Count; ++j)
					{
						indexes.Add((ushort)index);
						indexes.Add((ushort)(index + j - 1));
						indexes.Add((ushort)(index + j));
					}
				}
			}

			renderTriangles(ref paint, compositeOperation, ref scissor,
				new ArraySegment<VertexPositionColorTexture>(vertexes.ToArray()),
				indexes.ToArray());
		}

		public void renderStroke(ref Paint paint, CompositeOperationState compositeOperation, ref Scissor scissor, 
			float fringe, float strokeWidth, Buffer<Path> paths)
		{
			var vertexes = new List<VertexPositionColorTexture>();
			var indexes = new List<ushort>();
			var thickness = 2.0f;
			var col = paint.innerColor;
			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths[i];
				var idx = vertexes.Count;
				if (path.stroke != null)
				{
					var index = vertexes.Count;

					var stroke = path.stroke.Value;
					for (var j = 0; j < stroke.Count; ++j)
					{
						float dx;
						float dy;
						Vector2 uv = Vector2.Zero;
						int i2 = (int)(((j + 1) == (stroke.Count)) ? 0 : j + 1);
						Vector3 p1 = stroke.Array[stroke.Offset + j].Position;
						Vector3 p2 = stroke.Array[stroke.Offset + i2].Position;
						Vector2 diff = (Vector2)(new Vector2((float)((p2).X - (p1).X), (float)((p2).Y - (p1).Y)));
						float len;
						len = (float)((diff).X * (diff).X + (diff).Y * (diff).Y);
						if (len != 0.0f)
							len = (float)(1.0f / Math.Sqrt(len));
						else
							len = (float)(1.0f);
						diff = (Vector2)(new Vector2((float)((diff).X * (len)), (float)((diff).Y * (len))));
						dx = (float)(diff.X * (thickness * 0.5f));
						dy = (float)(diff.Y * (thickness * 0.5f));

						vertexes.Add(new VertexPositionColorTexture
						{
							Position = new Vector3((float)(p1.X + dy), (float)(p1.Y - dx), 1.0f),
							TextureCoordinate = uv,
							Color = col
						});

						vertexes.Add(new VertexPositionColorTexture
						{
							Position = new Vector3((float)(p2.X + dy), (float)(p2.Y - dx), 1.0f),
							TextureCoordinate = uv,
							Color = col
						});

						vertexes.Add(new VertexPositionColorTexture
						{
							Position = new Vector3((float)(p2.X - dy), (float)(p2.Y + dx), 1.0f),
							TextureCoordinate = uv,
							Color = col
						});

						vertexes.Add(new VertexPositionColorTexture
						{
							Position = new Vector3((float)(p1.X - dy), (float)(p1.Y + dx), 1.0f),
							TextureCoordinate = uv,
							Color = col
						});

						indexes.Add((ushort)(idx + 0));
						indexes.Add((ushort)(idx + 1));
						indexes.Add((ushort)(idx + 2));
						indexes.Add((ushort)(idx + 0));
						indexes.Add((ushort)(idx + 2));
						indexes.Add((ushort)(idx + 3));
						idx += (int)(4);
					}
				}
			}

			renderTriangles(ref paint, compositeOperation, ref scissor,
				new ArraySegment<VertexPositionColorTexture>(vertexes.ToArray()),
				indexes.ToArray());
		}

		public void renderTriangles(ref Paint paint, CompositeOperationState compositeOperation, 
			ref Scissor scissor, ArraySegment<VertexPositionColorTexture> verts)
		{
			if (verts.Count <= 0)
			{
				return;
			}

			var indexes = new ushort[verts.Count];
			for (var i = 0; i < indexes.Length; ++i)
			{
				indexes[i] = (ushort)i;
			}

			renderTriangles(ref paint, compositeOperation, ref scissor, verts, indexes);
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
			_device.SetVertexBuffer(_vertexBuffer);
			_device.Indices = _indexBuffer;

			_oldSamplerState = _device.SamplerStates[0];
			_oldBlendState = _device.BlendState;
			_oldDepthStencilState = _device.DepthStencilState;
			_oldRasterizerState = _device.RasterizerState;

			_device.SamplerStates[0] = SamplerState.LinearClamp;
			_device.BlendState = BlendState.AlphaBlend;
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
