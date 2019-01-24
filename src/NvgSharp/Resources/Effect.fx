#include "Macros.fxh"

DECLARE_TEXTURE(g_texture, 0);

BEGIN_CONSTANTS

    float2 viewSize;    
    float4 scissorExt;
    float4 scissorScale;
    float4 extent;
    float4 radius;
    float4 feather;
    float4 innerCol;
    float4 outerCol;
    float4 strokeMult;

MATRIX_CONSTANTS

    float4x4 dummy;
    float4x4 scissorMat;
    float4x4 paintMat;

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
    Output.position = float4(2.0 * pt.x / viewSize.x - 1.0, 1.0 - 2.0 * pt.y / viewSize.y, 0, 1);
     
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
#else
    float strokeAlpha = 1.0f;
#endif
	// Calculate gradient color using box gradient
	float2 pt = (mul((float3x3)paintMat, float3(input.fpos,1.0))).xy;
	float d = clamp((sdroundrect(pt, extent.xy, radius.x) + feather.x*0.5) / feather.x, 0.0, 1.0);
	float4 color = lerp(innerCol, outerCol, d);
	
	// Combine alpha
	color *= strokeAlpha * scissor;
    result = color;

#ifdef EDGE_AA
    if (strokeAlpha < strokeMult.y)
        discard;
#endif

    return result;
}

float4 PSMainFillImage(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
    float strokeAlpha = strokeMask(input.ftcoord);
#else
    float strokeAlpha = 1.0f;
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

#ifdef EDGE_AA
    if (strokeAlpha < strokeMult.y)
        discard;
#endif

    return result;
}

float4 PSMainSimple(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
    float strokeAlpha = strokeMask(input.ftcoord);
#else
    float strokeAlpha = 1.0f;
#endif
	// Stencil fill
	result = float4(1,1,1,1);
#ifdef EDGE_AA
    if (strokeAlpha < strokeMult.y)
        discard;
#endif

    return result;
}

float4 PSMainTriangles(PS_INPUT input) : SV_TARGET
{
    float4 result;
    float scissor = scissorMask(input.fpos);
#ifdef EDGE_AA
    float strokeAlpha = strokeMask(input.ftcoord);
#else
    float strokeAlpha = 1.0f;
#endif
	// Textured tris
	float4 color = SAMPLE_TEXTURE(g_texture, input.ftcoord);
	color = float4(color.xyz*color.w,color.w);
	color *= scissor;
	result = (color * innerCol);
#ifdef EDGE_AA
    if (strokeAlpha < strokeMult.y)
        discard;
#endif

    return result;
}

TECHNIQUE(FillGradient, VSMain, PSMainFillGradient);
TECHNIQUE(FillImage, VSMain, PSMainFillImage);
TECHNIQUE(Simple, VSMain, PSMainSimple);
TECHNIQUE(Triangles, VSMain, PSMainTriangles);