using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HyggeIMaoTai.UserInterface.Component
{
    /// <summary>
    /// Material Design 风格的分页控件
    /// </summary>
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
        private readonly List<Button> _simpleButtons = new();

        #endregion

        #region Constructor And Init

        /// <summary>
        /// 静态构造函数，注册默认样式
        /// </summary>
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }

        /// <summary>
        /// 初始化简单模式下的控件（页数小于等于5时）
        /// </summary>
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


        /// <summary>
        /// 更新简单模式下的控件状态（页数小于等于5时）
        /// </summary>
        private void UpdateControlSimple()
        {
            if (_simpleButtons.Count == 0) return;

            // 更新按钮可见性
            _btnCenterOne.Visibility = PageCount >= 1 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterTwo.Visibility = PageCount >= 2 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterThree.Visibility = PageCount >= 3 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterFour.Visibility = PageCount >= 4 ? Visibility.Visible : Visibility.Collapsed;
            _btnCenterFive.Visibility = PageCount >= 5 ? Visibility.Visible : Visibility.Collapsed;

            // 更新导航按钮状态
            _btnPrev.IsEnabled = CurrentPage > 1;
            _btnNext.IsEnabled = CurrentPage < PageCount;

            // 使用 Material Design 主题颜色
            var defaultBrush = TryFindResource("MaterialDesignPaper") as Brush ?? 
                               TryFindResource("PrimaryHueLightBrush") as Brush ?? 
                               new SolidColorBrush(Color.FromRgb(230, 230, 230));
            
            var activeBrush = TryFindResource("PrimaryHueMidBrush") as Brush ?? 
                             TryFindResource("PrimaryHueDarkBrush") as Brush ?? 
                             new SolidColorBrush(Color.FromRgb(103, 58, 183));

            // 重置所有按钮背景
            foreach (var btn in _simpleButtons)
            {
                btn.Background = defaultBrush;
            }

            // 设置当前页按钮背景
            if (CurrentPage > 0 && CurrentPage <= _simpleButtons.Count)
            {
                _simpleButtons[CurrentPage - 1].Background = activeBrush;
            }
        }


        /// <summary>
        /// 绑定简单模式下的按钮点击事件
        /// </summary>
        private void BindClickSimple()
        {
            _btnPrev.Click += (s, e) => 
            {
                if (CurrentPage > 1) CurrentPage -= 1;
            };
            
            _btnCenterOne.Click += (s, e) => CurrentPage = 1;
            _btnCenterTwo.Click += (s, e) => CurrentPage = 2;
            _btnCenterThree.Click += (s, e) => CurrentPage = 3;
            _btnCenterFour.Click += (s, e) => CurrentPage = 4;
            _btnCenterFive.Click += (s, e) => CurrentPage = 5;
            
            _btnNext.Click += (s, e) => 
            {
                if (CurrentPage < PageCount) CurrentPage += 1;
            };
        }

        /// <summary>
        /// 初始化完整模式下的控件（页数大于5时）
        /// </summary>
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
        /// <summary>
        /// 绑定完整模式下的按钮点击事件
        /// </summary>
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

        /// <summary>
        /// 更新完整模式下的控件状态（页数大于5时）
        /// </summary>
        private void UpdateControl()
        {
            // 更新导航按钮状态
            _btnPrev.IsEnabled = CurrentPage > 1;
            _btnNext.IsEnabled = CurrentPage < PageCount;

            // 更新按钮可见性逻辑
            _btnOne.Visibility = CurrentPage < 4 ? Visibility.Collapsed : Visibility.Visible;
            _btnDotPrev.Visibility = CurrentPage < 4 ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterOne.Visibility = CurrentPage != 3 && CurrentPage != PageCount ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterTwo.Visibility = CurrentPage == 1 || (PageCount - CurrentPage) == 2 ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterFour.Visibility = CurrentPage == 3 || CurrentPage == PageCount ? Visibility.Collapsed : Visibility.Visible;
            _btnCenterFive.Visibility = CurrentPage != 1 && (PageCount - CurrentPage) != 2 ? Visibility.Collapsed : Visibility.Visible;
            _btnDotNext.Visibility = PageCount - CurrentPage < 3 ? Visibility.Collapsed : Visibility.Visible;
            _btnLast.Visibility = PageCount - CurrentPage < 3 ? Visibility.Collapsed : Visibility.Visible;

            // 更新按钮内容
            _btnOne.Content = 1;
            _btnCenterOne.Content = CurrentPage - 2;
            _btnCenterTwo.Content = CurrentPage - 1;
            _btnCenterThree.Content = CurrentPage;
            _btnCenterFour.Content = CurrentPage + 1;
            _btnCenterFive.Content = CurrentPage + 2;
            _btnLast.Content = PageCount;

            // 使用 Material Design 主题颜色更新按钮样式
            var defaultBrush = TryFindResource("MaterialDesignPaper") as Brush ?? 
                               TryFindResource("PrimaryHueLightBrush") as Brush ?? 
                               new SolidColorBrush(Color.FromRgb(230, 230, 230));
            
            var activeBrush = TryFindResource("PrimaryHueMidBrush") as Brush ?? 
                             TryFindResource("PrimaryHueDarkBrush") as Brush ?? 
                             new SolidColorBrush(Color.FromRgb(103, 58, 183));

            // 重置所有中心按钮背景
            _btnCenterOne.Background = defaultBrush;
            _btnCenterTwo.Background = defaultBrush;
            _btnCenterThree.Background = activeBrush; // 当前页
            _btnCenterFour.Background = defaultBrush;
            _btnCenterFive.Background = defaultBrush;
        }

        /// <summary>
        /// 应用模板时初始化控件
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 根据页数选择初始化模式
            if (PageCount > 5)
            {
                InitControls();
            }
            else
            {
                InitControlsSimple();
            }
        }

        /// <summary>
        /// 设置页面索引（相对当前页的偏移量）
        /// </summary>
        /// <param name="page">页面偏移量，负数表示向前，正数表示向后</param>
        public void SetIndex(int page)
        {
            switch (page)
            {
                case < 0:
                    {
                        // 向前翻页
                        var newPage = CurrentPage + page;
                        if (newPage > 0)
                        {
                            CurrentPage = newPage;
                        }
                        break;
                    }
                case > 0:
                    {
                        // 向后翻页
                        var newPage = CurrentPage + page;
                        if (newPage <= PageCount)
                        {
                            CurrentPage = newPage;
                        }
                        break;
                    }
            }
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// 总页数
        /// </summary>
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

        /// <summary>
        /// 是否为简单模式（页数小于等于5时）
        /// </summary>
        public bool IsSimple
        {
            get => (bool)GetValue(IsSimpleProperty);
            set => SetValue(IsSimpleProperty, value);
        }
        
        public static readonly DependencyProperty IsSimpleProperty =
            DependencyProperty.Register(nameof(IsSimple), typeof(bool), typeof(Pagination), new PropertyMetadata(false));

        /// <summary>
        /// 当前页码（从1开始）
        /// </summary>
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
        /// 页面变更命令，当页码改变时执行
        /// </summary>
        public ICommand UpdatePageCommand
        {
            get => (ICommand)GetValue(UpdatePageCommandProperty);
            set => SetValue(UpdatePageCommandProperty, value);
        }

        public static readonly DependencyProperty UpdatePageCommandProperty =
            DependencyProperty.Register(
                nameof(UpdatePageCommand),
                typeof(ICommand),
                typeof(Pagination),
                new PropertyMetadata(null));

        #endregion

        #region Properties Change Hook

        /// <summary>
        /// 当前页变更时的回调处理
        /// </summary>
        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (d is not Pagination pagination) return;

            // 确保当前页在有效范围内
            if (pagination.CurrentPage < 1)
            {
                pagination.CurrentPage = 1;
                return;
            }
            if (pagination.CurrentPage > pagination.PageCount && pagination.PageCount > 0)
            {
                pagination.CurrentPage = pagination.PageCount;
                return;
            }

            // 根据页数更新控件
            if (pagination.PageCount > 5)
            {
                pagination.UpdateControl();
            }
            else
            {
                pagination.UpdateControlSimple();
            }

            // 执行页面变更命令
            pagination.UpdatePageCommand?.Execute(pagination.CurrentPage);
        }

        #endregion
    }
}
