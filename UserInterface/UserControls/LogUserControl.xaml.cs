using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using hygge_imaotai.Repository;

namespace hygge_imaotai.UserInterface.UserControls
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

            DB.Sqlite.Select<LogEntity>()
                .WhereIf(!string.IsNullOrEmpty(logListViewModel.Mobile),
                    i => i.MobilePhone.Contains(logListViewModel.Mobile))
                .WhereIf(!string.IsNullOrEmpty(logListViewModel.SearchContent),
                    i => i.Content.Contains(logListViewModel.SearchContent))
                .WhereIf(!string.IsNullOrEmpty(logListViewModel.Status),
                    i => i.Status.Contains(logListViewModel.Status))
                .Count(out var count)
                .Page(1, 10).ToList().ForEach(LogListViewModel.LogList.Add);

            // 分页数据
            var pageCount = count / 10 + 1;
            logListViewModel.Total = count;
            logListViewModel.PageCount = pageCount;
        }

        private void RefreshLogButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData();
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
