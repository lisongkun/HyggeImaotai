using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hygge_imaotai.Repository
{
    class CommonRepository
    {
        /// <summary>
        /// 创建订单数据库
        /// </summary>
        public static void CreateDatabase()
        {
            // 判断数据库文件是否存在
            if (File.Exists(App.OrderDatabasePath)) return;
            // 创建数据库连接
            using var connection = new SqliteConnection(App.OrderDatabaseConnectStr);
            connection.Open();
            // 判断orders表是否存在
            const string checkTableSql = "SELECT name FROM sqlite_master WHERE type='table' AND name='i_shop';";
            using var checkTableCommand = new SqliteCommand(checkTableSql, connection);
            using var reader = checkTableCommand.ExecuteReader();
            if (reader.HasRows)// 表存在，不需要创建
                return;

            // 创建新的表
            const string createTableSql = @"CREATE TABLE i_shop (
  shop_id INTEGER PRIMARY KEY NOT NULL,
  i_shop_id TEXT,
  province_name TEXT,
  city_name TEXT,
  district_name TEXT,
  full_address TEXT,
  lat TEXT,
  lng TEXT,
  name TEXT,
  tenant_name TEXT,
  create_time TEXT
);
";
            using var command = new SqliteCommand(createTableSql, connection);

            command.ExecuteNonQuery();
        }
    }
}
