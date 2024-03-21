# README
## Requirements
- MSSQL Express

## Configuration file (appsettings.Development.json)
````
{
  "ConnectionStrings": {
    ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
````

## How to generate the database
````
dotnet ef migrations add InitialCreate
dotnet ef database update
````
