using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;    //we need this to use lists
using System;    //we need this for our random number generator

namespace Level9Start
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //we need this for the coins (see Level 9)
        private Texture2D _coinTexture;

        //a handy random object. Games love randomness.
        private Random _rng;

        //this will be used to add stars. It's for our single-press logic.
        private bool _spacePressed;

        //a handy font, just in case
        private SpriteFont _gameFont;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {


            //set up our random object 
            _rng = new Random();


            //assume the space bar isn't being pressed when the game starts
            _spacePressed = false;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //for our coins to be shiny and beautiful. We won't create the coins until we get to Update();
            _coinTexture = Content.Load<Texture2D>("coin");

            //and don't forget the font
            _gameFont = Content.Load<SpriteFont>("GameFont");



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();





            //don't forget to reset the space bar
            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _spacePressed = false;
            }






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);





            base.Draw(gameTime);
        }
    }
}
