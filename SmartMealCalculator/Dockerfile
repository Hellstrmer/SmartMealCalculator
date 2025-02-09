FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN ls -la

COPY ["SmartMealCalculator/SmartMealCalculator.csproj", "./"]
RUN dotnet restore "SmartMealCalculator.csproj" --verbosity detailed

COPY SmartMealCalculator/ .

RUN dotnet build "SmartMealCalculator.csproj" -c Release -o /app/build --verbosity detailed

FROM build AS publish
RUN dotnet publish "SmartMealCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartMealCalculator.dll"]