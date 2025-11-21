using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyggeIMaoTai.Entity;
using NLog;

namespace HyggeIMaoTai.Repository
{
    /// <summary>
    /// 对日志表的操作
    /// </summary>
    public class LogRepository
    {
        #region Properties
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        /// <summary>
        /// 插入异常日志（异步）
        /// </summary>
        /// <param name="mobilePhone">手机号</param>
        /// <param name="userId">用户ID</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns></returns>
        public static async Task InsertExceptionLogAsync(string mobilePhone, string userId, string errorMessage)
        {
            await DB.Sqlite.Insert<LogEntity>(new LogEntity()
            {
                CreateTime = DateTime.Now,
                MobilePhone = mobilePhone,
                Content = $"[userId]:{userId}",
                Response = errorMessage,
                Status = "异常"
            }).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 插入异常日志（同步）
        /// </summary>
        /// <param name="mobilePhone">手机号</param>
        /// <param name="userId">用户ID</param>
        /// <param name="errorMessage">错误消息</param>
        public static void InsertExceptionLog(string mobilePhone, string userId, string errorMessage)
        {
            DB.Sqlite.Insert(new LogEntity()
            {
                CreateTime = DateTime.Now,
                MobilePhone = mobilePhone,
                Content = $"[userId]:{userId}",
                Response = errorMessage,
                Status = "异常"
            }).ExecuteAffrows();
        }

        /// <summary>
        /// 插入预约结果日志（异步）
        /// </summary>
        /// <param name="mobilePhone">手机号</param>
        /// <param name="userId">用户ID</param>
        /// <param name="itemId">商品ID</param>
        /// <param name="responseString">响应内容</param>
        /// <param name="isSuccess">是否成功</param>
        /// <returns></returns>
        public static async Task InsertReservationLogAsync(string mobilePhone, string userId, string itemId, string responseString, bool isSuccess)
        {
            await DB.Sqlite.Insert<LogEntity>(new LogEntity()
            {
                CreateTime = DateTime.Now,
                MobilePhone = mobilePhone,
                Content = $"[userId]:{userId} [shopId]:{itemId}",
                Response = responseString,
                Status = isSuccess ? "预约成功" : "预约失败"
            }).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 插入预约失败日志（异步）
        /// </summary>
        /// <param name="mobilePhone">手机号</param>
        /// <param name="userId">用户ID</param>
        /// <param name="itemId">商品ID</param>
        /// <param name="responseString">响应内容</param>
        /// <returns></returns>
        public static async Task InsertReservationFailureLogAsync(string mobilePhone, string userId, string itemId, string responseString)
        {
            var logEntity = new LogEntity()
            {
                CreateTime = DateTime.Now,
                MobilePhone = mobilePhone,
                Content = $"[userId]:{userId} [shopId]:{itemId}",
                Response = responseString,
                Status = "预约失败"
            };

            await DB.Sqlite.Insert<LogEntity>(logEntity).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 查询日志列表（带分页和条件筛选）
        /// </summary>
        /// <param name="mobile">手机号（可选）</param>
        /// <param name="searchContent">搜索内容（可选）</param>
        /// <param name="status">状态（可选）</param>
        /// <param name="pageIndex">页码，从1开始，默认为1</param>
        /// <param name="pageSize">每页数量，默认为10</param>
        /// <returns>元组：(日志列表, 总记录数)</returns>
        public static (List<LogEntity> logs, long total) GetLogList(string mobile = null, string searchContent = null, string status = null, int pageIndex = 1, int pageSize = 10)
        {
            var query = DB.Sqlite.Select<LogEntity>();
            
            if (!string.IsNullOrEmpty(mobile))
            {
                query = query.Where(i => i.MobilePhone.Contains(mobile));
            }
            
            if (!string.IsNullOrEmpty(searchContent))
            {
                query = query.Where(i => i.Content.Contains(searchContent));
            }
            
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(i => i.Status.Contains(status));
            }
            
            var result = query.Count(out var total)
                .Page(pageIndex, pageSize)
                .ToList();
            
            return (result, total);
        }
    }
}

