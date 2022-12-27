using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ShinpansenEngine._2D.Primitives;

namespace ShinpansenEngine.Engines;

public class GraphicsEngine : IDisposable
{
    public readonly GraphicsDevice GraphicsDevice;
    public readonly ContentManager ContentManager;
    public readonly SpriteBatch SpriteBatch;
    private HashSet<Shader> _shaders = new HashSet<Shader>();

    public GraphicsEngine(GraphicsDevice graphicsDevice, ContentManager contentManager)
    {
        this.GraphicsDevice = graphicsDevice;
        this.ContentManager = contentManager;
        this.SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    ~GraphicsEngine()
    {
        this.Dispose();
    }

    public void Dispose()
    {
        SpriteBatch?.Dispose();
        foreach(var shader in _shaders) 
            shader?.Dispose();
        GC.SuppressFinalize(this);
    }

    public Shader GetShader(string assetName)
    {
        var shaders = _shaders.Where(s => s.AssetName == assetName);
        if(shaders.Count() > 0)
            return shaders.First();
        else
        {
            var effect = ContentManager.Load<Effect>(assetName);
            var shader = new Shader(assetName, effect);
            _shaders.Add(shader);
            return shader;
        }
    }
}