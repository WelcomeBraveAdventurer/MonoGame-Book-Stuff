using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace CQ11SpaceDone
{
    internal class Obstacle
    {
        //attribute variables for our obstacles
        private float _obstacleX, _obstacleY;
        private Texture2D _obstacleSprite;

        //a constructor to set the default attribute values
        public Obstacle(float obstacleX, float obstacleY, Texture2D obstacleTexture)
        {
            _obstacleX = obstacleX;
            _obstacleY = obstacleY;
            _obstacleSprite = obstacleTexture;
        }

        //this getBounds() has specific hitboxes for different object types
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

        //this is helpful when we want to get the position of our obstacles in the scene
        public float GetX() { return _obstacleX; }
        public float GetY() { return _obstacleY; }

        //a simple, but effective draw method for our obstacles
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_obstacleSprite, new Vector2(_obstacleX, _obstacleY), Color.White);
            spriteBatch.End();
        }
    }
}

