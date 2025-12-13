using Core.Interfaces;

namespace Core.Models;

public class Box : IPushable
{
    public Point Position { get; set; }

    public Box(int x, int y) => Position = new Point(x, y);

    public Box(Point position) => Position = position;

    public void Push(Point newPosition)
    {
        Position = newPosition;
    }
}