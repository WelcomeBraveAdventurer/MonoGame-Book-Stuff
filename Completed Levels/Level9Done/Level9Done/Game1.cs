using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;    //we need this to use lists
using System;    //we need this for our random number generator

namespace Level9Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //we need this for the coins (see Level 9)
        private Texture2D _coinTexture;

        //these are the lists we'll need. Make not of the <type> contained in each list
        private List<Chest> _listOfChests;
        private List<Coin> _listOfCoins;
        private List<Star> _listOfStars;
        private List<Hero> _heroList;

        //more attributes for our stars
        private Texture2D _starSprite;
        private int _numberOfStars;
        private Random _rng;

        //this will be used to add stars. It's for our single-press logic.
        private bool _spacePressed;

        //a handy font, just in case
        private SpriteFont _gameFont;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //create all of our lists and make them ready for use.
            _listOfChests = new List<Chest>();
            _listOfCoins = new List<Coin>();
            _listOfStars = new List<Star>();
            _heroList = new List<Hero>();

            //set up our random object for the stars, and set the number of
            //stars we'll STARt with. :P
            _rng = new Random();
            _numberOfStars = _rng.Next(50, 76);

            //assume the space bar isn't being pressed when the game starts
            _spacePressed = false;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //for our coins to be shiny and beautiful. We won't create the coins until we get to Update();
            _coinTexture = Content.Load<Texture2D>("coin");


            //this is one way to add three chests to our list - you could use a list to do this,
            //like I did for the stars below.
            _listOfChests.Add(new Chest(200, 375, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed"))); //chest at index 0
            _listOfChests.Add(new Chest(375, 375, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed"))); //chest at index 1
            _listOfChests.Add(new Chest(550, 375, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed"))); //chest at index 2



            //set up our star sprite in advance (because there are so many of them)
            _starSprite = Content.Load<Texture2D>("star");

            //use a loop to add stars to the list - could we have used a loop
            //to create our three chests???
            _starSprite = Content.Load<Texture2D>("star");
            for (int i = 0; i < _numberOfStars; i++)
            {
                _listOfStars.Add(new Star(_rng.Next(50, 751), _rng.Next(50, 251),
                                                    _rng.Next(300, 601), _starSprite));
            }



            //a single hero for our lonely hero list
            _heroList.Add(new Hero(350, 275, 300, 5, Content.Load<Texture2D>("hero")));



            //and don't forget the font
            _gameFont = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //let's call each of our chest update methods. There is more than one chest
            //in the list, so we'll call them using their indexes (0, 1, and 2).
            _listOfChests[0].Update();
            _listOfChests[1].Update();
            _listOfChests[2].Update();


            //coin-spawning logic - this would be much shorter if it was done in a loop.
            //deal with chest 0 – if there’s a coin, create it and add it to the coin list
            if (!_listOfChests[0].IsClosed() && _listOfChests[0].HasCoin())
            {
                float tempCoinX = _listOfChests[0].GetX();
                float tempCoinY = _listOfChests[0].GetY() - 40;
                _listOfCoins.Add(new Coin(tempCoinX, tempCoinY, 1, _coinTexture));
                _listOfChests[0].RemoveCoin();
            }

            //deal with chest 1 – if there’s a coin, create it and add it to the coin list
            if (!_listOfChests[1].IsClosed() && _listOfChests[1].HasCoin())
            {
                _listOfCoins.Add(new Coin(_listOfChests[1].GetX(),
            _listOfChests[1].GetY() - 40, 1, _coinTexture));
                _listOfChests[1].RemoveCoin();
            }

            //deal with chest 2 – if there’s a coin, create it and add it to the coin list
            if (!_listOfChests[2].IsClosed() && _listOfChests[2].HasCoin())
            {
                _listOfCoins.Add(new Coin(_listOfChests[2].GetX(),
            _listOfChests[2].GetY() - 40, 1, _coinTexture));
                _listOfChests[2].RemoveCoin();
            }



            //star logic - an Update() loop
            for (int index = 0; index < _listOfStars.Count; index++)
            {
                _listOfStars[index].Update();
            }

            //check for expired stars and remove them - notice this loop runs backwards
            for (int index = _listOfStars.Count - 1; index >= 0; index--)
            {
                if (_listOfStars[index].IsExpired())
                {
                    _listOfStars.RemoveAt(index);
                }
            }

            //to add new stars to our list, we'll use the space bar
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_spacePressed)
            {
                _spacePressed = true;
                for (int i = 0; i < 20; i++)
                    _listOfStars.Add(new Star(_rng.Next(50, 751), _rng.Next(50, 251), _rng.Next(300, 601), _starSprite));
            }

            //don't forget to reset the space bar
            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _spacePressed = false;
            }


            //hero logic - there is only one hero, we're testing to see what happens if they perish.
            for (int i = 0; i < _heroList.Count; i++)
            {
                _heroList[i].Update();              //regular hero Update() method call


                _heroList[i].TakeDamage(1);         //hero takes 1 damage per frame. Hero will last 5 seconds in the scene.


                if (_heroList[i].GetHealth() <= 0)
                    _heroList.RemoveAt(i);          //the hero will "die" after 5 seconds, remove them from the list
                                                    //and the game will keep running
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //as always, we need to call our objects Draw() method. Now we need to do it for
            //each chest in the list. This could be put in a loop, once you know how the list loops work.
            _listOfChests[0].Draw(_spriteBatch);
            _listOfChests[1].Draw(_spriteBatch);
            _listOfChests[2].Draw(_spriteBatch);


            //draw our coins!!
            for (int index = 0; index < _listOfCoins.Count; index++)
            {
                _listOfCoins[index].Draw(_spriteBatch);
            }

            //draw our stars!
            for (int index = 0; index < _listOfStars.Count; index++)
            {
                _listOfStars[index].Draw(_spriteBatch);
            }

            //draw our hero (or not, if they have expired)
            for (int i = 0; i < _heroList.Count; i++)
            {
                _heroList[i].Draw(_spriteBatch);
            }


            //this is our star-counter, this is the only drawing that Game1 is doing, the rest comes from the objects.
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_gameFont, "Number of stars: " + _listOfStars.Count, new Vector2(25, 25), Color.White);
            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
