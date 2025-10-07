using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Level7Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //declare our scene’s attribute variables
        private Texture2D _heroSprite;
        private float _heroX, _heroY;
        private float _heroSpeed;
        private float _heroAngle, _heroRotationSpeed;
        private Color _heroColor;

        //we need some varaibles for our mouse input
        private bool _mouseLeftClicked, _mouseRightClicked, _heroDragging;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _heroX = 100;
            _heroY = 100;
            _heroSpeed = 5;
            _heroAngle = 0;
            _heroRotationSpeed = 0.25f;
            _heroColor = Color.White;

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


            //we can move our hero with the keyboard - this code works for WASD and arrow keys
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


            //change color when spacebar is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            //we can also move our hero with the mouse
            MouseState currentMouseState = Mouse.GetState();

            //if the left mouse button is clicked, toggle dragging mode on or off
            if (!_mouseLeftClicked && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _mouseLeftClicked = true;

                if (_heroDragging == false)
                    _heroDragging = true;
                else
                    _heroDragging = false;
            }

            //if the left mouse button is released, turn off the left-click flag
            if (currentMouseState.LeftButton != ButtonState.Pressed)
            {
                _mouseLeftClicked = false;
            }

            //if we are in dragging mode, move the hero to the mouse position
            if (_heroDragging == true && currentMouseState.LeftButton != ButtonState.Pressed)
            {
                if (currentMouseState.X < 0)
                    _heroX = 0;                     //keep the hero in the window
                else if (currentMouseState.X > 750)
                    _heroX = 750;                   //keep the hero in the window
                else
                    _heroX = Mouse.GetState().X;

                if (currentMouseState.Y < 0)
                    _heroY = 0;                     //keep the hero in the window
                else if (currentMouseState.Y > 430)
                    _heroY = 430;                   //keep the hero in the window
                else
                    _heroY = currentMouseState.Y;
            }

            //if the right mouse button is clicked, change the hero color
            if (!_mouseRightClicked && currentMouseState.RightButton == ButtonState.Pressed)
            {
                _mouseRightClicked = true;
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            //if the right mouse button is released, turn off the right-click flag
            if (currentMouseState.RightButton != ButtonState.Pressed)
            {
                _mouseRightClicked = false;
            }


            //we can also move our hero with a gamepad
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            //change color when A button is pressed
            if (currentGamePadState.IsButtonDown(Buttons.A))
            {
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            //use the left thumbstick to move the hero
            float horizontalMovement = currentGamePadState.ThumbSticks.Left.X;
            _heroX += horizontalMovement * _heroSpeed;
            float verticalMovement = currentGamePadState.ThumbSticks.Left.Y;
            _heroY -= verticalMovement * _heroSpeed;

            //use the left and right triggers to rotate the hero
            _heroAngle += currentGamePadState.Triggers.Left * _heroRotationSpeed;
            _heroAngle -= currentGamePadState.Triggers.Right * _heroRotationSpeed;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //draw the hero - the hero will change colours and positions based on the input above
            _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, _heroColor, _heroAngle,
                              new Vector2(_heroSprite.Width / 2, _heroSprite.Height / 2), 1,
                              SpriteEffects.None, 0);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
