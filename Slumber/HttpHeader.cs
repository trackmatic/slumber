namespace Slumber
{
    public class HttpHeader
    {
        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as HttpHeader;
            if (other == null)
            {
                return false;
            }
            return other.Name == Name;
        }

        public static HttpHeader Accept(string type)
        {
            return new HttpHeader(HttpHeaders.Accept, type);
        }

        public static HttpHeader ContentType(string type)
        {
            return new HttpHeader(HttpHeaders.ContentType, type);
        }
    }
}
