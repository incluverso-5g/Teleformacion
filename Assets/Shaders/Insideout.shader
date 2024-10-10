// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Based on Unlit shader, but culls the front faces instead of the back

Shader "Insideout" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_SpherePercentajex ("Sphere Percentaje X", Float) = 0.75
	_SpherePercentajey ("Sphere Percentaje Y", Float) = 0.75
	_offsetx ("offset  X", Float) = 0.75
	_offsety ("offset  Y", Float) = 0.75
	_AspectRatio ("AspectRatio", Float) = 1.484737
}

SubShader {
	Tags { "RenderType"="Opaque" }
	Cull front    // ADDED BY BERNIE, TO FLIP THE SURFACES
	LOD 100
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			float _AspectRatio;
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float4 _MainTex_ST;
			float _SpherePercentajex,_SpherePercentajey,_offsetx,_offsety;
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				// ADDED BY BERNIE:
				v.texcoord.x = 1 - v.texcoord.x;
				o.texcoord =v.texcoord.xy;// TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				if(  (i.texcoord.x + _offsetx) < (_SpherePercentajex/2 ) || (i.texcoord.x + _offsetx) > (1.0 - _SpherePercentajex/2) ||  (i.texcoord.y + _offsety) < (_SpherePercentajey/2) ||  (i.texcoord.y + _offsety) > (1.0 - _SpherePercentajey/2))
					discard;
				fixed4 col = tex2D(_MainTex, i.texcoord);
				return col;
			}
		ENDCG
	}
}

}