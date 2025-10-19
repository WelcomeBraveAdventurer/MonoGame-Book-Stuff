using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CQ9Start
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //once again, here are the attribute variables we need including
        //textures (part of the quest), a font, a list to hold our
        //stars (part of the quest), and a random number
        //generator (also part of the quest),
        private SpriteFont _gameFont;
        private Texture2D _spaceBackground;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {




            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our textures and fonts.
            _spaceBackground = Content.Load<Texture2D>("spaceBackground");
            _gameFont = Content.Load<SpriteFont>("GameFont");




        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //this bit of code simply displays the wave counter
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceBackground, new Vector2(0, 0), Color.White);


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
