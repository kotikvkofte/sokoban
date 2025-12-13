using Core.Interfaces;
using Core.Structs;

namespace Core.Models;

/// <summary>
/// Загрузщик игрового уровня.
/// </summary>
public class MapLoader : IMapLoader
{
    /// <summary>
    /// Список уровней.
    /// </summary>
    private static readonly List<string> levels =
    [
        """
        11111111
        100P0001
        1010B001
        1T000001
        11111111
        """,
        """
        11111111
        1P0000T1
        1B00B001
        1T000001
        11111111
        """,
        """
        00111110 
        11100010
        1TPB0010
        1110BT10
        1T11B010
        1010T011
        1B00BBT1
        1000T001
        11111111
        """,
    ];

    public LevelMap LoadLevel(int levelNumber, string playerName)
    {
        return ParseLevel(levels[levelNumber], playerName);
    }

    /// <summary>
    /// Парсер уровня (можно выделить в отдельный класс)
    /// </summary>
    /// <param name="level">Уровень в строковом формате</param>
    /// <param name="playerName">Имя игрока.</param>
    /// <returns>Игровой уровень.</returns>
    private LevelMap ParseLevel(string level, string playerName)
    {
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
                    player = new Player(playerName, pos);
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