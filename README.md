# 🍸 Cocktail Connoiseur Console App

A simple .NET console application that interacts with [TheCocktailDB API](https://www.thecocktaildb.com/) to explore cocktails by category.

---

## 🚀 Features

- 🔍 Fetch and display a list of **cocktail categories**
- 🧭 Allow the user to **select a category**
- 🍹 Display a list of **cocktails within that category**
- 📋 Allow the user to **select a cocktail** to view detailed information:
  - Ingredients
  - Measurements
  - Instructions

---

## 🛠 Tech Stack

- C# (.NET)
- REST API integration using **RestSharp**
- JSON deserialization via **Newtonsoft.Json**

---

## 🖥️ How It Works

1. **Get Categories**
   - Pulls available cocktail categories from the API
   - Displays them in a user-friendly table format

2. **Select Category**
   - Prompts user to select one of the categories
   - Displays cocktails available in that category

3. **Select Cocktail**
   - Prompts user to select a drink
   - Fetches and displays detailed info including:
     - Drink name
     - Instructions
     - Ingredients and measurements

---

## 📦 Installation & Running

```bash
git clone https://github.com/TheNigerianNerd/CodeReviews.Console.Drinks.git
cd CodeReviews.Console.Drinks
dotnet run
```
📡 API Reference

    TheCocktailDB API

## Project Structure
```
Drinks.TheNigerianNerd/
├── DrinksService.cs
├── Models/
│   ├── Category.cs
│   ├── Drink.cs
│   └── DrinkDetail.cs
├── TableVisualizationEngine.cs
└── Program.cs
```
🧠 Notes

    The app uses asynchronous API calls with RestClient.ExecuteAsync()

    User input is sanitized using helper functions

    Uses System.Web.HttpUtility.UrlEncode() to handle category names with spaces

✍️ Author

Oche "The Nigerian Nerd" Edache
🔗 GitHub
