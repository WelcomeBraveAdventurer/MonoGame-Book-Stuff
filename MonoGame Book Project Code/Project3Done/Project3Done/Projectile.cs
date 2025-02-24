using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3Done
{
    class Projectile
    {
        float _projectileX, _projectileY, _projectileSpeed;
        Texture2D _projectileSprite;
        
        public Projectile(float projectileX, float projectileY, float projectileSpeed, Texture2D projectileSprite)
        {
            _projectileX = projectileX;
            _projectileY = projectileY;
            _projectileSpeed = projectileSpeed;
            _projectileSprite = projectileSprite;
        }

        public Rectangle GetBounds() { return new Rectangle((int)_projectileX, (int)_projectileY, _projectileSprite.Width, _projectileSprite.Height);
        }

        public void Update()
        {
            _projectileX += _projectileSpeed;
        }

        public float GetX() { return _projectileX; }
        public float GetY() { return _projectileY; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_projectileSprite, new Vector2(_projectileX, _projectileY), Color.White);
            spriteBatch.End();
        }
    }
}
