Shader "Pieka/wheel"
{

	Properties
	{
		[PerRendererData] _ATex("ATex", 2D) = "white" {}
		[PerRendererData] _BTex("BTex", 2D) = "white" {} 
		[PerRendererData] _Trans("_Trans", float) = 0
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _ATex;
			sampler2D _BTex;
			float _Trans;
			float4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{ 
				float4 colA = tex2D(_ATex, i.uv);
				float4 colB = tex2D(_BTex, i.uv);
				float4 col = colA * (1-_Trans) + colB * _Trans;
				return col * _Color;
			}
			ENDCG
		}
	}
}