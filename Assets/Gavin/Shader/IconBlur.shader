Shader "Unlit/IconBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 avgCol = fixed4(0.0, 0.0, 0.0, 0.0);
                float avgCount = 0;
                float strength = 5000.0 + sin(_Time.x*100)*100.0;//lower is more blur
                for (int x = 0; x < 32; x++)
                {
                    for (int y = 0; y < 32; y++)
                    {

                        
                        float2 pos = i.uv - float2(float(x - 16) / 32, float(y - 16) / 32);
                        pos = clamp(pos, float2(0, 0), float2(1, 1));
                        float4 currentCol = tex2D(_MainTex, pos) * (1 - distance(i.uv, pos) * strength);
                        if (currentCol.r < 0 || currentCol.g < 0 || currentCol.b < 0 || currentCol.a < 0) {
                            continue;
                        }
                        avgCol += currentCol;
                        avgCount += (1 - distance(i.uv, pos) * strength);
                    }
                }
                avgCol /= avgCount;
                col = avgCol;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
