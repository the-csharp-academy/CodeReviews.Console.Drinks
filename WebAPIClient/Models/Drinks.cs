using Newtonsoft.Json;

namespace WebAPIClient.Models
{
    public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinksList { get; set; }
    }
}

