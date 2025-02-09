FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SmartMealCalculator/SmartMealCalculator.csproj", "./"]
RUN dotnet restore "SmartMealCalculator.csproj"
COPY SmartMealCalculator/ .
RUN dotnet build "SmartMealCalculator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartMealCalculator.csproj" -c Release -o /app/publish
RUN ls -la /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -la
ENTRYPOINT ["dotnet", "SmartMealCalculator.dll"]