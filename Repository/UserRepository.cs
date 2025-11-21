using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyggeIMaoTai.Entity;
using NLog;

namespace HyggeIMaoTai.Repository
{
    /// <summary>
    /// 对用户表的操作
    /// </summary>
    public class UserRepository
    {
        #region Properties
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        /// <summary>
        /// 插入用户（同步）
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>影响的行数</returns>
        public static int InsertUser(UserEntity userEntity)
        {
            return DB.Sqlite.Insert(userEntity).ExecuteAffrows();
        }

        /// <summary>
        /// 插入用户（异步）
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>影响的行数</returns>
        public static async Task<int> InsertUserAsync(UserEntity userEntity)
        {
            return await DB.Sqlite.Insert(userEntity).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 更新用户信息（根据手机号）
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>影响的行数</returns>
        public static int UpdateUser(UserEntity userEntity)
        {
            return DB.Sqlite.Update<UserEntity>()
                .Set(i => i.UserId, userEntity.UserId)
                .Set(i => i.Token, userEntity.Token)
                .Set(i => i.ItemCode, userEntity.ItemCode)
                .Set(i => i.ProvinceName, userEntity.ProvinceName)
                .Set(i => i.CityName, userEntity.CityName)
                .Set(i => i.Address, userEntity.Address)
                .Set(i => i.Lat, userEntity.Lat)
                .Set(i => i.Lng, userEntity.Lng)
                .Set(i => i.ShopType, userEntity.ShopType)
                .Set(i => i.PushPlusToken, userEntity.PushPlusToken)
                .Set(i => i.JsonResult, userEntity.JsonResult)
                .Set(i => i.CreateTime, userEntity.CreateTime)
                .Set(i => i.ExpireTime, userEntity.ExpireTime)
                .Where(i => i.Mobile == userEntity.Mobile)
                .ExecuteAffrows();
        }

        /// <summary>
        /// 更新用户信息（根据手机号，异步）
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>影响的行数</returns>
        public static async Task<int> UpdateUserAsync(UserEntity userEntity)
        {
            return await DB.Sqlite.Update<UserEntity>()
                .Set(i => i.UserId, userEntity.UserId)
                .Set(i => i.Token, userEntity.Token)
                .Set(i => i.ItemCode, userEntity.ItemCode)
                .Set(i => i.ProvinceName, userEntity.ProvinceName)
                .Set(i => i.CityName, userEntity.CityName)
                .Set(i => i.Address, userEntity.Address)
                .Set(i => i.Lat, userEntity.Lat)
                .Set(i => i.Lng, userEntity.Lng)
                .Set(i => i.ShopType, userEntity.ShopType)
                .Set(i => i.PushPlusToken, userEntity.PushPlusToken)
                .Set(i => i.JsonResult, userEntity.JsonResult)
                .Set(i => i.CreateTime, userEntity.CreateTime)
                .Set(i => i.ExpireTime, userEntity.ExpireTime)
                .Where(i => i.Mobile == userEntity.Mobile)
                .ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 删除用户（根据手机号）
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>影响的行数</returns>
        public static int DeleteUser(string mobile)
        {
            return DB.Sqlite.Delete<UserEntity>()
                .Where(i => i.Mobile == mobile)
                .ExecuteAffrows();
        }

        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>用户实体，如果不存在则返回 null</returns>
        public static UserEntity GetUserByMobile(string mobile)
        {
            return DB.Sqlite.Select<UserEntity>()
                .Where(i => i.Mobile == mobile)
                .First();
        }

        /// <summary>
        /// 查询用户列表（带分页和条件筛选）
        /// </summary>
        /// <param name="phone">手机号（可选）</param>
        /// <param name="userId">用户ID（可选）</param>
        /// <param name="province">省份（可选）</param>
        /// <param name="city">城市（可选）</param>
        /// <param name="pageIndex">页码，从1开始，默认为1</param>
        /// <param name="pageSize">每页数量，默认为10</param>
        /// <returns>元组：(用户列表, 总记录数)</returns>
        public static (List<UserEntity> users, long total) GetUserList(
            string phone = null, 
            string userId = null, 
            string province = null, 
            string city = null, 
            int pageIndex = 1, 
            int pageSize = 10)
        {
            var query = DB.Sqlite.Select<UserEntity>();

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(i => i.Mobile.Contains(phone));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(i => (i.UserId + "").Contains(userId));
            }

            if (!string.IsNullOrEmpty(province))
            {
                query = query.Where(i => i.ProvinceName.Contains(province));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(i => i.CityName.Contains(city));
            }

            var result = query.Count(out var total)
                .Page(pageIndex, pageSize)
                .ToList();

            return (result, total);
        }

        /// <summary>
        /// 获取所有有效的用户（用于批量预约）
        /// </summary>
        /// <returns>用户列表</returns>
        public static List<UserEntity> GetValidUsersForReservation()
        {
            return DB.Sqlite.Select<UserEntity>()
                .Where(i => i.ExpireTime > DateTime.Now 
                    && !string.IsNullOrEmpty(i.Lat) 
                    && !string.IsNullOrEmpty(i.Lng) 
                    && !string.IsNullOrEmpty(i.ShopType + "") 
                    && !string.IsNullOrEmpty(i.ItemCode))
                .ToList();
        }
    }
}

