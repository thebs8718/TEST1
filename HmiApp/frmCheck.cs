using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using TxyLib;

namespace HmiApp
{
    public partial class frmCheck : DevExpress.XtraEditors.XtraForm
    {
        public string returnPlateno = "";
        public int returnRow = 0;
        public int returnPdiRow = 0;
        List<LabelControl> LcRoller;
        public const int maxRollerSlanNumber = 5;
        public const double rollerLength = 56000;
        public const double rollerWidth = 4400;
        List<LabelControl> LcChgRoller;
        List<LabelControl> LcTrack;

        List<LabelControl> LcModelTemp;
        public const int maxFurRoller = 138;
        public const int maxChgRoller = 57;
        public const int maxSlanNumber = 5 * 24;
        public const int furnaceLength = 78300; 
        public frmCheck()
        {
            InitializeComponent();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {

            
            LcRoller = new List<LabelControl>();
            for (int idx = 1; idx <= maxRollerSlanNumber; idx++)
            {
                LabelControl lblFur1Slab = new LabelControl();
                lblFur1Slab.Visible = false;
                lblFur1Slab.BackColor = Color.Green;// System.Drawing.ColorTranslator.FromOle(Information.RGB(ZColor.ProfLegend[0, 0], ZColor.ProfLegend[0, 1], ZColor.ProfLegend[0, 2]));
                lblFur1Slab.Width = 30;
                lblFur1Slab.Height = 30;
                lblFur1Slab.Left = 100;

                lblFur1Slab.AutoSizeMode = LabelAutoSizeMode.None;
                lblFur1Slab.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        
                lblFur1Slab.Name = "LcRollerSlab" + idx.ToString();
                lblFur1Slab.Font = new Font("Arial", 12, FontStyle.Bold);
                lblFur1Slab.ForeColor = Color.Lime;
                //lblFur1Slab.BringToFront();
                lblFur1Slab.Text = idx.ToString();  
                   

                LcRoller.Add(lblFur1Slab);

                pclRoller.Controls.Add(lblFur1Slab);
            }

            LcTrack = new List<LabelControl>();
            for (int idx = 1; idx <= maxSlanNumber; idx++)
            {
                LabelControl lblFur1Slab = new LabelControl();
                lblFur1Slab.Visible = false;
                lblFur1Slab.BackColor = Color.Green;// System.Drawing.ColorTranslator.FromOle(Information.RGB(ZColor.ProfLegend[0, 0], ZColor.ProfLegend[0, 1], ZColor.ProfLegend[0, 2]));
                lblFur1Slab.Width = 30;
                lblFur1Slab.Height = 30;
                lblFur1Slab.Left = 100;

                lblFur1Slab.AutoSizeMode = LabelAutoSizeMode.None;
                lblFur1Slab.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                lblFur1Slab.Name = "LcRollerSlab" + idx.ToString();
                lblFur1Slab.Font = new Font("Arial", 12, FontStyle.Bold);
                lblFur1Slab.ForeColor = Color.Lime;
                //lblFur1Slab.BringToFront();
                lblFur1Slab.Text = idx.ToString();

                //lblFur1Slab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                lblFur1Slab.Click += new System.EventHandler(this.btFurMap_Click);

                LcTrack.Add(lblFur1Slab);

                plcFur.Controls.Add(lblFur1Slab);
            }
            LcModelTemp = new List<LabelControl>();
            LcModelTemp.Add(lcZoneM_U1);
            LcModelTemp.Add(lcZoneM_D1);
            LcModelTemp.Add(lcZoneM_U2);
            LcModelTemp.Add(lcZoneM_D2);
            LcModelTemp.Add(lcZoneM_U3);
            LcModelTemp.Add(lcZoneM_D3);
            LcModelTemp.Add(lcZoneM_U4);
            LcModelTemp.Add(lcZoneM_D4);
            LcModelTemp.Add(lcZoneM_U5);
            LcModelTemp.Add(lcZoneM_D5);
            LcModelTemp.Add(lcZoneM_U6);
            LcModelTemp.Add(lcZoneM_D6);
            LcModelTemp.Add(lcZoneM_U7);
            LcModelTemp.Add(lcZoneM_D7);
            LcModelTemp.Add(lcZoneM_U8);
            LcModelTemp.Add(lcZoneM_D8);
            LcModelTemp.Add(lcZoneM_U9);
            LcModelTemp.Add(lcZoneM_D9);
            LcModelTemp.Add(lcZoneM_U10);
            LcModelTemp.Add(lcZoneM_D10);
            LcModelTemp.Add(lcZoneM_U11);
            LcModelTemp.Add(lcZoneM_D11);
            LcModelTemp.Add(lcZoneM_U12);
            LcModelTemp.Add(lcZoneM_D12);

            InitLoadPdiData("");
            InitLoadChargeData();

            ShowRollerTrack();
            InitChgRoller();
            InitLoadFurData();
            ShowFurnacceTrack();

            PFunction.BindCustomDrawRowIndicator(gcFurView);
        }
        private void InitChgRoller()
        {
            LcChgRoller = new List<LabelControl>();
            for (int idx = 1; idx <= maxChgRoller; idx++)
            {
                LabelControl lblFur1Slab = new LabelControl();
                lblFur1Slab.Visible = true;
                lblFur1Slab.BackColor = Color.Green;// System.Drawing.ColorTranslator.FromOle(Information.RGB(ZColor.ProfLegend[0, 0], ZColor.ProfLegend[0, 1], ZColor.ProfLegend[0, 2]));
                lblFur1Slab.Width = 2;
                lblFur1Slab.Top = 12;
                lblFur1Slab.Height = pclRoller.Height - 24;
                lblFur1Slab.Left = pclRoller.Width - 2 - idx * ((pclRoller.Width - 4) / maxChgRoller);

                lblFur1Slab.AutoSizeMode = LabelAutoSizeMode.None;
                lblFur1Slab.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                lblFur1Slab.Name = "LcChgRoller" + idx.ToString();
                lblFur1Slab.Font = new Font("Arial", 10, FontStyle.Bold);
                lblFur1Slab.ForeColor = Color.Lime;
                lblFur1Slab.BackColor = Color.Red;

                lblFur1Slab.Text = "";


                LcChgRoller.Add(lblFur1Slab);

                pclRoller.Controls.Add(lblFur1Slab);
            }
        }

        private void InitLoadPdiData(string Sql)
        {
            try
            {


                GridView view = gcPDIView as GridView;


                view.BeginUpdate(); //开始视图的编辑，防止触发其他事件
                view.BeginDataUpdate(); //开始数据的编辑

                //修改附加选项
                view.OptionsView.ShowColumnHeaders = true;                         //因为有Band列了，所以把ColumnHeader隐藏
                view.OptionsView.ShowGroupPanel = false;                            //如果没必要分组，就把它去掉
                view.OptionsView.EnableAppearanceEvenRow = false;                   //是否启用偶数行外观
                view.OptionsView.EnableAppearanceOddRow = true;                     //是否启用奇数行外观 
                view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;   //是否显示过滤面板
                view.OptionsCustomization.AllowColumnMoving = false;                //是否允许移动列
                view.OptionsCustomization.AllowColumnResizing = false;              //是否允许调整列宽
                view.OptionsCustomization.AllowGroup = false;                       //是否允许分组
                view.OptionsCustomization.AllowFilter = false;                      //是否允许过滤
                view.OptionsCustomization.AllowSort = false;                         //是否允许排序
                view.OptionsSelection.EnableAppearanceFocusedCell = true;           //???
                view.OptionsBehavior.Editable = false;                               //是否允许用户编辑单元格
                view.OptionsView.AllowCellMerge = false;

                StringBuilder strSql = new StringBuilder();
              
                if (Sql.Trim() !="")
                { 
                    strSql.Append(string.Format(@"select trim(heat_no) as 计划号 ,trim(plate_no) as 钢板号,trim(steel_grade) as 钢种,HEAT_MODE as 处理类型,
                                            KEEP_TEMP_AIM as 保温温度,HEAT_TIME_AIM as 在炉时间, KEEP_TIME_MIN_sur as 保温时间,
                                            PLATE_THICK/100 as 厚度,PLATE_WIDTH as 宽度,PLATE_LENGTH as 长度,PLATE_WEIGHT as 重量 FROM B_PDI_DATA where flag = 0 
                                            and (UPPER(plate_no) like '%{0}%' or UPPER(heat_no) like '%{0}%') order by tom desc", Sql.ToUpper() ));
                                 }
                else
                {
                    strSql.Append(string.Format(@"select trim(heat_no) as 计划号 ,trim(plate_no) as 钢板号,trim(steel_grade) as 钢种,HEAT_MODE as 处理类型,
                                            KEEP_TEMP_AIM as 保温温度,HEAT_TIME_AIM as 在炉时间, KEEP_TIME_MIN_sur as 保温时间,
                                            PLATE_THICK/100 as 厚度,PLATE_WIDTH as 宽度,PLATE_LENGTH as 长度,PLATE_WEIGHT as 重量 FROM B_PDI_DATA 
                                            where sysdate - tom <0.5 and flag = 0 order by tom desc "));

                }
                DataSet ds = DBHelper.Query(strSql.ToString());

                //绑定数据源并显示
                gcPDI.DataSource = ds.Tables[0];
                gcPDI.MainView.PopulateColumns();

                view.Appearance.HeaderPanel.Font = new Font("Tahoma", 14);
                view.RowHeight = 30;

                view.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[5].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[7].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[8].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[9].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[10].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                view.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[8].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[9].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[10].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                view.Columns[0].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[1].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[2].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[3].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[4].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[5].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[6].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[7].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[8].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[9].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[10].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[0].Width = 90;
                view.Columns[1].Width = 140;
                view.Columns[1].Width = 120;
                view.EndDataUpdate();//结束数据的编辑
                view.EndUpdate();   //结束视图的编辑
            }
            catch (Exception ex)
            {

            }
            return;
        }


        private void InitLoadChargeData(int flag =0)
        {
            try
            {


                GridView view = gcChargeRollerView as GridView;


                view.BeginUpdate(); //开始视图的编辑，防止触发其他事件
                view.BeginDataUpdate(); //开始数据的编辑

                //修改附加选项
                view.OptionsView.ShowColumnHeaders = true;                         //因为有Band列了，所以把ColumnHeader隐藏
                view.OptionsView.ShowGroupPanel = false;                            //如果没必要分组，就把它去掉
                view.OptionsView.EnableAppearanceEvenRow = false;                   //是否启用偶数行外观
                view.OptionsView.EnableAppearanceOddRow = true;                     //是否启用奇数行外观 
                view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;   //是否显示过滤面板
                view.OptionsCustomization.AllowColumnMoving = false;                //是否允许移动列
                view.OptionsCustomization.AllowColumnResizing = false;              //是否允许调整列宽
                view.OptionsCustomization.AllowGroup = false;                       //是否允许分组
                view.OptionsCustomization.AllowFilter = false;                      //是否允许过滤
                view.OptionsCustomization.AllowSort = false;                         //是否允许排序
                view.OptionsSelection.EnableAppearanceFocusedCell = true;           //???
                view.OptionsBehavior.Editable = false;                               //是否允许用户编辑单元格
                view.OptionsView.AllowCellMerge = false;

                StringBuilder strSql = new StringBuilder();
                if (flag == 0)
                {
                            strSql.Append(string.Format(@"select CASE PRODUCT_MODE WHEN 1 THEN '正常' WHEN 2 THEN '并板' ELSE '叠板' END as 并叠,
                    INSIDE_SEQ_NO as 顺序, trim(PLATE_NO) as 板号, CASE HEAT_MODE WHEN 'A' THEN '退火' WHEN 'N' THEN '正火' ELSE '回火' END as 模式,KEEP_TEMP_AIM as 温度,
                            HEAT_TIME_AIM as 时间,KEEP_TIME_AIM as 保温,TO_CHAR(PLATE_LENGTH)||'*'||TO_CHAR(PLATE_WIDTH)||'*'||TO_CHAR(PLATE_THICK)  as 规格
                    FROM B_CHARGE_ROLLER_DATA where flag = 0 order by INSIDE_SEQ_NO asc "));
                }
                else
                {
                    strSql.Append(string.Format(@"select CASE PRODUCT_MODE WHEN 1 THEN '正常' WHEN 2 THEN '并板' ELSE '叠板' END as 并叠,
                    INSIDE_SEQ_NO as 顺序, trim(PLATE_NO) as 板号, CASE HEAT_MODE WHEN 'A' THEN '退火' WHEN 'N' THEN '正火' ELSE '回火' END as 模式,KEEP_TEMP_AIM as 温度,
                            HEAT_TIME_AIM as 时间,KEEP_TIME_AIM as 保温,TO_CHAR(PLATE_LENGTH)||'*'||TO_CHAR(PLATE_WIDTH)||'*'||TO_CHAR(PLATE_THICK)  as 规格
                    FROM B_CHARGE_ROLLER_DATA where flag = 1 order by INSIDE_SEQ_NO asc "));
                }

            

              
                DataSet ds = DBHelper.Query(strSql.ToString());

                //绑定数据源并显示
                gcChargeRoller.DataSource = ds.Tables[0];
                gcChargeRoller.MainView.PopulateColumns();

                view.Appearance.HeaderPanel.Font = new Font("Tahoma", 14);
                view.RowHeight = 30;

                view.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[5].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[7].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                view.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                view.Columns[0].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[1].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[2].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[3].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[4].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[5].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[6].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[7].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[0].Width = 70;
                view.Columns[1].Width = 70;
                view.Columns[2].Width = 140;
                view.Columns[3].Width = 70;
                view.Columns[4].Width = 70;
                view.Columns[5].Width = 70;
                view.Columns[6].Width = 70;
                view.Columns[7].Width = 150;

                view.EndDataUpdate();//结束数据的编辑
                view.EndUpdate();   //结束视图的编辑
            }
            catch (Exception ex)
            {

            }
            ShowRollerTrack();
            return;
        }

        private void InitLoadFurData()
        {
            try
            {


                GridView view = gcFurView as GridView;


                view.BeginUpdate(); //开始视图的编辑，防止触发其他事件
                view.BeginDataUpdate(); //开始数据的编辑

                //修改附加选项
                view.OptionsView.ShowColumnHeaders = true;                         //因为有Band列了，所以把ColumnHeader隐藏
                view.OptionsView.ShowGroupPanel = false;                            //如果没必要分组，就把它去掉
                view.OptionsView.EnableAppearanceEvenRow = false;                   //是否启用偶数行外观
                view.OptionsView.EnableAppearanceOddRow = true;                     //是否启用奇数行外观 
                view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;   //是否显示过滤面板
                view.OptionsCustomization.AllowColumnMoving = false;                //是否允许移动列
                view.OptionsCustomization.AllowColumnResizing = false;              //是否允许调整列宽
                view.OptionsCustomization.AllowGroup = false;                       //是否允许分组
                view.OptionsCustomization.AllowFilter = false;                      //是否允许过滤
                view.OptionsCustomization.AllowSort = false;                         //是否允许排序
                view.OptionsSelection.EnableAppearanceFocusedCell = true;           //???
                view.OptionsBehavior.Editable = false;                               //是否允许用户编辑单元格
                view.OptionsView.AllowCellMerge = false;

                StringBuilder strSql = new StringBuilder();
                //strSql.Append(string.Format(@"select PLATE_NO as 钢板号  ,HEAT_NO as 热处理计划号  , FUR_NO as 热处理炉号  ,ACT_TOWARD as 实际好面朝向  , 
                //                            CON_TOWARD as 合同好面朝向, STEEL_CODE as 钢种代码, VAR_CODE as 厚板品种代码, STEEL_MARK as 出钢记号,
                //                            STEEL_GRADE as 钢种牌号, HEAT_MODE as 热处理方式代码, KEEP_TEMP_AIM as 保温温度目标值, KEEP_TEMP_MAX as 保温温度下限值,
                //                             KEEP_TIME_MIN_SURFACE as 保温温度上限值, HEAT_TIME_AIM as 最小保持时间, KEEP_TIME_MIN_CORE as 母板热处理在炉时间目标值, HEAT_TIME_MIN as 最小保温时间,
                //                KEEP_TIME_MODE as 最小在炉时间, ANNEALING_TYPE as 保温时间类型, PLATE_THICK as 钢板厚度, PLATE_WIDTH as 钢板宽度, PLATE_LENGTH as 钢板长度,
                //                PLATE_WEIGHT as 钢板重量        from B_PDI_DATA"));
                strSql.Append(string.Format(@"select CASE PRODUCT_MODE WHEN 1 THEN '正常' WHEN 2 THEN '并板' ELSE '叠板' END as 并叠, INSIDE_SEQ_NO as 顺序,  
PLATE_NO as 板号, KEEP_TEMP_AIM as 温度,
to_char(HEAT_TIME_AIM)||'-'||  to_char(KEEP_TIME_AIM) 加热时间, TO_CHAR(PLATE_THICK/100)||'*'||TO_CHAR(PLATE_WIDTH)||'*'||TO_CHAR(PLATE_LENGTH)  as 规格,
round((sysdate -CHARGETIME)*24*60) as 在炉,KEEP_TIME_REMAIN as 剩余,CASE WHEN sysdate < KEEP_TIME_START THEN  0  ELSE round((sysdate-KEEP_TIME_START )*1440) END as 实际,
ROUND(HEAD_TEMP1/3+HEAD_TEMP2/3+HEAD_TEMP3/3) as 头温,ROUND(MID_TEMP1/3+MID_TEMP2/3+MID_TEMP3/3) as 中温,
ROUND(TAIL_TEMP1/3+TAIL_TEMP2/3+TAIL_TEMP3/3) as 尾温 from B_FUR_TRACK order by head_position desc ,INSIDE_SEQ_NO asc"));

                // KEEP_TEMP_MIN as 下限,KEEP_TEMP_MAX as 上限,

                DataSet ds = DBHelper.Query(strSql.ToString());

                //绑定数据源并显示
                gcFur.DataSource = ds.Tables[0];
                gcFur.MainView.PopulateColumns();

                view.Appearance.HeaderPanel.Font = new Font("Tahoma", 14);
                view.RowHeight = 30;

                view.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[5].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[7].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[8].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[9].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[10].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[11].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[12].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                view.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[8].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[9].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[10].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Columns[11].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[12].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                view.Columns[0].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[1].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[2].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[3].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[4].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[5].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[6].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[7].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[8].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[9].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[10].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[11].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[12].AppearanceCell.Font = new Font("Tahoma", 14);
                view.Columns[2].Width = 140;
                view.Columns[5].Width = 180;

                view.EndDataUpdate();//结束数据的编辑
                view.EndUpdate();   //结束视图的编辑
            }
            catch (Exception ex)
            {

            }
            return;
        }
        private void ShowFurnacceTrack()
        {

            try
            {
                List<B_FUR_TRACK> list = Pf.FurData();
                for (int idx = 0; idx < list.Count; idx++)
                {
                    
                    LcTrack[idx].Width = Convert.ToInt32(list[idx].plate_length * plcFur.Width / furnaceLength);

                    //LcTrack[idx].Font = LcTrack[idx].Width>=80 ? new Font("Arial", 12, FontStyle.Bold) : new Font("Arial", 8, FontStyle.Bold);
                    if (list[idx].product_mode == 1)
                    {
                        LcTrack[idx].Height = Convert.ToInt32(list[idx].plate_width * plcFur.Height / rollerWidth) - 8;
                        LcTrack[idx].Top = (plcFur.Height - LcTrack[idx].Height) / 2;
                       
                        LcTrack[idx].Left = plcFur.Width - Convert.ToInt32((list[idx].head_position - GobalVal.FurnaceChDoor) * plcFur.Width / furnaceLength) + 8; ;
                        if(list[idx].keep_time_start_flag ==1)
                        {
                            LcTrack[idx].Appearance.BackColor = lcColorComplete.Appearance.BackColor;
                            LcTrack[idx].Appearance.BackColor2 = lcColorComplete.Appearance.BackColor2;
                            LcTrack[idx].ForeColor = lcColorNormal.ForeColor;
                        }
                        else
                        {
                            LcTrack[idx].BackColor = lcColorNormal.BackColor;
                            LcTrack[idx].ForeColor = lcColorNormal.ForeColor;
                            LcTrack[idx].Appearance.BackColor = lcColorNormal.Appearance.BackColor;
                            LcTrack[idx].Appearance.BackColor2 = lcColorNormal.Appearance.BackColor2;

                        }
                    }
                    else if (list[idx].product_mode == 2)
                    {
                        LcTrack[idx].Height = Convert.ToInt32(list[idx].plate_width * plcFur.Height / rollerWidth) - 8;

                        LcTrack[idx].Top = 12 + (plcFur.Height - 24) / 2 * idx;

                        LcTrack[idx].Left = plcFur.Width - Convert.ToInt32((list[idx].head_position - GobalVal.FurnaceChDoor) * plcFur.Width / furnaceLength) + 8; ;
                        if (list[idx].keep_time_start_flag == 1)
                        {
                            LcTrack[idx].Appearance.BackColor = lcColorComplete.Appearance.BackColor;
                            LcTrack[idx].Appearance.BackColor2 = lcColorComplete.Appearance.BackColor2;
                            LcTrack[idx].ForeColor = lcColorJuxt.ForeColor;
                        }
                        else
                        {
                            LcTrack[idx].BackColor = lcColorJuxt.BackColor;
                            LcTrack[idx].ForeColor = lcColorJuxt.ForeColor;
                            LcTrack[idx].Appearance.BackColor = lcColorJuxt.Appearance.BackColor;
                            LcTrack[idx].Appearance.BackColor2 = lcColorJuxt.Appearance.BackColor2;

                        }
                    }
                    else if (list[idx].product_mode == 3)
                    {
                        LcTrack[idx].Height = plcFur.Height / 5 - 2;

                        LcTrack[idx].Top = 12 + (plcFur.Height - 24) / 5 * idx;

                        LcTrack[idx].Left = plcFur.Width - Convert.ToInt32((list[idx].head_position - GobalVal.FurnaceChDoor) * plcFur.Width / furnaceLength) + 8; ;
                        if (list[idx].keep_time_start_flag == 1)
                        {
                            LcTrack[idx].Appearance.BackColor = lcColorComplete.Appearance.BackColor;
                            LcTrack[idx].Appearance.BackColor2 = lcColorComplete.Appearance.BackColor2;
                            LcTrack[idx].ForeColor = lcColorStack.ForeColor;
                        }
                        else
                        {
                            LcTrack[idx].BackColor = lcColorStack.BackColor;
                            LcTrack[idx].ForeColor = lcColorStack.ForeColor;
                            LcTrack[idx].Appearance.BackColor = lcColorStack.Appearance.BackColor;
                            LcTrack[idx].Appearance.BackColor2 = lcColorStack.Appearance.BackColor2;

                        }
                    }
                    if  (LcTrack[idx].Width>119)
                    {
                        LcTrack[idx].Text = (idx + 1).ToString() + "-" + list[idx].plate_no.Trim();
                    }
                    else
                    {
                        string tempstr= (idx + 1).ToString() + "-" + list[idx].plate_no.Trim();
                        tempstr = tempstr.Substring(0, 6) + "\r\n " + tempstr.Substring(6);
                        LcTrack[idx].Text = tempstr;
                    } 

                    LcTrack[idx].Visible = true;
                    LcTrack[idx].BringToFront();
                }
                for (int idx = list.Count; idx < maxSlanNumber; idx++)
                {
                    LcTrack[idx].Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
            return;
        }

        private void btQuery_Click(object sender, EventArgs e)
        {

            InitLoadPdiData(TxteQuery.Text.Trim());
        }
        private void gcPDIView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int selectRow = gcPDIView.GetSelectedRows()[0];
            if (gcPDIView.RowCount == 0)
                return;
            txtd1.Text = this.gcPDIView.GetRowCellValue(selectRow, "计划号").ToString();
            txtd2.Text = this.gcPDIView.GetRowCellValue(selectRow, "钢板号").ToString();
            txtd3.Text = this.gcPDIView.GetRowCellValue(selectRow, "钢种").ToString();
            txtd4.Text = this.gcPDIView.GetRowCellValue(selectRow, "长度").ToString();
            txtd5.Text = this.gcPDIView.GetRowCellValue(selectRow, "宽度").ToString();
            txtd6.Text = this.gcPDIView.GetRowCellValue(selectRow, "厚度").ToString();
            txtd7.Text = this.gcPDIView.GetRowCellValue(selectRow, "重量").ToString();
            string htmode = this.gcPDIView.GetRowCellValue(selectRow, "处理类型").ToString();
            if (htmode == "A")
                txtd8.Text = "退火";
            else if (htmode == "N")
                txtd8.Text = "正火";
            else if (htmode == "T")
                txtd8.Text = "回火";

            txtd9.Text = this.gcPDIView.GetRowCellValue(selectRow, "保温温度").ToString();
            txtd10.Text = this.gcPDIView.GetRowCellValue(selectRow, "在炉时间").ToString();
            txtd11.Text = this.gcPDIView.GetRowCellValue(selectRow, "保温时间").ToString();

          

            returnPlateno = "";
            returnPdiRow = selectRow;
        }
        private void gcPDIView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }

        private void sbtPreCheck_Click(object sender, EventArgs e)
        {
            string heatno = txtd1.Text;
            string plateno = txtd2.Text;
            int productmode = rgProductMode.SelectedIndex + 1;

           string msgReturn =  Pf.PreCheck(heatno, plateno, productmode);
            if (msgReturn!="")
            {
                MessageBox.Show(msgReturn, "警告", MessageBoxButtons.OK);
               
                return;
            }
            LogUtil.Log(DateTime.Now.ToString()+"|预上料,PlateNo=" + plateno); 
            if (gcPDIView.RowCount == 1)
            {
                gcPDIView.DeleteRow(returnPdiRow);
                ShowRollerTrack(); 
                InitLoadChargeData();
            }
            else
            {
                InitLoadPdiData(TxteQuery.Text.Trim());
                InitLoadChargeData();
                ShowRollerTrack();
            }
             txtd1.Text="";
             txtd2.Text = "";
        }

        private void sbtPreReturn_Click(object sender, EventArgs e)
        {
            if (returnPlateno == "")
                return;


            if (!Pf.IsChecked(returnPlateno))
            {
                if (MessageBox.Show("该钢板已经发送给PLC,返回可能会出错,是否继续?", "警告", MessageBoxButtons.YesNo)== DialogResult.No)
                {
                    return;
                }
            }
          Pf.PreCheckReturn(returnPlateno);
            returnPlateno = "";
            InitLoadPdiData("");
            if(gcChargeRollerView.RowCount == 1)
            {
                gcChargeRollerView.DeleteRow(returnRow);
                ShowRollerTrack();
            }  
            else
            {
                InitLoadChargeData();
            }
            LogUtil.Log(DateTime.Now.ToString() + "|信息返回,PlateNo=" + returnPlateno);
        }
        private void slbUnLoad_Click(object sender, EventArgs e)
        {
            //if (returnPlateno == "")
            //    return;


            //if (!Pf.IsChecked(returnPlateno))
            //{
            //    if (MessageBox.Show("该钢板已经发送给PLC,返回可能会出错,是否继续?", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            
            List<string> UnloadPlateNo = Pf.GetUnCheckPlate();
            if (UnloadPlateNo.Count == 0)
                return;
            foreach (var rtplateno in UnloadPlateNo)
            {
                Pf.PreCheckReturn(rtplateno);
            }
            InitLoadPdiData("");
            if (gcChargeRollerView.RowCount == 1)
            {
                gcChargeRollerView.DeleteRow(returnRow);
                ShowRollerTrack();
            }
            else
            {
                InitLoadChargeData();
            }
            Pf.UnloadToPlc();
        }

        private void gcChargeRollerView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int selectRow = gcChargeRollerView.GetSelectedRows()[0];
            if (gcChargeRollerView.RowCount == 0)
                return;
            //string heatno = this.gcChargeRollerView.GetRowCellValue(selectRow, "计划号").ToString();
            returnPlateno = this.gcChargeRollerView.GetRowCellValue(selectRow, "板号").ToString();
            returnRow = selectRow;

             

        }
        private void sbtMoveUp_Click(object sender, EventArgs e)
        {
            if (returnPlateno == "")
                return;
            if (!Pf.IsChecked(returnPlateno))
            {
                if (MessageBox.Show("该钢板已经发送给PLC,继续操作可能会出错,是否继续?", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            string returnmsg = Pf.PreMoveUp(returnPlateno);
            returnPlateno = "";
            InitLoadChargeData();
            if (returnmsg != "")
            {
                MessageBox.Show(returnmsg);
            }
        }

        private void sbtMoveDn_Click(object sender, EventArgs e)
        {
            if (returnPlateno == "")
                return;
            if (!Pf.IsChecked(returnPlateno))
            {
                if (MessageBox.Show("该钢板已经发送给PLC,返回可能会出错,是否继续?", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            string returnmsg = Pf.PreMoveDn(returnPlateno);
            
            returnPlateno = "";
            InitLoadChargeData();
            if (returnmsg!="")
            {
                MessageBox.Show(returnmsg);
            }
        }

        private void ShowRollerTrack()
        {

            try
            {
                List<B_CHARGE_ROLLER_DATA> list = Pf.RollerData();
                for(int idx = 0;idx<list.Count;idx++)
                {
                    LcRoller[idx].Width = Convert.ToInt32(list[idx].plate_length * pclRoller.Width / rollerLength);
                    LcRoller[idx].Left = pclRoller.Width - Convert.ToInt32(list[idx].head_position * pclRoller.Width / rollerLength); 
                    
                        if (list[idx].product_mode == 1)
                        {
                            LcRoller[idx].Height = Convert.ToInt32(list[idx].plate_width * lcroller01.Height / rollerWidth) - 8;
                            LcRoller[idx].Top = (pclRoller.Height - LcRoller[idx].Height) / 2;
                            if (list[idx].plate_fur1 == 1)
                            {
                                LcRoller[idx].BackColor = lcColorFur1.BackColor;
                                LcRoller[idx].ForeColor = lcColorFur1.ForeColor;
                            }
                            else
                            { 
                                LcRoller[idx].BackColor = lcColorNormal.BackColor;
                                LcRoller[idx].ForeColor = lcColorNormal.ForeColor;
                            }
                         }
                        else if (list[idx].product_mode == 2)
                        {
                            LcRoller[idx].Height = Convert.ToInt32(list[idx].plate_width * lcroller01.Height / rollerWidth) - 8;

                            LcRoller[idx].Top = 12 +   (pclRoller.Height-24) / 2   * (list[idx].inside_seq_no-1);
                            if (list[idx].plate_fur1 == 1)
                            {
                                LcRoller[idx].BackColor = lcColorFur1.BackColor;
                                LcRoller[idx].ForeColor = lcColorFur1.ForeColor;
                            }
                            else
                            {
                            LcRoller[idx].BackColor = lcColorJuxt.BackColor;
                            LcRoller[idx].ForeColor = lcColorJuxt.ForeColor;
                        }
                  
                        }
                        else if (list[idx].product_mode == 3)
                        {
                            LcRoller[idx].Height = lcroller01.Height / 5 - 2;

                            LcRoller[idx].Top = 12 + (pclRoller.Height - 24) / 5   * (list[idx].inside_seq_no - 1);
                            if (list[idx].plate_fur1 == 1)
                            {
                                LcRoller[idx].BackColor = lcColorFur1.BackColor;
                                LcRoller[idx].ForeColor = lcColorFur1.ForeColor;
                            }
                            else
                            {
                                LcRoller[idx].BackColor = lcColorStack.BackColor;
                                LcRoller[idx].ForeColor = lcColorStack.ForeColor;
                        }
                    
                        }
                  
                    
                    LcRoller[idx].Text = (idx + 1).ToString() + "-" + list[idx].plate_no.Trim();
                    if (LcRoller[idx].Width > 119)
                    {
                        LcRoller[idx].Text = (idx + 1).ToString() + " -" + list[idx].plate_no.Trim();
                    }
                    else
                    {
                        string tempstr = (idx + 1).ToString() + "-" + list[idx].plate_no.Trim();
                        tempstr = tempstr.Substring(0, 6) + "\r\n " + tempstr.Substring(6);
                        LcRoller[idx].Text = tempstr;
                    }
                    LcRoller[idx].Visible = true;
                    LcRoller[idx].BringToFront();
                }
                for(int idx = list.Count;idx< maxSlanNumber;idx++)
                {
                    LcRoller[idx].Visible = false;
                }
            }
            catch
            {

            }
            return;
        }

        private void sbtChecked_Click(object sender, EventArgs e)
        {
            int platenum = gcChargeRollerView.RowCount;
            CheckData temp = new CheckData();
            List<string> value1 = new List<string>();
            List<int> value2 = new List<int>();
            int heattemp = 0, heattime = 0, heatmode = 0, keeptime = 0;

            if (( platenum > 0)|| (platenum <= 5 ))
            {
                //if (MessageBox.Show("是否将该批次钢板核对数据发送给PLC?", "上料操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    //Pf.Checked();
                //}
               
            }
            else if ( platenum > 5)
            {
                MessageBox.Show("钢板超过5块,请确认数量是否正常!", "上料操作", MessageBoxButtons.OK); 
                
            }

            string productmode1 = "正常";

            if (platenum > 0)       
            {
                productmode1 = this.gcChargeRollerView.GetRowCellValue(0, "并叠").ToString();
            }
            if (platenum>1)
            {
                for (int idx = 1; idx < platenum; idx++)
                {
                    string productmode2 = this.gcChargeRollerView.GetRowCellValue(idx, "并叠").ToString();
                    if (productmode2!= productmode1)
                    {
                        return;
                    }
                }
            }



            //多块且正常模式
            //if ((platenum > 1) &&(rgProductMode.SelectedIndex == 0))
            if ((platenum > 1) && (productmode1 == "正常"))
            {
                string  checknPlateno = this.gcChargeRollerView.GetRowCellValue(returnRow, "板号").ToString();
                 heattemp = Convert.ToInt32(gcChargeRollerView.GetRowCellValue(returnRow, "温度")) > heattemp ? Convert.ToInt32(gcChargeRollerView.GetRowCellValue(returnRow, "温度")) : heattemp;
                heattime = Convert.ToInt32(gcChargeRollerView.GetRowCellValue(returnRow, "时间")) > heattime ? Convert.ToInt32(gcChargeRollerView.GetRowCellValue(returnRow, "时间")) : heattime;
                keeptime = Convert.ToInt32(gcChargeRollerView.GetRowCellValue(returnRow, "保温"));
                value1.Add(checknPlateno);
                if (productmode1 == "正常")
                    heatmode = 1;

               else if (productmode1 == "并板")
                    heatmode = 2;


                value2.Add(heatmode);
                value2.Add(heattemp);
                value2.Add(heattime);
                value2.Add(keeptime);
                temp.SetValue(value1, value2);
                temp.ShowDialog();
                return;
            }

     
            for(int idx=0;idx< platenum;idx++)
            {
                string plateno = gcChargeRollerView.GetRowCellValue(idx, "板号").ToString();
                heattemp = Convert.ToInt32(gcChargeRollerView.GetRowCellValue(idx, "温度")) > heattemp ? Convert.ToInt32(gcChargeRollerView.GetRowCellValue(idx, "温度")) : heattemp;
                heattime = Convert.ToInt32(gcChargeRollerView.GetRowCellValue(idx, "时间")) > heattime ? Convert.ToInt32(gcChargeRollerView.GetRowCellValue(idx, "时间")) : heattime;
                keeptime = Convert.ToInt32(gcChargeRollerView.GetRowCellValue(idx, "保温"))  ;
                string productmode =  gcChargeRollerView.GetRowCellValue(idx, "并叠").ToString()  ;
                if (productmode == "正常")
                {
                    heatmode = 1;
                    if (platenum>1)
                    {
                        MessageBox.Show("钢板超过1块,请选择正确的生产模式!", "上料操作", MessageBoxButtons.OK);
                        return;
                    }
                }
                else if (productmode == "并板")
                {
                    heatmode = 2;
                    if (platenum > 2)
                    {
                        MessageBox.Show("钢板超过2块,请选择正确的生产模式!", "上料操作", MessageBoxButtons.OK);
                        return;
                    }
                }
                else if (productmode == "叠板")
                {
                    heatmode = 3;
                    if (platenum > 5)
                    {
                        MessageBox.Show("钢板超过5块,请选择正确的生产模式!", "上料操作", MessageBoxButtons.OK);
                        return;
                    }
                }
                value1.Add(plateno);
            }
            value2.Add(heatmode);
            value2.Add(heattemp);
            value2.Add(heattime);
            value2.Add(keeptime);
            temp.SetValue(value1, value2);
            temp.ShowDialog();


            InitLoadPdiData("");
            InitLoadChargeData();
            ShowRollerTrack();
        }


        private void sbtReject_Click(object sender, EventArgs e)
        {
            int platenum = gcChargeRollerView.RowCount;
            if (platenum > 0)
            {
                if (MessageBox.Show("是否将该批次钢板吊销?", "吊销操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Pf.Reject();
                }

            }
        }
        private void sbtCancelCheck_Click(object sender, EventArgs e)
        {
            int platenum = gcChargeRollerView.RowCount;
            if (platenum > 0)
            {
                if (MessageBox.Show("是否将该批次钢板吊销?", "吊销操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Pf.Reject();
                }

            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cbeManual.SelectedIndex == 0)
            {
                int platenum = gcChargeRollerView.RowCount;
                if (platenum > 0)
                {
                    if (MessageBox.Show("是否将该批次钢板吊销?", "吊销操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Pf.Reject();

                    }

                }
            }
            else if (cbeManual.SelectedIndex == 1)
            {
                int platenum = gcChargeRollerView.RowCount;
                if (platenum > 0)
                {
                    if (MessageBox.Show("是否将该批次钢板取消核对?", "取消核对操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Pf.PreCheckReturn();
                    }

                }
            }
            else if (cbeManual.SelectedIndex == 2)
            {
                int platenum = gcChargeRollerView.RowCount;
                if (platenum > 0)
                {
                    if (MessageBox.Show("是否将该批次钢板信号装入?", "信号操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Pf.HmiCharge();
                    }

                }
            }
            cbeManual.SelectedIndex = -1;
            InitLoadPdiData("");
            InitLoadChargeData(1);

        }

        private void sbChangeMode_Click(object sender, EventArgs e)
        {
            int platenum = gcChargeRollerView.RowCount;
            if (platenum > 0)
            {
                if (MessageBox.Show("是否将该批次模式切换?", "模式切换", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int productmode = rgProductMode.SelectedIndex + 1;
                   string msg = Pf.HmiModeChange(productmode);
                    if (msg != "")
                    {
                        MessageBox.Show(msg, "错误提示", MessageBoxButtons.OK); 
                        LogUtil.Log("模式切换,模式=" + productmode.ToString());
                    }
                    else
                    {
                        //InitLoadPdiData("");
                        InitLoadChargeData();
                    }
                }

            }
        }
        private void sbPdiHide_Click(object sender, EventArgs e)
        { 
            string plateno = txtd2.Text;
            Pf.PdiHide(plateno);          
            if (gcPDIView.RowCount >= 1)
                    gcPDIView.DeleteRow(returnPdiRow);

            LogUtil.Log(DateTime.Now.ToString() + "|计划隐藏,PlateNo=" + plateno);
            //gcPDIView.FocusedRowIndex = 1;
        }
        private void xtbControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtbControl.SelectedTabPage == xtraTabPage1)
            {
                InitLoadChargeData();
                lblTips.Text = "预上料信息";
                memoEdit1.Text = "";
                comboBoxEdit1.SelectedIndex = -1;

            }
            else if (xtbControl.SelectedTabPage == xtraTabPage2)
            {
                InitLoadChargeData(1);

                lblTips.Text = "已上料信息";
                memoEdit1.Text = "";
                comboBoxEdit1.SelectedIndex = -1;
            }

            else if (xtbControl.SelectedTabPage == xtraTabPage4)
            {
                ckbKeepTemp.Checked = false;
                ckbKeepTime.Checked = false;
                ckbPyrometer.Checked = false;
                int[] TmpCor = Pf.GetTempsetCor();
                speKeepTemp.Value = TmpCor[0];
                speKeepTime.Value = TmpCor[1];
                spePyrometer.Value = TmpCor[2];
                speKeepTemp.Enabled = false;
                speKeepTime.Enabled = false;
                spePyrometer.Enabled = false;
            }
            else if (xtbControl.SelectedTabPage == xtraTabPage5)
            {

                LoadPage5();

            }
        }

        private void LoadPage5()
        {
            List<int> defalutTemp = Pf.GetModelDefalut();
            speModelDefaultTemp2.Value = defalutTemp[0];

            speCorZone1.Value = defalutTemp[1];
            speCorZone2.Value = defalutTemp[2];
            speCorZone3.Value = defalutTemp[3];
            speCorZone4.Value = defalutTemp[4];
            speCorZone5.Value = defalutTemp[5];
            speCorZone6.Value = defalutTemp[6];
            speCorZone7.Value = defalutTemp[7];
            speCorZone8.Value = defalutTemp[8];
            speCorZone9.Value = defalutTemp[9];
            speCorZone10.Value = defalutTemp[10];
            speCorZone11.Value = defalutTemp[11];
            speCorZone12.Value = defalutTemp[12];
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowRollerTrack(); 
        
            ShowFurnacceTrack();

            if (loadpdinow())
            {
                if (xtbControl.SelectedTabPage == xtraTabPage1)
                {
                    InitLoadChargeData();

                }
                else if (xtbControl.SelectedTabPage == xtraTabPage2)
                {
                    InitLoadChargeData(1);

                }
            }
            
            LoadFurTemp();
            InitLoadFurData();
        }

        private bool loadpdinow()
        {
            bool returnval = false;
            int flag = 0;
            if(xtbControl.SelectedTabPage == xtraTabPage2)
            {
                flag = 1;
            }
            List<B_CHARGE_ROLLER_DATA> listcrd = Pf.PreRollerData(flag);

            if (listcrd.Count != gcChargeRollerView.RowCount)
                return true;

            for (int idx=0;idx< gcChargeRollerView.RowCount;idx++)
            {
                string Plateno = this.gcChargeRollerView.GetRowCellValue(idx, "板号").ToString();
                foreach(var temp in listcrd)
                {
                    returnval = true;
                    if (temp.plate_no.Trim() == Plateno)
                    {
                        returnval = false;
                        continue;
                    }
                       
                }
                if (returnval)
                    return returnval;

            }

            return false;
        }

        private void gcFurView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int selectRow = gcFurView.GetSelectedRows()[0];
            if (gcFurView.RowCount == 0)
            {
                txtd2.Text = "";
                return;
            }
           
            if (xtbControl.SelectedTabPage == xtraTabPage3)
            {
                 if (comboBoxEdit1.SelectedIndex == 2)
                {
                    memoEdit1.Text = "";
                }
                 else
                {
                    string productmode = this.gcFurView.GetRowCellValue(selectRow, "并叠").ToString();
                    if (productmode == "正常")
                    {
                        memoEdit1.Text = this.gcFurView.GetRowCellValue(selectRow, "板号").ToString();// Pf.GetAllPlateNo(this.gcFurView.GetRowCellValue(selectRow, "板号").ToString());

                    }
                    else
                    {
                        string plateno = this.gcFurView.GetRowCellValue(selectRow, "板号").ToString();
                        memoEdit1.Text = Pf.GetAllPlateNo(plateno);
                    }

                }
            }
            
            txtd2.Text = this.gcFurView.GetRowCellValue(selectRow, "板号").ToString();
        }
        private void LoadFurTemp()
        {
            List<int> list = Pf.FurTemp();
            if (list.Count == 0)
                return;
            lcZoneU1.Text = list[1].ToString() + "-" + list[0].ToString();
            lcZoneD1.Text = list[3].ToString() + "-" + list[2].ToString();
            lcZoneU2.Text = list[5].ToString() + "-" + list[4].ToString();
            lcZoneD2.Text = list[7].ToString() + "-" + list[6].ToString();
            lcZoneU3.Text = list[9].ToString() + "-" + list[8].ToString();
            lcZoneD3.Text = list[11].ToString() + "-" + list[10].ToString();
            lcZoneU4.Text = list[13].ToString() + "-" + list[12].ToString();
            lcZoneD4.Text = list[15].ToString() + "-" + list[14].ToString();
            lcZoneU5.Text = list[17].ToString() + "-" + list[16].ToString();
            lcZoneD5.Text = list[19].ToString() + "-" + list[18].ToString();
            lcZoneU6.Text = list[21].ToString() + "-" + list[20].ToString();
            lcZoneD6.Text = list[23].ToString() + "-" + list[22].ToString();
            lcZoneU7.Text = list[25].ToString() + "-" + list[24].ToString();
            lcZoneD7.Text = list[27].ToString() + "-" + list[26].ToString();
            lcZoneU8.Text = list[29].ToString() + "-" + list[28].ToString();
            lcZoneD8.Text = list[31].ToString() + "-" + list[30].ToString();
            lcZoneU9.Text = list[33].ToString() + "-" + list[32].ToString();
            lcZoneD9.Text = list[35].ToString() + "-" + list[34].ToString();
            lcZoneU10.Text = list[37].ToString() + "-" + list[36].ToString();
            lcZoneD10.Text = list[39].ToString() + "-" + list[38].ToString();
            lcZoneU11.Text = list[41].ToString() + "-" + list[40].ToString();
            lcZoneD11.Text = list[43].ToString() + "-" + list[42].ToString();
            lcZoneU12.Text = list[45].ToString() + "-" + list[44].ToString();
            lcZoneD12.Text = list[47].ToString() + "-" + list[46].ToString();
            lcTempD.Text = list[48].ToString();

            List<int> listModel = Pf.FurModelSetTemp();
            if (listModel.Count == 0)
                return;
            for(int idx = 0;idx < 12;idx++)
            {
                LcModelTemp[idx * 2].Text = (idx + 1).ToString() + "U - " + listModel[idx * 2].ToString();
                LcModelTemp[idx * 2 + 1].Text = (idx + 1).ToString() + "D - " + listModel[idx * 2 + 1].ToString();
            }

        }

        private void slbClick_Click(object sender, EventArgs e)
        {
            string plateno2 = memoEdit1.Text;
            string[] sArray = plateno2.Split('\n');
            List<string> lstPlateno = new List<string>();
            for (int idx = 0; idx < sArray.Count(); idx++)
            {
                lstPlateno.Add(sArray[idx]);
            }
            if (sArray.Count() == 0)
            {
                return;
            }


            if (comboBoxEdit1.SelectedIndex == 0)
            {


                Pf.ChargeReturn(lstPlateno);


            }
            else if (comboBoxEdit1.SelectedIndex == 1)
            {

                if (sArray.Count() > 0)
                {
                    Pf.HmiDisCharge(lstPlateno);
                }
            }
            else if (comboBoxEdit1.SelectedIndex == 2)
            {

                if (sArray.Count() > 0)
                {
                    Pf.HmiDisCharge(lstPlateno);
                }
            }
            memoEdit1.Text = "";
            cbeManual.SelectedIndex = -1;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmPlateCurve temp = new frmPlateCurve();
            temp.Show();
        }

        private void btFurMap_Click(object sender, EventArgs e)
        {
            LabelControl temp = (LabelControl)sender;
            string plateno = temp.Text;
            plateno = plateno.Substring(2);
            plateno = plateno.Replace("\r\n", "");
            plateno = plateno.Replace(" ", "");
            frmPlateCurve frmPlateCurve1 = new frmPlateCurve();
            frmPlateCurve1.SetPlateno(plateno);
            frmPlateCurve1.ShowDialog();


        }

        private void gcChargeRoller_Click(object sender, EventArgs e)
        {

        }

        private void ckbKeepTemp_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit temp = (CheckEdit)sender;
            if (temp.Name == "ckbKeepTemp")
            {
                if (!temp.Checked)
                {
                    speKeepTemp.Enabled = false;
                }
                else
                {
                    speKeepTemp.Enabled = true;
                }
            }
            else if (temp.Name == "ckbKeepTime")
            {
                if (!temp.Checked)
                {
                    speKeepTime.Enabled = false;
                }
                else
                {
                    speKeepTime.Enabled = true;
                }
            }
            else if (temp.Name == "ckbPyrometer")
            {
                if (!temp.Checked)
                {
                    spePyrometer.Enabled = false;
                }
                else
                {
                    spePyrometer.Enabled = true;
                }
            }
        }

        private void slbSaveTempSetCor_Click(object sender, EventArgs e)
        {
            int keeptemp = Convert.ToInt32(speKeepTemp.Value);
            int keeptime = Convert.ToInt32(speKeepTime.Value);
            int Pyrometer = Convert.ToInt32(spePyrometer.Value);
            Pf.HmiUpdTempsetCor(keeptemp, keeptime, Pyrometer);

            ckbKeepTemp.Checked = false;
            speKeepTemp.Enabled = false;
            ckbKeepTime.Checked = false;
            speKeepTime.Enabled = false;
            ckbPyrometer.Checked = false;
            spePyrometer.Enabled = false;

            LogUtil.Log(DateTime.Now.ToString() + "|温度修正,保温温度修正:" + keeptemp + "保温时间修正:" + keeptime.ToString() + "高温计修正:" + Pyrometer.ToString());

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            int defaulttemp = Convert.ToInt32(speModelDefaultTemp2.Value); 
            Pf.HmiUpdDefaultTemp(defaulttemp);

            LogUtil.Log(DateTime.Now.ToString() + "|模型空炉温度修正,温度修正:" + defaulttemp);
            ckbModelDefaultTemp.Checked = false;
            speModelDefaultTemp2.Enabled = false;
        }

        private void ckbModelDefaultTemp_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbModelDefaultTemp.Checked)
            {
                speModelDefaultTemp2.Enabled = true;
            }
            else
            {
                speModelDefaultTemp2.Enabled = false;
                //int defalutTemp = Pf.GetModelDefalut();
                //speModelDefaultTemp.Value = defalutTemp;
            }
        }

        private void slbCorrSet_Click(object sender, EventArgs e)
        {
            List<int> TempCorr = new List<int>();
            TempCorr.Add(Convert.ToInt32(speCorZone1.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone2.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone3.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone4.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone5.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone6.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone7.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone8.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone9.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone10.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone11.Value));
            TempCorr.Add(Convert.ToInt32(speCorZone12.Value));
            Pf.HmiUpdCorrTemp(TempCorr);
        }

        private void slbCorrAllSet_Click(object sender, EventArgs e)
        {
            List<int> TempCorr = new List<int>();
            for(int idx=0;idx<12;idx++)
            {

                TempCorr.Add(Convert.ToInt32(speCorAll.Value));
            }

            Pf.HmiUpdCorrTemp(TempCorr);
            LoadPage5();
        }
    }
}