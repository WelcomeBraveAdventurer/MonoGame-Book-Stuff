using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Project1Done
{
    class Card
    {
        private bool _isFlipped;
        private Texture2D _cardSprite1, _cardSprite2;
        private float _cardX, _cardY;
        private float _cardScale1, _cardScale2;
        private SoundEffect _cardFlipSound;
        public Card(float cardX, float cardY, Texture2D cardSprite1, Texture2D cardSprite2, SoundEffect cardFlipSound)
        {
            _isFlipped = false;

            _cardX = cardX;
            _cardY = cardY;
            _cardSprite1 = cardSprite1;
            _cardSprite2 = cardSprite2;

            _cardScale1 = .15f;
            _cardScale2 = 2f;
            _cardFlipSound = cardFlipSound;
        }

        public bool IsFlipped() { return _isFlipped; }

        public string GetCardType() { return _cardSprite2.Name; }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)(_cardX - (_cardSprite1.Width / 2 * _cardScale1)),
                (int)(_cardY - (_cardSprite1.Height / 2 * _cardScale1)),
                (int)(_cardSprite1.Width * _cardScale1),
                (int)(_cardSprite1.Height * _cardScale1));
        }

        public void Flip()
        {
            if (!_isFlipped)
                _cardFlipSound.Play();

            _isFlipped = !_isFlipped;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            if (!_isFlipped)
                spriteBatch.Draw(_cardSprite1, new Vector2(_cardX, _cardY), null, Color.White, 0, new Vector2(_cardSprite1.Width / 2, _cardSprite1.Height / 2), _cardScale1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_cardSprite2, new Vector2(_cardX, _cardY), null, Color.White, 0, new Vector2(_cardSprite2.Width / 2, _cardSprite2.Height / 2), _cardScale2, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
