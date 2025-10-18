using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Level12Start
{
    enum GremlinStates
    {
        idle, attacking, moving, dying
    }
    internal class Gremlin
    {
        //attribute variables to define the state of our hero
        private int _gremlinHealth;
        private float _gremlinX, _gremlinY, _gremlinSpeed;
        private Texture2D _gremlinSprite;
        private bool _gremlinMovingRight, _gremlinIsAttacking, _gremlinIsDead;

        private GremlinStates _gremlinState;

        private int _animationTimer, _animationIndex, _animationRow;



        //a constructor to set the default attribute values
        public Gremlin(float gremlinX, float gremlinY, int gremlinHealth, float gremlinSpeed, Texture2D gremlinSprite)
        {

            _gremlinX = gremlinX;
            _gremlinY = gremlinY;
            _gremlinHealth = gremlinHealth;
            _gremlinSpeed = gremlinSpeed;
            _gremlinSprite = gremlinSprite;
            _gremlinMovingRight = false;
            _gremlinIsAttacking = false;
            _gremlinIsDead = false;
            _animationTimer = 0;
            _animationIndex = 0;
            _animationRow = 0;
        }

        //accessor methods to retrieve attribute values
        public int GetHealth() { return _gremlinHealth; }


        //these are helpful when we want to tell Game1 or some other game object where 
        //the gremlin is.
        public float GetX() { return _gremlinX; }
        public float GetY() { return _gremlinY; }


        //mutator methods to change attribute values
        public void TakeDamage(int damageAmount) { _gremlinHealth -= damageAmount; }


        //helper methods that don’t affect attribute values – this
        //method uses a Random object to return a random gremlin damage value
        //between 2 and 4.
        public int DealDamage() { Random rng = new Random(); return rng.Next(2, 5); }

        public Rectangle GetBounds() { return new Rectangle((int)_gremlinX + 30, (int)_gremlinY + 30, 55, 55); }


        //this is where all our hero's update code will go.
        public void Update()
        {
            //gamepad code for the gremlin...right out of Level 7
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);



            _gremlinState = GremlinStates.idle;
            //get the current state of the keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (_gremlinHealth > 0 && (currentKeyboardState.IsKeyDown(Keys.Space)))
            {
                _gremlinState = GremlinStates.attacking;
            }
            else if (_gremlinHealth > 0 && !_gremlinIsAttacking)
            {

                if (_gremlinMovingRight)
                {
                    _gremlinX += _gremlinSpeed;
                    _gremlinState = GremlinStates.moving;
                }
                else
                {
                    _gremlinX -= _gremlinSpeed;
                    _gremlinState = GremlinStates.moving;
                }

            }
            else
            {
                _gremlinState = GremlinStates.dying;
                if (!_gremlinIsDead)
                    _animationIndex = 0;
                _gremlinIsDead = true;

            }

            _animationTimer++;
            if (_animationTimer >= 5)
            {

                _animationIndex++;
                if (_gremlinState == GremlinStates.idle && _animationIndex >= 4)
                    _animationIndex = 0;
                if(_gremlinState == GremlinStates.attacking && _animationIndex >= 7)
                    _animationIndex = 0;

                if (_gremlinState == GremlinStates.moving && _animationIndex >= 6)
                    _animationIndex = 0;


                if (_gremlinState == GremlinStates.dying && _animationIndex >= 9)
                    _animationIndex = 9;

                if (_gremlinState == GremlinStates.idle)
                    _animationRow = 0;
                if (_gremlinState == GremlinStates.attacking)
                    _animationRow = 1;
                if (_gremlinState == GremlinStates.moving)
                    _animationRow = 2;
                if (_gremlinState == GremlinStates.dying)
                    _animationRow = 3;

                _animationTimer = 0;

            }

        }

        public void ChangeDirection() { _gremlinMovingRight = !_gremlinMovingRight; }


        //A method to draw our gremlin in the scene
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if(!_gremlinMovingRight)
                spriteBatch.Draw(_gremlinSprite, new Vector2(_gremlinX, _gremlinY), new Rectangle(_animationIndex * 128, _animationRow * 96, 128, 96), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_gremlinSprite, new Vector2(_gremlinX, _gremlinY), new Rectangle(_animationIndex * 129, _animationRow * 96, 128, 96), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
        }

    }

}
