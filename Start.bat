@ECHO off
ECHO.
ECHO.
ECHO oooooooooo.                 .                                                                                           
ECHO `888'   `Y8b              .o8                                                                                           
ECHO  888      888  .ooooo.  .o888oo oooo d8b  .oooo.   oooo    ooo                                                          
ECHO  888      888 d88' `88b   888   `888""8P `P  )88b   `88.  .8'                                                           
ECHO  888      888 888ooo888   888    888      .oP"888    `88..8'                                                            
ECHO  888     d88' 888    .o   888 .  888     d8(  888     `888'                                                             
ECHO o888bood8P'   `Y8bod8P'   "888" d888b    `Y888""8o     `8'                                                              
ECHO.                                                                                                                         
ECHO.                                                                                                                         
ECHO.                                                                                                                         
ECHO   .oooooo.                                     oooooo     oooo  .oooooo..o         oooo                                 
ECHO  d8P'  `Y8b                                     `888.     .8'  d8P'    `Y8         `888                                 
ECHO 888           .ooooo.  oooo d8b  .ooooo.         `888.   .8'   Y88bo.               888  .oooo.   oooo    ooo  .oooo.   
ECHO 888          d88' `88b `888""8P d88' `88b         `888. .8'     `"Y8888o.           888 `P  )88b   `88.  .8'  `P  )88b  
ECHO 888          888   888  888     888ooo888          `888.8'          `"Y88b          888  .oP"888    `88..8'    .oP"888  
ECHO `88b    ooo  888   888  888     888    .o           `888'      oo     .d8P          888 d8(  888     `888'    d8(  888  
ECHO  `Y8bood8P'  `Y8bod8P' d888b    `Y8bod8P'            `8'       8""88888P'       .o. 88P `Y888""8o     `8'     `Y888""8o 
ECHO                                                                                 `Y888P                                  
ECHO.                                                                                      
ECHO.                                                                                      
ECHO Tests are now starting, take a cookie and relax
ECHO ===============================================
timeout 10  
ECHO.

set var=%~dp0

call Build.bat

cd %var%

call Test.bat

ECHO.
ECHO.
ECHO ooooooooo.   oooooooooooo       .o.       oooooooooo.   oooooo   oooo .o. 
ECHO `888   `Y88. `888'     `8      .888.      `888'   `Y8b   `888.   .8'  888 
ECHO  888   .d88'  888             .8"888.      888      888   `888. .8'   888 
ECHO  888ooo88P'   888oooo8       .8' `888.     888      888    `888.8'    Y8P 
ECHO  888`88b.     888    "      .88ooo8888.    888      888     `888'     `8' 
ECHO  888  `88b.   888       o  .8'     `888.   888     d88'      888      .o. 
ECHO o888o  o888o o888ooooood8 o88o     o8888o o888bood8P'       o888o     Y8P 
ECHO.
ECHO.
                                                                          
timeout 10  
              
