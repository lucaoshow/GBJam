Shader "Glitch"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off

        Pass
        {
            Name "GlitchBlitPass"

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // The Blit.hlsl file provides the vertex shader (Vert),
            // input structure (Attributes) and output strucutre (Varyings)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            #pragma vertex Vert
            #pragma fragment frag

            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            float _ShakeRate;
            float _ShakeSpeed;
            float _ShakeBlockSize;
            float _ShakeColorRate;

            float random( float seed )
            {
	            return frac( 543.2543 * sin( dot( float2( seed, seed ), float2( 3525.46, -54.3415 ) ) ) );
            }

            float4 frag (Varyings i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                float enable_shift = float( random( trunc( _Time.y * _ShakeSpeed ) ) < _ShakeRate );

                //float2 fixed_uv = i.texcoord;

	            //fixed_uv.x += ( random( (trunc( i.texcoord.y * _ShakeBlockSize ) / _ShakeBlockSize ) + _Time.y ) - 0.5 ) * _ShakeSpeed * enable_shift;

                float4 col = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, i.texcoord);

                //col.r = lerp( col.r, SAMPLE_TEXTURE2D_X( _CameraOpaqueTexture, sampler_CameraOpaqueTexture, fixed_uv + float2( _ShakeColorRate, 0.0 ) ).r, enable_shift);
                
                //col.b = lerp( col.b, SAMPLE_TEXTURE2D_X( _CameraOpaqueTexture, sampler_CameraOpaqueTexture, fixed_uv + float2( -_ShakeColorRate, 0.0 ) ).b, enable_shift);

                return col;
            }
            ENDHLSL
        }
    }
}
