
$deployPath = "$($env:USERPROFILE)\programs\WindowsClipboardManager"
$buildConfig = "Release"

& dotnet @("publish","-c", $buildConfig, "-o", "`"$deployPath`"", "-p:PublishSingleFile=true",
		"-p:RuntimeIdentifier=win-x64", "-p:SelfContained=false", "-p:ExcludeSymbolsFromSingleFile=true"
 )

