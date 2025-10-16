using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Project2Done
{
    internal class WallBuilder
    {
        //this is the most complicated class in the bunch. The Wallbuilder spawns all of the obstacles
        //in a "wall" formation. The destroyer interacts with the individual obstacles to carve a path
        //through the wall. The player interacts with the obstacles to record a crash (on collision).
        //coins (pick ups) are occaisionally left behind during the carving. coins are picked up
        //but the player. All these activites happen in here!!

        //a list is like an array but more convenient. We're going to keep a list of all
        //the obstacles and a list of all the coins in our scene. This way we can add to the list
        //and remove from the list as-needed!
        private List<Block> _listOfObstacles;
        private List<Coin> _listOfCoins;

        //since the wall builder spawns both obstacles and coins, we need to pass those textures in
        private Texture2D _obstacleSprite, _coinSprite;

        //this is the frequency with which we add a column of obstacles
        private float _repeatTime;

        //good ol'rng to keep things interesting
        private Random _rng;

        //argumented constructor, but with only two arguments, everything else is "hard-coded"
        public WallBuilder(Texture2D obstacleSprite, Texture2D coinTexture)
        {
            //set by argument
            _obstacleSprite = obstacleSprite;  
            _coinSprite = coinTexture; 

            //hard-coded
            _listOfObstacles = new List<Block>(); //initialize obstacle list
            _listOfCoins = new List<Coin>();  //initialize coin list
            _repeatTime = 1.0f; //frequence of obstacle column creation (don't mess with this
                                //without also messing with the obstacle speed).

            _rng = new Random(); //initialize rng
        }


        //this is a method that simply waits an alotted amount of time and then creates a column
        //of obstacles.
        public void Update(GameTime gameTime)
        {
            //here is the waiting
            if (_repeatTime > 0)
            {
                _repeatTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;  //wait
                if (_repeatTime <= 0)
                {
                    _repeatTime = 0;

                    //here is the column. Notice how the x-coordinate (1400) is to the right of
                    //our 1200 pixel screen. We want the obstacles to spawn off-screen. Then
                    //we use the i-coordinate to create the column of 20 stacked obstacles all
                    //with x speed of 1.5f.
                    for (int i = 0; i < 20; i++)
                    {
                        _listOfObstacles.Add(new Block(1400, -100 + i * 50, _obstacleSprite, 1.5f));
                    }

                    _repeatTime = 0.55f; //reset the timer!!!
                }

            }

        }

        //this is a method we'll call in our game's update. It is designed to help the destroyer
        //carve the path (by looking for collisions) and to help determine if the player has 
        //hit an obstacle. We do this here because the WallBuilder keeps track of all the obstacles
        //and coins (we'll deal with coins after this method).
        public void DestroyObstaclesAndCollideWithPlayer(Destroyer destroyer, Player player)
        {
            //flag to see if we're going to destroy an obstacle
            bool flagForDestroy = false;

            //get the destroy object's x and y (we need to know where it is)
            float destroyerX = destroyer.getX();
            float destroyerY = destroyer.getY();

            //this is a meaty loop - for EVERY obstacle in the game...
            for (int i = 0; i < _listOfObstacles.Count; i++)
            {
                //grab the obstacle
                Block o = _listOfObstacles.ElementAt(i);

                //get the obstacle's x, y, width and height
                float blockX = o.getX();
                float blockY = o.getY();
                float blockWidth = o.getWidth();
                float blockHeight = o.getHeight();

                //only do this part if the obstacle hasn't yet entered our view. We want to carve the
                //path prior to the obstacles entering our play area, not after. This will save
                //a bit of computation since we won't try to carve a path in obstacles that have
                //already entered the scene.
                if (blockX > 1200)
                {
                    //check to see if the obstacle in question is in contact with the destroyer.
                    //It's worth noting that the destroyer is only an x/y pair, it doesn't have
                    //height and width. The * 2f below helps us to carve a slightly larger path
                    //to avoid frustrating out player.
                    if (destroyerX > blockX && destroyerY > blockY && destroyerX < blockX + blockWidth * 2f && destroyerY < blockY + blockHeight * 2f)
                        flagForDestroy = true;

                    //here's the thing - we need to destroy blocks that have come in contact with 
                    //the destroy AND if they have left the scene. This second part is key to making 
                    //sure that we don't have a zillion blocks that have passed through the scene -
                    //that will eventually slow our game down or crash it.
                    if (o.getX() < -50 || flagForDestroy)
                    {
                        _listOfObstacles.RemoveAt(i);  //obstacle was hit by the destroyer or is off-screen
                                                //Get rid of it!
                        
                        //cute little bit of code that says a destroyed obstacle has a 5% chance
                        //of spawning a pick up coin. Awwww, that's cute.
                        if(_rng.NextDouble() < 0.05)
                        {
                            _listOfCoins.Add(new Coin(blockX, blockY - 20, 1.5f, _coinSprite));
                        }
                    }

                    //reset the flag for the next block to check
                    flagForDestroy = false;
                }
                else
                {   
                    //it takes this much code to see if an obstacle has hit our player. 
                    //If it has, set the player crashed variable to stop the action.
                    if (player.GetBounds().Intersects(o.GetBounds())) 
                        player.setCrashed();
                }
            }
        }

        //similar, but more consice than what we did above, check EVERY coin in the game to see
        //if it's overlapping the player. If it is, pick it up!! and remove it from the list.
        public void DestroyCoins(Player player, ScoreKeeper scoreKeeper)
        {
            for (int i = 0; i < _listOfCoins.Count; i++)
            {
                Coin s = _listOfCoins.ElementAt(i);

                if (player.GetBounds().Intersects(s.GetBounds()))
                {
                    _listOfCoins.RemoveAt(i);
                    scoreKeeper.updateScore(100);
                }
            }
        }

        //move all the obstacles and coins by calling their update functions. This will be called
        //in Game1.cs update().
        public void MoveObstaclesAndCoins()
        {
            foreach (Block o in _listOfObstacles)
            {
                o.Update();
            }

            foreach (Coin s in _listOfCoins)
            {
                s.Update();
            }
        }

        //draw all our obstacles and coins <3
        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Block o in _listOfObstacles)
            {
                o.Draw(_spriteBatch);
            }

            foreach (Coin s in _listOfCoins)
            {
                s.Draw(_spriteBatch);
            }
        }

    }
}
