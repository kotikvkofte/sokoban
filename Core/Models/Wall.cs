using Core.Interfaces;

namespace Core.Models;

/// <summary>
/// Стена, которая является преятствием для движений.
/// </summary>
public class Wall : IMapObject
{
    /// <summary>
    /// Позиция на карте.
    /// </summary>
    public Point Position { get; set; }

    public Wall(int x, int y) => Position = new Point(x, y);

    public Wall(Point position) => Position = position;
}