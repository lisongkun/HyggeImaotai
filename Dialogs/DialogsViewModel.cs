using System.Diagnostics;
using System.Windows.Input;
using hygge_imaotai.Dialogs.AddAccountDialog;
using hygge_imaotai.Dialogs.DirectAddAccountDialog;
using hygge_imaotai.Domain;
using hygge_imaotai.Entity;
using MaterialDesignThemes.Wpf;


namespace hygge_imaotai.Dialogs
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
