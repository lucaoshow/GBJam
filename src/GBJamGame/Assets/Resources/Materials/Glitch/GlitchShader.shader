Shader "Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShakeRate ("Shake Rate", Range(0.0, 1.0)) = 0.2
        _ShakeSpeed ("Shake Speed", Float) = 5.0
        _ShakeBlockSize ("Shake Block Size", Float) = 30.5
        _ShakeColorRate ("Shake Color Rate", Range(0.0, 1.0)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _ShakeRate;
            float _ShakeSpeed;
            float _ShakeBlockSize;
            float _ShakeColorRate;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                //float4 pos : TEXCOORD2;
                // float3 normal : TEXCOORD1;
            };

            float random( float seed )
            {
	            return frac( 543.2543 * sin( dot( float2( seed, seed ), float2( 3525.46, -54.3415 ) ) ) );
            }

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                //float4 vertexPos = outline(v.vertex, _OutlineThick);
                o.vertex = UnityObjectToClipPos(v.vertex); // local space to clip space
                o.uv = v.uv0; // remaping uv coordinates so the center is (0, 0)
                //o.pos = mul(unity_ObjectToWorld, float4(vertexPos.xyz, 1)); // local to world space position
                // o.normal = UnityObjectToWorldNormal(v.normals); // to world space normals
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float enable_shift = float( random( trunc( _Time.y * _ShakeSpeed ) ) < _ShakeRate );

                float2 fixed_uv = i.uv;

	            fixed_uv.x += ( random( (trunc( i.uv.y * _ShakeBlockSize ) / _ShakeBlockSize) + _Time.y ) - 0.5 ) * _ShakeSpeed * enable_shift;

                float4 col = tex2D(_MainTex, fixed_uv);

                col.r = lerp( col.r, tex2D( _MainTex, fixed_uv + float2( -_ShakeColorRate, 0.0 ) ).r, enable_shift);
                col.g = lerp( col.g, tex2D( _MainTex, fixed_uv + float2( _ShakeColorRate/4, 0.0 ) ).g, enable_shift);
                col.b = lerp( col.b, tex2D( _MainTex, fixed_uv + float2( -_ShakeColorRate/4, 0.0 ) ).b, enable_shift);

                return col;
            }
            ENDCG
        }
    }
}
