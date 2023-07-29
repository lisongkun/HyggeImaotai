using hygge_imaotai.Entity;
using System.Collections.ObjectModel;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 日志用户控件的ViewModel
    /// </summary>
    public class LogListViewModel:ViewModelBase
    {
        #region Fields

        // 分页数据
        private int total = 0;
        private int current = 1;
        private int pageSize = 10;
        private int pageCount = 0;

        #endregion

        #region Properties

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

        public static ObservableCollection<LogEntity> LogList { get; } = new ObservableCollection<LogEntity>();

        #endregion

    }
}
