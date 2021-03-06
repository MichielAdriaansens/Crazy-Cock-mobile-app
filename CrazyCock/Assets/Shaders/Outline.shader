﻿Shader "Unlit/Outline"
{
	//Outliner
	Properties
	{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Texture", 2D) = "white" {}
		_OutLineColor ("Outline colour", Color) = (0,0,0,1)
		_OutLineWidth ("Outline Width", Range (1,5)) = 1.01

	}

	CGINCLUDE
	#include "UnityCG.cginc"
	struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : POSITION;
		float3 normal : NORMAL;
	};

	float _OutLineWidth;
	float4 _OutLineColor;

	v2f vert(appdata v)
	{ 
		//UNITY_INITIALIZE_OUTPUT(appdata,v);
	 	v.vertex.xyz *= _OutLineWidth;
	 	v2f o;
	 	o.pos = UnityObjectToClipPos (v.vertex);

	 	return o;
	}

	ENDCG

	SubShader
	{
		Tags
		{
		"Queue" = "Transparent"
		}


		Pass //render outline
		{
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

		
			half4 frag(v2f i) : COLOR
			{
				return _OutLineColor;
			}
			ENDCG
		}
		pass //NormalRender
		{
			ZWrite On
			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}
			Lighting On

			SetTexture [_MainTex]
			{
				ConstantColor[_Color]
			}

			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
	}
}
