using System.Text.Json;
using API.DTOs;
using API.Interfaces;

namespace API.Services
{
    public class AppService : IAppService
    {
    public AppService()
    {
    }

    public object DeserializeFromStream()
    {
            string fileName = @"C:\Users\danie\Desktop\DannesBlogg\client\Backend\API\Data\AppData\AppData.json";
            string jsonString = File.ReadAllText(fileName);
            
            var categories = JsonSerializer.Deserialize<List<Category>>(jsonString)!;
            
            return categories;
    }
  }
}