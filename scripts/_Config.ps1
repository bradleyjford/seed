$ProjectRootPath = Resolve-Path (Join-Path $PSScriptRoot '..')
$ScriptsPath = Join-Path $ProjectRootPath 'scripts'
$ToolsPath = Join-Path $ProjectRootPath 'tools'

$WebRootPath = Join-Path $ProjectRootPath 'dist'

$WebSiteHostname = 'seed.local'
$WebSiteIPAddress = '127.0.0.2'

$WebSiteName = $WebSiteHostname
