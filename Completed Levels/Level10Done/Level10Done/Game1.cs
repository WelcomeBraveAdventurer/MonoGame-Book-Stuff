using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Level10Done
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _gameFont;

        private Texture2D _shipBGTexture;

        private List<Chest> _listOfChests;
        private List<Hero> _listOfHeroes;
        private List<Obstacle> _listOfObstacles;
        private List<HealthPickup> _listOfHealthPickups;
        private List<Gremlin> _listOfGremlins;

        private List<Coin> _listOfCoins;
        private Texture2D _coinSprite;

        private Texture2D _boundingBoxTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _listOfChests = new List<Chest>();
            _listOfHeroes = new List<Hero>();

            _listOfObstacles = new List<Obstacle>();
            _listOfHealthPickups = new List<HealthPickup>();
            _listOfGremlins = new List<Gremlin>();

            _listOfCoins = new List<Coin>();

            _boundingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _boundingBoxTexture.SetData(new Color[] { Color.White });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameFont = Content.Load<SpriteFont>("GameFont");

            _shipBGTexture = Content.Load<Texture2D>("shipBackground");

            _listOfChests.Add(new Chest(100, 100, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed")));
            _listOfChests.Add(new Chest(600, 350, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed")));

            _listOfHeroes.Add(new Hero(300, 100, 10, 5, Content.Load<Texture2D>("hero")));

            _listOfObstacles.Add(new Obstacle(650, 200, Content.Load<Texture2D>("console")));
            _listOfObstacles.Add(new Obstacle(370, 200, Content.Load<Texture2D>("table")));
            _listOfObstacles.Add(new Obstacle(50, 200, Content.Load<Texture2D>("crate")));


            _listOfHealthPickups.Add(new HealthPickup(100, 350, Content.Load<Texture2D>("healthVial")));
            _listOfHealthPickups.Add(new HealthPickup(600, 100, Content.Load<Texture2D>("healthVial")));

            _listOfGremlins.Add(new Gremlin(600, 200, 10, 5, Content.Load<Texture2D>("gremlin")));

            _coinSprite = Content.Load<Texture2D>("coin");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            for (int i = 0; i < _listOfChests.Count; i++)
            {
                _listOfChests[i].Update();
            }

            for (int i = 0; i < _listOfChests.Count; i++)
            {
                if (!_listOfChests[i].IsClosed() && _listOfChests[i].HasCoin())
                {
                    _listOfCoins.Add(new Coin(_listOfChests[i].GetX(),
                _listOfChests[i].GetY() - 40, 1, _coinSprite));
                    _listOfChests[i].RemoveCoin();
                }

            }

            for (int index = 0; index < _listOfObstacles.Count; index++)
            {
                Obstacle tempObstacle = _listOfObstacles[index];
                Hero tempHero = _listOfHeroes[0];
                Rectangle tempObstacleBounds = tempObstacle.GetBounds();
                if (tempObstacleBounds.Intersects(tempHero.GetBounds()))
                {
                    Vector2 tempHeroCenter = tempHero.GetCenterPoint();

                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(35, 0))))
                        tempHero.Block("right");
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(-35, 0))))
                        tempHero.Block("left");
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(0, 35))))
                        tempHero.Block("down");
                    if (tempObstacleBounds.Contains(Vector2.Add(tempHeroCenter, new Vector2(0, -35))))
                        tempHero.Block("up");
                }
            }

            for (int i = 0; i < _listOfHeroes.Count; i++)
            {
                _listOfHeroes[i].Update();
            }



            for (int i = _listOfHealthPickups.Count - 1; i >= 0; i--)
            {
                if (_listOfHealthPickups[i].GetBounds().Intersects(_listOfHeroes[0].GetBounds()))
                {
                    _listOfHeroes[0].Heal(_listOfHealthPickups[i].GetHealthValue());
                    _listOfHealthPickups.RemoveAt(i);
                }
            }

            for (int index = 0; index < _listOfGremlins.Count; index++)
            {
                Gremlin tempGremlin = _listOfGremlins[index];
                Hero tempHero = _listOfHeroes[0];
                if (tempGremlin.GetBounds().Intersects(tempHero.GetBounds()))
                {
                    tempHero.TakeDamage(tempGremlin.DealDamage());

                    Vector2 tempHeroCenter = tempHero.GetCenterPoint();

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

            _spriteBatch.Begin();
            _spriteBatch.Draw(_shipBGTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Hero Health: " + _listOfHeroes[0].GetHealth(), new Vector2(50, 430), Color.White);

            bool showBounds = true;
            if (showBounds)
            {
                for (int i = 0; i < _listOfHeroes.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfHeroes[i].GetBounds(), Color.Red);

                for (int i = 0; i < _listOfObstacles.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfObstacles[i].GetBounds(), Color.Blue);

                for (int i = 0; i < _listOfHealthPickups.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfHealthPickups[i].GetBounds(), Color.White);

                for (int i = 0; i < _listOfChests.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfChests[i].GetBounds(), Color.Green);

                for (int i = 0; i < _listOfGremlins.Count; i++)
                    _spriteBatch.Draw(_boundingBoxTexture, _listOfGremlins[i].GetBounds(), Color.Purple);
            }
            _spriteBatch.End();


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
