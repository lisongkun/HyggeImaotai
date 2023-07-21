using hygge_imaotai.Domain;

namespace hygge_imaotai.UserInterface.UserControl
{
    /// <summary>
    /// StoreManageUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class StoreManageUserControl : System.Windows.Controls.UserControl
    {
        public StoreManageUserControl()
        {
            InitializeComponent();
            DataContext = new StoreListViewModel();
        }
    }
}
