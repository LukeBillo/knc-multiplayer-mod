Param(
    [Parameter(Mandatory=$true)]
    [string] $ModLocation
)

Write-Output "Copying mod to $($ModLocation)";

Remove-Item -Path $ModLocation/* -Recurse

Get-ChildItem ./KingdomsAndCastles.Mods.Multiplayer -Filter "*.cs" -Recurse | ForEach-Object {
    Copy-Item $_.FullName -Destination $ModLocation;
}

$infoJsonExists = Test-Path -Path ./info.json

if (!$infoJsonExists) 
{
    Write-Warning "No info.json found- maybe you should make one?";
} 
else 
{
    $infoJson = Get-Item -Path ./info.json;
    Copy-Item $infoJson.FullName -Destination $ModLocation;
}

Write-Output "Done!"