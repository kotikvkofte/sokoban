using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Interfaces;
using Core.Logic;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Sokoban.Enums;

namespace Sokoban.Screens;

public class GameplayScreen : IGameScreen
{
    private GameplayStatsPanel _statsPanel;
    
    private KeyboardState _previousKeyboardState;

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

    private readonly GameEngine _engine;
    private readonly SpriteBatch _spriteBatch;
    private readonly MapDrawer _mapDrawer;

    public GameplayScreen(GameEngine engine,
        SpriteBatch spriteBatch,
        MapDrawer mapDrawer,
        KeyboardState previousKeyboardState)
    {
        _engine = engine;
        _spriteBatch = spriteBatch;
        _mapDrawer = mapDrawer;
        _previousKeyboardState = previousKeyboardState;
    }

    public void Initialize()
    {
        _statsPanel = new GameplayStatsPanel(_engine);
    }
    
    public void Update(GameTime gameTime)
    {
        var kb = Keyboard.GetState();

        _engine.State.CurrentTime = _engine.CurrentTime;

        foreach (var kv in _movementKeys.Where(kv => IsJustPressed(kb, kv.Key)))
        {
            _engine.Move(kv.Value);
            break;
        }

        _statsPanel.Update();

        _previousKeyboardState = kb;
    }

    public void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        _mapDrawer.DrawMap(_engine.Map, _spriteBatch);
        _spriteBatch.End();
        GumService.Default.Draw();
    }

    public void CloseScreen() => _statsPanel.ClosePanel();

    private bool IsJustPressed(KeyboardState kb, Keys key) =>
        kb.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);

}