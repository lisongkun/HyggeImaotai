using System;
using System.Windows;
using Flurl.Http;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai.UserInterface.UserControls
{
    /// <summary>
    /// AppointProjectUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppointProjectUserControl : System.Windows.Controls.UserControl
    {
        public AppointProjectUserControl()
        {
            InitializeComponent();
            DataContext = new AppointProjectViewModel();
        }

        private async void RefreshProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            App.MtSessionId = string.Empty;
            App.WriteCache("mtSessionId.txt",string.Empty);
            AppointProjectViewModel.ProductList.Clear();
            await IMTService.GetCurrentSessionId();
        }
    }
}
