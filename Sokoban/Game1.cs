using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Interfaces;
using Core.Logic;
using Core.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Point = Microsoft.Xna.Framework.Point;

namespace Sokoban;

public class Game1 : Game
{
    private const int TILE_SIZE = 50;
    const float TARGET_SCALE = 0.5f;
    const float PLAYER_SCALE = 0.7f;

    private int _currentLevel = 2;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardState _previousKeyboardState;

    private GameEngine _engine;

    private Texture2D _groundTexture;
    private Texture2D _wallTexture;
    private Texture2D _boxTexture;
    private Texture2D _targetTexture;
    private Texture2D _playerTexture;

    private readonly Dictionary<Keys, Direction> _movementKeys = new()
    {
        { Keys.Up, Direction.Up },
        { Keys.Down, Direction.Down },
        { Keys.Left, Direction.Left },
        { Keys.Right, Direction.Right },
        { Keys.W, Direction.Up },
        { Keys.S, Direction.Down },
        { Keys.A, Direction.Left },
        { Keys.D, Direction.Right },
    };

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _engine = new GameEngine(new MapLoader(), null);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _engine.StartLevel(_currentLevel, "Player");

        _groundTexture = Content.Load<Texture2D>("Images/Ground");
        _wallTexture = Content.Load<Texture2D>("Images/Wall");
        _playerTexture = Content.Load<Texture2D>("Images/Player");
        _boxTexture = Content.Load<Texture2D>("Images/Box");
        _targetTexture = Content.Load<Texture2D>("Images/Target");
    }

    protected override void Update(GameTime gameTime)
    {
        if (_engine.CheckWin())
        {
            _engine.StartLevel(++_currentLevel, "Player");
            return;
        }
        var keyboardState = Keyboard.GetState();

        foreach (var key in _movementKeys.Keys.Where(key => IsJustPressed(keyboardState, key)))
        {
            _engine.Move(_movementKeys[key]);
        }

        _previousKeyboardState = keyboardState;
        base.Update(gameTime);
    }

    private bool IsJustPressed(KeyboardState kb, Keys key) =>
        kb.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        for (var y = 0; y < _engine.Map.Height; y++)
        for (var x = 0; x < _engine.Map.Width; x++)
        {
            _spriteBatch.Draw(_groundTexture, GetRectangle(x, y), Color.White);

            if (_engine.IsTarget(x, y))
                _spriteBatch.Draw(_targetTexture, GetRectangle(x, y, TARGET_SCALE), Color.White);
        }

        foreach (var wall in _engine.Map.Walls)
            _spriteBatch.Draw(_wallTexture, GetRectangle(wall), Color.White);

        foreach (var box in _engine.Map.Boxes)
            _spriteBatch.Draw(_boxTexture, GetRectangle(box), Color.White);

        _spriteBatch.Draw(_playerTexture, GetRectangle(_engine.Map.Player, PLAYER_SCALE), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private Rectangle GetRectangle(int cellX, int cellY, float scale = 1)
    {
        var size = (int)(TILE_SIZE * scale);
        var offset = (TILE_SIZE - size) / 2;

        var x = cellX * TILE_SIZE + offset;
        var y = cellY * TILE_SIZE + offset;

        return new Rectangle(x, y, size, size);
    }

    private Rectangle GetRectangle(IMapObject obj, float scale = 1) =>
        GetRectangle(obj.Position.X, obj.Position.Y, scale);
}