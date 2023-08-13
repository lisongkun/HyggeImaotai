using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using hygge_imaotai.Entity;
using hygge_imaotai.Repository;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 用户管理 - 搜索的condition
    /// </summary>
    public class UserManageViewModel : ViewModelBase
    {
        #region Fields

        // 搜索条件
        private string? _phone;
        private string? _userId;
        private string? _province;
        private string? _city;

        // 分页数据
        private long _total = 0;
        private int _current = 1;
        private int _pageSize = 10;
        private long _pageCount = 0;

        #endregion

        #region Properties

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

        public static ObservableCollection<UserEntity> UserList { get; } =
            new ObservableCollection<UserEntity>();

        public bool? IsAllItems1Selected
        {
            get
            {
                var selected = UserList.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, UserList);
                    OnPropertyChanged();
                }
            }
        }

        public long Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        public int Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public int PageSize
        {
            get => _pageSize;
            set => SetProperty(ref _pageSize, value);
        }

        public long PageCount
        {
            get => _pageCount;
            set => SetProperty(ref _pageCount, value);
        }

        #endregion

        #region Constructor

        public UserManageViewModel()
        {
            CurrentPageChangeCommand = new AnotherCommandImplementation(UpdateData);
        }

        #endregion

        #region Functions

        /// <summary>
        /// 列表进行全选
        /// </summary>
        /// <param name="select"></param>
        /// <param name="models"></param>
        private static void SelectAll(bool select, IEnumerable<UserEntity> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        #endregion

        #region DelegateCommand
        public ICommand CurrentPageChangeCommand { get; private set; }
        private void UpdateData(object parameter)
        {
            UserList.Clear();
            DB.Sqlite.Select<UserEntity>()
                .WhereIf(!string.IsNullOrEmpty(this.Phone),
                    i => i.Mobile.Contains(this.Phone))
                .WhereIf(!string.IsNullOrEmpty(this.UserId),
                    i => (i.UserId + "").Contains(this.UserId))
                .WhereIf(!string.IsNullOrEmpty(this.Province),
                    i => i.ProvinceName.Contains(this.Province))
                .WhereIf(!string.IsNullOrEmpty(this.City),i => i.CityName.Contains(this.City))
                .Page((int)parameter, 10).ToList().ForEach(UserList.Add);
        }

        #endregion
    }
}
