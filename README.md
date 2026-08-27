# Project

## Configuration

Add these in the appsettings or set the usersecrets using it

    "ConnectionStrings": {
        "DefaultConnection": "connection-string"
    },
    "LicenseKeys": {
        "LuckyPennySoftware": "license"
    },
    "VercelBlobOptions": {
        "Token": "token",
        "BlobUrl": "url"
    },
    "TokenConfiguration": {
        "SecretKey": "kzy",
        "Issuer": "issuer",
        "ValidityInHours": 0
    },
    "SeedingConfig": {
        "AdminDefaultPassword": "Password123$"
    }

## Add migration
1. Open a terminal inside root folder of solution
2. Execute the following command
```sh
dotnet ef migrations add [migration-name] --startup-project ./Inkukan.Api --project ./Inkukan.Infrastructure
```