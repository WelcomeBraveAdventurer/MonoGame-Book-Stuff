using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CQ8Start
{
    internal class Coin

    {
        //attribute variables to define the state of our coin
        private int _value;
        private float _coinX, _coinY;
        private Texture2D _coinSprite;


        //a constructor to set the default attribute values
        public Coin(float coinX, float coinY, int value, Texture2D coinSprite)
        {
            _coinX = coinX;
            _coinY = coinY;
            _value = value;
            _coinSprite = coinSprite;
        }

        //these are helpful when we want to tell Game1 or some other game object where 
        //the coin is.
        public float GetX() { return _coinX; }
        public float GetY() { return _coinY; }

        //accessor methods to retrieve attribute values
        public int GetValue() { return _value; }


        //Where is Update()? the coin just sits there looking shiny and valuable. We don't
        //need an Update() method since it doesn't really "do" anything else.

        //a draw method for our coin object
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(_coinSprite, new Vector2(_coinX, _coinY), Color.White);
            spriteBatch.End();

        }

    }

}
