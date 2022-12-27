#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

struct VertexShaderOutput
{
    float2 coords : TEXCOORD0;
    float4 color : COLOR0;
};

sampler2D TextureSampler;
float Width;
float Height;
float2 Rotation;
float4 OutlineCol;
float OutlineSize;

float4 CircleFunction(float dx, float dy, float4 textureColor) : COLOR 
{
    if(dx * dx + dy * dy <= 0.25f)
        return textureColor;
    else
        return float4(0.0f, 0.0f, 0.0f, 0.0f);
}

float4 OutlineCircleFunction(VertexShaderOutput output) : COLOR 
{
    float dx = output.coords.x - 0.5f;
    float dy = output.coords.y - 0.5f;
    float4 textureColor = tex2D(TextureSampler, output.coords);

    if(OutlineSize == 0)
        return CircleFunction(dx, dy, textureColor);

    float maxSize = max(Width, Height);
    float minSize = min(Width, Height);
    float outlineRatio = OutlineSize*2.0f/maxSize;
    float minMaxRatio = maxSize/minSize;

    float dxOut;
    if (maxSize == Width)
        dxOut = (output.coords.x - 0.5f) * (1.0f + outlineRatio);
    else
        dxOut = (output.coords.x - 0.5f) * (1.0f + outlineRatio * minMaxRatio);

    float dyOut;
    if (maxSize == Height)
        dyOut = (output.coords.y - 0.5f) * (1.0f + outlineRatio);
    else
        dyOut = (output.coords.y - 0.5f) * (1.0f + outlineRatio * minMaxRatio);

    if(dxOut * dxOut + dyOut * dyOut <= 0.25f)
        return textureColor;
    else 
        return CircleFunction(dx, dy, OutlineCol);
}

technique CircleTechnique
{
    pass Pass
    {
        PixelShader = compile PS_SHADERMODEL OutlineCircleFunction(); 
    }
}