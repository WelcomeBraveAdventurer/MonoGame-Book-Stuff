using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CQ11SpaceStart
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //our programs are getting more complicated, we need more attributes!!

        //a font to draw text on the screen and a background texture for our scene
        private SpriteFont _gameFont;
        private Texture2D _shipBGTexture;

        //lists make everything work - chests, heroes, obstacles, health pickups, and gremlins, and coins
        private List<Chest> _listOfChests;
        private List<Hero> _listOfHeroes;
        private List<Obstacle> _listOfObstacles;
        private List<HealthPickup> _listOfHealthPickups;
        private List<Gremlin> _listOfGremlins;
        private List<Coin> _listOfCoins;

        //we're going to pre-load the coin sprite to make it easier to create coins
        private Texture2D _coinSprite;

        //lets track our hero's riches
        private int _heroCoinCount;

        //this will be used to display the hit boxes
        private Texture2D _boundingBoxTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            //prepare the lists!!!
            _listOfChests = new List<Chest>();
            _listOfHeroes = new List<Hero>();
            _listOfObstacles = new List<Obstacle>();
            _listOfHealthPickups = new List<HealthPickup>();
            _listOfGremlins = new List<Gremlin>();
            _listOfCoins = new List<Coin>();

            //this is for drawing the hit boxes
            _boundingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _boundingBoxTexture.SetData(new Color[] { Color.White });

            //sorry hero, you have to EARN the coins
            _heroCoinCount = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the font and background texture
            _gameFont = Content.Load<SpriteFont>("GameFont");
            _shipBGTexture = Content.Load<Texture2D>("shipBackground");

            //create some chests and add them to the list
            _listOfChests.Add(new Chest(100, 100, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed")));
            _listOfChests.Add(new Chest(600, 350, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed")));

            //create a hero and add it to the list
            _listOfHeroes.Add(new Hero(300, 100, 10, 5, Content.Load<Texture2D>("hero")));

            //create some obstacles and add them to the list
            _listOfObstacles.Add(new Obstacle(650, 200, Content.Load<Texture2D>("console")));
            _listOfObstacles.Add(new Obstacle(370, 200, Content.Load<Texture2D>("table")));
            _listOfObstacles.Add(new Obstacle(50, 200, Content.Load<Texture2D>("crate")));

            //create some health pickups and add them to the list
            _listOfHealthPickups.Add(new HealthPickup(100, 350, Content.Load<Texture2D>("healthVial")));
            _listOfHealthPickups.Add(new HealthPickup(600, 100, Content.Load<Texture2D>("healthVial")));

            //create a gremlin and add it to the list
            _listOfGremlins.Add(new Gremlin(600, 200, 10, 2, Content.Load<Texture2D>("gremlin")));

            //load and assign the coin sprite
            _coinSprite = Content.Load<Texture2D>("coin");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //have the chests update themselves
            for (int i = 0; i < _listOfChests.Count; i++)
            {
                _listOfChests[i].Update();
            }

            //check the chests to see if any are open and have a coin inside
            for (int i = 0; i < _listOfChests.Count; i++)
            {
                if (!_listOfChests[i].IsClosed() && _listOfChests[i].HasCoin())
                {
                    //a coin was found, add it to the list!
                    _listOfCoins.Add(new Coin(_listOfChests[i].GetX(), _listOfChests[i].GetY() - 40, 1, _coinSprite));
                    _listOfChests[i].RemoveCoin();
                }

            }

            //check for collisions between the hero and obstacles
            for (int index = 0; index < _listOfObstacles.Count; index++)
            {
                Obstacle tempObstacle = _listOfObstacles[index];
                Hero tempHero = _listOfHeroes[0];
                Rectangle tempObstacleBounds = tempObstacle.GetBounds();
                if (tempObstacleBounds.Intersects(tempHero.GetBounds()))
                {
                    Vector2 tempHeroCenter = tempHero.GetCenterPoint();

                    //stop moving if the hero hits an obstacle. We check the four cardinal directions.
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(35, 0))))
                        tempHero.Block("right");
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(-35, 0))))
                        tempHero.Block("left");
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(0, 35))))
                        tempHero.Block("down");
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(0, -35))))
                        tempHero.Block("up");
                }

                //check for collisions between the gremlin and obstacles
                if (_listOfGremlins[0].GetBounds().Intersects(tempObstacle.GetBounds()))
                {
                    _listOfGremlins[0].ChangeDirection();  //change directions if the gremlin hits an obstacle
                }
            }

            //have the hero update itself
            for (int i = 0; i < _listOfHeroes.Count; i++)
            {
                _listOfHeroes[i].Update();
            }

            //check for collisions between the hero and chests
            for (int i = 0; i < _listOfChests.Count; i++)
            {
                Chest tempChest = _listOfChests[i];
                Hero tempHero = _listOfHeroes[0];
                if (tempChest.IsClosed() && tempChest.GetBounds().Intersects(tempHero.GetBounds()))
                {
                    tempChest.OpenChest();  //open the chest if the hero touches it
                }
            }

            //check for collisions between the hero and health pickups
            for (int i = _listOfHealthPickups.Count - 1; i >= 0; i--)
            {
                if (_listOfHealthPickups[i].GetBounds().Intersects(_listOfHeroes[0].GetBounds()))
                {
                    //hero heals a bit and the health pickup is removed from the list
                    _listOfHeroes[0].Heal(_listOfHealthPickups[i].GetHealthValue());
                    _listOfHealthPickups.RemoveAt(i);
                }
            }

            //did the hero pick up any coins?
            for (int i = _listOfCoins.Count - 1; i >= 0; i--)
            {
                if (_listOfCoins[i].GetBounds().Intersects(_listOfHeroes[0].GetBounds()))
                {
                    _heroCoinCount++;
                    _listOfCoins.RemoveAt(i);
                }
            }

            //finally, check for collisions between the hero and gremlins
            for (int index = 0; index < _listOfGremlins.Count; index++)
            {
                //we only have one gremlin, but we could have more, so we loop through the list
                _listOfGremlins[index].Update();

                Gremlin tempGremlin = _listOfGremlins[index];
                Hero tempHero = _listOfHeroes[0];
                if (tempGremlin.GetBounds().Intersects(tempHero.GetBounds()))
                {
                    tempHero.TakeDamage(tempGremlin.DealDamage());

                    Vector2 tempHeroCenter = tempHero.GetCenterPoint();

                    //this is our hero "bounce back" if they collide with the gremlin.
                    if (tempGremlin.GetBounds().Contains(Vector2.Add(tempHeroCenter, new Vector2(35, 0))))
                        tempHero.EscapeDamage("left");
                    if (tempGremlin.GetBounds().Contains(Vector2.Add(tempHeroCenter, new Vector2(-35, 0))))
                        tempHero.EscapeDamage("right");
                    if (tempGremlin.GetBounds().Contains(Vector2.Add(tempHeroCenter, new Vector2(0, 35))))
                        tempHero.EscapeDamage("up");
                    if (tempGremlin.GetBounds().Contains(Vector2.Add(tempHeroCenter, new Vector2(0, -35))))
                        tempHero.EscapeDamage("down");
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //this code shows the hero health and the coins collected.
            _spriteBatch.Begin();
            _spriteBatch.Draw(_shipBGTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Hero Health: " + _listOfHeroes[0].GetHealth(), new Vector2(50, 430), Color.White);
            _spriteBatch.DrawString(_gameFont, "Coins Collected: " + _heroCoinCount, new Vector2(275, 430), Color.White);

            //this code draws the hit boxes for all the objects in the scene
            bool showBounds = true;
            if (showBounds)
            {
                for (int i = 0; i < _listOfHeroes.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfHeroes[i].GetBounds(), Color.Red);

                for (int i = 0; i < _listOfObstacles.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfObstacles[i].GetBounds(), Color.Blue);

                for (int i = 0; i < _listOfCoins.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfCoins[i].GetBounds(), Color.Yellow);

                for (int i = 0; i < _listOfHealthPickups.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfHealthPickups[i].GetBounds(), Color.White);

                for (int i = 0; i < _listOfChests.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfChests[i].GetBounds(), Color.Green);

                for (int i = 0; i < _listOfGremlins.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfGremlins[i].GetBounds(), Color.Purple);
            }
            _spriteBatch.End();


            //this code calls all our object draw methods
            for (int i = 0; i < _listOfChests.Count; i++)
                _listOfChests[i].Draw(_spriteBatch);

            for (int i = 0; i < _listOfObstacles.Count; i++)
                _listOfObstacles[i].Draw(_spriteBatch);

            for (int i = 0; i < _listOfHealthPickups.Count; i++)
                _listOfHealthPickups[i].Draw(_spriteBatch);

            for (int i = 0; i < _listOfGremlins.Count; i++)
                _listOfGremlins[i].Draw(_spriteBatch);

            for (int i = 0; i < _listOfHeroes.Count; i++)
                _listOfHeroes[i].Draw(_spriteBatch);

            for (int i = 0; i < _listOfCoins.Count; i++)
                _listOfCoins[i].Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
