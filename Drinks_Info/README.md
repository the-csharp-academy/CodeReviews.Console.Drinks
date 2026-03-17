# Drinks Info
This app is a user interface that interacts with the public external cocktails database API. More info to come as I create more. 

**Tech used:** C#, RestSharp 



## Optimizations

None yet.


## Lessons Learned

- This is the first time I have coded something that communicates with an external API. 

- I am spending more time thinking over nullable reference types, and nullable warnings. Previously I always was just able to use a null forgiving operator safely, but in this project I must put more time and consideration into it. 

## Project Requirements

- Your job is to create a system that allows the restaurant employee to pull data from any drink in the database.

- When the users open the application, they should be presented with the Drinks Category Menu and invited to choose a category. Then they'll have the chance to choose a drink and see information about it.

- When the users visualise the drink detail, there shouldn't be any properties with empty values.

- You should handle errors so that if the API is down, the application doesn't crash.

**Suggested Challenges:**
- Add a 'Favorite Drinks' Functionality
  
- Find a way to show the images of the drinks.
  
- Add a view count to the images.
  
- Refactor and use the .NET HTTP client.
  
