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
    /// Игровая статистика.
    /// </summary>
    public GameState State { get; } = new();

    /// <summary>
    /// Количество уровней.
    /// </summary>
    public int LevelCount => mapLoader.LevelsCount;
    
    /// <summary>
    /// Текущий уровень.
    /// </summary>
    public LevelMap Map { get; private set; }

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
        if(levelNumber >= LevelCount || levelNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(levelNumber), "Такого уровня нет.");
        
        Map = mapLoader.LoadLevel(levelNumber);
        Map.Player.Name = playerName;
        
        State.CurrentLevel = levelNumber;
        State.CurrentMovesCount = 0;
        State.CurrentTime = TimeSpan.Zero;

        levelTimer.Restart();
    }

    public TimeSpan CurrentTime => levelTimer.Elapsed;
    
    /// <summary>
    /// Движение объектов на карте в указанном направлении.
    /// </summary>
    /// <param name="direction">Напарвление.</param>
    /// <returns></returns>
    public void Move(Direction direction)
    {
        if (Map.TryMove(direction))
            State.CurrentMovesCount++;
    }

    public bool CheckWin() => Map.IsAllBoxOnTargets();

    public void EndLevel()
    {
        //TODO:сохранение результатов в БД
    }
}