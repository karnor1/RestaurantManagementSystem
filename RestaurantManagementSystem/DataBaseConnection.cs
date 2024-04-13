using System.IO;
using Newtonsoft.Json;

namespace RestaurantManagementSystem
{
    public class DataBaseConnection<T> : IOrderDatabaseParser<T>
    {
        string filePath;

        public DataBaseConnection(string _filepath)
        {
             filePath = _filepath;

        }

        public List<T> GetData()
        {
            if (!File.Exists(filePath)) { File.Create(filePath); }
            string jsonData = File.ReadAllText(filePath);
            List<T> data = JsonConvert.DeserializeObject<List<T>>(jsonData);
            if (data == null)
            {
                data = new List<T>();
            }
            return data;
        }

        public bool SaveData<T>(T data)
        {
            if (!File.Exists(filePath)) { File.Create(filePath); }

            string jsonData = JsonConvert.SerializeObject(data);

            File.WriteAllText(filePath, jsonData);
            return true;
        }

    }

}

