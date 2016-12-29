namespace Slumber
{
    /// <summary>
    /// A class to convert a string to an object 
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// When implemented it should convert the provided string into an object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        T Deserialize<T>(string content);
    }
}
