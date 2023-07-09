using hygge_imaotai.Domain;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace hygge_imaotai
{
    /// <summary>
    /// HomeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("http://donate.lisok.cn/#");
    }
}
