using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;


namespace Project3Start
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _gameFont, _heroFont;
        private Texture2D _shipBGTexture;

        private Song _spaceStationSong, _groovingSong;
        private SoundEffect _coinCollectSound, _healthPickupSound;

        private List<Chest> _listOfChests;
        private List<Hero> _listOfHeroes;
        private List<Obstacle> _listOfObstacles;
        private List<HealthPickup> _listOfHealthPickups;
        private List<Gremlin> _listOfGremlins;
        private List<Coin> _listOfCoins;

        private Texture2D _coinSpriteSheet;

        private int _heroCoinCount;

        private Texture2D _boundingBoxTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _listOfChests = new List<Chest>();
            _listOfHeroes = new List<Hero>();

            _listOfObstacles = new List<Obstacle>();
            _listOfHealthPickups = new List<HealthPickup>();
            _listOfGremlins = new List<Gremlin>();

            _listOfCoins = new List<Coin>();


            _boundingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _boundingBoxTexture.SetData(new Color[] { Color.White });

            _heroCoinCount = 0;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameFont = Content.Load<SpriteFont>("GameFont");
            _heroFont = Content.Load<SpriteFont>("HeroFont");

            _shipBGTexture = Content.Load<Texture2D>("shipBackground");

            _listOfChests.Add(new Chest(100, 100, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed"), Content.Load<SoundEffect>("cashRegisterSound")));
            _listOfChests.Add(new Chest(600, 350, Content.Load<Texture2D>("chestOpen"), Content.Load<Texture2D>("chestClosed"), Content.Load<SoundEffect>("cashRegisterSound")));

            _listOfHeroes.Add(new Hero(300, 100, 10, 5, Content.Load<Texture2D>("heroSpriteSheet"), Content.Load<SoundEffect>("footstepsBoots"), Content.Load<SoundEffect>("punchSound"), Content.Load<SoundEffect>("pixelDeathSound"), Content.Load<SpriteFont>("HeroFont")));


            _listOfObstacles.Add(new Obstacle(650, 200, Content.Load<Texture2D>("console")));
            _listOfObstacles.Add(new Obstacle(370, 200, Content.Load<Texture2D>("table")));
            _listOfObstacles.Add(new Obstacle(50, 200, Content.Load<Texture2D>("crate")));


            _listOfHealthPickups.Add(new HealthPickup(100, 350,
                                     Content.Load<Texture2D>("healthSpriteSheet")));
            _listOfHealthPickups.Add(new HealthPickup(600, 100,
                                     Content.Load<Texture2D>("healthSpriteSheet")));



            _listOfGremlins.Add(new Gremlin(550, 160, 10, 2, Content.Load<Texture2D>("gremlinSpriteSheet"), Content.Load<SpriteFont>("HeroFont")));

            _coinSpriteSheet = Content.Load<Texture2D>("coinSpriteSheet");


            _spaceStationSong = Content.Load<Song>("spaceStationSong");

            _spaceStationSong = Content.Load<Song>("spaceStationSong");
            _groovingSong = Content.Load<Song>("groovingSong");

            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop(); // stop current audio playback if playing or paused.
            }

            MediaPlayer.Volume = 0.25f;
            MediaPlayer.IsRepeating = true;


            Random rng = new Random();
            if (rng.Next(1, 101) < 50)
                MediaPlayer.Play(_spaceStationSong);
            else
                MediaPlayer.Play(_groovingSong);

            _coinCollectSound = Content.Load<SoundEffect>("coinSound");
            _healthPickupSound = Content.Load<SoundEffect>("gameBonus");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < _listOfChests.Count; i++)
            {
                _listOfChests[i].Update();
            }

            for (int i = 0; i < _listOfChests.Count; i++)
            {
                if (!_listOfChests[i].IsClosed() && _listOfChests[i].HasCoin())
                {
                    _listOfCoins.Add(new Coin(_listOfChests[i].GetX(),
                _listOfChests[i].GetY() - 40, 1, _coinSpriteSheet));
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
                for (int gIndex = 0; gIndex < _listOfGremlins.Count; gIndex++)
                {

                    if (_listOfGremlins[gIndex].GetBounds().Intersects(tempObstacle.GetBounds()))
                    {
                        _listOfGremlins[gIndex].ChangeDirection();
                    }
                }
            }


            for (int i = 0; i < _listOfHeroes.Count; i++)
            {
                _listOfHeroes[i].Update();
            }

            for (int i = 0; i < _listOfHealthPickups.Count; i++)
            {
                _listOfHealthPickups[i].Update();
            }

            for (int i = 0; i < _listOfCoins.Count; i++)
            {
                _listOfCoins[i].Update();
            }



            for (int i = 0; i < _listOfChests.Count; i++)
            {
                Chest tempChest = _listOfChests[i];
                Hero tempHero = _listOfHeroes[0];
                if (tempChest.IsClosed() && tempChest.GetBounds().Intersects(tempHero.GetBounds()))
                {
                    tempChest.OpenChest();
                }
            }


            for (int i = _listOfHealthPickups.Count - 1; i >= 0; i--)
            {
                if (_listOfHealthPickups[i].GetBounds().Intersects(_listOfHeroes[0].GetBounds()))
                {
                    _listOfHeroes[0].Heal(_listOfHealthPickups[i].GetHealthValue());
                    _listOfHealthPickups.RemoveAt(i);
                    _healthPickupSound.Play();
                }
            }

            for (int i = _listOfCoins.Count - 1; i >= 0; i--)
            {
                if (_listOfCoins[i].GetBounds().Intersects(_listOfHeroes[0].GetBounds()))
                {
                    _heroCoinCount++;
                    _listOfCoins.RemoveAt(i);
                    _coinCollectSound.Play();
                }
            }



            for (int index = 0; index < _listOfGremlins.Count; index++)
            {
                _listOfGremlins[index].Update();

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

            for (int index = _listOfGremlins.Count - 1; index >= 0; index--)
            {
                if (_listOfGremlins[index].IsDead())
                {
                    _listOfGremlins.RemoveAt(index);
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Initialize();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_shipBGTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_gameFont, "Coins Collected: " + _heroCoinCount, new Vector2(50, 430), Color.White);


            bool showBounds = false;
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
