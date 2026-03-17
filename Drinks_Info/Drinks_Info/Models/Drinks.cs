using Newtonsoft.Json;


namespace Drinks_Info.Models
{
    public class Drinks
    {
        [JsonProperty ("drinks")]
        public required List<Drink> DrinksList { get; set; }

    }
    public class Drink { 
    
        public required string idDrink {  get; set; }
        public required string strDrink { get; set; }

        public override string ToString()
        {
            return strDrink;
        }
    }

}
