namespace Core.Interfaces;

/// <summary>
/// Нтерфейс для объектов, которые могут быть перемещены
/// </summary>
public interface IPushable : IMapObject
{
    void Push(Point newPosition);
}