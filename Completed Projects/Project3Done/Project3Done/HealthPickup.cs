using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project3Done
{
    internal class HealthPickup
    {
        //attribute variables to define the state of our health pick up
        private int _healthValue, _animationTimer, _animationIndex;
        private Texture2D _healthPickupSpriteSheet;
        private float _healthPickupX, _healthPickupY;

        public HealthPickup(float healthPickupX, float healthPickupY, Texture2D healthPickupSpriteSheet)
        {
            _healthValue = 3;
            _healthPickupSpriteSheet = healthPickupSpriteSheet;
            _healthPickupX = healthPickupX;
            _healthPickupY = healthPickupY;
            _animationTimer = 0;
            _animationIndex = 0;
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
            _animationTimer++;
            if (_animationTimer >= 5)
            {

                _animationIndex++;
                if (_animationIndex >= 8)
                    _animationIndex = 0;

                _animationTimer = 0;
            }
        }


        //a draw method for our health pick up object
        public void Draw(SpriteBatch spriteBatch)
        {

        spriteBatch.Begin();
        spriteBatch.Draw(
            _healthPickupSpriteSheet,               //name of the texture
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
