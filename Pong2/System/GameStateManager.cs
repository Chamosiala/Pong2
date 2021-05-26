namespace Pong2.System {
  internal class GameStateManager {
    public enum GameState {
      Running,
      ResettingBall
    }

    public GameState State;

    private readonly ScoreBoardManager _scoreBoardManager;

    public GameStateManager(ScoreBoardManager scoreBoardManager) {
      State = GameState.Running;
      _scoreBoardManager = scoreBoardManager;
    }

    public void Update() {
      if (_scoreBoardManager.isBallOut)
        State = GameState.ResettingBall;
    }

    public void ChangeStateToRunning() {
      State = GameState.Running;
    }
  }
}