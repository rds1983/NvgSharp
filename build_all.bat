dotnet --version
dotnet build build\Monogame\NvgSharp.sln /p:Configuration=Release --no-incremental
dotnet build build\FNA\NvgSharp.sln /p:Configuration=Release --no-incremental
call copy_zip_package_files.bat
rename "ZipPackage" "NvgSharp.%APPVEYOR_BUILD_VERSION%"
7z a NvgSharp.%APPVEYOR_BUILD_VERSION%.zip NvgSharp.%APPVEYOR_BUILD_VERSION%
