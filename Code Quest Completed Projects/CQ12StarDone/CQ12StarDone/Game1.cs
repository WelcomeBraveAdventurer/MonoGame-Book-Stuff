using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace CQ12StarDone
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _gameFont;
        private Texture2D _spaceBackground;
        private Texture2D _starSprite;
        private List<Star> _starList;
        private List<Explosion> _explosionList;
        private Random _rng;
        private int _waveCounter;

        private Song _backgroundMusic;
        private SoundEffect _popSound, _whistleSound;

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
            // TODO: Add your initialization logic here
            _waveCounter = 0;
            _starList = new List<Star>();
            _explosionList = new List<Explosion>();
            _leftMouseClick = false;

            _hitBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitBoxTexture.SetData(new Color[] { Color.White });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _starSprite = Content.Load<Texture2D>("star");
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");
            _gameFont = Content.Load<SpriteFont>("GameFont");

            _rng = new Random();

            _popSound = Content.Load<SoundEffect>("popSound");
            _whistleSound = Content.Load<SoundEffect>("sadWhistleSound");


            _backgroundMusic = Content.Load<Song>("wizardSong");

            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop();
            }

            MediaPlayer.Play(_backgroundMusic);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(_starList.Count <= 0)
            {
                _waveCounter++;
                for (int i = 0; i < _waveCounter; i++)
                {
                    _starList.Add(new Star(_rng.Next(50, 751), -300, _starSprite));
                }
                
            }

            MouseState currentMouseState = Mouse.GetState();
            if (!_leftMouseClick && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _leftMouseClick = true;
                for (int i = _starList.Count - 1; i >= 0; i--)
                {

                    if (_starList[i].GetBounds().Contains(currentMouseState.Position))
                    {
                        _explosionList.Add(new Explosion(currentMouseState.X, currentMouseState.Y, Content.Load<Texture2D>("explosionSpritesheet"), _popSound));
                        _starList.RemoveAt(i);
                    }
                    
                }
            }

            if (_leftMouseClick && currentMouseState.LeftButton != ButtonState.Pressed)
                _leftMouseClick = false;

            for (int i = _starList.Count - 1; i >= 0; i--)
            {
                if (_starList[i].GetY() > 500)
                {
                    _starList.RemoveAt(i);
                    _whistleSound.Play();
                }
            }

            for(int i = 0; i < _starList.Count; i++)
            {
                _starList[i].Update();
            }

            for(int i = 0; i < _explosionList.Count; i++) {
                _explosionList[i].Update();
            }
            
            for(int i = _explosionList.Count - 1; i >= 0; i--)
            {
                if (_explosionList[i].isExplosionFinished())
                {
                    _explosionList.RemoveAt(i);
                }
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Wave: " + _waveCounter, new Vector2(10, 10), Color.White);


            bool showBounds = false;
            if (showBounds)
            {
                for (int i = 0; i < _starList.Count; i++)
                    _spriteBatch.Draw(_hitBoxTexture, _starList[i].GetBounds(), Color.White);
            }

            
            _spriteBatch.End();

            // TODO: Add your drawing code here
            for(int i = 0; i < _starList.Count; i++)
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
