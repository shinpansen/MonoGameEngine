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

float4 FillCircleFunction(VertexShaderOutput output) : COLOR 
{
    float dx = abs(output.coords.x - 0.5f);
    float dy = abs(output.coords.y - 0.5f);
    float4 textureColor = tex2D(TextureSampler, output.coords);
    float outlineLimit = 0.5f - (0.5f * (OutlineSize / (max(Width, Height) * 0.5f)));
    float evalOutline = 1;
    if (Width > Height)
    {
        float m = Width / Height;
        evalOutline = sqrt(dx * dx + (dy*m * dy * m));
    }
    else
    {
        float m = Height / Width;
        evalOutline = sqrt((dx*m * dx * m) + dy * dy);
    }

    float radiusH = 1.0f - (OutlineSize / Width / 2.0f);
    float radiusV = 1.0f - (OutlineSize / Height / 2.0f);
    float radiusAverage = (radiusH + radiusV) * 0.5f;

    float minRadius = 0.0f;
    float x = abs(output.coords.x - 0.5f);
    float y = abs(output.coords.y - 0.5f);
    if( x > y ) {
        minRadius = lerp( radiusH, radiusAverage, y / x );
    }
    else {
        minRadius = lerp( radiusV, radiusAverage, x / y );
    }

    float radius = sqrt(x * x + y * y);

    if(x * x + y * y <= 1.0f && radius >= minRadius)
        return OutlineCol;
    else if(dx * dx + dy * dy <= 0.25f)
        return textureColor;
    else
        return float4(0.0f, 0.0f, 0.0f, 0.0f);
}

technique FillCircleTechnique
{
    pass Pass
    {
        PixelShader = compile PS_SHADERMODEL FillCircleFunction(); 
    }
}