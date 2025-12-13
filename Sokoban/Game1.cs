using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Interfaces;
using Core.Logic;
using Core.Models;
using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Sokoban.Enums;
using Sokoban.Screens;

namespace Sokoban;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private IGameScreen _currentScreen;
    private MainMenuScreen _mainMenuScreen;
    private GameplayScreen _gameplayScreen;
    private GameEngine _engine;
    private KeyboardState _previousKeyboardState;
    private string _playerName;
    private MapDrawer _mapDrawer;

    private int _currentLevel = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        InitializeGum();

        _engine = new GameEngine(new MapLoader(), null);

        _mainMenuScreen = new MainMenuScreen(StartGame);
        _previousKeyboardState = new KeyboardState();

        _currentScreen = _mainMenuScreen;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _mapDrawer = new MapDrawer(
            Content.Load<Texture2D>("Images/Ground"),
            Content.Load<Texture2D>("Images/Wall"),
            Content.Load<Texture2D>("Images/Box"),
            Content.Load<Texture2D>("Images/Target"),
            Content.Load<Texture2D>("Images/Player")
        );
    }

    protected override void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            _currentScreen = _mainMenuScreen;
            _mainMenuScreen.OpenMenu();
        }

        if (_currentScreen is GameplayScreen && keyboardState.IsKeyDown(Keys.R))
        {
            _engine.StartLevel(_currentLevel, _playerName);
        }

        _currentScreen.Update(gameTime);

        if (_engine.Map is not null && _engine.CheckWin())
        {
            _engine.EndLevel();
            _engine.StartLevel(++_currentLevel, _playerName);
            ResizeTile(_engine.Map);
            _previousKeyboardState = keyboardState;
            return;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _currentScreen.Draw(gameTime);
        base.Draw(gameTime);
    }

    private bool IsJustPressed(KeyboardState kb, Keys key) =>
        kb.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);

    private void InitializeGum()
    {
        GumService.Default.Initialize(this, DefaultVisualsVersion.V2);
        GumService.Default.ContentLoader.XnaContentManager = Content;

        FrameworkElement.KeyboardsForUiControl.Clear();
        FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);
        FrameworkElement.GamePadsForUiControl.Clear();
        FrameworkElement.GamePadsForUiControl.AddRange(GumService.Default.Gamepads);

        FrameworkElement.TabKeyCombos.Clear();
        FrameworkElement.TabReverseKeyCombos.Clear();

        GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
        GumService.Default.Renderer.Camera.Zoom = 1.0f;
    }

    private void StartGame(string playerName, int levelNum)
    {
        _currentLevel = levelNum;
        _playerName = playerName;
        _engine.StartLevel(_currentLevel, _playerName);

        ResizeTile(_engine.Map);
        
        _gameplayScreen = new GameplayScreen(
            _engine,
            _spriteBatch,
            _mapDrawer,
            _previousKeyboardState,
            _currentLevel,
            _playerName
        );
        _currentScreen = _gameplayScreen;
    }
    
    private void ResizeTile(LevelMap map)
    {
        var screenWidth  = GraphicsDevice.PresentationParameters.BackBufferWidth;
        var screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        var tileSizeX = (screenWidth - 200)  / map.Width;
        var tileSizeY = screenHeight / map.Height;

        var tileSize = Math.Min(tileSizeX, tileSizeY);

        _mapDrawer.TileSize = tileSize;
    }
}