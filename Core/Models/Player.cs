using Core.Interfaces;

namespace Core.Models;

public class Player : IMovable
{
    public string Name { get; set; } = "Player";
    public Point Position { get; set; }
    public void Move(Point newPosition)
    {
        Position = newPosition;
    }
}