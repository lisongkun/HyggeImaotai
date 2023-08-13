using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using hygge_imaotai.Entity;
using hygge_imaotai.UserInterface.UserControl;
using hygge_imaotai.UserInterface.UserControls;
using MaterialDesignThemes.Wpf;

namespace hygge_imaotai.Domain
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            DemoItems = new ObservableCollection<DemoItem>
            {
              new DemoItem(
                    "Home",
                    typeof(HomeUserControl),
                    selectedIcon: PackIconKind.Home,
                    unselectedIcon: PackIconKind.HomeOutline)
            };

            foreach (var item in GenerateDemoItems(snackbarMessageQueue))
            {
                DemoItems.Add(item);
            }



            _demoItemsView = CollectionViewSource.GetDefaultView(DemoItems);
            _demoItemsView.Filter = DemoItemsFilter;

            HomeCommand = new AnotherCommandImplementation(
                _ =>
                {
                    SearchKeyword = string.Empty;
                    SelectedIndex = 0;
                });

            MovePrevCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!string.IsNullOrWhiteSpace(SearchKeyword))
                        SearchKeyword = string.Empty;

                    SelectedIndex--;
                },
                _ => SelectedIndex > 0);

            MoveNextCommand = new AnotherCommandImplementation(
               _ =>
               {
                   if (!string.IsNullOrWhiteSpace(SearchKeyword))
                       SearchKeyword = string.Empty;

                   SelectedIndex++;
               },
               _ => SelectedIndex < DemoItems.Count - 1);

        }

        private readonly ICollectionView _demoItemsView;
        private DemoItem? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;
        private bool _controlsEnabled = true;

        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    _demoItemsView.Refresh();
                }
            }
        }

        public ObservableCollection<DemoItem> DemoItems { get; }

        public DemoItem? SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        public bool ControlsEnabled
        {
            get => _controlsEnabled;
            set => SetProperty(ref _controlsEnabled, value);
        }

        public AnotherCommandImplementation HomeCommand { get; }
        public AnotherCommandImplementation MovePrevCommand { get; }
        public AnotherCommandImplementation MoveNextCommand { get; }


        private static IEnumerable<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue is null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));


            yield return new DemoItem(
                "用户管理",
                typeof(UserManageControl),
                selectedIcon: PackIconKind.User,
                unselectedIcon: PackIconKind.User);

            yield return new DemoItem(
                "预约项目",
                typeof(AppointProjectUserControl),
                selectedIcon: PackIconKind.FileDocument,
                unselectedIcon: PackIconKind.FileDocument);

            yield return new DemoItem(
                "店铺管理",
                typeof(ShopManageUserControl),
                selectedIcon: PackIconKind.Store,
                unselectedIcon:PackIconKind.Store
            );

            yield return new DemoItem(
                "日志管理",
                typeof(LogUserControl),
                selectedIcon: PackIconKind.Book,
                unselectedIcon: PackIconKind.Book);

        }

        private bool DemoItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchKeyword))
            {
                return true;
            }

            return obj is DemoItem item
                   && item.Name.ToLower().Contains(_searchKeyword!.ToLower());
        }
    }
}
