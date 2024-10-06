// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hand Painted 2D/Flame"
{
	Properties
	{
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		_FlameMap("Flame Map", 2D) = "white" {}
		[Toggle(_FLAMEBOTTOM_ON)] _FlameBottom("Flame Bottom", Float) = 0
		[KeywordEnum(Orange,Blue)] _FlameColor("Flame Color", Float) = 0

		[HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }

		Cull Off
		HLSLINCLUDE
		#pragma target 2.0
		
		#pragma prefer_hlslcc gles
		

		ENDHLSL

		
		Pass
		{
			Name "Sprite Unlit"
            Tags { "LightMode"="Universal2D" }

			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			
			#define ASE_SRP_VERSION 120111

			
			#pragma vertex vert
			#pragma fragment frag

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS SHADERPASS_SPRITEUNLIT

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"

			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"
			#pragma shader_feature_local _FLAMECOLOR_ORANGE _FLAMECOLOR_BLUE
			#pragma shader_feature_local _FLAMEBOTTOM_ON


			sampler2D _FlameMap;


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float3 positionWS : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D( _AlphaTex ); SAMPLER( sampler_AlphaTex );
				float _EnableAlphaTexture;
			#endif

			float4 _RendererColor;

			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( v.vertex.xyz );

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;
				o.positionWS = vertexInput.positionWS;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				Gradient gradient30_g24 = NewGradient( 0, 4, 2, float4( 0.9882353, 0.5215687, 0.4235294, 0.1191424 ), float4( 0.9921569, 0.6941177, 0.4627451, 0.2141909 ), float4( 0.9921569, 0.7764706, 0.4941176, 0.8326696 ), float4( 0.9960784, 0.8705882, 0.5019608, 0.9531395 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_14_0_g24 = ( 1.0 - IN.texCoord0.xy.y );
				Gradient gradient10_g24 = NewGradient( 0, 3, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 0.5000076 ), float4( 0, 0, 0, 1 ), 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 break4_g24 = ( IN.texCoord0.xy * float2( 0.1,0.1 ) );
				float mulTime6_g24 = _TimeParameters.x * -0.5;
				float4 transform3_g24 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float temp_output_5_0_g24 = ( transform3_g24.z * 10.0 );
				float2 appendResult8_g24 = (float2(break4_g24.x , ( break4_g24.y + mulTime6_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				float simplePerlin2D11_g24 = snoise( appendResult8_g24*4.0 );
				simplePerlin2D11_g24 = simplePerlin2D11_g24*0.5 + 0.5;
				float temp_output_13_0_g24 = ( simplePerlin2D11_g24 * 2.0 );
				float temp_output_25_0_g24 = (0.0 + (( ( temp_output_14_0_g24 * SampleGradient( gradient10_g24, IN.texCoord0.xy.x ).r * temp_output_14_0_g24 * temp_output_13_0_g24 ) * temp_output_13_0_g24 ) - 0.0) * (1.0 - 0.0) / (2.0 - 0.0));
				Gradient gradient26_g24 = NewGradient( 0, 4, 2, float4( 0.4156862, 0.9400152, 0.945098, 0.07095445 ), float4( 0.4156863, 0.7019608, 0.945098, 0.2141909 ), float4( 0.4588236, 0.7874286, 0.9764706, 0.9785764 ), float4( 0.7179847, 0.6666667, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				#if defined(_FLAMECOLOR_ORANGE)
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#elif defined(_FLAMECOLOR_BLUE)
				float4 staticSwitch34_g24 = SampleGradient( gradient26_g24, temp_output_25_0_g24 );
				#else
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#endif
				float4 break37_g24 = staticSwitch34_g24;
				float2 break15_g24 = ( IN.texCoord0.xy * float2( 0.2,0.2 ) );
				float mulTime16_g24 = _TimeParameters.x * -1.0;
				float2 appendResult22_g24 = (float2(break15_g24.x , ( break15_g24.y + mulTime16_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				#ifdef _FLAMEBOTTOM_ON
				float staticSwitch28_g24 = IN.texCoord0.xy.y;
				#else
				float staticSwitch28_g24 = 1.0;
				#endif
				Gradient gradient23_g24 = NewGradient( 0, 6, 2, float4( 0, 0, 0, 0 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.3000076 ), float4( 1, 1, 1, 0.4 ), float4( 1, 1, 1, 0.6 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.7000076 ), float4( 0, 0, 0, 1 ), 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_35_0_g24 = saturate( ( tex2D( _FlameMap, appendResult22_g24 ).r * tex2D( _FlameMap, appendResult8_g24 ).r * staticSwitch28_g24 * temp_output_14_0_g24 * SampleGradient( gradient23_g24, IN.texCoord0.xy.x ).r * temp_output_13_0_g24 ) );
				float smoothstepResult36_g24 = smoothstep( 0.07 , 0.09 , temp_output_35_0_g24);
				float4 appendResult39_g24 = (float4(break37_g24.r , break37_g24.g , break37_g24.b , smoothstepResult36_g24));
				
				float4 Color = appendResult39_g24;

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D( _AlphaTex, sampler_AlphaTex, IN.texCoord0.xy );
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture );
				#endif

				#if defined(DEBUG_DISPLAY)
				SurfaceData2D surfaceData;
				InitializeSurfaceData(Color.rgb, Color.a, surfaceData);
				InputData2D inputData;
				InitializeInputData(IN.positionWS.xy, half2(IN.texCoord0.xy), inputData);
				half4 debugColor = 0;

				SETUP_DEBUG_DATA_2D(inputData, IN.positionWS);

				if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
				{
					return debugColor;
				}
				#endif
				
				Color *= IN.color * _RendererColor;
				return Color;
			}

			ENDHLSL
		}
		
		Pass
		{
			
			Name "Sprite Unlit Forward"
            Tags { "LightMode"="UniversalForward" }

			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			
			#define ASE_SRP_VERSION 120111

			
			#pragma vertex vert
			#pragma fragment frag

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS SHADERPASS_SPRITEFORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"

			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"
			#pragma shader_feature_local _FLAMECOLOR_ORANGE _FLAMECOLOR_BLUE
			#pragma shader_feature_local _FLAMEBOTTOM_ON


			sampler2D _FlameMap;


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float3 positionWS : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D( _AlphaTex ); SAMPLER( sampler_AlphaTex );
				float _EnableAlphaTexture;
			#endif

			float4 _RendererColor;

			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( v.vertex.xyz );

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;
				o.positionWS = vertexInput.positionWS;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				Gradient gradient30_g24 = NewGradient( 0, 4, 2, float4( 0.9882353, 0.5215687, 0.4235294, 0.1191424 ), float4( 0.9921569, 0.6941177, 0.4627451, 0.2141909 ), float4( 0.9921569, 0.7764706, 0.4941176, 0.8326696 ), float4( 0.9960784, 0.8705882, 0.5019608, 0.9531395 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_14_0_g24 = ( 1.0 - IN.texCoord0.xy.y );
				Gradient gradient10_g24 = NewGradient( 0, 3, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 0.5000076 ), float4( 0, 0, 0, 1 ), 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 break4_g24 = ( IN.texCoord0.xy * float2( 0.1,0.1 ) );
				float mulTime6_g24 = _TimeParameters.x * -0.5;
				float4 transform3_g24 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float temp_output_5_0_g24 = ( transform3_g24.z * 10.0 );
				float2 appendResult8_g24 = (float2(break4_g24.x , ( break4_g24.y + mulTime6_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				float simplePerlin2D11_g24 = snoise( appendResult8_g24*4.0 );
				simplePerlin2D11_g24 = simplePerlin2D11_g24*0.5 + 0.5;
				float temp_output_13_0_g24 = ( simplePerlin2D11_g24 * 2.0 );
				float temp_output_25_0_g24 = (0.0 + (( ( temp_output_14_0_g24 * SampleGradient( gradient10_g24, IN.texCoord0.xy.x ).r * temp_output_14_0_g24 * temp_output_13_0_g24 ) * temp_output_13_0_g24 ) - 0.0) * (1.0 - 0.0) / (2.0 - 0.0));
				Gradient gradient26_g24 = NewGradient( 0, 4, 2, float4( 0.4156862, 0.9400152, 0.945098, 0.07095445 ), float4( 0.4156863, 0.7019608, 0.945098, 0.2141909 ), float4( 0.4588236, 0.7874286, 0.9764706, 0.9785764 ), float4( 0.7179847, 0.6666667, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				#if defined(_FLAMECOLOR_ORANGE)
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#elif defined(_FLAMECOLOR_BLUE)
				float4 staticSwitch34_g24 = SampleGradient( gradient26_g24, temp_output_25_0_g24 );
				#else
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#endif
				float4 break37_g24 = staticSwitch34_g24;
				float2 break15_g24 = ( IN.texCoord0.xy * float2( 0.2,0.2 ) );
				float mulTime16_g24 = _TimeParameters.x * -1.0;
				float2 appendResult22_g24 = (float2(break15_g24.x , ( break15_g24.y + mulTime16_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				#ifdef _FLAMEBOTTOM_ON
				float staticSwitch28_g24 = IN.texCoord0.xy.y;
				#else
				float staticSwitch28_g24 = 1.0;
				#endif
				Gradient gradient23_g24 = NewGradient( 0, 6, 2, float4( 0, 0, 0, 0 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.3000076 ), float4( 1, 1, 1, 0.4 ), float4( 1, 1, 1, 0.6 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.7000076 ), float4( 0, 0, 0, 1 ), 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_35_0_g24 = saturate( ( tex2D( _FlameMap, appendResult22_g24 ).r * tex2D( _FlameMap, appendResult8_g24 ).r * staticSwitch28_g24 * temp_output_14_0_g24 * SampleGradient( gradient23_g24, IN.texCoord0.xy.x ).r * temp_output_13_0_g24 ) );
				float smoothstepResult36_g24 = smoothstep( 0.07 , 0.09 , temp_output_35_0_g24);
				float4 appendResult39_g24 = (float4(break37_g24.r , break37_g24.g , break37_g24.b , smoothstepResult36_g24));
				
				float4 Color = appendResult39_g24;

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D( _AlphaTex, sampler_AlphaTex, IN.texCoord0.xy );
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture );
				#endif

				
				#if defined(DEBUG_DISPLAY)
				SurfaceData2D surfaceData;
				InitializeSurfaceData(Color.rgb, Color.a, surfaceData);
				InputData2D inputData;
				InitializeInputData(IN.positionWS.xy, half2(IN.texCoord0.xy), inputData);
				half4 debugColor = 0;

				SETUP_DEBUG_DATA_2D(inputData, IN.positionWS);

				if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
				{
					return debugColor;
				}
				#endif

				Color *= IN.color * _RendererColor;
				return Color;
			}

			ENDHLSL
		}
		
        Pass
        {
			
            Name "SceneSelectionPass"
            Tags { "LightMode"="SceneSelectionPass" }
        
            Cull Off
        
            HLSLPROGRAM
        
            #define ASE_SRP_VERSION 120111

        
            #pragma target 2.0
			
			#pragma vertex vert
			#pragma fragment frag
        
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            #define SHADERPASS SHADERPASS_DEPTHONLY
			#define SCENESELECTIONPASS 1
        
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"
			#pragma shader_feature_local _FLAMECOLOR_ORANGE _FLAMECOLOR_BLUE
			#pragma shader_feature_local _FLAMEBOTTOM_ON


			sampler2D _FlameMap;


            struct VertexInput
			{
				float3 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
        
            
            int _ObjectId;
            int _PassValue;
            
			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert(VertexInput v )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
				float3 positionWS = TransformObjectToWorld(v.vertex);
				o.clipPos = TransformWorldToHClip(positionWS);
		
				return o;
			}

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				Gradient gradient30_g24 = NewGradient( 0, 4, 2, float4( 0.9882353, 0.5215687, 0.4235294, 0.1191424 ), float4( 0.9921569, 0.6941177, 0.4627451, 0.2141909 ), float4( 0.9921569, 0.7764706, 0.4941176, 0.8326696 ), float4( 0.9960784, 0.8705882, 0.5019608, 0.9531395 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_14_0_g24 = ( 1.0 - IN.ase_texcoord.xy.y );
				Gradient gradient10_g24 = NewGradient( 0, 3, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 0.5000076 ), float4( 0, 0, 0, 1 ), 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 break4_g24 = ( IN.ase_texcoord.xy * float2( 0.1,0.1 ) );
				float mulTime6_g24 = _TimeParameters.x * -0.5;
				float4 transform3_g24 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float temp_output_5_0_g24 = ( transform3_g24.z * 10.0 );
				float2 appendResult8_g24 = (float2(break4_g24.x , ( break4_g24.y + mulTime6_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				float simplePerlin2D11_g24 = snoise( appendResult8_g24*4.0 );
				simplePerlin2D11_g24 = simplePerlin2D11_g24*0.5 + 0.5;
				float temp_output_13_0_g24 = ( simplePerlin2D11_g24 * 2.0 );
				float temp_output_25_0_g24 = (0.0 + (( ( temp_output_14_0_g24 * SampleGradient( gradient10_g24, IN.ase_texcoord.xy.x ).r * temp_output_14_0_g24 * temp_output_13_0_g24 ) * temp_output_13_0_g24 ) - 0.0) * (1.0 - 0.0) / (2.0 - 0.0));
				Gradient gradient26_g24 = NewGradient( 0, 4, 2, float4( 0.4156862, 0.9400152, 0.945098, 0.07095445 ), float4( 0.4156863, 0.7019608, 0.945098, 0.2141909 ), float4( 0.4588236, 0.7874286, 0.9764706, 0.9785764 ), float4( 0.7179847, 0.6666667, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				#if defined(_FLAMECOLOR_ORANGE)
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#elif defined(_FLAMECOLOR_BLUE)
				float4 staticSwitch34_g24 = SampleGradient( gradient26_g24, temp_output_25_0_g24 );
				#else
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#endif
				float4 break37_g24 = staticSwitch34_g24;
				float2 break15_g24 = ( IN.ase_texcoord.xy * float2( 0.2,0.2 ) );
				float mulTime16_g24 = _TimeParameters.x * -1.0;
				float2 appendResult22_g24 = (float2(break15_g24.x , ( break15_g24.y + mulTime16_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				#ifdef _FLAMEBOTTOM_ON
				float staticSwitch28_g24 = IN.ase_texcoord.xy.y;
				#else
				float staticSwitch28_g24 = 1.0;
				#endif
				Gradient gradient23_g24 = NewGradient( 0, 6, 2, float4( 0, 0, 0, 0 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.3000076 ), float4( 1, 1, 1, 0.4 ), float4( 1, 1, 1, 0.6 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.7000076 ), float4( 0, 0, 0, 1 ), 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_35_0_g24 = saturate( ( tex2D( _FlameMap, appendResult22_g24 ).r * tex2D( _FlameMap, appendResult8_g24 ).r * staticSwitch28_g24 * temp_output_14_0_g24 * SampleGradient( gradient23_g24, IN.ase_texcoord.xy.x ).r * temp_output_13_0_g24 ) );
				float smoothstepResult36_g24 = smoothstep( 0.07 , 0.09 , temp_output_35_0_g24);
				float4 appendResult39_g24 = (float4(break37_g24.r , break37_g24.g , break37_g24.b , smoothstepResult36_g24));
				
				float4 Color = appendResult39_g24;

				half4 outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				return outColor;
			}

            ENDHLSL
        }

		
        Pass
        {
			
            Name "ScenePickingPass"
            Tags { "LightMode"="Picking" }
        
            Cull Back
        
            HLSLPROGRAM
        
            #define ASE_SRP_VERSION 120111

        
            #pragma target 2.0
			
			#pragma vertex vert
			#pragma fragment frag
        
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            #define SHADERPASS SHADERPASS_DEPTHONLY
			#define SCENEPICKINGPASS 1
        
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        	#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"
        	#pragma shader_feature_local _FLAMECOLOR_ORANGE _FLAMECOLOR_BLUE
        	#pragma shader_feature_local _FLAMEBOTTOM_ON


			sampler2D _FlameMap;


            struct VertexInput
			{
				float3 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
        
            float4 _SelectionID;
        
			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert(VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
				float3 positionWS = TransformObjectToWorld(v.vertex);
				o.clipPos = TransformWorldToHClip(positionWS);
		
				return o;
			}

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				Gradient gradient30_g24 = NewGradient( 0, 4, 2, float4( 0.9882353, 0.5215687, 0.4235294, 0.1191424 ), float4( 0.9921569, 0.6941177, 0.4627451, 0.2141909 ), float4( 0.9921569, 0.7764706, 0.4941176, 0.8326696 ), float4( 0.9960784, 0.8705882, 0.5019608, 0.9531395 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_14_0_g24 = ( 1.0 - IN.ase_texcoord.xy.y );
				Gradient gradient10_g24 = NewGradient( 0, 3, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 0.5000076 ), float4( 0, 0, 0, 1 ), 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 break4_g24 = ( IN.ase_texcoord.xy * float2( 0.1,0.1 ) );
				float mulTime6_g24 = _TimeParameters.x * -0.5;
				float4 transform3_g24 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float temp_output_5_0_g24 = ( transform3_g24.z * 10.0 );
				float2 appendResult8_g24 = (float2(break4_g24.x , ( break4_g24.y + mulTime6_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				float simplePerlin2D11_g24 = snoise( appendResult8_g24*4.0 );
				simplePerlin2D11_g24 = simplePerlin2D11_g24*0.5 + 0.5;
				float temp_output_13_0_g24 = ( simplePerlin2D11_g24 * 2.0 );
				float temp_output_25_0_g24 = (0.0 + (( ( temp_output_14_0_g24 * SampleGradient( gradient10_g24, IN.ase_texcoord.xy.x ).r * temp_output_14_0_g24 * temp_output_13_0_g24 ) * temp_output_13_0_g24 ) - 0.0) * (1.0 - 0.0) / (2.0 - 0.0));
				Gradient gradient26_g24 = NewGradient( 0, 4, 2, float4( 0.4156862, 0.9400152, 0.945098, 0.07095445 ), float4( 0.4156863, 0.7019608, 0.945098, 0.2141909 ), float4( 0.4588236, 0.7874286, 0.9764706, 0.9785764 ), float4( 0.7179847, 0.6666667, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				#if defined(_FLAMECOLOR_ORANGE)
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#elif defined(_FLAMECOLOR_BLUE)
				float4 staticSwitch34_g24 = SampleGradient( gradient26_g24, temp_output_25_0_g24 );
				#else
				float4 staticSwitch34_g24 = SampleGradient( gradient30_g24, temp_output_25_0_g24 );
				#endif
				float4 break37_g24 = staticSwitch34_g24;
				float2 break15_g24 = ( IN.ase_texcoord.xy * float2( 0.2,0.2 ) );
				float mulTime16_g24 = _TimeParameters.x * -1.0;
				float2 appendResult22_g24 = (float2(break15_g24.x , ( break15_g24.y + mulTime16_g24 + transform3_g24.x + transform3_g24.y + temp_output_5_0_g24 )));
				#ifdef _FLAMEBOTTOM_ON
				float staticSwitch28_g24 = IN.ase_texcoord.xy.y;
				#else
				float staticSwitch28_g24 = 1.0;
				#endif
				Gradient gradient23_g24 = NewGradient( 0, 6, 2, float4( 0, 0, 0, 0 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.3000076 ), float4( 1, 1, 1, 0.4 ), float4( 1, 1, 1, 0.6 ), float4( 0.5882353, 0.5882353, 0.5882353, 0.7000076 ), float4( 0, 0, 0, 1 ), 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float temp_output_35_0_g24 = saturate( ( tex2D( _FlameMap, appendResult22_g24 ).r * tex2D( _FlameMap, appendResult8_g24 ).r * staticSwitch28_g24 * temp_output_14_0_g24 * SampleGradient( gradient23_g24, IN.ase_texcoord.xy.x ).r * temp_output_13_0_g24 ) );
				float smoothstepResult36_g24 = smoothstep( 0.07 , 0.09 , temp_output_35_0_g24);
				float4 appendResult39_g24 = (float4(break37_g24.r , break37_g24.g , break37_g24.b , smoothstepResult36_g24));
				
				float4 Color = appendResult39_g24;
				half4 outColor = _SelectionID;
				return outColor;
			}

            ENDHLSL
        }
		
	}
	CustomEditor "ASEMaterialInspector"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.FunctionNode;262;2799.462,1203.075;Inherit;False;Flame;0;;24;9fc766ece6f51465e80cc609576ec4dd;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;274;3171.499,1206.933;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;cf964e524c8e69742b1d21fbe2ebcc4a;True;Sprite Unlit Forward;0;1;Sprite Unlit Forward;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;275;3171.499,1206.933;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;cf964e524c8e69742b1d21fbe2ebcc4a;True;SceneSelectionPass;0;2;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=SceneSelectionPass;True;0;True;12;all;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;276;3171.499,1206.933;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;cf964e524c8e69742b1d21fbe2ebcc4a;True;ScenePickingPass;0;3;ScenePickingPass;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Picking;True;0;True;12;all;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;273;3112.499,1202.933;Float;False;True;-1;2;ASEMaterialInspector;0;15;Hand Painted 2D/Flame;cf964e524c8e69742b1d21fbe2ebcc4a;True;Sprite Unlit;0;0;Sprite Unlit;4;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;Hidden/InternalErrorShader;0;0;Standard;3;Vertex Position;1;0;Debug Display;0;0;External Alpha;0;0;0;4;True;True;True;True;False;;False;0
WireConnection;273;1;262;0
ASEEND*/
//CHKSM=367DF9930041DEF8790C902607550994E684075A