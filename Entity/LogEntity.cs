using System;
using System.Security.Principal;
using FreeSql.DataAnnotations;
using hygge_imaotai.Domain;

namespace hygge_imaotai.Entity
{
    /// <summary>
    /// 日志的模型
    /// </summary>
    public class LogEntity : ViewModelBase
    {
        #region Fields

        private int _id;
        private string _status;
        private string _mobilePhone;
        private string _content;
        private string _response;
        private DateTime _createTime;

        #endregion
        #region Properties
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public string MobilePhone
        {
            get => _mobilePhone;
            set => SetProperty(ref _mobilePhone, value);
        }

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public string Response
        {
            get => _response;
            set => SetProperty(ref _response, value);
        }

        public DateTime CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }

        #endregion

        #region Construct
        /// <summary>
        /// No Parameter Construct
        /// </summary>
        public LogEntity()
        {
        }

        public LogEntity(int id, string status, string mobilePhone, string content, string response, DateTime createTime)
        {
            Id = id;
            Status = status;
            MobilePhone = mobilePhone;
            Content = content;
            Response = response;
            CreateTime = createTime;
        }

        #endregion

        #region Functions

        

        #endregion
    }
}
