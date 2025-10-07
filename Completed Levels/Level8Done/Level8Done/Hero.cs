using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Level8Done
{
    internal class Hero
    {
        //attribute variables to define the state of our hero
        private int _heroHealth;
        private float _heroX, _heroY, _heroSpeed;
        private Texture2D _heroTexture;


        //a constructor to set the default attribute values
        public Hero(float heroX, float heroY, int heroHealth, float heroSpeed, Texture2D heroTexture)
        {

            _heroX = heroX;
            _heroY = heroY;
            _heroHealth = heroHealth;
            _heroSpeed = heroSpeed;
            _heroTexture = heroTexture;
        }

        //accessor methods to retrieve attribute values
        public int GetHealth() { return _heroHealth; }


        //these are helpful when we want to tell Game1 or some other game object where 
        //the hero is.
        public float GetX() { return _heroX; }
        public float GetY() { return _heroY; }


        //mutator methods to change attribute values
        public void TakeDamage(int damageAmount) { _heroHealth -= damageAmount; }
        public void Heal(int healAmount) { _heroHealth += healAmount; }


        //helper methods that don’t affect attribute values – this
        //method uses a Random object to return a random heroic damage value
        //between 1 and 3.
        public int DealDamage() { Random rng = new Random(); return rng.Next(1, 4); }


        //this is where all our hero's update code will go.
        public void Update()
        {
            //get the current state of the keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();

            //standard WASD keys
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                _heroX -= _heroSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                _heroX += _heroSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                _heroY -= _heroSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                _heroY += _heroSpeed;
            }
        }


        //A method to draw our hero in the scene
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_heroTexture, new Vector2(_heroX, _heroY), Color.White);
            spriteBatch.End();
        }

    }

}
