using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong2.Entities;
using Pong2.System;

namespace Pong2 {
  public class Game1 : Game {
    public const int WINDOW_WIDTH = 1000;
    public const int WINDOW_HEIGHT = 600;

    public int paddleStartPosY = 265;
    public int ballStartPosY = 290;

    private const string ASSET_NAME_SPRITESHEET = "pongSprites2x";
    private const string ASSET_NAME_SCOREFONT = "Score";

    private int ballResetTimer = 60 * 2;

    private Texture2D _spriteSheetTexture;

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private GameStateManager _gameStateManager;

    private Ball _ball;
    private Paddle _player1Paddle;
    private Paddle _player2Paddle;
    private Border _topBorder;
    private Border _bottomBorder;
    private ScoreBoard _player1Score;
    private ScoreBoard _player2Score;

    private InputController _inputController;
    private CollisionDetector _collisionDetector;
    private AIController _aiController;
    private ScoreBoardManager _scoreBoardManager;

    public Game1() {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      base.Initialize();

      _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
      _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
      _graphics.ApplyChanges();
    }

    protected override void LoadContent() {
      _spriteSheetTexture = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);
      _font = Content.Load<SpriteFont>(ASSET_NAME_SCOREFONT);

      _spriteBatch = new SpriteBatch(GraphicsDevice);

      _ball = new Ball(_spriteSheetTexture, new Vector2(490, ballStartPosY));

      _player1Paddle = new Paddle(_spriteSheetTexture, new Vector2(20, paddleStartPosY));
      _player2Paddle = new Paddle(_spriteSheetTexture, new Vector2(960, paddleStartPosY));

      _topBorder = new Border(new Vector4(0, 50, 50, 1000));
      _bottomBorder = new Border(new Vector4(0, 550, 550, 1000));

      _player1Score = new ScoreBoard(_font, new Vector2(25, 0));
      _player2Score = new ScoreBoard(_font, new Vector2(950, 0));

      _inputController = new InputController(_player1Paddle);
      _collisionDetector = new CollisionDetector(_ball, _player1Paddle, _player2Paddle, _topBorder, _bottomBorder);
      _aiController = new AIController(_player2Paddle, _ball);
      _scoreBoardManager = new ScoreBoardManager(_player1Score, _player2Score, _ball);

      _gameStateManager = new GameStateManager(_scoreBoardManager);
    }

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
        Reset();

      base.Update(gameTime);

      _gameStateManager.Update();
      _inputController.ProcessControls();
      _player1Paddle.Update(gameTime);
      _player1Paddle.Stop();

      if (_gameStateManager.State == GameStateManager.GameState.Running) {
        _collisionDetector.Update(gameTime);
        _ball.Update(gameTime);
        _aiController.Update();
        _player2Paddle.Update(gameTime);
        _player2Paddle.Stop();
        _scoreBoardManager.Update();
      }
      else if (_gameStateManager.State == GameStateManager.GameState.ResettingBall) {
        ballResetTimer--;
        if (ballResetTimer == 0) {
          _gameStateManager.ChangeStateToRunning();
          ballResetTimer = 60 * 2;
          _scoreBoardManager.isBallOut = false;
        }
      }
    }


    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      _spriteBatch.Begin();
      _ball.Draw(gameTime, _spriteBatch);
      _player1Paddle.Draw(gameTime, _spriteBatch);
      _player2Paddle.Draw(gameTime, _spriteBatch);
      _topBorder.Draw(gameTime, _spriteBatch);
      _bottomBorder.Draw(gameTime, _spriteBatch);
      _player1Score.Draw(gameTime, _spriteBatch);
      _player2Score.Draw(gameTime, _spriteBatch);


      _spriteBatch.End();

      base.Draw(gameTime);
    }

    private void Reset() {
      LoadContent();
    }
  }
}