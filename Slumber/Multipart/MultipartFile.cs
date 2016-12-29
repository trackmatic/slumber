namespace Slumber.Multipart
{
    public class MultipartFile : IMultipartFile
    {
        public MultipartFile(string filename, string contentType, byte[] data)
        {
            Filename = filename;
            ContentType = contentType;
            Data = data;
        }

        public string Filename { get; }
        public string ContentType { get; }
        public byte[] Data { get; }
    }
}