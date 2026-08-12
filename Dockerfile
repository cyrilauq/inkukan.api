FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# Central package management files - must be present before any restore
COPY Directory.Packages.props .
COPY Directory.Build.props .

# 1. On copie les fichiers projets (en gardant l'arborescence src/)
COPY ./src/Inkukan.Api/*.csproj ./src/Inkukan.Api/
COPY ./src/Inkukan.Application/*.csproj ./src/Inkukan.Application/
COPY ./src/Inkukan.Domain/*.csproj ./src/Inkukan.Domain/
COPY ./src/Inkukan.Infrastructure/*.csproj ./src/Inkukan.Infrastructure/

# 2. On restore
RUN dotnet restore "src/Inkukan.Api/Inkukan.Api.csproj"

# 3. On copie tout le code source
COPY ./ .

# 4. On compile
WORKDIR "/source/src/Inkukan.Api"
RUN dotnet build "./Inkukan.Api.csproj" -c Release --no-restore

# 5. On publie
FROM build AS publish
RUN dotnet publish "Inkukan.Api.csproj" -c Release -o /app/publish

FROM base AS finale
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

ENTRYPOINT ["dotnet", "Inkukan.Api.dll"]