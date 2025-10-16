using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Project1Done;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //game textures
    private Texture2D _spaceBGSprite, _gameOverSprite;
    private List<Texture2D> _listOfCardSprites;

    //a list for our cards
    private List<Card> _listOfCards;

    //booleans
    private bool _mouseLeftClicked, _gameOver;

    //more variables to display information and control the flow of our game
    private int _numCardsFlipped, _decisionTimer, _countDownTimer;
    private SpriteFont _timerFont;
    private Song _gameMusic;
    private SoundEffect _cardFlipSound, _matchMadeSound, _gameOverHappySound, _gameOverSadSound; 

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //change the default resolution of our scene window
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 768;

    }

    protected override void Initialize()
    {
        //set up our list of cards and our list of card sprites
        _listOfCards = new List<Card>();
        _listOfCardSprites = new List<Texture2D>();

        //assume the game doesn't start with a mouse click
        _mouseLeftClicked = false;

        //set up our game variables
        _numCardsFlipped = 0;
        _decisionTimer = 0;
        _countDownTimer = 60 * 31;  //30 seconds at 60 frames per second
        _gameOver = false;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //load in our textures and sounds
        _spaceBGSprite = Content.Load<Texture2D>("Sprites/starryBackground");
        _gameOverSprite = Content.Load<Texture2D>("Sprites/gameOver");
        _timerFont = Content.Load<SpriteFont>("Fonts/TimerFont");
        _gameMusic = Content.Load<Song>("Sounds/slimeSong");
        _cardFlipSound = Content.Load<SoundEffect>("Sounds/cardFlipSound");
        _matchMadeSound = Content.Load<SoundEffect>("Sounds/cashRegisterSound");
        _gameOverHappySound = Content.Load<SoundEffect>("Sounds/gameBonusSound"); 
        _gameOverSadSound = Content.Load<SoundEffect>("Sounds/gameOverSound"); 

        //add our card sprites to the card sprite list - in pairs
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/asteroid"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/asteroid"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/coin"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/coin"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/console"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/console"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/crate"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/crate"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/gremlin"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/gremlin"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/healthVial"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/healthVial"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/hero"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/hero"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/star"));
        _listOfCardSprites.Add(Content.Load<Texture2D>("Sprites/star"));

        //randomize the order of the card sprites in the list
        _listOfCardSprites = _listOfCardSprites.OrderBy(x => Random.Shared.Next()).ToList();

        //create our cards in a grid formation
        float startX = 100;
        float startY = 125;
        float xSpace = 125;
        float ySpace = 175;

        //loop through the list of card sprites and create a card for each sprite
        for (int i = 0; i < _listOfCardSprites.Count; i++)
        {
            _listOfCards.Add(new Card(startX + (xSpace * (i % 4)), startY + (ySpace * (i / 4)), Content.Load<Texture2D>("Sprites/cardBack"), _listOfCardSprites[i], _cardFlipSound));
        }


        //if it's not already playing, play the back ground music
        if (MediaPlayer.State != MediaState.Stopped)
        {
            MediaPlayer.Stop();
        }

        MediaPlayer.Play(_gameMusic);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //only update the game if it isn't over
        if (!_gameOver)
        {
            //update our countdown timer
            _countDownTimer--;
            
            //assume there are no cards flipped over
            _numCardsFlipped = 0;

            //check to see how many cards are flipped over - this is where we update the variable _numCardsFlipped
            for (int cardIndex = 0; cardIndex < _listOfCards.Count; cardIndex++)
            {
                //if a card is flipped, we make a note of it
                if (_listOfCards[cardIndex].IsFlipped())
                    _numCardsFlipped++;

                //if there are two cards flipped, we start the decision timer
                if (_decisionTimer <= 0 && _numCardsFlipped >= 2)
                    _decisionTimer = 30;
            }

            //if the decision timer is running, we decrement it
            if (_decisionTimer > 0)
            {
                _decisionTimer--;
                if (_decisionTimer == 0)
                {
                    //Step 1. Find the flipped cards
                    Card tempCard1 = null;
                    Card tempCard2 = null;
                    for (int cardIndex = 0; cardIndex < _listOfCards.Count; cardIndex++)
                    {
                        if (_listOfCards[cardIndex].IsFlipped() && tempCard1 == null)
                            tempCard1 = _listOfCards[cardIndex];
                        else if (_listOfCards[cardIndex].IsFlipped() && tempCard2 == null)
                            tempCard2 = _listOfCards[cardIndex];
                    }
                    //Step 2. See if there is a match
                    if (tempCard1 != null && tempCard2 != null)
                    {
                        //Step 3. Remove matched card.
                        if (tempCard1.GetCardType() == tempCard2.GetCardType())
                        {
                            _matchMadeSound.Play();
                            _listOfCards.Remove(tempCard1);
                            _listOfCards.Remove(tempCard2);
                        }
                        //Step 4. Flip unmatched cards back over
                        else
                        {
                            tempCard1.Flip();
                            tempCard2.Flip();
                        }
                    }
                }
            }


            //get the current state of the mouse
            MouseState currentMouseState = Mouse.GetState();

            //check to see if the left mouse button is clicked and if it is,
            //check to see if a card was clicked on
            for (int cardIndex = 0; cardIndex < _listOfCards.Count; cardIndex++)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed
                    && !_mouseLeftClicked
                    && _listOfCards[cardIndex].GetBounds().Contains(currentMouseState.Position)
                    && _numCardsFlipped < 2)
                {
                    _mouseLeftClicked = true;
                    _listOfCards[cardIndex].Flip();
                }
            }

            //reset the mouse click boolean if the left mouse button is released
            if (currentMouseState.LeftButton != ButtonState.Pressed)
            {
                _mouseLeftClicked = false;
            }

            //check to see if the game is over and if it is play a sound to let the player know if they
            //won (happy) or lost (sad)
            if (_listOfCards.Count <= 0 || _countDownTimer <=0)
            {
                if (_countDownTimer > 0 && !_gameOver)
                    _gameOverHappySound.Play();
                if (_countDownTimer <= 0 && !_gameOver)
                    _gameOverSadSound.Play();
                
                //make the game over official
                _gameOver = true;
                
            }
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //start drawing
        _spriteBatch.Begin();

        //draw the background
        _spriteBatch.Draw(_spaceBGSprite, new Vector2(0, 0), Color.White);

        //draw the timer if it's not game over.
        if (!_gameOver)
            _spriteBatch.DrawString(_timerFont, "" + _countDownTimer / 60, new Vector2(900 - _timerFont.MeasureString("" + _countDownTimer / 60).X/2, 400 - _timerFont.MeasureString("" + _countDownTimer / 60).Y / 2), Color.White);

        //if the game is over, draw the game over sprite
        if (_gameOver)
        {
            _spriteBatch.Draw(_gameOverSprite, new Vector2(_graphics.PreferredBackBufferWidth / 2 -
                _gameOverSprite.Width / 2, _graphics.PreferredBackBufferHeight / 2 - _gameOverSprite.Height / 2), Color.White);
        }

        _spriteBatch.End();

        //draw the cards if the game isn't over
        if (!_gameOver)
        {
            for (int cardIndex = 0; cardIndex < _listOfCards.Count; cardIndex++)
            {
                _listOfCards[cardIndex].Draw(_spriteBatch);
            }
        }

        base.Draw(gameTime);
    }
}
