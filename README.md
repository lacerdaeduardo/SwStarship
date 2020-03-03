# Sw Starships Resupply Calculator 

## **About this project** 
This project was developed using dotnet core 3.1 and C# 

The solution contains 3 projects:

**SwStarship.App** - Console App, that runs the application itself

**SwStarship.Core** - Contains Models, Services and Business Logic 

**SwStarship.Tests** - Contains all unit tests to make sure business logic are working.

## **How to build and generate an executable of this project**

**Note:** **it's required** to be installed **.NET Core 3.1** (SDK and Runtime) to successfully build, run and test this app.

To compile and run, just execute use the prompt and navigate to the root folder of this repository. 

Then run the following commands:

```
# build the solution
dotnet build

# Create an executable for Console Project
dotnet publish -c release 

# Navigate to released executable folder
cd ./src/SwStarship.App/bin/Release/netcoreapp3.1

# Execute the Executable to start the program
dotnet dotnet SwStarship.App.dll
```

Enjoy!

## **Unit tests**
To run unit tests, use this command on cmd at root folder of this project `dotnet test`