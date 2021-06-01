mkdir -Force $PSScriptRoot\lib | Out-Null

"Looking for Hearthstone Deck Tracker install..."
$HDTPath = "$Env:LOCALAPPDATA\HearthstoneDeckTracker"
if(Test-Path $HDTPath)
{
    $HDTExe = Get-ChildItem "$Env:LOCALAPPDATA\HearthstoneDeckTracker" | Where-Object { $_.PSIsContainer -and $_.Name.StartsWith("app-")} | Sort-Object CreationTime -desc | Select-Object -f 1 | Get-ChildItem | Where-Object { $_.Name.Equals("HearthstoneDeckTracker.exe")}
    if($HDTExe.Exists)
    {
        "Copying $($HDTExe.Name) v$($HDTExe.VersionInfo.FileVersion)... "
        Copy-Item $HDTExe.FullName "$PSScriptRoot\lib\$($HDTExe.Name)" -Force
    }
}

 function FetchLib($name) {
	"Fetching $name..."
	$url = "https://libs.hearthsim.net/hdt/$name.dll"
	try { (New-Object Net.WebClient).DownloadFile($url, "$PSScriptRoot\lib\$name.dll") }
    catch { $error[0].Exception.ToString() }
}

FetchLib "HearthDb"