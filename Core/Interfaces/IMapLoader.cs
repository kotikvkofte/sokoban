using Core.Models;

namespace Core.Interfaces;

public interface IMapLoader
{
    LevelMap LoadLevel(int levelNumber, string playerName);
}