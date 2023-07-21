using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace hygge_imaotai.UserInterface.Component
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:hygge_imaotai.UserInterface.Component"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:hygge_imaotai.UserInterface.Component;assembly=hygge_imaotai.UserInterface.Component"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:Pagination/>
    ///
    /// </summary>
    public class Pagination : Control
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }
        private Button _btnPrev = null;
        private Button _btnOne = null;
        private Button _btnDotPrev = null;
        private Button _btnCenterOne = null;
        private Button _btnCenterTwo = null;
        private Button _btnCenterThree = null;
        private Button _btnCenterFour = null;
        private Button _btnCenterFive = null;
        private Button _btnDotNext = null;
        private Button _btnLast = null;
        private Button _btnNext = null;
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(Pagination), new PropertyMetadata(1, (d, e) =>
            {
                if (!(d is Pagination pagination)) return;
                var page = (int)e.NewValue;
                pagination.IsSimple = page < 6;
            }));
        public bool IsSimple
        {
            get { return (bool)GetValue(IsSimpleProperty); }
            set { SetValue(IsSimpleProperty, value); }
        }
        public static readonly DependencyProperty IsSimpleProperty =
            DependencyProperty.Register("IsSimple", typeof(bool), typeof(Pagination), new PropertyMetadata(false));


        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination), new PropertyMetadata(1, (d, e) =>
            {
                if (!(d is Pagination pagination)) return;
                if (pagination.PageCount > 5)
                {
                    pagination.UpdateControl();
                }
                else
                {
                    pagination.UpdateControlSimple();
                }
            }));
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
        private List<Button> _simpleButtons = new List<Button>();
        private void InitControlsSimple()
        {
            _btnPrev = GetTemplateChild("btnPrev") as Button;
            _btnCenterOne = GetTemplateChild("btnCenterOne") as Button;
            _btnCenterTwo = GetTemplateChild("btnCenterTwo") as Button;
            _btnCenterThree = GetTemplateChild("btnCenterThree") as Button;
            _btnCenterFour = GetTemplateChild("btnCenterFour") as Button;
            _btnCenterFive = GetTemplateChild("btnCenterFive") as Button;
            _btnNext = GetTemplateChild("btnNext") as Button;
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
            _btnPrev = GetTemplateChild("btnPrev") as Button;
            _btnOne = GetTemplateChild("btnOne") as Button;
            _btnDotPrev = GetTemplateChild("btnDotPrev") as Button;
            _btnCenterOne = GetTemplateChild("btnCenterOne") as Button;
            _btnCenterTwo = GetTemplateChild("btnCenterTwo") as Button;
            _btnCenterThree = GetTemplateChild("btnCenterThree") as Button;
            _btnCenterFour = GetTemplateChild("btnCenterFour") as Button;
            _btnCenterFive = GetTemplateChild("btnCenterFive") as Button;
            _btnDotNext = GetTemplateChild("btnDotNext") as Button;
            _btnLast = GetTemplateChild("btnLast") as Button;
            _btnNext = GetTemplateChild("btnNext") as Button;
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
        public void SetIndex(int page)
        {
            if (page < 0)
            {
                if (CurrentPage + page > 0)
                {
                    CurrentPage += page;
                }
            }
            else if (page > 0)
            {
                if (CurrentPage + page <= PageCount)
                {
                    CurrentPage += page;
                }
            }
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
    }
}
