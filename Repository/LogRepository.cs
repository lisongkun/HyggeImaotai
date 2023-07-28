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

        #endregion
    }
}
