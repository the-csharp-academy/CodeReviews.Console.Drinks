using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIClient.Validation
{
    internal class Validator
    {
        internal static bool IsIdValid(string? userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }

            foreach (char c in userInput)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }

        internal static bool IsStringValid(string? userInput)
        {
            if(string.IsNullOrEmpty(userInput))
            {
                return false;
            }

            foreach(char c in userInput)
            {
                if (!Char.IsLetter(c) && c != '/' && c != ' ')
                    return false;
            }
            return true;
        }
    }
}
