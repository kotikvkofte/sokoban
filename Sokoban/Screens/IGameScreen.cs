using Microsoft.Xna.Framework;

namespace Sokoban.Screens;

public interface IGameScreen
{
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime);
}