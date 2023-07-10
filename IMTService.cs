using Flurl.Http;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hygge_imaotai
{
    public class IMTService
    {
        private static string SALT = "2af72f100c356273d46284f6fd1dfc08";
        private static string Signature(string content)
        {
            var text = SALT + content + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var md5 = "";
            using var md = MD5.Create();
            var hashBytes = md.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            md5 = sb.ToString();
            return md5;
        }

        private async Task<string> GetMtVersion()
        {
            var htmlSource = await "https://apps.apple.com/cn/app/i%E8%8C%85%E5%8F%B0/id1600482450"
                .GetStringAsync();
            var pattern = new Regex(@"new__latest__version\"">(.*?)<\/p>", RegexOptions.Singleline);
            var matcher = pattern.Match(htmlSource);
            var replace = "";
            if (!matcher.Success) return replace;
            var mtVersion = matcher.Groups[1].Value;
            replace = mtVersion.Replace("版本 ", "");

            return replace;
        }

        private async Task SendCode(string phone)
        {
            var responseJsonAsync = await "https://app.moutai519.com.cn/xhr/front/user/register/vcode".WithHeader("MT-Lat", "28.499562")
                .WithHeader("MT-K", "1675213490331")
                .WithHeader("MT-Lng", "102.182324")
                .WithHeader("Host", "app.moutai519.com.cn")
                .WithHeader("MT-User-Tag", "0")
                .WithHeader("Accept", "*/*")
                .WithHeader("MT-Network-Type", "WIFI")
                // .WithHeader("MT-Token", "2")
                .WithHeader("MT-Team-ID", "")
                .WithHeader("MT-Info", "028e7f96f6369cafe1d105579c5b9377")
                .WithHeader("MT-Device-ID", "2F2075D0-B66C-4287-A903-DBFF6358342A")
                .WithHeader("MT-Bundle-ID", "com.moutai.mall")
                .WithHeader("Accept-Language", "en-CN;q=1, zh-Hans-CN;q=0.9")
                .WithHeader("MT-Request-ID", "167560018873318465")
                .WithHeader("MT-APP-Version", await GetMtVersion())
                .WithHeader("User-Agent", "iOS;16.3;Apple;?unrecognized?")
                .WithHeader("MT-R", "clips_OlU6TmFRag5rCXwbNAQ/Tz1SKlN8THcecBp/HGhHdw==")
                .WithHeader("Content-Length", "93")
                .WithHeader("Accept-Encoding", "gzip, deflate, br")
                .WithHeader("Connection", "keep-alive")
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(new
                {
                    mobile = phone,
                    md5 = Signature(phone),
                    timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                });
        }

    }
}
