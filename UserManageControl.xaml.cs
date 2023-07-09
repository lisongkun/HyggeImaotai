using System.Windows.Controls;
using hygge_imaotai.Domain;

namespace hygge_imaotai
{
    /// <summary>
    /// UserManageControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageControl : UserControl
    {
        public UserManageControl()
        {
            InitializeComponent();
            DataContext = new FieldsViewModel();
        }
    }
}
