namespace Slumber
{
    /// <summary>
    /// Encodes query parameters in a requests uri
    /// </summary>
    public interface IParameterEncoder
    {
        /// <summary>
        /// Encodes inline parameters
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        string Encode(string path, RestQueryParameter parameter);

        /// <summary>
        /// Encodes query parameters
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        string Encode(RestQueryParameter parameter);
    }
}
