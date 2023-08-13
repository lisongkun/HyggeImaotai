using System;
using System.Windows;
using System.Windows.Controls;
using hygge_imaotai.Domain;
using MaterialDesignThemes.Wpf;

namespace hygge_imaotai.Entity
{
    public class DemoItem : ViewModelBase
    {
        private readonly Type _contentType;
        private readonly object? _dataContext;

        private object? _content;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
        private Thickness _marginRequirement = new(16);


        public DemoItem(string name, Type contentType,
            PackIconKind selectedIcon, PackIconKind unselectedIcon, object? dataContext = null)
        {
            Name = name;
            _contentType = contentType;
            _dataContext = dataContext;
            SelectedIcon = selectedIcon;
            UnselectedIcon = unselectedIcon;
        }

        public string Name { get; }


        public object? Content => _content ??= CreateContent();

        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }


        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get => _horizontalScrollBarVisibilityRequirement;
            set => SetProperty(ref _horizontalScrollBarVisibilityRequirement, value);
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get => _verticalScrollBarVisibilityRequirement;
            set => SetProperty(ref _verticalScrollBarVisibilityRequirement, value);
        }

        public Thickness MarginRequirement
        {
            get => _marginRequirement;
            set => SetProperty(ref _marginRequirement, value);
        }

        private object? CreateContent()
        {
            var content = Activator.CreateInstance(_contentType);
            if (_dataContext != null && content is FrameworkElement element)
            {
                element.DataContext = _dataContext;
            }

            return content;
        }

    }
}
