using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CQ4Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //attribute variables - textures - for our scene
        private Texture2D _gremlinSprite;
        private Texture2D _computerTerminalSprite;
        private Texture2D _healthVialSprite;
        private Texture2D _spaceFloorBackground;

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

            _gremlinSprite = Content.Load<Texture2D>("gremlin");
            _computerTerminalSprite = Content.Load<Texture2D>("console");
            _healthVialSprite = Content.Load<Texture2D>("healthVial");
            _spaceFloorBackground = Content.Load<Texture2D>("spaceFloorBackground");

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

            _spriteBatch.Draw(_spaceFloorBackground, new Vector2(0, 0), Color.White);

            for (int crateX = 0; crateX < 800; crateX += 64)
            {
                _spriteBatch.Draw(_computerTerminalSprite, new Vector2(crateX, 280), Color.White);
            }

            _spriteBatch.Draw(_gremlinSprite, new Vector2(192, 240), Color.White);

            _spriteBatch.Draw(_healthVialSprite, new Vector2(250, 200), Color.White);

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
