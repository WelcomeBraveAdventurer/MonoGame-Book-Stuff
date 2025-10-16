using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CQ11SpaceDone
{
    internal class Coin
    {
        //attribute variables to define the state of our coin
        private int _value, _animationTimer, _animationIndex;
        private float _coinX, _coinY;
        private Texture2D _coinSpriteSheet;


        //a constructor to set the default attribute values
        public Coin(float coinX, float coinY, int value, Texture2D coinSpriteSheet)
        {
            _coinX = coinX;
            _coinY = coinY;
            _value = value;
            _coinSpriteSheet = coinSpriteSheet;
        }

        //these are helpful when we want to tell Game1 or some other game object where 
        //the coin is.
        public float GetX() { return _coinX; }
        public float GetY() { return _coinY; }

        //accessor methods to retrieve attribute values
        public int GetValue() { return _value; }


        public void Update()
        {
            //animate our coin by cycling through the frames of our sprite sheet
            _animationTimer++;
            if (_animationTimer >= 5)
            {
                //if we go too far, loop back to the beginning
                _animationIndex++;
                if (_animationIndex >= 6)
                    _animationIndex = 0;

                //reset the timer
                _animationTimer = 0;
            }
        }

        //a method to get the bounding rectangle of our coin
        public Rectangle GetBounds()
        {
            return new Rectangle((int)_coinX + 20, (int)_coinY + 15, 25, 35);
        }

        //a draw method for our coin object
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            //draw the coin using the appropriate frame from the sprite sheet
            spriteBatch.Draw(
            _coinSpriteSheet,                                   //name of the texture
            new Vector2(_coinX, _coinY),                        //screen location
            new Rectangle(_animationIndex * 64, 0, 64, 64),     //piece to “cut” out of the sprite sheet
            Color.White,                                        //color to blend
            0.0f,                                               //rotation
            new Vector2(0, 0),                                  //sprite center of rotation
            1.0f,                                               //scale 
            SpriteEffects.None,                                 //sprite effects
            0                                                   //layer depth
            );


            spriteBatch.End();

        }

    }

}
