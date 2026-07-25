# DrinkExplorer 🍸

A colorful, interactive C# console application for discovering cocktails and drinks, built on top of [TheCocktailDB](https://www.thecocktaildb.com/api.php) API. Browse drinks by category, search by name, view detailed recipes, save favourites, and track your most-viewed drinks — all from a slick terminal UI powered by [Spectre.Console](https://spectreconsole.net/).

## Features

- **Browse by Category** — View all available drink categories and drill down into drinks within each one.
- **Search Drinks** — Look up drinks by name directly from TheCocktailDB.
- **Drink Details** — See category, alcoholic content, glass type, and full preparation instructions in a styled panel.
- **Favourites** — Save drinks to a local favourites list for quick access later.
- **View Tracking** — Automatically tracks how many times each drink has been viewed, with a "Most Viewed Drinks" leaderboard.


## Tech Stack

- **.NET / C#**
- **[Spectre.Console](https://spectreconsole.net/)** — console UI (tables, panels, prompts, selection menus)
- **[SixLabors.ImageSharp](https://sixlabors.com/products/imagesharp/)** — image handling
- **TheCocktailDB API** — drink data source (`https://www.thecocktaildb.com/api/json/v1/1/`)
- Local database/repository layer for storing favourites and view counts

## Project Structure

```
DrinkExplorer/
├── Controller/
│   └── Menu.cs                    # Top-level navigation and menu flow
├── Services/
│   ├── ManageDrinksService.cs     # Fetches/searches drinks, shows drink details
│   └── FavouriteDrinkService.cs   # Displays saved favourites and most-viewed drinks
├── Database/
│   └── FavoriteDrinkRepository.cs # Persistence for favourites and view counts
├── Models/
│   ├── Drink.cs
│   ├── DrinkResponse.cs
│   ├── DrinkView.cs
│   └── CategoryResponse.cs
├── Validation/
│   └── InputValidator.cs          # Validates user input (e.g., drink name search)
└── Program.cs                     # Application entry point
```

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later recommended)
- SQL Server (local or remote instance)
- An internet connection (the app calls the live TheCocktailDB API)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Sangeerths/DrinkExplorer.git
   cd DrinkExplorer
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Set up the database by running `scripts.sql` against your SQL Server instance to create the required tables.
4. Run the application:
   ```bash
   dotnet run
   ```

## Usage

On launch, you'll be greeted with the main menu:

- **Drinks Menu**
  - **View All Categories** — browse drinks grouped by category
  - **Search Drink** — search for a drink by name
  - **Go Back** — return to the main menu
- **My Favourites**
  - **View All Favourite Drinks** — see everything you've saved
  - **Most Viewed Drink** — see your top viewed drinks, ranked
  - **Go Back** — return to the main menu
- **Exit** — quit the application

From any drink's detail view, you can open its image in a browser or add it to your favourites list.

## Notes

- Drink data is sourced live from TheCocktailDB's free API tier, so results depend on that service's availability.
- Favourites and view counts are stored locally, so your saved drinks persist between sessions.

