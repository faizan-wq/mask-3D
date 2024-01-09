Shader "Shader Graphs/GlassShader" {
	Properties {
		Vector1_29e24600119d4340b09b5d3375f19a29 ("Metalic", Range(0, 1)) = 0
		Vector1_c03d32c853214e5aa593a7f4c9280486 ("Smooth", Range(0, 1)) = 0
		_Color ("GlassColor", Vector) = (0,0,0,0)
		Vector1_eb28e3a121cf4d79a756ba0d9e39c686 ("ReflectionStrength", Float) = 1
		Vector1_1ddb9c712d6f4730856df22cd4cada9c ("NoiseStrength", Float) = 4
		Vector1_ad7076c0274d4b31815a784c3e6df6c0 ("NormalStrength", Float) = 1
		Vector1_1e80e116449c43a69e61bde7a50e8f73 ("DistortStrength", Float) = 1
		[NoScaleOffset] Cubemap_aa2bcdc082b945f7ac31d6346c260d4c ("Cubemap", Cube) = "" {}
		Color_013d2789dddb4e9a92c8ea997fd200f5 ("EmissionColor", Vector) = (0,0,0,0)
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "ShaderGraph.PBRMasterGUI"
}