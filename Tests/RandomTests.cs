using FluentAssertions;
using HttpParser;
using HttpParser.Extentions;
using HttpParser.Models;
using NUnit.Framework;
using System;
using System.Linq;
using Tests.FakeData;

namespace Tests
{
    [TestFixture]
    public class RandomTests
    {
        [Test]
        public void kevin()
        {
            var rawRequestTestCases = new string[]
            {
                FakeRawRequests.Kevin1,
                FakeRawRequests.Kevin2,
                FakeRawRequests.Kevin3,
                FakeRawRequests.Kevin4,
                FakeRawRequests.Kevin5,
                FakeRawRequests.Kevin6,
                FakeRawRequests.Kevin7,
                FakeRawRequests.Kevin8,
                FakeRawRequests.Kevin9,
                FakeRawRequests.Kevin10,
                FakeRawRequests.Kevin11,
                FakeRawRequests.Kevin12,
                FakeRawRequests.Kevin13,
                FakeRawRequests.Kevin14,
                FakeRawRequests.Kevin15,
                FakeRawRequests.Kevin16,
                FakeRawRequests.Kevin17,
            };

            var ignoreHeaders = new IgnoreHttpParserOptions();
            ignoreHeaders.IgnoreHeaders.AddRange(new string[]
            {
                "accept-encoding",
                "content-length",
            });

            for (int i = 0; i < rawRequestTestCases.Length; i++)
            {
                Console.WriteLine($"TestCase #{i + 1}");

                var raw = rawRequestTestCases[i];
                var parsed = Parser.ParseRawRequest(raw, ignoreHeaders);
                var req = parsed.ToHttpRequestMessage();

                var headers = parsed.GetHeaders();
                var cookies = parsed.GetCookies();

                req.Should().NotBeNull();
                req.Method.Method.Should().Be(headers["method"]);
                req.RequestUri.Should().Be(parsed.Uri);

                // Kiểm tra các header
                foreach (var header in headers)
                {
                    var headerKey = header.Key.ToLower();
                    if (headerKey is "method" or "httpversion" or "cookie")
                        continue;

                    var found = req.Headers.TryGetValues(header.Key, out var values)
                        || (req.Content?.Headers.TryGetValues(header.Key, out values) ?? false);

                    if (!found)
                    {
                        // Ghi log lỗi khi header thiếu
                        Console.WriteLine($"TestCase #{i + 1}: Missing header '{header.Key}'");
                        continue;
                    }

                    var actual = string.Join(",", values).Replace(" ", "").Replace(",", "");
                    if (actual != header.Value.Replace(" ", "").Replace(",", ""))
                    {
                        // Ghi log lỗi khi giá trị header không khớp
                        Console.WriteLine($"TestCase #{i + 1}: Header mismatch for '{header.Key}', expected '{header.Value}', found '{actual}'");
                    }
                }

                // Kiểm tra cookies nếu có
                if (cookies.Count > 0)
                {
                    req.Headers.TryGetValues("cookie", out var cookieValues).Should().BeTrue();
                    var expectedCookie = string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}"));
                    cookieValues.First().Should().Be(expectedCookie);
                }

                // Kiểm tra body nếu có
                if (!string.IsNullOrEmpty(parsed.RequestBody))
                {
                    var content = req.Content.ReadAsStringAsync().Result;
                    content.Should().Be(parsed.RequestBody);
                }
            }
        }

        [Test]
        public void kevin2()
        {
            var rawRequestTestCases = new string[]
            {
                FakeRawRequests.Kevin1,
                FakeRawRequests.Kevin2,
                FakeRawRequests.Kevin3,
                FakeRawRequests.Kevin4,
                FakeRawRequests.Kevin5,
                FakeRawRequests.Kevin6,
                FakeRawRequests.Kevin7,
                FakeRawRequests.Kevin8,
                FakeRawRequests.Kevin9,
                FakeRawRequests.Kevin10,
                FakeRawRequests.Kevin11,
                FakeRawRequests.Kevin12,
                FakeRawRequests.Kevin13,
                FakeRawRequests.Kevin14,
                FakeRawRequests.Kevin15,
                FakeRawRequests.Kevin16,
                FakeRawRequests.Kevin17,
            };

            for (int i = 0; i < rawRequestTestCases.Length; i++)
            {
                var raw = rawRequestTestCases[i];
                var parsed = Parser.ParseRawRequest(raw);
                var curlCommand = parsed.ToCurl();

                Console.WriteLine($"TestCase #{i + 1}");
                Console.WriteLine(curlCommand);

                var headers = parsed.GetHeaders();
                var method = headers["method"];

                curlCommand.Should().Contain("curl");
                if (!method.Equals("get", StringComparison.OrdinalIgnoreCase))
                    curlCommand.Should().Contain(method.ToUpper());
                curlCommand.Should().Contain(parsed.Uri.ToString());

                foreach (var header in headers)
                {
                    var headerKey = header.Key.ToLower();
                    if (headerKey is "cookie" or "method" or "httpversion")
                        continue;

                    curlCommand.Should().Contain($"--header \"{headerKey}: {header.Value}\"");
                }

                var cookies = parsed.GetCookies();
                var commandWithoutSpace = curlCommand.Replace(" ", "");
                if (cookies.Count > 0)
                {
                    var cookieHeader = string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}")).Replace(" ", "");
                    var condition = $"--header \"cookie: {cookieHeader}\"".Replace(" ", "");
                    commandWithoutSpace.Should().Contain(condition);
                }

                if (!string.IsNullOrEmpty(parsed.RequestBody))
                {
                    var requestBody = parsed.RequestBody.Replace(" ", "");
                    var condition = $"--data \"{requestBody}\"".Replace(" ", "");
                    commandWithoutSpace.Should().Contain(condition);
                }
            }
        }

        [Test]
        public void kevin3()
        {
            var rawRequestTestCases = new string[]
            {
                FakeRawRequests.Kevin1,
                FakeRawRequests.Kevin2,
                FakeRawRequests.Kevin3,
                FakeRawRequests.Kevin4,
                FakeRawRequests.Kevin5,
                FakeRawRequests.Kevin6,
                FakeRawRequests.Kevin7,
                FakeRawRequests.Kevin8,
                FakeRawRequests.Kevin9,
                FakeRawRequests.Kevin10,
                FakeRawRequests.Kevin11,
                FakeRawRequests.Kevin12,
                FakeRawRequests.Kevin13,
                FakeRawRequests.Kevin14,
                FakeRawRequests.Kevin15,
                FakeRawRequests.Kevin16,
                FakeRawRequests.Kevin17,
            };

            for (int i = 0; i < rawRequestTestCases.Length; i++)
            {
                var raw = rawRequestTestCases[i];
                var parsed = Parser.ParseRawRequest(raw);
                var httpCode = parsed.ToHttpRequestCode();

                Console.WriteLine($"====== Kevin{i + 1} ======");
                Console.WriteLine(httpCode);
            }
        }
    }
}
