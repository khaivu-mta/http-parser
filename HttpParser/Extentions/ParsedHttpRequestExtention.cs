using HttpParser.Models;
using System.Net.Http.Headers;
using System.Text;

namespace HttpParser.Extentions
{
    public static class ParsedHttpRequestExtention
    {
        public static HttpRequestMessage ToHttpRequestMessage(this ParsedHttpRequest parsed)
        {
            var method = new HttpMethod(parsed.GetHeader("method"));
            var request = new HttpRequestMessage(method, parsed.Uri);
            var headers = parsed.GetHeaders();
            var cookies = parsed.GetCookies();

            foreach (var header in headers)
            {
                var headerKey = header.Key.ToLower();

                if (headerKey == "method" || headerKey == "httpversion" || headerKey == "cookie")
                    continue;

                request.Headers.TryAddWithoutValidation(headerKey, header.Value);
            }

            if (cookies.Count > 0)
            {
                var cookieHeader = string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}"));
                request.Headers.TryAddWithoutValidation("cookie", cookieHeader);
            }

            if (!string.IsNullOrEmpty(parsed.RequestBody) && MethodAllowsBody(headers["method"]))
            {
                var contentType = headers.FirstOrDefault(h => h.Key.ToLower() == "content-type").Value
                    ?? "text/plain";

                request.Content = new StringContent(parsed.RequestBody, Encoding.UTF8);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            }

            return request;
        }

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
    }
}
