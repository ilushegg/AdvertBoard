#FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
#WORKDIR /source
#COPY . .
#RUN dotnet restore
#RUN dotnet publish -c Release -o /app
#
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
#WORKDIR /app
#COPY --from=build /app ./
#
#EXPOSE 5000
#
#ENTRYPOINT ["dotnet", "AdvertBoard.Api.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "src/AdvertBoard/Hosts/AdvertBoard.Api/AdvertBoard.Api.csproj"
RUN dotnet restore "src/AdvertBoard/Hosts/AdvertBoard.Api/AdvertBoard.Api.csproj"
COPY . .
WORKDIR "/src/src/AdvertBoard/Hosts/AdvertBoard.Api"
RUN dotnet build "AdvertBoard.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdvertBoard.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdvertBoard.Api.dll"]