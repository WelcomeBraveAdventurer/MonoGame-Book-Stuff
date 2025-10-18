using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Level10Start
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


            for (int i = 0; i < _listOfHeroes.Count; i++)
            {
                _listOfHeroes[i].Update();
            }






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_shipBGTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Hero Health: " + _listOfHeroes[0].GetHealth(), new Vector2(50, 430), Color.White);


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
