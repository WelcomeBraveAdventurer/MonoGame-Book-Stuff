using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Project1Done
{
    class Card
    {
        //we need a few attributes to define the state of our card - some booleans, some sprites, some coordinates,
        //and even some sounds
        private bool _isFlipped;
        private Texture2D _cardSprite1, _cardSprite2;
        private float _cardX, _cardY;
        private float _cardScale1, _cardScale2;
        private SoundEffect _cardFlipSound;

        //constructor to initialize our card
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

        //get the state of our card's flip status
        public bool IsFlipped() { return _isFlipped; }

        //get the type of card (based on the name of the second sprite)
        public string GetCardType() { return _cardSprite2.Name; }

        //get the bounding rectangle of our card (used for mouse click detection)
        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)(_cardX - (_cardSprite1.Width / 2 * _cardScale1)),
                (int)(_cardY - (_cardSprite1.Height / 2 * _cardScale1)),
                (int)(_cardSprite1.Width * _cardScale1),
                (int)(_cardSprite1.Height * _cardScale1));
        }

        //flip the card and play a sound if it's being flipped to face up
        public void Flip()
        {
            if (!_isFlipped)
                _cardFlipSound.Play();

            _isFlipped = !_isFlipped;
        }

        //draw the card (face up or face down based on its state)
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
