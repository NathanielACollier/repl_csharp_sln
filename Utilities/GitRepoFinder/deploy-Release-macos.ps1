<#
See the avalonia guide for packaging macos applications
https://docs.avaloniaui.net/docs/distribution-publishing/macos

The dotnet-bundle is here:
https://github.com/egramtel/dotnet-bundle

To use the dotnet bundling that creates the macos package you need this:
<PackageReference Include="Dotnet.Bundle" Version="*" />
#>

$projectFileInfo = Get-ChildItem -Path $PSScriptRoot | where { $_.Extension -eq ".csproj"} | select -First 1

$deployDir = [system.io.directory]::CreateDirectory(  [System.IO.Path]::Combine([system.environment]::GetFolderPath("userprofile"), "programs", [system.io.path]::GetFileNameWithoutExtension($projectFileInfo.Name) ))

# For some reason it needs to be the full path to the icon file for the bundle
$iconPath = [system.io.path]::Combine( $projectFileInfo.DirectoryName, "docs", "Icon.icns")

# clear existing folder
remove-item ([system.io.path]::Combine($deployDir.FullName, "*")) -Recurse -Force

& dotnet restore -r osx-arm64
& dotnet @( "msbuild" , "-t:BundleApp", "-p:RuntimeIdentifier=osx-arm64", "-property:Configuration=Release", "-p:UseAppHost=true",
           ("-p:PublishDir=" + $deployDir.FullName),
             "-p:CFBundleIconFile=$iconPath" )

# remove everything from publish folder except the app
get-ChildItem -Path ($deployDir.FullName) | where { -not($_.Extension  -eq ".app") } | remove-item -Force -Recurse

#& ./deploy-Release.ps1 -runtimeConfig "osx-x64"