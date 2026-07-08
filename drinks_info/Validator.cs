using System;

namespace drinks_info
{
    public class Validator
    {
        internal static bool IsStringValid(string? stringInput)
        {
            if (String.IsNullOrEmpty(stringInput))
                return false;

            foreach(char c in stringInput)
            {
                if (!Char.IsLetter(c) && c != '/' && c != ' ')
                    return false;
            }

            return true;
        }

        internal static bool IsIdValid(string? stringInput)
        {
            if (String.IsNullOrEmpty(stringInput))
                return false;

            foreach (char c in stringInput)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }

        internal static bool IsChoiceValid(string? stringInput)
        {
            if (String.IsNullOrEmpty(stringInput))
                return false;

            int choice;
            if(!int.TryParse(stringInput, out choice))
            {
                return false;
            }
            return 0 <= choice && choice <= 3;
        }
    }
}
