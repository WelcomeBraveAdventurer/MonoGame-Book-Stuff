using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CQ7Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //declare our hero's variables
        private Texture2D _heroSprite;
        private float _heroX, _heroY;
        private float _heroSpeed;
        private float _heroAngle, _heroRotationSpeed;
        private Color _heroColor;

        //these variables will help with our input
        private bool _mouseLeftClicked, _mouseRightClicked, _heroDragging, _spaceBarPressed;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            //lots of hero variables to set up here
            _heroX = 100;
            _heroY = 100;
            _heroSpeed = 5;
            _heroAngle = 0;
            _heroRotationSpeed = 0.25f;
            _heroColor = Color.White;

            //these will help us to control some of our input
            _spaceBarPressed = false;
            _mouseLeftClicked = false;
            _mouseRightClicked = false;
            _heroDragging = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our hero texture
            _heroSprite = Content.Load<Texture2D>("heroGray");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            KeyboardState currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _heroX -= _heroSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _heroX += _heroSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W) || currentKeyboardState.IsKeyDown(Keys.Up))
            {
                _heroY -= _heroSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) || currentKeyboardState.IsKeyDown(Keys.Down))
            {
                _heroY += _heroSpeed;
            }


            if (currentKeyboardState.IsKeyDown(Keys.Space) && !_spaceBarPressed)
            {
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
                _spaceBarPressed = true;
            }

            if (!currentKeyboardState.IsKeyDown(Keys.Space))
            {
                _spaceBarPressed = false;
            }


            MouseState currentMouseState = Mouse.GetState();

            if (!_mouseLeftClicked && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _mouseLeftClicked = true;

                if (_heroDragging == false)
                    _heroDragging = true;
                else
                    _heroDragging = false;
            }

            if (currentMouseState.LeftButton != ButtonState.Pressed)
            {
                _mouseLeftClicked = false;
            }

            if (_heroDragging == true && currentMouseState.LeftButton != ButtonState.Pressed)
            {
                if (currentMouseState.X < 0)
                    _heroX = 0;
                else if (currentMouseState.X > 750)
                    _heroX = 750;
                else
                    _heroX = Mouse.GetState().X;

                if (currentMouseState.Y < 0)
                    _heroY = 0;
                else if (currentMouseState.Y > 430)
                    _heroY = 430;
                else
                    _heroY = currentMouseState.Y;
            }

            if (!_mouseRightClicked && currentMouseState.RightButton == ButtonState.Pressed)
            {
                _mouseRightClicked = true;
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            if (currentMouseState.RightButton != ButtonState.Pressed)
            {
                _mouseRightClicked = false;
            }


            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentGamePadState.IsButtonDown(Buttons.A))
            {
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            float horizontalMovement = currentGamePadState.ThumbSticks.Left.X;
            _heroX += horizontalMovement * _heroSpeed;
            float verticalMovement = currentGamePadState.ThumbSticks.Left.Y;
            _heroY -= verticalMovement * _heroSpeed;

            _heroAngle += currentGamePadState.Triggers.Left * _heroRotationSpeed;
            _heroAngle -= currentGamePadState.Triggers.Right * _heroRotationSpeed;




            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, _heroColor, _heroAngle,
                              new Vector2(_heroSprite.Width / 2, _heroSprite.Height / 2), 1,
                              SpriteEffects.None, 0);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
