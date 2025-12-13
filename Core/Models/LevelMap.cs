using Core.Enums;
using Core.Interfaces;
using Core.Structs;

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
    /// HashSet позиций целей.
    /// </summary>
    private readonly HashSet<Point> targetPositions = [..targets.Select(t => t.Position)];
    
    /// <summary>
    /// Является ли объект целью.
    /// </summary>
    /// <param name="x">Кооридната по оси X</param>
    /// <param name="y">Кооридната по оси X</param>
    public bool IsTarget(int x, int y) => targetPositions.Contains(new Point(x, y));

    /// <summary>
    /// Все ли коробки находятся в целях.
    /// </summary>
    internal bool IsAllBoxOnTargets() => Boxes.All(b => targetPositions.Contains(b.Position));

    /// <summary>
    /// Попытка передвинуть игрока.
    /// </summary>
    /// <param name="direction">Направление движения.</param>
    /// <returns>True - игрок перемещен, false - двигаться нельзя.</returns>
    internal bool TryMove(Direction direction)
    {
        var nextPlayerPos = Player.Position.GetNextPoint(direction);

        switch (GetObject(nextPlayerPos))
        {
            case Wall:
                return false;
            case IPushable box:
                if (!TryPush(box, direction))
                    return false;
                break;
            
        }

        Player.Move(nextPlayerPos);
        return true;
    }

    /// <summary>
    /// Получение объекта карты.
    /// </summary>
    /// <param name="position">Позиция объекта карты.</param>
    /// <returns>Объект карты.</returns>
    private IMapObject? GetObject(Point position)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height)
            return new Wall(position);

        return map[position.X, position.Y];
    }
    
    /// <summary>
    /// Попытка переместить объект.
    /// </summary>
    /// <param name="pushable">Перемещаемый объект.</param>
    /// <param name="direction">Направление перемещения.</param>
    /// <returns>True - объект перемещен, false - перемещение невозможно.</returns>
    private bool TryPush(IPushable pushable, Direction direction)
    {
        var nextPos = pushable.Position.GetNextPoint(direction);
        var nextObj = GetObject(nextPos);
        if (nextObj is not (null or Target))
            return false;

        var mapObject = map[pushable.Position.X, pushable.Position.Y];
        map[pushable.Position.X, pushable.Position.Y] = null;
        map[nextPos.X, nextPos.Y] = mapObject;
        
        pushable.Push(nextPos);

        return true;
    }
}