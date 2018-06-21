namespace Eagles.Base.Json
{
    public interface IJsonSerialize:IInterfaceBase
    {
        object DeserializeObject(string json);

        string SerializeObject(object obj);

        T Deserialize<T>(string json);
    }
}
