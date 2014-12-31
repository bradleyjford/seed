@echo off

..\..\..\makecert.exe -r -pe -n "CN=seed" -b 01/01/2000 -e 01/01/2036 -eku 1.3.6.1.5.5.7.3.1 -ss my -sr localMachine -sky exchange -sp "Microsoft RSA SChannel Cryptographic Provider" -sy 12 dev.cer
"..\..\..\tools\certmgr\certmgr.exe /c /add dev.cer /s root

"C:\Program Files (x86)\IIS Express\iisexpressadmincmd.exe" setupFriendlyHostnameUrl -url:https://seed:443/
"C:\Program Files (x86)\IIS Express\iisexpressadmincmd.exe" setupSslUrl -url:https://seed:443/ -CertHash:f54059d5c958eaf3fba4624069e900c9594e8a0f
