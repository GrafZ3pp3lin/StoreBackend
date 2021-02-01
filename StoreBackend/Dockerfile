#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["StoreBackend/StoreBackend.csproj", "StoreBackend/"]
RUN dotnet restore "StoreBackend/StoreBackend.csproj"
COPY . .
WORKDIR "/src/StoreBackend"
RUN dotnet build "StoreBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoreBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreBackend.dll"]