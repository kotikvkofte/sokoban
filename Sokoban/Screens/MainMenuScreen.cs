using System;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace Sokoban.Screens;

public class MainMenuScreen : IGameScreen
{
    private readonly Action<string, int> _onStart;
    private Panel _panel;
    private TextBox _nameTextBox;
    private TextBox _levelNumTextBox;
    private Button _startButton;
    private string _playerName = "";
    private string _levelNum = "1";

    public MainMenuScreen(Action<string, int> onStart)
    {
        _onStart = onStart;
        CreateMainMenuUi();
    }

    public void OpenMenu() => _panel.IsVisible = true;

    public void Update(GameTime gameTime) => GumService.Default.Update(gameTime);

    public void Draw(GameTime gameTime) => GumService.Default.Draw();

    private void CreateMainMenuUi()
    {
        _panel = new Panel();
        _panel.Dock(Dock.Fill);
        _panel.AddToRoot();

        var titleLabel = new Label();
        titleLabel.Text = "Sokoban";
        titleLabel.Anchor(Anchor.Top);
        titleLabel.Visual.Y = 20;
        _panel.AddChild(titleLabel);

        var nameLabel = new Label();
        nameLabel.Text = "Enter player name:";
        nameLabel.Anchor(Anchor.Center);
        nameLabel.Visual.Y = -70;
        _panel.AddChild(nameLabel);

        _nameTextBox = new TextBox();
        _nameTextBox.Width = 200;
        _nameTextBox.Placeholder = "Name...";
        _nameTextBox.Anchor(Anchor.Center);
        _nameTextBox.Visual.Y = -40;
        _nameTextBox.TextChanged += (_, _) => _playerName = _nameTextBox.Text;
        _panel.AddChild(_nameTextBox);

        var levelNumLabel = new Label();
        levelNumLabel.Text = "Enter level number:";
        levelNumLabel.Anchor(Anchor.Center);
        levelNumLabel.Visual.Y = 0;
        _panel.AddChild(levelNumLabel);

        _levelNumTextBox = new TextBox();
        _levelNumTextBox.Width = 200;
        _levelNumTextBox.Placeholder = "1";
        _levelNumTextBox.Anchor(Anchor.Center);
        _levelNumTextBox.Visual.Y = 30;
        _levelNumTextBox.TextChanged += (_, _) => _levelNum = _levelNumTextBox.Text;
        _panel.AddChild(_levelNumTextBox);

        _startButton = new Button();
        _startButton.Text = "Start";
        _startButton.Anchor(Anchor.Center);
        _startButton.Visual.Y = 80;
        _startButton.Click += (_, _) => TryStart();
        _panel.AddChild(_startButton);

        _nameTextBox.IsFocused = true;
    }

    private void TryStart()
    {
        var name = _playerName?.Trim();
        if (string.IsNullOrEmpty(name))
            return;

        _panel.IsVisible = false;
        _onStart(name, int.Parse(_levelNum));
    }
}