using hygge_imaotai.Entity;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hygge_imaotai.Repository
{
    /// <summary>
    /// 日志表的相关操作
    /// </summary>
    public class LogRepository
    {
        #region Functions

        /// <summary>
        /// 向表中添加一行数据
        /// </summary>
        /// <param name="logEntity"></param>
        public static void InsertLog(LogEntity logEntity)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"INSERT INTO i_logs (status, mobilePhone, content, response, createTime) VALUES (@status, @mobilePhone, @content, @response, @createTime);";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@status", logEntity.Status);
            command.Parameters.AddWithValue("@mobilePhone", logEntity.MobilePhone);
            command.Parameters.AddWithValue("@content", logEntity.Content);
            command.Parameters.AddWithValue("response", logEntity.Response);
            command.Parameters.AddWithValue("@createTime", logEntity.CreateTime);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalCount()
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"select count(*) from i_logs";
            using var command = new SqliteCommand(insertSql, connection);
            using var reader = command.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            return count;
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<LogEntity> GetPageData(int page, int pageSize)
        {
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            const string insertSql = @"select * from i_logs ORDER BY id limit @pageSize OFFSET @offset";
            using var command = new SqliteCommand(insertSql, connection);
            command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            using var reader = command.ExecuteReader();
            var list = new List<LogEntity>();
            while (reader.Read())
            {
                var logEntity = new LogEntity()
                {
                    Id = reader.GetInt32(0),
                    Status = reader.GetString(1),
                    MobilePhone = reader.GetString(2),
                    Content = reader.GetString(3),
                    Response = reader.GetString(4),
                    CreateTime = reader.GetDateTime(5)
                };
                list.Add(logEntity);
            }
            return list;
        }
        #endregion


    }
}
