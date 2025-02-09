# Base stage for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

## Copy and restore the project file
#COPY ["SmartMealCalculator.csproj", "SmartMealCalculator/"]
#RUN dotnet restore "SmartMealCalculator.csproj" --verbosity detailed
#
## Copy the rest of the source code
#COPY SmartMealCalculator/ ./SmartMealCalculator/

COPY ["SmartMealCalculator/SmartMealCalculator.csproj", "./"]
RUN dotnet restore "SmartMealCalculator.csproj" --verbosity detailed

COPY SmartMealCalculator/ ./SmartMealCalculator/

# Build the project
RUN dotnet build "SmartMealCalculator.csproj" -c Release -o /app/build --verbosity detailed

# Publish the project
FROM build AS publish
RUN dotnet publish "SmartMealCalculator.csproj" -c Release -o /app/publish
RUN ls -la /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -la
ENTRYPOINT ["dotnet", "SmartMealCalculator.dll"]