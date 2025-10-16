using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CQ6Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //variables for the hero sprite, the hero coordinates,
        //and hero movement tracking
        private Texture2D _heroSprite;
        private float _heroX, _heroY;
        private bool _heroMoveRight, _heroMoveDown;

        //variables for the game font and score
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
            _heroX = 350;
            _heroY = 200;
            //let’s convince our hero to move right by default
            _heroMoveRight = true;
            _heroMoveDown = true;
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

            //if we reach the bottom “edge” of the scene, we no longer move down
            if (_heroY >= 420)
            {
                _heroMoveDown = false;
                _gameScore += 1;
            }
            //if we reach the top “edge” of the scene, we move down again
            if (_heroY <= 0)
            {
                _heroMoveDown = true;
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

            //move down
            if (_heroMoveDown == true)
            {
                //move down 3 pixels each timestep
                _heroY += 3;
            }
            //move up
            if (_heroMoveDown == false)
            {
                //move up 3 pixels each timestep
                _heroY -= 3;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //start our drawing statements
            _spriteBatch.Begin();

            //if the hero is moving right (this just controls the way the hero is facing)
            if (_heroMoveRight == true)
                _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, Color.White,
                                      0f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
            else //the hero is moving left so flip the sprite
                _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, Color.White,
                                      0f, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0f);

            //print the silly scorel to the scene
            _spriteBatch.DrawString(_gameFont, "Score: " + _gameScore, new Vector2(600, 20), Color.White);

            //this ends our drawing statements
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
