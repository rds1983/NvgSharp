rem delete existing
rmdir "ZipPackage" /Q /S

rem Create required folders
mkdir "ZipPackage"
mkdir "ZipPackage\MonoGame"
mkdir "ZipPackage\FNA"

set "CONFIGURATION=Release\net45"

rem Copy output files
copy "src\NvgSharp\bin\MonoGame\%CONFIGURATION%\NvgSharp.dll" "ZipPackage\MonoGame" /Y
copy "src\NvgSharp\bin\MonoGame\%CONFIGURATION%\NvgSharp.pdb" "ZipPackage\MonoGame" /Y
copy "src\NvgSharp\bin\FNA\%CONFIGURATION%\NvgSharp.dll" "ZipPackage\FNA" /Y
copy "src\NvgSharp\bin\FNA\%CONFIGURATION%\NvgSharp.pdb" "ZipPackage\FNA" /Y