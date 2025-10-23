using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CQ11SpaceDone
{
    //this is an enum to give us named states for our gremlin
    enum GremlinStates
    {
        idle, attacking, moving, dying
    }

    internal class Gremlin
    {
        //attribute variables to define the state of our gremlin
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


            //assume the gremlin is idle until we find out otherwise
            _gremlinState = GremlinStates.idle;

            //get the current state of the keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();

            //space is being pressed so our gremlin is attacking
            if (_gremlinHealth > 0 && (currentKeyboardState.IsKeyDown(Keys.Space)))
            {
                //change the gremlin state to attacking
                _gremlinState = GremlinStates.attacking;
            }
            //if the gremlin is alive and not attacking, it might be moving (or still idle)
            else if (_gremlinHealth > 0 && !_gremlinIsAttacking)
            {
                //gremlin is moving right, update x and change the state
                if (_gremlinMovingRight)
                {
                    _gremlinX += _gremlinSpeed;
                    _gremlinState = GremlinStates.moving;
                }
                else //gremlin is moving left, update x and change the state
                {
                    _gremlinX -= _gremlinSpeed;
                    _gremlinState = GremlinStates.moving;
                }

            }
            else
            {
                //gremlin is dead, change the state to dying
                _gremlinState = GremlinStates.dying;

                //if the gremlin just died, reset the animation index to 0
                if (!_gremlinIsDead)
                    _animationIndex = 0;

                //set the boolean to true so we don't keep resetting the animation index
                _gremlinIsDead = true;

            }

            //animate our gremlin by cycling through the frames of our sprite sheet
            _animationTimer++;
            if (_animationTimer >= 5)
            {
                //the gremlin animation works like the hero animation from Level 11, we have to 
                //be aware of the state the gremlin is in to make sure we loop the animation correctly.
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

        //a method to change the direction of our gremlin
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
