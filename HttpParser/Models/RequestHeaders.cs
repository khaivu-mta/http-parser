﻿namespace HttpParser.Models
{
    internal class RequestHeaders
    {
        public Dictionary<string, string> Headers;

        public RequestHeaders(string[] lines)
        {
            Headers = new Dictionary<string, string>();
            var lastIndex = DetectLastRowIndex(lines);
            for (int i = 1; i < lastIndex; i++)
            {
                var (key, value) = GetHeader(lines[i]);

                if (key == "cookie") continue;

                Headers[key] = value;
            }
        }

        public void AddHeader(string key, string value)
        {
            Headers[key] = value;
        }

        public void RemoveHeader(string key)
        {
            if (Headers.ContainsKey(key))
                Headers.Remove(key);
        }

        private static (string key, string value) GetHeader(string line)
        {
            var pieces = line.Split(new[] { ':' }, 2);

            return (pieces[0].Trim(), pieces[1].Trim());
        }

        private static int DetectLastRowIndex(string[] lines)
        {
            var blankIndex = Array.IndexOf(lines, "");
            return blankIndex == -1 ? lines.Length : blankIndex - 1;
        }
    }
}
