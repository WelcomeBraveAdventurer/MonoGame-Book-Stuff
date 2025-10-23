using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Project3Start
{
    enum HeroStates
    {
        idle, shooting, moving, dying
    }

    internal class Hero
    {
        //attribute variables to define the state of our hero
        private int _heroHealth;
        private float _heroX, _heroY, _heroSpeed;
        private Texture2D _heroSpriteSheet;

        private SpriteFont _heroFont;

        private bool _heroMovingRight, _heroIsShooting;    

        private bool _heroBlockLeft, _heroBlockRight, _heroBlockUp, _heroBlockDown, _heroIsDead;

        private HeroStates _heroState;

        private int _animationTimer, _animationIndex, _animationRow;

        private SoundEffect _heroWalkingSound, _heroHurtSound, _heroDeathSound;
        private SoundEffectInstance _heroWalkingSoundInstance, _heroHurtSoundInstance, _heroDeathSoundInstance
            ;

        //a constructor to set the default attribute values
        public Hero(float heroX, float heroY, int heroHealth, float heroSpeed, Texture2D heroSpriteSheet, SoundEffect heroWalkingSound, SoundEffect heroHurtSound, SoundEffect heroDeathSound, SpriteFont heroFont)
        {

            _heroX = heroX;
            _heroY = heroY;
            _heroHealth = heroHealth;
            _heroSpeed = heroSpeed;
            _heroSpriteSheet = heroSpriteSheet;
            _heroWalkingSound = heroWalkingSound;
            _heroHurtSound = heroHurtSound;
            _heroDeathSound = heroDeathSound;
            _heroWalkingSoundInstance = _heroWalkingSound.CreateInstance();
            _heroHurtSoundInstance = _heroHurtSound.CreateInstance();
            _heroDeathSoundInstance = _heroDeathSound.CreateInstance();
            _heroWalkingSoundInstance.Volume = 0.5f;

            _heroFont = heroFont;

            _heroMovingRight = true;    //start out looking right
            _animationTimer = 0;
            _animationIndex = 0;
            _animationRow = 0;

            _heroState = HeroStates.idle;
            _heroIsShooting = false;

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
        public void TakeDamage(int damageAmount) { 
            
            _heroHealth -= damageAmount;
            if (_heroHurtSoundInstance.State == SoundState.Stopped)
            {
                _heroHurtSoundInstance.Play();
            }

        }
        public void Heal(int healAmount) { _heroHealth += healAmount; }


        //helper methods that don’t affect attribute values – this
        //method uses a Random object to return a random heroic damage value
        //between 1 and 3.
        public int DealDamage() { Random rng = new Random(); return rng.Next(1, 4); }

        public Rectangle GetBounds() { return new Rectangle((int)_heroX + 10, (int)_heroY + 10, 40, 50); }

public bool IsMovingRight() { return _heroMovingRight;  }

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
            _heroState = HeroStates.idle;
            //get the current state of the keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (_heroHealth > 0 && (currentKeyboardState.IsKeyDown(Keys.Space)))
            {
                _heroState = HeroStates.shooting;
            }
            else if (_heroHealth > 0 && !_heroIsShooting)
            {

                //standard WASD keys
                if (!_heroBlockLeft && currentKeyboardState.IsKeyDown(Keys.A))
                {
                    _heroX -= _heroSpeed;
                    _heroMovingRight = false;  //A is left, not right
                    _heroState = HeroStates.moving;
                }
                if (!_heroBlockRight && currentKeyboardState.IsKeyDown(Keys.D))
                {
                    _heroX += _heroSpeed;
                    _heroMovingRight = true;  //D is right
                    _heroState = HeroStates.moving;
                }
                if (!_heroBlockUp && currentKeyboardState.IsKeyDown(Keys.W))
                {
                    _heroY -= _heroSpeed;
                    _heroState = HeroStates.moving;
                }
                if (!_heroBlockDown && currentKeyboardState.IsKeyDown(Keys.S))
                {
                    _heroY += _heroSpeed;
                    _heroState = HeroStates.moving;
                }

            }
            else
            {
                _heroState = HeroStates.dying;
                if (!_heroIsDead)
                {
                    _animationIndex = 0;
                    _heroIsDead = true;
                    if (_heroDeathSoundInstance.State == SoundState.Stopped)
                    {
                        _heroDeathSoundInstance.Play();
                    }
                }
                

            }

            _animationTimer++;
            if (_animationTimer >= 5)
            {

                _animationIndex++;
                if ((_heroState == HeroStates.idle || _heroState == HeroStates.shooting)
                                                   && _animationIndex >= 4)
                    _animationIndex = 0;
                if (_heroState == HeroStates.moving && _animationIndex >= 6)
                    _animationIndex = 0;

                if (_heroState == HeroStates.dying && _animationIndex >= 5)
                    _animationIndex = 5;

                if (_heroState == HeroStates.idle)
                    _animationRow = 0;
                if (_heroState == HeroStates.shooting)
                    _animationRow = 1;
                if (_heroState == HeroStates.moving)
                    _animationRow = 2;
                if (_heroState == HeroStates.dying)
                    _animationRow = 3;

                _animationTimer = 0;

            }

            if(_heroState == HeroStates.moving &&  _heroWalkingSoundInstance.State == SoundState.Stopped)
            {
                _heroWalkingSoundInstance.Play();
            }
            else if (_heroState != HeroStates.moving)
            {
                _heroWalkingSoundInstance.Stop();
            }


            _heroBlockLeft = false;
            _heroBlockRight = false;
            _heroBlockUp = false;
            _heroBlockDown = false;
        }

        public HeroStates GetHeroState() { return _heroState; }

        //A method to draw our hero in the scene
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(_heroFont, "Health: " + _heroHealth, new Vector2(_heroX + 32 - _heroFont.MeasureString("Health: " + _heroHealth).X / 2, _heroY - 20), Color.White);

            //using the 9-argument version so we can flip the hero based on the direction they are moving
            if (_heroMovingRight)
                spriteBatch.Draw(_heroSpriteSheet, new Vector2(_heroX, _heroY), new Rectangle(_animationIndex * 64, _animationRow * 64, 64, 64), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_heroSpriteSheet, new Vector2(_heroX, _heroY), new Rectangle(_animationIndex * 64, _animationRow * 64, 64, 64), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
        }

    }

}

