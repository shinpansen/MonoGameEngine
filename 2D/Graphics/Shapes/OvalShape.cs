using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinpansenEngine.Engines;
using ShinpansenEngine._2D.Graphics.Interfaces;

namespace ShinpansenEngine._2D.Graphics.Shapes;

public class OvalShape : DrawableShape
{
    private static readonly string OvalShapeAssetName = "Shaders/Circle";

    public OvalShape(GraphicsEngine graphicsEngine) : 
                     base(graphicsEngine, OvalShapeAssetName)
    {
    }

    public OvalShape(GraphicsEngine graphicsEngine, 
                     Vector2 location, 
                     Vector2 size) : 
                     base(graphicsEngine, OvalShapeAssetName, location, size)
    {
    }

    public OvalShape(GraphicsEngine graphicsEngine,
                     string textureAssetName,
                     Vector2 location,
                     Vector2 size) : 
                     base(graphicsEngine, OvalShapeAssetName, textureAssetName, location, size)
    {
    }

    public OvalShape(GraphicsEngine graphicsEngine,
                     string textureAssetName,
                     Vector2 location,
                     Vector2 size,
                     uint outlineThickness,
                     Color outlineColor) : 
                     base(graphicsEngine, OvalShapeAssetName, textureAssetName, location, size, outlineThickness, outlineColor)
    {
    }

    public OvalShape(GraphicsEngine graphicsEngine,
                     Color color, 
                     Vector2 location, 
                     Vector2 size) : 
                     base(graphicsEngine, OvalShapeAssetName, color, location, size)
    {
    }

    public OvalShape(GraphicsEngine graphicsEngine,
                     Color color, 
                     Vector2 location, 
                     Vector2 size,
                     uint outlineThickness,
                     Color outlineColor) : 
                     base(graphicsEngine, OvalShapeAssetName, color, location, size, outlineThickness, outlineColor)
    {
    }
}