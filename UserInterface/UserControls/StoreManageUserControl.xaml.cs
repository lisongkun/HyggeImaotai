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
            DataContext = new StoreListViewModel();
            RefreshData();
        }

        private void RefreshData()
        {
            var storeListViewModel = (StoreListViewModel)DataContext;
            StoreListViewModel.StoreList.Clear();
            ShopRepository.GetPageData(1, 10,storeListViewModel).ForEach(StoreListViewModel.StoreList.Add);
            // 分页数据
            var total = ShopRepository.GetTotalCount((StoreListViewModel)DataContext);
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
            await IMTService.RefreshShop();
            RefreshData();
        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var storeListViewModel = (StoreListViewModel)DataContext;
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
