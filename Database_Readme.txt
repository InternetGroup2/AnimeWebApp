dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools


Former AppDbSetting

{
  "ConnectionStrings": {
    "AppDbContextConnection": "Data Source=AnimeWebApp.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

dotnet ef migrations add InitialCreate --context AppDbContext
dotnet tool install –global dotnet-ef
dotnet ef migrations add Initial
dotnet ef database update
