#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["BowlingScoreCalculator.Api/BowlingScoreCalculator.Api.csproj", "BowlingScoreCalculator.Api/"]
RUN dotnet restore "BowlingScoreCalculator.Api/BowlingScoreCalculator.Api.csproj"
COPY . .
WORKDIR "/src/BowlingScoreCalculator.Api"
RUN dotnet build "BowlingScoreCalculator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BowlingScoreCalculator.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BowlingScoreCalculator.Api.dll"]