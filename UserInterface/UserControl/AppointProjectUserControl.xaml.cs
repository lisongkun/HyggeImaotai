using System;
using System.Windows;
using System.Windows.Controls;
using Flurl.Http;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai
{
    /// <summary>
    /// AppointProjectUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppointProjectUserControl : UserControl
    {
        public AppointProjectUserControl()
        {
            InitializeComponent();
            DataContext = new AppointProjectViewModel();
        }

        private async void RefreshProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            AppointProjectViewModel.ProductList.Clear();
            DateTime midNight = DateTime.Now.Date;
            DateTimeOffset epochStart = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.FromHours(8));
            TimeSpan timeSpan = midNight.AddHours(-8) - epochStart;
            long milliseconds = (long)timeSpan.TotalMilliseconds;

            var responseStr = await ("https://static.moutai519.com.cn/mt-backend/xhr/front/mall/index/session/get/" +
                                     milliseconds)
                .GetStringAsync();
            var jObject = JObject.Parse(responseStr);
            if (jObject.GetValue("code").Value<int>() == 2000)
            {
                var dataJObject = jObject["data"];
                var itemList = (JArray)dataJObject["itemList"];
                foreach (var itemElement in itemList)
                {
                    AppointProjectViewModel.ProductList.Add(new ProductEntity(itemElement["itemCode"].Value<string>(), 
                        itemElement["title"].Value<string>(), 
                        itemElement["content"].Value<string>(),
                        itemElement["picture"].Value<string>(),DateTime.Now));
                }
                App.WriteCache("productList.json",JsonConvert.SerializeObject(AppointProjectViewModel.ProductList));
            }

        }
    }
}
