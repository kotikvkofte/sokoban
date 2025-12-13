using Core.Interfaces;

namespace Core.Models;

/// <summary>
/// Класс коробки, которую можно перемещать.
/// </summary>
public class Box : IPushable
{
    /// <summary>
    /// Положение коробки на карте.
    /// </summary>
    public Point Position { get; set; }

    public Box(int x, int y) => Position = new Point(x, y);

    public Box(Point position) => Position = position;

    /// <summary>
    /// Перемещение коробки на другое место.
    /// </summary>
    /// <param name="newPosition">Место, куда переместить коробку.</param>
    public void Push(Point newPosition)
    {
        Position = newPosition;
    }
}