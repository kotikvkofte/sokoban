namespace Core.Interfaces;

/// <summary>
/// Интерфейс для объектов, которые могут быть перемещены.
/// </summary>
public interface IPushable : IMapObject
{
    /// <summary>
    /// Перемещение объекта на карте. 
    /// </summary>
    /// <param name="newPosition">Новая позиция объекта.</param>
    void Push(Point newPosition);
}