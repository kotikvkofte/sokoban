using Core.Models;

namespace Core.Interfaces;

/// <summary>
/// Интерфейся для сохранения игрового процесса
/// </summary>
public interface IGameProgressSaver
{
    /// <summary>
    /// Сохранение текущего статуса игры.
    /// </summary>
    /// <param name="state"></param>
    void SaveProgress(GameState state);
}