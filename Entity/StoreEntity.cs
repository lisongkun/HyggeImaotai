

using System;
using hygge_imaotai.Domain;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai.Entity
{
    /// <summary>
    /// 店铺的实体类
    /// </summary>
    public class StoreEntity:ViewModelBase
    {
        #region Field

        private string _productId;
        private string _province;
        private string _city;
        private string _area;
        private string _unbrokenAddress;
        private string _lat;
        private string _lng;
        private string _name;
        private string _companyName;
        private DateTime _createdAt;

        #endregion

        #region Construct

        public StoreEntity()
        {
        }


        public StoreEntity(string shopId, JObject jObject)
        {
            ProductId = shopId;
            Province =  jObject.GetValue("provinceName").Value<string>();
            City = jObject.GetValue("cityName").Value<string>();
            Area = jObject.GetValue("districtName").Value<string>();
            UnbrokenAddress = jObject.GetValue("fullAddress").Value<string>();
            Lat = jObject.GetValue("lat").Value<string>();
            Lng = jObject.GetValue("lng").Value<string>();
            Name = jObject.GetValue("name").Value<string>();
            CompanyName = jObject.GetValue("tenantName").Value<string>();
            CreatedAt = DateTime.Now;
        }

        public StoreEntity(string productId, string province, string city, string area, string unbrokenAddress, string lat, string lng, string name, string companyName)
        {
            _productId = productId;
            _province = province;
            _city = city;
            _area = area;
            _unbrokenAddress = unbrokenAddress;
            _lat = lat;
            _lng = lng;
            _name = name;
            _companyName = companyName;
            _createdAt = DateTime.Now;
        }

        #endregion

        #region Properties

        public string ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }

        public string Province
        {
            get => _province;
            set => SetProperty(ref _province, value);
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public string Area
        {
            get => _area;
            set => SetProperty(ref _area, value);
        }

        public string UnbrokenAddress
        {
            get => _unbrokenAddress;
            set => SetProperty(ref _unbrokenAddress, value);
        }

        public string Lat
        {
            get => _lat;
            set => SetProperty(ref _lat, value);
        }

        public string Lng
        {
            get => _lng;
            set => SetProperty(ref _lng, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        #endregion
    }
}
