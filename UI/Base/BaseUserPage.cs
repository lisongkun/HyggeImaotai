using HyggeImaotai.UI.Form;
using NLog;
using Sunny.UI;

namespace HyggeImaotai.UI.Base
{
    /// <summary>
    /// 所有自定义用户控件页面的父类
    /// </summary>

    public class BaseUserPage : UIUserControl
    {
        #region Fields
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected FrmMain _frmMain;

        #endregion

        #region Initial Methods


        /// <summary>
        /// 对控件属性进行初始化的方法
        /// </summary>
        public virtual void ControlDefaultSetting() { }
        #endregion

    }
}
