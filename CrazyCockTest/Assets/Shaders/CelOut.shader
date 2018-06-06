Shader "Custom/CelOut" 
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

		_Outline ("Outline Width", Range(0.002,0.1)) = 0.02
	//	_Outline ("Outline Width", Range(1,2)) = 1
		_OutlineColor("Outline Colour", Color) = (0,0,0,1)

	
	} 
	subshader 
	{
		Tags
		{
			"RenderTyoe" = "Opaque"
			"Queue" = "Transparent"
		}

		ZWrite off
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		struct Input 
		{
			float2 uv_MainTex; 
		};
		float _Outline;
		float4 _OutlineColor;

		void vert (inout appdata_full v) 
		{
			v.vertex.xyz += v.normal * _Outline;
		//	v.vertex.xyz *= _Outline;
		}

		sampler2D _MainTex;
		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Emission = _OutlineColor.rgb;
		}

		ENDCG


		ZWrite on
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
	}	
}
