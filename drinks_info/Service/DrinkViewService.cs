using drinks_info.Models;
using System;
using System.Text.Json;

namespace drinks_info.Service
{
    public class DrinkViewService
    {
        private readonly string _filePath =
            Path.Combine(
                Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
                "views.json"
            );

        public List<DrinkView> Load()
        {
            CheckFileExist();

            try
            {
                var json = File.ReadAllText(_filePath);
                var data = JsonSerializer.Deserialize<List<DrinkView>>(json)
                        ?? new List<DrinkView>();

                return data;
            }

            catch
            {
                var emptyData = new List<DrinkView>();
                Save(emptyData);
                return emptyData;
            }
        }

        public void IncreaseViewCount(string drinkId, string drinkName)
        {
            var data = Load();
            var record = data.FirstOrDefault(x => x.DrinkId ==  drinkId);

            if(record == null)
            {
                data.Add(new DrinkView
                {
                    DrinkId = drinkId,
                    DrinkName = drinkName,
                    ViewCount = 1
                });
            }
            else
            {
                record.ViewCount++;
            }
            Save(data);
        }

        public void GetAll()
        {
            var views = Load();

            if(views == null || views.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\n\nNo drinks have been viewed yet.");
                return;
            }

            var sortedViews = views.OrderByDescending(x => x.ViewCount).ToList();

            TableVisualisationEngine.ShowTable(sortedViews, "Drink Views Count");
        }

        public void Save(List<DrinkView> data)
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
                    new List<DrinkView>(),
                    new JsonSerializerOptions { WriteIndented = true }
                );
                File.WriteAllText(_filePath, emptyJson);
            }
        }
    }
}