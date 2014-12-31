Import-Module Carbon

$IsAdmin = Test-AdminPrivilege

if (!$IsAdmin)
{
    Write-Host "This script must be run with administrator permissions"
    return
}

. .\_Config.ps1

Write-Host "Removing URL reservation... "
$NetShURL = "https://${WebSiteHostName}:443/"

& netsh http delete urlacl url=$NetShURL

Write-Host "Removing SSL certificate binding... "
Remove-SslCertificateBinding -IPAddress $WebSiteIPAddress -Port 443


$SSLCACertificate = Get-Certificate -Path $SSLCACertificatePath

Write-Host "Uninstalling CA Certificate `"$($SSLCACertificate.Subject)`"... "

Uninstall-Certificate -Thumbprint $SSLCACertificate.Thumbprint -StoreLocation LocalMachine -StoreName AuthRoot

$SSLCertificate = Get-Certificate -Path $SSLPFXPath -Password $SSLPrivateKeyPassword

Write-Host "Uninstalling SSL certificate `"$($SSLCertificate.Subject)`"... "

Uninstall-Certificate -Thumbprint $SSLCertificate.Thumbprint -StoreLocation LocalMachine -StoreName My 

Write-Host "Removing hosts file entry `"$WebSiteHostName`"... "
Remove-HostsEntry -HostName $WebSiteHostName
