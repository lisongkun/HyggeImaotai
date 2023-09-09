using HyggeImaotai.UI.Base;
using System.Diagnostics;

namespace HyggeImaotai.UI.Form.FrmMainPages
{
    /// <summary>
    /// 首页 - 用户子页面 - Home
    /// </summary>
    public partial class UserPageHome : BaseUserPage
    {
        public UserPageHome(FrmMain frmMain)
        {
            InitializeComponent();
            _frmMain = frmMain;
        }

        #region Control Event
        /// <summary>
        /// 跳转到Github
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LlGithub_Click(object sender, System.EventArgs e)
        {
            // 跳转链接为:https://github.com/lisongkun/HyggeImaotai
            Process.Start(new ProcessStartInfo { FileName = "https://github.com/lisongkun/HyggeImaotai", UseShellExecute = true });
        }
        /// <summary>
        /// 跳转到Blog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LlBlog_Click(object sender, System.EventArgs e)
        {
            // 跳转链接为:https://www.lisok.cn/
            Process.Start(new ProcessStartInfo { FileName = "https://www.lisok.cn/", UseShellExecute = true });

        }
        /// <summary>
        /// 跳转到捐献页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LlDonate_Click(object sender, System.EventArgs e)
        {
            // 跳转链接为:http://donate.lisok.cn/#
            Process.Start(new ProcessStartInfo { FileName = "http://donate.lisok.cn/#", UseShellExecute = true });
        }
        #endregion


    }


}
