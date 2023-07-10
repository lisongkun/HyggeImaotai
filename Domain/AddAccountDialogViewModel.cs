namespace hygge_imaotai.Domain
{
    public class AddAccountDialogViewModel : ViewModelBase
    {
        private string _phone;
        private string _phoneCode;


        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string PhoneCode
        {
            get => _phoneCode;
            set => SetProperty(ref _phoneCode, value);
        }
    }
}
