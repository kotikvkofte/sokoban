using System.Diagnostics;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Core.Logic;

/// <summary>
/// Класс, отвечающий за геймплей.
/// </summary>
/// <param name="mapLoader"></param>
/// <param name="progressSaver"></param>
public class GameEngine(IMapLoader mapLoader, IGameProgressSaver progressSaver)
{
    /// <summary>
    /// Текущий уровень.
    /// </summary>
    public LevelMap Map { get; private set; }
    
    /// <summary>
    /// Игровая статистика.
    /// </summary>
    private readonly GameState state = new();

    /// <summary>
    /// Объект, отвечающий за сохранение игрового прогресса.
    /// </summary>
    private readonly IGameProgressSaver progressSaver = progressSaver;
    
    /// <summary>
    /// Таймер.
    /// </summary>
    private readonly Stopwatch levelTimer = new Stopwatch();

    /// <summary>
    /// Начать уровень.
    /// </summary>
    /// <param name="levelNumber">Номер уровня.</param>
    /// <param name="playerName">Имя игрока.</param>
    public void StartLevel(int levelNumber, string playerName)
    {
        Map = mapLoader.LoadLevel(levelNumber, playerName);

        state.CurrentLevel = levelNumber;
        state.CurrentMovesCount = 0;
        state.CurrentTime = TimeSpan.Zero;

        levelTimer.Restart();
    }

    /// <summary>
    /// Движение объектов на карте в указанном направлении.
    /// </summary>
    /// <param name="direction">Напарвление.</param>
    /// <returns></returns>
    public void Move(Direction direction)
    {
        if (Map.TryMove(direction))
            state.CurrentMovesCount++;
    }

    public bool CheckWin() => Map.IsAllBoxOnTargets();

    public void EndLevel()
    {
    }
}