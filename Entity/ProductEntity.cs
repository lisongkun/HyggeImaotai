using System;
using hygge_imaotai.Domain;

namespace hygge_imaotai.Entity
{
    public class ProductEntity:ViewModelBase
    {
        #region Fields
        private string _code;
        private string _title;
        private string _description;
        private string _img;
        private DateTime _created;


        #endregion


        #region Construct

        public ProductEntity()
        {
        }

        public ProductEntity(string code, string title, string description, string img, DateTime created)
        {
            _code = code;
            _title = title;
            _description = description;
            _img = img;
            _created = created;
        }

        #endregion

        #region Properties

        public string Code
        {
            get => _code;
            set => SetProperty(ref _code, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Img
        {
            get => _img;
            set => SetProperty(ref _img, value);
        }

        public DateTime Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }

        #endregion





    }
}
