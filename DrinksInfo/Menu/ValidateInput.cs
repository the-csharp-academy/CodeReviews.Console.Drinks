namespace DrinksInfo.Menu;
internal class ValidateInput
{
    public static bool ValidateString(string input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        foreach (char c in input)
        {
            if (!char.IsLetter(c) && c != '/' && c != ' ') return false;
        }

        return true;
    }

    public static bool ValidateId(string input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        foreach (char c in input)
        {
            if (!char.IsDigit(c)) return false;
        }

        return true;
    }
}