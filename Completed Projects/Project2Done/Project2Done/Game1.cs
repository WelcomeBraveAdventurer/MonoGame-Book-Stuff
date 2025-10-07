using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Project2Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //here are our game attributes (the above ones are MonoGame defaults)
       
        //textures we need
        private Texture2D _backgroundSprite, _obstacleSprite, _coinSprite, _gameoverSprite;

        //objects we need
        private WallBuilder _wallBuilder;
        private Player _player;
        private Destroyer _destroyer1, _destroyer2;
        private Timer _timer;
        private ScoreKeeper _scoreKeeper;
        private GameOver _gameOver;

        //fix the destroyers
        Random rng = new Random();

        //mostly default game constructor except that the window was resized to 1200x600 to better
        //fit the background image.
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        //no changes
        protected override void Initialize()
        {
            base.Initialize();
        }

        //most of our initialization is in here
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and initialize our textures
            _backgroundSprite = Content.Load<Texture2D>("spaceBackground");
            _obstacleSprite = Content.Load<Texture2D>("block");
            _coinSprite = Content.Load<Texture2D>("coin");
            _gameoverSprite = Content.Load<Texture2D>("gameOver");

            //initialize our wall builder
            _wallBuilder = new WallBuilder(_obstacleSprite, _coinSprite);

            //two destroyers are better than one
            _destroyer1 = new Destroyer(1300, (float)rng.Next(100,500), (rng.Next(75, 151) / 100.0f));
            _destroyer2 = new Destroyer(1300, (float)rng.Next(100,500), (rng.Next(75, 151) / 100.0f));

            //Timer
            _timer = new Timer(Content.Load<SpriteFont>("TimerFont"));

            //ScoreKeeper
            _scoreKeeper = new ScoreKeeper(Content.Load<SpriteFont>("TimerFont"));

            //one player is all we can handle for now
            _player = new Player(100, 100, Content.Load<Texture2D>("spaceship"), 1.75f);

            _gameOver = new GameOver(_gameoverSprite);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //all the magic
            if (!_player.hasCrashed())  //if player hasn't crashed, do the game
                                        //when they crash, stop the updates!!
            {
                _timer.Update(gameTime);
                _wallBuilder.Update(gameTime); //wall builder update method (create the wall)
                _wallBuilder.MoveObstaclesAndCoins(); //ask the obstacles and stars to move
                
                //destroyers move up and down only.
                _destroyer1.Update(); 
                _destroyer2.Update();
                
                //check to see if any obstacles were hit by the destroyer(s) and if the
                //player has hit an obstacle
                _wallBuilder.DestroyObstaclesAndCollideWithPlayer(_destroyer1, _player);
                _wallBuilder.DestroyObstaclesAndCollideWithPlayer(_destroyer2, _player);
                
                //check to see if the player has pick up any stars
                _wallBuilder.DestroyCoins(_player, _scoreKeeper);

                //move the player
                _player.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //cheap method to draw the background image
            DrawBackground();

            //draw obstacles and stars
            _wallBuilder.Draw(_spriteBatch);

            //draw the player
            if(! _player.hasCrashed() )   
                _player.Draw(_spriteBatch);
            else
                _gameOver.Draw(_spriteBatch);

            //draw the timer and the scorekeeper
            _timer.Draw(_spriteBatch); 
            
            _scoreKeeper.Draw(_spriteBatch);
            base.Draw(gameTime);
        }

        //cheap, but effective
        private void DrawBackground()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundSprite, new Vector2(0, 0), null, Color.White, 0, new Vector2(0,0), 2, SpriteEffects.None, 0);
            _spriteBatch.End();
        }
    }
}