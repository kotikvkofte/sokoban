using Core.Interfaces;
using DBA;
using DBA.Entity;

namespace Core.Models;

public class DbGameProgress : IGameProgress
{
    public void SaveProgress(GameState state, string playerName)
    {
        using var context = new AppDbContext();
        var playerEntity = context.Players.FirstOrDefault(p => p.Name == playerName);
        if (playerEntity == null)
        {
            var newPlayer = new PlayerEntity
            {
                Name = playerName,
            };
            context.Add(newPlayer);
            context.SaveChanges();
            playerEntity = newPlayer;
        }

        var levelEntity = context.Levels.FirstOrDefault(x => x.Id == state.CurrentLevel);
        if (levelEntity == null)
            throw new ArgumentException("Invalid current level.");

        var stateEntity =
            context.Statistics.FirstOrDefault(x => x.Player == playerEntity && x.Level.Id == state.CurrentLevel);
        if (stateEntity == null)
        {
            var newStateEntity = new StatisticEntity()
            {
                Player = playerEntity,
                Level = levelEntity,
                BestCount = state.CurrentMovesCount,
                BestTime = state.CurrentTime,
            };
            context.Add(newStateEntity);
            context.SaveChanges();
            return;
        }

        stateEntity.BestCount = Math.Min(stateEntity.BestCount, state.CurrentMovesCount);
        stateEntity.BestTime = state.CurrentTime < stateEntity.BestTime
            ? state.CurrentTime
            : stateEntity.BestTime;

        context.SaveChanges();
    }

    public GameState LoadProgress(string playerName, int levelNum)
    {
        var gameState = new GameState
        {
            CurrentLevel = levelNum,
        };

        using var context = new AppDbContext();
        var playerEntity = context.Players.FirstOrDefault(p => p.Name == playerName);
        if (playerEntity == null)
        {
            return gameState;
        }

        var stats = context.Statistics.FirstOrDefault(x => x.Player == playerEntity && x.Level.Id == levelNum);

        if (stats == null)
            return gameState;

        gameState.BestMovesCount = stats.BestCount;
        gameState.BestPassedTime = stats.BestTime;
        gameState.PassedLevels = context.Statistics.Count(s => s.Player == playerEntity);

        return gameState;
    }
}