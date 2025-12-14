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
    private EndLevelScreen _endLevelScreen;
    private EndGameScreen _endGameScreen;
    private GameEngine _engine;
    private KeyboardState _previousKeyboardState;
    private MapDrawer _mapDrawer;

    private int _currentLevel = 0;
    private string _playerName = "";

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        InitializeGum();

        _engine = new GameEngine(new DbMapLoader(), null);
        _previousKeyboardState = new KeyboardState();

        _mainMenuScreen = new MainMenuScreen(StartGame);
        _endLevelScreen = new EndLevelScreen(ToMainMenu, ToNextLevel, _engine.State);
        _endGameScreen = new EndGameScreen(ToMainMenu, _engine.State);

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

        _gameplayScreen = new GameplayScreen(
            _engine,
            _spriteBatch,
            _mapDrawer,
            _previousKeyboardState
        );
    }

    protected override void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            _gameplayScreen.CloseScreen();
            ToMainMenu();
        }

        if (_currentScreen is GameplayScreen)
        {
            if (keyboardState.IsKeyDown(Keys.R))
            {
                _gameplayScreen.CloseScreen();
                StartCurrentLevel();
            }

            TryEndLevel();
        }

        _currentScreen.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _currentScreen.Draw(gameTime);
        base.Draw(gameTime);
    }

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

    private void ToNextLevel()
    {
        _currentLevel++;
        StartCurrentLevel();
    }

    private void ToMainMenu()
    {
        _currentScreen = _mainMenuScreen;
        _mainMenuScreen.OpenMenu();
    }

    private void StartCurrentLevel()
    {
        _engine.StartLevel(_currentLevel, _playerName);
        ResizeTile(_engine.Map);
        _gameplayScreen.Initialize();

        _currentScreen = _gameplayScreen;
    }

    private void StartGame(string playerName, int levelNum)
    {
        _currentLevel = levelNum;
        _playerName = playerName;
        try
        {
            StartCurrentLevel();
        }
        catch (Exception e)
        {
            MessageBox.Show("Error!", "Can't find level " + levelNum, ["Ok"]);
            _mainMenuScreen.OpenMenu();
            Console.WriteLine(e);
        }
    }

    private void ResizeTile(LevelMap map)
    {
        var screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth - 200;
        var screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        var tileSizeX = screenWidth / map.Width;
        var tileSizeY = screenHeight / map.Height;

        var tileSize = Math.Min(tileSizeX, tileSizeY);

        _mapDrawer.TileSize = tileSize;
    }

    private void TryEndLevel()
    {
        if (_engine.Map is null || !_engine.CheckWin())
            return;
        _engine.State.PassedLevels++;
        _gameplayScreen.CloseScreen();

        if (_engine.LevelCount == _currentLevel)
        {
            _endGameScreen.Open();
            _currentScreen = _endGameScreen;
        }
        else
        {
            _engine.EndLevel();
            _currentScreen = _endLevelScreen;
            _endLevelScreen.Open();
        }
    }
}