namespace Slumber
{
    public interface ISerializationFactory
    {
        IDeserializer CreateDeserializer();

        ISerializer CreateSerializer();
        bool AppliesTo(string contentType);
    }
}
