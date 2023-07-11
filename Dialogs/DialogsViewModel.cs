using System.Diagnostics;
using System.Windows.Input;
using hygge_imaotai.Dialogs.AddAccountDialog;
using hygge_imaotai.Domain;
using MaterialDesignThemes.Wpf;

namespace hygge_imaotai.Dialogs
{
    public class DialogsViewModel : ViewModelBase
    {
        public ICommand RunAddAccountDialogCommand => new AnotherCommandImplementation(ExecuteRunAddAccountDialog);

        private async void ExecuteRunAddAccountDialog(object? _)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddAccountDialog.AddAccountDialog(new AddAccountDialogViewModel());

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("You can intercept the closing event, and cancel here.");
    }
}
