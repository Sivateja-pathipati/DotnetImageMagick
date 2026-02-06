using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Utilities
{
    internal class ConsoleUtilities
    {
        public static int OptionsGenerator(string[] options, string exitOption = "Exit the Application")
        {
            Console.WriteLine("Please choose the following ");
            int i = 1;
            for (; i <= options.Length; i++)
            {
                Console.WriteLine($"{i} => {options[i - 1]}");
            }
            Console.WriteLine($"{0} => {exitOption}");

            int userInput;
            try
            {
                userInput = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine($"Invalid choice performing '{exitOption}' option");
                userInput = 0;
            }
            userInput = userInput >= 0 && userInput <= options.Length ? userInput : 0;
            return userInput;
        }
        public static void PrintAll(IEnumerable<string> strings)
        {
            if (strings.Count() == 0)
            {
                Console.WriteLine("Empty List");
            }
            foreach (string str in strings)
            {
                Console.WriteLine(str);
            }
        }

        


    }
}
