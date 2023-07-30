using System.Windows;
using hygge_imaotai.Domain;
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
            LogRepository.GetPageData(1, 10,logListViewModel).ForEach(LogListViewModel.LogList.Add);
            // 分页数据
            var total = LogRepository.GetTotalCount(logListViewModel);
            var pageCount = total / 10 + 1;
            logListViewModel.Total = total;
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
