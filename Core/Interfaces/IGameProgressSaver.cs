using Core.Models;

namespace Core.Interfaces;

public interface IGameProgressSaver
{
    void SaveProgress(GameState state);
}