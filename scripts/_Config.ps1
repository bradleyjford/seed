$ProjectRootPath = Resolve-Path (Join-Path $PSScriptRoot '..')
$ScriptsPath = Join-Path $ProjectRootPath 'scripts'
$ToolsPath = Join-Path $ProjectRootPath 'tools'

$ApplicationID = '{5EED5EED-516E-4312-91D2-826033054602}'

$SSLCACertificatePath = Join-Path $ScriptsPath 'ca.cer'

$SSLPFXPath = Join-Path $ScriptsPath 'dev.pfx'
$SSLPrivateKeyPassword = '123'

$WebRootPath = Join-Path $ProjectRootPath 'dist'

$WebSiteHostname = 'seed.local'
$WebSiteIPAddress = '127.0.0.2'

$WebSiteName = $WebSiteHostname
