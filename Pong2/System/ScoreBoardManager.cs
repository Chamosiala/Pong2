using Pong2.Entities;

namespace Pong2.System {
  internal class ScoreBoardManager {
    public bool isBallOut;

    private readonly ScoreBoard _player1Score;
    private readonly ScoreBoard _player2Score;
    private readonly Ball _ball;


    public ScoreBoardManager(ScoreBoard player1Score, ScoreBoard player2Score, Ball ball) {
      _player1Score = player1Score;
      _player2Score = player2Score;
      _ball = ball;
      isBallOut = false;
    }

    public void Update() {
      if (_ball.Position.X > 1000) {
        _player1Score.Score++;
        isBallOut = true;
        _ball.ResetBall();
      }
      else if (_ball.Position.X < 0) {
        _player2Score.Score++;
        isBallOut = true;
        _ball.ResetBall();
      }
    }
  }
}