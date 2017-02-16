FROM microsoft/dotnet:1.1.0-runtime
WORKDIR /app
COPY Box9.Leds.Pi.Api/bin/Debug/netcoreapp1.1/publish /app
ENV ASPNETCORE_URLS http://*:54700
EXPOSE 54700

ENTRYPOINT /bin/bash -c "dotnet Box9.Leds.Pi.Api.dll"