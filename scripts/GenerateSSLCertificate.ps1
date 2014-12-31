Import-Module Carbon

$IsAdmin = Test-AdminPrivilege

if (!$IsAdmin)
{
    Write-Host "This must be run with administrator permissions"
    return
}

. .\_Config.ps1

$SSLCACertificatePath = Join-Path $ScriptsPath 'ca.cer'
$SSLCAPrivateKeyPath = Join-Path $ScriptsPath 'ca.pvk'
$SSLCertificatePath = Join-Path $env:Temp 'dev.cer'
$SSLPrivateKeyPath = Join-Path $env:Temp 'dev.pvk'

Write-Host "Please specify the password `"$SSLPrivateKeyPassword`" when prompted"

# Generate a CA certificate
& "$ToolsPath\makecert.exe" -r -pe -n "CN=Localhost Development CA" -cy authority -a sha256 -l 2048 -sky signature -# 2 -sv $SSLCAPrivateKeyPath $SSLCACertificatePath

# Generate the webserver certificate
& "$ToolsPath\makecert.exe" -ic $SSLCACertificatePath -iv $SSLCAPrivateKeyPath  -pe -n "CN=$WebSiteHostName" -a sha256 -b 01/01/2000 -e 01/01/2100 -eku 1.3.6.1.5.5.7.3.1 -sky exchange -sp "Microsoft RSA SChannel Cryptographic Provider" -sy 12 -# 1 -sv $SSLPrivateKeyPath $SSLCertificatePath


# Combine the certificate and private key into a PFX file
& "$ToolsPath\pvk2pfx.exe" -pvk $SSLPrivateKeyPath -pi $SSLPrivateKeyPassword -spc $SSLCertificatePath -pfx $SSLPFXPath -f

rm $SSLCertificatePath
rm $SSLPrivateKeyPath