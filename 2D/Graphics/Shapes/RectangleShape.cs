using Microsoft.Xna.Framework;
using ShinpansenEngine.Engines;
using ShinpansenEngine._2D.Graphics.Interfaces;

namespace ShinpansenEngine._2D.Graphics.Shapes;

public class RectangleShape : DrawableShape
{
    private static readonly string RectangleShapeAssetName = "Shaders/Rectangle";

    public RectangleShape(GraphicsEngine graphicsEngine) : 
                          base(graphicsEngine, RectangleShapeAssetName)
    {
    }

    public RectangleShape(GraphicsEngine graphicsEngine, 
                          Vector2 location, 
                          Vector2 size) : 
                          base(graphicsEngine, RectangleShapeAssetName, location, size)
    {
    }

    public RectangleShape(GraphicsEngine graphicsEngine,
                          string textureAssetName,
                          Vector2 location,
                          Vector2 size) : 
                          base(graphicsEngine, RectangleShapeAssetName, textureAssetName, location, size)
    {
    }

    public RectangleShape(GraphicsEngine graphicsEngine, 
                          Color color, 
                          Vector2 location, 
                          Vector2 size) : 
                          base(graphicsEngine, RectangleShapeAssetName, color, location, size)
    {
    }
}