using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfoPuls.Model.Entity;
using InfoPuls.Model.Tools;
using InfoPuls.Model.Extensions;

namespace InfoPuls.Model.DataAccess
{
    public class UserRepository : IRepository<User>
    {
        public Dictionary<string, User> GetInstances(string pathToFile)
        {
            if(!File.Exists(pathToFile))
                throw new ArgumentException(String.Format("Path to file not valid : {0}", pathToFile) , "pathToFile");

            string[] lines = File.ReadAllLines(pathToFile);

            var result = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);

            foreach (string line in lines)
            {
                if(string.IsNullOrEmpty(line))
                    continue;

                string[] userString = Helper.SplitAndTrim(line, ',');

                if(userString.Length != 4)
                    continue;
                
                string email = Helper.isEmail(userString[0]) ? userString[0] : string.Empty;
                string lastName = userString[1];
                string firstName = userString[2];
                DateTime dateOfBirth = Helper.GetDateInUsFormat(userString[3]);

                if(!string.IsNullOrEmpty(email) && !result.ContainsKey(email))
                    result.Add(email, new User(email, lastName, firstName, dateOfBirth));
            }

            return result;
        }


        public Dictionary<string, User> GetSimpleIntersection(Dictionary<string, User> first, Dictionary<string, User> second)
        {
           return  first.SimpleIntersection(second);
        }

        public Dictionary<string, User> GetStrongIntersection(Dictionary<string, User> first, Dictionary<string, User> second)
        {
            Helper.ArgumentNullReferenceException(first, "first", "GetStrongIntersection");
            Helper.ArgumentNullReferenceException(second, "second", "GetStrongIntersection");

            Dictionary<string, User> min = first.Count <= second.Count ? first : second;
            Dictionary<string, User> max = second.Count >= first.Count ? second : first;
            
            var result = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);

            IEnumerable<string> intersectKeys = min.Keys.Intersect(max.Keys, StringComparer.OrdinalIgnoreCase);
            foreach (string key in intersectKeys)
            {
                int minValueHash = min[key].GetHashCode();
                int maxValueHash = max[key].GetHashCode();

                if (minValueHash == maxValueHash)
                    result.Add(key, min[key]);
            }

            return result;
        }
    }
}
