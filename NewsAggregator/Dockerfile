FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NewsAggregator.csproj", "./"]
RUN dotnet restore "./NewsAggregator.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "NewsAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAggregator.dll"]