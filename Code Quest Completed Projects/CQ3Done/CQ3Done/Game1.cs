using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace CQ3Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //here are the sprite font variables that we'll need
        private SpriteFont _quoteFont1, _quoteFont2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load our sprite fonts and assign them to the variables we declared above
            _quoteFont1 = Content.Load<SpriteFont>("QuoteFont1");
            _quoteFont2 = Content.Load<SpriteFont>("QuoteFont2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //this begins our drawing statements
            _spriteBatch.Begin();

            //draw our game-y strings using the sprite fonts we loaded and assigned
            _spriteBatch.DrawString(_quoteFont1, "It's dangerous to go alone! Take this.", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_quoteFont2, "The cake is a lie!", new Vector2(100,200), Color.PeachPuff);
            _spriteBatch.DrawString(_quoteFont2, "Stay awhile, and listen!", new Vector2(250, 50), Color.Black);
            _spriteBatch.DrawString(_quoteFont1, "Thank you Mario! But our Princess is in another castle!", new Vector2(50, 400), Color.RoyalBlue);

            //this ends our drawing statements
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
