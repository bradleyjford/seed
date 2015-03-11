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

$NetShURL = "http://${WebSiteHostName}:80/"

Write-Host "Adding URL reservation `"$NetShURL`"... "

& netsh http delete urlacl url=$NetShURL
& netsh http add urlacl url=$NetShURL user=everyone

