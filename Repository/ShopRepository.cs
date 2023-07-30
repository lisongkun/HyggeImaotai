using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using Flurl.Http;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using Microsoft.Data.Sqlite;
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
        /// 清空数据表数据
        /// </summary>
        public static void TruncateTable()
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string truncateTableSql = "DELETE FROM i_shop;";
            using var command = new SqliteCommand(truncateTableSql, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 向表中添加一行数据
        /// </summary>
        /// <param name="storeEntity"></param>
        public static void InsertShop(StoreEntity storeEntity)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"INSERT INTO i_shop (i_shop_id,province_name,city_name,district_name,full_address,lat,lng,name,tenant_name,create_time)" +
                                     "VALUES (@i_shop_id,@province_name,@city_name,@district_name,@full_address,@lat,@lng,@name,@tenant_name,@create_time);";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@i_shop_id", storeEntity.ProductId);
            command.Parameters.AddWithValue("@province_name", storeEntity.Province);
            command.Parameters.AddWithValue("@city_name", storeEntity.City);
            command.Parameters.AddWithValue("@district_name", storeEntity.Area);
            command.Parameters.AddWithValue("@full_address", storeEntity.UnbrokenAddress);
            command.Parameters.AddWithValue("@lat", storeEntity.Lat);
            command.Parameters.AddWithValue("@lng", storeEntity.Lng);
            command.Parameters.AddWithValue("@name", storeEntity.Name);
            command.Parameters.AddWithValue("@tenant_name", storeEntity.CompanyName);
            command.Parameters.AddWithValue("@create_time", storeEntity.CreatedAt);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="storeListViewModel"></param>
        /// <returns></returns>
        public static List<StoreEntity> GetPageData(int page, int pageSize, StoreListViewModel? storeListViewModel = null)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            var insertSql = @"select * from i_shop where 1 = 1 ";
            if (storeListViewModel != null)
            {
                if (!string.IsNullOrEmpty(storeListViewModel.ShopId)) insertSql += "And i_shop_id = @shopId ";
                if (!string.IsNullOrEmpty(storeListViewModel.Province)) insertSql += "And province_name = @provinceName ";
                if (!string.IsNullOrEmpty(storeListViewModel.City)) insertSql += "And city_name = @cityName ";
                if (!string.IsNullOrEmpty(storeListViewModel.Area)) insertSql += "And district_name = @districtName ";
                if (!string.IsNullOrEmpty(storeListViewModel.CompanyName)) insertSql += "And name = @name ";
            }

            insertSql += "limit @pageSize OFFSET @offset";

            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            if (storeListViewModel != null)
            {
                if (!string.IsNullOrEmpty(storeListViewModel.ShopId)) command.Parameters.AddWithValue("@shopId", storeListViewModel.ShopId);
                if (!string.IsNullOrEmpty(storeListViewModel.Province)) command.Parameters.AddWithValue("@provinceName", storeListViewModel.Province);
                if (!string.IsNullOrEmpty(storeListViewModel.City)) command.Parameters.AddWithValue("@cityName", storeListViewModel.City);
                if (!string.IsNullOrEmpty(storeListViewModel.Area)) command.Parameters.AddWithValue("@districtName", storeListViewModel.Area);
                if (!string.IsNullOrEmpty(storeListViewModel.CompanyName)) command.Parameters.AddWithValue("@name", storeListViewModel.CompanyName);
            }
            using var reader = command.ExecuteReader();
            var list = new List<StoreEntity>();
            while (reader.Read())
            {
                var storeEntity = new StoreEntity
                {
                    ProductId = reader.GetString(1),
                    Province = reader.GetString(2),
                    City = reader.GetString(3),
                    Area = reader.GetString(4),
                    UnbrokenAddress = reader.GetString(5),
                    Lat = reader.GetString(6),
                    Lng = reader.GetString(7),
                    Name = reader.GetString(8),
                    CompanyName = reader.GetString(9),
                    CreatedAt = DateTime.Parse(reader.GetString(10))
                };
                list.Add(storeEntity);
            }
            return list;
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <param name="storeListViewModel"></param>
        /// <returns></returns>
        public static int GetTotalCount(StoreListViewModel storeListViewModel)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            var insertSql = @"select count(*) from i_shop where 1 = 1 ";
            if (!string.IsNullOrEmpty(storeListViewModel.ShopId)) insertSql += "And i_shop_id = @shopId ";
            if (!string.IsNullOrEmpty(storeListViewModel.Province)) insertSql += "And province_name = @provinceName ";
            if (!string.IsNullOrEmpty(storeListViewModel.City)) insertSql += "And city_name = @cityName ";
            if (!string.IsNullOrEmpty(storeListViewModel.Area)) insertSql += "And district_name = @districtName ";
            if (!string.IsNullOrEmpty(storeListViewModel.CompanyName)) insertSql += "And name = @name ";


            using var command = new SqliteCommand(insertSql, connection);
            if (!string.IsNullOrEmpty(storeListViewModel.ShopId)) command.Parameters.AddWithValue("@shopId", storeListViewModel.ShopId);
            if (!string.IsNullOrEmpty(storeListViewModel.Province)) command.Parameters.AddWithValue("@provinceName", storeListViewModel.Province);
            if (!string.IsNullOrEmpty(storeListViewModel.City)) command.Parameters.AddWithValue("@cityName", storeListViewModel.City);
            if (!string.IsNullOrEmpty(storeListViewModel.Area)) command.Parameters.AddWithValue("@districtName", storeListViewModel.Area);
            if (!string.IsNullOrEmpty(storeListViewModel.CompanyName)) command.Parameters.AddWithValue("@name", storeListViewModel.CompanyName);

            using var reader = command.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            return count;
        }

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
            var filteredShop = iShops.Where(i => shopIdList.Contains(i.ProductId)).ToList();

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
        private static string GetMinDistanceShopId(List<StoreEntity> filteredShop, string userEntityProvinceName, string userEntityLat, string userEntityLng)
        {
            var iShopList = filteredShop.Where(i => i.Province.Contains(userEntityProvinceName)).ToList();
            var mapPoint = new MapPoint(double.Parse(userEntityLat), double.Parse(userEntityLng));
            foreach (var storeEntity in iShopList)
            {
                var point = new MapPoint(double.Parse(storeEntity.Lat), double.Parse(storeEntity.Lng));
                storeEntity.Distance = GetDistance(mapPoint, point);
            }
            var minDistance = iShopList.Min(i => i.Distance);
            var shopId = iShopList.FirstOrDefault(i => i.Distance == minDistance)?.ProductId;
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
        private static string GetMaxInventoryShopId(List<IMTItemInfo> shopIdList, List<StoreEntity> filteredShop, string userEntityCityName)
        {
            // 本城市的shopId集合
            var cityShopIdList = filteredShop.Where(i => i.City.Contains(userEntityCityName)).Select(i => i.ProductId)
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
        private static List<StoreEntity> GetAllShopList()
        {
            var list = new List<StoreEntity>();
            if (File.Exists(App.StoreListFile))
            {
                list = JsonConvert.DeserializeObject<List<StoreEntity>>(File.ReadAllText(App.StoreListFile));
                if (list.Count == 0)
                    throw new Exception("未获取到可用商店列表,请先尝试刷新商店列表");
                return list;
            }
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string queryAllSql = @"select * from i_shop";
            using var command = new SqliteCommand(queryAllSql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var storeEntity = new StoreEntity
                {
                    ProductId = reader.GetString(1),
                    Province = reader.GetString(2),
                    City = reader.GetString(3),
                    Area = reader.GetString(4),
                    UnbrokenAddress = reader.GetString(5),
                    Lat = reader.GetString(6),
                    Lng = reader.GetString(7),
                    Name = reader.GetString(8),
                    CompanyName = reader.GetString(9),
                    CreatedAt = DateTime.Parse(reader.GetString(10))
                };
                list.Add(storeEntity);
            }
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
                        .GetStringAsync();
            var responseJObject = JObject.Parse(response);
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
