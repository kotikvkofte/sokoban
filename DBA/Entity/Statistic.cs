namespace DBA.Entity;

public class StatisticEntity
{
    public int Id { get; set; }
    public TimeSpan BestTime { get; set; } = TimeSpan.Zero;
    public int BestCount { get; set; } = 0;
    
    public PlayerEntity Player { get; set; }
    public LevelEntity Level { get; set; }
}