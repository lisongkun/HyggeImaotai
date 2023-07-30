using System;
using hygge_imaotai.Entity;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using hygge_imaotai.Domain;

namespace hygge_imaotai.Repository
{
    /// <summary>
    /// 用户表的操作类
    /// </summary>
    public class UserRepository
    {
        #region Functions

        /// <summary>
        /// 向表中添加一行数据
        /// </summary>
        /// <param name="user"></param>
        public static void InsertUser(UserEntity user)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            // 先根据手机号查询是否存在
            const string querySql = @"SELECT * FROM i_user WHERE mobile_phone=@mobile;";
            using var command = new SqliteCommand(querySql, connection);
            command.Parameters.AddWithValue("@mobile", user.Mobile);
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                throw new Exception("数据已经存在");
            }

            const string insertSql = @"INSERT INTO i_user(mobile_phone,user_id,token,item_code,province_name,city_name,address,lat,lng,shop_type,push_plus_token,json_result,create_time,expire_time)
  Values(@mobile,@user_id,@token,@item_code,@province_name,@city_name,@address,@lat,@lng,@shop_type,@push_plus_token,@json_result,@create_time,@expire_time);";
            using var insertCommand = new SqliteCommand(insertSql, connection);
            insertCommand.Parameters.AddWithValue("@mobile", user.Mobile);
            insertCommand.Parameters.AddWithValue("@user_id", user.UserId);
            insertCommand.Parameters.AddWithValue("@token", user.Token);
            insertCommand.Parameters.AddWithValue("@item_code", user.ItemCode);
            insertCommand.Parameters.AddWithValue("@province_name", user.ProvinceName);
            insertCommand.Parameters.AddWithValue("@city_name", user.CityName);
            insertCommand.Parameters.AddWithValue("@address", user.Address);
            insertCommand.Parameters.AddWithValue("@lat", user.Lat);
            insertCommand.Parameters.AddWithValue("@lng", user.Lng);
            insertCommand.Parameters.AddWithValue("@shop_type", user.ShopType);
            insertCommand.Parameters.AddWithValue("@push_plus_token", user.PushPlusToken);
            insertCommand.Parameters.AddWithValue("@json_result", user.JsonResult);
            insertCommand.Parameters.AddWithValue("@create_time", user.CreateTime);
            insertCommand.Parameters.AddWithValue("@expire_time", user.ExpireTime);
            insertCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalCount(UserManageViewModel viewModel)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            var insertSql = @"select count(*) from i_user where 1 = 1 ";
            if (!string.IsNullOrEmpty(viewModel.Phone)) insertSql += "and mobile_phone like @phone ";
            if (!string.IsNullOrEmpty(viewModel.UserId)) insertSql += "and user_id like @userId ";
            if (!string.IsNullOrEmpty(viewModel.Province)) insertSql += "and province_name like @province ";
            if (!string.IsNullOrEmpty(viewModel.City)) insertSql += "and city_name like @city ";
            using var command = new SqliteCommand(insertSql, connection);
            if (!string.IsNullOrEmpty(viewModel.Phone)) command.Parameters.AddWithValue("@phone", $"%{viewModel.Phone}%");
            if (!string.IsNullOrEmpty(viewModel.UserId)) command.Parameters.AddWithValue("@userId", $"%{viewModel.UserId}%");
            if (!string.IsNullOrEmpty(viewModel.Province)) command.Parameters.AddWithValue("@province", $"%{viewModel.Province}%");
            if (!string.IsNullOrEmpty(viewModel.City)) command.Parameters.AddWithValue("@city", $"%{viewModel.City}%");
            using var reader = command.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            return count;
        }

        /// <summary>
        /// 获取符合条件的可预约用户
        /// </summary>
        /// <returns></returns>
        public static List<UserEntity> GetReservationUser()
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            // 过期时间大于当前时间的 且 Lat 不为空字符串 且 Lng 不为空空字符串 且 ShopType 不为空字符串 且 ItemCode 不为空字符串
            const string insertSql = @"select * from i_user where expire_time>@expire_time and lat!='' and lng!='' and shop_type!='' and item_code!=''";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@expire_time", DateTime.Now);
            using var reader = command.ExecuteReader();
            var list = new List<UserEntity>();
            while (reader.Read())
            {
                var userEntity = new UserEntity()
                {
                    Mobile = reader.GetString(0),
                    UserId = long.Parse(reader.GetString(1)),
                    Token = reader.GetString(2),
                    ItemCode = reader.GetString(3),
                    ProvinceName = reader.GetString(4),
                    CityName = reader.GetString(5),
                    Address = reader.GetString(6),
                    Lat = reader.GetString(7),
                    Lng = reader.GetString(8),
                    ShopType = reader.GetInt32(9),
                    PushPlusToken = reader.GetString(10),
                    JsonResult = reader.GetString(11),
                    CreateTime = reader.GetDateTime(12),
                    ExpireTime = reader.GetDateTime(13)
                };
                list.Add(userEntity);
            }
            return list;
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static List<UserEntity> GetPageData(int page, int pageSize, UserManageViewModel viewModel)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            string insertSql = @"select * from i_user where 1 = 1 ";
            if(!string.IsNullOrEmpty(viewModel.Phone)) insertSql += "and mobile_phone like @phone ";
            if(!string.IsNullOrEmpty(viewModel.UserId)) insertSql += "and user_id like @userId ";
            if(!string.IsNullOrEmpty(viewModel.Province)) insertSql += "and province_name like @province ";
            if(!string.IsNullOrEmpty(viewModel.City)) insertSql += "and city_name like @city ";
            insertSql += "limit @pageSize OFFSET @offset";

            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
            command.Parameters.AddWithValue("@pageSize", pageSize);

            if(!string.IsNullOrEmpty(viewModel.Phone)) command.Parameters.AddWithValue("@phone", $"%{viewModel.Phone}%");
            if(!string.IsNullOrEmpty(viewModel.UserId)) command.Parameters.AddWithValue("@userId", $"%{viewModel.UserId}%");
            if(!string.IsNullOrEmpty(viewModel.Province)) command.Parameters.AddWithValue("@province", $"%{viewModel.Province}%");
            if(!string.IsNullOrEmpty(viewModel.City)) command.Parameters.AddWithValue("@city", $"%{viewModel.City}%");

            using var reader = command.ExecuteReader();
            var list = new List<UserEntity>();
            while (reader.Read())
            {
                var userEntity = new UserEntity()
                {
                    Mobile = reader.GetString(0),
                    UserId = long.Parse(reader.GetString(1)),
                    Token = reader.GetString(2),
                    ItemCode = reader.GetString(3),
                    ProvinceName = reader.GetString(4),
                    CityName = reader.GetString(5),
                    Address = reader.GetString(6),
                    Lat = reader.GetString(7),
                    Lng = reader.GetString(8),
                    ShopType = reader.GetInt32(9),
                    PushPlusToken = reader.GetString(10),
                    JsonResult = reader.GetString(11),
                    CreateTime = reader.GetDateTime(12),
                    ExpireTime = reader.GetDateTime(13)
                };
                list.Add(userEntity);
            }
            return list;
        }

        public static void UpdateUser(UserEntity user)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"UPDATE i_user SET user_id=@user_id,token=@token,item_code=@item_code,province_name=@province_name,city_name=@city_name,address=@address,lat=@lat,lng=@lng,
shop_type=@shop_type,push_plus_token=@push_plus_token,json_result=@json_result,create_time=@create_time,expire_time=@expire_time WHERE mobile_phone=@mobile;";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@mobile", user.Mobile);
            command.Parameters.AddWithValue("@user_id", user.UserId);
            command.Parameters.AddWithValue("@token", user.Token);
            command.Parameters.AddWithValue("@item_code", user.ItemCode);
            command.Parameters.AddWithValue("@province_name", user.ProvinceName);
            command.Parameters.AddWithValue("@city_name", user.CityName);
            command.Parameters.AddWithValue("@address", user.Address);
            command.Parameters.AddWithValue("@lat", user.Lat);
            command.Parameters.AddWithValue("@lng", user.Lng);
            command.Parameters.AddWithValue("@shop_type", user.ShopType);
            command.Parameters.AddWithValue("@push_plus_token", user.PushPlusToken);
            command.Parameters.AddWithValue("@json_result", user.JsonResult);
            command.Parameters.AddWithValue("@create_time", user.CreateTime);
            command.Parameters.AddWithValue("@expire_time", user.ExpireTime);
            command.ExecuteNonQuery();
        }
        public static void Delete(UserEntity parameter)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"delete from i_user where mobile_phone=@mobile;";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@mobile", parameter.Mobile);
            command.ExecuteNonQuery();
        }
        #endregion

    }
}
