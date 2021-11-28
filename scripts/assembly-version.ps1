Param(
    [Parameter(Mandatory=$true)]
    [string]$project,
    [Parameter(Mandatory=$true)]
    [int]$build,
    [Parameter(Mandatory=$true)]
    [string]$overrideVersion
)
$versionPattern = '(?<major>\d+)\.(?<minor>\d+)(\.(?<patch>\d+))?(\.(?<build>\d+))?'

$solutionPath = $(Resolve-Path "$PSScriptRoot\..").Path
$assemblyInfoFile = "$solutionPath\$project\Properties\AssemblyInfo.cs"

$assemblyInfo = [IO.File]::ReadAllText($assemblyInfoFile)
$versionRegex = [System.Text.RegularExpressions.Regex]::new('Version\(\"' + $versionPattern + '\"\)')

$match = $versionRegex.Match($assemblyInfo)
if(!$match.Success) {
    throw "No version numbers formatted <major>.<minor>(.<patch>(.<build>)) found in $assemblyInfoFile"
}
if($overrideVersion)
{
    $overrideRegex = [System.Text.RegularExpressions.Regex]::new('v' + $versionPattern)
    $override = $overrideRegex.Match($overrideVersion)
    if($override.Groups['major'].Success) {$overrideMajor=$override.Groups['major'].Value}
    if($override.Groups['minor'].Success) {$overrideMinor=$override.Groups['minor'].Value}
    if($override.Groups['patch'].Success) {$overridePatch=$override.Groups['patch'].Value}
}
$major = if($overrideMajor) {$overrideMajor} else {$match.Groups['major'].Value}
$minor = if($overrideMinor) {$overrideMinor} else {$match.Groups['minor'].Value}
$patch = if($overridePatch) {$overridePatch} else {$match.Groups['patch'].Value}


$assemblyVersion = "$major.$minor.$patch.$build"
$assemblyInfo = $versionRegex.Replace($assemblyInfo, 'Version("' + $assemblyVersion + '")')
[IO.File]::WriteAllText($assemblyInfoFile, $assemblyInfo)

Write-Host "$project AssemblyVersion=$assemblyVersion"
Write-Output $assemblyVersion