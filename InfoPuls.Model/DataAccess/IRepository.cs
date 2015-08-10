using System.Collections.Generic;

namespace InfoPuls.Model.DataAccess
{
    public interface IRepository<T>
    {
        Dictionary<string, T> GetInstances(string pathToFile);
        Dictionary<string, T> GetSimpleIntersection(Dictionary<string, T> first, Dictionary<string, T> second);
        Dictionary<string, T> GetStrongIntersection(Dictionary<string, T> first, Dictionary<string, T> second);
    }
}
