using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2Done
{
    //our wall is built of obstacles
    internal class Block
    {
        //like most things in the program, we need coordinates and speed
        //obstacles simply move from right to left (endlessly)
        private float _obstacleX, _obstacleY, _obstacleSpeed;
        
        //this is our lovely texture
        private Texture2D _obstacleSprite;

        //argumented constructor (zero-arg constructor not useful here)
        public Block(int obstacleX, int obstacleY, Texture2D obstacleSprite, float speed)
        {
            //set all the attributes!!!
            _obstacleX = obstacleX;
            _obstacleY = obstacleY;
            _obstacleSprite = obstacleSprite;
            _obstacleSpeed = speed;
        }

        //all an obstacle does is move from right to left (endlessly)
        public void Update()
        {
            _obstacleX -= _obstacleSpeed;
        }

        //this rectangle defines the bounds of an obstacle and will be used in detecting collisions
        //with the player
        public Rectangle GetBounds() { return new Rectangle((int)_obstacleX, (int)_obstacleY, _obstacleSprite.Width, _obstacleSprite.Height); }

        //these four methods will help us with collision dection between the obstacles and the
        //destroyer object
        public float getX() { return _obstacleX; }
        public float getY() { return _obstacleY; }
        public float getWidth() {return _obstacleSprite.Width; }
        public float getHeight() { return _obstacleSprite.Height; }

        //draw our beautiful obstacles (so pretty)
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_obstacleSprite, new Vector2(_obstacleX, _obstacleY), null, Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.None, 0);
            _spriteBatch.End();
        }
    }
}
