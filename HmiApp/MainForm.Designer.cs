namespace HmiApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbi1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi2 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSLogo = new DevExpress.XtraBars.BarStaticItem();
            this.barSTime = new DevExpress.XtraBars.BarStaticItem();
            this.bbi3 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi4 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbiexit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bar5 = new DevExpress.XtraBars.Bar();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbi1,
            this.bbi2,
            this.barSTime,
            this.bbiexit,
            this.barButtonItem1,
            this.barSLogo,
            this.barLargeButtonItem1,
            this.bbi3,
            this.bbi4});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 9;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            this.bar1.Visible = false;
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar2.BarAppearance.Disabled.Options.UseFont = true;
            this.bar2.BarAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar2.BarAppearance.Hovered.Options.UseFont = true;
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarAppearance.Pressed.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar2.BarAppearance.Pressed.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSLogo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSTime),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi3),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi4),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiexit)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbi1
            // 
            this.bbi1.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi1.Caption = "生产计划";
            this.bbi1.Id = 0;
            this.bbi1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbi1.ImageOptions.Image")));
            this.bbi1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbi1.ImageOptions.LargeImage")));
            this.bbi1.Name = "bbi1";
            // 
            // bbi2
            // 
            this.bbi2.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi2.Caption = "炉内管理";
            this.bbi2.Id = 1;
            this.bbi2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbi2.ImageOptions.Image")));
            this.bbi2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbi2.ImageOptions.LargeImage")));
            this.bbi2.Name = "bbi2";
            // 
            // barLargeButtonItem1
            // 
            this.barLargeButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barLargeButtonItem1.Id = 6;
            this.barLargeButtonItem1.ImageOptions.Image = global::HmiApp.Properties.Resources.logo_fill;
            this.barLargeButtonItem1.Name = "barLargeButtonItem1";
            // 
            // barSLogo
            // 
            this.barSLogo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barSLogo.Caption = "    2#热处理炉   ";
            this.barSLogo.Id = 5;
            this.barSLogo.Name = "barSLogo";
            // 
            // barSTime
            // 
            this.barSTime.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barSTime.Caption = "barStaticItem1";
            this.barSTime.Id = 2;
            this.barSTime.Name = "barSTime";
            // 
            // bbi3
            // 
            this.bbi3.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi3.Caption = "炉内信息";
            this.bbi3.Id = 7;
            this.bbi3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbi3.ImageOptions.Image")));
            this.bbi3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbi3.ImageOptions.LargeImage")));
            this.bbi3.Name = "bbi3";
            this.bbi3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbi4
            // 
            this.bbi4.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi4.Caption = "出钢信息";
            this.bbi4.Id = 8;
            this.bbi4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbi4.ImageOptions.Image")));
            this.bbi4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbi4.ImageOptions.LargeImage")));
            this.bbi4.Name = "bbi4";
            // 
            // bbiexit
            // 
            this.bbiexit.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbiexit.Caption = "退出";
            this.bbiexit.Id = 3;
            this.bbiexit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiexit.ImageOptions.Image")));
            this.bbiexit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiexit.ImageOptions.LargeImage")));
            this.bbiexit.MinSize = new System.Drawing.Size(100, 0);
            this.bbiexit.Name = "bbiexit";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1918, 93);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1077);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1918, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 93);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 984);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1918, 93);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 984);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bar4
            // 
            this.bar4.BarAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar4.BarAppearance.Hovered.Options.UseFont = true;
            this.bar4.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar4.BarAppearance.Normal.Options.UseFont = true;
            this.bar4.BarAppearance.Pressed.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar4.BarAppearance.Pressed.Options.UseFont = true;
            this.bar4.BarName = "Main menu";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.MultiLine = true;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Main menu";
            // 
            // bar5
            // 
            this.bar5.BarAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar5.BarAppearance.Hovered.Options.UseFont = true;
            this.bar5.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar5.BarAppearance.Normal.Options.UseFont = true;
            this.bar5.BarAppearance.Pressed.Font = new System.Drawing.Font("Tahoma", 14F);
            this.bar5.BarAppearance.Pressed.Options.UseFont = true;
            this.bar5.BarName = "Main menu";
            this.bar5.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar5.DockCol = 0;
            this.bar5.DockRow = 0;
            this.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar5.OptionsBar.AllowQuickCustomization = false;
            this.bar5.OptionsBar.MultiLine = true;
            this.bar5.OptionsBar.UseWholeRow = true;
            this.bar5.Text = "Main menu";
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 93);
            this.barDockControl1.Manager = this.barManager1;
            this.barDockControl1.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControl1.Size = new System.Drawing.Size(1918, 0);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.xtraTabbedMdiManager1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1918, 1100);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.ShowMdiChildCaptionInParentTitle = true;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.Bar bar5;
        private DevExpress.XtraBars.BarLargeButtonItem bbi1;
        private DevExpress.XtraBars.BarLargeButtonItem bbi2;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarStaticItem barSTime;
        private DevExpress.XtraBars.BarLargeButtonItem bbiexit;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarStaticItem barSLogo;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem1;
        private DevExpress.XtraBars.BarLargeButtonItem bbi3;
        private DevExpress.XtraBars.BarLargeButtonItem bbi4;
    }
}