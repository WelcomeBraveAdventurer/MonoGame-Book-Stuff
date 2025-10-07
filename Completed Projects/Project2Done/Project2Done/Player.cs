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
    //the player of our game!!
    internal class Player
    {
        //coordinates and speed please
        private float _playerX, _playerY, _playerSpeed;

        //this is what we look like
        private Texture2D _playerSprite;

        //we need to know if we've crashed into one of the obstacles
        private bool _playerCrashed;

        //argumented constructor (a zero arg one wouldn't be helpful at all imho)
        public Player(float playerX, float playerY, Texture2D playerSprite, float playerSpeed)
        {
            //set all the attributes!!
            _playerX = playerX;
            _playerY = playerY;
            _playerSpeed = playerSpeed;
            _playerSprite = playerSprite;

            //don't allow a pre-crashed player, hard-code this to not crashed.
            this._playerCrashed = false;
        }

        //the only thing that the player does on it's own is move in the four directions
        //using keyboard mappings.
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _playerX -= _playerSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _playerX += _playerSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _playerY -= _playerSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _playerY += _playerSpeed;
        }

        //the player's rectangle (bounds) will be used to detect collisions with 
        //stars and obstacles.
        public Rectangle GetBounds() { return new Rectangle((int)_playerX, (int)_playerY + 10, _playerSprite.Width, _playerSprite.Height - 20); }

        //two quick methods to set and to access our crashed variable.
        public void setCrashed() { _playerCrashed = true; }
        public bool hasCrashed() { return _playerCrashed; }

        //draw the player's beauty
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerSprite, new Vector2(_playerX, _playerY), Color.White);
            _spriteBatch.End();
        }
    }
}
