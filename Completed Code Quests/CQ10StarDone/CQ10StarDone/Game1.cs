using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CQ10StarDone
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //attribute variables for our textures and sprites
        private SpriteFont _gameFont;
        private Texture2D _spaceBackground;
        private Texture2D _starSprite;

        //a list to store our stars
        private List<Star> _starList;

        //a random number generator to help with star placement
        private Random _rng;

        //a wave counter to keep track of how many waves have passed
        private int _waveCounter;

        //to help with mosut input.
        private bool _leftMouseClick;

        //a texture to help us see the hit boxes of our stars
        private Texture2D _hitBoxTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //set our starting values, and create our star list
            _waveCounter = 0;
            _starList = new List<Star>();
            _leftMouseClick = false;

            //create a texture in case we want to see our star hit boxes
            _hitBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitBoxTexture.SetData(new Color[] { Color.White });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our textures and fonts
            _starSprite = Content.Load<Texture2D>("star");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");
            _gameFont = Content.Load<SpriteFont>("GameFont");

            //initialize our random number generator
            _rng = new Random();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //if there are no stars, increase the wave counter and add a new wave of stars
            if (_starList.Count <= 0)
            {
                _waveCounter++;
                for (int i = 0; i < _waveCounter; i++)
                {
                    //add waveCounter number of stars at random X locations
                    _starList.Add(new Star(_rng.Next(50, 751), -300, _starSprite));
                }
                
            }

            //check for mouse input to see if we are clicking on any stars
            MouseState currentMouseState = Mouse.GetState();
            if (!_leftMouseClick && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _leftMouseClick = true;  //we clicked on a star! Remove it from the list.
                for (int i = _starList.Count - 1; i >= 0; i--)
                {
                    if (_starList[i].GetBounds().Contains(currentMouseState.Position))
                        _starList.RemoveAt(i);
                }
            }

            //if stars fall below the scene we need to remove them from the list
            for (int i = _starList.Count - 1; i >= 0; i--)
            {
                if (_starList[i].GetY() > 600)
                    _starList.RemoveAt(i);
            }

            //reset our mouse input variable
            if (_leftMouseClick && currentMouseState.LeftButton != ButtonState.Pressed)
                _leftMouseClick = false;


            //update all our stars
            for (int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //draw the background and wave counter first
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Wave: " + _waveCounter, new Vector2(10, 10), Color.White);

            //if you want to see the star hit boxes, set this to true
            bool showBounds = false;
            if (showBounds)
            {
                for (int i = 0; i < _starList.Count; i++)
                    _spriteBatch.Draw(_hitBoxTexture, _starList[i].GetBounds(), Color.White);
            }
            _spriteBatch.End();

            //draw all of our stars to the scene
            for(int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
