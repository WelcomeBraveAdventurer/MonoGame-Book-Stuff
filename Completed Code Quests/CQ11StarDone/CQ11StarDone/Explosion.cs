using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CQ11StarDone
{
    internal class Explosion
    {
        //attributes that we need for our explosion to look awesome
        private Texture2D _explosionSpritesheet;
        private float _explosionX, _explosionY;
        private bool _explosionFinished;
        private int _animationIndex, _animationTimer;

        //constructor to set up our explosion's default values
        public Explosion(float explosionX,  float explosionY, Texture2D explosionSpritesheet)
        {
            _explosionX = explosionX;
            _explosionY = explosionY;
            _explosionSpritesheet = explosionSpritesheet;

            _animationIndex = 0;
            _animationTimer = 0;
            _explosionFinished = false;
        }

        public void Update()
        {
            //this timer will help us slow down the animation a bit
            _animationTimer++;
            if(_animationTimer >= 5)
            {
                _animationIndex++;
                if( _animationIndex >= 9)  //there are 9 frames in our explosion spritesheet
                {
                    //once the animation is done, we set this to true so we can remove the explosion from the list
                    _explosionFinished = true;  
                }

                //reset the timer
                _animationTimer = 0;
            }
        }

        //simple getter to see if the explosion is done
        public bool isExplosionFinished() { return _explosionFinished;  }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //all the variables come together to draw our lovely explosion
            spriteBatch.Draw(_explosionSpritesheet, new Vector2(_explosionX, _explosionY), new Rectangle(_animationIndex * 128, 0, 128, 128), Color.White, 0, new Vector2(64,64), 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
