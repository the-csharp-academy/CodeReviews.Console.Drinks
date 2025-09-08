using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInformation.JJHH17
{
    internal class UserInput
    {
        DrinksService drinksService = new DrinksService();

        internal void GetCategoriesInput()
        {
            drinksService.GetCategories();
        }
    }
}
