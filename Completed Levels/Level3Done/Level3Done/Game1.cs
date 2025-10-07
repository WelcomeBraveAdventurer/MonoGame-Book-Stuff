using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Level3Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //The only attribute variables we need here are two SpriteFonts
        private SpriteFont _mainFont, _dialogFont;

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

            //Load and assign our two SpriteFont variables - make sure these fonts
            //are in the MGCB!
            _mainFont = Content.Load<SpriteFont>("MainFont");
            _dialogFont = Content.Load<SpriteFont>("DialogFont");

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
            
            //this is key! It starts our drawing session.
            _spriteBatch.Begin();

            //some variables to try
            string text = "Welcome, brave adventurer! (MainFont)";  
            int textX = 50;
            int textY = 100;
            Vector2 textPosition = new Vector2(textX, textY);
            Color textColor = new Color(255, 255, 255);

            //playing with 2D vectors
            Vector2 topCorner = new Vector2(0, 0);
            Vector2 overAndDown = new Vector2(50, 150);
            Vector2 tooLeft = new Vector2(-45, 400);
            Vector2 tooLow = new Vector2(600, 501);

            //all of our DrawString() statements in action
            _spriteBatch.DrawString(_mainFont, text, textPosition, textColor);
            _spriteBatch.DrawString(_dialogFont, "Drawing text is cool. (DialogFont)", new Vector2(50, 150), Color.White);

            //uncomment the statements below to see what happens

            //_spriteBatch.DrawString(_mainFont, "Top corner is awesome.", topCorner, Color.Black);
            //_spriteBatch.DrawString(_mainFont, "Moved over and down.", overAndDown, Color.Black);
            //_spriteBatch.DrawString(_mainFont, "What a strange place to start writing.", tooLeft, Color.Black);
            //_spriteBatch.DrawString(_mainFont, "Um, something is wrong here.", tooLow, Color.Black);

            //this is key! It ends our drawing session.
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
