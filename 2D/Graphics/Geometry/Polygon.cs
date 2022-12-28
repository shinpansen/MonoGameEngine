using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ShinpansenEngine._2D.Graphics.Geometry;

public abstract class Polygon : Transformable
{
    public List<Vector2> Points = new List<Vector2>();
}