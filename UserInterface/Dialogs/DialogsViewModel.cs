using System.Windows.Input;
using HyggeIMaoTai.Domain;
using HyggeIMaoTai.Entity;
using HyggeIMaoTai.UserInterface.Dialogs.AddAccountDialog;
using HyggeIMaoTai.UserInterface.Dialogs.DirectAddAccountDialog;
using MaterialDesignThemes.Wpf;

namespace HyggeIMaoTai.UserInterface.Dialogs
{
    public class DialogsViewModel : ViewModelBase
    {
        /// <summary>
        /// 定义普通添加账号的弹窗
        /// </summary>
        public ICommand RunAddAccountDialogCommand => new AnotherCommandImplementation(ExecuteRunAddAccountDialog);

        public ICommand RunDirectAddAccountDialogCommand =>
            new AnotherCommandImplementation(ExecuteRunDirectAddAccountDialog);

        private async void ExecuteRunAddAccountDialog(object? _)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddAccountDialogUserControl(new AddAccountDialogViewModel());

            //show the dialog
            await DialogHost.Show(view, "RootDialog");
        }

        private async void ExecuteRunDirectAddAccountDialog(object? _)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new DirectAddAccountDialogUserControl(new UserEntity());

            //show the dialog
            await DialogHost.Show(view, "RootDialog");
        }
    }
}
