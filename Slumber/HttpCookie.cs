using System.Collections.Generic;
using System.Linq;

namespace Slumber
{
    public class HttpCookie
    {
        private readonly Dictionary<string, string> _values;

        private readonly string _cookie;

        public HttpCookie(string cookie)
        {
            _cookie = cookie;
            _values = cookie.Split(';').Select(x => x.Trim().Split('=')).GroupBy(x => x[0]).Select(x => x.First()).ToDictionary(x => x[0], x => x.Length == 1 ? string.Empty : x[1]);
        }

        public string GetName()
        {
            return _values.First().Key;
        }

        public string GetValue()
        {
            return _values.First().Value;
        }

        public string GetPath()
        {
            return _values["path"];
        }

        public string GetDomain()
        {
            return _values["domain"];
        }

        public override int GetHashCode()
        {
            return _cookie.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as HttpCookie;
            if (other == null)
            {
                return false;
            }
            return other._cookie == _cookie;
        }
    }
}
