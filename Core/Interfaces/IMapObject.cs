namespace Core.Interfaces;

/// <summary>
/// Интерфейс для объекта, расположенного на карте
/// </summary>
public interface IMapObject
{
    Point Position { get; set; }
}