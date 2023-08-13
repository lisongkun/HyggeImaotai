using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai.Repository
{
    /// <summary>
    /// 对i_shop表的操作
    /// </summary>
    public class ShopRepository
    {
        

        /// <summary>
        /// 获取预约的店铺id
        /// </summary>
        /// <param name="userEntityShopType"></param>
        /// <param name="item"></param>
        /// <param name="userEntityProvinceName"></param>
        /// <param name="userEntityCityName"></param>
        /// <param name="userEntityLat"></param>
        /// <param name="userEntityLng"></param>
        /// <returns></returns>
        public static async Task<string> GetShopId(int userEntityShopType, string item, string userEntityProvinceName, string userEntityCityName, string userEntityLat, string userEntityLng)
        {
            var shopList = await GetShopsByProvince(userEntityProvinceName, item);
            var shopIdList = shopList.Select(i => i.ShopId).ToList();
            var iShops = GetAllShopList();
            // 获取今日的门店信息列表
            var filteredShop = iShops.Where(i => shopIdList.Contains(i.ShopId)).ToList();

            var shopId = "";
            if (userEntityShopType == 1)
            {
                // 预约本市出货量最大的门店
                shopId = GetMaxInventoryShopId(shopList, filteredShop, userEntityCityName);
                if (string.IsNullOrEmpty(shopId))
                {
                    // 本市没有则预约本省最近的
                    shopId = GetMinDistanceShopId(filteredShop, userEntityProvinceName, userEntityLat, userEntityLng);
                }
            }

            if (userEntityShopType == 2)
            {
                // 预约本省距离最近的门店
                shopId = GetMinDistanceShopId(filteredShop, userEntityProvinceName, userEntityLat, userEntityLng);
            }
            return shopId;
        }

        /// <summary>
        /// 预约本省距离最近的门店
        /// </summary>
        /// <param name="filteredShop"></param>
        /// <param name="userEntityProvinceName"></param>
        /// <param name="userEntityLat"></param>
        /// <param name="userEntityLng"></param>
        /// <returns></returns>
        private static string GetMinDistanceShopId(List<ShopEntity> filteredShop, string userEntityProvinceName, string userEntityLat, string userEntityLng)
        {
            var iShopList = filteredShop.Where(i => i.Province.Contains(userEntityProvinceName)).ToList();
            var mapPoint = new MapPoint(double.Parse(userEntityLat), double.Parse(userEntityLng));
            foreach (var storeEntity in iShopList)
            {
                var point = new MapPoint(double.Parse(storeEntity.Lat), double.Parse(storeEntity.Lng));
                storeEntity.Distance = GetDistance(mapPoint, point);
            }
            var minDistance = iShopList.Min(i => i.Distance);
            var shopId = iShopList.FirstOrDefault(i => i.Distance == minDistance)?.ShopId;
            return shopId;
        }

        /// <summary>
        /// 获取两个点之间的距离
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double GetDistance(MapPoint point1, MapPoint point2)
        {
            var lat1 = (point1.Latitude * Math.PI) / 180; //将角度换算为弧度
            var lat2 = (point2.Latitude * Math.PI) / 180; //将角度换算为弧度
            var latDifference = lat1 - lat2;
            var lonDifference = (point1.Longitude * Math.PI) / 180 - (point2.Longitude * Math.PI) / 180;
            //计算两点之间距离   6378137.0 取自WGS84标准参考椭球中的地球长半径(单位:m)
            return 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(latDifference / 2), 2)
                                           + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(lonDifference / 2), 2))) * 6378137.0;
        }

        /// <summary>
        /// 预约本城市出货量最大的门店
        /// </summary>
        /// <param name="shopIdList"></param>
        /// <param name="filteredShop"></param>
        /// <param name="userEntityCityName"></param>
        /// <returns></returns>
        private static string GetMaxInventoryShopId(List<IMTItemInfo> shopIdList, List<ShopEntity> filteredShop, string userEntityCityName)
        {
            // 本城市的shopId集合
            var cityShopIdList = filteredShop.Where(i => i.City.Contains(userEntityCityName)).Select(i => i.ShopId)
                .ToList();

            var collect = shopIdList.Where(i => cityShopIdList.Contains(i.ShopId)).ToList();
            collect.Sort((a, b) => b.Inventory.CompareTo(a.Inventory));
            if (collect.Count > 0) return collect[0].ShopId;
            return null;
        }

        /// <summary>
        /// 获取所有StoreEntity
        /// </summary>
        /// <returns></returns>
        private static List<ShopEntity> GetAllShopList()
        {
            var list = new List<ShopEntity>();
            if (File.Exists(App.StoreListFile))
            {
                list = JsonConvert.DeserializeObject<List<ShopEntity>>(File.ReadAllText(App.StoreListFile));
                if (list.Count == 0)
                    throw new Exception("未获取到可用商店列表,请先尝试刷新商店列表");
                return list;
            }

            list = DB.Sqlite.Select<ShopEntity>().ToList();
            if(list.Count != 0)
                File.WriteAllText(App.StoreListFile, JsonConvert.SerializeObject(list));
            if (list.Count == 0)
                throw new Exception("未获取到可用商店列表,请先尝试刷新商店列表");
            return list;
        }


        /// <summary>
        /// 获取当前省份下所有店铺中出售指定产品的店铺
        /// </summary>
        /// <param name="province">省份</param>
        /// <param name="itemId">产品id</param>
        /// <returns></returns>
        public static async Task<List<IMTItemInfo>?> GetShopsByProvince(string province, string itemId)
        {
            var midNight = DateTime.Now.Date;
            var epochStart = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.FromHours(8));
            var timeSpan = midNight.AddHours(-8) - epochStart;
            var milliseconds = (long)timeSpan.TotalMilliseconds;


            var list = new List<IMTItemInfo>();

            var requestUrl =
                $"https://static.moutai519.com.cn/mt-backend/xhr/front/mall/shop/list/slim/v3/{await IMTService.GetCurrentSessionId()}/{province}/{itemId}/{milliseconds}";
            var response =
                await
                    requestUrl
                        .AllowAnyHttpStatus()
                        .GetAsync();
            if (response.StatusCode == 404)
            {
                throw new Exception("本次抢购会话已过期,请手动刷新一下商品列表和店铺列表后重试");
            }

            var responseText = await response.GetStringAsync();

            var responseJObject = JObject.Parse(responseText);
            if (responseJObject["code"].Value<int>() != 2000)
            {
                Console.WriteLine($"查询所在省市的投放产品和数量error,{province}-{itemId}");
                return null;
            }

            var data = responseJObject["data"].Value<JObject>();
            var shopList = data["shops"].Value<JArray>();
            foreach (var shop in shopList)
            {
                var shops = shop.Value<JObject>();
                var items = shops["items"].Value<JArray>();
                foreach (var item in items)
                {
                    var itemObj = item.Value<JObject>();
                    if (itemObj["itemId"].Value<string>() == itemId)
                    {
                        list.Add(new IMTItemInfo(
                            shops["shopId"].Value<string>(),
                            itemObj["count"].Value<int>(),
                            itemObj["itemId"].Value<string>(),
                            itemObj["inventory"].Value<int>()
                        ));
                    }
                }
            }

            return list;
        }
    }
}
