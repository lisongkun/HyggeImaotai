namespace HyggeImaotai.UI.Form.FrmMainPages
{
    partial class UserPageHome
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPageHome));
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LlGithub = new Sunny.UI.UILinkLabel();
            this.LlBlog = new Sunny.UI.UILinkLabel();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.LlDonate = new Sunny.UI.UILinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.uiTitlePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(372, 56);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(439, 67);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "欢迎使用i茅台预约小助手";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(177, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(173, 151);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // LlGithub
            // 
            this.LlGithub.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.LlGithub.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LlGithub.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.LlGithub.Location = new System.Drawing.Point(375, 152);
            this.LlGithub.Name = "LlGithub";
            this.LlGithub.Size = new System.Drawing.Size(100, 23);
            this.LlGithub.TabIndex = 2;
            this.LlGithub.TabStop = true;
            this.LlGithub.Text = "Github";
            this.LlGithub.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.LlGithub.Click += new System.EventHandler(this.LlGithub_Click);
            // 
            // LlBlog
            // 
            this.LlBlog.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.LlBlog.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LlBlog.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.LlBlog.Location = new System.Drawing.Point(711, 152);
            this.LlBlog.Name = "LlBlog";
            this.LlBlog.Size = new System.Drawing.Size(100, 23);
            this.LlBlog.TabIndex = 3;
            this.LlBlog.TabStop = true;
            this.LlBlog.Text = "Blog";
            this.LlBlog.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.LlBlog.Click += new System.EventHandler(this.LlBlog_Click);
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Controls.Add(this.LlDonate);
            this.uiTitlePanel1.Controls.Add(this.uiLabel3);
            this.uiTitlePanel1.Controls.Add(this.uiLabel2);
            this.uiTitlePanel1.Controls.Add(this.pictureBox2);
            this.uiTitlePanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTitlePanel1.Location = new System.Drawing.Point(4, 345);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.ShowText = false;
            this.uiTitlePanel1.Size = new System.Drawing.Size(1056, 382);
            this.uiTitlePanel1.TabIndex = 4;
            this.uiTitlePanel1.Text = "Open Source";
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(13, 46);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(163, 164);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.Silver;
            this.uiLabel2.Location = new System.Drawing.Point(182, 82);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(859, 88);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.TabIndex = 1;
            this.uiLabel2.Text = "This project is completely open source. If you like it and want to say thanks you" +
    " could hit the GitHub Star button, tweet or post about it, or tell your mum abou" +
    "t it!";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.Silver;
            this.uiLabel3.Location = new System.Drawing.Point(20, 265);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(890, 58);
            this.uiLabel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel3.TabIndex = 2;
            this.uiLabel3.Text = "Feel like you want to make a donation ? It would be gratefully received. Click th" +
    "e button to donate via Open Collective.";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LlDonate
            // 
            this.LlDonate.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.LlDonate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LlDonate.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.LlDonate.Location = new System.Drawing.Point(931, 281);
            this.LlDonate.Name = "LlDonate";
            this.LlDonate.Size = new System.Drawing.Size(100, 23);
            this.LlDonate.TabIndex = 3;
            this.LlDonate.TabStop = true;
            this.LlDonate.Text = "Donate";
            this.LlDonate.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.LlDonate.Click += new System.EventHandler(this.LlDonate_Click);
            // 
            // UserPageHome
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uiTitlePanel1);
            this.Controls.Add(this.LlBlog);
            this.Controls.Add(this.LlGithub);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.uiLabel1);
            this.Name = "UserPageHome";
            this.Size = new System.Drawing.Size(1064, 741);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.uiTitlePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UILinkLabel LlGithub;
        private Sunny.UI.UILinkLabel LlBlog;
        private Sunny.UI.UITitlePanel uiTitlePanel1;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Sunny.UI.UILinkLabel LlDonate;
    }
}
