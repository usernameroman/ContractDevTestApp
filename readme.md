# ContractDevTestApp

ContractDevTestApp | 2021 | .NET 5 API for ContractDevTestApp project.

## Deployed environments

- Test - [Link](https://contractdevtestapp.azurewebsites.net)

## Prerequisites

- A Windows 10 PC, Mac or Linux
- Visual Studio 2019 (Windows 10), or optional Visual Studio Code (Windows, Mac or Linux)
- .NET 5.0 SDK : [Download link](https://dotnet.microsoft.com/download)

## Setup

To start delepment process perform this steps first:

1. Update _appsettings.Development.json_

Configs are already set up, except of `ConnectionStrings:DefaultConnection`. You can change them directly in the file **(be careful not to commit those changes)** or set secrets for _ContractDevTestApp_ project (more preferable).

Set user secrets via console

```
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "PUT_YOUR_DATABASE_CONNECTION_STRING"

```

2. Apply migrations to your local machine

To apply migrations select _ContractDevTestApp_ project as Startup project and run command `dotnet ef database update -p ContractDevTestApp --configuration Development` from cmd in _root_ folder

## Publish 
To publish application to Azure you can use Visual Studio integrated publish functionality using `ContractDevTestApp\PublishProfiles\ContractDevTestApp.PublishSettings` file.