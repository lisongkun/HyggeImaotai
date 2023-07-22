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
using hygge_imaotai.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace hygge_imaotai
{
    public class IMTService
    {
        private static string SALT = "2af72f100c356273d46284f6fd1dfc08";
        private static string _mtVersion = "";
        private static string AES_KEY = "qbhajinldepmucsonaaaccgypwuvcjaa";
        private static string AES_IV = "2018534749963515";

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

        private static async Task<string> GetMtVersion()
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
                FieldsViewModel.SearchResult.Add(new UserEntity(phone, responseJson));
            }
            return true;
        }

        /// <summary>
        /// 预约方法
        /// </summary>
        /// <param name="userEntity"></param>
        public static async void Reservation(UserEntity userEntity)
        {
            var items = userEntity.ItemCode.Split("@");
            foreach (var item in items)
            {
                var shopId = await ShopRepository.GetShopId(userEntity.ShopType, item, userEntity.ProvinceName, userEntity.CityName, userEntity.Lat, userEntity.Lng);
                if (!string.IsNullOrEmpty(shopId))
                {
                    await Reservation(userEntity, item, shopId);
                }
            }
        }

        public static async Task<JObject> Reservation(UserEntity user, string itemId, string shopId)
        {
            var client = new HttpClient();


            var info = new Dictionary<string, object>
            {
                { "count", 1 },
                { "itemId", itemId }
            };

            var values = new Dictionary<string, string>
            {
                { "itemInfoList", JsonConvert.SerializeObject(new List<Dictionary<string,object>>(){info}) },
                { "sessionId",await GetCurrentSessionId() },
                {"userId",user.UserId + ""},
                {"shopId",shopId}
            };
            values.Add("actParam", AesEncrypt(JsonConvert.SerializeObject(values)));

            client.DefaultRequestHeaders.Add("MT-Lat", user.Lat);
            client.DefaultRequestHeaders.Add("MT-K", "1675213490331");
            client.DefaultRequestHeaders.Add("MT-Lng", user.Lng);
            client.DefaultRequestHeaders.Add("Host", "app.moutai519.com.cn");
            client.DefaultRequestHeaders.Add("MT-User-Tag", "0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Add("MT-Network-Type", "WIFI");
            client.DefaultRequestHeaders.TryAddWithoutValidation("MT-Token", user.Token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("MT-Team-ID", "");
            client.DefaultRequestHeaders.Add("MT-Info", "028e7f96f6369cafe1d105579c5b9377");
            client.DefaultRequestHeaders.Add("MT-Device-ID", "2F2075D0-B66C-4287-A903-DBFF6358342A");
            client.DefaultRequestHeaders.Add("MT-Bundle-ID", "com.moutai.mall");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-CN;q=1, zh-Hans-CN;q=0.9");
            client.DefaultRequestHeaders.Add("MT-Request-ID", "167560018873318465");
            client.DefaultRequestHeaders.Add("MT-APP-Version", await GetMtVersion());
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "iOS;16.3;Apple;?unrecognized?");
            client.DefaultRequestHeaders.Add("MT-R", "clips_OlU6TmFRag5rCXwbNAQ/Tz1SKlN8THcecBp/HGhHdw==");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "93");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("userId", user.UserId + "");

            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");

            var response = await client
                .PostAsync("https://app.moutai519.com.cn/xhr/front/mall/reservation/add", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            return responseJson;
        }

        private static string AesEncrypt(string serializeObject)
        {
            var temp = EncryptAES_CBC(serializeObject);
            return temp;
        }

        static string EncryptAES_CBC(string input)
        {
            byte[] key = Encoding.UTF8.GetBytes(AES_KEY);
            byte[] iv = Encoding.UTF8.GetBytes(AES_IV);

            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                string base64String = Convert.ToBase64String(encryptedBytes);
                return base64String;
            }
        }

        /// <summary>
        /// 获取当前会话的专属ID,并更新商品列表数据
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetCurrentSessionId()
        {
            var mtSessionId = App.MtSessionId;
            if (!string.IsNullOrEmpty(mtSessionId)) return mtSessionId;

            var midNight = DateTime.Now.Date;
            var epochStart = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.FromHours(8));
            var timeSpan = midNight.AddHours(-8) - epochStart;
            var milliseconds = (long)timeSpan.TotalMilliseconds;

            var responseStr = await ("https://static.moutai519.com.cn/mt-backend/xhr/front/mall/index/session/get/" +
                                    milliseconds)
                .GetStringAsync();
            var jObject = JObject.Parse(responseStr);
            if (jObject.GetValue("code").Value<int>() == 2000)
            {
                var dataJObject = jObject["data"];
                App.MtSessionId = dataJObject["sessionId"].Value<string>();
                var itemList = (JArray)dataJObject["itemList"];
                foreach (var itemElement in itemList)
                {
                    AppointProjectViewModel.ProductList.Add(new ProductEntity(itemElement["itemCode"].Value<string>(),
                        itemElement["title"].Value<string>(),
                        itemElement["content"].Value<string>(),
                        itemElement["picture"].Value<string>(), DateTime.Now));
                }
                App.WriteCache("productList.json", JsonConvert.SerializeObject(AppointProjectViewModel.ProductList));
                App.WriteCache("mtSessionId.txt", App.MtSessionId);
            }

            return mtSessionId;
        }
    }
}
