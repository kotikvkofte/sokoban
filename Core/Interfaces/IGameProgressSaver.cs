using Core.Models;
using DBA.Entity;

namespace Core.Interfaces;

/// <summary>
/// Интерфейся для сохранения и загрузки игрового процесса
/// </summary>
public interface IGameProgress
{
    /// <summary>
    /// Сохранение текущего статуса игры.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="playerName">Имя игрока</param>
    void SaveProgress(GameState state, string playerName);
    
    GameState GetProgress(string playerName, int levelNum);
    
    List<StatisticEntity> GetStatistics();
}