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
    public partial class frmDischangePlate : DevExpress.XtraEditors.XtraForm
    {

        public frmDischangePlate()
        {
            InitializeComponent();
        }
        bool[] lineStatus = new bool[6] { true, true, true, true, true, true};


        private void InitLoadChargeData(string sql ="")
        {
            try
            {


                GridView view = gcDiscView as GridView;


                view.BeginUpdate(); //开始视图的编辑，防止触发其他事件
                view.BeginDataUpdate(); //开始数据的编辑

                //修改附加选项
                view.OptionsView.ShowColumnHeaders = true;                         //因为有Band列了，所以把ColumnHeader隐藏
                view.OptionsView.ShowGroupPanel = false;                            //如果没必要分组，就把它去掉
                view.OptionsView.EnableAppearanceEvenRow = false;                   //是否启用偶数行外观
                view.OptionsView.EnableAppearanceOddRow = true;                     //是否启用奇数行外观 
                view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;   //是否显示过滤面板
                view.OptionsCustomization.AllowColumnMoving = false;                //是否允许移动列
                view.OptionsCustomization.AllowColumnResizing = true;              //是否允许调整列宽
                view.OptionsCustomization.AllowGroup = false;                       //是否允许分组
                view.OptionsCustomization.AllowFilter = false;                      //是否允许过滤
                view.OptionsCustomization.AllowSort = false;                         //是否允许排序
                view.OptionsSelection.EnableAppearanceFocusedCell = true;           //???
                view.OptionsBehavior.Editable = false;                               //是否允许用户编辑单元格
                view.OptionsView.AllowCellMerge = false;

                StringBuilder strSql = new StringBuilder();
                if (sql == "")
                {
                    strSql.Append(string.Format(@" select  trim(PLATE_NO) as 板号,trim(HEAT_NO) as 计划,trim(STEEL_GRADE) as 钢种,trim(HEAT_MODE)   as 模式      ,
                    KEEP_TEMP_AIM   as 目温, 
                    to_char(PLATE_THICK/100)||'*'||  to_char(PLATE_WIDTH)||'*'|| to_char(PLATE_LENGTH)||'*'||to_char(PLATE_WEIGHT)    as 规格,
                     to_char(CHARGETIME,'yyyy-mm-dd hh24:mi')  as 装时, to_char(tom,'yyyy-mm-dd hh24:mi')   as 出时, 
                    to_char( round(HEAD_TEMP1/3+HEAD_TEMP2/3+HEAD_TEMP3/3))   as 头温 ,
                    to_char( round(mid_temp1/3+mid_temp2/3+mid_temp3/3))   as 中温 ,to_char( round(tail_temp1/3+tail_temp2/3+tail_temp3/3))   as 尾温  ,
                    HEAT_TIME_AIM  as 加热时间 , round((tom-chargetime)*1440) as 实际加热, KEEP_TIME_AIM as 保温预计 ,
                    CASE WHEN sysdate < KEEP_TIME_START THEN  0  ELSE round((tom-KEEP_TIME_START )*1440) END as 实际保温,DISCHARGE_TEMP as 测温   from H_FUR_TRACK where to_char(tom,'yyyymmdd')= to_char(sysdate,'yyyymmdd')  order by tom desc"));
                }
                else
                {
                    strSql.Append(string.Format(@" select  trim(PLATE_NO) as 板号,trim(HEAT_NO) as 计划,trim(STEEL_GRADE) as 钢种,trim(HEAT_MODE)   as 模式      ,
                    KEEP_TEMP_AIM   as 目温, 
                    to_char(PLATE_THICK/100)||'*'||  to_char(PLATE_WIDTH)||'*'|| to_char(PLATE_LENGTH)||'*'||to_char(PLATE_WEIGHT)    as 规格,
                     to_char(CHARGETIME,'yyyy-mm-dd hh24:mi')  as 装时, to_char(tom,'yyyy-mm-dd hh24:mi')   as 出时,
                    to_char( round(HEAD_TEMP1/3+HEAD_TEMP2/3+HEAD_TEMP3/3)) as 头温 ,
                    to_char( round(mid_temp1/3+mid_temp2/3+mid_temp3/3))   as 中温 ,to_char( round(tail_temp1/3+tail_temp2/3+tail_temp3/3))   as 尾温  ,
                    HEAT_TIME_AIM  as 加热时间 , round((tom-chargetime)*1440) as 实际加热, KEEP_TIME_AIM as 保温预计 ,
                    CASE WHEN sysdate < KEEP_TIME_START THEN  0  ELSE round((tom-KEEP_TIME_START )*1440) END as 实际保温,DISCHARGE_TEMP as 测温 from H_FUR_TRACK  where {0}  order by tom desc", sql));
                }

                //to_char(CHARGETIME, 'yyyy-mm-dd hh24:mi') as 装时, to_char(tom, 'yyyy-mm-dd hh24:mi') as 出时, CHG_SPEED as 装速,DISC_SPEED as 出速 ,
                   


                DataSet ds = DBHelper.Query(strSql.ToString());

                //绑定数据源并显示
                gcDisc.DataSource = ds.Tables[0];
                gcDisc.MainView.PopulateColumns();
                view.Appearance.HeaderPanel.Font = new Font("Tahoma", 14);
                view.RowHeight = 30;
                for (int idx = 0; idx < 16; idx++)
                {
                    view.Columns[idx].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    view.Columns[idx].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    //view.Columns[idx].AppearanceCell.Font = new Font("Tahoma", 12); 
                    view.Columns[idx].AppearanceCell.Font = new Font("Tahoma", 14);
                 
                }

                //view.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //view.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                //view.Columns[0].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[1].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[2].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[3].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[4].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[5].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[6].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[7].AppearanceCell.Font = new Font("Tahoma", 12);
                //view.Columns[0].Width = 120;
                //view.Columns[7].Width = 140;
                //view.Columns[9].Width = 120;
                //view.Columns[10].Width = 120;
                //view.Columns[17].Width = 120;
                //view.Columns[18].Width = 120;
                view.Columns[0].Width = 100;
                view.Columns[1].Width = 80;
                view.Columns[2].Width = 80;
                view.Columns[5].Width = 140;
                view.Columns[6].Width = 100;
                view.Columns[7].Width = 100;
                view.Columns[3].Width = 40;
                view.Columns[4].Width = 40;
                view.Columns[8].Width = 40;
                view.Columns[9].Width = 40;
                view.Columns[10].Width = 40;
                view.Columns[11].Width = 40;
                view.Columns[12].Width = 40;
                view.Columns[13].Width = 40;
                view.Columns[14].Width = 40;
                view.Columns[15].Width = 40; 

                //view.Columns[6].Width = 70;
                //view.Columns[7].Width = 150;

                view.EndDataUpdate();//结束数据的编辑
                view.EndUpdate();   //结束视图的编辑
            }
            catch (Exception ex)
            {

            } 
            return;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string starttime1 = dtstart.Text;
            string endtime1 = dtend.Text;
            string starttime2 = testart.Text;
            string endtime2 = teend.Text;
            StringBuilder strSql = new StringBuilder();
 
            strSql.Append(string.Format(" to_char(tom,'yyyy-mm-dd hh24:mi')>'{0}' and  to_char(tom,'yyyy-mm-dd hh24:mi')<'{1}'", starttime1+" "+ starttime2.PadLeft(8,'0'), endtime1 + " " + endtime2.PadLeft(8,'0')));
             
            InitLoadChargeData(strSql.ToString());
        }

        private void gcDisc_Load(object sender, EventArgs e)
        {
            this.dtstart.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtstart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtstart.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtstart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtstart.Properties.Mask.EditMask = "yyyy-MM-dd";


            this.dtend.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtend.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtend.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtend.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtend.Properties.Mask.EditMask = "yyyy-MM-dd";

            //this.testart.Properties.DisplayFormat.FormatString = "HH:mm";
            //this.testart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //this.testart.Properties.EditFormat.FormatString = "HH:mm";
            //this.testart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //this.testart.Properties.Mask.EditMask = "HH:mm";

            //this.teend.Properties.DisplayFormat.FormatString = "HH:mm";
            //this.teend.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //this.teend.Properties.EditFormat.FormatString = "HH:mm";
            //this.teend.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //this.teend.Properties.Mask.EditMask = "HH:mm";
            PFunction.BindCustomDrawRowIndicator(gcDiscView);
        }

        private void frmDischangePlate_Load(object sender, EventArgs e)
        {
            dtstart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtend.Text = DateTime.Now.ToString("yyyy-MM-dd");
            teend.Text = "23:59:50";

            LoadNowShift();
        }

        private void gcDiscView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                
            }

        }



        private void LoadNowShift()
        {
            StringBuilder strSql = new StringBuilder();

           
            try
            {
                if (!ceShift.Checked)
                    return;
                int hours =Convert.ToInt32( DateTime.Now.ToString("HHmm"));
                if (hours<800)
                {
                    string startDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    string startTime = "20:00:00";
                    string endDay = DateTime.Now.ToString("yyyy-MM-dd");
                    string endTime = "08:00:00";

                    strSql.Append(string.Format(" to_char(tom,'yyyy-mm-dd hh24:mi')>='{0}' and  to_char(tom,'yyyy-mm-dd hh24:mi')<'{1}'", startDay + " " + startTime.PadLeft(8, '0'), endDay + " " + endTime.PadLeft(8, '0')));

                }
                else if ((hours >= 800)&&(hours<2000))
                {
                    string startDay = DateTime.Now.ToString("yyyy-MM-dd");
                    string startTime = "08:00:00";
                    string endDay = DateTime.Now.ToString("yyyy-MM-dd");
                    string endTime = "20:00:00";

                    strSql.Append(string.Format(" to_char(tom,'yyyy-mm-dd hh24:mi')>='{0}' and  to_char(tom,'yyyy-mm-dd hh24:mi')<'{1}'", startDay + " " + startTime.PadLeft(8, '0'), endDay + " " + endTime.PadLeft(8, '0')));


                }
                else if (hours >= 2000)
                {
                    string startDay = DateTime.Now.ToString("yyyy-MM-dd");
                    string startTime = "20:00:00";
                    string endDay = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                    string endTime = "08:00:00";

                    strSql.Append(string.Format(" to_char(tom,'yyyy-mm-dd hh24:mi')>='{0}' and  to_char(tom,'yyyy-mm-dd hh24:mi')<'{1}'", startDay + " " + startTime.PadLeft(8, '0'), endDay + " " + endTime.PadLeft(8, '0')));

                }
                //DataSet dsNow =  DBHelper.Query(strSql.ToString());
                //dsNow.Tables[0].Rows.Count != 
                if (gcDiscView.RowCount == 0)
                {
                    InitLoadChargeData(strSql.ToString());
                }
                else
                {
                    string gsPlateNo =  this.gcDiscView.GetRowCellValue(0, "板号").ToString();
                    string sql = "select trim(plate_no) as plate_no from H_FUR_TRACK where" + strSql.ToString() +" order by tom desc";
                      DataSet dsNow = DBHelper.Query(sql);       
                      if (dsNow.Tables[0].Rows.Count !=0)
                        {
                            string dbPlateNo = Convert.ToString(dsNow.Tables[0].Rows[0]["plate_no"]);
                            if (dbPlateNo!= gsPlateNo)
                            {
                                InitLoadChargeData(strSql.ToString());
                            }
                        }
                      else
                    {
                        InitLoadChargeData(strSql.ToString());
                    }
                     
                }

            }
            catch(Exception ex)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadNowShift();
        }

        private void ceShift_CheckedChanged(object sender, EventArgs e)
        {
            if (ceShift.Checked)
            {


                LoadNowShift();
            }
        }

        private void gcDiscView_DoubleClick(object sender, EventArgs e)
        {
            if (gcDiscView.RowCount == 0)
                return;

            int selectRow = gcDiscView.GetSelectedRows()[0];
            string plateno = this.gcDiscView.GetRowCellValue(selectRow, "板号").ToString();
            frmPlateCurve temp = new frmPlateCurve();
            temp.SetPlateno(plateno);
            temp.SetType(2);
            temp.SelineStatus(lineStatus);
            temp.ShowDialog();
        }

        private void ceb1_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit temp = (CheckEdit)sender;
            int seq =Convert.ToInt32( temp.Name.Substring(temp.Name.Length - 1));
            lineStatus[seq - 1] = temp.Checked;
        }
    }
}