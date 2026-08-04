using Newtonsoft.Json;

namespace drinks_info.Models
{
    internal class DrinkDetailObject
    {
        [JsonProperty("drinks")]
        public List<DrinkDetail> DrinkDetailList { get; set; }
    }
}