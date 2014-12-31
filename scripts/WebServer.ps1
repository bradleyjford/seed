$ProjectRootPath = Resolve-Path (Join-Path $PSScriptRoot '..')

$env:PROJECT_ROOT = $ProjectRootPath

& "C:\Program Files (x86)\IIS Express\iisexpress.exe" /config:applicationHost.config /site:Seed.Api /systray:false /trace:i
