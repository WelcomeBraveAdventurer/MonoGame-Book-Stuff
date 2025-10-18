using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Level10Start
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

