function Release-RegexAssembly($certificate) {
  if(-not $certificate) {
    $certificate = '.\Source\Enkoni.Framework.pfx'
  }
  
  # Execute the RegexBuilder
  & '.\Source\Supporting projects\RegexBuilder\bin\Debug\RegexBuilder.exe'

  # Disassembly the output
  ildasm '.\Enkoni.Framework.Validation.RegularExpressions.dll' /out:.\Enkoni.Framework.Validation.RegularExpressions.il

  #Re-assembly and sign it with a strong name
  ilasm .\Enkoni.Framework.Validation.RegularExpressions.il /dll /key=.\Source\Enkoni.Framework.snk /output=.\Enkoni.Framework.Validation.RegularExpressions.dll
  Remove-Item .\Enkoni.Framework.Validation.RegularExpressions.il
  
  #$timeStamper = "http://timestamp.verisign.com/scripts/timestamp.dll"
  # Read the certificate (will prompt for a password)
  #$cert = Get-PfxCertificate $certificate
  
  # Sign the assembly
  #Set-AuthenticodeSignature -Filepath 'Enkoni.Framework.Validation.RegularExpressions.dll' -Certificate $cert -TimeStampServer $timeStamper
    
  # Move the signed assembly to the default libraries directory
  Move-Item -Path 'Enkoni.Framework.Validation.RegularExpressions.dll' -Destination ".\Source\Libraries\" -Force
}

Release-RegexAssembly