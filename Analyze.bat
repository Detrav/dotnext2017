@ECHO off
ECHO Begin - all analyze!
  Pushd Java
  ECHO   Begin - Java log analyze!
  dotnet build "..\JavaLogAnalyzer" -c Release
  FOR /D %%d IN (*) DO (
    ECHO     Begin - %%d
    dotnet ..\JavaLogAnalyzer\bin\Release\netcoreapp2.0\JavaLogAnalyzer.dll "%%d.1.results" > "%%d.results";
    ECHO     End   - %%d
  )                      
  ECHO   End   - Java log analyze!
  popd
ECHO End   - all analyze!
