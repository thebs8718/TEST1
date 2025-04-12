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
using DevExpress.XtraCharts;

namespace HmiApp
{
    public partial class frmPlateCurve : DevExpress.XtraEditors.XtraForm
    {
        public frmPlateCurve()
        {
            InitializeComponent();
        }

        public string plateno = "";
        public int type = 1;
        bool[] lineStatus = new bool[6] { true, true, true, true, true, true };

        private void initCurve(string curve_name)
        {
            //清空ChartControl控件                                    									
            chartControl1.Series.Clear();
            //创建图形对象的列表                                      									
            List<Series> list = new List<Series>();

            if (type == 1)
            {
                //通过LoadData返回一个DataTable                       									
                DataTable dtXY = Pf.LoadData(curve_name);
                //创建一个图形对象                                    									
                //Series series = CreateSeries("温度曲线", ViewType.Line, dtXY, 0);
                //CreateChart(dtXY);
                //Pf.CreateSeries(chartControl1, "位置", ViewType.Line, dtXY, "顺序", "位置");
                Pf.CreateSeries(chartControl1, "上表温度", ViewType.Line, dtXY, "顺序", "上表温度");
                Pf.CreateSeries(chartControl1, "中心温度", ViewType.Line, dtXY, "顺序", "中心温度");
                Pf.CreateSeries(chartControl1, "下表温度", ViewType.Line, dtXY, "顺序", "下表温度");
            }
            else if (type == 2)
            {
                //通过LoadData返回一个DataTable                       									
                DataTable dtXY = Pf.LoadDiscPlateCurve(curve_name);
                //创建一个图形对象                                    									
                //Series series = CreateSeries("温度曲线", ViewType.Line, dtXY, 0);
                //CreateChart(dtXY);
                //Pf.CreateSeries(chartControl1, "位置", ViewType.Line, dtXY, "顺序", "位置");
                if (lineStatus[0])
                Pf.CreateSeries(chartControl1, "头部温度", ViewType.Line, dtXY, "时间", "头部温度");
                if (lineStatus[1])
                    Pf.CreateSeries(chartControl1, "中部温度", ViewType.Line, dtXY, "时间", "中部温度");
                if (lineStatus[2])
                    Pf.CreateSeries(chartControl1, "尾部温度", ViewType.Line, dtXY, "时间", "尾部温度");
                if (lineStatus[3])
                    Pf.CreateSeries(chartControl1, "头部炉温", ViewType.Line, dtXY, "时间", "头部炉温");
                if (lineStatus[4])
                    Pf.CreateSeries(chartControl1, "中部炉温", ViewType.Line, dtXY, "时间", "中部炉温");
                if (lineStatus[5])
                    Pf.CreateSeries(chartControl1, "尾部炉温", ViewType.Line, dtXY, "时间", "尾部炉温");
            }

            return;
        }

         
        public void SetPlateno(string temp)
        {
            plateno = temp;
        }
        public void SetType(int temp)
        {
            type = temp;
        }
        public void SelineStatus(bool[] temp)
        {
            lineStatus = temp;
        }
        private void frmPlateCurve_Load(object sender, EventArgs e)
        {
            this.Left = 150;
            this.Top = 300;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            if (plateno!="")
            {
                this.Text = plateno + "曲线";
                initCurve(plateno);
            }
            
        }
    }
}