using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Level6Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //for this example, we need to track our hero’s texture, position, and movement direction
        private Texture2D _heroSprite;
        private float _heroX, _heroY;
        private bool _heroMoveRight;

        //we also need to track our score and display it with a font
        private SpriteFont _gameFont;
        private int _gameScore;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {

            //let’s set our hero near the middle of the scene
            _heroX = 125;
            _heroY = 100;
            //let’s convince our hero to move right by default
            _heroMoveRight = true;
            //set our starting score
            _gameScore = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Remember to load your texture using the MGCB
            _heroSprite = Content.Load<Texture2D>("hero");
            _gameFont = Content.Load<SpriteFont>("GameFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if we reach the right “edge” of the scene, we no longer move right
            if (_heroX >= 735)
            {
                _heroMoveRight = false;
                _gameScore += 1;
            }
            //if we reach the left “edge” of the scene, we move right again
            if (_heroX <= 0)
            {
                _heroMoveRight = true;
                _gameScore += 1;
            }
            //move right
            if (_heroMoveRight == true)
            {
                //move to the right 3 pixels each timestep
                _heroX += 3;
            }
            //move left
            if (_heroMoveRight == false)
            {
                //move to the left 3 pixels each timestep
                _heroX -= 3;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //this is the old Draw statement, commented out but left for reference
            //_spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), Color.White);

            if (_heroMoveRight == true)
                _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, Color.White,
                                      0f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
            else
                _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, Color.White,
                                      0f, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0f);

            _spriteBatch.DrawString(_gameFont, "Score: " + _gameScore, new Vector2(600, 20), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
