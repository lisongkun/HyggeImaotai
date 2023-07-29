using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace hygge_imaotai.UserInterface.Component
{
    public class Pagination : Control
    {

        #region Fields

        private Button _btnPrev = null!;
        private Button _btnOne = null!;
        private Button _btnDotPrev = null!;
        private Button _btnCenterOne = null!;
        private Button _btnCenterTwo = null!;
        private Button _btnCenterThree = null!;
        private Button _btnCenterFour = null!;
        private Button _btnCenterFive = null!;
        private Button _btnDotNext = null!;
        private Button _btnLast = null!;
        private Button _btnNext = null!;

        #endregion

        #region Constructor And Init

        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }

        private void InitControlsSimple()
        {
            _btnPrev = (GetTemplateChild("btnPrev") as Button)!;
            _btnCenterOne = (GetTemplateChild("btnCenterOne") as Button)!;
            _btnCenterTwo = (GetTemplateChild("btnCenterTwo") as Button)!;
            _btnCenterThree = (GetTemplateChild("btnCenterThree") as Button)!;
            _btnCenterFour = (GetTemplateChild("btnCenterFour") as Button)!;
            _btnCenterFive = (GetTemplateChild("btnCenterFive") as Button)!;
            _btnNext = (GetTemplateChild("btnNext") as Button)!;
            _simpleButtons.Clear();
            _simpleButtons.Add(_btnCenterOne);
            _simpleButtons.Add(_btnCenterTwo);
            _simpleButtons.Add(_btnCenterThree);
            _simpleButtons.Add(_btnCenterFour);
            _simpleButtons.Add(_btnCenterFive);
            BindClickSimple();
            UpdateControlSimple();
        }


        private void UpdateControlSimple()
        {
            _btnCenterOne.Visibility = PageCount >= 1 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterTwo.Visibility = PageCount >= 2 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterThree.Visibility = PageCount >= 3 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterFour.Visibility = PageCount >= 4 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterFive.Visibility = PageCount >= 5 ? Visibility.Visible : Visibility.Collapsed;
            _btnPrev.IsEnabled = CurrentPage > 1;
            _btnNext.IsEnabled = CurrentPage < PageCount;
            _btnCenterOne.Background = _btnCenterTwo.Background = _btnCenterThree.Background = _btnCenterFour.Background = _btnCenterFive.Background = Brushes.LightBlue;
            _simpleButtons[CurrentPage - 1].Background = Brushes.Green;
        }


        private void BindClickSimple()
        {
            _btnPrev.Click += (s, e) => CurrentPage -= 1;
            _btnCenterOne.Click += (s, e) => CurrentPage = 1;
            _btnCenterTwo.Click += (s, e) => CurrentPage = 2;
            _btnCenterThree.Click += (s, e) => CurrentPage = 3;
            _btnCenterFour.Click += (s, e) => CurrentPage = 4;
            _btnCenterFive.Click += (s, e) => CurrentPage = 5;
            _btnNext.Click += (s, e) => CurrentPage += 1;
        }


        private void InitControls()
        {
            _btnPrev = (GetTemplateChild("btnPrev") as Button)!;
            _btnOne = (GetTemplateChild("btnOne") as Button)!;
            _btnDotPrev = (GetTemplateChild("btnDotPrev") as Button)!;
            _btnCenterOne = (GetTemplateChild("btnCenterOne") as Button)!;
            _btnCenterTwo = (GetTemplateChild("btnCenterTwo") as Button)!;
            _btnCenterThree = (GetTemplateChild("btnCenterThree") as Button)!;
            _btnCenterFour = (GetTemplateChild("btnCenterFour") as Button)!;
            _btnCenterFive = (GetTemplateChild("btnCenterFive") as Button)!;
            _btnDotNext = (GetTemplateChild("btnDotNext") as Button)!;
            _btnLast = (GetTemplateChild("btnLast") as Button)!;
            _btnNext = (GetTemplateChild("btnNext") as Button)!;
            BindClick();
            UpdateControl();
        }
        private void BindClick()
        {
            _btnPrev.Click += (s, e) => SetIndex(-1);
            _btnOne.Click += (s, e) => SetIndex(1 - CurrentPage);
            _btnDotPrev.Click += (s, e) => SetIndex(-3);
            _btnCenterOne.Click += (s, e) => SetIndex(-2);
            _btnCenterTwo.Click += (s, e) => SetIndex(-1);
            _btnCenterFour.Click += (s, e) => SetIndex(1);
            _btnCenterFive.Click += (s, e) => SetIndex(2);
            _btnDotNext.Click += (s, e) => SetIndex(3);
            _btnLast.Click += (s, e) => SetIndex(PageCount - CurrentPage);
            _btnNext.Click += (s, e) => SetIndex(1);
        }

        private void UpdateControl()
        {
            _btnPrev.IsEnabled = CurrentPage > 1;
            _btnOne.Visibility = CurrentPage < 4 ? Visibility.Collapsed : Visibility.Visible;
            _btnDotPrev.Visibility = CurrentPage < 4 ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterOne.Visibility = CurrentPage != 3 && CurrentPage != PageCount ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterTwo.Visibility = CurrentPage == 1 || (PageCount - CurrentPage) == 2 ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterFour.Visibility = CurrentPage == 3 || CurrentPage == PageCount ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterFive.Visibility = CurrentPage != 1 && (PageCount - CurrentPage) != 2 ? Visibility.Collapsed : Visibility.Visible;
            _btnDotNext.Visibility = PageCount - CurrentPage < 3 ? Visibility.Collapsed : Visibility.Visible;
            _btnLast.Visibility = PageCount - CurrentPage < 3 ? Visibility.Collapsed : Visibility.Visible;
            _btnNext.IsEnabled = CurrentPage != PageCount;


            _btnOne.Content = 1;
            _btnCenterOne.Content = CurrentPage - 2;
            _btnCenterTwo.Content = CurrentPage - 1;
            _btnCenterThree.Content = CurrentPage;
            _btnCenterFour.Content = CurrentPage + 1;
            _btnCenterFive.Content = CurrentPage + 2;
            _btnLast.Content = PageCount;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            if (PageCount > 5)
            {
                InitControls();
            }
            else
            {
                InitControlsSimple();
            }
        }
        private readonly List<Button> _simpleButtons = new();

        public void SetIndex(int page)
        {
            switch (page)
            {
                case < 0:
                    {
                        if (CurrentPage + page > 0)
                        {
                            CurrentPage += page;
                        }

                        break;
                    }
                case > 0:
                    {
                        if (CurrentPage + page <= PageCount)
                        {
                            CurrentPage += page;
                        }

                        break;
                    }
            }
        }

        #endregion


        public int PageCount
        {
            get => (int)GetValue(PageCountProperty);
            set => SetValue(PageCountProperty, value);
        }

        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register(nameof(PageCount), typeof(int), typeof(Pagination), new PropertyMetadata(1,
                (d, e) =>
                {
                    if (d is not Pagination pagination) return;
                    var page = (int)e.NewValue;
                    pagination.IsSimple = page < 6;
                }));

        public bool IsSimple
        {
            get => (bool)GetValue(IsSimpleProperty);
            set => SetValue(IsSimpleProperty, value);
        }
        public static readonly DependencyProperty IsSimpleProperty =
            DependencyProperty.Register(nameof(IsSimple), typeof(bool), typeof(Pagination), new PropertyMetadata(false));


        public int CurrentPage
        {
            get => (int)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(int),
                typeof(Pagination),
                new PropertyMetadata(1, OnCurrentPageChanged));

        /// <summary>
        /// 用于处理currentPage被修改
        /// </summary>
        public static readonly DependencyProperty UpdatePageCommandProperty =
            DependencyProperty.Register(
                               nameof(UpdatePageCommand),
                                              typeof(ICommand),
                                              typeof(Pagination),
                                              new PropertyMetadata(null));

        public ICommand UpdatePageCommand
        {
            get => (ICommand)GetValue(UpdatePageCommandProperty);
            set => SetValue(UpdatePageCommandProperty, value);
        }


        #region Properties Change Hook

        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (d is not Pagination pagination) return;
            if (pagination.PageCount > 5)
            {
                pagination.UpdateControl();
            }
            else
            {
                pagination.UpdateControlSimple();
            }
            var control = (Pagination)d;
            control.UpdatePageCommand?.Execute(pagination.CurrentPage);
        }

        #endregion
    }
}
