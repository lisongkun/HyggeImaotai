using System.IO;
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
    public partial class ShopManageUserControl
    {
        public ShopManageUserControl()
        {
            InitializeComponent();
            DataContext = new ShopListViewModel();
            RefreshData();
        }

        private void RefreshData()
        {
            var storeListViewModel = (ShopListViewModel)DataContext;
            ShopListViewModel.StoreList.Clear();
            DB.Sqlite.Select<ShopEntity>()
                .WhereIf(!string.IsNullOrEmpty(storeListViewModel.ShopId),
                    i => i.ShopId.Contains(storeListViewModel.ShopId))
                .WhereIf(!string.IsNullOrEmpty(storeListViewModel.Province),
                    i => i.Province.Contains(storeListViewModel.Province))
                .WhereIf(!string.IsNullOrEmpty(storeListViewModel.City),
                    i => i.City.Contains(storeListViewModel.City))
                .WhereIf(!string.IsNullOrEmpty(storeListViewModel.Area), i => i.Area.Contains(storeListViewModel.Area))
                .WhereIf(!string.IsNullOrEmpty(storeListViewModel.CompanyName), i => i.CompanyName.Contains(storeListViewModel.CompanyName))
                .Count(out var total)
                .Page(1, 10).ToList().ForEach(ShopListViewModel.StoreList.Add);


            // 分页数据

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
            // 判断App.StoreListFile是否存在,存在则删除
            if (File.Exists(App.StoreListFile))
            {
                File.Delete(App.StoreListFile);
            }
            await IMTService.RefreshShop();
            RefreshData();
        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var storeListViewModel = (ShopListViewModel)DataContext;
            storeListViewModel.ShopId = "";
            storeListViewModel.Province = "";
            storeListViewModel.City = "";
            storeListViewModel.Area = "";
            storeListViewModel.CompanyName = "";
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
