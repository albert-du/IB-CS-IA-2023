# KitchenInventory

This is an application to inventory items in your kitchen.

## Features
* Locations
* Categories
* Expiring soon
* Search by name or description

## Technologies Used
* [.NET 7.0](https://dot.net)
    * [C# 11](https://dotnet.microsoft.com/en-us/languages/csharp)
    * [ASP.NET Core 7.0](https://dotnet.microsoft.com/en-us/apps/aspnet)
    * [Blazor WebAssembly](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
    * [Entity Framework Core 7.0](https://learn.microsoft.com/en-us/ef/)
* [SQLite](https://www.sqlite.org/index.html)
* [Tailwind CSS](https://tailwindcss.com/)
* [heroicons](https://heroicons.com/)

## Running Instructions
* Install [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
* Install [Node.js](https://nodejs.org/en/) OR use the Tailwind CSS CDN
  * If using the CDN, uncomment the CDN link in `src\KitchenInventory\Client\wwwroot\index.html`
  * If not using the CDN, run `npm install`, then `tailwind-build.cmd/.sh`
* Run `dotnet run` in the `src\KitchenInventory\Server` directory
* Navigate to the port listed in a browser.