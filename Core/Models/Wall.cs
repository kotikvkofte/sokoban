using Core.Interfaces;

namespace Core.Models;

public class Wall : IMapObject
{
    public Point Position { get; set; }

    public Wall(int x, int y) => Position = new Point(x, y);
    public Wall(Point position) => Position = position;
}