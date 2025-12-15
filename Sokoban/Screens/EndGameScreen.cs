using System;
using Core.Models;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace Sokoban.Screens;

public class EndGameScreen : IGameScreen
{
    private Panel _panel;
    private Button _toMenuButton;
    private Label _totalStepsLabel;
    private Label _totalTimeLabel;
    private Label _bestStepsLabel;
    private Label _bestTimeLabel;
    private readonly Action _toMenu;
    private GameState _state;

    public EndGameScreen(Action toMenu, GameState gameState)
    {
        _toMenu = toMenu;
        _state = gameState;
        CreateEndGameUi();
    }

    public void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);
        _totalStepsLabel.Text = $"Total steps: {_state.CurrentMovesCount}";
        _totalTimeLabel.Text = $"Total time: {_state.CurrentTime.Seconds}";
        _bestStepsLabel.Text = $"Best steps: {_state.BestMovesCount}";
        _bestTimeLabel.Text = $"Best time: {_state.BestPassedTime.Seconds}";
    }

    public void Draw(GameTime gameTime) => GumService.Default.Draw();

    public void Open(GameState state)
    {
        _state = state;
        _panel.IsVisible = true;
    }

    private void CreateEndGameUi()
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
        completedLevelLabel.Text = "Congratulations! You have completed all the levels!";
        completedLevelLabel.Anchor(Anchor.Center);
        completedLevelLabel.Visual.Y = -120;
        _panel.AddChild(completedLevelLabel);

        _bestStepsLabel = new Label();
        _bestStepsLabel.Text = $"Best steps: {_state.BestMovesCount}";
        _bestStepsLabel.Anchor(Anchor.Center);
        _bestStepsLabel.Visual.Y = -70;
        _bestStepsLabel.Visual.X = -100;
        _panel.AddChild(_bestStepsLabel);
        
        _totalStepsLabel = new Label();
        _totalStepsLabel.Text = $"Total steps: {_state.CurrentMovesCount}";
        _totalStepsLabel.Anchor(Anchor.Center);
        _totalStepsLabel.Visual.Y = -70;
        _totalStepsLabel.Visual.X = 100;
        _panel.AddChild(_totalStepsLabel);

        _bestTimeLabel = new Label();
        _bestTimeLabel.Text = $"Best time: {_state.BestPassedTime.Seconds}";
        _bestTimeLabel.Anchor(Anchor.Center);
        _bestTimeLabel.Visual.Y = -20;
        _bestTimeLabel.Visual.X = -100;
        _panel.AddChild(_bestTimeLabel);
        
        _totalTimeLabel = new Label();
        _totalTimeLabel.Text = $"Total time: {_state.CurrentTime.Seconds}";
        _totalTimeLabel.Anchor(Anchor.Center);
        _totalTimeLabel.Visual.Y = -20;
        _totalTimeLabel.Visual.X = 100;
        _panel.AddChild(_totalTimeLabel);

        _toMenuButton = new Button();
        _toMenuButton.Text = "Back to menu";
        _toMenuButton.Anchor(Anchor.Center);
        _toMenuButton.Visual.Y = 20;
        _toMenuButton.Click += (_, _) =>
        {
            _panel.IsVisible = false;
            _toMenu();
        };
        _panel.AddChild(_toMenuButton);
    }
}