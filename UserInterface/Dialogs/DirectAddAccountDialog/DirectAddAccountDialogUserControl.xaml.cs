using System.Linq;
using System.Windows;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;

namespace hygge_imaotai.UserInterface.Dialogs.DirectAddAccountDialog
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class DirectAddAccountDialogUserControl
    {
        private IMTService service = new();
        private UserEntity _dataContext;

        public DirectAddAccountDialogUserControl(UserEntity dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = (dataContext as UserEntity)!;
            
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var foundUserEntity =
                FieldsViewModel.SearchResult.FirstOrDefault(user => user.Mobile == _dataContext.Mobile);
            if (foundUserEntity != null) return;
            FieldsViewModel.SearchResult.Add(_dataContext);
        }
    }
}
