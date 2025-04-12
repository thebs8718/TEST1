namespace HmiApp
{
    partial class frmDischangePlate
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ceShift = new DevExpress.XtraEditors.CheckEdit();
            this.ceTimer = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.teend = new DevExpress.XtraEditors.TimeEdit();
            this.dtstart = new DevExpress.XtraEditors.DateEdit();
            this.testart = new DevExpress.XtraEditors.TimeEdit();
            this.dtend = new DevExpress.XtraEditors.DateEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcDisc = new DevExpress.XtraGrid.GridControl();
            this.gcDiscView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.ceb1 = new DevExpress.XtraEditors.CheckEdit();
            this.ceb2 = new DevExpress.XtraEditors.CheckEdit();
            this.ceb3 = new DevExpress.XtraEditors.CheckEdit();
            this.ceb4 = new DevExpress.XtraEditors.CheckEdit();
            this.ceb5 = new DevExpress.XtraEditors.CheckEdit();
            this.ceb6 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceShift.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTimer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtend.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDisc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDiscView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceb1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb6.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.ceShift);
            this.panelControl1.Controls.Add(this.ceTimer);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1913, 117);
            this.panelControl1.TabIndex = 0;
            // 
            // ceShift
            // 
            this.ceShift.EditValue = true;
            this.ceShift.Location = new System.Drawing.Point(80, 32);
            this.ceShift.Name = "ceShift";
            this.ceShift.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceShift.Properties.Appearance.Options.UseFont = true;
            this.ceShift.Properties.Caption = "当班出钢信息";
            this.ceShift.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Radio;
            this.ceShift.Properties.RadioGroupIndex = 1;
            this.ceShift.Size = new System.Drawing.Size(144, 29);
            this.ceShift.TabIndex = 7;
            this.ceShift.CheckedChanged += new System.EventHandler(this.ceShift_CheckedChanged);
            // 
            // ceTimer
            // 
            this.ceTimer.Location = new System.Drawing.Point(243, 32);
            this.ceTimer.Name = "ceTimer";
            this.ceTimer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceTimer.Properties.Appearance.Options.UseFont = true;
            this.ceTimer.Properties.Caption = "按时间";
            this.ceTimer.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Radio;
            this.ceTimer.Properties.RadioGroupIndex = 1;
            this.ceTimer.Size = new System.Drawing.Size(108, 29);
            this.ceTimer.TabIndex = 6;
            this.ceTimer.TabStop = false;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.teend);
            this.panelControl3.Controls.Add(this.dtstart);
            this.panelControl3.Controls.Add(this.testart);
            this.panelControl3.Controls.Add(this.dtend);
            this.panelControl3.Location = new System.Drawing.Point(357, 12);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(960, 68);
            this.panelControl3.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(38, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 25);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "起始时间";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(835, 22);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(92, 29);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "确认";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(433, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 25);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "结束时间";
            // 
            // teend
            // 
            this.teend.EditValue = new System.DateTime(2021, 7, 13, 0, 0, 0, 0);
            this.teend.Location = new System.Drawing.Point(707, 22);
            this.teend.Name = "teend";
            this.teend.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.teend.Properties.Appearance.Options.UseFont = true;
            this.teend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teend.Size = new System.Drawing.Size(100, 32);
            this.teend.TabIndex = 4;
            // 
            // dtstart
            // 
            this.dtstart.EditValue = null;
            this.dtstart.Location = new System.Drawing.Point(144, 22);
            this.dtstart.Name = "dtstart";
            this.dtstart.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dtstart.Properties.Appearance.Options.UseFont = true;
            this.dtstart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtstart.Size = new System.Drawing.Size(162, 32);
            this.dtstart.TabIndex = 3;
            // 
            // testart
            // 
            this.testart.EditValue = new System.DateTime(2021, 7, 13, 0, 0, 0, 0);
            this.testart.Location = new System.Drawing.Point(312, 22);
            this.testart.Name = "testart";
            this.testart.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.testart.Properties.Appearance.Options.UseFont = true;
            this.testart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.testart.Size = new System.Drawing.Size(100, 32);
            this.testart.TabIndex = 4;
            // 
            // dtend
            // 
            this.dtend.EditValue = null;
            this.dtend.Location = new System.Drawing.Point(539, 22);
            this.dtend.Name = "dtend";
            this.dtend.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dtend.Properties.Appearance.Options.UseFont = true;
            this.dtend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtend.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtend.Size = new System.Drawing.Size(162, 32);
            this.dtend.TabIndex = 3;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcDisc);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 117);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1913, 844);
            this.panelControl2.TabIndex = 1;
            // 
            // gcDisc
            // 
            this.gcDisc.Location = new System.Drawing.Point(5, 5);
            this.gcDisc.MainView = this.gcDiscView;
            this.gcDisc.Name = "gcDisc";
            this.gcDisc.Size = new System.Drawing.Size(1904, 787);
            this.gcDisc.TabIndex = 0;
            this.gcDisc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gcDiscView});
            this.gcDisc.Load += new System.EventHandler(this.gcDisc_Load);
            // 
            // gcDiscView
            // 
            this.gcDiscView.Appearance.Row.Options.UseTextOptions = true;
            this.gcDiscView.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDiscView.GridControl = this.gcDisc;
            this.gcDiscView.Name = "gcDiscView";
            this.gcDiscView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gcDiscView_CustomDrawRowIndicator);
            this.gcDiscView.DoubleClick += new System.EventHandler(this.gcDiscView_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.ceb6);
            this.panelControl4.Controls.Add(this.ceb3);
            this.panelControl4.Controls.Add(this.ceb5);
            this.panelControl4.Controls.Add(this.ceb2);
            this.panelControl4.Controls.Add(this.ceb4);
            this.panelControl4.Controls.Add(this.ceb1);
            this.panelControl4.Location = new System.Drawing.Point(1429, 5);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(362, 106);
            this.panelControl4.TabIndex = 8;
            // 
            // ceb1
            // 
            this.ceb1.EditValue = true;
            this.ceb1.Location = new System.Drawing.Point(26, 7);
            this.ceb1.Name = "ceb1";
            this.ceb1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceb1.Properties.Appearance.Options.UseFont = true;
            this.ceb1.Properties.Caption = "头部温度";
            this.ceb1.Size = new System.Drawing.Size(106, 29);
            this.ceb1.TabIndex = 0;
            this.ceb1.CheckedChanged += new System.EventHandler(this.ceb1_CheckedChanged);
            // 
            // ceb2
            // 
            this.ceb2.EditValue = true;
            this.ceb2.Location = new System.Drawing.Point(138, 7);
            this.ceb2.Name = "ceb2";
            this.ceb2.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceb2.Properties.Appearance.Options.UseFont = true;
            this.ceb2.Properties.Caption = "中部温度";
            this.ceb2.Size = new System.Drawing.Size(106, 29);
            this.ceb2.TabIndex = 0;
            this.ceb2.CheckedChanged += new System.EventHandler(this.ceb1_CheckedChanged);
            // 
            // ceb3
            // 
            this.ceb3.EditValue = true;
            this.ceb3.Location = new System.Drawing.Point(244, 7);
            this.ceb3.Name = "ceb3";
            this.ceb3.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceb3.Properties.Appearance.Options.UseFont = true;
            this.ceb3.Properties.Caption = "尾部温度";
            this.ceb3.Size = new System.Drawing.Size(106, 29);
            this.ceb3.TabIndex = 0;
            this.ceb3.CheckedChanged += new System.EventHandler(this.ceb1_CheckedChanged);
            // 
            // ceb4
            // 
            this.ceb4.EditValue = true;
            this.ceb4.Location = new System.Drawing.Point(26, 72);
            this.ceb4.Name = "ceb4";
            this.ceb4.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceb4.Properties.Appearance.Options.UseFont = true;
            this.ceb4.Properties.Caption = "头部炉温";
            this.ceb4.Size = new System.Drawing.Size(106, 29);
            this.ceb4.TabIndex = 0;
            this.ceb4.CheckedChanged += new System.EventHandler(this.ceb1_CheckedChanged);
            // 
            // ceb5
            // 
            this.ceb5.EditValue = true;
            this.ceb5.Location = new System.Drawing.Point(138, 72);
            this.ceb5.Name = "ceb5";
            this.ceb5.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceb5.Properties.Appearance.Options.UseFont = true;
            this.ceb5.Properties.Caption = "中部炉温";
            this.ceb5.Size = new System.Drawing.Size(106, 29);
            this.ceb5.TabIndex = 0;
            this.ceb5.CheckedChanged += new System.EventHandler(this.ceb1_CheckedChanged);
            // 
            // ceb6
            // 
            this.ceb6.EditValue = true;
            this.ceb6.Location = new System.Drawing.Point(244, 72);
            this.ceb6.Name = "ceb6";
            this.ceb6.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ceb6.Properties.Appearance.Options.UseFont = true;
            this.ceb6.Properties.Caption = "尾部炉温";
            this.ceb6.Size = new System.Drawing.Size(106, 29);
            this.ceb6.TabIndex = 0;
            this.ceb6.CheckedChanged += new System.EventHandler(this.ceb1_CheckedChanged);
            // 
            // frmDischangePlate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1913, 961);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDischangePlate";
            this.Text = "出钢信息";
            this.Load += new System.EventHandler(this.frmDischangePlate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceShift.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTimer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtstart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtend.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDisc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDiscView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceb1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceb6.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcDisc;
        private DevExpress.XtraGrid.Views.Grid.GridView gcDiscView;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dtstart;
        private DevExpress.XtraEditors.TimeEdit testart;
        private DevExpress.XtraEditors.TimeEdit teend;
        private DevExpress.XtraEditors.DateEdit dtend;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.CheckEdit ceTimer;
        private DevExpress.XtraEditors.CheckEdit ceShift;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.CheckEdit ceb6;
        private DevExpress.XtraEditors.CheckEdit ceb3;
        private DevExpress.XtraEditors.CheckEdit ceb5;
        private DevExpress.XtraEditors.CheckEdit ceb2;
        private DevExpress.XtraEditors.CheckEdit ceb4;
        private DevExpress.XtraEditors.CheckEdit ceb1;
    }
}