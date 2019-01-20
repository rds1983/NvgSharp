using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NanoVGSharp.Samples.Demo
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public unsafe class Game1 : Game
	{
		GraphicsDeviceManager _graphics;

		private NanoVGContext _context;
		private SpriteBatch _spriteBatch;
		private Demo _demo;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1000,
				PreferredBackBufferHeight = 600
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

			_demo = new Demo();
			_demo.loadDemoData(GraphicsDevice, _context);

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

			if (_graphics.PreferredBackBufferWidth != Window.ClientBounds.Width ||
				_graphics.PreferredBackBufferHeight != Window.ClientBounds.Height)
			{
				_graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
				_graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
				_graphics.ApplyChanges();
			}

			// TODO: Add your drawing code here

			var mouseState = Mouse.GetState();

			_context.nvgBeginFrame(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, 1.0f);

			var t = (float)gameTime.TotalGameTime.TotalSeconds;
			_demo.renderDemo(_context, 
				mouseState.X, 
				mouseState.Y, 
				_graphics.PreferredBackBufferWidth, 
				_graphics.PreferredBackBufferHeight, 
				t, 
				false);

			_context.nvgEndFrame();

/*			var texture = ((XNARenderer)_context._renderer)._textures[8];

			_spriteBatch.Begin();
			_spriteBatch.Draw(texture, Vector2.Zero, Color.White);
			_spriteBatch.End();*/

			base.Draw(gameTime);
		}
	}
}