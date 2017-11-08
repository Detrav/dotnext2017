@ECHO off
echo.
echo.
ECHO oooooooooo.               o8o  oooo        .o8  
ECHO `888'   `Y8b              `"'  `888       "888  
ECHO  888     888 oooo  oooo  oooo   888   .oooo888  
ECHO  888oooo888' `888  `888  `888   888  d88' `888  
ECHO  888    `88b  888   888   888   888  888   888  
ECHO  888    .88P  888   888   888   888  888   888  
ECHO o888bood8P'   `V88V"V8P' o888o o888o `Y8bod88P" 
echo.
echo.                                                
ECHO Begin - all build!
  Pushd Core
  ECHO   Begin - Core build
  FOR /D %%d IN (*) DO (
    ECHO     Begin - %%d
      dotnet build %%d -c Release
    ECHO     End   - %%d
  )
  ECHO   End   - Core build
  popd

  Pushd Java
  ECHO   Begin - Java build!
  FOR /D %%d IN (*) DO (
    ECHO     Begin - %%d
    Pushd %%d
      call gradlew.bat build
    popd
    ECHO     End   - %%d
  )                      
  ECHO   End   - Java build!
  popd
ECHO End   - all build!
 