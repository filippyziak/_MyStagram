using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyStagram.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJSON(this object obj) => JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        public static T FromJSON<T>(this string obj) => JsonConvert.DeserializeObject<T>(obj);
    }
}