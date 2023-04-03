## 🌲🏞️ Parks Directory 🏞️🌲

#### By Sarah Andyshak

#### An API for finding national and state parks, monuments, and forests.

## Technologies Used

* C#
* .NET
* ASP.Net
* Entity Framework

## Description

An API for creating webpages about national and state parks, monuments, and forests. 

## How To Run This Project

### Install Tools

Install the tools that are introduced in [this series of lessons on LearnHowToProgram.com](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c).

If you have not already, install the `dotnet-ef` tool by running the following command in your terminal:

```
dotnet tool install --global dotnet-ef --version 6.0.0
```

### Set Up and Run Project

1. Clone this repo.
2. Open the terminal and navigate to this project's production directory called "ParksDirectory".
3. Within the production directory "ParksDirectory", create two new files: `appsettings.json` and `appsettings.Development.json`.
4. Within `appsettings.json`, put in the following code. Make sure to replacing the `uid` ("YOUR-USER-NAME-HERE")and `pwd` ("YOUR-PASSWORD-HERE") values in the MySQL database connection string with your own username and password for MySQL.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=planetary_dictionary;uid=YOUR-USER-NAME-HERE;pwd=YOUR-PASSWORD-HERE;"
  }
}
```

5. Within `appsettings.Development.json`, add the following code:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Trace",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

6. Create the database using the migrations in the Parks Directory API project. Open your shell (e.g., Terminal or GitBash) to the production directory "ParksDirectory", and run `dotnet ef database update`. You may need to run this command for each of the branches in this repo if multiple branches have been created. 
    - To optionally create a migration, run the command `dotnet ef migrations add MigrationName` where `MigrationName` is your custom name for the migration in UpperCamelCase. To learn more about migrations, visit the LHTP lesson [Code First Development and Migrations](https://www.learnhowtoprogram.com/c-and-net-part-time/many-to-many-relationships/code-first-development-and-migrations).
7. Within the production directory "ParksDirectory", run `dotnet watch run --launch-profile "ParksDirectory-Production"` in the command line to start the project in production mode with a watcher. 
8. To optionally further build out this project in development mode, start the project with `dotnet watch run` in the production directory "ParksDirectory".
9. Use your program of choice to make API calls. In your API calls, use the domain _http://localhost:5000_. Keep reading to learn about all of the available endpoints.

## Testing the API Endpoints

You are welcome to test this API via [Postman](https://www.postman.com/) or [curl](https://curl.se/).

### Available Endpoints

```
GET http://localhost:5000/api/parks/
GET http://localhost:5000/api/parks/{id}
GET http://localhost:5000/api/parks/page/{page}
POST http://localhost:5000/api/parks/
PUT http://localhost:5000/api/parks/{id}
DELETE http://localhost:5000/api/parks/{id}
```

Note: `{id}` is a variable and it should be replaced with the id number of the planet you want to GET, PUT, or DELETE. For GET .../page/{page}, {page} indicates the page number you wish to view. 

#### Optional Query String Parameters for GET Request

GET requests to `http://localhost:5000/api/parks/` can optionally include query strings to filter or search parks. For example:

| Parameter   | Type        |  Required    | Description |
| ----------- | ----------- | -----------  | ----------- |
| name        | String      | not required | Returns parks with a matching name value |
| classification  | Number      | not required | Returns parks with a matching classification value |



The following query will return all parks with the name "Crater Lake":

```
GET http://localhost:5000/api/parks?name=crater lake
```

The following query will return the second page, with 2 parks on it:

```
GET http://localhost:5000/api/parks/page/2
```

To find a park founded before 1950, use the following query string:

```
GET http://localhost:5000/api/parks?foundedBefore=1950
```

Similarly, use the following query string to find a park founded after 1950:

```
GET http://localhost:5000/api/parks?foundedAfter=1950
```

You can include multiple query strings by separating them with an `&`:

```
GET http://localhost:5000/api/planets?name=crater lake&location=oregon
```

#### Additional Requirements for POST Request

When making a POST request to `http://localhost:5000/api/parks/`, you need to include a **body**. Here's an example body in JSON:

```json
{
  "name": "Crater Lake",
  "classification": "national park",
  "location": "Oregon",
  "majorLandmarks": "Crater Lake, Wizard Island",
  "yearFounded": 1902
}
```

#### Additional Requirements for PUT Request

When making a PUT request to `http://localhost:5000/api/parks/{id}`, you need to include a **body** that includes the parks's `parkId` property. Here's an example body in JSON:

```json
{
  "parkId": 1,
  "name": "Crater Lake",
  "classification": "national park",
  "location": "Oregon",
  "majorLandmarks": "Crater Lake, Wizard Island",
  "yearFounded": 1902
}
```

And here's the PUT request we would send the previous body to:

```
http://localhost:5000/api/park/1
```

Notice that the value of `parkId` needs to match the id number in the URL. In this example, they are both 1.

## Note about further exploration for coursework
This API provides the ability to enable responsive pagination in MVC projects. Responsive pagination works fine when the API is tested with Postman. However, when working on a similar MVC during the week, the dev group I participated in had trouble enabling pagination in the website and ended up hard coding pagination, rather than creating responsive pagination.

## Known Bugs

* As of 31 March 2023, the search feature only works with complete names, locations, etc. This may be better to address in MVC search functions.

## License
Enjoy the API! If you have questions or suggestions for fixing the code, please contact me!

[MIT](https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt)

Copyright (c) 2023 Sarah Andyshak