using System;
using System.Collections.Generic;
using System.Linq;
using DBA.Entity;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace Sokoban.Screens;

public class LeaderBoardScreen : IGameScreen
{
    private List<StatisticEntity> _statistics;
    private Panel _panel;
    private Panel _bottomPanel;
    private Panel _topPanel;

    public LeaderBoardScreen(Action toMainMenu)
    {
        InitPanels(toMainMenu);
    }

    public void Update(GameTime gameTime) => GumService.Default.Update(gameTime);

    public void Draw(GameTime gameTime) => GumService.Default.Draw();

    public void UpdateStatistic(List<StatisticEntity> statisticList)
    {
        _statistics = statisticList;
        
        _panel = new Panel();
        _panel.Dock(Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();
        
        for (var i = 0; i < _statistics.Count; i++)
        {
            DrawStringStat(i);
        }
    }
    
    private void DrawStringStat(int stringNum)
    {
        var stat = _statistics[stringNum];
        
        var levelNum = new Label();
        levelNum.Text = $"{stat.Level.Id}";
        levelNum.Anchor(Anchor.Top);
        levelNum.Visual.Y = (stringNum + 1) * 40;
        levelNum.Visual.X = -150;
        _panel.AddChild(levelNum);
        
        var playerName = new Label();
        playerName.Text = $"{stat.Player.Name}";
        playerName.Anchor(Anchor.Top);
        playerName.Visual.Y = (stringNum + 1) * 40;
        playerName.Visual.X = -50;
        _panel.AddChild(playerName);
        
        var bestTime = new Label();
        bestTime.Text = $"{stat.BestTime.Seconds}";
        bestTime.Anchor(Anchor.Top);
        bestTime.Visual.Y = (stringNum + 1) * 40;
        bestTime.Visual.X = 50;
        _panel.AddChild(bestTime);
        
        var bestCounts = new Label();
        bestCounts.Text = $"{stat.BestCount}";
        bestCounts.Anchor(Anchor.Top);
        bestCounts.Visual.Y = (stringNum + 1) * 40;
        bestCounts.Visual.X = 150;
        _panel.AddChild(bestCounts);
    }

    private void InitPanels(Action toMainMenu)
    {
        _bottomPanel = new Panel();
        _bottomPanel.Dock(Dock.Bottom);
        _bottomPanel.IsVisible = false;
        _bottomPanel.AddToRoot();
        
        CreateTopPanel();
        
        var leaderBoardButton = new Button();
        leaderBoardButton.Text = "Back to menu";
        leaderBoardButton.Anchor(Anchor.Center);
        leaderBoardButton.Click += (_, _) =>
        {
            if (_panel != null) 
                _panel.IsVisible = false;
            
            _topPanel.IsVisible = false;
            _bottomPanel.IsVisible = false;
            toMainMenu();
        };
        _bottomPanel.AddChild(leaderBoardButton);
    }

    private void CreateTopPanel()
    {
        _topPanel = new Panel();
        _topPanel.Dock(Dock.Top);
        _topPanel.IsVisible = false;
        _topPanel.AddToRoot();
        
        var levelNum = new Label();
        levelNum.Text = "Level";
        levelNum.Anchor(Anchor.Center);
        levelNum.Visual.X = -150;
        _topPanel.AddChild(levelNum);
        
        var playerName = new Label();
        playerName.Text = "Player";
        playerName.Anchor(Anchor.Center);
        playerName.Visual.X = -50;
        _topPanel.AddChild(playerName);
        
        var bestTime = new Label();
        bestTime.Text = "Time";
        bestTime.Anchor(Anchor.Center);
        bestTime.Visual.X = 50;
        _topPanel.AddChild(bestTime);
        
        var bestCounts = new Label();
        bestCounts.Text = "Steps";
        bestCounts.Anchor(Anchor.Center);
        bestCounts.Visual.X = 150;
        _topPanel.AddChild(bestCounts);
    }
    
    public void Open()
    {
        _bottomPanel.IsVisible = true;
        _topPanel.IsVisible = true;
        _panel.IsVisible = true;   
    }
}