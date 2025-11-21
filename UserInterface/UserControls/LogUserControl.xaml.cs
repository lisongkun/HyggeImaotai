using System;
using System.Threading.Tasks;
using System.Windows;
using HyggeIMaoTai.Domain;
using HyggeIMaoTai.Repository;
using HyggeIMaoTai.Utils;

namespace HyggeIMaoTai.UserInterface.UserControls
{
    /// <summary>
    /// LogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogUserControl
    {
        public LogUserControl()
        {
            InitializeComponent();
            DataContext = new LogListViewModel();
            RefreshData();
        }

        private void RefreshData()
        {
            var logListViewModel = (LogListViewModel)DataContext;
            LogListViewModel.LogList.Clear();

            var (logList, count) = LogRepository.GetLogList(
                logListViewModel.Mobile,
                logListViewModel.SearchContent,
                logListViewModel.Status,
                1,
                10);

            foreach (var log in logList)
            {
                LogListViewModel.LogList.Add(log);
            }

            // 分页数据
            var pageCount = count / 10 + 1;
            logListViewModel.Total = count;
            logListViewModel.PageCount = pageCount;
        }

        /// <summary>
        /// 刷新日志按钮被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RefreshLogButton_OnClick(object sender, RoutedEventArgs e)
        {
            // 在 UI 线程获取查询条件
            var logListViewModel = (LogListViewModel)DataContext;
            var mobile = logListViewModel.Mobile;
            var searchContent = logListViewModel.SearchContent;
            var status = logListViewModel.Status;

            await DialogHelper.ShowLoadingDialogAsync(async () =>
            {
                await Task.Run(() =>
                {
                    // 在后台线程执行数据库查询
                    var (logList, count) = LogRepository.GetLogList(
                        mobile,
                        searchContent,
                        status,
                        1,
                        10);

                    // 回到 UI 线程更新界面
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        LogListViewModel.LogList.Clear();
                        foreach (var log in logList)
                        {
                            LogListViewModel.LogList.Add(log);
                        }

                        // 分页数据
                        var pageCount = count / 10 + 1;
                        logListViewModel.Total = count;
                        logListViewModel.PageCount = pageCount;
                    });
                });
            }, "正在刷新日志数据...");
        }

        private void QueryButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var logListViewModel = (LogListViewModel)DataContext;
            logListViewModel.Mobile = "";
            logListViewModel.Status = "";
            logListViewModel.SearchContent = "";
        }
    }
}
