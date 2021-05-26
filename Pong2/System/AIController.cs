using Pong2.Entities;

namespace Pong2.System {
  internal class AIController {
    private readonly Paddle _player2Paddle;
    private readonly Ball _ball;

    public AIController(Paddle paddle, Ball ball) {
      _player2Paddle = paddle;
      _ball = ball;
    }

    public void Update() {
      var directionFormula = (_ball.Position.Y - _player2Paddle.Position.Y) /
                             (_ball.BallStartPosY - _player2Paddle.PaddleStartPosY);
      if (!(_ball.HorizontalVelocity > 0 && _ball.Position.X > 500)) return;
      if (directionFormula < 0)
        _player2Paddle.ChangeStateToMovingUp();
      else if (directionFormula > 2)
        _player2Paddle.ChangeStateToMovingDown();
    }
  }
}