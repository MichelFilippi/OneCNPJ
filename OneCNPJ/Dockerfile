# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["OneCNPJ.API/OneCNPJ.API.csproj", "OneCNPJ.API/"]
RUN dotnet restore "OneCNPJ.API/OneCNPJ.API.csproj"

COPY . .
WORKDIR "/src/OneCNPJ.API"
RUN dotnet publish "OneCNPJ.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OneCNPJ.API.dll"]
