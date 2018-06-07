Shader "Custom/advCelOut" 
{
//CustomLambert
	properties 
	{
		_Colour ("Colour Tint", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		[NoScaleOffSet]
		_NormalMap ("Normal Map", 2D ) = "bump" {}
		_NormalStrentgh("Normal Strentgh", Float) = 1
		_NumCels("number of cels", Range(2,15)) = 3

		_Outline ("Outline Width", Range(0.002,0.1)) = 0.005
		_OutlineColor("Outline Colour", Color) = (0,0,0,1)

	
	} 
	subshader 
	{
		Tags
		{
			"RenderTyoe" = "Opaque"
		}

		CGPROGRAM
		#pragma surface surf MyCel

		sampler2D _MainTex;
		sampler2D _NormalMap;
	
		fixed4 _Colour;
		float _NormalStrentgh;
		float _NumCels;
	

		struct Input 
		{
			float2 uv_MainTex : TEXCOORD0;	
		};

		half4 LightingMyCel(SurfaceOutput s,half3 lightDir,half atten)
		{
			half NDotL = dot(s.Normal, lightDir); //Light intensity
			NDotL = floor (NDotL * _NumCels -1) / (_NumCels -1);

			half4 c;
			c.rgb = s.Albedo * NDotL * atten * _LightColor0.rgb;
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _Colour;

			fixed4 norm = tex2D(_NormalMap, IN.uv_MainTex);
			fixed3 unpacked = UnpackNormal(norm);
			unpacked.xy *= _NormalStrentgh;
			o.Normal = normalize(unpacked);
		}
		ENDCG

		pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			float _Outline;
			float4 _OutlineColor;

			v2f vert (appdata v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex); //position clipSpace
				float3 norm = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal)); //calculate normal vertexes pos based on World pos instead of local
				float2 offset = TransformViewToProjection(norm.xy); //calculate offset between worldspace and clippingspace(View)

				o.pos.xy += offset * o.pos.z * _Outline; //add offset to position of vertexes and * by input value. pushing out xy
				o.color = _OutlineColor;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return i.color;
			}

			ENDCG
		}
	}	
}
