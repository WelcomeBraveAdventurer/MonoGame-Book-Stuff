using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CQ5Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //attribute variables - textures - for our scene
        private Texture2D _gremlinSprite;
        private Texture2D _consoleSprite;
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

            //load our sprites into the appropriate variables
            _gremlinSprite = Content.Load<Texture2D>("gremlin");
            _consoleSprite = Content.Load<Texture2D>("console");
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


            //begin our drawing statements
            _spriteBatch.Begin();

            //first the background
            _spriteBatch.Draw(_spaceFloorBackground, new Vector2(0, 0), null, Color.White, ((float)Math.PI / 180f) * 45f, new Vector2(_spaceFloorBackground.Width/2, _spaceFloorBackground.Height/2), 1, SpriteEffects.None, 0);

            //next the terminals
            for (int terminalX = 0; terminalX < 800; terminalX += 64)
            {
                _spriteBatch.Draw(_consoleSprite, new Vector2(terminalX, 280), null, Color.White, 0, new Vector2(0,0), new Vector2(1.5f, 0.6f), SpriteEffects.None, 0);
            }

            //now the gremlin
            _spriteBatch.Draw(_gremlinSprite, new Vector2(192, 240), null, Color.White, 0, new Vector2(0,0), 0.75f, SpriteEffects.FlipHorizontally, 0);

            //finally, the health vial
            _spriteBatch.Draw(_healthVialSprite, new Vector2(250, 200), null, Color.White, -1.3f, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
            
            //end the drawing statements
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
