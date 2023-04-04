Shader "Standard-Emissive"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Albedo ("Albedo (RGB), Alpha (A)", 2D) = "white" {}


        _Emission("Emission", 2D) = "black" {}
        _EmissionColor("Color", Color) = (1,1,1,1)
        _EmissionIntensity("Intensity", Float) = 1
        _EmissionGlow("Glow", Float) = 1
        _EmissionGlowDuration("GlowDuration", Float) = 5

        
        _ScrollingMask("Scrolling Mask", 2D) = "white" {}
        _ScrollX("Scroll Speed (X)", Float) = 0.2
        _ScrollY("Scroll Speed (Y)", Float) = 0.2

        //_Normal ("Normal (RGB)", 2D) = "bump" {}
        //_MaskMap ("Mask Map (Metalic, Occlusion, Detail Mask, Smoothness)", 2D) = "black" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #include "UnityPBSLighting.cginc"
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        

        struct Input
        {
            float2 uv_Albedo;
        };

        //half _Glossiness;
        //half _Metallic;
        float4 _Color;
        sampler2D _Albedo;
        sampler2D _Emission;
        float4 _EmissionColor;
        float _EmissionIntensity;
        float _EmissionGlow;
        float _EmissionGlowDuration;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 albedo = tex2D (_Albedo, IN.uv_Albedo);
            fixed4 emission = tex2D(_Emission, IN.uv_Albedo);
            o.Albedo = albedo.rgb * _Color;
            o.Alpha = albedo.a;
        
            o.Emission = emission.rgb * _EmissionColor * (_EmissionIntensity + abs(frac(_Time.y * (1/_EmissionGlowDuration)) - 0.5) * _EmissionGlow);

            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            
        }
        ENDCG
        Pass
        {
            Tags{"RenderType"="Fade"}
            LOD 200
            Blend DstColor Zero
            
            CGPROGRAM
            
            
            
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            sampler2D _ScrollingMask;
            float4 _ScrollingMask_ST;
            float _ScrollX;
            float _ScrollY;

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



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _ScrollingMask);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv.x += (_Time.y * _ScrollX);
                i.uv.y += (_Time.y * _ScrollY);
                
                
                // sample the texture
                fixed4 col = tex2D(_ScrollingMask, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
