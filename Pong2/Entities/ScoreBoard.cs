using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong2.Entities {
  internal class ScoreBoard {
    public SpriteFont Font;
    public Vector2 Position { get; set; }
    public int Score { get; set; } = 0;

    public ScoreBoard(SpriteFont font, Vector2 position) {
      Font = font;
      Position = position;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
      spriteBatch.DrawString(Font, Score.ToString(), Position, Color.White);
    }
  }
}