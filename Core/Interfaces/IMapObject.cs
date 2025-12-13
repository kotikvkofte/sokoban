using Core.Structs;

namespace Core.Interfaces;

/// <summary>
/// Интерфейс для объекта, расположенного на карте.
/// </summary>
public interface IMapObject
{
    /// <summary>
    /// Позиция объекта на карте.
    /// </summary>
    Point Position { get; set; }
}