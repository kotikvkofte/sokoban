using Core.Interfaces;

namespace Core.Models;

public class Box(int x, int y) : IMapObject
{
    public Point Position { get; set; } =  new Point(x,y);
    
    public void Push(Point newPosition)
    {
        Position = newPosition;
    }
}