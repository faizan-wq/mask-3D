Shader "Shader Graphs/CarPaint" {
	Properties {
		[NoScaleOffset] Texture2D_6442A3D ("Base Color Map", 2D) = "white" {}
		Color_3040B7A4 ("Color", Vector) = (1,0,0,0)
		[NoScaleOffset] Texture2D_8275A5B2 ("Normal Map", 2D) = "white" {}
		Vector1_C2847D76 ("Normal Intensity", Range(0, 1)) = 1
		[NoScaleOffset] Texture2D_156EB7C1 ("Mask Map", 2D) = "white" {}
		Vector1_725D1505 ("Metallic Intensity", Range(0, 1)) = 0.562
		Vector1_5258EE3 ("Occlusion Intensity", Range(0, 1)) = 1
		Vector1_80ED81F4 ("Smoothness Remap Min", Range(0, 1)) = 0
		Vector1_80AA701 ("Smoothness Remap Max", Range(0, 1)) = 1
		Vector2_78863940 ("Tiling", Vector) = (10,10,0,0)
		[ToggleUI] Boolean_748C5B4A ("Make Triplanar", Float) = 0
		[ToggleUI] Boolean_7513451A ("Make Triplanar WorldSpace (Triplanar must be activated)", Float) = 0
		Vector1_615CB50D ("Triplanar Blend", Float) = 0
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