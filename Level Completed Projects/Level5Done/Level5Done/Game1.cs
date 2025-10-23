using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Level5Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //all we need for this level is a sprite and a font!
        private Texture2D _heroSprite;
        private SpriteFont _gameFont;

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

            //load our sprites and assign them to variables!
            _heroSprite = Content.Load<Texture2D>("hero");
            _gameFont = Content.Load<SpriteFont>("GameFont");
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


            //since this example is more complex, there is some commented out code below, remove 
            //the comments to see what the code does!

            _spriteBatch.Begin();
            /*
            _spriteBatch.Draw(_heroSprite, new Vector2(100, 100), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(200, 100), null, Color.White, 0.76f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(300, 100), null, Color.White, 1.57f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(400, 100), null, Color.White, 3.14f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(500, 100), null, Color.White, 6.28f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            
            int centerX = _heroSprite.Width / 2;    //width of our sprite divided by 2
            int centerY = _heroSprite.Height / 2;   //heigh of our sprite divided by 2

            _spriteBatch.Draw(_heroSprite, new Vector2(100, 200), null, Color.White,
                        0f, new Vector2(centerX, centerY), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(200, 200), null, Color.White,
                        0.76f, new Vector2(centerX, centerY), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(300, 200), null, Color.White,
                        1.57f, new Vector2(centerX, centerY), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(400, 200), null, Color.White,
                        3.14f, new Vector2(centerX, centerY), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(500, 200), null, Color.White,
                        6.28f, new Vector2(centerX, centerY), 1f, SpriteEffects.None, 0f);


            
            _spriteBatch.Draw(_heroSprite, new Vector2(100, 300), null, Color.White, 0, new Vector2(0, 0),
                              0.25f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(200, 300), null, Color.White, 0, new Vector2(0, 0),
                              0.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(300, 300), null, Color.White, 0, new Vector2(0, 0),
                              1.0f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(400, 300), null, Color.White, 0, new Vector2(0, 0),
                              2.0f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(500, 300), null, Color.White, 0, new Vector2(0, 0),
                              3.5f, SpriteEffects.None, 0f);
            */
            
            _spriteBatch.Draw(_heroSprite, new Vector2(700, 100), null, Color.White, 0, new Vector2(0, 0),
                  1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(700, 200), null, Color.White, 0, new Vector2(0, 0),
                              1f, SpriteEffects.FlipHorizontally, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(700, 300), null, Color.White, 0, new Vector2(0, 0),
                              1f, SpriteEffects.FlipVertically, 0f);
            _spriteBatch.Draw(_heroSprite, new Vector2(700, 400), null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0f);


            /*
            _spriteBatch.DrawString(
            _gameFont,                       //SpriteFont to use
            "Hello World!",                  //text to display
            new Vector2(300, -50),           //position in the scene
            Color.Yellow,                    //color for blending
            1.2f,                            //rotation
            new Vector2(0, 0),                //center of rotation
            2.5f,                            //scale factor		
            SpriteEffects.FlipHorizontally,  //sprite effects for flipping
            0f);                             //layer depth    

            */

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
