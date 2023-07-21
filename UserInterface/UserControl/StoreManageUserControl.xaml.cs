using System.Windows.Controls;
using hygge_imaotai.Domain;

namespace hygge_imaotai
{
    /// <summary>
    /// StoreManageUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class StoreManageUserControl : UserControl
    {
        public StoreManageUserControl()
        {
            InitializeComponent();
            DataContext = new StoreListViewModel();
        }
    }
}
