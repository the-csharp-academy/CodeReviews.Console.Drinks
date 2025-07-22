using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.TheNigerianNerd.Models;

public class Drinks
{
    [JsonProperty("drinks")]
    public List<Drink> DrinksList { get; set; }
}

public class Drink
{
    public string idDrink { get; set; }
    public string strDrink { get; set; }
}
