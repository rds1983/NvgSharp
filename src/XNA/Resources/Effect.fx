#include "Macros.fxh"

DECLARE_TEXTURE(g_texture, 0);

BEGIN_CONSTANTS

    float4 innerCol;
    float4 outerCol;
    float2 scissorExt;
    float2 scissorScale;
    float2 extent;
    float radius;
    float feather;
    float strokeMult;
    float strokeThr;

MATRIX_CONSTANTS

    float4x4 dummy;
    float4x4 scissorMat;
    float4x4 paintMat;
	float4x4 transformMat;

END_CONSTANTS


struct VS_OUTPUT
{
    float4 position   : SV_Position;    // vertex position
    float2 ftcoord    : TEXCOORD0;      // float 2 tex coord
    float2 fpos       : TEXCOORD1;      // float 2 position 
};
  
struct PS_INPUT
{
    float4 position   : SV_Position;    // vertex position
    float2 ftcoord    : TEXCOORD0;      // float 2 tex coord
    float2 fpos       : TEXCOORD1;      // float 2 position 
};

VS_OUTPUT VSMain(float2 pt : POSITION, float2 tex : TEXCOORD0)
{
    VS_OUTPUT Output;
    Output.ftcoord = tex;
    Output.fpos = pt;
    Output.position = mul(float4(pt.x, pt.y, 0, 1), transformMat);
     
    return Output;
}

float sdroundrect(float2 pt, float2 ext, float rad) 
{
    float2 ext2 = ext - float2(rad,rad);
    float2 d = abs(pt) - ext2;
    return min(max(d.x,d.y),0.0) + length(max(d,0.0)) - rad;
}

// Scissoring
float scissorMask(float2 p) 
{
    float2 sc = (abs((mul((float3x3)scissorMat, float3(p.x, p.y, 1.0))).xy) - scissorExt.xy);
    sc = float2(0.5,0.5) - sc * scissorScale.xy;
    return clamp(sc.x,0.0,1.0) * clamp(sc.y,0.0,1.0);
}

#ifdef EDGE_AA
// Stroke - from [0..1] to clipped pyramid, where the slope is 1px.
float strokeMask(float2 ftcoord)
{
    return min(1.0, (1.0 - abs(ftcoord.x*2.0 - 1.0))*strokeMult.x) * min(1.0f, ftcoord.y);
}
#endif

float4 PSMainFillGradient(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
    float strokeAlpha = strokeMask(input.ftcoord);
    if (strokeAlpha < strokeThr) discard;
#else
    float strokeAlpha = 1.0;
#endif
	// Calculate gradient color using box gradient
	float2 pt = (mul((float3x3)paintMat, float3(input.fpos,1.0))).xy;
	float d = clamp((sdroundrect(pt, extent.xy, radius.x) + feather.x*0.5) / feather.x, 0.0, 1.0);
	float4 color = lerp(innerCol, outerCol, d);
	
	// Combine alpha
	color *= strokeAlpha * scissor;
    result = color;

    return result;
}

float4 PSMainFillImage(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
    float strokeAlpha = strokeMask(input.ftcoord);
    if (strokeAlpha < strokeThr) discard;
#else
    float strokeAlpha = 1.0;
#endif
	// Calculate color fron texture
	float2 pt = (mul((float3x3)paintMat, float3(input.fpos,1.0))).xy / extent.xy;
	float4 color = SAMPLE_TEXTURE(g_texture, pt);
	color = float4(color.xyz*color.w,color.w);
	
	// Apply color tint and alpha.
	color *= innerCol;
	// Combine alpha
	color *= strokeAlpha * scissor;
	result = color;

    return result;
}

float4 PSMainSimple(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
    float strokeAlpha = strokeMask(input.ftcoord);
    if (strokeAlpha < strokeThr) discard;
#else
    float strokeAlpha = 1.0;
#endif
	// Stencil fill
	result = float4(1,1,1,1);

    return result;
}

float4 PSMainTriangles(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
	float strokeAlpha = strokeMask(input.ftcoord);
	if (strokeAlpha < strokeThr) discard;
#else
	float strokeAlpha = 1.0;
#endif
	// Textured tris
	float4 color = SAMPLE_TEXTURE(g_texture, input.ftcoord);
	color *= scissor;
	result = (color * innerCol);

    return result;
}

TECHNIQUE(FillGradient, VSMain, PSMainFillGradient);
TECHNIQUE(FillImage, VSMain, PSMainFillImage);
TECHNIQUE(Simple, VSMain, PSMainSimple);
TECHNIQUE(Triangles, VSMain, PSMainTriangles);