using System.Linq;
using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;

namespace hygge_imaotai.UserInterface.Dialogs.DirectAddAccountDialog
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class DirectAddAccountDialogUserControl
    {
        private IMTService service = new();
        private UserEntity _dataContext;

        private string willFindPhone;

        public DirectAddAccountDialogUserControl(UserEntity dataContext, bool isUpadte = false)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = (dataContext as UserEntity)!;
            willFindPhone = isUpadte ? dataContext.Mobile : _dataContext.Mobile;
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var foundUserEntity =
                FieldsViewModel.SearchResult.FirstOrDefault(user => user.Mobile == willFindPhone);
            if (foundUserEntity != null)
            {
                // 更新寻找到的用户信息
                foundUserEntity.Mobile = _dataContext.Mobile;
                foundUserEntity.UserId = _dataContext.UserId;
                foundUserEntity.Token = _dataContext.Token;
                foundUserEntity.ItemCode = _dataContext.ItemCode;
                foundUserEntity.ProvinceName = _dataContext.ProvinceName;
                foundUserEntity.CityName = _dataContext.CityName;
                foundUserEntity.Lat = _dataContext.Lat;
                foundUserEntity.Lng = _dataContext.Lng;
                foundUserEntity.ShopType = _dataContext.ShopType;
                foundUserEntity.ExpireTime = _dataContext.ExpireTime;
                return;
            }
            FieldsViewModel.SearchResult.Add(_dataContext);
        }
    }
}
