using Microsoft.Xna.Framework;

namespace ShinpansenEngine._2D.Graphics.Geometry
{
    public abstract class Transformable
    {
        public Vector2 Location = new Vector2(50, 50);
        public Vector2 Origin = new Vector2(0, 0);
        public Vector2 Rotation = new Vector2(0, 0);
        public Vector2 Scale = new Vector2(0, 0);
    }
}