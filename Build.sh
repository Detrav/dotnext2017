#!/bin/bash

#@echo off
echo ""
echo ""
echo "oooooooooo.               o8o  oooo        .o8  "
echo "\`888'   \`Y8b              \`\"'  \`888       \"888  "
echo " 888     888 oooo  oooo  oooo   888   .oooo888  "
echo " 888oooo888' \`888  \`888  \`888   888  d88' \`888  "
echo " 888    \`88b  888   888   888   888  888   888  "
echo " 888    .88P  888   888   888   888  888   888  "
echo "o888bood8P'   \`V88V\"V8P' o888o o888o \`Y8bod88P\" "
echo ""
echo ""
echo "Begin - all build!"
  pushd Core
  echo "  Begin - Core build"
  for D in *; do
    if [ -d "${D}" ]; then
    echo "    Begin - ${D}"
      dotnet build ${D} -c Release
    echo "    End   - ${D}"
    fi
  done
  echo "  End   - Core build"
  popd

  pushd Java
  echo "  Begin - Java build!"
  for D in *; do
    if [ -d "${D}" ]; then
    echo "    Begin - ${D}"
    pushd ${D}
      ./gradlew build
    popd
    echo "    End   - ${D}"
    fi
  done
  echo "  End   - Java build!"
  popd
echo "End   - all build!"
 