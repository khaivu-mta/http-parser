using System;
using System.Linq;

namespace HttpParser.Models
{
    internal class RequestBody
    {
        public string Body { get; set; }

        public RequestBody(RequestLine requestLine, string[] lines)
        {
            if (MethodHasNoBody(requestLine.Method))
            {
                Body = null;
            }
            else if (MethodCanHaveBody(requestLine.Method))
            {
                Body = ExtractBodyFromLines(lines);
            }
        }

        /// <summary>
        /// Các HTTP method không cho phép có body theo RFC 9110.
        /// </summary>
        private static bool MethodHasNoBody(string method)
        {
            return method == "GET"
                || method == "HEAD"
                || method == "TRACE";
        }

        /// <summary>
        /// Các HTTP method có thể có body theo RFC 9110, RFC 5789, RFC 4918.
        /// </summary>
        private static bool MethodCanHaveBody(string method)
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

        /// <summary>
        /// Tách phần body ra khỏi các dòng HTTP raw request (sau dòng trống).
        /// </summary>
        private static string ExtractBodyFromLines(string[] lines)
        {
            var emptyLineIndex = Array.FindIndex(lines, string.IsNullOrWhiteSpace);
            if (emptyLineIndex < 0 || emptyLineIndex == lines.Length - 1)
            {
                return null;
            }

            var bodyLines = lines.Skip(emptyLineIndex + 1);
            return string.Join("\n", bodyLines);
        }
    }
}
