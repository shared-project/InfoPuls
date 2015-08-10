using System.Collections.Generic;
using InfoPuls.Model.Entity;
using InfoPuls.Model.Tools;

namespace InfoPuls.Model.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<S, T> SimpleIntersection<S, T>(this Dictionary<S, T> current, Dictionary<S, T> instance)
            where T : User
        {
            Helper.ArgumentNullReferenceException(current, "current", "SimpleIntersection");
            Helper.ArgumentNullReferenceException(instance, "instance", "SimpleIntersection");

            Dictionary<S, T> min = current.Count <= instance.Count ? current : instance;
            Dictionary<S, T> max = instance.Count >= current.Count ? instance : current;

            // the same as below
            // var result = min.Where(x => max.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            
            Dictionary<S, T> result = new Dictionary<S, T>();
            foreach (var value in min)
            {
                if(max.ContainsKey(value.Key))
                    result.Add(value.Key, value.Value);
            }

            return result;
        }
    }
}
