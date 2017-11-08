@ECHO off
ECHO.
ECHO.
ECHO ooooooooooooo                        .            
ECHO 8'   888   `8                      .o8            
ECHO      888       .ooooo.   .oooo.o .o888oo  .oooo.o 
ECHO      888      d88' `88b d88(  "8   888   d88(  "8 
ECHO      888      888ooo888 `"Y88b.    888   `"Y88b.  
ECHO      888      888    .o o.  )88b   888 . o.  )88b 
ECHO     o888o     `Y8bod8P' 8""888P'   "888" 8""888P' 
ECHO.                                                   
ECHO.                                              

IF NOT [%~1]==[] ( CALL :SPECIFIEDTEST %* )
IF [%~1]==[] ( CALL :ALLTEST )

GOTO :EOF

:SPECIFIEDTEST

ECHO Begin - Specified tests!
  Pushd Core
  ECHO   Begin - Core tests
  FOR %%d IN (%*) DO (
    ECHO     Begin - %%d
      dotnet %%d\bin\Release\netcoreapp2.0\%%d.dll > "%%d.results"
    ECHO     End   - %%d
  )
  ECHO   End   - Core tests
  popd

  Pushd Java
  ECHO   Begin - Java tests!
  FOR %%d IN (%*) DO (
    ECHO     Begin - %%d
      if "%%d" == "Recursive" (
      java -XX:CompileThreshold=15 -Xss100m -XX:+UseG1GC -Xms50m -Xmx4g -XX:+PrintGCDetails -XX:+PrintFlagsFinal -XX:+UseStringDeduplication -jar  %%d\build\libs\%%d-1.0.jar > "%%d.1.results"
      )
      if NOT "%%d" == "Recursive" (
      java -XX:CompileThreshold=15 -XX:+UseG1GC -Xms50m -Xmx4g -XX:+PrintGCDetails -XX:+PrintFlagsFinal -XX:+UseStringDeduplication -jar  %%d\build\libs\%%d-1.0.jar > "%%d.1.results"
      )
    ECHO     End   - %%d
  )                      
  ECHO   End   - Java tests!
  
  ECHO   Begin - Java log analyze!
  dotnet build "..\JavaLogAnalyzer" -c Release
  FOR %%d IN (%*) DO (
    ECHO     Begin - %%d
    dotnet ..\JavaLogAnalyzer\bin\Release\netcoreapp2.0\JavaLogAnalyzer.dll "%%d.1.results" > "%%d.results";
    ECHO     End   - %%d
  )                      
  ECHO   End   - Java log analyze!
  popd
ECHO End   - Specified tests!

GOTO :EOF

:ALLTEST

del /q Core\*.results
del /q Java\*.results

ECHO Begin - all tests!
  Pushd Core
  ECHO   Begin - Core tests
  FOR /D %%d IN (*) DO (
    ECHO     Begin - %%d
      dotnet %%d\bin\Release\netcoreapp2.0\%%d.dll > "%%d.results"
    ECHO     End   - %%d
  )
  ECHO   End   - Core tests
  popd

  Pushd Java
  ECHO   Begin - Java tests!
  FOR /D %%d IN (*) DO (
    ECHO     Begin - %%d
    if "%%d" == "Recursive" (
      java -XX:CompileThreshold=15 -Xss100m -XX:+UseG1GC -Xms50m -Xmx4g -XX:+PrintGCDetails -XX:+PrintFlagsFinal -XX:+UseStringDeduplication -jar  %%d\build\libs\%%d-1.0.jar > "%%d.1.results"
      )
      if NOT "%%d" == "Recursive" (
      java -XX:CompileThreshold=15 -XX:+UseG1GC -Xms50m -Xmx4g -XX:+PrintGCDetails -XX:+PrintFlagsFinal -XX:+UseStringDeduplication -jar  %%d\build\libs\%%d-1.0.jar > "%%d.1.results"
      )
    ECHO     End   - %%d
  )                      
  ECHO   End   - Java tests!
  
  ECHO   Begin - Java log analyze!
  dotnet build "..\JavaLogAnalyzer" -c Release
  FOR /D %%d IN (*) DO (
    ECHO     Begin - %%d
    dotnet ..\JavaLogAnalyzer\bin\Release\netcoreapp2.0\JavaLogAnalyzer.dll "%%d.1.results" > "%%d.results";
    ECHO     End   - %%d
  )                      
  ECHO   End   - Java log analyze!
  popd
ECHO End   - all tests!

GOTO :EOF
