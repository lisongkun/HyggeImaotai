namespace HyggeImaotai.UI.Form
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.TcmLeftMenu = new Sunny.UI.UITabControlMenu();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.TcmLeftMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TcmLeftMenu
            // 
            this.TcmLeftMenu.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TcmLeftMenu.Controls.Add(this.tabPage1);
            this.TcmLeftMenu.Controls.Add(this.tabPage2);
            this.TcmLeftMenu.Controls.Add(this.tabPage3);
            this.TcmLeftMenu.Controls.Add(this.tabPage4);
            this.TcmLeftMenu.Controls.Add(this.tabPage5);
            this.TcmLeftMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TcmLeftMenu.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TcmLeftMenu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcmLeftMenu.Location = new System.Drawing.Point(0, 35);
            this.TcmLeftMenu.Multiline = true;
            this.TcmLeftMenu.Name = "TcmLeftMenu";
            this.TcmLeftMenu.SelectedIndex = 0;
            this.TcmLeftMenu.Size = new System.Drawing.Size(1265, 741);
            this.TcmLeftMenu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TcmLeftMenu.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(201, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1064, 741);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "首页";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(201, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1064, 741);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "用户管理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(201, 0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1064, 741);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "预约项目";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(201, 0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1064, 741);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "门店列表";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(201, 0);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1064, 741);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "日志";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1265, 776);
            this.Controls.Add(this.TcmLeftMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.ShowTitleIcon = true;
            this.Text = "Hygge Imaotai";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.TcmLeftMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITabControlMenu TcmLeftMenu;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
    }
}

