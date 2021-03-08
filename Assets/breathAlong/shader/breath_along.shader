// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32785,y:32730,varname:node_3138,prsc:2|emission-9478-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31091,y:32617,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9896,x:32184,y:32563,ptovrint:False,ptlb:diffuse_tx,ptin:_diffuse_tx,varname:node_9896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9478,x:32506,y:32730,varname:node_9478,prsc:2|A-9896-RGB,B-7241-RGB,C-9896-A,D-3567-OUT;n:type:ShaderForge.SFN_Time,id:8344,x:30846,y:32754,varname:node_8344,prsc:2;n:type:ShaderForge.SFN_Add,id:6424,x:31308,y:32746,varname:node_6424,prsc:2|A-7241-A,B-2058-OUT;n:type:ShaderForge.SFN_Abs,id:7943,x:31720,y:32748,varname:node_7943,prsc:2|IN-8101-OUT;n:type:ShaderForge.SFN_Sin,id:8101,x:31490,y:32746,varname:node_8101,prsc:2|IN-6424-OUT;n:type:ShaderForge.SFN_Slider,id:9763,x:30717,y:32926,ptovrint:False,ptlb:speede,ptin:_speede,varname:node_9763,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Multiply,id:2058,x:31105,y:32799,varname:node_2058,prsc:2|A-8344-T,B-9763-OUT;n:type:ShaderForge.SFN_Add,id:3567,x:31985,y:32794,varname:node_3567,prsc:2|A-7943-OUT,B-4292-OUT;n:type:ShaderForge.SFN_Slider,id:4292,x:31657,y:33011,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:node_4292,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:7241-9896-9763-4292;pass:END;sub:END;*/

Shader "Shader Forge/breath_along" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _diffuse_tx ("diffuse_tx", 2D) = "white" {}
        _speede ("speede", Range(0, 5)) = 0
        _opacity ("opacity", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _diffuse_tx; uniform float4 _diffuse_tx_ST;
            uniform float _speede;
            uniform float _opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _diffuse_tx_var = tex2D(_diffuse_tx,TRANSFORM_TEX(i.uv0, _diffuse_tx));
                float4 node_8344 = _Time + _TimeEditor;
                float3 emissive = (_diffuse_tx_var.rgb*_Color.rgb*_diffuse_tx_var.a*(abs(sin((_Color.a+(node_8344.g*_speede))))+_opacity));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
