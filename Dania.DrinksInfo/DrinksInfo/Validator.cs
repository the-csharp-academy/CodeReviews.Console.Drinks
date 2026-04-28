namespace DrinksInfo
{
    internal class Validator
    {
        internal static bool IsStringValid(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput))
                return false;

            foreach (char c in stringInput)
            {
                if (!Char.IsLetter(c) && c != '/' && c != ' ')
                    return false;
            }

            return true;
        }

        internal static bool IsIdValid(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput))
                return false;

            foreach (char c in stringInput)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }
    }
}
