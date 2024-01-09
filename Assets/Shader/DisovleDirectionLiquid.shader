Shader "DisovleDirectionLiquid" {
	Properties {
		Vector1_713f33dcd0ae47ac844cf04d7205e6c3 ("Alpha", Range(0, 1)) = 0
		Color_70670778238a4ece8fef643dcae31342 ("Color", Vector) = (1,0,0,0)
		Vector1_19f70bd615e342edbcd6c06ebb277d39 ("NoiseSize", Float) = 1
		Vector1_87c515af63d6419f9a116cec5c4bc0fe ("DistotionSpeed", Float) = 0.1
		Vector2_d2eef765449d46bf88d3d23f35682060 ("DistortionDir", Vector) = (1,1,0,0)
		Vector2_617db7dae0274855b52cbbeb2378a19a ("DistortionTile", Vector) = (1,1,0,0)
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "ShaderGraph.PBRMasterGUI"
}