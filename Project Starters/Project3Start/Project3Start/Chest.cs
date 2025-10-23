using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Project3Start
{
    internal class Chest
    {
        //attribute variables to define the state of our chest
        private bool _isClosed;
        private bool _hasCoin;
        private Random _rng;
        private float _chestX, _chestY;
        private Texture2D _chestOpenSprite, _chestClosedSprite;

        private SoundEffect _cashRegisterSound;

        //a constructor to set the default attribute values
        public Chest(float chestX, float chestY, Texture2D chestOpenTexture,
                        Texture2D chestClosedTexture, SoundEffect cashRegisterSound)
        {

            _chestX = chestX;
            _chestY = chestY;
            _chestOpenSprite = chestOpenTexture;
            _chestClosedSprite = chestClosedTexture;
            _cashRegisterSound = cashRegisterSound;

            _isClosed = true;

            _rng = new Random();
            _hasCoin = _rng.Next(1, 101) < 75;   //75% chance that the chest has a coin inside. I like those odds.
        }


        //accessor method to see if the chest is closed (true) or open (false)
        public bool IsClosed() { return _isClosed; }
        //accessor method to see if the chest has a valuable coin inside
        public bool HasCoin() { return _hasCoin; }

        //these are helpful when we want to tell Game1 or some other game object where 
        //the chest is.
        public float GetX() { return _chestX; }
        public float GetY() { return _chestY; }

        //mutator method to change the chest to open
        public void OpenChest() { 
            _isClosed = false;
            if(_hasCoin)
                _cashRegisterSound.Play();
        }
        //mutator method to change the chest to open
        public void RemoveCoin() { _hasCoin = false; }


        public Rectangle GetBounds() { return new Rectangle((int)_chestX + 16, (int)_chestY + 18, 32, 32); }


        //there is where all the chest action code goes
        public void Update()
        {
            //get the current mouse state
            MouseState currentMouseState = Mouse.GetState();

            //if we pressed the left button and the chest is closed, open it!
            //if (currentMouseState.LeftButton == ButtonState.Pressed && _isClosed)
            //{
            //    OpenChest();
            //}
        }

        //a draw method for our chest object
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            if (_isClosed)
                spriteBatch.Draw(_chestClosedSprite, new Vector2(_chestX, _chestY), Color.White);  //chest is closed
            else
                spriteBatch.Draw(_chestOpenSprite, new Vector2(_chestX, _chestY), Color.White);    //chest is open
            spriteBatch.End();

        }


    }

}
