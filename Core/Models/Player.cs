using Core.Interfaces;

namespace Core.Models;

public class Player(string name, Point position) : IMovable
{
    public string Name { get; set; } = name;
    public Point Position { get; set; } = position;

    public void Move(Point newPosition)
    {
        Position = newPosition;
    }
}