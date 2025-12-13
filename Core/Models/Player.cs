using Core.Interfaces;
using Core.Structs;

namespace Core.Models;

/// <summary>
/// Класс игрока, который может передвигаться.
/// </summary>
public class Player(string name, Point position) : IMovable
{
    /// <summary>
    /// Имя игрока.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Положение игрока на  карте.
    /// </summary>
    public Point Position { get; set; } = position;

    /// <summary>
    /// Передвижение игрока на другую позицию.
    /// </summary>
    /// <param name="newPosition">Новая позиция игрока.</param>
    public void Move(Point newPosition) => Position = newPosition;
}