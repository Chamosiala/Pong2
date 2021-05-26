using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong2.Entities {
  internal interface IGameEntity {
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
  }
}