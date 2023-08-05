using System.Collections.ObjectModel;
using System.Windows.Input;
using hygge_imaotai.Entity;
using hygge_imaotai.Repository;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 门店列表Page的ViewModel
    /// </summary>
    public class ShopListViewModel : ViewModelBase
    {
        #region Field
        private string _shopId;
        private string _province;
        private string _city;
        private string _area;
        private string _companyName;

        // 分页数据
        private long _total = 0;
        private int _current = 1;
        private long _pageSize = 10;
        private long _pageCount = 0;
        #endregion

        #region Properties

        public string ShopId
        {
            get => _shopId;
            set => SetProperty(ref _shopId, value);
        }

        public string Province
        {
            get => _province;
            set => SetProperty(ref _province, value);
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public string Area
        {
            get => _area;
            set => SetProperty(ref _area, value);
        }

        public string CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }

        public static ObservableCollection<ShopEntity> StoreList { get; } = new ObservableCollection<ShopEntity>();

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

        public long PageSize
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

        public ShopListViewModel()
        {
            CurrentPageChangeCommand = new AnotherCommandImplementation(UpdateData);
        }
        #endregion

        #region DelegateCommand
        public ICommand CurrentPageChangeCommand { get; private set; }
        private void UpdateData(object parameter)
        {
            StoreList.Clear();
            DB.Sqlite.Select<ShopEntity>()
                .WhereIf(!string.IsNullOrEmpty(this.ShopId),
                    i => i.ShopId.Contains(this.ShopId))
                .WhereIf(!string.IsNullOrEmpty(this.Province),
                    i => i.Province.Contains(this.Province))
                .WhereIf(!string.IsNullOrEmpty(this.City),
                    i => i.City.Contains(this.City))
                .WhereIf(!string.IsNullOrEmpty(this.Area), i => i.Area.Contains(this.Area))
                .WhereIf(!string.IsNullOrEmpty(this.CompanyName), i => i.CompanyName.Contains(this.CompanyName))
                .Page((int)parameter, 10).ToList().ForEach(StoreList.Add);
        }

        #endregion
    }
}
