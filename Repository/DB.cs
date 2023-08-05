using System;
using System.Diagnostics;

namespace hygge_imaotai.Repository
{
    public class DB
    {
        static readonly Lazy<IFreeSql> SqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
            .UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句,Trace在输出选项卡中查看
            .UseConnectionString(FreeSql.DataType.Sqlite, App.OrderDatabaseConnectStr)
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build());
        public static IFreeSql Sqlite => SqliteLazy.Value;
    }
}
