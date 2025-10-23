using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Level8Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //textures for our scene objects
        private Texture2D _coinTexture, _chestOpenTexture, _chestClosedTexture, _heroTexture;
        
        //variables for the objects themselves.
        private Hero _hero;
        private Chest _chest;
        private Coin _coin;

        //a handy font, just in case
        private SpriteFont _gameFont;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //there is nothing for us to do in here.

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the coin texture into its variable
            _coinTexture = Content.Load<Texture2D>("coin");
            //create the coin object at x = 100, y = 100, with a value of 1 and using the coin texture.
            _coin = new Coin(100, 100, 1, _coinTexture);

            //load the two chest textures - open and closed
            _chestOpenTexture = Content.Load<Texture2D>("chestOpen");
            _chestClosedTexture = Content.Load<Texture2D>("chestClosed");

            //create the chest at x = 200, y = 100, and using the two textures
            _chest = new Chest(200, 100, _chestOpenTexture, _chestClosedTexture);

            //load the hero texture
            _heroTexture = Content.Load<Texture2D>("hero");
            //create the hero at x = 300, y = 100, with 10 health, 5 speed and the hero texture
            _hero = new Hero(300, 100, 10, 5, _heroTexture);

            //and don't forget the font
            _gameFont = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if we don't call our chest and hero Update() methods, they won't do anything.
            _hero.Update();
            _chest.Update();
            
            //just a simple healing press of the space bar
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                _hero.Heal(1);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //if we don't call our object Draw() methods, we won't see them. Notice
            //there is no Begin() or End() here - those are contained in the object Draw() methods.
            _coin.Draw(_spriteBatch);
            _chest.Draw(_spriteBatch);
            _hero.Draw(_spriteBatch);

            //show some extra information by calling the object accessor methods. Since this is separate
            //from the object Draw() methods, we need a Begin() and End() here.
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_gameFont, "Hero health: " + _hero.GetHealth(),
                              new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_gameFont, "Chest is closed: " + _chest.IsClosed(), new Vector2(300, 10), Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
