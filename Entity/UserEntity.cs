﻿using System;
using System.Windows.Input;
using hygge_imaotai.Domain;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai.Entity
{
    public class UserEntity:ViewModelBase
    {
        #region Field

        
private bool _isSelected;

        private long _userId;
        private string _mobile;
        private string _token;
        /// <summary>
        /// 商品预约code，用@间隔
        /// </summary>
        private string _itemCode;
        /// <summary>
        /// 省份
        /// </summary>
        private string _provinceName;

        /// <summary>
        /// 城市
        /// </summary>
        private string _cityName;

        /// <summary>
        /// 完整地址
        /// </summary>
        private string _address;

        /// <summary>
        /// 纬度
        /// </summary>
        private string _lat;

        /// <summary>
        /// 经度
        /// </summary>
        private string _lng;

        /// <summary>
        /// 类型
        /// </summary>
        private int _shopType;

        /// <summary>
        /// 返回参数
        /// </summary>
        private string _jsonResult;

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _createTime;

        /// <summary>
        /// token过期时间
        /// </summary>
        private DateTime _expireTime;
        #endregion
        

        #region Properties

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public long UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        public string Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        public string ItemCode
        {
            get => _itemCode;
            set => SetProperty(ref _itemCode, value);
        }

        public string ProvinceName
        {
            get => _provinceName;
            set => SetProperty(ref _provinceName, value);
        }

        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
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

        public int ShopType
        {
            get => _shopType;
            set => SetProperty(ref _shopType, value);
        }

        public string JsonResult
        {
            get => _jsonResult;
            set => SetProperty(ref _jsonResult, value);
        }

        public DateTime CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }

        public DateTime ExpireTime
        {
            get => _expireTime;
            set => SetProperty(ref _expireTime, value);
        }

        #endregion

        #region Commond

        public ICommand DeleteCommand => new AnotherCommandImplementation(DeleteItemFunc);
        private static void DeleteItemFunc(object? parameter)
        {
            FieldsViewModel.SearchResult.Remove((parameter as UserEntity)!);
        }
        #endregion

        #region Construct Function

        public UserEntity(string mobile, JObject jsonObject):base()
        {
            var data = jsonObject["data"];
            this.UserId = data["userId"].Value<long>();
            this.Mobile = mobile;
            this.Token = data["token"].Value<string>();
            this.JsonResult = jsonObject.ToString()[..2000];

            this.CreateTime = DateTime.Now;
            this.ExpireTime = DateTime.Now.AddDays(30);
        }

        public UserEntity(bool isSelected, long userId, string mobile, string token, string itemCode, string provinceName, string cityName, string address, string lat, string lng, int shopType, string jsonResult, DateTime createTime, DateTime expireTime):base()
        {
            _isSelected = isSelected;
            _userId = userId;
            _mobile = mobile;
            _token = token;
            _itemCode = itemCode;
            _provinceName = provinceName;
            _cityName = cityName;
            _address = address;
            _lat = lat;
            _lng = lng;
            _shopType = shopType;
            _jsonResult = jsonResult;
            _createTime = createTime;
            _expireTime = expireTime;
        }

        #endregion
        
    }
}
