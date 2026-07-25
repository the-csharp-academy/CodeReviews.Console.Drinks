using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DrinkExplorer.Validation
{
    public class InputValidator
    {
        public InputValidator() { }

        public bool ValidateDrinkName(string drinkName)
        {
            if (string.IsNullOrWhiteSpace(drinkName)) return false;

            string trimmed = drinkName.Trim();
            return trimmed.Length >= 2
                && trimmed.Length <= 50
                && trimmed.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }
    }
}
