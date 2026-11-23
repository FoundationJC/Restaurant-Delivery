using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Restaurant.Core
{
    public class JsonFileService<T>
    {
        private readonly string _filePath;
        public JsonFileService(string filePath)

        {
            _filePath = filePath;
        }
        public void Save(List<T> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_filePath, json);
        }
        public List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }



    }
}
