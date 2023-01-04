using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinpansenEngine.Engines;
using ShinpansenEngine._2D.Graphics.Drawable;
using ShinpansenEngine._2D.Graphics.Geometry;
using ShinpansenEngine._2D.Graphics.Interfaces;

namespace ShinpansenEngine._2D.Graphics.Drawable
{
    public abstract class Shape : Geometry.Shape, IDrawable2D, IShader2D
    {
        protected GraphicsEngine GraphicsEngine;
        protected Texture2D Texture2D;
        protected readonly string ShaderAssetName = "Shaders/Rectangle";
        protected readonly Shader Shader = null;
        public Color OutlineColor = Color.Transparent;
        public uint OutlineThickness = 100;

        public Shape(GraphicsEngine graphicsEngine, string shaderAssetName)
        {
            if(graphicsEngine == null)
                throw new ArgumentNullException("GraphicsEngine must be initialized");
            this.GraphicsEngine = graphicsEngine;
            this.ShaderAssetName = shaderAssetName;
            this.Shader = graphicsEngine.GetShader(shaderAssetName);
            InitializeShaderParameters();
        }

        public Shape(GraphicsEngine graphicsEngine, 
                     string shaderAssetName,
                     Vector2 location, 
                     Vector2 size) : 
                     this(graphicsEngine, shaderAssetName)
        {
            this.Location = location;
            this.Size = size;
            SetTextureFromColor(Color.Red);
        }

        public Shape(GraphicsEngine graphicsEngine, 
                     string shaderAssetName,
                     Vector2 location, 
                     Vector2 size,
                     uint outlineThickness,
                     Color outlineColor) : 
                     this(graphicsEngine, shaderAssetName, location, size)
{
            this.OutlineThickness = outlineThickness;
            this.OutlineColor = outlineColor;
        }

        public Shape(GraphicsEngine graphicsEngine,
                     string shaderAssetName,
                     string textureAssetName,
                     Vector2 location,
                     Vector2 size) : 
                     this(graphicsEngine, shaderAssetName, location, size)
        {
            SetTextureFromContent(textureAssetName);
        }

        public Shape(GraphicsEngine graphicsEngine,
                     string shaderAssetName,
                     string textureAssetName,
                     Vector2 location,
                     Vector2 size,
                     uint outlineThickness,
                     Color outlineColor) : 
                     this(graphicsEngine, shaderAssetName, location, size, outlineThickness, outlineColor)
        {
            SetTextureFromContent(textureAssetName);
        }

        public Shape(GraphicsEngine graphicsEngine,
                     string shaderAssetName,
                     Color color, 
                     Vector2 location, 
                     Vector2 size) : 
                     this(graphicsEngine, shaderAssetName, location, size)
        {
            SetTextureFromColor(color);
        }

        public Shape(GraphicsEngine graphicsEngine,
                     string shaderAssetName,
                     Color color, 
                     Vector2 location, 
                     Vector2 size,
                     uint outlineThickness,
                     Color outlineColor) : 
                     this(graphicsEngine, shaderAssetName, location, size, outlineThickness, outlineColor)
        {
            SetTextureFromColor(color);
        }

        public void SetTextureFromContent(string assetName)
        {
            Texture2D = GraphicsEngine.GetTexture2D(assetName);
        }

        public void SetTextureFromColor(Color color)
        {
            Texture2D = new Texture2D(GraphicsEngine.GraphicsDevice, 1, 1);
            Texture2D.SetData<Color>(new Color[] { color });
        }

        public virtual Point GetCanvasSize()
        {
            return new Point((int)this.Size.X, (int)this.Size.Y);
        }

        public void InitializeShaderParameters()
        {
            if(Shader == null) return;
            Shader.WidthShaderParameter = Shader.ShaderEffect.Parameters["Width"];
            Shader.HeightShaderParameter = Shader.ShaderEffect.Parameters["Height"];
            Shader.RotationShaderParameter = Shader.ShaderEffect.Parameters["Rotation"];
            Shader.OutlineThicknessShaderParameter = Shader.ShaderEffect.Parameters["OutlineSize"];
            Shader.OutlineColorShaderParameter = Shader.ShaderEffect.Parameters["OutlineCol"];
        }

        public void UpdateShaderParameters()
        {
            if(Shader == null) return;
            Shader.WidthShaderParameter?.SetValue(this.Size.X);
            Shader.HeightShaderParameter?.SetValue(this.Size.Y);
            Shader.RotationShaderParameter?.SetValue(this.Rotation);
            Shader.OutlineThicknessShaderParameter?.SetValue(this.OutlineThickness);
            Shader.OutlineColorShaderParameter?.SetValue(this.OutlineColor.ToVector4());
        }

        public void Draw(GameTime gameTime)
        {
            UpdateShaderParameters();
            var test = GetCanvasSize();
            GraphicsEngine.SpriteBatch.Begin(effect:Shader.ShaderEffect);
            GraphicsEngine.SpriteBatch.Draw(Texture2D, 
                new Rectangle(Location.ToPoint(), GetCanvasSize()), Color.White);
            GraphicsEngine.SpriteBatch.End();
        }
    }
}