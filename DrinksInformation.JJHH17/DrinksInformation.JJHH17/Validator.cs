using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInformation.JJHH17
{
    internal class Validator
    {

        internal static bool IsStringValid(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput))
            {
                return false;
            }

            foreach(char c in stringInput)
            {
                if (!char.IsLetter(c) && c != '/' && c != ' ')
                    return false;
            }
            return true;
        }
    }
}
