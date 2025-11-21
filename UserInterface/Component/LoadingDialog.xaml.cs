using System.Windows;
using System.Windows.Controls;

namespace HyggeIMaoTai.UserInterface.Component
{
    /// <summary>
    /// LoadingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingDialog : System.Windows.Controls.UserControl
    {
        public LoadingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置加载消息文本
        /// </summary>
        /// <param name="message">消息文本</param>
        public void SetMessage(string message)
        {
            MessageTextBlock.Text = message;
        }
    }
}

