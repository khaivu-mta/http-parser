using HttpParser.Models;
using System;

namespace HttpParser
{
    public static class Parser
    {
        public static ParsedHttpRequest ParseRawRequest(string raw, IgnoreHttpParserOptions? options = null)
        {
            try
            {
                var lines = SplitLines(raw);

                var requestLine = new RequestLine(lines);
                var requestHeaders = new RequestHeaders(lines);
                requestHeaders.AddHeader("method", requestLine.Method);
                requestHeaders.AddHeader("httpversion", requestLine.HttpVersion);

                var requestCookies = new RequestCookies(lines);
                var requestBody = new RequestBody(requestLine, lines);

                var parsed = new ParsedHttpRequest(
                    url: requestLine.Url,
                    headers: requestHeaders.Headers,
                    cookies: requestCookies.ParsedCookies,
                    body: requestBody.Body,
                    regexIgnore: options ?? new()
                    );

                parsed.ApplyIgnoreOptions();

                return parsed;
            }
            catch (CouldNotParseHttpRequestException c)
            {
                Console.WriteLine($"Could not parse the raw request. {c.Message}");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled error parsing the raw request: {raw}\r\nError {e.Message}");
                throw;
            }
        }

        private static string[] SplitLines(string raw)
        {
            return raw
                .TrimStart('\r', '\n')
                .TrimEnd('\r', '\n')
                .Split(new[] { "\\n", "\n", "\r\n" }, StringSplitOptions.None);
        }
    }
}
