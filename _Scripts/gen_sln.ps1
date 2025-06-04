<#
Generate SLN solution file by taking all csproj and adding them
#>

$solutionRootFolder = Resolve-Path -Path ($PSScriptRoot + "\..")

Write-Host "Solution Folder Path $solutionRootFolder"

$projects = Get-ChildItem -Path $solutionRootFolder -Recurse | Where-Object { $_.Name -match '^.+\.(csproj|vbproj)$' }

# $projects | select FullName | Write-Host

$solutionName = "repl_csharp_sln"
$slnxPath = [System.IO.Path]::Combine($solutionRootFolder, "$solutionName.slnx")
$slnPath = [System.IO.Path]::Combine($solutionRootFolder, "$solutionName.sln")

<#
!!! REMEMBER !!!
Need to cover all the folder paths too

#>

if( Test-Path $slnPath ){
    Write-Host "Removing $slnPath"
    Remove-Item -Path $slnPath
}

if( Test-Path $slnxPath){
    Write-Host "Removing $slnxPath"
    Remove-Item -Path $slnxPath
}


# now do the dotnet cli commands in the solution folder
cd $solutionRootFolder

# create the sln file
dotnet new solution -f sln

dotnet new solution -f slnx

foreach( $projFile in $projects){
    $projFolder = [System.IO.DirectoryInfo]  [System.IO.Path]::GetDirectoryName($projFile.FullName)
    $solutionFolderPath = $projFolder.FullName.Substring($solutionRootFolder.Path.Length + 1)

    # take the Project folder Name out, the solution doesn't include the project folder as a solution folder that would get weird
    $solutionFolderPath = $solutionFolderPath.Substring(0, $solutionFolderPath.Length - $projFolder.Name.Length)
    

    Write-Host "Project: $($projFile.Name) `nSolution folder: $solutionFolderPath `nSolution File: $slnPath"
    
    Write-Host "Adding to SLN"
    dotnet solution $slnPath add $projFile.FullName --solution-folder $solutionFolderPath

    Write-Host "Adding to SLNX"
    dotnet solution $slnxPath add $projFile.FullName --solution-folder $solutionFolderPath
}