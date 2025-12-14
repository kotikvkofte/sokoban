using Core.Models;

namespace Core.Interfaces;

/// <summary>
/// Интерфейс для загрузки игрового уровня.
/// </summary>
public interface IMapLoader
{
    /// <summary>
    /// Загрузка игрового уровняю
    /// </summary>
    /// <param name="levelNumber">Номер уровня</param>
    /// <param name="playerName">Имя игрока.</param>
    /// <returns></returns>
    LevelMap LoadLevel(int levelNumber);

    /// <summary>
    /// Количество уровней.
    /// </summary>
    public int LevelsCount { get;}
}