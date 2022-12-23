<#
See the avalonia guide for packaging macos applications
https://docs.avaloniaui.net/docs/distribution-publishing/macos

To use the dotnet bundling that creates the macos package you need this:
<PackageReference Include="Dotnet.Bundle" Version="*" />
#>

& dotnet restore -r osx-x64
& dotnet msbuild -t:BundleApp -p:RuntimeIdentifier=osx-x64 -property:Configuration=Release -p:UseAppHost=true

#& ./deploy-Release.ps1 -runtimeConfig "osx-x64"