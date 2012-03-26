Shader "Custom/Color" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	}
	Category
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off
		SubShader
		{
			Material {
				Diffuse [_Color]
				Ambient [_Color]
			}
			Pass {
				Color [_Color]
			}
		}
	}
}
