using System.Threading;
using System.Windows;
using Flurl.Http;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using hygge_imaotai.Repository;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai.UserInterface.UserControls
{
    /// <summary>
    /// StoreManageUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class StoreManageUserControl
    {
        public StoreManageUserControl()
        {
            InitializeComponent();
            var storeListViewModel = new StoreListViewModel();
            DataContext = storeListViewModel;

            ShopRepository.GetPageData(1, 10).ForEach(item =>
            {
                StoreListViewModel.StoreList.Add(item);
            });

            // 分页数据
            var total = ShopRepository.GetTotalCount();
            var pageCount = total / 10 + 1;
            storeListViewModel.Total = total;
            storeListViewModel.PageCount = pageCount;
        }

        /// <summary>
        /// 刷新Shop数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RefreshShopButton_OnClick(object sender, RoutedEventArgs e)
        {
            StoreListViewModel.StoreList.Clear();
            ShopRepository.TruncateTable();

            var responseStr = await "https://static.moutai519.com.cn/mt-backend/xhr/front/mall/resource/get"
                .GetStringAsync();
            var jObject = JObject.Parse(responseStr);
            var shopsUrl = jObject.GetValue("data").Value<JObject>().GetValue("mtshops_pc").Value<JObject>().GetValue("url").Value<string>();
            var shopInnerJson = await shopsUrl.GetStringAsync();

            var shopInnerJObject = JObject.Parse(shopInnerJson);
            var thread = new Thread(() =>
            {
                foreach (var property in shopInnerJObject.Properties())
                {
                    var shopId = property.Name;
                    var nestedObject = (JObject)property.Value;
                    ShopRepository.InsertShop(new StoreEntity(shopId, nestedObject));
                }
            });
            thread.Start();
            thread.Join();
            ShopRepository.GetPageData(1, 20).ForEach(item =>
            {
                StoreListViewModel.StoreList.Add(item);
            });
        }
    }
}
