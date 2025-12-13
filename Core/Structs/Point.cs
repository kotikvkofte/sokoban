namespace Core.Structs;

/// <summary>
/// Структура, характерихующее положение объекта на карте.
/// </summary>
/// <param name="x">Координата оси X.</param>
/// <param name="y">Координата оси Y.</param>
public readonly struct Point(int x, int y)
{
    /// <summary>
    /// Координата оси X.
    /// </summary>
    public readonly int X = x;
    
    /// <summary>
    /// Координата оси Y.
    /// </summary>
    public readonly int Y = y;

    /// <summary>
    /// Получение следующего объекта в заданном направлении.
    /// </summary>
    /// <param name="direction">Направление</param>
    /// <returns>Расположение объекта на карте.</returns>
    public Point GetNextPoint(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Point(X, Y - 1),
            Direction.Down => new Point(X, Y + 1),
            Direction.Left => new Point(X - 1, Y),
            Direction.Right => new Point(X + 1, Y)
        };
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
    
    public override bool Equals(object? obj) => obj is Point point && point.X == X && point.Y == Y;
}