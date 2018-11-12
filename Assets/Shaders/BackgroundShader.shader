Shader "Pieka/background"
{

	Properties
	{
		_MainText("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Size("Size", range(1, 100000)) = 10
	}

	SubShader
	{
		Tags
		{
			"PreviewType" = "Plane"
		}
		Pass
		{
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

			sampler2D _MainText;
			float4 _Color;
			float _Size;

			float4 frag(v2f i) : SV_Target
			{ 
				float4 color = tex2D(_MainText, i.uv*_Size);
				return color * _Color;
			}
			ENDCG
		}
	}
}