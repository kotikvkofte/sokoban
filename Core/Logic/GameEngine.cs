using System.Diagnostics;
using Core.Interfaces;
using Core.Models;

namespace Core.Logic;

public class GameEngine
{
    public LevelMap Map { get; private set; }
    private readonly GameState state;
    private HashSet<Point> targetPositions;

    private readonly IMapLoader mapLoader;
    private readonly IGameProgressSaver progressSaver;
    private readonly Stopwatch levelTimer = new Stopwatch();

    public GameEngine(IMapLoader mapLoader, IGameProgressSaver progressSaver)
    {
        this.mapLoader = mapLoader;
        this.progressSaver = progressSaver;
        state = new GameState();
    }

    public void StartLevel(int levelNumber, string playerName)
    {
        Map = mapLoader.LoadLevel(levelNumber, playerName);

        targetPositions = [..Map.Targets.Select(t => t.Position)];

        state.CurrentLevel = levelNumber;
        state.CurrentMovesCount = 0;
        state.CurrentTime = TimeSpan.Zero;

        levelTimer.Restart();
    }

    public bool IsTarget(int x, int y) => targetPositions.Contains(new Point(x, y));
    
    public bool CheckWin() => Map.Boxes.All(b => targetPositions.Contains(b.Position));

    public bool Move(Direction direction)
    {
        var currentPlayerPos = Map.Player.Position;
        var nextPlayerPos = currentPlayerPos.GetNextPoint(direction);

        var nextMapObject = Map.GetObject(nextPlayerPos);
        switch (nextMapObject)
        {
            case Wall:
                return false;
            case IPushable box:
            {
                var isMove = MoveBox(box, direction);
                if (!isMove)
                    return false;
                break;
            }
        }

        Map.Player.Move(nextPlayerPos);
        state.CurrentMovesCount++;

        if (CheckWin())
            EndLevel();

        return true;
    }

    private bool MoveBox(IPushable box, Direction direction)
    {
        var nextBoxPos = box.Position.GetNextPoint(direction);
        var nextBoxObj = Map.GetObject(nextBoxPos);
        if (nextBoxObj is not (null or Target))
            return false;

        // Map.MoveObjectOnMap(box.Position, nextBoxPos);
        box.Push(nextBoxPos);

        return true;
    }


    private void EndLevel()
    {
    }
}