Shader "Custom/waterTest" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _SpeedX("SpeedX", range(0.0001, 10))=3
        _SpeedY("SpeedY", range(0.0001, 10))=3
        _RippleSizeX("Ripple Size X", range(0.0001, 20))=8
        _RippleSizeY("Ripple Size Y", range(0.0001, 20))=8
        _DistortionScaleX("Distortion X", range(0.0001, 0.05))=0.01
        _DistortionScaleY("Distortion Y", range(0.0001, 0.05))=0.01
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
     
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
 
        sampler2D _MainTex;
        float4 _MainTex_ST;



        float _SpeedX;
        float _SpeedY;
        float _RippleSizeX;
        float _RippleSizeY;
        float _DistortionScaleX;
        float _DistortionScaleY;
 
        struct Input {
            float2 st_MainTex;
        };
 
        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);
 
            o.st_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
 
            // add distortion
            // this is the part you need to modify, i  recomment to expose such
            // hard-coded values to the inspector for easier tweaking.
            o.st_MainTex.x += sin((o.st_MainTex.x+o.st_MainTex.y)*_RippleSizeX + _Time.g*_SpeedX)*_DistortionScaleX;
            o.st_MainTex.y += cos((o.st_MainTex.x-o.st_MainTex.y)*_RippleSizeY + _Time.g*_SpeedY)*_DistortionScaleY;
        }
 
        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.st_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}