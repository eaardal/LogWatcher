using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LogWatcher.Domain.Helpers
{
    public class JsonHelpers
    {
        public static string ConvertToPrettifiedJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        } 
    }
}