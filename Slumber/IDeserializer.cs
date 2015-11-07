namespace Slumber
{
    /// <summary>
    /// A factory for creating a deserializer 
    /// </summary>
    public interface IDeserializer
    {
        T Deserialize<T>(string content);
    }
}
