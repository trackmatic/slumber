namespace Slumber.Multipart
{
    public interface IMultipartFile
    {
        string Filename { get; }
        string ContentType { get; }
        byte[] Data { get; }
    }
}
