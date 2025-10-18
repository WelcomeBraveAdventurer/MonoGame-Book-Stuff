using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Level11Start
{
    internal class Obstacle
    {
        private float _obstacleX, _obstacleY;
        private Texture2D _obstacleSprite;

        public Obstacle(float obstacleX, float obstacleY, Texture2D obstacleTexture)
        {
            _obstacleX = obstacleX;
            _obstacleY = obstacleY;
            _obstacleSprite = obstacleTexture;
        }

        public Rectangle GetBounds()
        {
            if (_obstacleSprite.Name == "table")
                return new Rectangle((int)_obstacleX, (int)_obstacleY + 12, 64, 42);
            else if (_obstacleSprite.Name == "console")
                return new Rectangle((int)_obstacleX + 15, (int)_obstacleY + 10, 35, 50);
            else if (_obstacleSprite.Name == "crate")
                return new Rectangle((int)_obstacleX + 14, (int)_obstacleY + 15, 35, 35);
            else
                return new Rectangle((int)_obstacleX, (int)_obstacleY, _obstacleSprite.Width, _obstacleSprite.Height);

        }


        public float GetX() { return _obstacleX; }
        public float GetY() { return _obstacleY; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_obstacleSprite, new Vector2(_obstacleX, _obstacleY), Color.White);
            spriteBatch.End();
        }
    }
}

