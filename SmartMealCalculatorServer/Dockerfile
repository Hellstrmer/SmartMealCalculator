ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SmartMealCalculatorServer/SmartMealCalculatorServer.csproj ./
RUN dotnet restore

COPY SmartMealCalculatorServer/ ./
RUN dotnet publish "SmartMealCalculatorServer.csproj" -c Release -o /app/publish --verbosity detailed

RUN ls -la /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Release

RUN ls -la

ENTRYPOINT ["dotnet", "SmartMealCalculatorServer.dll"]