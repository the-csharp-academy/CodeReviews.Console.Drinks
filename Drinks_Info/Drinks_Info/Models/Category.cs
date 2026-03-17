using Newtonsoft.Json;

namespace Drinks_Info.Models
{
    public class Category
    {
        
        public required string StrCategory { get; set; } 

        public override string ToString()
        {
            return StrCategory;
        }
    }

    public class Categories
    {
        [JsonProperty("drinks")]
        public required List<Category> CategoriesList { get; set; }
    }
}
