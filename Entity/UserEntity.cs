using System;
using System.Windows.Input;
using FreeSql.DataAnnotations;
using hygge_imaotai.Domain;
using hygge_imaotai.Repository;
using hygge_imaotai.UserInterface.Component;
using hygge_imaotai.UserInterface.Dialogs.DirectAddAccountDialog;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hygge_imaotai.Entity
{
    public class UserEntity : ViewModelBase
    {
        #region Field


        private bool _isSelected;

        private long _userId;
        private string _mobile;
        private string _token;
        /// <summary>
        /// 商品预约code，用@间隔
        /// </summary>
        private string _itemCode = string.Empty;
        /// <summary>
        /// 省份
        /// </summary>
        private string _provinceName = string.Empty;

        /// <summary>
        /// 城市
        /// </summary>
        private string _cityName = string.Empty;

        /// <summary>
        /// 完整地址
        /// </summary>
        private string _address = string.Empty;

        /// <summary>
        /// 纬度
        /// </summary>
        private string _lat = string.Empty;

        /// <summary>
        /// 经度
        /// </summary>
        private string _lng = string.Empty;

        /// <summary>
        /// 类型
        /// </summary>
        private int _shopType =  1;
        /// <summary>
        /// 对接推送令牌
        /// </summary>
        private string _pushPlusToken = string.Empty;

        /// <summary>
        /// 返回参数
        /// </summary>
        private string _jsonResult = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _createTime = DateTime.Now;

        /// <summary>
        /// token过期时间
        /// </summary>
        private DateTime _expireTime = DateTime.Now.AddDays(30);
        #endregion


        #region Properties
        [Column(IsIgnore = true)]
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

        public string PushPlusToken
        {
            get => _pushPlusToken;
            set => SetProperty(ref _pushPlusToken, value);
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
        public ICommand ModifyCommand => new AnotherCommandImplementation(ModifyItemFunc);
        public ICommand ReserveCommand => new AnotherCommandImplementation(ReserveCommandItemFunc);

        private static void DeleteItemFunc(object? parameter)
        {
            var userEntity = (parameter as UserEntity)!;
            DB.Sqlite.Delete<UserEntity>().Where(i => i.Mobile == userEntity.Mobile).ExecuteAffrows();
            UserManageViewModel.UserList.Remove((parameter as UserEntity)!);
        }

        private static void ModifyItemFunc(object? parameter)
        {
            var userEntity = parameter as UserEntity;
            // 深拷贝一份userEntity
            var view = new DirectAddAccountDialogUserControl(JsonConvert.DeserializeObject<UserEntity>(JsonConvert.SerializeObject(userEntity)), true);
            DialogHost.Show(view, "RootDialog");
        }

        private static async void ReserveCommandItemFunc(object? parameter)
        {
            var userEntity = parameter as UserEntity;
            if (string.IsNullOrEmpty(userEntity?.ItemCode))
            {
                new MessageBoxCustom("预约商品码未填写", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            else
            {
                try
                {
                    await IMTService.Reservation(userEntity);
                    new MessageBoxCustom("手动发起预约成功,响应结果请查看日志", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                catch (Exception e)
                {
                    new MessageBoxCustom("预约请求失败,响应结果详细请查看日志", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
        }
        #endregion

        #region Construct Function

        public UserEntity()
        {
        }

        public UserEntity(string mobile, JObject jsonObject) : base()
        {
            var data = jsonObject["data"];
            this.UserId = data["userId"].Value<long>();
            this.Mobile = mobile;
            this.Token = data["token"].Value<string>();
            this.JsonResult = jsonObject.ToString();

            this.CreateTime = DateTime.Now;
            this.ExpireTime = DateTime.Now.AddDays(30);

            this.ItemCode = string.Empty;
            this.ProvinceName = string.Empty;
            this.CityName = string.Empty;
            this.Address = string.Empty;
            this.Lat = string.Empty;
            this.Lng = string.Empty;
            this.PushPlusToken = string.Empty;
        }

        public UserEntity(bool isSelected, long userId, string mobile, string token, string itemCode, string provinceName, string cityName, string address, string lat, string lng, int shopType,string pushToken, string jsonResult, DateTime createTime, DateTime expireTime) : base()
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
            _pushPlusToken = pushToken;
            _expireTime = expireTime;
        }

        #endregion

    }
}
