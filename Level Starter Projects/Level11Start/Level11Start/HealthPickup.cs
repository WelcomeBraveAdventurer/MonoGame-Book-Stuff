using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Level11Start
{
    internal class HealthPickup
    {
        //attribute variables to define the state of our health pick up
        private int _healthValue, _animationTimer, _animationIndex;
        private Texture2D _healthPickupSprite;
        private float _healthPickupX, _healthPickupY;

        public HealthPickup(float healthPickupX, float healthPickupY, Texture2D healthPickupSprite)
        {
            _healthValue = 3;
            _healthPickupSprite = healthPickupSprite;
            _healthPickupX = healthPickupX;
            _healthPickupY = healthPickupY;

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


        public void Update()
        {



        }


        //a draw method for our health pick up object
        public void Draw(SpriteBatch spriteBatch)
        {

        spriteBatch.Begin();
        spriteBatch.Draw(
            _healthPickupSprite,               //name of the texture
            new Vector2(_healthPickupX, _healthPickupY),        //screen location
            new Rectangle(_animationIndex * 64, 0, 64, 64),  //piece to “cut” out of the sprite sheet
            Color.White,                                     //color to blend
            0.0f,                                            //rotation
            new Vector2(0, 0),                                //sprite center of rotation
            1.0f,                                            //scale 
            SpriteEffects.None,                              //sprite effects
            0                                             //layer depth
        );
        spriteBatch.End();


    }

}

}
