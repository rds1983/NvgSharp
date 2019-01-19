using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NanoVGSharp;
using System.IO;

namespace SpriteFontPlus.Samples.TtfBaking
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public unsafe class Game1 : Game
	{
		GraphicsDeviceManager _graphics;

		private NanoVGContext _context;
		private SpriteBatch _spriteBatch;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
			};

			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			Window.AllowUserResizing = true;
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			var device = GraphicsDevice;

			_context = new NanoVGContext(GraphicsDevice, 0);

			byte[] bytes;
			using (var ms = new MemoryStream())
			{
				using (var stream = TitleContainer.OpenStream("Fonts/Roboto-Regular.ttf"))
				{
					stream.CopyTo(ms);
				}

				bytes = ms.ToArray();
			}

			_context.nvgCreateFontMem("Roboto Regular", bytes, 0);

			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(new Color(0.3f, 0.3f, 0.3f));

			// TODO: Add your drawing code here

			_context.nvgBeginFrame(800, 600, 1.0f);
			_context.nvgFontSize(32);

			_context.nvgBeginPath();
			_context.nvgRect(0, 20, 200, 100);
			_context.nvgFillColor(Color.BlueViolet);
			_context.nvgFill();

			_context.nvgText(0, 50, "The quick brown fox jumps over the lazy dog");
			_context.nvgEndFrame();

/*			var texture = ((XNARenderer)_context._renderer)._textures[0];

			_spriteBatch.Begin();
			_spriteBatch.Draw(texture, Vector2.Zero, Color.White);
			_spriteBatch.End();*/

			base.Draw(gameTime);
		}
	}
}