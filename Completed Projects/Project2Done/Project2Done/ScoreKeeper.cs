using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2Done
{
    internal class ScoreKeeper
    {
        private int _score;
        private SpriteFont _scoreFont;

        public ScoreKeeper(SpriteFont scoreFont)
        {
            _score = 0;
            _scoreFont = scoreFont;
        }

        public void updateScore(int score)
        {
            _score += score;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            string text = "Space Score: ";
            spritebatch.Begin();
            spritebatch.DrawString(_scoreFont, text, new Vector2(300, 10), Color.White);
            spritebatch.DrawString(_scoreFont, _score + "", new Vector2(_scoreFont.MeasureString(text).X + 310, 10), Color.White);
            spritebatch.End();
        }
    }
}
