using Core.Interfaces;

namespace Core.Models;

public class LevelMap(Player player, IMapObject?[,] map, List<Target> targets)
{
    public Player Player { get; } = player;
    public List<Target> Targets => targets;
    public IEnumerable<Box> Boxes => map.OfType<Box>();
    public IEnumerable<Wall> Walls => map.OfType<Wall>();
    public int Width => map.GetLength(0);
    public int Height => map.GetLength(1);

    public IMapObject? GetObject(Point position)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height)
            return new Wall(position);

        return map[position.X, position.Y];
    }

    public void MoveObjectOnMap(Point oldPosition, Point newPosition)
    {
        var mapObject = map[oldPosition.X, oldPosition.Y];
        map[oldPosition.X, oldPosition.Y] = null;
        map[newPosition.X, newPosition.Y] = mapObject;
    }
}