namespace HmiApp
{
    partial class frmPdi
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
            this.gcPDI = new DevExpress.XtraGrid.GridControl();
            this.gcPDIView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbnRequest = new DevExpress.XtraEditors.SimpleButton();
            this.sbnModify = new DevExpress.XtraEditors.SimpleButton();
            this.TxteQuery = new DevExpress.XtraEditors.TextEdit();
            this.btQuery = new DevExpress.XtraEditors.SimpleButton();
            this.sbnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcPDI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPDIView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxteQuery.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPDI
            // 
            this.gcPDI.Location = new System.Drawing.Point(3, 64);
            this.gcPDI.MainView = this.gcPDIView;
            this.gcPDI.Name = "gcPDI";
            this.gcPDI.Size = new System.Drawing.Size(1898, 884);
            this.gcPDI.TabIndex = 1;
            this.gcPDI.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gcPDIView});
            // 
            // gcPDIView
            // 
            this.gcPDIView.DetailHeight = 375;
            this.gcPDIView.GridControl = this.gcPDI;
            this.gcPDIView.Name = "gcPDIView";
            this.gcPDIView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gcPDIView_RowCellClick);
            // 
            // sbnRequest
            // 
            this.sbnRequest.Location = new System.Drawing.Point(62, 24);
            this.sbnRequest.Name = "sbnRequest";
            this.sbnRequest.Size = new System.Drawing.Size(116, 34);
            this.sbnRequest.TabIndex = 2;
            this.sbnRequest.Text = "请求计划";
            this.sbnRequest.Click += new System.EventHandler(this.sbnRequest_Click);
            // 
            // sbnModify
            // 
            this.sbnModify.Location = new System.Drawing.Point(211, 24);
            this.sbnModify.Name = "sbnModify";
            this.sbnModify.Size = new System.Drawing.Size(116, 34);
            this.sbnModify.TabIndex = 2;
            this.sbnModify.Text = "修改计划";
            this.sbnModify.Click += new System.EventHandler(this.sbnModify_Click);
            // 
            // TxteQuery
            // 
            this.TxteQuery.Location = new System.Drawing.Point(522, 25);
            this.TxteQuery.Name = "TxteQuery";
            this.TxteQuery.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.TxteQuery.Properties.Appearance.Options.UseFont = true;
            this.TxteQuery.Properties.MaxLength = 11;
            this.TxteQuery.Size = new System.Drawing.Size(143, 26);
            this.TxteQuery.TabIndex = 3;
            // 
            // btQuery
            // 
            this.btQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btQuery.Appearance.Options.UseFont = true;
            this.btQuery.Location = new System.Drawing.Point(671, 28);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(75, 23);
            this.btQuery.TabIndex = 4;
            this.btQuery.Text = "刷新";
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // sbnDelete
            // 
            this.sbnDelete.Location = new System.Drawing.Point(361, 22);
            this.sbnDelete.Name = "sbnDelete";
            this.sbnDelete.Size = new System.Drawing.Size(116, 34);
            this.sbnDelete.TabIndex = 5;
            this.sbnDelete.Text = "删除计划";
            this.sbnDelete.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmPdi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1913, 961);
            this.Controls.Add(this.sbnDelete);
            this.Controls.Add(this.TxteQuery);
            this.Controls.Add(this.btQuery);
            this.Controls.Add(this.sbnModify);
            this.Controls.Add(this.sbnRequest);
            this.Controls.Add(this.gcPDI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPdi";
            this.Text = "生产计划";
            this.Load += new System.EventHandler(this.frmPdi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPDI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPDIView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxteQuery.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPDI;
        private DevExpress.XtraGrid.Views.Grid.GridView gcPDIView;
        private DevExpress.XtraEditors.SimpleButton sbnRequest;
        private DevExpress.XtraEditors.SimpleButton sbnModify;
        private DevExpress.XtraEditors.TextEdit TxteQuery;
        private DevExpress.XtraEditors.SimpleButton btQuery;
        private DevExpress.XtraEditors.SimpleButton sbnDelete;
    }
}