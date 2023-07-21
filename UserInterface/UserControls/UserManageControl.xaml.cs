using hygge_imaotai.Domain;

namespace hygge_imaotai.UserInterface.UserControls
{
    /// <summary>
    /// UserManageControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageControl
    {
        public UserManageControl()
        {
            InitializeComponent();
            DataContext = new FieldsViewModel();
        }
    }
}
