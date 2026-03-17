
namespace Drinks_Info
{
    internal class Validation
    {
        internal static bool isIdValid(string drink)
        {
            //TODO why is this validation always failing
            if (String.IsNullOrEmpty(drink)) { 
                    
                return false;
            }   
            foreach (char c in drink)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool isStringValid(string category)
        {
            if (category == null)
            {
                return false;
            }
            else if (category.Length == 0)
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
    }
}
