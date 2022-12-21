
$projectFolderPath = [System.IO.Path]::GetDirectoryName( $MyInvocation.MyCommand.Path )
Write-Host "Project Folder: " $projectFolderPath

$projectFileInfo = Get-ChildItem -Path $projectFolderPath | where { $_.Extension -eq ".csproj"} | select -First 1

$deployDir = [system.io.directory]::CreateDirectory(  [System.IO.Path]::Combine([system.environment]::GetFolderPath("userprofile"), "programs", [system.io.path]::GetFileNameWithoutExtension($projectFileInfo.Name) ))
Write-Host "Deploying: " $deployDir.FullName

# clear existing folder
remove-item ([system.io.path]::Combine($deployDir.FullName, "*")) -Recurse -Force

$buildConfig = "Release"


Write-Host "Building: " $projectFileInfo.FullName

$projectFilePath = "`"" + $projectFileInfo.FullName + "`""
$deployDirPath = "`"" + $deployDir.FullName + "`""

& dotnet @("publish", $projectFilePath,  "-c", $buildConfig, "-o", $deployDirPath, "-p:PublishSingleFile=true",
		"-p:RuntimeIdentifier=win-x64", "-p:SelfContained=false", "-p:ExcludeSymbolsFromSingleFile=true"
 )

