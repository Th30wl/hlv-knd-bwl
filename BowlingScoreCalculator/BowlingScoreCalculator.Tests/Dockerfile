#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine

WORKDIR /src
COPY ["BowlingScoreCalculator.Tests/BowlingScoreCalculator.Tests.csproj", "BowlingScoreCalculator.Tests/"]
COPY ["BowlingScoreCalculator.Api/BowlingScoreCalculator.Api.csproj", "BowlingScoreCalculator.Api/"]
RUN dotnet restore "BowlingScoreCalculator.Tests/BowlingScoreCalculator.Tests.csproj"
COPY . .
WORKDIR "/src/BowlingScoreCalculator.Tests"


ENTRYPOINT ["dotnet", "test", "BowlingScoreCalculator.Tests.csproj", "--logger:trx", "--nologo"]