using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CQ5Start
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //assign all of our textures to their corresponding variables.
            _gremlinSprite = Content.Load<Texture2D>("gremlin");
            _computerTerminalSprite = Content.Load<Texture2D>("console");
            _healthVialSprite = Content.Load<Texture2D>("healthVial");
            _spaceFloorBackground = Content.Load<Texture2D>("spaceFloorBackground");

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

            //here starts the drawing of the scene
            _spriteBatch.Begin();

            //draw the background first
            _spriteBatch.Draw(_spaceFloorBackground, new Vector2(0, 0), Color.White);

            //loop through the terminals to draw a full row across the scene
            for (int terminalX = 0; terminalX < 800; terminalX += 64)
            {
                _spriteBatch.Draw(_computerTerminalSprite, new Vector2(terminalX, 280), Color.White);
            }

            //draw our space gremlin and our health vial
            _spriteBatch.Draw(_gremlinSprite, new Vector2(192, 240), Color.White);
            _spriteBatch.Draw(_healthVialSprite, new Vector2(250, 200), Color.White);

            //this concludes our drawing
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
