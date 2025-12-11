using Core.Interfaces;

namespace Core.Models;

public class Target(int x, int y) : IMapObject
{
    public Point Position { get; set; } =  new Point(x,y);
}