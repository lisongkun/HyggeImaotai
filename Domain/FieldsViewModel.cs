using System.Collections.ObjectModel;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 用户管理 - 搜索的condition
    /// </summary>
    public class FieldsViewModel:ViewModelBase
    {
        private string? _phone;
        private string? _userId;
        private string? _province;
        private string? _city;

        public FieldsViewModel()
        {
            SearchResult = CreateData();
        }

        private static ObservableCollection<UserManageViewModel> CreateData()
        {
            return new ObservableCollection<UserManageViewModel>
            {
                new UserManageViewModel
                {
                    Phone = "13712345678",
                    UserId = "1234567890",
                    Token = "abcdefg",
                    AppointmentProjectCode = "1001@1002@1003",
                    Province = "河北省",
                    City = "石家庄市市",
                    Latitude = "37N",
                    Longitude = "119E",
                    Type = '1',
                    ExpireTime = "2023-07-15"
                },
            };
        }

        public string? Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string? UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string? Province
        {
            get => _province;
            set => SetProperty(ref _province, value);
        }

        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public ObservableCollection<UserManageViewModel> SearchResult { get; }
    }
}
