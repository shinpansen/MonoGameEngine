using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ShinpansenEngine._2D.Graphics.Drawable;

namespace ShinpansenEngine.Engines;

public class GraphicsEngine : IDisposable
{
    public readonly GraphicsDevice GraphicsDevice;
    public readonly ContentManager ContentManager;
    public readonly SpriteBatch SpriteBatch;
    private HashSet<Tuple<string, Texture2D>> _textures2D = new HashSet<Tuple<string, Texture2D>>();
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

    public Texture2D GetTexture2D(string assetName)
    {
        var textures = _textures2D.Where(s => s.Item1 == assetName);
        if(textures.Count() > 0)
            return textures.First().Item2;
        else
        {
            var texture = ContentManager.Load<Texture2D>(assetName);
            _textures2D.Add(new Tuple<string, Texture2D>(assetName, texture));
            return texture;
        }
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