echo $$$$$$$$$$$$$$$$$$$$ installing template {nuspec - package id / path} 
dotnet new -i  ..\WebApi.TemplatePack\bin\Debug\templates\*.nupkg

rmdir .\content /s /q
mkdir content
cd content

echo $$$$$$$$$$$$$$$$$$$$ create content from template {template.json - shortname}
dotnet new Testewebapi -n Teste

echo $$$$$$$$$$$$$$$$$$$$ unstalling template {nuspec - package id / template.json - identity }
dotnet new -u WebApi
pause