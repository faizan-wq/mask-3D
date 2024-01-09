Shader "Shader Graphs/WaterShader" {
	Properties {
		[HDR] Color_8e292e7e7cec45c3928f1925582227fd ("Color", Vector) = (0.1688768,0.3241735,0.6509434,0)
		Vector1_d39022b7a18c402e9641c98ae8506ecb ("DistortionSpeed", Float) = 1
		Vector2_4db40338dcdc4c39b1ed6d2b1075b338 ("DistortionTile", Vector) = (1,1,0,0)
		Vector2_12c4c0a4cbb94dd6a1b4bf29a3199a3a ("DistortionDir", Vector) = (1,0,0,0)
		Vector1_9e547fc312314b6488f909cd1295b445 ("Alpha", Float) = 0
		Vector1_a78243a142a24aa4bfbea2692297902e ("NoiseSize", Float) = 1
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