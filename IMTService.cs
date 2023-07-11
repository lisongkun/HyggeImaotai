using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai
{
    public class IMTService
    {
        private static string SALT = "2af72f100c356273d46284f6fd1dfc08";
        private static string _mtVersion = "";

        private static string Signature(string content, long timestamp)
        {
            var text = SALT + content + timestamp;
            using var md = MD5.Create();
            var hashBytes = md.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append($"{b:X2}");
            }
            return sb.ToString().ToLower();
        }

        private async Task<string> GetMtVersion()
        {
            if (!string.IsNullOrEmpty(_mtVersion)) return _mtVersion;
            var htmlSource = await "https://apps.apple.com/cn/app/i%E8%8C%85%E5%8F%B0/id1600482450"
                .GetStringAsync();
            var pattern = new Regex(@"new__latest__version\"">(.*?)<\/p>", RegexOptions.Singleline);
            var matcher = pattern.Match(htmlSource);
            var replace = "";
            if (!matcher.Success) return replace;
            var mtVersion = matcher.Groups[1].Value;
            replace = mtVersion.Replace("版本 ", "");
            _mtVersion = replace;
            return replace;
        }

        public async Task<bool> SendCode(string phone)
        {
            var client = new HttpClient();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var values = new Dictionary<string, string>
        {
            { "mobile", phone },
            { "md5", Signature(phone,timestamp) },
            {"timestamp",timestamp+""}
        };
            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "iOS;16.3;Apple;?unrecognized?");
            client.DefaultRequestHeaders.Add("MT-Lat", "28.499562");
            client.DefaultRequestHeaders.Add("MT-K", "1675213490331");
            client.DefaultRequestHeaders.Add("MT-Lng", "102.182324");
            client.DefaultRequestHeaders.Add("Host", "app.moutai519.com.cn");
            client.DefaultRequestHeaders.Add("MT-User-Tag", "0");
            client.DefaultRequestHeaders.Add("MT-Network-Type", "WIFI");
            client.DefaultRequestHeaders.TryAddWithoutValidation("MT-Team-ID", "");
            client.DefaultRequestHeaders.Add("MT-Info", "028e7f96f6369cafe1d105579c5b9377");
            client.DefaultRequestHeaders.Add("MT-Device-ID", "2F2075D0-B66C-4287-A903-DBFF6358342A");
            client.DefaultRequestHeaders.Add("MT-Bundle-ID", "com.moutai.mall");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-CN;q=1, zh-Hans-CN;q=0.9");
            client.DefaultRequestHeaders.Add("MT-Request-ID", "167560018873318465");
            client.DefaultRequestHeaders.Add("MT-APP-Version", await GetMtVersion());
            client.DefaultRequestHeaders.Add("MT-R", "clips_OlU6TmFRag5rCXwbNAQ/Tz1SKlN8THcecBp/HGhHdw==");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "93");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var response = await client
                .PostAsync("https://app.moutai519.com.cn/xhr/front/user/register/vcode", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var code = (string)responseJson["code"];
            if (code == "2000") return true;
            throw new Exception(responseString);
        }

        public async Task<bool> Login(string phone, string code)
        {
            var client = new HttpClient();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var values = new Dictionary<string, string>
            {
                { "mobile", phone },
                { "ydToken", "" },
                {"vCode",code},
                {"ydLogId",""},
                {"md5",Signature(phone + code + "" + "",timestamp)},
                {"timestamp",timestamp+""},
                {"MT-APP-Version",await GetMtVersion()}
            };
            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "iOS;16.3;Apple;?unrecognized?");
            client.DefaultRequestHeaders.Add("MT-Lat", "28.499562");
            client.DefaultRequestHeaders.Add("MT-K", "1675213490331");
            client.DefaultRequestHeaders.Add("MT-Lng", "102.182324");
            client.DefaultRequestHeaders.Add("Host", "app.moutai519.com.cn");
            client.DefaultRequestHeaders.Add("MT-User-Tag", "0");
            client.DefaultRequestHeaders.Add("MT-Network-Type", "WIFI");
            client.DefaultRequestHeaders.TryAddWithoutValidation("MT-Team-ID", "");
            client.DefaultRequestHeaders.Add("MT-Info", "028e7f96f6369cafe1d105579c5b9377");
            client.DefaultRequestHeaders.Add("MT-Device-ID", "2F2075D0-B66C-4287-A903-DBFF6358342A");
            client.DefaultRequestHeaders.Add("MT-Bundle-ID", "com.moutai.mall");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-CN;q=1, zh-Hans-CN;q=0.9");
            client.DefaultRequestHeaders.Add("MT-Request-ID", "167560018873318465");
            client.DefaultRequestHeaders.Add("MT-APP-Version", await GetMtVersion());
            client.DefaultRequestHeaders.Add("MT-R", "clips_OlU6TmFRag5rCXwbNAQ/Tz1SKlN8THcecBp/HGhHdw==");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "93");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var response = await client
                .PostAsync("https://app.moutai519.com.cn/xhr/front/user/register/login", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var responseCode = (string)responseJson["code"];
            if (responseCode != "2000") throw new Exception(responseString);
            // 存储一下数据
            var foundUserEntity = FieldsViewModel.SearchResult.FirstOrDefault(user => user.Mobile == phone);
            if (foundUserEntity != null)
            {
                FieldsViewModel.SearchResult[FieldsViewModel.SearchResult.IndexOf(foundUserEntity)] = new UserEntity(phone, responseJson);
            }
            else
            {
                FieldsViewModel.SearchResult.Add(new UserEntity(phone,responseJson));
            }
            return true;
        }

    }
}
