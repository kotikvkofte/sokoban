using Core.Interfaces;
using Core.Structs;

namespace Core.Models;

/// <summary>
/// Цель, куда нужно поставить коробку.
/// </summary>
public class Target : IMapObject
{
    /// <summary>
    /// Позиция на карте.
    /// </summary>
    public Point Position { get; set; }

    public Target(int x, int y) => Position = new Point(x, y);

    public Target(Point position) => Position = position;
}