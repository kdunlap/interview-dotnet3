using System.Collections.Generic;
using System.IO;
using GroceryStoreAPI.Models;
using Newtonsoft.Json;

namespace GroceryStoreAPI.Helpers
{
    public static class JsonLoader
    {
        private const string FilePath = "database.json";

        public static IEnumerable<Customer> LoadCustomers()
        {
            using StreamReader r = new StreamReader(FilePath);
            var json = r.ReadToEnd();
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<Customer>>>(json);
            return result["customers"];
        }
    }
}