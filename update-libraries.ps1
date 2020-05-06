md -Force $PSScriptRoot\lib | Out-Null

 function FetchLib($name) {
	"Fetching $name..."
	$url = "https://libs.hearthsim.net/hdt/$name.dll"
	try { (New-Object Net.WebClient).DownloadFile($url, "$PSScriptRoot\lib\$name.dll") }
    catch { $error[0].Exception.ToString() }
}

FetchLib "HearthDb"