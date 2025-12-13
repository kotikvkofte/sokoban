using Core.Interfaces;

namespace Core.Models;

public class Target: IMapObject
{
    public Point Position { get; set; }

    public Target(int x, int y) =>  Position = new Point(x, y);
    public Target(Point position) =>  Position = position;
}