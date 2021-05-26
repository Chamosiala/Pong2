using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong2.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Pong2.Entities {
  class Ball : IGameEntity {
    public Sprite BallSprite;

    public enum BallState {
      Idle,
      Moving,
      Out
    }

    public const int BallSpritePosX = 365;
    public const int BallSpritePosY = 520;
    public const int BallSpriteWidth = 19;
    public const int BallSpriteHeight = 19;
    
    public Vector2 Position { get; set; }
    public float BallStartPosY { get; set; }
    public float BallStartPosX { get; set; }
    public BallState State { get; private set; }
    public float HorizontalVelocity { get; set; } = -500f;
    public float VerticalVelocity { get; set; } = 0f;

    public Rectangle CollisionBox {
      get {
        Rectangle box = new Rectangle(
          (int)Math.Round(Position.X),
          (int)Math.Round(Position.Y),
          BallSpriteWidth,
          BallSpriteHeight
        );

        return box;
      }
    }

    public Ball(Texture2D spriteSheet, Vector2 position) {
      State = BallState.Idle;
      
      BallSprite = new Sprite(spriteSheet, BallSpritePosX, BallSpritePosY, BallSpriteWidth, BallSpriteHeight);
      
      Position = position;
      BallStartPosX = position.X;
      BallStartPosY = position.Y;
    }

    public void Update(GameTime gameTime)
    {
      if (State == BallState.Idle) {
        State = BallState.Moving;
      }

      if (State == BallState.Moving) {
        MoveBall(gameTime);
      }
    }


    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      BallSprite.Draw(spriteBatch, Position);
    }

    public void ResetBall() {
      
      Position = new Vector2(BallStartPosX, BallStartPosY);
    }

    private void MoveBall(GameTime gameTime) {
      float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
      Position = new Vector2(Position.X + HorizontalVelocity * totalSeconds, Position.Y + VerticalVelocity * totalSeconds);
    }

  }
}
