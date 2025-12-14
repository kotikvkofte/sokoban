using Core.Logic;
using Core.Models;
using Gum.Forms.Controls;
using Gum.Wireframe;
using MonoGameGum;

namespace Sokoban.Screens;

public class GameplayStatsPanel
{
    private Panel _statsPanel;
    private Label _playerLabel;
    private Label _levelLabel;
    private Label _movesLabel;
    private Label _timeLabel;
    
    private readonly GameEngine _engine;

    public GameplayStatsPanel(GameEngine engine)
    {
        _engine = engine;
        CreateStatsPanel();
    }

    public void Update()
    {
        _playerLabel.Text = $"Name: {_engine.Map.Player.Name}";
        _levelLabel.Text = $"Level: {_engine.State.CurrentLevel + 1}";
        _movesLabel.Text = $"Steps: {_engine.State.CurrentMovesCount}";
        _timeLabel.Text = $"Time: {_engine.State.CurrentTime.Seconds}";
    }
    
    public void ClosePanel() => _statsPanel.IsVisible = false;
    
    private void CreateStatsPanel()
    {
        _statsPanel = new Panel();
        _statsPanel.Dock(Dock.Right);
        _statsPanel.Width = 100;
        _statsPanel.MaxWidth = 200;
        _statsPanel.AddToRoot();

        var levelInfoLabel = new Label();
        levelInfoLabel.Text = $"R-restart level";
        levelInfoLabel.Anchor(Anchor.TopLeft);
        levelInfoLabel.Visual.X = 20;
        levelInfoLabel.Visual.Y = 10;
        _statsPanel.AddChild(levelInfoLabel);
        
        var menuInfoLabel = new Label();
        menuInfoLabel.Text = $"Esc-back to menu";
        menuInfoLabel.Anchor(Anchor.TopLeft);
        menuInfoLabel.Visual.X = 20;
        menuInfoLabel.Visual.Y = 40;
        _statsPanel.AddChild(menuInfoLabel);

        _playerLabel = new Label();
        _playerLabel.Text = $"Name: {_engine.Map.Player.Name}";
        _playerLabel.Anchor(Anchor.TopLeft);
        _playerLabel.Visual.X = 20;
        _playerLabel.Visual.Y = 70;
        _statsPanel.AddChild(_playerLabel);

        _levelLabel = new Label();
        _levelLabel.Text = $"Level: {_engine.State.CurrentLevel + 1}";
        _levelLabel.Anchor(Anchor.TopLeft);
        _levelLabel.Visual.X = 20;
        _levelLabel.Visual.Y = 100;
        _statsPanel.AddChild(_levelLabel);

        _movesLabel = new Label();
        _movesLabel.Text = "Steps: 0";
        _movesLabel.Anchor(Anchor.TopLeft);
        _movesLabel.Visual.X = 20;
        _movesLabel.Visual.Y = 130;
        _statsPanel.AddChild(_movesLabel);

        _timeLabel = new Label();
        _timeLabel.Text = "Time: 00:00";
        _timeLabel.Anchor(Anchor.TopLeft);
        _timeLabel.Visual.X = 20;
        _timeLabel.Visual.Y = 160;
        _statsPanel.AddChild(_timeLabel);
    }
}