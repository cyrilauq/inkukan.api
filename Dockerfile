FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# 1. On copie les fichiers projets
COPY ./Inkukan.Api/*.csproj Inkukan.Api/
COPY ./Inkukan.Application/*.csproj Inkukan.Application/
COPY ./Inkukan.Domain/*.csproj Inkukan.Domain/
COPY ./Inkukan.Infrastructure/*.csproj Inkukan.Infrastructure/

# 2. On restore
RUN dotnet restore "Inkukan.Api/Inkukan.Api.csproj"

# 3. On copie tout le code source
COPY ./ .

# 4. On compile (SANS le -o /app/build pour laisser les DLL � leur place standard)
WORKDIR "/source/Inkukan.Api"
RUN dotnet build "./Inkukan.Api.csproj" -c Release --no-restore

# 5. On publie
FROM build AS publish
# Ici on retire --no-build pour laisser dotnet s'assurer que tout est l�, 
# mais comme c'est d�j� compil�, ce sera instantan�.
RUN dotnet publish "Inkukan.Api.csproj" -c Release -o /app/publish

FROM base AS finale
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

ENTRYPOINT ["dotnet", "Inkukan.Api.dll"]