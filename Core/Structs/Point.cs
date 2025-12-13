namespace Core;

public readonly struct Point(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;

    public Point GetNextPoint(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Point(X, Y - 1),
            Direction.Down => new Point(X, Y + 1),
            Direction.Left => new Point(X - 1, Y),
            Direction.Right => new Point(X + 1, Y),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Wrong direction")
        };
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
    
    public override bool Equals(object? obj) => obj is Point point && point.X == X && point.Y == Y;
}