#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["StudentDataService/StudentDataService.csproj", "StudentDataService/"]
COPY ["StudentData.Business/StudentData.Business.csproj", "StudentData.Business/"]
COPY ["StudentData.Common/StudentData.Common.csproj", "StudentData.Common/"]
COPY ["DataaParserService.Common/DataaParserService.Common.csproj", "DataaParserService.Common/"]
RUN dotnet restore "StudentDataService/StudentDataService.csproj"
COPY . .
WORKDIR "/src/StudentDataService"
RUN dotnet build "StudentDataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentDataService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentDataService.dll"]
