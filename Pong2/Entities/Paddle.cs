using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong2.Graphics;

namespace Pong2.Entities {
  internal class Paddle : IGameEntity, ICollidable {
    public Sprite PaddleSprite;

    public enum PaddleState {
      Stopped,
      MovingUp,
      MovingDown
    }

    public const int PaddleSpritePosX = 145;
    public const int PaddleSpritePosY = 470;
    public const int PaddleSpriteWidth = 19;
    public const int PaddleSpriteHeight = 69;

    public Vector2 Position { get; set; }
    public float PaddleStartPosY { get; set; }
    public float PaddleStartPosX { get; set; }
    public PaddleState State { get; private set; }

    private const float VerticalVelocity = 500f;

    public Rectangle CollisionBox {
      get {
        var box = new Rectangle(
          (int) Math.Round(Position.X),
          (int) Math.Round(Position.Y),
          PaddleSpriteWidth,
          PaddleSpriteHeight
        );

        return box;
      }
    }

    public Paddle(Texture2D spriteSheet, Vector2 position) {
      State = PaddleState.Stopped;

      PaddleSprite = new Sprite(spriteSheet, PaddleSpritePosX, PaddleSpritePosY, PaddleSpriteWidth, PaddleSpriteHeight);

      Position = position;
      PaddleStartPosX = position.X;
      PaddleStartPosY = position.Y;
    }

    public void Update(GameTime gameTime) {
      switch (State) {
        case PaddleState.MovingUp:
          MoveUp(gameTime);
          break;
        case PaddleState.MovingDown:
          MoveDown(gameTime);
          break;
      }
    }

    public void Stop() {
      State = PaddleState.Stopped;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
      PaddleSprite.Draw(spriteBatch, Position);
    }

    public void ChangeStateToMovingUp() {
      State = PaddleState.MovingUp;
    }

    public void ChangeStateToMovingDown() {
      State = PaddleState.MovingDown;
    }


    private bool IsWithinUpperBound() {
      return Position.Y > 55;
    }

    private bool IsWithinLowerBound() {
      return Position.Y < 545 - PaddleSpriteHeight;
    }

    private void MoveUp(GameTime gameTime) {
      if (IsWithinUpperBound())
        Position = new Vector2(Position.X,
          Position.Y - VerticalVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds);
    }

    private void MoveDown(GameTime gameTime) {
      if (IsWithinLowerBound())
        Position = new Vector2(Position.X,
          Position.Y + VerticalVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds);
    }
  }
}