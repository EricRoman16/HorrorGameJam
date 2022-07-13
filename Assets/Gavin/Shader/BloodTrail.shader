Shader "Unlit/BloodTrail"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BloodColor ("Blood Color", Color) = (0.7,1,0.7,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
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
            float4 _BloodColor;

            sampler2D _BloodTex;

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
                // Texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = 0;//lerp(_BloodColor, col.rgb, tex2D(_BloodTex, i.uv).a);
                col.r = tex2D(_BloodTex, i.uv).a - (sin(_Time.z*3)+1)/10;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
