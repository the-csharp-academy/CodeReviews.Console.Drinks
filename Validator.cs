using Drinks_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drinks_App
{
    class Validator
    {


        internal static bool ValidCategory(string? category)
        {
           if(string.IsNullOrEmpty(category))
            {
                return false;
            }

           foreach (char c in category)
            {
                if (!Char.IsLetter(c) && c != '/' && c != ' ')
                {
                    return false;
                }
            }

            return true;
        }
        internal static bool ValidateDrinkId(string? drinkId)
        {
            if (string.IsNullOrEmpty(drinkId))
            {
                return false;
            }

            foreach (char c in drinkId)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
