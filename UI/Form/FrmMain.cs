using Sunny.UI;
using System.Collections.Generic;
using System.Windows.Forms;
using HyggeImaotai.UI.Form.FrmMainPages;

namespace HyggeImaotai.UI.Form
{
    /// <summary>
    /// 主功能窗口
    /// </summary>
    public partial class FrmMain : UIForm
    {
        #region Field
        /// <summary>
        /// 子页面存放
        /// </summary>
        private readonly List<UIUserControl> _childPages = new List<UIUserControl>();

        #endregion

        #region Initial Methods
        /// <summary>
        /// No Arg Constructor
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            InitializeAllChildPage();
            AppContext.FrmMainGlobalInstance = this;
        }
        /// <summary>
        /// 初始化所有的子页面
        /// </summary>
        public void InitializeAllChildPage()
        {
            var userPageSubmitTask = new UserPageHome(this);
            tabPage1.Controls.Add(userPageSubmitTask);
            _childPages.Add(userPageSubmitTask);



            _childPages.ForEach(i => i.Dock = DockStyle.Fill);
        }
        /// <summary>
        /// 窗口加载完毕回调事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, System.EventArgs e)
        {

        }
        #endregion


    }
}
