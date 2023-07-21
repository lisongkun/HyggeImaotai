using hygge_imaotai.Domain;

namespace hygge_imaotai.UserInterface.Dialogs.AddAccountDialog
{
    public class AddAccountDialogViewModel : ViewModelBase
    {
        private string _phone;
        private string _phoneCode;
        private string _remainingText = "·¢ËÍÑéÖ¤Âë";


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
