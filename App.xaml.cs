using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using Newtonsoft.Json;


namespace hygge_imaotai
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string CacheDir = "cache"; 
        private readonly string _productListFile = Path.Combine(CacheDir, "productList.json");


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
 
            // 判断cache文件夹是否存在
            if(!Directory.Exists(CacheDir))
                Directory.CreateDirectory(CacheDir);

            // 判断productListFile是否存在,存在则读入缓存
            if (File.Exists(_productListFile))
            {
                var json = File.ReadAllText(_productListFile);
                AppointProjectViewModel.ProductList = JsonConvert.DeserializeObject<ObservableCollection<ProductEntity>>(json);
            }
        }

        public static void WriteCache(string filename,string content)
        {
            var path = Path.Combine(CacheDir, filename);
            File.WriteAllText(path,content);
        }
    }
}
