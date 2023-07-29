using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using hygge_imaotai.UserInterface.Component;
using NLog;

namespace hygge_imaotai.UserInterface.Dialogs.AddAccountDialog
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class AddAccountDialogUserControl
    {
        #region Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        #endregion

        private IMTService service = new();
        private DispatcherTimer timer;
        private AddAccountDialogViewModel _dataContext;
        private int _remainingTime;

        public AddAccountDialogUserControl(AddAccountDialogViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _dataContext = (dataContext as AddAccountDialogViewModel)!;
            // 创建一个DispatcherTimer实例
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 设置时间间隔为1秒
            };
            timer.Tick += Timer_Tick; // 指定计时器触发事件的处理方法
        }

        // 计时器触发事件的处理方法
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_remainingTime > 0)
            {
                _remainingTime--; // 每秒减少1秒
                _dataContext.RemainingText = $"剩余{_remainingTime}秒...";
            }
            else
            {
                SendPhoneCodeButton.IsEnabled = true;
                _dataContext.RemainingText = "发送验证码";
                timer.Stop();
            }
        }

        private async void SendPhoneCodeButton_OnClick(object sender, RoutedEventArgs e)
        {
            string phone = _dataContext.Phone;
            try
            {
                var sendFlag = await service.SendCode(phone);
                SendPhoneCodeButton.IsEnabled = false;
                _remainingTime = 60;
                timer.Start();
                new MessageBoxCustom("发送验证码成功!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                new MessageBoxCustom($"发送验证码失败!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }


        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var phone = _dataContext.Phone;
            var code = _dataContext.PhoneCode;
            try
            {
                await service.Login(phone, code);
                new MessageBoxCustom("用户登录成功!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception exception)
            {
                Logger.Error($"用户登录失败!原因:{exception.Message}");
                new MessageBoxCustom($"用户登录失败!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
