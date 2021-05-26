using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pong2.Entities;

namespace Pong2.System {
  internal class CollisionDetector {
    private readonly Ball _ball;
    private readonly Paddle _player1Paddle;
    private readonly Paddle _player2Paddle;
    private readonly Border _topBorder;
    private readonly Border _bottomBorder;

    private readonly List<ICollidable> ListOfCollidables = new List<ICollidable>();

    public CollisionDetector(Ball ball, Paddle paddle1, Paddle paddle2, Border topBorder, Border bottomBorder) {
      _ball = ball;
      _player1Paddle = paddle1;
      _player2Paddle = paddle2;
      _topBorder = topBorder;
      _bottomBorder = bottomBorder;

      ListOfCollidables.Add(_player1Paddle);
      ListOfCollidables.Add(_player2Paddle);
      ListOfCollidables.Add(_topBorder);
      ListOfCollidables.Add(_bottomBorder);
    }

    public void Update(GameTime gameTime) {
      foreach (var collidable in ListOfCollidables)
        if (IsCollisionDetected(collidable))
          Ricochet(collidable);
    }

    public bool IsCollisionDetected(ICollidable collidable) {
      return _ball.CollisionBox.Intersects(collidable.CollisionBox);
    }

    public void Ricochet(ICollidable collidable) {
      if (collidable is Paddle paddle)
        RicochetOffPaddle(paddle);
      else
        _ball.VerticalVelocity *= -1;
    }

    private void RicochetOffPaddle(Paddle paddle) {
      var directionFormula = (_ball.Position.Y - paddle.Position.Y) / (_ball.BallStartPosY - paddle.PaddleStartPosY);
      var ballHitsTop = directionFormula < 0.9;
      var ballHitsCenter = directionFormula > 0.9 && directionFormula < 1.1;
      var ballHitsBottom = directionFormula > 1.1;

      _ball.HorizontalVelocity *= -1;

      if (ballHitsBottom)
        _ball.VerticalVelocity = Math.Abs(_ball.HorizontalVelocity);
      else if (ballHitsCenter)
        _ball.VerticalVelocity = 0;
      else if (ballHitsTop)
        _ball.VerticalVelocity = Math.Abs(_ball.HorizontalVelocity) * -1;
    }
  }
}