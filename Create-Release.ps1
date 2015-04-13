$LastExitCode = 0

Start-Transcript -Path release.log
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

Write-Host "Release notes successfully read. Going to prepare the working directory..." -ForegroundColor Cyan

# Clear or create the working directory
[string]$path = Join-Path $projectDir '.\WorkingDir\'
if(Test-Path $path) {
  Clear-Directory -path $path
}
else {
  New-Item -Type Directory -Path '.\WorkingDir'
}  

Write-Host "Going to update the version numbers..." -ForegroundColor Cyan

# Update the version numbers
Update-Versions -releaseInfo $releaseInfo

Write-Host "Version numbers successfully updated. Going to build the solution..." -ForegroundColor Cyan

# Build the solution
[string]$solution = Join-Path $projectDir '.\Source\Enkoni.Framework.sln'
Build-Solution -solutionFile $solution -outputPath $path

$exitCode = $LastExitCode
if($exitCode -ne 0) {
  return
}

[System.DateTime]$buildTime = [System.DateTime]::Now

Write-Host "Solution was successfully build. Going to execute the unit tests..." -ForegroundColor Cyan

# Execute the available test cases
Test-Solution

$exitCode = $LastExitCode
if($exitCode -ne 0) {
  return
}

[System.DateTime]$testTime = [System.DateTime]::Now

Write-Host "Unit tests were successfully executed. Going to transform the release notes..." -ForegroundColor Cyan

# Create the release notes
$xmlPath = Join-Path $projectDir '.\Source\ReleaseNotes.xml'
$xslPath = Join-Path $projectDir '.\Source\ReleaseNotes.xsl'
$outputPath = Join-Path $projectDir '.\WorkingDir\Release Notes.txt'

Write-ReleaseNotes -xml $xmlPath -xsl $xslPath -output $outputPath

[System.DateTime]$transformTime = [System.DateTime]::Now

Write-Host "Release notes were successfully transformed. Going to sign the assemblies..." -ForegroundColor Cyan

[System.DateTime]$sign1Time = [System.DateTime]::Now
[System.DateTime]$sign2Time = [System.DateTime]::Now

Sign-Assemblies -certificate '.\Source\Enkoni.Framework.pfx'
$exitCode = $LastExitCode
if($exitCode -ne 0) {
  return
}

[System.DateTime]$signTime = [System.DateTime]::Now

Write-Host "Assemblies were successfully signed. Going to create the NuGet packages..." -ForegroundColor Cyan

Pack-NuGet -releaseInfo $releaseInfo -outputDirectory (Join-Path $projectDir '\WorkingDir')
$exitCode = $LastExitCode
if($exitCode -ne 0) {
  return
}

[System.DateTime]$nugetTime = [System.DateTime]::Now

Write-Host "NuGet packages were successfully created. Going to generate the documentation..." -ForegroundColor Cyan

$aliasExists = Test-Path Alias:\msbuild
if(!$aliasExists) {
  Set-Alias msbuild "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe"
}

msbuild /p:Configuration=Release`;Platform=AnyCPU`;DumpLogOnFailure=True (Join-Path $projectDir '\Source\Enkoni.Documentation.shfbproj')
$exitCode = $LastExitCode
if($exitCode -ne 0) {
  Write-Host -ForegroundColor Red 'MSBuild failed. Review MSBuild-output and try again'
  return $exitCode
}

[System.DateTime]$docuTime = [System.DateTime]::Now
  
Write-Host "The documentation was successfuly generated. Going to save the release..." -ForegroundColor Cyan

[string]$productVersion = $releaseInfo.Version
[string]$releaseDir = -join ($projectDir, '\Releases\Release ', $productVersion)
if(!(Test-Path $releaseDir)) {
  New-Item -Type Directory -Path $releaseDir
}
else {
  Clear-Directory -path $releaseDir
}

Copy-Item -Path "WorkingDir\*" -Include "*.dll","*.xml","*.pdb","*.nupkg", "*.txt" -Exclude "*.CodeAnalysisLog.xml", "*.Tests.*", "Microsoft.Expression.Interactions.*" -Destination $releaseDir

[System.DateTime]$copyTime = [System.DateTime]::Now

Out-Zip -Filter .\WorkingDir\Documentation\* -Path $releaseDir\Documentation.zip

[System.DateTime]$zipTime = [System.DateTime]::Now

Remove-Item -Recurse -Force -Path 'WorkingDir'

[System.DateTime]$endTime = [System.DateTime]::Now

Write-Host "Release succeeded. Total elapsed time: "($endTime - $startTime) -ForegroundColor Green
Write-Host "  - Release notes validation:`t`t"($validationTime - $startTime) -ForegroundColor Green
Write-Host "  - Release notes reading:`t`t"($readNotesTime - $validationTime) -ForegroundColor Green
Write-Host "  - Build solution:`t`t`t"($buildTime - $readNotesTime) -ForegroundColor Green
Write-Host "  - Execute unit tests:`t`t`t"($testTime - $buildTime) -ForegroundColor Green
Write-Host "  - Release notes transformation:`t"($transformTime - $testTime) -ForegroundColor Green
Write-Host "  - Assembly signing (netto):`t`t"(($sign1Time - $transformTime) + ($signTime - $sign2Time)) -ForegroundColor Green
Write-Host "  - NuGet package creation:`t`t"($nugetTime - $signTime) -ForegroundColor Green
Write-Host "  - Build documentation:`t`t"($docuTime - $nugetTime) -ForegroundColor Green
Write-Host "  - Copying to release directory:`t"($copyTime - $docuTime) -ForegroundColor Green
Write-Host "  - Compressing documentation:`t`t"($zipTime - $copyTime) -ForegroundColor Green
#TODO: Push NuGet (seperate script)
#TODO: Find Releasenotes editor

Stop-Transcript