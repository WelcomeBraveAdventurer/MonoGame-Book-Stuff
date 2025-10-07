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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our textures to their variables
            _heroSprite = Content.Load<Texture2D>("hero");
            _crateSprite = Content.Load<Texture2D>("crate");
            _coinSprite = Content.Load<Texture2D>("coin");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");

            //load and assign our fonts to their variables
            _titleFont = Content.Load<SpriteFont>("TitleFont");
            _labelFont = Content.Load<SpriteFont>("LabelFont");

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

            //begin our drawing statements
            _spriteBatch.Begin();

            //draw our background first
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);

            //draw the background label
            _spriteBatch.DrawString(_labelFont, "Background", new Vector2(25, 25), Color.White);

            //draw a row of crates and labels
            for (int crateX = 0; crateX < 800; crateX += 64)
            {
                _spriteBatch.Draw(_crateSprite, new Vector2(crateX, 280), Color.SteelBlue);
                _spriteBatch.DrawString(_labelFont, "Crate", new Vector2(crateX + 10, 340), Color.SteelBlue);
            }

            //draw the hero and coin and their respective labels
            _spriteBatch.Draw(_heroSprite, new Vector2(192, 240), Color.Orchid);
            _spriteBatch.DrawString(_labelFont, "Hero", new Vector2(200, 220), Color.Orchid);

            _spriteBatch.Draw(_coinSprite, new Vector2(250, 200), Color.White);
            _spriteBatch.DrawString(_labelFont, "Coin", new Vector2(260, 190), Color.White);

            //draw the title text last so it's on top
            _spriteBatch.DrawString(_titleFont, "Welcome, brave adventurer!", new Vector2(160, 100), Color.LightGoldenrodYellow);


            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
