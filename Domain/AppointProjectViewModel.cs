using System;
using System.Collections.ObjectModel;
using HyggeIMaoTai.Entity;

namespace HyggeIMaoTai.Domain
{
    /// <summary>
    /// 预约项目页面对应的VM
    /// </summary>
    public class AppointProjectViewModel : ViewModelBase
    {
        public static ObservableCollection<ProductEntity> ProductList { get; set; } = new ObservableCollection<ProductEntity>();
    }
}
