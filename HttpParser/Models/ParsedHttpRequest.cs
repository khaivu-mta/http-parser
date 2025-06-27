using System.Text;
using System.Text.RegularExpressions;

namespace HttpParser.Models
{
    public class ParsedHttpRequest
    {
        public string Url { get; set; }
        public string Method { get; private set; }
        private Dictionary<string, string> _headers;
        private Dictionary<string, string> _cookies;

        public string RequestBody { get; set; }
        public Uri Uri { get; set; }

        private IgnoreHttpParserOptions regexIgnoreHttpParserOptions;

        public ParsedHttpRequest(
            string url,
            Dictionary<string, string> headers,
            Dictionary<string, string> cookies,
            string body,
            IgnoreHttpParserOptions regexIgnore)
        {
            Url = url;
            _headers = headers.ToDictionary(h => h.Key.ToLower(), h => h.Value);
            _cookies = cookies.ToDictionary(c => c.Key.ToLower(), c => c.Value);
            RequestBody = body;
            Uri = new(url);
            this.regexIgnoreHttpParserOptions = regexIgnore;
            this.Method = _headers["method"];
        }

        #region Helper
        private static bool MethodAllowsBody(string method)
        {
            return method == "POST"
                || method == "PUT"
                || method == "PATCH"
                || method == "DELETE"
                || method == "OPTIONS"
                || method == "CONNECT"
                || method == "PROPFIND"
                || method == "MKCOL"
                || method == "LOCK"
                || method == "UNLOCK";
        }

        public Dictionary<string, string> GetHeaders()
        {
            return this._headers;
        }

        public Dictionary<string, string> GetCookies()
        {
            return this._cookies;
        }

        public string GetHeader(string key)
        {
            var lowerKey = key.ToLower();
            return _headers.ContainsKey(lowerKey) ? _headers[lowerKey] : string.Empty;
        }

        public string GetCookie(string key)
        {
            var lowerKey = key.ToLower();
            return _cookies.ContainsKey(lowerKey) ? _cookies[lowerKey] : string.Empty;
        }

        public override string ToString()
        {
            if (_headers == null || !_headers.ContainsKey("method") || !_headers.ContainsKey("httpversion"))
            {
                return string.Empty;
            }

            var method = _headers["method"];
            var version = _headers["httpversion"];

            var sb = new StringBuilder($"{method} {Url} {version}{Environment.NewLine}");

            var headersToIgnore = new List<string> { "method", "httpversion" };
            if (_cookies == null) headersToIgnore.Add("cookie");

            foreach (var header in _headers)
            {
                if (headersToIgnore.Contains(header.Key)) continue;
                sb.Append($"{header.Key}: {header.Value}{Environment.NewLine}");
            }

            if (_cookies?.Count > 0)
            {
                var cookies = string.Join("; ", _cookies.Select(c => $"{c.Key}={c.Value}"));
                sb.Append($"cookie: {cookies}{Environment.NewLine}");
            }

            if (MethodAllowsBody(method) && !string.IsNullOrEmpty(RequestBody))
            {
                sb.Append(Environment.NewLine);
                sb.Append(RequestBody);
            }

            return sb.ToString().Trim();
        }
        #endregion

        // Apply ignore options with regex
        public void ApplyIgnoreOptions()
        {
            if (regexIgnoreHttpParserOptions == null) return;

            if (!string.IsNullOrEmpty(Url) &&
                regexIgnoreHttpParserOptions.IgnoreUrls.Any(pattern =>
                    Regex.IsMatch(Url, pattern, RegexOptions.IgnoreCase)))
            {
                Url = string.Empty;
            }

            if (_headers != null && regexIgnoreHttpParserOptions.IgnoreHeaders.Count > 0)
            {
                var keysToRemove = _headers.Keys
                    .Where(h => regexIgnoreHttpParserOptions.IgnoreHeaders
                        .Any(pattern => Regex.IsMatch(h, pattern, RegexOptions.IgnoreCase)))
                    .ToList();

                foreach (var key in keysToRemove) _headers.Remove(key);
            }

            if (_cookies != null && regexIgnoreHttpParserOptions.IgnoreCookies.Count > 0)
            {
                var keysToRemove = _cookies.Keys
                    .Where(c => regexIgnoreHttpParserOptions.IgnoreCookies
                        .Any(pattern => Regex.IsMatch(c, pattern, RegexOptions.IgnoreCase)))
                    .ToList();

                foreach (var key in keysToRemove) _cookies.Remove(key);
            }

            if (!string.IsNullOrEmpty(RequestBody) &&
                regexIgnoreHttpParserOptions.IgnoreRequestBodies.Any(pattern =>
                    Regex.IsMatch(RequestBody, pattern, RegexOptions.IgnoreCase)))
            {
                RequestBody = string.Empty;
            }
        }
    }
}
