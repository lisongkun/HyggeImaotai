using System;
using System.Collections.ObjectModel;
using hygge_imaotai.Entity;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 预约项目页面对应的VM
    /// </summary>
    public class AppointProjectViewModel : ViewModelBase
    {
        public static ObservableCollection<ProductEntity> ProductList { get; set; } = new ObservableCollection<ProductEntity>();
    }
}
