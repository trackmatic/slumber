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
        ISerializer CreateSerializer(IRequest request);
    }
}
