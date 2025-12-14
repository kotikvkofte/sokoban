using Core.Interfaces;
using Core.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Screens;

public class MapDrawer(
    Texture2D groundTexture,
    Texture2D wallTexture,
    Texture2D boxTexture,
    Texture2D targetTexture,
    Texture2D playerTexture)
{
    const float TARGET_SCALE = 0.5f;
    const float PLAYER_SCALE = 0.7f;

    public int TileSize { get; set; } = 50;
    
    public void DrawMap(LevelMap levelMap, SpriteBatch spriteBatch)
    {
        for (var y = 0; y < levelMap.Height; y++)
        for (var x = 0; x < levelMap.Width; x++)
        {
            spriteBatch.Draw(groundTexture, GetRectangle(x, y), Color.White);

            if (levelMap.IsTarget(x, y))
                spriteBatch.Draw(targetTexture, GetRectangle(x, y, TARGET_SCALE), Color.White);
        }

        foreach (var wall in levelMap.Walls)
            spriteBatch.Draw(wallTexture, GetRectangle(wall), Color.White);

        foreach (var box in levelMap.Boxes)
            spriteBatch.Draw(boxTexture, GetRectangle(box), Color.White);

        spriteBatch.Draw(playerTexture, GetRectangle(levelMap.Player, PLAYER_SCALE), Color.White);
    }
    
    private Rectangle GetRectangle(int cellX, int cellY, float scale = 1)
    {
        var size = (int)(TileSize * scale);
        var offset = (TileSize - size) / 2;

        var x = cellX * TileSize + offset;
        var y = cellY * TileSize + offset;

        return new Rectangle(x, y, size, size);
    }

    private Rectangle GetRectangle(IMapObject obj, float scale = 1) =>
        GetRectangle(obj.Position.X, obj.Position.Y, scale);
}