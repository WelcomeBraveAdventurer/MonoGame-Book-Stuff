using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Level7Start
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //declare our scene’s attribute variables
        private Texture2D _heroSprite;
        private float _heroX, _heroY;
        private float _heroSpeed;
        private float _heroAngle, _heroRotationSpeed;
        private Color _heroColor;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //set our initial hero X and Y coordinates, speed, angle, rotation speed, and color
            _heroX = 100;
            _heroY = 100;
            _heroSpeed = 5;
            _heroAngle = 0;
            _heroRotationSpeed = 0.25f;
            _heroColor = Color.White;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load and assign our hero texture
            _heroSprite = Content.Load<Texture2D>("heroGray");
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

            _spriteBatch.Begin();
            //draw the hero - the hero will change colours and positions based on the input above
            _spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, _heroColor, _heroAngle, new Vector2(_heroSprite.Width / 2, _heroSprite.Height / 2), 1, SpriteEffects.None, 0);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
