FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY manufacturingonaspdotnet/Api.csproj manufacturingonaspdotnet/
RUN dotnet restore manufacturingonaspdotnet/Api.csproj

COPY manufacturingonaspdotnet/ manufacturingonaspdotnet/

RUN dotnet publish manufacturingonaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]