using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Level10Done
{
    internal class Hero
    {
        //attribute variables to define the state of our hero
        private int _heroHealth;
        private float _heroX, _heroY, _heroSpeed;
        private Texture2D _heroSprite;

        private bool _heroMovingRight;    //to flip the sprite when needed

        private bool _heroBlockLeft, _heroBlockRight, _heroBlockUp, _heroBlockDown;



        //a constructor to set the default attribute values
        public Hero(float heroX, float heroY, int heroHealth, float heroSpeed, Texture2D heroSprite)
        {

            _heroX = heroX;
            _heroY = heroY;
            _heroHealth = heroHealth;
            _heroSpeed = heroSpeed;
            _heroSprite = heroSprite;
            _heroMovingRight = true;    //start out looking right


            _heroBlockLeft = false;
            _heroBlockRight = false;
            _heroBlockUp = false;
            _heroBlockDown = false;

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

        public Rectangle GetBounds() { return new Rectangle((int)_heroX + 10, (int)_heroY + 10, 40, 50); }

        public void Block(string direction)
        {
            if (direction == "left")
                _heroBlockLeft = true;
            if (direction == "right")
                _heroBlockRight = true;
            if (direction == "up")
                _heroBlockUp = true;
            if (direction == "down")
                _heroBlockDown = true;
        }

        public void EscapeDamage(string direction)
        {
            if (direction == "left")
                _heroX -= 50;
            if (direction == "right")
                _heroX += 50;
            if (direction == "up")
                _heroY -= 50;
            if (direction == "down")
                _heroY += 50;
        }

        public Vector2 GetCenterPoint() { return new Vector2(_heroX + 64 / 2, _heroY + 64 / 2); }

        //this is where all our hero's update code will go.
        public void Update()
        {
            //get the current state of the keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();

            //standard WASD keys
            if (!_heroBlockLeft && currentKeyboardState.IsKeyDown(Keys.A))
            {
                _heroX -= _heroSpeed;
                _heroMovingRight = false;  //A is left, not right
            }
            if (!_heroBlockRight && currentKeyboardState.IsKeyDown(Keys.D))
            {
                _heroX += _heroSpeed;
                _heroMovingRight = true;  //D is right
            }
            if (!_heroBlockUp && currentKeyboardState.IsKeyDown(Keys.W))
            {
                _heroY -= _heroSpeed;
            }
            if (!_heroBlockDown && currentKeyboardState.IsKeyDown(Keys.S))
            {
                _heroY += _heroSpeed;
            }


            _heroBlockLeft = false;
            _heroBlockRight = false;
            _heroBlockUp = false;
            _heroBlockDown = false;
        }


        //A method to draw our hero in the scene
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //using the 9-argument version so we can flip the hero based on the direction they are moving
            if (_heroMovingRight)
                spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_heroSprite, new Vector2(_heroX, _heroY), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
        }

    }

}

