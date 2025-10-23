using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CQ11StarStart
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //set up our game attribute variables, we've seen these before
        private SpriteFont _gameFont;
        private Texture2D _spaceBackground;
        private Texture2D _starSprite;
        private List<Star> _starList;
        private List<Explosion> _explosionList;
        private Random _rng;
        private int _waveCounter;

        private bool _leftMouseClick;
        private Texture2D _hitBoxTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //initialize our game variables
            _waveCounter = 0;
            _starList = new List<Star>();
            _explosionList = new List<Explosion>();
            _leftMouseClick = false;


            //you only need this if you want to see the star hit boxes for debugging
            _hitBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitBoxTexture.SetData(new Color[] { Color.White });

            _rng = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our sprites and fonts
            _starSprite = Content.Load<Texture2D>("star");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");
            _gameFont = Content.Load<SpriteFont>("GameFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if there are no stars left, increase the wave counter and add that many stars to the list
            if (_starList.Count <= 0)
            {
                _waveCounter++;
                for (int i = 0; i < _waveCounter; i++)
                {
                    _starList.Add(new Star(_rng.Next(50, 751), -300, _starSprite));
                }
                
            }

            //if we have clicked and it's on a star...remove it from the list and replace it with an EXPLOSION!
            MouseState currentMouseState = Mouse.GetState();
            if (!_leftMouseClick && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _leftMouseClick = true;
                for (int i = _starList.Count - 1; i >= 0; i--)
                {
                    //put the explosion where the mouse is, you can adjust this if you want it to be centered on the star or something
                    if (_starList[i].GetBounds().Contains(currentMouseState.Position))
                    {
                        //does this seem like a good place to add an explosion?



                        _starList.RemoveAt(i);  //remove the star that was clicked
                    }
                    
                }
            }


            //reset our mouse input varibles
            if (_leftMouseClick && currentMouseState.LeftButton != ButtonState.Pressed)
                _leftMouseClick = false;

            //remove any stars that have fallen out of the scene
            for (int i = _starList.Count - 1; i >= 0; i--)
            {
                if (_starList[i].GetY() > 600)
                    _starList.RemoveAt(i);
            }

            //update all our stars 
            for (int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Update();
            }

            //update all our explosions (CQ11)


     

            //remove any explosions that are finished (CQ11)

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //show the wave counter and background
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Wave: " + _waveCounter, new Vector2(10, 10), Color.White);

            //this is just for debugging, it will show the star hit boxes if you set showBounds to true
            bool showBounds = false;
            if (showBounds)
            {
                for (int i = 0; i < _starList.Count; i++)
                    _spriteBatch.Draw(_hitBoxTexture, _starList[i].GetBounds(), Color.White);
            }

            
            _spriteBatch.End();


            //draw all our stars
            for (int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Draw(_spriteBatch);
            }


            //draw all our explosions (CQ11)
     




            base.Draw(gameTime);
        }
    }
}
