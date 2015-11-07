namespace Slumber
{
    public interface ISerializationFactory
    {
        IDeserializer CreateDeserializer();

        ISerializer CreateSerializer();
    }
}
