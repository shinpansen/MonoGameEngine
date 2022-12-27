using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinpansenEngine.Engines;

namespace ShinpansenEngine._2D.Graphics.Shapes
{
    public class CircleShape : OvalShape
    {
        public CircleShape(GraphicsEngine graphicsEngine) : 
                           base(graphicsEngine)
        {
        }

        public CircleShape(GraphicsEngine graphicsEngine,
                           Vector2 location, 
                           float radius) : 
                           base(graphicsEngine, location, new Vector2(radius*2, radius*2))
        {
        }

        public CircleShape(GraphicsEngine graphicsEngine, 
                           string textureAssetName,
                           Vector2 location, 
                           float radius) : 
                           base(graphicsEngine, textureAssetName, location, 
                           new Vector2(radius*2, radius*2))
        {
        }

        public CircleShape(GraphicsEngine graphicsEngine, 
                           Color color,
                           Vector2 location, 
                           float radius) : 
                           base(graphicsEngine, color, location, 
                           new Vector2(radius*2, radius*2))
        {
        }
    }
}