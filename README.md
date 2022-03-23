# SmartAC

SmartAC is a project made for a fictional client with requirements to do some operations regarding some smart Air Conditioners that can communicate with the api

## Project requirements
- Visual Studio 2022
- SQL Server
- .NET 5 installed
## Installation
- Open command line in a folder where you intend to clone the repo
- Clone the repo using git command.

```
git clone {repo_url}
```
- Open the solution using Visual Studio
- Rebuild the whole solution
- If you have a specific environment configuration for SQL server, please go to appsettings.json in each web api project and modify the connection string to fit your environment
- Open package console manager, set the SmartAC.Context.Sql project as the default one
- Run the following command
```
update-database
```
- If you want both AdminAPI and DeviceAPI projects to run at the same time, right click on the solution, click on Set Startup Projects, select Multiple startup projects and configure both of them to be Start instead of None
- Click on start button to run the APIs


## Shortcuts
- GetDevicesByRegisterationDate method needs enhancements
- SafeReadingActionFilter action filter code needs enhancements
- Logger was supposed to be added
- Handling status codes to be either 200 or 401 is handled only in exceptions, not in bad request
- .NET core's default Dependency Injection was used for time saving purposes. In normal case, a more efficient DI library would have been used and instead of referencing the layers directly, they should be auto-loaded in the runtime to register the dependencies in the realtime without referencing the concrete implementation layer
- Only a happy path unit test scenario was written for short time

## Architecture

APIs represent the end points to be used by consumers/front-end
- SmartAC.DevicesAPI : An api for the devices communications mainly
- SmartAC.AdminAPI : An api for the admin dashboard

For the business logic implementation, the business layer provides services that validate and apply the business logic. It's supposed to be only referenced in front end/api layers only
- SmartAC.Services

For the data layer, a simple layer is added for the data communication based actions. It's supposed to be referenced by business logic layer only
- SmartAC.Data

For the Database context, a layer was added to maintain the migrations, the entity frameworks fluent api's rules and validations for records, entities and relationships. It's supposed to be referenced by the data layer only
- SmartAC.Context.Sql

The Common layer is mainly for commonly used functions that are business agnostic. It can be referenced at any layer (Except for the model layer)
- SmartAC.Common

The Model layer contains all data models, requests, responses, mappings, consts, enums, validators (Fluent Validation) and interfaces. It can be referenced by any layer (Except for the Common layer)
- SmartAC.Models

The testing project is in Tests folder where it should be referencing the APIs projects for testing how the apis should respond
- SmartAC.API.Test


## Highlights
- SafeReadingActionFilter is mainly for handling business logic related to alerts and sensor readings mentioned in the requirements
- Authorize filter is implemented to handle the JWT token validation
- Generic repository is implemented for database direct communication
- Fluent Validation is used for applying business logic validations as much as possible in each request
- NUnit and Moq are used for the unit testing project
- .NET Core's DI is used
- ExceptionHandlerMiddleware is a middleware implemented to handle exceptions and to log them (if logger is implemented/used)