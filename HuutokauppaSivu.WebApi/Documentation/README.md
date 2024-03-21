# README

https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli

# Requirements
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

## TODO

````
https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/DbContextPooling/Program.cs



````


https://gist.github.com/joshbuchea/6f47e86d2510bce28f8e7f42ae84c716


https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-8.0&tabs=visual-studio

## Sisäänkirjautuminen
https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api

## Itemin POISTO/DELETE, MUOKKAUS/UPDATE ja LISÄYS/POST handlerit
https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/http-message-handlers



