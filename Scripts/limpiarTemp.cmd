cd "C:\Program Files\CCleaner\"
c:
CCleaner /AUTO

echo eliminando archivos temporales

c:

rem v2.0.50727
cd "C:\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files"

cd
rmdir /S /Q .\soa
rmdir /S /Q .\soa20
rmdir /S /Q .\root
rmdir /S /Q .\towertoolswebsite
rmdir /S /Q .\webservicessite
rmdir /S /Q .\wmplusweb
rmdir /S /Q .\vs

rem v4.0
cd "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files"

cd
rmdir /S /Q .\soa
rmdir /S /Q .\soa20
rmdir /S /Q .\root
rmdir /S /Q .\towertoolswebsite
rmdir /S /Q .\webservicessite
rmdir /S /Q .\wmplusweb
rmdir /S /Q .\vs

cd "C:\Users\fminguela\AppData\Local\Temp\Temporary ASP.NET Files"

rmdir /S /Q .\soa
rmdir /S /Q .\soa20
rmdir /S /Q .\root
rmdir /S /Q .\towertoolswebsite
rmdir /S /Q .\webservicessite
rmdir /S /Q .\wmplusweb
rmdir /S /Q .\vs

PAUSE