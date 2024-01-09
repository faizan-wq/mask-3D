Shader "Shader Graphs/PlasticDirt" {
	Properties {
		Color_9B66A82A ("Color", Vector) = (0.1215686,0.1215686,0.1215686,0)
		[NoScaleOffset] Texture2D_83AEF590 ("Mask Map", 2D) = "white" {}
		Vector1_32245D37 ("Smoothness Remap Min", Range(0, 1)) = 0.064
		Vector1_58C2FC05 ("Smoothness Remap Max", Range(0, 1)) = 0
		Vector1_F35F1AE8 ("Occlusion Intensity", Range(0, 1)) = 0
		Vector2_AD3BE3E0 ("Tiling", Vector) = (10,10,0,0)
		Vector2_B2F0D7BA ("Offset", Vector) = (0,0,0,0)
		[ToggleUI] Boolean_7C9DEA63 ("Make Triplanar", Float) = 0
		[ToggleUI] Boolean_78B83922 ("Make Triplanar WorldSpace (Triplanar must be activated)", Float) = 0
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