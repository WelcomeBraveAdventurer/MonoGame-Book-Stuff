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
    internal class Timer
    {
        private TimeSpan _timer;
        private SpriteFont _timerFont;

        public Timer(SpriteFont timerFont)
        {
            _timer = TimeSpan.Zero;
            _timerFont = timerFont;
        }

        public void Update(GameTime gameTime)
        {
            _timer = gameTime.TotalGameTime;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            string text = "Space Timer: ";
            string time = _timer.ToString(@"mm\:ss");
            spritebatch.Begin();
            spritebatch.DrawString(_timerFont, text, new Vector2(10, 10), Color.White);
            spritebatch.DrawString(_timerFont, time, new Vector2(_timerFont.MeasureString(text).X + 10, 10), Color.White);
            spritebatch.End();
        }
    }
}
