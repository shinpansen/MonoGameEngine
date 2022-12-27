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

float4 FillRectangleFunction(VertexShaderOutput output) : COLOR 
{
	float4 textureColor = tex2D(TextureSampler, output.coords);
	return textureColor;
}

technique FillCircleTechnique
{
    pass Pass
    {
        PixelShader = compile PS_SHADERMODEL FillRectangleFunction(); 
    }
}