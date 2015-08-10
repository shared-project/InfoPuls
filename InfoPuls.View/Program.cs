using System;
using System.Collections.Generic;
using InfoPuls.Model.DataAccess;
using InfoPuls.Model.Entity;
using InfoPuls.Model.Tools;

namespace InfoPuls.View
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string pathToFile1 = Helper.GetFullPathToFileInCurrentFolder("testData1.txt");
                string pathToFile2 = Helper.GetFullPathToFileInCurrentFolder("testData2.txt");

                IRepository<User> repository = new UserRepository();
                
                Dictionary<string, User> users1 = repository.GetInstances(pathToFile1);
                Dictionary<string, User> users2 = repository.GetInstances(pathToFile2);
                
                Dictionary<string, User> result = repository.GetSimpleIntersection(users1, users2);

                ShowResult(result, "Simple Intersection");

                Dictionary<string, User> result2 = repository.GetStrongIntersection(users1, users2);

                ShowResult(result2, "Strong Intersection");
            }
            catch (Exception e)
            {
                string errorMessage = String.Format("ErrorMessage : {0}\nStackTrace:\n {1}", e.Message, e.StackTrace);
                Console.WriteLine(errorMessage);
            }

            Console.ReadKey();
        }

        public static void ShowResult(Dictionary<string, User> result, string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();

            foreach (var user in result)
            {
                Console.WriteLine(user.Value);
            }
        }
    }
}
