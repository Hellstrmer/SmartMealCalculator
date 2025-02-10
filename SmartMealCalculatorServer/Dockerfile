#
#
#ARG BUILD_CONFIGURATION=Release
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build


## Base stage for runtime
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 80
#
## Build stage
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#
## Copy and restore the project file
#COPY ["SmartMealCalculatorServer/SmartMealCalculatorServer.csproj", "SmartMealCalculatorServer/"]
#RUN dotnet restore "SmartMealCalculatorServer/SmartMealCalculatorServer.csproj" --verbosity detailed
#
## Copy the rest of the source code
#COPY SmartMealCalculatorServer/ ./SmartMealCalculatorServer/
#
## Build the project
#RUN dotnet build "SmartMealCalculatorServer/SmartMealCalculatorServer.csproj" -c Release -o /app/build --verbosity detailed
#
## Publish the project
#FROM build AS publish
#RUN dotnet publish "SmartMealCalculatorServer/SmartMealCalculatorServer.csproj" -c Release -o /app/publish --verbosity detailed
#RUN ls -la /app/publish  # Kontrollera utdata
#
## Final stage
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#RUN ls -la  
#ENTRYPOINT ["dotnet", "SmartMealCalculatorServer.dll"]

##
#Bygg lokalt#
##
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY SmartMealCalculatorServer/*.csproj ./
RUN dotnet restore
COPY SmartMealCalculatorServer/ ./
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SmartMealCalculatorServer.dll"]