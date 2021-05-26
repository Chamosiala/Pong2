using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Pong2.Entities {
  internal class Border : ICollidable {
    public const float BORDER_THICKNESS = 2.5f;
    public Vector4 Position { get; set; }

    public Border(Vector4 position) {
      Position = position;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
      spriteBatch.DrawLine(Position.X, Position.Y, Position.W, Position.Z, Color.White, BORDER_THICKNESS);
    }

    public Rectangle CollisionBox {
      get {
        var box = new Rectangle(
          (int) Math.Round(Position.X),
          (int) Math.Round(Position.Y),
          (int) Math.Round(Position.W),
          (int) Math.Round(BORDER_THICKNESS)
        );

        return box;
      }
    }
  }
}