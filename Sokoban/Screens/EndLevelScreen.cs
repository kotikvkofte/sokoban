using System;
using Core.Models;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace Sokoban.Screens;

public class EndLevelScreen : IGameScreen
{
    private readonly Action _toMenu;
    private readonly Action _nextLevel;
    private Panel _panel;
    private Button _nextLevelButton;
    private Button _toMenuButton;
    private Label _totalStepsLabel;
    private Label _totalTimeLabel;
    private readonly GameState _state;

    public EndLevelScreen(Action toMenu, Action toNextLevel, GameState state)
    {
        _toMenu = toMenu;
        _nextLevel = toNextLevel;
        _state = state;
        CreateMainMenuUi();
    }

    public void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);
        _totalStepsLabel.Text = $"Total steps: {_state.CurrentMovesCount}";
        _totalTimeLabel.Text = $"Total time: {_state.CurrentTime.Seconds}";
    }

    public void Draw(GameTime gameTime) => GumService.Default.Draw();

    public void Open() => _panel.IsVisible = true;
    
    private void CreateMainMenuUi()
    {
        _panel = new Panel();
        _panel.Dock(Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        var titleLabel = new Label();
        titleLabel.Text = "Sokoban";
        titleLabel.Anchor(Anchor.Top);
        titleLabel.Visual.Y = 20;
        _panel.AddChild(titleLabel);

        var completedLevelLabel = new Label();
        completedLevelLabel.Text = "The level has been successfully completed!";
        completedLevelLabel.Anchor(Anchor.Center);
        completedLevelLabel.Visual.Y = -120;
        _panel.AddChild(completedLevelLabel);

        _totalStepsLabel = new Label();
        _totalStepsLabel.Text = $"Total steps: {_state.CurrentMovesCount}";
        _totalStepsLabel.Anchor(Anchor.Center);
        _totalStepsLabel.Visual.Y = -70;
        _panel.AddChild(_totalStepsLabel);
        
        _totalTimeLabel = new Label();
        _totalTimeLabel.Text = $"Total time: {_state.CurrentTime.Seconds}";
        _totalTimeLabel.Anchor(Anchor.Center);
        _totalTimeLabel.Visual.Y = -20;
        _panel.AddChild(_totalTimeLabel);
        
        _toMenuButton = new Button();
        _toMenuButton.Text = "Back to menu";
        _toMenuButton.Anchor(Anchor.Center);
        _toMenuButton.Visual.Y = 30;
        _toMenuButton.Visual.X = -100;
        _toMenuButton.Click += (_, _) =>
        {
            _panel.IsVisible = false;
            _toMenu();
        };
        _panel.AddChild(_toMenuButton);
        
        _nextLevelButton = new Button();
        _nextLevelButton.Text = "Next level";
        _nextLevelButton.Anchor(Anchor.Center);
        _nextLevelButton.Visual.Y = 30;
        _nextLevelButton.Visual.X = 100;
        _nextLevelButton.Click += (_, _) =>
        {
            _panel.IsVisible = false;
            _nextLevel();
        };
        _panel.AddChild(_nextLevelButton);
    }
}