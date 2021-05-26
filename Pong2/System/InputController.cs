using Microsoft.Xna.Framework.Input;
using Pong2.Entities;

namespace Pong2.System {
  internal class InputController {
    private readonly Paddle _player1Paddle;
    private KeyboardState _previousKeyboardState;

    public InputController(Paddle paddle) {
      _player1Paddle = paddle;
    }

    public void ProcessControls() {
      var keyboardState = Keyboard.GetState();

      if (IsOnlyUpArrowPressed(keyboardState))
        _player1Paddle.ChangeStateToMovingUp();
      else if (IsOnlyDownArrowPressed(keyboardState))
        _player1Paddle.ChangeStateToMovingDown();

      _previousKeyboardState = keyboardState;
    }

    private bool IsOnlyDownArrowPressed(KeyboardState keyboardState) {
      return keyboardState.IsKeyDown(Keys.Down) && !_previousKeyboardState.IsKeyDown(Keys.Up);
    }

    private bool IsOnlyUpArrowPressed(KeyboardState keyboardState) {
      return keyboardState.IsKeyDown(Keys.Up) && !_previousKeyboardState.IsKeyDown(Keys.Down);
    }
  }
}