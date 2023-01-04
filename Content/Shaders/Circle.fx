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

float4 CircleFunctionNew(VertexShaderOutput output) : COLOR 
{
    float maxSize = max(Width, Height);
    float width = Width / maxSize;
    float height = Height / maxSize;

    float2 center = float2(0.5f, 0.5f);
    float x = output.coords.x;
    float y = output.coords.y;

    x = cos(0.523599f) * (x-0.5f) - sin(0.523599f) * (y-0.5f) + 0.5f;
    y = sin(0.523599f) * (x-0.5f) + cos(0.523599f) * (y-0.5f) + 0.5f; 

    if (x < 0.5f)
        x = 0.5f - (0.5f - x) / width;
    else
        x = 0.5f + (x - 0.5f) / width;

    if (y < 0.5f)
        y = 0.5f - (0.5f - y) / height;
    else
        y = 0.5f + (y - 0.5f) / height;

    float distance = length(float2(x, y) - center);

    if (distance <= 0.5f)
        return tex2D(TextureSampler, output.coords);
    else
        return float4(0.0f, 0.0f, 0.0f, 1.0f);
}

float4 CircleFunction(float dx, float dy, float4 textureColor) : COLOR 
{
    if(dx * dx + dy * dy <= 0.25f)
        return textureColor;
    else
        return float4(0.0f, 0.0f, 0.0f, 0.0f);
}

float4 OutlineCircleFunction(VertexShaderOutput output) : COLOR 
{
    float4 textureColor = tex2D(TextureSampler, output.coords);

    float x = output.coords.x;
    float y = output.coords.y;
    float px = cos(0.523599f) * (x-0.5f) - sin(0.523599f) * (y-0.5f) + 0.5;
    float py = sin(0.523599f) * (x-0.5f) + cos(0.523599f) * (y-0.5f) + 0.5;
    float dx = px - 0.5f;
    float dy = py - 0.5f;

    if(OutlineSize == 0)
        return CircleFunction(dx, dy, textureColor);

    float maxSize = max(Width, Height);
    float minSize = min(Width, Height);
    float outlineRatio = OutlineSize*2.0f/maxSize;
    float minMaxRatio = maxSize/minSize;

    float dxOut;
    if (maxSize == Width)
        dxOut = (px - 0.5f) * (1.0f + outlineRatio);
    else
        dxOut = (px - 0.5f) * (1.0f + outlineRatio * minMaxRatio);

    float dyOut;
    if (maxSize == Height)
        dyOut = (py - 0.5f) * (1.0f + outlineRatio);
    else
        dyOut = (py - 0.5f) * (1.0f + outlineRatio * minMaxRatio);

    if(dxOut * dxOut + dyOut * dyOut <= 0.25f)
        return textureColor;
    else 
        return CircleFunction(dx, dy, OutlineCol);
}

technique CircleTechnique
{
    pass Pass
    {
        PixelShader = compile PS_SHADERMODEL CircleFunctionNew(); 
    }
}