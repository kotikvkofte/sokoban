using Core.Interfaces;

namespace Core.Models;

public class LevelMap(Player player, IMapObject?[,] map)
{
    public Player Player { get; } = player;
    public List<Box> Boxes { get; init; } = map.OfType<Box>().ToList();
    public List<Target> Targets { get; } = map.OfType<Target>().ToList();
    public List<Wall> Walls { get; } = map.OfType<Wall>().ToList();
    
    public int Width => map.GetLength(0);
    public int Height => map.GetLength(0);
}