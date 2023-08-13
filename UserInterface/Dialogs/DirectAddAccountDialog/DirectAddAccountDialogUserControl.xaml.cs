using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using hygge_imaotai.Repository;
using hygge_imaotai.UserInterface.UserControls;

namespace hygge_imaotai.UserInterface.Dialogs.DirectAddAccountDialog
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class DirectAddAccountDialogUserControl
    {
        private IMTService service = new();
        private readonly UserEntity _dataContext;

 

        public DirectAddAccountDialogUserControl(UserEntity dataContext, bool isUpadte = false)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = (dataContext as UserEntity)!;
           
            TitleBlock.Text = isUpadte ? "更新i茅台用户:" : "添加i茅台用户:";
            LoginButton.Content = isUpadte ? "更新" : "添加";
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            // 判断经纬度是否符合规范
            bool latIsInteger = Regex.IsMatch(_dataContext.Lat, @"^\d+$");
            bool latIsFloat = Regex.IsMatch(_dataContext.Lat, @"^\d+(\.\d+)?$");
            if (!latIsFloat && !latIsInteger)
            {
                MessageBox.Show("纬度不符合规范");
                return;
            }

            bool lngIsInteger = Regex.IsMatch(_dataContext.Lng, @"^\d+$");
            bool lngIsFloat = Regex.IsMatch(_dataContext.Lng, @"^\d+(\.\d+)?$");
            if (!latIsFloat && !latIsInteger)
            {
                MessageBox.Show("经度不符合规范");
                return;
            }


            var foundUserEntity =
                UserManageViewModel.UserList.FirstOrDefault(user => user.Mobile == _dataContext.Mobile);
            if (foundUserEntity != null)
            {
                // 此处执行更新操作o
                // 更新寻找到的用户信息
                DB.Sqlite.Update<UserEntity>()
                    .Set(i => i.UserId,_dataContext.UserId)
                    .Set(i => i.Token, _dataContext.Token)
                    .Set(i => i.ItemCode, _dataContext.ItemCode)
                    .Set(i => i.ProvinceName, _dataContext.ProvinceName)
                    .Set(i => i.CityName, _dataContext.CityName)
                    .Set(i => i.Lat, _dataContext.Lat)
                    .Set(i => i.Lng, _dataContext.Lng)
                    .Set(i => i.ShopType, _dataContext.ShopType)
                    .Set(i => i.ExpireTime, _dataContext.ExpireTime)
                    .Where(i => i.Mobile == _dataContext.Mobile).ExecuteAffrows();

                return;
            }

            DB.Sqlite.Insert(_dataContext).ExecuteAffrows();

            // 刷新用户列表
            UserManageControl.RefreshData(UserManageControl.UserListViewModel);
        }
    }
}
