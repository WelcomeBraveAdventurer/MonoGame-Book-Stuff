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

    private int _numCardsFlipped, _decisionTimer, _countDownTimer;
    private SpriteFont _timerFont;
    private Song _gameMusic;
    private SoundEffect _cardFlipSound, _matchMadeSound, _gameOverHappySound, _gameOverSadSound; 

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 768;

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _listOfCards = new List<Card>();
        _listOfCardSprites = new List<Texture2D>();

        _mouseLeftClicked = false;

        _numCardsFlipped = 0;
        _decisionTimer = 0;
        _countDownTimer = 60 * 31;
        _gameOver = false;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

_spaceBGSprite = Content.Load<Texture2D>("Sprites/starryBackground");
_gameOverSprite = Content.Load<Texture2D>("Sprites/gameOver");
_timerFont = Content.Load<SpriteFont>("Fonts/TimerFont");

_gameMusic = Content.Load<Song>("Sounds/slimeSong");
_cardFlipSound = Content.Load<SoundEffect>("Sounds/cardFlipSound");
_matchMadeSound = Content.Load<SoundEffect>("Sounds/cashRegisterSound");
_gameOverHappySound = Content.Load<SoundEffect>("Sounds/gameBonusSound"); 
_gameOverSadSound = Content.Load<SoundEffect>("Sounds/gameOverSound"); 

if(MediaPlayer.State != MediaState.Stopped)
{
    MediaPlayer.Stop();
}

MediaPlayer.Play(_gameMusic);

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

        _listOfCardSprites = _listOfCardSprites.OrderBy(x => Random.Shared.Next()).ToList();

        float startX = 100;
        float startY = 125;
        float xSpace = 125;
        float ySpace = 175;

        for (int i = 0; i < _listOfCardSprites.Count; i++)
        {
            _listOfCards.Add(new Card(startX + (xSpace * (i % 4)), startY + (ySpace * (i / 4)), Content.Load<Texture2D>("Sprites/cardBack"), _listOfCardSprites[i], _cardFlipSound));
        }



        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (!_gameOver)
        {
            _countDownTimer--;
            _numCardsFlipped = 0;
            for (int cardIndex = 0; cardIndex < _listOfCards.Count; cardIndex++)
            {
                if (_listOfCards[cardIndex].IsFlipped())
                    _numCardsFlipped++;

                if (_decisionTimer <= 0 && _numCardsFlipped >= 2)
                    _decisionTimer = 30;
            }

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



            MouseState currentMouseState = Mouse.GetState();

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

            if (currentMouseState.LeftButton != ButtonState.Pressed)
            {
                _mouseLeftClicked = false;
            }

            if (_listOfCards.Count <= 0 || _countDownTimer <=0)
            {
                if (_countDownTimer > 0 && !_gameOver)
                    _gameOverHappySound.Play();
                if (_countDownTimer <= 0 && !_gameOver)
                    _gameOverSadSound.Play();
                
                _gameOver = true;
                
            }
        }


        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

_spriteBatch.Begin();
_spriteBatch.Draw(_spaceBGSprite, new Vector2(0, 0), Color.White);

if(!_gameOver)
    _spriteBatch.DrawString(_timerFont, "" + _countDownTimer / 60, new Vector2(900 - _timerFont.MeasureString("" + _countDownTimer / 60).X/2, 400 - _timerFont.MeasureString("" + _countDownTimer / 60).Y / 2), Color.White);

if (_gameOver)
{
    _spriteBatch.Draw(_gameOverSprite, new Vector2(_graphics.PreferredBackBufferWidth / 2 -
        _gameOverSprite.Width / 2, _graphics.PreferredBackBufferHeight / 2 - _gameOverSprite.Height / 2), Color.White);
    //_spriteBatch.DrawString(_gameFont, "Press R to restart.", new Vector2(400 - _gameFont.MeasureString("Press R to restart.").X / 2, 350), Color.White);
}

_spriteBatch.End();

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
