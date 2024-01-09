Shader "FingersGestures/FingersExampleTransitionShader" {
	Properties {
		_MainTex1 ("Texture", 2D) = "green" {}
		_MainTex2 ("Texture", 2D) = "blue" {}
		_Fade ("Fade", Range(0, 1)) = 0.5
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
}