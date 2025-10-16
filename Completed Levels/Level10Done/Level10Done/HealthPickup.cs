using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Level10Done
{
    internal class HealthPickup
    {
        //attribute variables to define the state of our health pick up
        private int _healthValue;
        private float _healthPickupX, _healthPickupY;
        private Texture2D _healthPickupSprite;


        //a constructor to set the default attribute values
        public HealthPickup(float healthPickupX, float healthPickupY, Texture2D healthPickupSprite)
        {
            _healthValue = 5;
            _healthPickupX = healthPickupX;
            _healthPickupY = healthPickupY;
            _healthPickupSprite = healthPickupSprite;
        }

        //these are helpful when we want to tell Game1 or some other game object where 
        //the health pick up is.
        public float GetX() { return _healthPickupX; }
        public float GetY() { return _healthPickupY; }

        //accessor methods to retrieve attribute values
        public int GetHealthValue() { return _healthValue; }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_healthPickupX +20, (int)_healthPickupY + 15, 25, 35);
        }


        //Where is Update()? Like the coin, a health pickup just sits there (for now) looking healthy. We don't
        //need an Update() method since it doesn't really "do" anything else (yet).




        //a draw method for our health pick up object
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(_healthPickupSprite, new Vector2(_healthPickupX, _healthPickupY), Color.White);
            spriteBatch.End();

        }

    }

}
