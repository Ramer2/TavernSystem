You need to add appsetting.json file, which will look like this:

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "TavernDatabase": "<your connection string>"
  }
}
