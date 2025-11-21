using System.Text.RegularExpressions;
using System.Windows;
using HyggeIMaoTai.Domain;
using HyggeIMaoTai.Entity;
using HyggeIMaoTai.Repository;
using HyggeIMaoTai.UserInterface.UserControls;
using MaterialDesignThemes.Wpf;

namespace HyggeIMaoTai.UserInterface.Dialogs.DirectAddAccountDialog
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class DirectAddAccountDialogUserControl
    {
        private IMTService service = new();
        private readonly UserEntity _dataContext;

 
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="isUpadte"></param>
        public DirectAddAccountDialogUserControl(UserEntity dataContext, bool isUpadte = false)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = (dataContext as UserEntity)!;
           
            TitleBlock.Text = isUpadte ? "更新i茅台用户:" : "添加i茅台用户:";
            LoginButton.Content = isUpadte ? "更新" : "添加";
        }

        /// <summary>
        /// 登录按钮被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            // 判断经纬度是否符合规范
            bool latIsInteger = Regex.IsMatch(_dataContext.Lat, @"^\d+$");
            bool latIsFloat = Regex.IsMatch(_dataContext.Lat, @"^\d+(\.\d+)?$");
            if (!latIsFloat && !latIsInteger)
            {
                MessageBox.Show("纬度不符合规范", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // 验证失败，不关闭对话框
            }

            bool lngIsInteger = Regex.IsMatch(_dataContext.Lng, @"^\d+$");
            bool lngIsFloat = Regex.IsMatch(_dataContext.Lng, @"^\d+(\.\d+)?$");
            if (!lngIsFloat && !lngIsInteger)
            {
                MessageBox.Show("经度不符合规范", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // 验证失败，不关闭对话框
            }

            // 检查用户是否已存在
            var foundUserEntity = UserRepository.GetUserByMobile(_dataContext.Mobile);
            
            if (foundUserEntity != null)
            {
                // 执行更新操作
                UserRepository.UpdateUser(_dataContext);

                // 刷新用户列表
                UserManageControl.RefreshData(UserManageControl.UserListViewModel);
                
                MessageBox.Show("用户信息更新成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // 执行插入操作
                UserRepository.InsertUser(_dataContext);

                // 刷新用户列表
                UserManageControl.RefreshData(UserManageControl.UserListViewModel);
                
                MessageBox.Show("用户添加成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // 操作成功，关闭对话框
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
