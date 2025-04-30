using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpParser.Models
{
    internal class RequestCookies
    {
        public Dictionary<string, string> ParsedCookies = new Dictionary<string, string>();

        public RequestCookies(string[] lines)
        {
            // Xác định kiểu cookie và xử lý phù hợp
            var cookieLine = string.Join(" ", lines.Where(l => l.ToLower().StartsWith("cookie")));

            if (cookieLine.Contains(";"))
            {
                // Nếu có dấu ';', xử lý cookie trên một dòng
                PopulateParsedCookiesFromSingleLine(cookieLine);
            }
            else
            {
                // Nếu không có dấu ';', xử lý cookie tách sẵn trên nhiều dòng
                foreach (var line in lines)
                {
                    if (line.ToLower().StartsWith("cookie"))
                    {
                        PopulateParsedCookiesFromMultipleLines(line);
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý cookie trên một dòng. Cookie sẽ được phân tách bằng dấu ';' và từng cặp key=value.
        /// </summary>
        /// <param name="cookiesLine">Dòng chứa các cookie được phân tách bằng dấu ';'</param>
        private void PopulateParsedCookiesFromSingleLine(string cookiesLine)
        {
            if (string.IsNullOrEmpty(cookiesLine)) return;

            // Tách cookie theo dấu ';'
            var cookies = cookiesLine.Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cookie in cookies)
            {
                var cookieParts = cookie.Split(new[] { '=' }, 2); // Tách theo dấu '=' chỉ một lần
                if (cookieParts.Length == 2)
                {
                    var key = cookieParts[0].Trim();
                    var value = cookieParts[1].Trim();
                    ParsedCookies[key] = value;
                }
            }
        }

        /// <summary>
        /// Xử lý cookie tách sẵn trên nhiều dòng. Mỗi dòng chứa một cặp key=value.
        /// </summary>
        /// <param name="cookiesLine">Dòng chứa cookie theo dạng key=value</param>
        private void PopulateParsedCookiesFromMultipleLines(string cookiesLine)
        {
            if (string.IsNullOrEmpty(cookiesLine)) return;

            // Tách cookie theo từng dòng nếu có nhiều cookie
            foreach (var line in cookiesLine.Split('\n'))
            {
                var cookieParts = line.Split(new[] { '=' }, 2); // Tách theo dấu '=' chỉ một lần
                if (cookieParts.Length == 2)
                {
                    var key = cookieParts[0].Trim();
                    var value = cookieParts[1].Trim();
                    ParsedCookies[key] = value;
                }
            }
        }
    }
}
