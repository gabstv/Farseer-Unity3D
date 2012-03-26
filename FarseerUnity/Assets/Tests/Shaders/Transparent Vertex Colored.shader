Shader "Transparent/Vertex Colored" {
Properties {
	_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	_Emission ("Emmisive Color", Color) = (0,0,0,0)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

Category {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
	SubShader {
		Material {
			Diffuse [_Color]
			Ambient [_Color]
			Emission [_Emission]	
		}
		Pass {
			ColorMaterial AmbientAndDiffuse
			Lighting Off
			Cull Off
        SetTexture [_MainTex] {
            Combine texture * primary, texture * primary
        }
        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine previous * constant DOUBLE, previous * constant
        }  
		}
	} 
}
}