#!/bin/bash

#@echo off
echo ""
echo ""
echo "ooooooooooooo                        .            "
echo "8'   888   \`8                      .o8            "
echo "     888       .ooooo.   .oooo.o .o888oo  .oooo.o "
echo "     888      d88' \`88b d88(  \"8   888   d88(  \"8 "
echo "     888      888ooo888 \`\"Y88b.    888   \`\"Y88b.  "
echo "     888      888    .o o.  )88b   888 . o.  )88b "
echo "    o888o     \`Y8bod8P' 8\"\"888P'   \"888\" 8\"\"888P' "
echo ""
echo ""

echo "Begin - all tests!"
  pushd Core
  echo "  Begin - Core tests"
  for D in *; do
    if [ -d "${D}" ]; then
    echo "    Begin - ${D}"
       dotnet ${D}/bin/Release/netcoreapp2.0/${D}.dll > "${D}.results"
    echo "    End   - ${D}"
    fi
  done
  echo "  End   - Core tests"
  popd

  pushd Java
  echo "  Begin - Java tests!"
  for D in *; do
    if [ -d "${D}" ]; then
    echo "    Begin - ${D}"
    if [ ${D} = "Recursive" ]; then
      java -XX:CompileThreshold=15 -Xss100m -XX:+UseG1GC -Xms50m -Xmx4g -XX:+PrintGCDetails -XX:+PrintFlagsFinal -XX:+UseStringDeduplication -jar  ${D}/build/libs/${D}-1.0.jar > "${D}.1.results"
      else
      java -XX:CompileThreshold=15 -XX:+UseG1GC -Xms50m -Xmx4g -XX:+PrintGCDetails -XX:+PrintFlagsFinal -XX:+UseStringDeduplication -jar  ${D}/build/libs/${D}-1.0.jar > "${D}.1.results"
      fi
    echo "    End   - ${D}"
    fi
  done
  echo "  End   - Java tests!"
  
  echo "  Begin - Java log analyze!"
  dotnet build "../JavaLogAnalyzer" -c Release
  for D in *; do
    if [ -d "${D}" ]; then
    echo "    Begin - ${D}"
     dotnet ../JavaLogAnalyzer/bin/Release/netcoreapp2.0/JavaLogAnalyzer.dll "${D}.1.results" > "${D}.results";
    echo "    End   - ${D}"
    fi
  done
  echo "  End   - Java log analyze!"
  popd
echo "End   - all tests!"
