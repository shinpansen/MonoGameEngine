using Microsoft.Xna.Framework;

namespace ShinpansenEngine._2D.Graphics.Interfaces;

public interface IDrawable2D
{
    void SetTextureFromContent(string content);
    void SetTextureFromColor(Color color);
    Point GetCanvasSize();
    void Draw(GameTime gameTime);
}