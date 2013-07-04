$LastExitCode = 0

# Load the functions
. .\Release-Functions.ps1

[System.DateTime]$startTime = [System.DateTime]::Now

# Determine the Project directory
[string]$projectDir = (Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)

Write-Host "Going to validate the release notes..." -ForegroundColor Cyan

# Validate the release notes
$messages = Validate-ReleaseNotes -xmlPath (-join ($projectDir, '\Source\ReleaseNotes.xml')) -xsdPath (-join ($projectDir, '\Source\ReleaseNotes.xsd')) | Select-Object Message
[bool]$proceed = $TRUE
# Check the results to see if there were any validation messages
foreach($message in $messages) {
  if(!(-not $message -or $message.Message -eq [System.String]::Empty))
  {
    Write-Host -ForegroundColor Red $message.Message
    $proceed = $FALSE
  }
}

# Proceed only if there were no messages
if(!$proceed) {
  return
}

[System.DateTime]$validationTime = [System.DateTime]::Now

Write-Host "Validation successfull. Going to read the release notes..." -ForegroundColor Cyan

$releaseInfo = Read-ReleaseNotes -path '.\Source\ReleaseNotes.xml'

[int]$exitCode = $LastExitCode
if($exitCode -ne 0) {
  return
}

[System.DateTime]$readNotesTime = [System.DateTime]::Now

Write-Host "The release notes were successfuly read. Going to push the packages..." -ForegroundColor Cyan

[string]$productVersion = $releaseInfo.Version
[string]$releaseDir = -join ('.\Releases\Release ', $productVersion)
if(!(Test-Path $releaseDir)) {
  Write-Host "The release directory could not be located" -ForegroundColor Red
  return 1
}

$menu = @"
To which repository do you want to push the NuGet packages?
1) Local repository
2) Public repository

"@

Write-Host $menu
$repositoryChoice = Read-Host
switch($repositoryChoice) {
  1 {
    $apiKey = 'd2addced-c1be-4220-8b87-568ae8e952c3'
    $server = 'http://localhost/nuget/api/v2/package'
  }
  2 {
    $apiKey = '37d6d849-3ba7-4481-a091-1bb1a1dacb4b'
    $server = 'https://nuget.org/api/v2/package'
  }
  default {
    Write-Host "No valid option was selected. Going to abort."
    return 1
  }
}

Push-NuGet -inputDirectory $releaseDir -apiKey $apiKey -server $server

[System.DateTime]$endTime = [System.DateTime]::Now

Write-Host "Push succeeded. Total elapsed time: "($endTime - $startTime) -ForegroundColor Green
Write-Host "  - Release notes validation:`t"($validationTime - $startTime) -ForegroundColor Green
Write-Host "  - Release notes reading:`t"($readNotesTime - $validationTime) -ForegroundColor Green
Write-Host "  - Push NuGet packages:`t"($endTime - $readNotesTime) -ForegroundColor Green
