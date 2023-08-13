using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using hygge_imaotai.Repository;

namespace hygge_imaotai.UserInterface.UserControls
{
    /// <summary>
    /// UserManageControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageControl
    {
        #region Properties

        public static UserManageViewModel UserListViewModel { get; } = new();

        #endregion
        public UserManageControl()
        {
            InitializeComponent();
            DataContext = UserListViewModel;

            RefreshData(UserListViewModel);
        }

        public static void RefreshData(UserManageViewModel userListViewModel)
        {
            UserManageViewModel.UserList.Clear();

            DB.Sqlite.Select<UserEntity>()
                .WhereIf(!string.IsNullOrEmpty(userListViewModel.Phone),
                    i => i.Mobile.Contains(userListViewModel.Phone))
                .WhereIf(!string.IsNullOrEmpty(userListViewModel.UserId),
                    i => (i.UserId + "").Contains(userListViewModel.UserId))
                .WhereIf(!string.IsNullOrEmpty(userListViewModel.Province),
                    i => i.ProvinceName.Contains(userListViewModel.Province))
                .WhereIf(!string.IsNullOrEmpty(userListViewModel.City), i => i.CityName.Contains(userListViewModel.City)).Count(out var total)
                .Page(1, 10).ToList().ForEach(UserManageViewModel.UserList.Add);

            // 分页数据
            var pageCount = total / 10 + 1;
            userListViewModel.Total = total;
            userListViewModel.PageCount = pageCount;
        }

        private void QueryButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData((UserManageViewModel)DataContext);
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
