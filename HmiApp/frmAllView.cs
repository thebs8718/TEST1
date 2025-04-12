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
using System.Threading;

namespace HmiApp
{
    public partial class frmAllView : DevExpress.XtraEditors.XtraForm
    {
        List<LabelControl> LcFurnaceRoller;
        List<LabelControl> LcChgRoller;

        List<LabelControl> LcTrack;
        List<LabelControl> LcRoller;
        int maxFurRoller = 138;
        int maxChgRoller = 57; 
        public const int maxSlanNumber = 5 * 24;
        public const int furnaceLength = 78300;
        public const int rollerWidth = 4000;
        public const int rollerLength = 56000;
        public frmAllView()
        {
            InitializeComponent();
        }

        private void frmAllView_Load(object sender, EventArgs e)
        {
            InitLoadFurData();
            InitFurRoller();
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
                lblFur1Slab.Font = new Font("Arial", 10, FontStyle.Bold);
                lblFur1Slab.ForeColor = Color.Lime;
                //lblFur1Slab.BringToFront();
                lblFur1Slab.Text = idx.ToString();


                LcTrack.Add(lblFur1Slab);

                plcFur.Controls.Add(lblFur1Slab);
            }

            LcRoller = new List<LabelControl>();
            for (int idx = 1; idx <= 5 * 2; idx++)
            {
                LabelControl lblFur1Slab = new LabelControl();
                lblFur1Slab.Visible = false;
                lblFur1Slab.BackColor = Color.Green;// System.Drawing.ColorTranslator.FromOle(Information.RGB(ZColor.ProfLegend[0, 0], ZColor.ProfLegend[0, 1], ZColor.ProfLegend[0, 2]));
                lblFur1Slab.Width = 30;
                lblFur1Slab.Height = 30;
                lblFur1Slab.Left = 100;

                lblFur1Slab.AutoSizeMode = LabelAutoSizeMode.None;
                lblFur1Slab.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                lblFur1Slab.Name = "LcChgRollerSlab" + idx.ToString();
                lblFur1Slab.Font = new Font("Arial", 10, FontStyle.Bold);
                lblFur1Slab.ForeColor = Color.Lime;
                //lblFur1Slab.BringToFront();
                lblFur1Slab.Text = idx.ToString();


                LcRoller.Add(lblFur1Slab);

                pclRoller.Controls.Add(lblFur1Slab);
            }
            ThreadStart thStart = new ThreadStart(Timer1);//threadStart委托 
            Thread thread = new Thread(thStart);
            thread.Priority = ThreadPriority.Highest;
            thread.IsBackground = true; //关闭窗体继续执行
            thread.Start();
            LoadRollerMap();
            ShowFurnacceTrack();
            InitChgRoller();
             
        }
        public void Timer1()
        {
            while (true)
            {
              
                Task.Delay(2000);
            }

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            ShowFurnacceTrack();
            ShowRollerTrack();
            LoadFurTemp();
            InitLoadFurData();
            LoadRollerMap();
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
                int templeft = idx *  (pclRoller.Width - 4) / maxChgRoller ;
                lblFur1Slab.Left = pclRoller.Width + 4 - templeft;
            
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

        private void InitFurRoller()
        {
            LcFurnaceRoller = new List<LabelControl>();
            for (int idx = 1; idx <= maxFurRoller; idx++)
            {
                LabelControl lblFur1Slab = new LabelControl();
                lblFur1Slab.Visible = true;
                lblFur1Slab.BackColor = Color.Green;// System.Drawing.ColorTranslator.FromOle(Information.RGB(ZColor.ProfLegend[0, 0], ZColor.ProfLegend[0, 1], ZColor.ProfLegend[0, 2]));
                lblFur1Slab.Width = 2;
                lblFur1Slab.Top = 12;
                lblFur1Slab.Height = plcFur.Height -24;
                if (idx==138)
                {

                }
                double templeft = (double)idx * (((double)plcFur.Width - 4) / (double)maxFurRoller);
           
                lblFur1Slab.Left = plcFur.Width - 2 - Convert.ToInt32(templeft);

                lblFur1Slab.AutoSizeMode = LabelAutoSizeMode.None;
                lblFur1Slab.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                lblFur1Slab.Name = "LcFurnaceRoller" + idx.ToString();
                lblFur1Slab.Font = new Font("Arial", 10, FontStyle.Bold);
                lblFur1Slab.ForeColor = Color.Lime;
                lblFur1Slab.BackColor = Color.Red;
                lblFur1Slab.ToolTip = idx.ToString();
                lblFur1Slab.Text = "";


                LcFurnaceRoller.Add(lblFur1Slab);

                plcFur.Controls.Add(lblFur1Slab);
            }
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
to_char(HEAT_TIME_AIM)||'-'||  to_char(KEEP_TIME_AIM) 加热时间, TO_CHAR(PLATE_LENGTH)||'|'||TO_CHAR(PLATE_WIDTH)||'|'||TO_CHAR(PLATE_THICK/100)  as 规格,
round((sysdate -CHARGETIME)*24*60) as 在炉,KEEP_TIME_REMAIN as 保温剩余,CASE WHEN sysdate < KEEP_TIME_START THEN  0  ELSE round((sysdate-KEEP_TIME_START )*1440) END as 实际保温,
ROUND(HEAD_TEMP1/3+HEAD_TEMP2/3+HEAD_TEMP3/3) as 头部温度,ROUND(MID_TEMP1/3+MID_TEMP2/3+MID_TEMP3/3) as 中部温度,
ROUND(TAIL_TEMP1/3+TAIL_TEMP2/3+TAIL_TEMP3/3) as 尾部温度 from B_FUR_TRACK order by SEQ_NO asc ,INSIDE_SEQ_NO asc"));

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
            
        private void LoadRollerMap()
        {
            try
            {
                decimal[] rollerspeed = Pf.GetRollerSpeed();
                for(int idx=0;idx< LcFurnaceRoller.Count;idx++)
                {
                    if (rollerspeed[idx] > 0)
                    {
                        LcFurnaceRoller[idx].BackColor = Color.Lime;

                    }
                    else if (rollerspeed[idx] == 0)
                    {
                        LcFurnaceRoller[idx].BackColor = Color.Gold;

                    }
                    else if (rollerspeed[idx] < 0)
                    {
                        LcFurnaceRoller[idx].BackColor = Color.Yellow;

                    }
                }
            }
            catch
            {

            }
        }
        private void ShowRollerTrack()
        {

            try
            {
                List<B_CHARGE_ROLLER_DATA> list = Pf.RollerData();
                for (int idx = 0; idx < list.Count; idx++)
                {
                    LcRoller[idx].Width = Convert.ToInt32(list[idx].plate_length * pclRoller.Width / rollerLength);
                    LcRoller[idx].Left = pclRoller.Width - Convert.ToInt32(list[idx].head_position * pclRoller.Width / rollerLength); ;
                    if (list[idx].product_mode == 1)
                    {
                        LcRoller[idx].Height = Convert.ToInt32(list[idx].plate_width * lcroller01.Height / rollerWidth) - 8;
                        LcRoller[idx].Top = (pclRoller.Height - LcRoller[idx].Height) / 2;
                        LcRoller[idx].BackColor = lcColorNormal.BackColor;
                        LcRoller[idx].ForeColor = lcColorNormal.ForeColor;
                    }
                    else if (list[idx].product_mode == 2)
                    {
                        LcRoller[idx].Height = Convert.ToInt32(list[idx].plate_width * lcroller01.Height / rollerWidth) - 8;

                        LcRoller[idx].Top = 12 + (pclRoller.Height - 24) / 2 * idx;
                        LcRoller[idx].BackColor = lcColorJuxt.BackColor;
                        LcRoller[idx].ForeColor = lcColorJuxt.ForeColor;
                    }
                    else if (list[idx].product_mode == 3)
                    {
                        LcRoller[idx].Height = lcroller01.Height / 5 - 2;

                        LcRoller[idx].Top = 12 + (pclRoller.Height - 24) / 5 * idx;
                        LcRoller[idx].BackColor = lcColorStack.BackColor;
                        LcRoller[idx].ForeColor = lcColorStack.ForeColor;
                    }
                    LcRoller[idx].Text = (idx + 1).ToString() + "-" + list[idx].plate_no.Trim();
                    LcRoller[idx].Visible = true;
                    LcRoller[idx].BringToFront();
                }
                for (int idx = list.Count; idx < maxSlanNumber; idx++)
                {
                    LcRoller[idx].Visible = false;
                }
            }
            catch
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
                    if (list[idx].product_mode == 1)
                    {
                        LcTrack[idx].Height = Convert.ToInt32(list[idx].plate_width * plcFur.Height / rollerWidth) - 8;
                        LcTrack[idx].Top = (plcFur.Height - LcTrack[idx].Height) / 2;
                        LcTrack[idx].BackColor = lcColorNormal.BackColor;
                        LcTrack[idx].ForeColor = lcColorNormal.ForeColor;
                        LcTrack[idx].Left = plcFur.Width - Convert.ToInt32( (list[idx].head_position - GobalVal.FurnaceChDoor) * plcFur.Width / furnaceLength) + 8; ;

                    }
                    else if (list[idx].product_mode == 2)
                    {
                        LcTrack[idx].Height = Convert.ToInt32(list[idx].plate_width * plcFur.Height / rollerWidth) - 8;

                        LcTrack[idx].Top = 12 + (plcFur.Height - 24) / 2 * idx;
                        LcTrack[idx].BackColor = lcColorJuxt.BackColor;
                        LcTrack[idx].ForeColor = lcColorJuxt.ForeColor;
                        LcTrack[idx].Left = plcFur.Width - Convert.ToInt32((list[idx].head_position - GobalVal.FurnaceChDoor) * plcFur.Width / furnaceLength) + 8; ;

                    }
                    else if (list[idx].product_mode == 3)
                    {
                        LcTrack[idx].Height = plcFur.Height / 5 - 2;

                        LcTrack[idx].Top = 12 + (plcFur.Height - 24) / 5 * idx;
                        LcTrack[idx].BackColor = lcColorStack.BackColor;
                        LcTrack[idx].ForeColor = lcColorStack.ForeColor;
                        LcTrack[idx].Left = plcFur.Width - Convert.ToInt32((list[idx].head_position - GobalVal.FurnaceChDoor) * plcFur.Width / furnaceLength) + 8; ;

                    }
                    LcTrack[idx].Text = (idx + 1).ToString() + "-" + list[idx].plate_no.Trim();
                    LcTrack[idx].Visible = true;
                    LcTrack[idx].BringToFront();
                }
                for (int idx = list.Count; idx < maxSlanNumber; idx++)
                {
                    LcTrack[idx].Visible = false;
                }
            }
            catch(Exception ex)
            {

            }
            return;
        }

        private void LoadFurTemp()
        {
            List<int> list = Pf.FurTemp();
            lcZoneU1.Text = list[0].ToString() + "-" + list[1].ToString();
            lcZoneD1.Text = list[2].ToString() + "-" + list[3].ToString();
            lcZoneU2.Text = list[4].ToString() + "-" + list[5].ToString();
            lcZoneD2.Text = list[6].ToString() + "-" + list[7].ToString();
            lcZoneU3.Text = list[8].ToString() + "-" + list[9].ToString();
            lcZoneD3.Text = list[10].ToString() + "-" + list[11].ToString();
            lcZoneU4.Text = list[12].ToString() + "-" + list[13].ToString();
            lcZoneD4.Text = list[14].ToString() + "-" + list[15].ToString();
            lcZoneU5.Text = list[16].ToString() + "-" + list[17].ToString();
            lcZoneD5.Text = list[18].ToString() + "-" + list[19].ToString();
            lcZoneU6.Text = list[20].ToString() + "-" + list[21].ToString();
            lcZoneD6.Text = list[22].ToString() + "-" + list[23].ToString();
            lcZoneU7.Text = list[24].ToString() + "-" + list[25].ToString();
            lcZoneD7.Text = list[26].ToString() + "-" + list[27].ToString();
            lcZoneU8.Text = list[28].ToString() + "-" + list[29].ToString();
            lcZoneD8.Text = list[30].ToString() + "-" + list[31].ToString();
            lcZoneU9.Text = list[32].ToString() + "-" + list[33].ToString();
            lcZoneD9.Text = list[34].ToString() + "-" + list[35].ToString();
            lcZoneU10.Text = list[36].ToString() + "-" + list[37].ToString();
            lcZoneD10.Text = list[38].ToString() + "-" + list[39].ToString();
            lcZoneU11.Text = list[40].ToString() + "-" + list[41].ToString();
            lcZoneD11.Text = list[42].ToString() + "-" + list[43].ToString();
            lcZoneU12.Text = list[44].ToString() + "-" + list[45].ToString();
            lcZoneD12.Text = list[46].ToString() + "-" + list[47].ToString();
            lcTempD.Text = list[48].ToString()  ;
        }

        private void gcFurView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int selectRow = gcFurView.GetSelectedRows()[0];
            if (gcFurView.RowCount == 0)
            {
                txtd2.Text = "";
                return;
            }
            memoEdit1.Text = Pf.GetAllPlateNo(this.gcFurView.GetRowCellValue(selectRow, "板号").ToString());
            txtd2.Text = this.gcFurView.GetRowCellValue(selectRow, "板号").ToString();
        
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


            if (cbeManual.SelectedIndex ==0)
            {
     
                
                    Pf.ChargeReturn(lstPlateno);
                
                
            }
            else
            {
              
                if (sArray.Count() > 0)
                {
                    Pf.HmiDisCharge(lstPlateno);
                }
            }
            memoEdit1.Text = "";
            cbeManual.SelectedIndex = -1;
        }
    }
}