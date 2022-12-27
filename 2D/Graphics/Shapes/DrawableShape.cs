using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShinpansenEngine.Engines;
using ShinpansenEngine._2D.Primitives;
using ShinpansenEngine._2D.Graphics.Interfaces;

namespace ShinpansenEngine._2D.Graphics.Shapes
{
    public abstract class DrawableShape : Shape, IDrawable2D, IShader2D
    {
        protected GraphicsEngine GraphicsEngine;
        protected Texture2D Texture2D;
        protected readonly string ShaderAssetName = "Shaders/Rectangle";
        protected readonly Shader Shader = null;
        public Color OutlineColor = Color.Transparent;
        public uint OutlineThickness = 100;

        public DrawableShape(GraphicsEngine graphicsEngine, string shaderAssetName)
        {
            if(graphicsEngine == null)
                throw new ArgumentNullException("GraphicsEngine must be initialized");
            this.GraphicsEngine = graphicsEngine;
            this.ShaderAssetName = shaderAssetName;
            this.Shader = graphicsEngine.GetShader(shaderAssetName);
            InitializeShaderParameters();
        }

        public DrawableShape(GraphicsEngine graphicsEngine, 
                             string shaderAssetName,
                             Vector2 location, 
                             Vector2 size) : 
                             this(graphicsEngine, shaderAssetName)
        {
            this.Location = location;
            this.Size = size;
            SetTextureFromColor(Color.Red);
        }

        public DrawableShape(GraphicsEngine graphicsEngine, 
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

        public DrawableShape(GraphicsEngine graphicsEngine,
                             string shaderAssetName,
                             string textureAssetName,
                             Vector2 location,
                             Vector2 size) : 
                             this(graphicsEngine, shaderAssetName, location, size)
        {
            SetTextureFromContent(textureAssetName);
        }

        public DrawableShape(GraphicsEngine graphicsEngine,
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

        public DrawableShape(GraphicsEngine graphicsEngine,
                             string shaderAssetName,
                             Color color, 
                             Vector2 location, 
                             Vector2 size) : 
                             this(graphicsEngine, shaderAssetName, location, size)
        {
            SetTextureFromColor(color);
        }

        public DrawableShape(GraphicsEngine graphicsEngine,
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

        public void SetTextureFromContent(string content)
        {
            Texture2D = GraphicsEngine.ContentManager.Load<Texture2D>(content);
        }

        public void SetTextureFromColor(Color color)
        {
            Texture2D = new Texture2D(GraphicsEngine.GraphicsDevice, 1, 1);
            Texture2D.SetData<Color>(new Color[] { color });
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
            GraphicsEngine.SpriteBatch.Begin(effect:Shader.ShaderEffect);
            GraphicsEngine.SpriteBatch.Draw(Texture2D, 
                new Rectangle(Location.ToPoint(), Size.ToPoint()), Color.White);
            GraphicsEngine.SpriteBatch.End();
        }
    }
}