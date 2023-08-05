using System.Data.SQLite;
using System.IO;
using hygge_imaotai.Entity;

namespace hygge_imaotai.Repository
{
    /// <summary>
    /// 共用或基本的数据库操作
    /// </summary>
    internal class CommonRepository
    {
        /// <summary>
        /// 创建订单数据库
        /// </summary>
        public static void CreateDatabase()
        {
            // 判断数据库文件是否存在
            if (File.Exists(App.OrderDatabasePath)) return;
            // 创建数据库连接
            SQLiteConnection.CreateFile(App.OrderDatabasePath);
            var sqLiteConnection = new SQLiteConnection(App.OrderDatabaseConnectStr);
            sqLiteConnection.Open();
            // 创建表结构
            var types = new[] { typeof(UserEntity),typeof(ShopEntity),typeof(LogEntity) };
            DB.Sqlite.CodeFirst.SyncStructure(types);
        }
    }
}
