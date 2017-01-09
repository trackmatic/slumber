using Slumber.Multipart;

namespace Slumber
{
    public class HttpRequestBuilder<T>
    {
        #region Factory Methods

        public static HttpRequestBuilder<T> Get(string path)
        {
            return New(path, HttpMethods.Get);
        }

        public static HttpRequestBuilder<T> Post(string path)
        {
            return New(path, HttpMethods.Post);
        }

        public static HttpRequestBuilder<T> Put(string path)
        {
            return New(path, HttpMethods.Put);
        }

        public static HttpRequestBuilder<T> Patch(string path)
        {
            return New(path, HttpMethods.Patch);
        }
        
        public static HttpRequestBuilder<T> Delete(string path)
        {
            return New(path, HttpMethods.Delete);
        }

        public static HttpRequestBuilder<T> New(string path, string method)
        {
            return new HttpRequestBuilder<T>(new HttpRequest<T>(path, method));
        }

        #endregion

        private readonly HttpRequest<T> _httpRequest;

        public HttpRequestBuilder(HttpRequest<T> httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public HttpRequestBuilder<T> QueryParameter(string name, object value, bool ignoreEmptyValues = false)
        {
            _httpRequest.AddQueryParameter(name, value, ignoreEmptyValues);
            return this;
        }

        public HttpRequestBuilder<T> Header(string name, string value)
        {
            _httpRequest.AddHeader(name, value);
            return this;
        }

        public HttpRequestBuilder<T> Content(object content)
        {
            _httpRequest.Data = content;
            return this;
        }

        public HttpRequestBuilder<T> Content(IMultipartContent content)
        {
            return WithContentType(ContentTypes.MultipartFormData).Content((object)content);
        }

        public HttpRequestBuilder<T> WithContentType(string type)
        {
            var header = HttpHeaders.ContentType;
            _httpRequest.RemoveHeader(header);
            _httpRequest.AddHeader(header, type);
            return this;
        }

        public HttpRequest<T> Build()
        {
            return _httpRequest;
        }
    }
}
