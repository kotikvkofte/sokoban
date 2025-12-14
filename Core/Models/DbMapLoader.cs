using Core.Interfaces;
using Core.Structs;
using DBA;

namespace Core.Models;

public class DbMapLoader : IMapLoader
{
    private readonly AppDbContext _context = new AppDbContext();
    
    public int LevelsCount => _context.Levels.Count();
    
    public LevelMap LoadLevel(int levelNumber)
    {
        var levelEntity = _context.Levels.FirstOrDefault(l => l.Id == levelNumber);
        
        if (levelEntity == null)
            throw new Exception("No level not found");

        var level = levelEntity.Map;
        
        var stringLines = level.Replace("\r", "").Split(['\n']);

        var height = stringLines.Length;
        var width = stringLines[0].Length;
        var map = new IMapObject?[width, height];
        var targets = new List<Target>();
        Player? player = null;

        for (var y = 0; y < stringLines.Length; y++)
        for (var x = 0; x < stringLines[y].Length; x++)
        {
            var pos = new Point(x, y);
            switch (stringLines[y][x])
            {
                case '1':
                    map[x, y] = new Wall(pos);
                    break;
                case 'P':
                    player = new Player(pos);
                    break;
                case 'B':
                    map[x, y] = new Box(pos);
                    break;
                case 'T':
                    targets.Add(new Target(pos));
                    break;
                default:
                    map[x, y] = null;
                    break;
            }
        }

        if (player is null)
            throw new Exception("No player found");

        return new LevelMap(player, map, targets);
    }

}