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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _gremlinSprite = Content.Load<Texture2D>("gremlin");
            _consoleSprite = Content.Load<Texture2D>("console");
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


            //_spriteBatch.Draw(_heroSprite, new Vector2(700, 100), null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(_spaceFloorBackground, new Vector2(0, 0), null, Color.White, ((float)Math.PI / 180f) * 45f, new Vector2(_spaceFloorBackground.Width/2, _spaceFloorBackground.Height/2), 1, SpriteEffects.None, 0);

            for (int crateX = 0; crateX < 800; crateX += 64)
            {
                _spriteBatch.Draw(_consoleSprite, new Vector2(crateX, 280), null, Color.White, 0, new Vector2(0,0), new Vector2(1.5f, 0.6f), SpriteEffects.None, 0);
            }

            _spriteBatch.Draw(_gremlinSprite, new Vector2(192, 240), null, Color.White, 0, new Vector2(0,0), 0.75f, SpriteEffects.FlipHorizontally, 0);

            _spriteBatch.Draw(_healthVialSprite, new Vector2(250, 200), null, Color.White, -1.3f, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
            

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
