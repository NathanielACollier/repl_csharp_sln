
$deployPath = "$($env:USERPROFILE)\programs\WindowsClipboardManager"

# clear existing folder
remove-item ([system.io.path]::Combine($deployPath, "*")) -Recurse -Force

$buildConfig = "Release"

& dotnet @("publish","-c", $buildConfig, "-o", "`"$deployPath`"", "-p:PublishSingleFile=true",
		"-p:RuntimeIdentifier=win-x64", "-p:SelfContained=false", "-p:ExcludeSymbolsFromSingleFile=true"
 )

