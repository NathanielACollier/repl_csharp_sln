
$deployDir = [system.io.directory]::CreateDirectory("$($env:USERPROFILE)\programs\GitRepoFinder")
Write-Host "Deploying: " $deployDir.FullName

# clear existing folder
remove-item ([system.io.path]::Combine($deployDir.FullName, "*")) -Recurse -Force

$buildConfig = "Release"

$projectFile = Get-ChildItem -Path $PSScriptRoot | where { $_.Extension -eq ".csproj"} | select -First 1

Write-Host "Building: " $projectFile

& dotnet @("publish", $projectFile,  "-c", $buildConfig, "-o", "`"" + $deployDir.FullName + "`"", "-p:PublishSingleFile=true",
		"-p:RuntimeIdentifier=win-x64", "-p:SelfContained=false", "-p:ExcludeSymbolsFromSingleFile=true"
 )

