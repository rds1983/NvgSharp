2mgfx Effect.fx Effect.dx11.mgfxo /Profile:DirectX_11 /Defines:EDGE_AA
2mgfx Effect.fx Effect.ogl.mgfxo /Profile:OpenGL /Defines:EDGE_AA

"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\fxc.exe" /T fx_2_0 /Fo Effect.fxb Effect.fx /D EDGE_AA=1