using Newtonsoft.Json;

namespace DrinksInfo.Models
{
    public class Category
    {
        public string strCategory { get; set; }
    }

    public class Categories
    {
        [JsonProperty("drinks")]
        public List<Category> categoriesList { get; set; }
    }
}
