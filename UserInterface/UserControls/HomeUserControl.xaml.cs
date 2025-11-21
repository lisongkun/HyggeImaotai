using System.Windows;
using HyggeIMaoTai.Domain;

namespace HyggeIMaoTai.UserInterface.UserControl
{
    /// <summary>
    /// HomeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class HomeUserControl : System.Windows.Controls.UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("http://donate.lisok.cn/#");
    }
}
