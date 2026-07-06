using System;

namespace drinks_info
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInput userInput = new();
            userInput.GetCategoriesInput();
        }
    }
}