using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static void PrintAllMethodsInTheApplication()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<string?> namespaces = assembly.GetTypes()
                                 .Where(t => t.IsClass)
                                 .Select(t => t.Namespace)
                                 .Distinct()
                                 .OrderBy(ns => ns);

            foreach (string ns in namespaces)
            {
                Console.WriteLine($"Namespace : {ns}");
                assembly = Assembly.GetExecutingAssembly();
                Type[] classes = assembly.GetTypes()
                                        .Where(t => t.IsClass & t.Namespace == ns)
                                        .OrderBy(t => t.Name)
                                        .ToArray();
                foreach (Type t in classes)
                {
                    Console.WriteLine($"    Class : {t.Name}");
                    PrintAllMethodsOfClass(t);
                }

            }

        }

        public static void PrintAllMethodsOfClass(Type type)
        {
            MethodInfo[] methods = type.GetMethods();
            string[] commonMethods = new string[] { "GetType", "Equals", "GetHashCode", "ToString" };
            methods = methods.Where(t => !commonMethods.Contains(t.Name)).ToArray();
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"        --------> {method.Name} ");
            }
        }



        


    }
}
