using Microsoft.Data.Sqlite;
using System.IO;

namespace hygge_imaotai.Repository
{
    /// <summary>
    /// 共用或基本的数据库操作
    /// </summary>
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
            // 判断表是否存在
            CreateShopTable(connection);
            CreateLogTable(connection);
        }

        /// <summary>
        /// 创建店铺表
        /// </summary>
        /// <param name="connection"></param>
        private static void CreateShopTable(SqliteConnection connection)
        {
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

        /// <summary>
        /// 创建日志表
        /// </summary>
        private static void CreateLogTable(SqliteConnection connection)
        {
            const string checkTableSql = "SELECT name FROM sqlite_master WHERE type='table' AND name='i_logs';";
            using var checkTableCommand = new SqliteCommand(checkTableSql, connection);
            using var reader = checkTableCommand.ExecuteReader();
            if (reader.HasRows)// 表存在，不需要创建
                return;

            // 创建新的表
            const string createTableSql = @"CREATE TABLE i_logs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    status TEXT NOT NULL,
    mobilePhone TEXT NOT NULL,
    content TEXT NOT NULL,
    response TEXT,
    createTime DATETIME NOT NULL
);
";
            using var command = new SqliteCommand(createTableSql, connection);

            command.ExecuteNonQuery();
        }
    }
}
