using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace CQ12StarDone
{
    internal class Explosion
    {
        private Texture2D _explosionSpritesheet;
        private float _explosionX, _explosionY;
        private bool _explosionFinished;
        private int _animationIndex, _animationTimer;
        private SoundEffect _explosionSound;

        public Explosion(float explosionX,  float explosionY, Texture2D explosionSpritesheet, SoundEffect explosionSound)
        {
            _explosionX = explosionX;
            _explosionY = explosionY;
            _explosionSpritesheet = explosionSpritesheet;
            _explosionSound = explosionSound;

            _animationIndex = 0;
            _animationTimer = 0;
            _explosionFinished = false;

            PlaySound();
        }

        public void PlaySound()
        {
            _explosionSound.Play();
        }

        public void Update()
        {
            
            _animationTimer++;
            if(_animationTimer >= 5)
            {
                _animationIndex++;
                if( _animationIndex >= 9 )
                {
                    _explosionFinished = true;
                }
                _animationTimer = 0;
            }
        }

        public bool isExplosionFinished() { return _explosionFinished;  }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //all the variables come together to draw our lovely stars
            spriteBatch.Draw(_explosionSpritesheet, new Vector2(_explosionX, _explosionY), new Rectangle(_animationIndex * 128, 0, 128, 128), Color.White, 0, new Vector2(64,64), 1, SpriteEffects.None, 0);
            //spriteBatch.Draw(_explosionSpritesheet, new Vector2(_explosionX, _explosionY), new Rectangle(0, 0, 128, 128), Color.White, 0, new Vector2(64, 64), 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
