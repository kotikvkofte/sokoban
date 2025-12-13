using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Interfaces;
using Core.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban.Enums;

namespace Sokoban.Screens;

public class GameplayScreen(
    GameEngine engine,
    SpriteBatch spriteBatch,
    MapDrawer mapDrawer,
    KeyboardState previousKeyboardState,
    int currentLevel,
    string playerName) : IGameScreen
{
    private KeyboardState _previousKeyboardState = previousKeyboardState;
    private int _currentLevel = currentLevel;

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

    public void Update(GameTime gameTime)
    {
        var kb = Keyboard.GetState();

        foreach (var kv in _movementKeys)
        {
            if (IsJustPressed(kb, kv.Key))
            {
                engine.Move(kv.Value);
                break;
            }
        }

        _previousKeyboardState = kb;
    }

    public void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();
        mapDrawer.DrawMap(engine.Map, spriteBatch);
        spriteBatch.End();
    }

    private bool IsJustPressed(KeyboardState kb, Keys key) =>
        kb.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
}