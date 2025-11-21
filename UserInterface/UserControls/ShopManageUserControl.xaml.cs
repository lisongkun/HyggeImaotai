using System;
using System.IO;
using System.Windows;
using HyggeIMaoTai.Domain;
using HyggeIMaoTai.Entity;
using HyggeIMaoTai.Repository;
using HyggeIMaoTai.Utils;

namespace HyggeIMaoTai.UserInterface.UserControls
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

        /// <summary>
        /// 刷新数据
        /// </summary>
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
            RefreshShopButton.IsEnabled = false;
            
            try
            {
                // 使用工具类显示加载对话框并执行异步操作
                await DialogHelper.ShowLoadingDialogAsync(async () =>
                {
                    // 判断App.StoreListFile是否存在,存在则删除
                    if (File.Exists(App.StoreListFile))
                    {
                        File.Delete(App.StoreListFile);
                    }
                    await IMTService.RefreshShop();
                    RefreshData();
                }, "正在刷新商店数据...");
            }
            catch (Exception ex)
            {
                // 错误已在 DialogHelper 中处理，这里可以添加额外的错误处理逻辑
                // 如果需要自定义错误处理，可以使用带 onError 参数的重载方法
            }
            finally
            {
                RefreshShopButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// 重置按钮被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var storeListViewModel = (ShopListViewModel)DataContext;
            storeListViewModel.ShopId = "";
            storeListViewModel.Province = "";
            storeListViewModel.City = "";
            storeListViewModel.Area = "";
            storeListViewModel.CompanyName = "";
        }

        /// <summary>
        /// 搜索按钮被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
