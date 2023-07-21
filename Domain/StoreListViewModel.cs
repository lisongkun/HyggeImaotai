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

        public static ObservableCollection<StoreEntity> StoreList { get; } = new ObservableCollection<StoreEntity>()
        {
            new StoreEntity("143430124001","湖南省","长沙市","宁乡市","长沙市宁乡市城郊街道宁乡大道198号优雅翠园148-158号门面","28.27368","112.565637",
                "宁乡市宁乡大道贵州茅台专卖店","长沙海斌酒业贸易有限责任公司")
        };

        #endregion

    }
}
