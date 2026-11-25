# Drinks

Console based Menu application to learn WebAPI.
Navigate and view drinks from 'thecocktaildb.com'.

## How to setup

- Turn internet on.
- Launch the project with .Net 11 and the IDE of your choice (cmd : 'dotnet run' within the 'Drinks.FeedMyData' folder).

## Given Requirements:

- You were hired by restaurant to create a solution for their drinks menu.
- Their drinks menu is provided by an external company. All the data about the drinks is in the companies database, accessible through an API.
- Your job is to create a system that allows the restaurant employee to pull data from any drink in the database.
- You don't need SQL here, as you won't be operating the database. All you need is to create a user-friendly way to present the data to the users (the restaurant employees)
- When the users open the application, they should be presented with the Drinks Category Menu and invited to choose a category. Then they'll have the chance to choose a drink and see information about it.
- When the users visualise the drink detail, there shouldn't be any properties with empty values.
- You should handle errors so that if the API is down, the application doesn't crash.
- Include a README with instructions on how to run the project, what the app does, how it works, and your architectural choices. Most importantly, include clear setup and run instructions and a reflection on your development experience, written in your own words.

## Features

- Display data from a WebAPI
- A simple console based UI using Spectre.Console

## NuGet used
- Refit 17.0
- Spectre.Console

## How it works
- 'IDrinksApi' get a client feeded (cocktailDb API) and a Contract ('DrinkssJsonContext') binding the api data with the app Models. This Interface defines all the subpath within the api. [Refit workflow]
- 'DrinksService' get the data thanks to 'IDrinksApi' and return their model counterpart.
- 'UserInterface' finally let the user navigate inside the data pulled from the API thanks to 'DrinksService'.
- 'Displayer' and 'ChoicesPrompt' contain helpers method to declutter 'UserInterface'.

## Challenges

- First time using a WebApi. I tried three ways to integrate it (System.Net.Http / RestSharp / ReFit)
- The last version of the ReFit package (17.0, from November 2027) stopped to used Reflection to increase performance. So most of the example I read were outdated. I never really dealt with Reflection before. I had to understand what it meant.
- It's somehow good since Reflection was necessary to process the data pulled from the API.

## Lessons Learned

- Discovering the world of WebApi. It feels like there is still a lot to see.
- Getting the hang of the "why" and "when" of Reflection.

## Areas to Improve

- I want to get better at writing concise syntax. 
	
## Resources Used
	
- The c# academy : Video: Drinks Info App [https://www.youtube.com/watch?v=fc7peZ-FHs4]
- [Microsoft Docs: Http Requests] https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
- Spectre documentation
- [reactiveui.net] https://www.reactiveui.net/documentation/refit/serialization/json/
- [Dotnet.Rest] https://dotnet.rest/docs/libraries/
- Copilot for mentoring, learning better syntax

