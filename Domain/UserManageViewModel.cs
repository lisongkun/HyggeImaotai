namespace hygge_imaotai.Domain
{
    public class UserManageViewModel : ViewModelBase
    {
        private bool _isSelected;
        private string _phone;
        private string _userId;
        private string _token;
        private string _appointmentProjectCode;
        private string _province;
        private string _city;
        private string _latitude;
        private string _longitude;
        private char _type;
        private string _expireTime;


        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        public string AppointmentProjectCode
        {
            get => _appointmentProjectCode;
            set => SetProperty(ref _appointmentProjectCode, value);
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

        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public char Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public string ExpireTime
        {
            get => _expireTime;
            set => SetProperty(ref _expireTime, value);
        }
    }
}
