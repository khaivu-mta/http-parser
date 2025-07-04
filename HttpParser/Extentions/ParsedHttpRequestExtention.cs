﻿using HttpParser.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HttpParser.Extentions
{
    public static class ParsedHttpRequestExtention
    {
        public static string ToHttpRequestCode(this ParsedHttpRequest parsed)
        {
            var sb = new StringBuilder();
            sb.AppendLine("var client = new HttpClient();");

            var method = parsed.Method.ToUpperInvariant() switch
            {
                "GET" => "HttpMethod.Get",
                "POST" => "HttpMethod.Post",
                "PUT" => "HttpMethod.Put",
                "DELETE" => "HttpMethod.Delete",
                "HEAD" => "HttpMethod.Head",
                "OPTIONS" => "HttpMethod.Options",
                "TRACE" => "HttpMethod.Trace",
                "PATCH" => "HttpMethod.Patch",
                _ => $"new HttpMethod(\"{parsed.Method}\")"
            };

            sb.AppendLine($"var request = new HttpRequestMessage({method}, \"{parsed.Uri}\");");

            foreach (var header in parsed.GetHeaders())
            {
                if (header.Key.Equals("method", StringComparison.OrdinalIgnoreCase) ||
                    header.Key.Equals("httpversion", StringComparison.OrdinalIgnoreCase) ||
                    header.Key.Equals("cookie", StringComparison.OrdinalIgnoreCase))
                    continue;

                sb.AppendLine($"request.Headers.TryAddWithoutValidation(\"{header.Key}\", \"{header.Value}\");");
            }

            var cookies = parsed.GetCookies();
            if (cookies.Count > 0)
            {
                var cookieHeader = string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}"));
                sb.AppendLine($"request.Headers.TryAddWithoutValidation(\"cookie\", \"{cookieHeader}\");");
            }

            if (!string.IsNullOrWhiteSpace(parsed.RequestBody))
            {
                var pairs = parsed.RequestBody.Split('&')
                    .Select(x => x.Split('='))
                    .Where(x => x.Length == 2)
                    .Select(kv =>
                    {
                        var key = WebUtility.UrlDecode(kv[0]);  // Decode key to avoid double encoding
                        var value = WebUtility.UrlDecode(kv[1]).Replace("\"", "\\\"");  // Decode value and escape quotes

                        return $"collection.Add(new(\"{key}\", \"{value}\"));";
                    });

                sb.AppendLine("var collection = new List<KeyValuePair<string, string>>();");
                foreach (var line in pairs)
                {
                    sb.AppendLine(line);
                }

                sb.AppendLine("var content = new FormUrlEncodedContent(collection);");
                sb.AppendLine("request.Content = content;");
            }

            sb.AppendLine("var response = await client.SendAsync(request);");
            sb.AppendLine("response.EnsureSuccessStatusCode();");
            sb.AppendLine("Console.WriteLine(await response.Content.ReadAsStringAsync());");

            return sb.ToString();
        }


        public static string ToCurl(this ParsedHttpRequest parsed)
        {
            var sb = new StringBuilder();
            sb.Append("curl");

            var method = parsed.GetHeader("method").ToUpperInvariant();
            if (method != "GET")
                sb.Append($" --request {method}");

            var headers = parsed.GetHeaders();
            var contentType = "";
            var hasCookieHeader = false;

            foreach (var header in headers)
            {
                var key = header.Key.ToLowerInvariant();
                if (key is "method" or "httpversion")
                    continue;

                if (key == "content-type")
                    contentType = header.Value;

                if (key == "cookie")
                {
                    hasCookieHeader = true;
                    var rawCookie = header.Value
                        .Split(';')
                        .Select(x => x.Trim().TrimStart().Replace("cookie:", "", StringComparison.OrdinalIgnoreCase).Trim()) // Loại bỏ cookie: ở đầu key
                        .Where(x => !string.IsNullOrEmpty(x));
                    var combined = string.Join("; ", rawCookie);
                    sb.Append($" --header \"cookie: {combined}\"");
                    continue;
                }

                sb.Append($" --header \"{header.Key}: {header.Value}\"");
            }

            var cookies = parsed.GetCookies();
            if (!hasCookieHeader && cookies.Count > 0)
            {
                var cookieHeader = string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}"));
                sb.Append($" --header \"cookie: {cookieHeader}\"");
            }

            if (!string.IsNullOrEmpty(parsed.RequestBody) && method != "GET")
            {
                if (string.IsNullOrEmpty(contentType))
                    sb.Append(" --header \"Content-Type: text/plain\"");

                sb.Append($" --data \"{parsed.RequestBody}\"");
            }

            sb.Append($" \"{parsed.Uri}\"");
            return sb.ToString();
        }

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
