# Drink Info App

Browse and read instructions and ingredient lists for making popular cocktails using data pulled from the Cocktail Database (https://www.thecocktaildb.com/).

## Description

The Cocktail Database (https://www.thecocktaildb.com/) is an open, crowd-sourced database of drinks and cocktails from around the world.
It provides a free API for developers to use for learning and practice.
This application pulls a list of drink categories from this database via web API for the user to select from.
Once the category is selected, a list of drinks within that category is pulled and displayed to the user (max 100 lines).
The user then selects a specific drink from that list to view the details on how to make it.

Additional Functionality:
- View an image of the drink
- Save drinks to a Favorites list
- Keep track of the number of times a drink is viewed

## Getting Started

### Technologies

- C# Console Project
- Asynchronous operation
- Spectre.Console
- Spectre.Console.ImageSharp for rendering an image in console window
- HttpClient for API connection
- SQLite for storing favorite drink Ids and view counters
- Dapper ORM for managing SQLite operations
- DI handled with Microsoft.Extensions.DependencyInejction
- Single-project Clean Architecture
- Results pattern used for passing values, errors, and exceptions between layers
- Repository pattern for API and SQLite CRUD operations

### Initial Setup

- Clone Repository
- In DrinkInfo > appsettings.json update connection string for SQLite database to desired location
- Ensure a stable internet connection for accessing the Cocktail Database API
- Run the application to build the database and see with starter data

## Program Operation

### Main Menu
The main menu prints the following options:

<img width="255" height="113" alt="image" src="https://github.com/user-attachments/assets/dacc7290-4d30-4bfe-8841-5d9849587618" />

### Browse Drink Menu
Selecting this option will query the Cocktails DB for a list of all drink categories.
If successful, it will print a Spectre.Console selection prompt where the user selects an option from the list.

<img width="285" height="251" alt="image" src="https://github.com/user-attachments/assets/153c8ed1-df0d-412b-9c45-d3c0ee2fa0b9" />

 
Upon selecting a Category, the app will again query the Cocktails DB, this time for drink summary data (Id and Name).
It will print all drinks matching the category into another Spectre.Console selection prompt, where the user can select a specific drink.
This list is searchable by typing the desired cocktail.

> Note that this uses the free version of the Cocktails DB API which only permits the first 100 records to be queried.

<img width="434" height="375" alt="image" src="https://github.com/user-attachments/assets/96c06e5a-6b50-48df-bf28-bc9f4b035735" />

 
Selecting a drink will query the Cocktails DB and print the information on how to make the selected drink.
A menu appears at the bottom of the screen to guide the user on available actions.

<img width="1132" height="610" alt="image" src="https://github.com/user-attachments/assets/b0aa76eb-5cd2-4134-af59-da1832a753dd" />


 
The following are the available options from this screen:
- V: Uses the image URL pulled from the Cocktail DB API to display a full console screen image of the drink
- F or X: Adds or removes the drink from the favorites list, depending on current state
- D: If accessed from "Browse Drink Menu" flow, will return to the list of all drinks in the selected category
- C: If accessed from "Browse Drink Menu" flow, will return to the category selection
- L: If accessed from "Manage Favorite Drink" flow, will return to the Favorite Drink list
- M: Returns to main menu

Below is a sample of the rendered drink image. It has been zoomed out in the console to make it more viewable. Zooming must be done manually by the user.

<img width="608" height="611" alt="image" src="https://github.com/user-attachments/assets/2ebcd148-158e-446d-9300-b42885663bc2" />


### Manage Favorite Drinks
Selecting the Manage Favorite Drinks option will display the list of all drinks currently saved as a favorite.
Full drink data is not saved, only the drink ID. Selecting a drink from this list will pull the data from the API.

<img width="362" height="154" alt="image" src="https://github.com/user-attachments/assets/00280f70-49c4-4799-969c-a7e25168c7b7" />

### Exit Application
Selecting the Exit Application option from the main menu will exit the application.

![Cheers-2](https://github.com/user-attachments/assets/7e557d2e-db35-4944-bd3f-3d893ffb112d)
## Project Requirements
This project follows the guidelines for The C Sharp Academy Intermediate Console Application Drink Info as found here: https://www.thecsharpacademy.com/project/15/drinks
