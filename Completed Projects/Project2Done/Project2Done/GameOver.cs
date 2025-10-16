using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project2Done
{
    internal class GameOver
    {
        private Texture2D _gameOverSprite;

        public GameOver(Texture2D gameOverSprite)
        {
            _gameOverSprite = gameOverSprite;
        }

        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Begin();
            spritebatch.Draw(_gameOverSprite, new Vector2(400, 300), Color.White);
            spritebatch.End();
        }
    }
}
