using System;

namespace drinks_info.Models
{
    public class FavouritesList
    {
        public List<string> favourites { get; set; }

        public FavouritesList()
        {
            favourites = new List<string>();
        }
    }
}
