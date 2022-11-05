FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV PORT=8080

COPY . .
WORKDIR ./src/AdvertBoard/Hosts/AdvertBoard.Api
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build ./src/AdvertBoard/Hosts/AdvertBoard.Api/out ./
ENTRYPOINT ["dotnet", "AdvertBoard.Api.dll"]