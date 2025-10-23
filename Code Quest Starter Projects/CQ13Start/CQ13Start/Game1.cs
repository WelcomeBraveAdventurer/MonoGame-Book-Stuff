using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace CQ13Start
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //attributes for our game!
        private SpriteFont _gameFont;
        private Texture2D _spaceBackground, _starSprite;
        private List<Star> _starList;
        private List<Explosion> _explosionList;
        private Random _rng;
        private int _waveCounter;

        //here are the new sound attributes
        private Song _backgroundMusic;
        private SoundEffect _popSound, _whistleSound;

        //this version adds a _gameOver variable
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
   
            //set all our default values
            _waveCounter = 0;
            _starList = new List<Star>();
            _explosionList = new List<Explosion>();
            _leftMouseClick = false;


            //this texture will be used to draw the hitboxes if we want to see them
            _hitBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitBoxTexture.SetData(new Color[] { Color.White });

            _rng = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load all our game sprite and fonts
            _starSprite = Content.Load<Texture2D>("star");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");
            _gameFont = Content.Load<SpriteFont>("GameFont");

            //load our new sound assets
            _popSound = Content.Load<SoundEffect>("popSound");
            _whistleSound = Content.Load<SoundEffect>("sadWhistleSound");


            //don't forget to load the new game over sprite and game over sound (CQ13)


            _backgroundMusic = Content.Load<Song>("wizardSong");


            //here's where we start playing our background music - only if it isn't already playing
            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop();
            }
            MediaPlayer.Play(_backgroundMusic);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //only update the game if the start timer is done (CQ13) and the game isn't over
            



            //if there are no more stars, we need to create a new wave and add more stars than before
            if (_starList.Count <= 0)
            {
                _waveCounter++;
                for (int i = 0; i < _waveCounter; i++)
                {
                    _starList.Add(new Star(_rng.Next(50, 751), -300, _starSprite));
                }

            }

            //check for mouse input
            MouseState currentMouseState = Mouse.GetState();
            if (!_leftMouseClick && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _leftMouseClick = true;

                //check to see if any stars were clicked on
                for (int i = _starList.Count - 1; i >= 0; i--)
                {

                    if (_starList[i].GetBounds().Contains(currentMouseState.Position))
                    {
                        //if so, create a new explosion at that star's position and pass in the appropriate
                        //sound asset so that it can be played.
                        _explosionList.Add(new Explosion(currentMouseState.X, currentMouseState.Y, Content.Load<Texture2D>("explosionSpritesheet"), _popSound));
                        _starList.RemoveAt(i);
                    }

                }
            }

            //reset the left mouse click if the button is released
            if (_leftMouseClick && currentMouseState.LeftButton != ButtonState.Pressed)
                _leftMouseClick = false;


            //remove any stars that have left the screen, and play the whistle sound
            for (int i = _starList.Count - 1; i >= 0; i--)
            {
                if (_starList[i].GetY() > 500)
                {
                    _starList.RemoveAt(i);
                    _whistleSound.Play();
                                        //add to the missed star variable (CQ13)
                }
            }

            //update all our stars and explosions
            for (int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Update();
            }

            for (int i = 0; i < _explosionList.Count; i++)
            {
                _explosionList[i].Update();
            }

            //remove any explosions that are finished
            for (int i = _explosionList.Count - 1; i >= 0; i--)
            {
                if (_explosionList[i].isExplosionFinished())
                {
                    _explosionList.RemoveAt(i);
                }
            }
            





            //count down the start timer if it's still running (CQ13)



            //check for game over - 5 missed stars ends the game (CQ13)



            //here we add a press R to restart feature (CQ13)
 


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //draw the background and the text elements first
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Wave: " + _waveCounter, new Vector2(10, 10), Color.White);

            //show the player how many stars they've missed (CQ13)




            //optionally draw the star hitboxes if you want to see them for debugging
            bool showBounds = false;
            if (showBounds)
            {
                for (int i = 0; i < _starList.Count; i++)
                    _spriteBatch.Draw(_hitBoxTexture, _starList[i].GetBounds(), Color.White);
            }

            //draw the "Get ready!" text if the start timer is still running (CQ13)




            //draw the game over screen if the game is over (CQ13)





            _spriteBatch.End();

            //draw all our stars and explosions
            for (int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < _explosionList.Count; i++)
            {
                _explosionList[i].Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
