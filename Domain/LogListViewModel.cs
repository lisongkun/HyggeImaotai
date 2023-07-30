using System;
using hygge_imaotai.Entity;
using System.Collections.ObjectModel;
using hygge_imaotai.Repository;
using System.Windows.Input;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 日志用户控件的ViewModel
    /// </summary>
    public class LogListViewModel:ViewModelBase
    {
        #region Fields
        // 检索条件
        private string _mobile;

        private string _status;

        private string _searchContent;
        // 分页数据
        private int _total = 0;
        private int _current = 1;
        private int _pageSize = 10;
        private int _pageCount = 0;

        #endregion

        #region Properties

        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public string SearchContent
        {
            get => _searchContent;
            set => SetProperty(ref _searchContent, value);
        }

        public int Total
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

        public int PageCount
        {
            get => _pageCount;
            set => SetProperty(ref _pageCount, value);
        }

        public static ObservableCollection<LogEntity> LogList { get; } = new ObservableCollection<LogEntity>();

        #endregion

        #region Constructor

        public LogListViewModel()
        {
            CurrentPageChangeCommand = new AnotherCommandImplementation(UpdateData);
        }

        #endregion

        #region DelegateCommand
        public ICommand CurrentPageChangeCommand { get; private set; }
        private void UpdateData(object parameter)
        {
            LogList.Clear();
            LogRepository.GetPageData((int)parameter, 10,this).ForEach(LogList.Add);
        }

        #endregion

    }
}
