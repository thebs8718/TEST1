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
using TxyLib;
using DevExpress.XtraGrid.Views.Grid;

namespace HmiApp
{
    public partial class frmPdi : DevExpress.XtraEditors.XtraForm
    {
        string[] ModifyPdi;
        public frmPdi()
        {
            InitializeComponent();
        }

        private void frmPdi_Load(object sender, EventArgs e)
        {
            InitLoadPdiData("");
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
                strSql.Append(string.Format(@"select PLATE_NO as 钢板号  ,HEAT_NO as 热处理计划号  , FUR_NO as 热处理炉号  ,ACT_TOWARD as 实际好面朝向  , 
                                            CON_TOWARD as 合同好面朝向, STEEL_CODE as 钢种代码, VAR_CODE as 厚板品种代码, STEEL_MARK as 出钢记号,
                                            STEEL_GRADE as 钢种牌号, HEAT_MODE as 热处理方式代码, KEEP_TEMP_AIM as 保温温度目标值, KEEP_TEMP_MAX as 保温温度上限值,
                                             KEEP_TEMP_MIN as 保温温度下限值, HEAT_TIME_AIM as 最小保持时间, HEAT_TIME_AIM as 母板热处理在炉时间目标值, HEAT_TIME_MIN as 最小保温时间,
                                KEEP_TIME_MODE as 最小在炉时间, ANNEALING_TYPE as 保温时间类型, PLATE_THICK as 钢板厚度, PLATE_WIDTH as 钢板宽度, PLATE_LENGTH as 钢板长度,
                                PLATE_WEIGHT as 钢板重量        from B_PDI_DATA"));

                if (Sql != "")
                    strSql.Append(string.Format(@" where (UPPER(plate_no) like '%{0}%' or UPPER(heat_no) like '%{0}%')", Sql.ToUpper()));


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
               
                //view.Columns[0].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[1].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[2].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[3].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[4].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[5].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[6].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[7].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[8].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[9].AppearanceCell.Font = new Font("Tahoma", 14);
                //view.Columns[10].AppearanceCell.Font = new Font("Tahoma", 14);

                view.EndDataUpdate();//结束数据的编辑
                view.EndUpdate();   //结束视图的编辑

                ModifyPdi = new string[6] { "0", "0", "0", "0", "0", "0"};
            }
            catch (Exception ex)
            {

            }
            return;
        }

        private void sbnRequest_Click(object sender, EventArgs e)
        {
            frmRequestPdi temp = new frmRequestPdi();
            temp.Show();
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
            ModifyPdi[0] = this.gcPDIView.GetRowCellValue(selectRow, "热处理计划号").ToString();
            ModifyPdi[1] = this.gcPDIView.GetRowCellValue(selectRow, "钢板号").ToString();
            ModifyPdi[2] = this.gcPDIView.GetRowCellValue(selectRow, "钢板长度").ToString();
            ModifyPdi[3] = this.gcPDIView.GetRowCellValue(selectRow, "钢板宽度").ToString();
            ModifyPdi[4] = this.gcPDIView.GetRowCellValue(selectRow, "钢板厚度").ToString();
            ModifyPdi[5] = this.gcPDIView.GetRowCellValue(selectRow, "钢板重量").ToString();
        }

        private void sbnModify_Click(object sender, EventArgs e)
        {
            frmModifyPdi temp = new frmModifyPdi();
            temp.SetModify(ModifyPdi);
            temp.ShowDialog();


            InitLoadPdiData(TxteQuery.Text.Trim());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string heat_no = ModifyPdi[0];
                string plate_no = ModifyPdi[1];

                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format(@"select * from B_FUR_TRACK where plate_no  ='{1}' and heat_no = '{0}'", heat_no, plate_no));
                if (DBHelper.Exists(strSql.ToString()))
                {
                    MessageBox.Show("钢板号<" + plate_no + "> 正在炉内,无法删除计划!", "删除操作", MessageBoxButtons.OK);
                    return;
                }
                strSql.Clear();
                strSql.Append(string.Format(@"select * from B_CHARGE_ROLLER_DATA where plate_no  ='{1}' and heat_no = '{0}'", heat_no, plate_no));
                if (DBHelper.Exists(strSql.ToString()))
                {
                    MessageBox.Show("钢板号<" + plate_no + "> 正在辊道,无法删除计划!", "删除操作", MessageBoxButtons.OK);
                    return;
                }

                if(MessageBox.Show("删除计划,钢板号<" + plate_no + ">  !", "删除操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    strSql.Clear();
                    strSql.Append(string.Format(@"delete B_PDI_DATA where plate_no  ='{1}' and heat_no = '{0}'", heat_no, plate_no));
                    DBHelper.Query(strSql.ToString());
                    InitLoadPdiData(TxteQuery.Text.Trim());
                }

                LogUtil.Log(DateTime.Now.ToString() + "|删除计划,计划号:" + heat_no+"|板号:"+ plate_no);
            }
            catch
            {

            }
           
        }
    }
}