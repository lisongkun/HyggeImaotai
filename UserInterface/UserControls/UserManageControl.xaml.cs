using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Repository;

namespace hygge_imaotai.UserInterface.UserControls
{
    /// <summary>
    /// UserManageControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageControl
    {
        public UserManageControl()
        {
            InitializeComponent();
            DataContext = new UserManageViewModel();
            RefreshData();
        }

        private void RefreshData()
        {
            var userListViewModel = (UserManageViewModel)DataContext;
            UserManageViewModel.UserList.Clear();
            UserRepository.GetPageData(1,10,userListViewModel).ForEach(UserManageViewModel.UserList.Add);
            // 分页数据
            var total = UserRepository.GetTotalCount(userListViewModel);
            var pageCount = total / 10 + 1;
            userListViewModel.Total = total;
            userListViewModel.PageCount = pageCount;
        }

        private void QueryButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var userListViewModel = (UserManageViewModel)DataContext;
            userListViewModel.Phone = "";
            userListViewModel.UserId = "";
            userListViewModel.Province = "";
            userListViewModel.City = "";
        }
    }
}
