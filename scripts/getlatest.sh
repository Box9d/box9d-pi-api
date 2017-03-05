#!/bin/sh
[ -e /home/pi/dotnet ] || mkdir /home/pi/dotnet
[ -e /home/pi/dotnetboot/Debug.zip ] && rm /home/pi/dotnetboot/Debug.zip
wget https://ci.appveyor.com/api/projects/RickPowell/box9d-pi-api/artifacts/src/Box9.Leds.Pi.WebHost/bin/Debug.zip -P /home/pi/dotnetboot && rm -r /home/pi/dotnet && mkdir /home/pi/dotnet && unzip /home/pi/dotnetboot/Debug.zip -d /home/pi/dotnet