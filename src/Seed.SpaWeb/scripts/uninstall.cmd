@echo off

"C:\Program Files (x86)\IIS Express\iisexpressadmincmd.exe" deleteSslUrl -url:https://seed:443/
"C:\Program Files (x86)\IIS Express\iisexpressadmincmd.exe" deleteFriendlyHostnameUrl -url:https://seed:443/
