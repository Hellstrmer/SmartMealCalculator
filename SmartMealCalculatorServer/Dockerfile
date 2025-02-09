ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiera först projektfilen från rätt plats
COPY SmartMealCalculatorServer/SmartMealCalculatorServer.csproj ./
RUN dotnet restore

# Kopiera resten av källkoden
COPY SmartMealCalculatorServer/ ./
RUN dotnet publish "SmartMealCalculatorServer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Release
ENTRYPOINT ["dotnet", "SmartMealCalculatorServer.dll"]