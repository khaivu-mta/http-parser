namespace Tests.FakeData
{
    public class FakeRawRequests
    {
        public const string GetWithoutQueryString = @"GET https://httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Moz23v4234t234tg23g4234fri/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";

        public const string Kevin1 = @"POST https://vik-game.moonactive.net/api/v1/users/ror_nvbnbvvnv/store HTTP/2.0
content-type: application/x-www-form-urlencoded
x-unity-version: 2021.3.21f1
accept: application/json
authorization: Bearer eyJhbbbbbbbbbbbbbbbbbbbbbbbbbbm4Ag
x-client-version: 3.5.2023
accept-encoding: gzip, deflate, br
accept-language: vi-VN,vi;q=0.9
content-length: 146
user-agent: CoinMaster/2r3123r213rwin/24.2.0
x-platform: iOS
cookie: _cfuvid=fafaf_________gagag-23r123r-0.0.1.1-604800000
cookie: cme=d14_45
cookie: gw-destination=go

Device%5budid%5d=586_____________3d2&API_KEY=viki&API_SECRET=coin&Device%5bchange%5d=20250414_7&fbToken=&locale=vi&saleId=default";

        public const string Kevin2 = @"POST https://vik-game.moonactive.net/api/v2/friends/users/ror_nvbnbvvnv HTTP/2.0
content-type: application/x-www-form-urlencoded
x-unity-version: 2021.3.21f1
accept: application/json
authorization: Bearer eyJhbbbbbbbbbbbbbbbbbbbbbbbbbbm4Ag
x-client-version: 3.5.2023
accept-encoding: gzip, deflate, br
accept-language: vi-VN,vi;q=0.9
content-length: 213
user-agent: CoinMaster/r23r1 CFNetwork/123r12r Darwin/24.2.0
x-platform: iOS
cookie: _cfuvid=fafaf_________gagag-1744649249847-0.0.1.1-604800000
cookie: cme=d14_45
cookie: gw-destination=go

Device%5budid%5d=586_____________3d2&API_KEY=viki&API_SECRET=coin&Device%5bchange%5d=20250414_7&fbToken=fafaf_________gabb&locale=vi&non_players=500&p=fb&snfb=true&providers=%5b%22facebook%22%5d";

        public const string Kevin3 = @"POST https://vik-game.moonactive.net/api/v2/users/ror_nvbnbvvnv/fullData HTTP/2
Host: vik-game.moonactive.net
Cookie: __cf_bm=mnmmnmnmnxiOw; _cfuvid=Rs.mndsfmdsnf-fs-0.0.1.1-fsf; cme=global
Content-Type: application/x-www-form-urlencoded
X-Unity-Version: 2021.3.21f1
Accept: application/json
Authorization: Bearer eyJhbbbbbbbbbbbbbbbbbbbbbbbbbbm4Ag
X-Client-Version: 3.5.1830
Accept-Encoding: gzip, deflate, br
Accept-Language: vi-VN,vi;q=0.9
Content-Length: 265
User-Agent: CoinMaster/q2fe23 CFNetwork/123r123r/22.2.0
X-Platform: iOS

Device%5budid%5d=586_____________3d2&API_KEY=viki&API_SECRET=coin&Device%5bchange%5d=20241116_3&fbToken=fafaf_________gabb&locale=vi&Device%5bos%5d=iOS&Client%5bversion%5d=3.5.1830&extended=true&config=all&segmented=true&device_model=iPhone13%2c2";

        public const string Kevin4 = @"POST https://example.com/api/token HTTP/1.1
content-type: application/x-www-form-urlencoded
authorization: Bearer abc123
cookie: sessionid=xyz789

grant_type=client_credentials";

        public const string Kevin5 = @"POST https://example.com/api/token HTTP/1.1
content-type: application/x-www-form-urlencoded

grant_type=password&username=abc&password=123";

        public const string Kevin6 = @"POST https://example.com/api/token HTTP/1.1
content-type: application/x-www-form-urlencoded
cookie: c1=v1
cookie: c2=v2

grant_type=client_credentials";

        public const string Kevin7 = @"POST https://example.com/user/create HTTP/1.1
content-type: application/json
authorization: Bearer token123

{""name"":""Kevin""}";

        public const string Kevin8 = @"POST https://example.com/user/create HTTP/1.1
content-type: application/json
authorization: Bearer token123";

        public const string Kevin9 = @"POST https://example.com/user/create HTTP/1.1
content-type: application/json
authorization: Bearer token123

{""name"":""Kevin"",""email"":""k@v.com""}";

        public const string Kevin10 = @"POST https://example.com/upload HTTP/1.1
content-type: multipart/form-data; boundary=----WebKitFormBoundaryabc123

------WebKitFormBoundaryabc123
Content-Disposition: form-data; name=""file""; filename=""test.txt""
Content-Type: text/plain

content of the file
------WebKitFormBoundaryabc123--";

        public const string Kevin11 = @"GET https://example.com/api/user?id=123 HTTP/1.1";
        public const string Kevin12 = @"PUT https://example.com/api/user/123 HTTP/1.1
content-type: application/json
authorization: Bearer puttoken

{""email"":""kevin@example.com""}";
        public const string Kevin13 = @"PUT https://example.com/api/user/123 HTTP/1.1
content-type: application/json";
        public const string Kevin14 = @"PUT https://example.com/api/user/123 HTTP/1.1
content-type: application/json

{""email"":""kevin@example.com"",""active"":true}";
        public const string Kevin15 = @"DELETE https://example.com/api/user/123 HTTP/1.1
authorization: Bearer deletetoken
x-request-id: 789xyz";
        public const string Kevin16 = @"PATCH https://example.com/api/user/123 HTTP/1.1
content-type: application/json

{""status"":""inactive""}";
        public const string Kevin17 = @"PATCH https://example.com/api/user/123 HTTP/1.1
content-type: application/json

{""status"":""inactive"",""updated_by"":""admin""}";


        public const string GetWithQueryString = @"GET https://httpbin.org/get?name=ryan HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";

        public const string PostWithRequestBody = @"POST https://httpbin.org/post HTTP/1.1
Host: httpbin.org
User-Agent: curl/7.54.1
Accept: */*
Content-Type: application/x-www-form-urlencoded
Cookie: ilikecookies=chocchip

helloworld";

        public const string BadlyFormattedRequest1 = @"GET www.httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";

        public const string BadlyFormattedRequest2 = @"POST https://httpbin.org/post HTTP/1.1
Host: httpbin.org
User-Agent: curl/7.54.1
Accept: */*
Content-Type: application/x-www-form-urlencoded
Cookie: ilikecookies=chocchip

helloworld

";

        public const string RequestWithCookiesInTheWrongSpot = @"POST http://www.providerlookuponline.com/Coventry/po7/Client_FacetWebService.asmx/FillStateFacet HTTP/1.1
Accept: application/json, text/javascript, */*; q=0.01
Origin: http://www.providerlookuponline.com
X-Requested-With: XMLHttpRequest
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36
Content-Type: application/json; charset=UTF-8
Referer: http://www.providerlookuponline.com/coventry/po7/Search.aspx
Accept-Encoding: gzip, deflate
Host: www.providerlookuponline.com
Cookie: ASP.NET_SessionId=gegwregwergwegrewgwerg
Content-Length: 23

#{{RequestBody}}";
    }
}
