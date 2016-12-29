namespace Slumber
{
    public interface ISerializationFactory
    {
        /// <summary>
        /// When implemented it should return an implementation of IDeserializer
        /// </summary>
        /// <returns></returns>
        IDeserializer CreateDeserializer();

        /// <summary>
        /// When implemented it should return an implementation of ISerializer
        /// </summary>
        /// <returns></returns>
        ISerializer CreateSerializer();

        /// <summary>
        /// When implemented it should return whether the supplied content type is supported or not
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        bool AppliesTo(string contentType);
    }
}
