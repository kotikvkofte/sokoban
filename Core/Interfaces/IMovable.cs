namespace Core.Interfaces;

/// <summary>
/// Интерфейс для объектов на карте, которые могут перемещаться сами
/// </summary>
public interface IMovable : IMapObject
{
    void Move(Point newPosition);
}