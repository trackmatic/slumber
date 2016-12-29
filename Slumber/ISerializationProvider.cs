namespace Slumber
{
    public interface ISerializationProvider
    {
        ISerializer CreateSerializer(IRequest request);
        IDeserializer CreateDeserializer(IResponse response);
        void Register(ISerializationFactory factory);
    }
}
