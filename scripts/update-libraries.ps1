mkdir -Force .\lib | Out-Null

"Looking for Hearthstone Deck Tracker install..."
$HDTPath = "$Env:LOCALAPPDATA\HearthstoneDeckTracker"
$HDTInstalled = Test-Path $HDTPath

function CopyLocal($name) {
    if($HDTInstalled)
    {
        $HDTFile = Get-ChildItem $HDTPath | 
            Where-Object { $_.PSIsContainer -and $_.Name.StartsWith("app-")} | 
            Sort-Object CreationTime -desc |
            Select-Object -f 1 |
            Get-ChildItem |
            Where-Object { $_.Name.Equals($name)}
        if($HDTFile.Exists)
        {
            Write-Host "Copying $($HDTFile.Name) v$($HDTFile.VersionInfo.FileVersion) "
            Copy-Item $HDTFile.FullName ".\lib\$($HDTFile.Name)" -Force
            return $true
        }
        Write-Host "$name not found locally"
    }
    return $false
}

 function FetchLib($name) {
    if(-Not (CopyLocal $name))
    {
	    "Fetching $name..."
	    $url = "https://libs.hearthsim.net/hdt/$name"
	    try 
        { 
            (New-Object Net.WebClient).DownloadFile($url, ".\lib\$name")
            
            $HDTFile = Get-ChildItem  ".\lib\" |
                Where-Object { $_.Name.Equals($name)}
            if($HDTFile.Exists)
            {
                Write-Host "Downloaded $($HDTFile.Name) v$($HDTFile.VersionInfo.FileVersion) "
            }
            else
            {
                Write-Host "$name download failed without error"
            }
        }
        catch { $error[0].Exception.ToString() }    
    }
}

CopyLocal "HearthstoneDeckTracker.exe" | Out-Null
FetchLib "HearthDb.dll"