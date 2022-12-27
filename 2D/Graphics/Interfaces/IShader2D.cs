using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinpansenEngine._2D.Graphics.Interfaces;

public interface IShader2D
{
    void InitializeShaderParameters();
    void UpdateShaderParameters();
}