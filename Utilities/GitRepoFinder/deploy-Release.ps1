
# default it to windows
$runtimeConfig = "win-x64"

if ($IsLinux) {
    $runtimeConfig = "linux-x64"
}

$projectFileInfo = Get-ChildItem -Path $PSScriptRoot | where { $_.Extension -eq ".csproj"} | select -First 1

$deployDir = [system.io.directory]::CreateDirectory(  [System.IO.Path]::Combine([system.environment]::GetFolderPath("userprofile"), "programs", [system.io.path]::GetFileNameWithoutExtension($projectFileInfo.Name) ))

# clear existing folder
remove-item ([system.io.path]::Combine($deployDir.FullName, "*")) -Recurse -Force

$buildConfig = "Release"

& dotnet @("publish", $projectFileInfo.FullName,  "-r", $runtimeConfig ,"-c", $buildConfig, "-o", 
            $deployDir.FullName , "-p:PublishSingleFile=true",
		"-p:RuntimeIdentifier=win-x64", "-p:SelfContained=false", "-p:ExcludeSymbolsFromSingleFile=true"
 )

