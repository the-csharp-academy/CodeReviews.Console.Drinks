# Drinks Info.

My second intermediate console project on C# Academy.  
A console application for managing drinks info using TheCocktailDB API to retrieve information about the drinks and work with it.

# Requeriments

- [x] You were hired by restaurant to create a solution for their drinks menu.
- [x] Their drinks menu is provided by an external company. All the data about the drinks is in the companies database, accessible through an API.
- [x] Your job is to create a system that allows the restaurant employee to pull data from any drink in the database.
- [x] You don't need SQL here, as you won't be operating the database. All you need is to create a user-friendly way to present the data to the users (the restaurant employees)
- [x] When the users open the application, they should be presented with the Drinks Category Menu and invited to choose a category. Then they'll have the chance to choose a drink and see information about it.
- [x] When the users visualise the drink detail, there shouldn't be any properties with empty values.
- [x] You should handle errors so that if the API is down, the application doesn't crash.

# C# Academy Challenges

- [x] Add a 'Favorite Drinks' functionality
- [x] Find a way to show a picture of the drinks
- [x] If you followed the tutorial in the resources, you noticed we used Rest Client for the handling of the HTTP requests. Try using .NET's HTTP client instead, as it's more commonly used in the industry.

# Features

- The program uses TheCocktailDB API to show and work with the information about the drinks.
- The programs uses SQlite to save some info about the drinks.
- Dapper was used to query the database.
- The Microsoft.Configuration package is used to work with the information collected from the API in JSON format.

**Spectre Console UI**
  - The application uses a Spectre Console UI where we can navigate using the keyboard.

  <p align="center">
    <img width="266" height="160" alt="imagen" src="https://github.com/user-attachments/assets/ae50f80f-d0c4-4254-a615-e2b5229063f0" />
  </p>

  - The first menu shows four options:
    - ShowCategories
    - ShowFavoriteDrinks
    - ShowDrinkViews
    - Exit (Close the program)

## ShowCategories

- The ShowCategories menu is the main section of the program where we can view the different drinks provided by the API

<p align="center">
  <img width="512" height="857" alt="imagen" src="https://github.com/user-attachments/assets/c2aff869-9d86-416d-ac9a-8d41b4808d0d" />
</p>

- Once we've finished reviewing the information about the drink we're interested in, we have the option to add it to our favorites.

<p align="center">
  <img width="1232" height="667" alt="imagen" src="https://github.com/user-attachments/assets/dfcdbc3e-50a1-40d5-b783-a850fe66a31b" />
</p>

- When we finished to add or not the drink in our favorites drinks we can choose go back to categories menu,drinks menu or main menu.

## ShowFavoriteDrinks

- The ShowFavoriteDrinks shows us three options to manage our favorite drinks.

<p align="center">
  <img width="237" height="153" alt="imagen" src="https://github.com/user-attachments/assets/4d0c7a1d-a4ca-4c36-acc8-2bfb8ed0834f" />
</p>

- The view option shows us our favorite drinks and we can view the details of the drink we choose.

<p align="center">
  <img width="713" height="211" alt="imagen" src="https://github.com/user-attachments/assets/7f2d2ba7-2c63-4407-b2bf-ea7680ab1831" />
  <img width="1915" height="686" alt="imagen" src="https://github.com/user-attachments/assets/55454c89-cdc3-457c-bb4b-d1bfb1988b66" />
</p>

- The add option send us to the categories menu.
- The remove option remove the drink we choose.

<p align="center">
  <img width="527" height="257" alt="imagen" src="https://github.com/user-attachments/assets/74f2666d-07a1-480b-83d9-ada014662ae3" />
</p>

## ShowDrinksViews

- The ShowDrinksViews menu allows us to view how many times a drink has been viewed.

<p align="center">
  <img width="225" height="130" alt="imagen" src="https://github.com/user-attachments/assets/20c46dc5-1656-4f1c-ab3e-236066872bdc" />
</p>

- We can also add images of the drinks shown

<p align="center">
  <img width="641" height="325" alt="imagen" src="https://github.com/user-attachments/assets/5e533c23-cb12-42ab-87ae-93f270dcec45" />
</p>

- Once we're done, we can go to the categories menu or return to the views menu

# Challenges
- Display beverage information correctly using AnsiConsole tables.
- Save the necessary information about drinks to your favorites and those you've viewed.
- Allow users to access the categories menu from the Favorites and Views menus.
- Manage API usage with asynchronous functions using Task

# What I have learned
- How to correctly retrieve information from an API and display it in the console.
- format the data that the API sends me in JSON format so I can use it in the project
- Use the AnsiConsole tables to display information dynamically, rather than creating a method every time you want to display it.
- Organize the classes into different directories.

# Areas to improve
- Learn more about how asynchronous classes and methods work in C#
- Use basic HTML and CSS or a front-end framework to create more dynamic interfaces and display images.
- Make better use of the tools provided by SpectreConsole to improve the console interface.

# Resourced Used 

- TheCocktailDB Documentation: https://www.thecocktaildb.com/documentation
- Microsoft Documentation for HTTP request: https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
- Specter Console Documentation: https://spectreconsole.net/
- Learn Dapper: https://www.learndapper.com/
- Microsoft documentation for List: https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1?view=net-8.0
- Microsoft documentation for Tasks: https://learn.microsoft.com/es-es/dotnet/api/system.threading.tasks.task?view=net-10.0
- C# Video of the Project: https://www.youtube.com/watch?v=fc7peZ-FHs4
- SQlite documentation: https://sqlite.org/docs.html
