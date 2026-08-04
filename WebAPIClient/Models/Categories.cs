using Newtonsoft.Json;

namespace WebAPIClient.Models
{
    public class Categories
    {
        [JsonProperty("drinks")]
        public List<Category> CategoriesList { get; set; }
    }
}
