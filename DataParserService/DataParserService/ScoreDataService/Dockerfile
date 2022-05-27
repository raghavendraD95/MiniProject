#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ScoreDataService/ScoreDataService.csproj", "ScoreDataService/"]
COPY ["ScoreData.Business/ScoreData.Business.csproj", "ScoreData.Business/"]
COPY ["DataaParserService.Common/DataaParserService.Common.csproj", "DataaParserService.Common/"]
RUN dotnet restore "ScoreDataService/ScoreDataService.csproj"
COPY . .
WORKDIR "/src/ScoreDataService"
RUN dotnet build "ScoreDataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScoreDataService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScoreDataService.dll"]
