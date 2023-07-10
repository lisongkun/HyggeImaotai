using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Flurl;
using Flurl.Http;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class AddAccountDialog : UserControl
    {

        public AddAccountDialog()
        {
            InitializeComponent();
        }

        private void SendPhoneCodeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dataContext = (DataContext as AddAccountDialogViewModel)!;
            string phone = dataContext.Phone;
            
        }

        
    }
}
