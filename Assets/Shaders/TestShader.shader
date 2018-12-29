Shader "Pieka/test"
{
SubShader
    {
        // Draw ourselves after all opaque geometry
        Tags 
		{
			 "Queue" = "Transparent" 
			 "PreviewType" = "Plane"
		}

        // Grab the screen behind the object into _BackgroundTexture
        GrabPass
        {
            "_BackgroundTexture"
        }

        // Render the object with the texture generated above, and invert the colors
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v) {
                v2f o;
                // use UnityObjectToClipPos from UnityCG.cginc to calculate 
                // the clip-space of the vertex
                o.pos = UnityObjectToClipPos(v.vertex);
                // use ComputeGrabScreenPos function from UnityCG.cginc
                // to get the correct texture coordinate
                o.grabPos = ComputeGrabScreenPos(o.pos);
                return o;
            }

            sampler2D _BackgroundTexture;

            float4 frag(v2f i) : SV_Target
            {
                float4 bgcolor = tex2D(_BackgroundTexture, i.grabPos);
				bgcolor *= bgcolor.a;
				return 1-bgcolor;
            }
            ENDCG
        }

    }
}