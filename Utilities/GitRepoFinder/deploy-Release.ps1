
$myScriptFolder = [System.IO.Path]::GetDirectoryName( $MyInvocation.MyCommand.Path )
Write-Host "Script Folder: " $myScriptFolder

$projectFileInfo = Get-ChildItem -Path $myScriptFolder | where { $_.Extension -eq ".csproj"} | select -First 1

$deployDir = [system.io.directory]::CreateDirectory(  [System.IO.Path]::Combine([system.environment]::GetFolderPath("userprofile"), "programs", [system.io.path]::GetFileNameWithoutExtension($projectFileInfo.Name) ))
Write-Host "Deploying: " $deployDir.FullName

# clear existing folder
remove-item ([system.io.path]::Combine($deployDir.FullName, "*")) -Recurse -Force

$buildConfig = "Release"


Write-Host "Building: " $projectFileInfo.FullName

& dotnet @("publish", "`"" + $projectFileInfo.FullName + "`"",  "-c", $buildConfig, "-o", "`"" + $deployDir.FullName + "`"", "-p:PublishSingleFile=true",
		"-p:RuntimeIdentifier=win-x64", "-p:SelfContained=false", "-p:ExcludeSymbolsFromSingleFile=true"
 )

