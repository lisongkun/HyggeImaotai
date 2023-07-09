using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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

            foreach (var item in GenerateDemoItems(snackbarMessageQueue).OrderBy(i => i.Name))
            {
                DemoItems.Add(item);
            }

            MainDemoItems = new ObservableCollection<DemoItem>
            {
                DemoItems.First(x => x.Name == "Home"),
                //DemoItems.First(x => x.Name == "Buttons"),
                //DemoItems.First(x => x.Name == "Toggles"),
                //DemoItems.First(x => x.Name == "Fields"),
                //DemoItems.First(x => x.Name == "Pickers")
            };

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
        public ObservableCollection<DemoItem> MainDemoItems { get; }

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
                "Rating Bar",
                typeof(RatingBar),
                selectedIcon: PackIconKind.Star,
                unselectedIcon: PackIconKind.StarOutline);


            yield return new DemoItem(
                "Typography",
                typeof(Typography),
                selectedIcon: PackIconKind.FormatSize,
                unselectedIcon: PackIconKind.FormatTitle)
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            

            yield return new DemoItem(
                "Expander",
                typeof(Expander),
                selectedIcon: PackIconKind.UnfoldMoreHorizontal,
                unselectedIcon: PackIconKind.UnfoldMoreHorizontal);


            yield return new DemoItem(
                "Elevation",
                typeof(Elevation),
                selectedIcon: PackIconKind.BoxShadow,
                unselectedIcon: PackIconKind.BoxShadow);
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
