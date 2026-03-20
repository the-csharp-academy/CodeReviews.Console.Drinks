using System.Text.Json.Serialization;

namespace DrinksInfo.Infrastructure.Models;

public record CategoryApi([property: JsonPropertyName("strCategory")] string Name);


