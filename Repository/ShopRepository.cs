using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using hygge_imaotai.Entity;
using Microsoft.Data.Sqlite;

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
        /// <returns></returns>
        public static List<StoreEntity> GetPageData(int page, int pageSize)
        {
                        using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"select * from i_shop limit @page,@pageSize";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@page", page);
            command.Parameters.AddWithValue("@pageSize", pageSize);
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
        /// <returns></returns>
        public static int GetTotalCount()
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"select count(*) from i_shop";
            using var command = new SqliteCommand(insertSql, connection);
            using var reader = command.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            return count;
        }

    }
}
