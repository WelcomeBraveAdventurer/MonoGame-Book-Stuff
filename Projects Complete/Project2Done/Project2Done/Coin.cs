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
    //stars are pickups in our "game"
    internal class Coin
    {
        //we need coordinates and a speed variable
        private float _coinX, _coinY, _coinSpeed;
        
        //this is our texture for display
        private Texture2D _coinSprite;
    
        //argumented constructor - a zero-arg one wouldn't be helpful
        public Coin(float coinX, float coinY, float coinSpeed, Texture2D coinSprite)
        {
            //set all the attributes!!
            _coinX = coinX;
            _coinY = coinY;
            _coinSpeed = coinSpeed;
            _coinSprite = coinSprite;
        }

        //all a star does (on its own) is move to the left
        public void Update()
        {
            _coinX -= _coinSpeed;
        }

        //we'll use the star's rectangle to check for collisions with the player (pick up)
        public Rectangle GetBounds() { return new Rectangle((int)_coinX + 20, (int)_coinY + 20, _coinSprite.Width - 40, _coinSprite.Height - 40); }

        //draw all the stary goodness
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_coinSprite, new Vector2(_coinX, _coinY), Color.White);
            _spriteBatch.End();
        }
    }

}
