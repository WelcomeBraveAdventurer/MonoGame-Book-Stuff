using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CQ10SpaceStart
{
    internal class Gremlin
    {
        //attribute variables to define the state of our hero
        private int _gremlinHealth;
        private float _gremlinX, _gremlinY, _gremlinSpeed;
        private Texture2D _gremlinSprite;
        private bool _gremlinMovingRight;



        //a constructor to set the default attribute values
        public Gremlin(float gremlinX, float gremlinY, int gremlinHealth, float gremlinSpeed, Texture2D gremlinSprite)
        {

            _gremlinX = gremlinX;
            _gremlinY = gremlinY;
            _gremlinHealth = gremlinHealth;
            _gremlinSpeed = gremlinSpeed;
            _gremlinSprite = gremlinSprite;
            _gremlinMovingRight = false;
        }

        //accessor methods to retrieve attribute values
        public int GetHealth() { return _gremlinHealth; }


        //these are helpful when we want to tell Game1 or some other game object where 
        //the gremlin is.
        public float GetX() { return _gremlinX; }
        public float GetY() { return _gremlinY; }


        //mutator methods to change attribute values
        public void TakeDamage(int damageAmount) { _gremlinHealth -= damageAmount; }


        //helper methods that don’t affect attribute values – this
        //method uses a Random object to return a random gremlin damage value
        //between 2 and 4.
        public int DealDamage() { Random rng = new Random(); return rng.Next(2, 5); }

        public Rectangle GetBounds() { return new Rectangle((int)_gremlinX + 5, (int)_gremlinY + 5, 55, 55); }


        //this is where all our hero's update code will go.
        public void Update()
        {

            //get your gremlin pacing here

        }


        //a method to change the gremlin's direction
        public void ChangeDirection() { _gremlinMovingRight = !_gremlinMovingRight; }


        //A method to draw our gremlin in the scene
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if(!_gremlinMovingRight)
                spriteBatch.Draw(_gremlinSprite, new Vector2(_gremlinX, _gremlinY), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_gremlinSprite, new Vector2(_gremlinX, _gremlinY), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
        }

    }

}
