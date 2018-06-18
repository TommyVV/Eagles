using Newtonsoft.Json;

namespace Eagles.Base.Json.Implement
{
    public class JsonSerialize:IJsonSerialize
    {

        public object DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
