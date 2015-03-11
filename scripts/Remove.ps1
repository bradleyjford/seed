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

Write-Host "Removing hosts file entry `"$WebSiteHostName`"... "
Remove-HostsEntry -HostName $WebSiteHostName
