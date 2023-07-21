using System.Collections.ObjectModel;
using hygge_imaotai.Entity;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 门店列表Page的ViewModel
    /// </summary>
    public class StoreListViewModel:ViewModelBase
    {
        #region Field
        private string _shopId;
        private string _province;
        private string _city;
        private string _area;
        private string _companyName;

        // 分页数据
        private int total = 0;
        private int current = 1;
        private int pageSize = 10;
        private int pageCount = 0;
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

        public static ObservableCollection<StoreEntity> StoreList { get; } = new ObservableCollection<StoreEntity>();

        public int Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }

        public int Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        public int PageSize
        {
            get => pageSize;
            set => SetProperty(ref pageSize, value);
        }

        public int PageCount
        {
            get => pageCount;
            set => SetProperty(ref pageCount, value);
        }

        #endregion

    }
}
