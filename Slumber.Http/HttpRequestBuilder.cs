namespace Slumber.Http
{
    public class HttpRequestBuilder<T>
    {
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
            return new HttpRequestBuilder<T>(new HttpRestRequest<T>(path, method));
        }

        private readonly HttpRestRequest<T> _request;

        public HttpRequestBuilder(HttpRestRequest<T> request)
        {
            _request = request;
        }

        public HttpRequestBuilder<T> QueryParameter(string name, object value, bool ignoreEmptyValues = false)
        {
            _request.AddQueryParameter(name, value, ignoreEmptyValues);
            return this;
        }

        public HttpRequestBuilder<T> Header(string name, string value)
        {
            _request.AddHeader(name, value);
            return this;
        }

        public HttpRequestBuilder<T> Content(object content)
        {
            _request.Data = content;
            return this;
        }

        public HttpRestRequest<T> Build()
        {
            return _request;
        }
    }
}
