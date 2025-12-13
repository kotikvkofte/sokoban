namespace Core.Models;

/// <summary>
/// Статистика игры.
/// </summary>
public class GameState
{
    /// <summary>
    /// Текущий уровень.
    /// </summary>
    public int CurrentLevel { get; set; } = 0;
    
    /// <summary>
    /// Кол-ва пройденных уровнейю
    /// </summary>
    public int PassedLevels { get; set; } = 0;
    
    /// <summary>
    /// Лучшее количество ходов.
    /// </summary>
    public int BestMovesCount { get; set; } = 0;
    
    /// <summary>
    /// Лучшее время прохождения.
    /// </summary>
    public TimeSpan BestPassedTime { get; set; } = TimeSpan.Zero;
    
    /// <summary>
    /// Текущее кол-во ходов.
    /// </summary>
    public int CurrentMovesCount { get; set; } = 0;
    
    /// <summary>
    /// Текущее время.
    /// </summary>
    public TimeSpan CurrentTime { get; set; } = TimeSpan.Zero;
}