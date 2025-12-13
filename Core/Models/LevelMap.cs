using Core.Interfaces;

namespace Core.Models;

/// <summary>
/// Игровой уровень.
/// </summary>
/// <param name="player">Игрок.</param>
/// <param name="map">Двумерный массив объектов.</param>
/// <param name="targets">Список целей.</param>
public class LevelMap(Player player, IMapObject?[,] map, List<Target> targets)
{
    /// <summary>
    /// Игрок, управляемый пользователем.
    /// </summary>
    public Player Player => player;
    
    /// <summary>
    /// Список целей.
    /// </summary>
    public List<Target> Targets => targets;
    
    /// <summary>
    /// Список коробок, расположенных на карте.
    /// </summary>
    public IEnumerable<Box> Boxes => map.OfType<Box>();
    
    /// <summary>
    /// Список стен, расположенных на карте.
    /// </summary>
    public IEnumerable<Wall> Walls => map.OfType<Wall>();
    
    /// <summary>
    /// Ширина карты.
    /// </summary>
    public int Width => map.GetLength(0);
    
    /// <summary>
    /// Высоты карты.
    /// </summary>
    public int Height => map.GetLength(1);

    /// <summary>
    /// Получение объекта карты.
    /// </summary>
    /// <param name="position">Позиция объекта карты.</param>
    /// <returns>Объект карты.</returns>
    public IMapObject? GetObject(Point position)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height)
            return new Wall(position);

        return map[position.X, position.Y];
    }

    /// <summary>
    /// Обновление информации о положении объекта на карте.
    /// </summary>
    /// <param name="oldPosition">Старое расположение объекта.</param>
    /// <param name="newPosition">Новое расположение объекта.</param>
    public void MoveObjectOnMap(Point oldPosition, Point newPosition)
    {
        var mapObject = map[oldPosition.X, oldPosition.Y];
        map[oldPosition.X, oldPosition.Y] = null;
        map[newPosition.X, newPosition.Y] = mapObject;
    }
}