﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ScratchCard/MaskProgress" {
    Properties {
        _MainTex ("Main", 2D) = "white" {}
    }

    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        ZTest Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct app2vert
            {
                float4 position: POSITION;
                float4 color: COLOR;
                float2 texcoord: TEXCOORD0;
            };

            struct vert2frag
            {
                float4 position: POSITION;
                float4 color: COLOR;
                float2 texcoord: TEXCOORD0;
            };

            vert2frag vert(app2vert input)
            {
                vert2frag output;
                output.position = UnityObjectToClipPos(input.position);
				output.color = input.color;
                output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
                return output;
            }

            float4 frag(vert2frag input) : COLOR
            {
            	float4 col = input.color;
                float count = 0.0f;
                float texWidth = 15.0f;
                float texHeight = 15.0f;
                for	(int i = 0; i < 15; i++)
                {
                	for	(int j = 0; j < 15; j++)
                	{
                		float2 newTexcoord = float2(i / (texWidth - 1.0f), j / (texHeight - 1.0f));
                		count += tex2D(_MainTex, newTexcoord).a;
                	}
                }

                count /= 225.0f;
                return float4(count, count, count, 1.0f) * col;
            }
            ENDCG
        }
    }
}
