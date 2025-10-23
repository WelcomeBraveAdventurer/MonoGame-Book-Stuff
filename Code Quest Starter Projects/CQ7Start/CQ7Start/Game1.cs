using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CQ7Start
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

        //declare our gremlin's variables
        private Texture2D _gremlinSprite;
        private float _gremlinX, _gremlinY;
        private float _gremlinSpeed;
        private float _gremlinAngle, _gremlinRotationSpeed;
        private Color _gremlinColor;

        //declare our star's variables
        private Texture2D _starSprite;
        private float _starX, _starY;
        private float _starAngle;
        private Color _starColor;

        //these variables will help with our input
        private bool _mouseLeftClicked, _mouseRightClicked, _starDragging;


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

            //and the gremlin variables to set up here
            _gremlinX = 600;
            _gremlinY = 100;
            _gremlinSpeed = 5;
            _gremlinAngle = 0;
            _gremlinRotationSpeed = 0.25f;
            _gremlinColor = Color.White;


            //star variables to set up
            _starX = 400;
            _starY = 300;
            _starColor = Color.White;
            _starAngle = 0;




            //these will help us to control some of our input
            _mouseLeftClicked = false;
            _mouseRightClicked = false;
            _starDragging = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our hero texture
            _heroSprite = Content.Load<Texture2D>("heroGray");
            _gremlinSprite = Content.Load<Texture2D>("gremlinGray");
            _starSprite = Content.Load<Texture2D>("star");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            //Keyboard Hero! (CQ7)
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


            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                Random rng = new Random();
                _heroColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            //CQ7 - don't forget to reset the space bar press when you've writtent the single press logic




            //Gamepad Gremlin!!! (CQ7)
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentGamePadState.IsButtonDown(Buttons.A))
            {
                Random rng = new Random();
                _gremlinColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            float horizontalMovement = currentGamePadState.ThumbSticks.Left.X;
            _gremlinX += horizontalMovement * _heroSpeed;
            float verticalMovement = currentGamePadState.ThumbSticks.Left.Y;
            _gremlinY -= verticalMovement * _heroSpeed;

            _gremlinAngle += currentGamePadState.Triggers.Left * _heroRotationSpeed;
            _gremlinAngle -= currentGamePadState.Triggers.Right * _heroRotationSpeed;



            //Mouse star! (CQ7)

            MouseState currentMouseState = Mouse.GetState();

            if (!_mouseLeftClicked && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _mouseLeftClicked = true;

                if (_starDragging == false)
                    _starDragging = true;
                else
                    _starDragging = false;
            }

            if (_starDragging == true && currentMouseState.LeftButton != ButtonState.Pressed)
            {
                if (currentMouseState.X < 0)
                    _starX = 0;
                else if (currentMouseState.X > 750)
                    _starX = 750;
                else
                    _starX = Mouse.GetState().X;

                if (currentMouseState.Y < 0)
                    _starY = 0;
                else if (currentMouseState.Y > 430)
                    _starY = 430;
                else
                    _starY = currentMouseState.Y;
            }

            if (!_mouseRightClicked && currentMouseState.RightButton == ButtonState.Pressed)
            {
                _mouseRightClicked = true;
                Random rng = new Random();
                _starColor = new Color(rng.Next(128, 255), rng.Next(128, 255), rng.Next(128, 255));
            }

            if (currentMouseState.LeftButton != ButtonState.Pressed)
            {
                _mouseLeftClicked = false;
            }

            if (currentMouseState.RightButton != ButtonState.Pressed)
            {
                _mouseRightClicked = false;
            }




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            
            //draw the hero!
            _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, _heroColor, _heroAngle, new Vector2(_heroSprite.Width / 2, _heroSprite.Height / 2), 1, SpriteEffects.None, 0);

            //draw the gremlin!
            _spriteBatch.Draw(_gremlinSprite, new Vector2(_gremlinX, _gremlinY), null, _gremlinColor, _gremlinAngle, new Vector2(_gremlinSprite.Width / 2, _gremlinSprite.Height / 2), 1, SpriteEffects.None, 0);

            //draw the star!
            _spriteBatch.Draw(_starSprite, new Vector2(_starX, _starY), null, _starColor, _starAngle, new Vector2(_starSprite.Width / 2, _starSprite.Height / 2), 1, SpriteEffects.None, 0);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
