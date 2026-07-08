using drinks_info.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace drinks_info.Service
{
    public class FavouriteService
    {
        private readonly string _filePath =
            Path.Combine(
                Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
                "favourites.json"
            );

        public FavouritesList Load()
        {
            CheckFileExist();

            try
            {
                var json = File.ReadAllText(_filePath);
                var data =  JsonSerializer.Deserialize<FavouritesList>(json)
                       ?? new FavouritesList();

                data.favourites ??= new List<string>();
                return data;
            }
            catch
            {
                var emptyData = new FavouritesList();
                Save(emptyData);
                return emptyData;
            }
        }

        public void Add(string drinkId)
        {
            var data = Load();

            if (!data.favourites.Contains(drinkId))
            {
                data.favourites.Add(drinkId);
                Save(data);
            }
        }

        public void Remove(string drinkId)
        {
            var data = Load();

            if (data.favourites.Contains(drinkId))
            {
                data.favourites.Remove(drinkId);
                Save(data);
            }
        }

        public bool IsFavourite(string drinkId)
        {
            return Load().favourites.Contains(drinkId);
        }

        public void GetAll()
        {
            var favourites = Load().favourites;
            if (favourites == null || favourites.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\n\nNo favourite drinks saved yet.");
                return;
            }

            var drinksService = new DrinksService();
            var favoriteDrinks = new List<Drink>();

            foreach (var id in favourites)
            {
                if (string.IsNullOrEmpty(id)) continue;
                
                var drink = drinksService.GetDrinkById(id);
                if (drink != null)
                {
                    favoriteDrinks.Add(drink);
                }
            }

            TableVisualisationEngine.ShowTable(favoriteDrinks, "Favourite drinks");
        }

        public void Save(FavouritesList data)
        {
            var json = JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_filePath, json);
        }

        private void CheckFileExist()
        {
            if (!File.Exists(_filePath))
            {
                var emptyJson = JsonSerializer.Serialize(
                    new FavouritesList(),
                    new JsonSerializerOptions { WriteIndented = true }
                );

                File.WriteAllText(_filePath, emptyJson);
            }
        }
    }
}