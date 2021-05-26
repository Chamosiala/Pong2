using Microsoft.Xna.Framework;

namespace Pong2.Entities {
  internal interface ICollidable {
    Rectangle CollisionBox { get; }
  }
}