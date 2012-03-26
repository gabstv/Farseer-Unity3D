Shader "Custom/FDView" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	}
	Category
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off
		//Blend SrcAlpha OneMinusSrcAlpha 
		SubShader
		{
			//BindChannels
			//{
			//
			//}
			Material {
				Diffuse [_Color]
				Ambient [_Color]
			}
			Pass {
				ColorMaterial AmbientAndDiffuse
				Lighting Off
				Cull Off
		        Blend SrcAlpha OneMinusSrcAlpha 
			}
		}
	}
}
