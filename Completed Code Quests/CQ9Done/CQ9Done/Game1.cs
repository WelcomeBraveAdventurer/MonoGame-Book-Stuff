using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CQ9Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //once again, here are the attribute variables we need including
        //textures, a font, a list to hold our stars, a random number generator,
        private SpriteFont _gameFont;
        private Texture2D _spaceBackground;
        private Texture2D _starSprite;
        private List<Star> _starList;
        private Random _rng;
        private int _waveCounter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //let's start with our wave counter at 0 and our empty list of stars
            _waveCounter = 0;
            _starList = new List<Star>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our textures and fonts.
            _starSprite = Content.Load<Texture2D>("star");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");
            _gameFont = Content.Load<SpriteFont>("GameFont");

            //get our random number generator ready to go
            _rng = new Random();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //if there are no stars in the scene, increase the wave counter by one
            //and add that many new stars to list. This will create our next incoming wave.
            if(_starList.Count <= 0)
            {
                _waveCounter++;
                for (int i = 0; i < _waveCounter; i++)
                {
                    _starList.Add(new Star(_rng.Next(50, 751), -300, _starSprite));
                }
                
            }

            //if any stars are below the scene, we need to remove them
            for(int i = _starList.Count - 1; i >= 0; i--) 
            {
                if (_starList[i].GetY() > 600)
                    _starList.RemoveAt(i);
            }

            //update all the stars - this includes rotating and falling
            for(int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //this bit of code simply displays the wave counter
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Wave: " + _waveCounter, new Vector2(10,10), Color.White);
            _spriteBatch.End();

            //the stars are drawn by calling their own draw method.
            for(int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
