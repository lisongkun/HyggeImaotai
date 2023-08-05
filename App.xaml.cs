using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using hygge_imaotai.Jobs;
using hygge_imaotai.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;


namespace hygge_imaotai
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string CacheDir = "cache";
        // 内部使用缓存文件
        private readonly string _productListFile = Path.Combine(CacheDir, "productList.json");
        private readonly string _sessionIdFile = Path.Combine(CacheDir, "mtSessionId.txt");
        // 共用缓存文件
        public static string StoreListFile = Path.Combine(CacheDir, "storeList.json");
        /// <summary>
        /// 订单数据库表名
        /// </summary>
        public const string OrderDatabasePath = "cache/imaotai.db";
        /// <summary>
        /// 订单数据库连接字符串
        /// </summary>
        public const string OrderDatabaseConnectStr = "Data Source=cache/imaotai.db;";

        /// <summary>
        /// 茅台会话ID
        /// </summary>
        public static string MtSessionId = "";


        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 判断cache文件夹是否存在
            if (!Directory.Exists(CacheDir))
                Directory.CreateDirectory(CacheDir);

            // 判断productListFile是否存在,存在则读入缓存
            if (File.Exists(_productListFile))
            {
                var json = File.ReadAllText(_productListFile);
                AppointProjectViewModel.ProductList = JsonConvert.DeserializeObject<ObservableCollection<ProductEntity>>(json);
            }
            // 开始初始化数据库
            CommonRepository.CreateDatabase();
            // 读取会话ID
            if (File.Exists(_sessionIdFile))
                MtSessionId = File.ReadAllText(_sessionIdFile);

            // 创建任务调度器
            var stdSchedulerFactory = new StdSchedulerFactory();
            var scheduler = await stdSchedulerFactory.GetScheduler();
            await scheduler.Start();
            Console.WriteLine("任务调度器已启动");

            // 创建抢购预约的任务
            var jobDetail = JobBuilder.Create<ReservationJob>().Build();
            var trigger = TriggerBuilder.Create().WithCronSchedule("0 5 9 ? * * ").Build();
            // 创建刷新数据的任务
            var refreshJobDetail = JobBuilder.Create<RefreshJob>().Build();
            var refreshTrigger = TriggerBuilder.Create().WithCronSchedule("0 25,55 6,7,8 ? * * ").Build();
            // 添加调度
            await scheduler.ScheduleJob(jobDetail, trigger);
            await scheduler.ScheduleJob(refreshJobDetail, refreshTrigger);
        }

        public static void WriteCache(string filename, string content)
        {
            var path = Path.Combine(CacheDir, filename);
            File.WriteAllText(path, content);
        }
    }
}
