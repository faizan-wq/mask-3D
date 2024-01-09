Shader"Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.01
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}

        Pass
        {
            Stencil
            {
Ref1
                Compalways
                Pass replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float4 screenPos : TEXCOORD0;
};

float _OutlineThickness;
float4 _OutlineColor;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.screenPos = ComputeScreenPos(o.vertex);
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 col = half4(0, 0, 0, 0);
    float2 screenPos = i.screenPos.xy / i.screenPos.w;
    float2 ddx = ddx(screenPos);
    float2 ddy = ddy(screenPos);
    float2 normal = normalize(float2(ddy.y, -ddx.x));

    float2 offset = normal * _OutlineThickness;

    col = tex2D(_MainTex, screenPos + offset);
    col.rgb = _OutlineColor.rgb;
    col.a = 1.0;

    return col;
}
            ENDCG
        }
    }
}
}
