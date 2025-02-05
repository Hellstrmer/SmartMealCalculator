#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#COPY SmartMealCalculatorServer.csproj .
#RUN dotnet restore
#COPY . .
#RUN dotnet publish -c Release -o /app/publish
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /app
#COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "SmartMealCalculatorServer.dll"]

ARG BUILD_CONFIGURATION=Release  # Default to Release for production

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SmartMealCalculatorServer.csproj .
RUN dotnet restore

COPY . .

RUN dotnet publish "SmartMealCalculatorServer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set the ASPNETCORE_ENVIRONMENT based on the build configuration
ENV ASPNETCORE_ENVIRONMENT=${BUILD_CONFIGURATION}

ENTRYPOINT ["dotnet", "SmartMealCalculatorServer.dll"]