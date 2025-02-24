using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CQ3Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _quoteFont1, _quoteFont2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _quoteFont1 = Content.Load<SpriteFont>("QuoteFont1");
            _quoteFont2 = Content.Load<SpriteFont>("QuoteFont2");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_quoteFont1, "It's dangerous to go alone! Take this.", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_quoteFont2, "The cake is a lie!", new Vector2(100,200), Color.PeachPuff);
            _spriteBatch.DrawString(_quoteFont2, "Stay awhile, and listen!", new Vector2(250, 50), Color.Black);
            _spriteBatch.DrawString(_quoteFont1, "Thank you Mario! But our Princess is in another castle!", new Vector2(50, 400), Color.RoyalBlue);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
