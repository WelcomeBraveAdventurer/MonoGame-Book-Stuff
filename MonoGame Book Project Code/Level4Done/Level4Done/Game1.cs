using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Level4Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //attribute variables - textures - for our scene
        private Texture2D _heroSprite;
        private Texture2D _crateSprite;
        private Texture2D _coinSprite;
        private Texture2D _spaceBackground;

        //attribute variables - fonts - for our scene
        private SpriteFont _titleFont, _labelFont;


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

            // TODO: use this.Content to load your game content here

            _heroSprite = Content.Load<Texture2D>("hero");
            _crateSprite = Content.Load<Texture2D>("crate");
            _coinSprite = Content.Load<Texture2D>("coin");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");


            _titleFont = Content.Load<SpriteFont>("TitleFont");
            _labelFont = Content.Load<SpriteFont>("LabelFont");

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

            // TODO: Add your drawing code here



            _spriteBatch.Begin();

            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_labelFont, "Background", new Vector2(25, 25), Color.White);


            for (int crateX = 0; crateX < 800; crateX += 64)
            {
                _spriteBatch.Draw(_crateSprite, new Vector2(crateX, 280), Color.SteelBlue);
                _spriteBatch.DrawString(_labelFont, "Crate", new Vector2(crateX + 10, 340), Color.SteelBlue);
            }

            _spriteBatch.Draw(_heroSprite, new Vector2(192, 240), Color.Orchid);
            _spriteBatch.DrawString(_labelFont, "Hero", new Vector2(200, 220), Color.Orchid);


            _spriteBatch.Draw(_coinSprite, new Vector2(250, 200), Color.White);
            _spriteBatch.DrawString(_labelFont, "Coin", new Vector2(260, 190), Color.White);

            _spriteBatch.DrawString(_titleFont, "Welcome, brave adventurer!", new Vector2(160, 100), Color.LightGoldenrodYellow);


            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
