namespace Core.Interfaces;

/// <summary>
/// Интерфейс для объектов на карте, которые могут перемещаться сами
/// </summary>
public interface IMovable : IMapObject
{
    /// <summary>
    /// Движение объекта на карте.
    /// </summary>
    /// <param name="newPosition">Новая позиция объекта.</param>
    void Move(Point newPosition);
}