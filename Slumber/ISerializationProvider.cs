namespace Slumber
{
    public interface ISerializationProvider
    {
        ISerializer CreateSerializer(IRequest request);
        IDeserializer CreateDeserializer(IResponse response);
        void Register(string contentType, ISerializationFactory factory);
        void Remove(string contentType);
        ISerializationFactory GetFactory(string contentType);
    }
}
