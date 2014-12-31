Import-Module Carbon

$IsAdmin = Test-AdminPrivilege

if (!$IsAdmin)
{
    Write-Host "This script must be run with administrator permissions."
    return
}

. .\_Config.ps1

Write-Host "Adding hosts file entry `"$WebSiteHostName`" -> `"$WebSiteIPAddress`"... "
Set-HostsEntry -IPAddress $WebSiteIPAddress -HostName $WebSiteHostName

$SSLCACertificate = Get-Certificate -Path $SSLCACertificatePath
$SSLCertificate = Get-Certificate -Path $SSLPFXPath -Password $SSLPrivateKeyPassword

Write-Host "Trusting CA Certificate `"$($SSLCACertificate.Subject)`"... "
Install-Certificate -Path $SSLCACertificatePath -StoreLocation LocalMachine -StoreName AuthRoot

Write-Host "Installing SSL certificate `"$($SSLCertificate.Subject)`"... "
Install-Certificate -Path $SSLPFXPath -StoreLocation LocalMachine -StoreName My -Password $SSLPrivateKeyPassword -Exportable

$NetShURL = "https://${WebSiteHostName}:443/"

Write-Host "Adding URL reservation `"$NetShURL`"... "

& netsh http delete urlacl url=$NetShURL
& netsh http add urlacl url=$NetShURL user=everyone

Write-Host "Adding SSL certificate binding... "
Set-SslCertificateBinding -IPAddress $WebSiteIPAddress -Port 443 -ApplicationID $ApplicationID -Thumbprint $SSLCertificate.Thumbprint
