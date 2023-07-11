using hygge_imaotai.Domain;

namespace hygge_imaotai.Dialogs.AddAccountDialog
{
    public class AddAccountDialogViewModel : ViewModelBase
    {
        private string _phone;
        private string _phoneCode;
        private string _remainingText = "������֤��";


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

        public string RemainingText
        {
            get => _remainingText;
            set => SetProperty(ref _remainingText, value);
        }
    }
}
