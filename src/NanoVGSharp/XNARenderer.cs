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

		public int renderCreateTexture(int type, int w, int h, int imageFlags, byte[] d)
		{
			var texture = new Texture2D(_device, w, h);

			if (d != null)
			{
				texture.SetData(d);
			}

			_textures.Add(texture);

			return _textures.Count - 1;
		}

		public void renderDeleteTexture(int image)
		{
			throw new NotImplementedException();
		}

		public void renderUpdateTexture(int image, int x, int y, int w, int h, byte[] d)
		{
			var texture = _textures[image];

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
			var texture = _textures[image];

			w = texture.Width;
			h = texture.Height;
		}

		public void renderViewport(float width, float height, float devicePixelRatio)
		{
			// TO DO
		}

		public void renderFill(ref NVGpaint paint, NVGcompositeOperationState compositeOperation, ref NVGscissor scissor, 
			float fringe, Bounds bounds, Buffer<NVGpath> paths)
		{
			for (var i = 0; i < paths.Count; ++i)
			{
				var path = paths[i];

				var k = 5;
			}
		}

		public void renderStroke(ref NVGpaint paint, NVGcompositeOperationState compositeOperation, ref NVGscissor scissor, 
			float fringe, float strokeWidth, Buffer<NVGpath> paths)
		{
			throw new NotImplementedException();
		}

		public void renderTriangles(ref NVGpaint paint, NVGcompositeOperationState compositeOperation, ref NVGscissor scissor, ArraySegment<VertexPositionColorTexture> verts)
		{
			if (verts.Count <= 0)
			{
				return;
			}

			for (var i = 0; i < verts.Count; ++i)
			{
				verts.Array[verts.Offset + i].Color = Color.White;
			}

			if (_vertexBuffer.VertexCount < verts.Count)
			{
				// Resize vertex buffer if data doesnt fit
				_vertexBuffer = new DynamicVertexBuffer(_device, VertexPositionColorTexture.VertexDeclaration, verts.Count * 2,
					BufferUsage.WriteOnly);
			}
			_vertexBuffer.SetData(verts.Array, verts.Offset, verts.Count);

			var indexes = new ushort[verts.Count];
			for (var i = 0; i < indexes.Length; ++i)
			{
				indexes[i] = (ushort)i;
			}

			if (_indexBuffer.IndexCount < indexes.Length)
			{
				// Resize index buffer if data doesnt fit
				_indexBuffer = new DynamicIndexBuffer(_device, typeof(ushort), indexes.Length * 2, BufferUsage.WriteOnly);
			}

			_indexBuffer.SetData(indexes, 0, indexes.Length);

			var texture = _textures[paint.image];

			basicEffect.TextureEnabled = true;
			basicEffect.Texture = texture;

			foreach (var pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, verts.Count / 3);
			}
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
