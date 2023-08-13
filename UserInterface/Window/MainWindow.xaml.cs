using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using hygge_imaotai.Domain;
using MaterialDesignThemes.Wpf;



namespace hygge_imaotai.UserInterface.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {


        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(() => Thread.Sleep(2500)).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue?.Enqueue("欢迎使用 i茅台预约小助手");
            }, TaskScheduler.FromCurrentSynchronizationContext());
            var dataContextCopy = new MainWindowViewModel(MainSnackbar.MessageQueue!);
            dataContextCopy.SelectedItem = dataContextCopy.DemoItems[0];
            DataContext = dataContextCopy;
        }
        private void OnCopy(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string stringValue)
            {
                try
                {
                    Clipboard.SetDataObject(stringValue);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth <= 700)
            {
                NavRail.Visibility = Visibility.Collapsed;
                NavDrawer.OpenMode = DrawerHostOpenMode.Modal;
                NavDrawer.IsLeftDrawerOpen = false;
                MenuToggleButton.Visibility = Visibility.Visible;
                FAB.Visibility = Visibility.Visible;
                DrawerFAB.Visibility = Visibility.Collapsed;
            }
            else if (ActualWidth > 700 && ActualWidth <= 1600)
            {
                NavRail.Visibility = Visibility.Visible;
                NavDrawer.OpenMode = DrawerHostOpenMode.Modal;
                NavDrawer.IsLeftDrawerOpen = false;
                MenuToggleButton.Visibility = Visibility.Visible;
                FAB.Visibility = Visibility.Collapsed;
                DrawerFAB.Visibility = Visibility.Collapsed;
            }
            else if (ActualWidth > 1600)
            {
                NavRail.Visibility = Visibility.Collapsed;
                NavDrawer.OpenMode = DrawerHostOpenMode.Standard;
                NavDrawer.IsLeftDrawerOpen = true;
                MenuToggleButton.Visibility = Visibility.Collapsed;
                FAB.Visibility = Visibility.Collapsed;
                DrawerFAB.Visibility = Visibility.Visible;
            }
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e) => Link.OpenInBrowser(ConfigurationManager.AppSettings["GitHub"]);

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            NavDrawer.IsLeftDrawerOpen = false;
            if (!(ActualWidth > 1600)) return;
            NavRail.Visibility = Visibility.Visible;
            MenuToggleButton.Visibility = Visibility.Visible;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NavDrawer.OpenMode is DrawerHostOpenMode.Standard) return;
            //until we had a StaysOpen flag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            DemoItemsSearchBox.Focus();
            if (!(ActualWidth > 1600)) return;
            NavRail.Visibility = Visibility.Collapsed;
            MenuToggleButton.Visibility = Visibility.Collapsed;
        }

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
            => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
            => MainScrollViewer.ScrollToHome();

        private void TaskbarIcon_OnTrayLeftMouseUp(object sender, RoutedEventArgs e)
        {
            this.Visibility = this.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
