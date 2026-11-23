using System.Text.Json;
using System.Text.Json.Serialization;

namespace Drinks;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Category))]
[JsonSerializable(typeof(Categories))]
// [JsonSerializable(typeof(Drink))]
// [JsonSerializable(typeof(DrinkDetail))]
internal sealed partial class DrinksJsonContext : JsonSerializerContext;
