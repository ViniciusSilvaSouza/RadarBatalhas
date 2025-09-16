# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Environment to avoid host-specific NuGet paths and speed up first run
ENV DOTNET_NOLOGO=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    NUGET_PACKAGES=/root/.nuget/packages \
    NUGET_FALLBACK_PACKAGES=

# Configure NuGet (no Windows fallback folders)
COPY NuGet.config ./

# Copy project files first (better cache) and restore
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Application/Application.csproj src/Application/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/
COPY tests/Unit/Unit.csproj tests/Unit/
COPY tests/Integration/Integration.csproj tests/Integration/
COPY src/Api/Api.csproj src/Api/
RUN dotnet restore src/Api/Api.csproj --configfile NuGet.config

# Copy the rest of the source
COPY . .

# Build (for clearer diagnostics) and then publish
RUN dotnet build src/Api/Api.csproj -c Release --no-restore -v minimal
RUN dotnet publish src/Api/Api.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Api.dll"]

