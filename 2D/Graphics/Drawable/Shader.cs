using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ShinpansenEngine._2D.Graphics.Drawable
{
    public class Shader : IDisposable
    {
        public Shader(string assetName, Effect shaderEffect)
        {
            this.AssetName = assetName;
            this.ShaderEffect = shaderEffect;
        }

        ~Shader()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            ShaderEffect?.Dispose();
            WidthShaderParameter = null;
            HeightShaderParameter = null;
            OutlineThicknessShaderParameter = null;
            OutlineColorShaderParameter = null;
            RotationShaderParameter = null;
            GC.SuppressFinalize(this);
        }

        public readonly string AssetName;
        public readonly Effect ShaderEffect;
        public EffectParameter WidthShaderParameter = null;
        public EffectParameter HeightShaderParameter = null;
        public EffectParameter OutlineThicknessShaderParameter = null;
        public EffectParameter OutlineColorShaderParameter = null;
        public EffectParameter RotationShaderParameter = null;

        
    }
}