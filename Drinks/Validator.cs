namespace Drinks;

public class Validator
{
    internal static bool IsStringValid(string input)
    {
        if (String.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        foreach(char c in input)
        {
            if (!Char.IsLetter(c) && c != '/' && c != ' ')
            {
                return false;
            }
        }

        return true;
    }

    internal static bool IsIdValid(string input)
    {
        if (String.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        foreach (char c in input)
        {
            if (!Char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }
}