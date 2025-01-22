#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#COPY ["SmartMealCalculator.csproj", "."]
#RUN dotnet restore "SmartMealCalculator.csproj"
#COPY . .
#RUN dotnet publish "SmartMealCalculator.csproj" -c Release -o /app/publish
#
#FROM nginx:alpine
#COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
#COPY nginx.conf /etc/nginx/nginx.conf
#EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY SmartMealCalculator.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80