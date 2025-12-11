namespace Core.Models;

public class GameState
{
    public int CurrentLevel { get; set; } = 1;
    public int PassedLevels { get; set; } = 0;
    public int BestMovesCount { get; set; } = 0;
    public TimeSpan BestPassedTime { get; set; } = TimeSpan.Zero;
    
    public int CurrentMovesCount { get; set; } = 0;
    public TimeSpan CurrentTime { get; set; } = TimeSpan.Zero;
                
}