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
namespace HmiApp
{
    public partial class CheckData : DevExpress.XtraEditors.XtraForm
    {
        public CheckData()
        {
            InitializeComponent();
        }
        private List<string> plateNo = new List<string>();
        private List<int> chgValues = new List<int>();

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
        public void SetValue(List<string> value1, List<int> value2)
        {
            plateNo = value1;
            chgValues = value2;

        }

      

        private void CheckData_Load(object sender, EventArgs e)
        {
            if(chgValues.Count>=3)
            {
                if (chgValues[0] == 1)
                {
                    lcMode.Text = "正常模式";
                }
                else if (chgValues[0] == 2)
                {
                    lcMode.Text = "并板模式";
                }
                else
                {
                    lcMode.Text = "叠板模式";
                }
            }
            lcPlateNo1.Text = plateNo.Count >= 1 ? plateNo[0] : "";
            lcPlateNo2.Text = plateNo.Count >= 2 ? plateNo[1] : "";
            lcPlateNo3.Text = plateNo.Count >= 3 ? plateNo[2] : "";
            lcPlateNo4.Text = plateNo.Count >= 4 ? plateNo[3] : "";
            lcPlateNo5.Text = plateNo.Count >= 5 ? plateNo[4] : "";

            int[] tempset = Pf.GetTempsetCor();
            teKeepTempCor.Text = tempset[0].ToString();
            teKeepTimeCor.Text = tempset[1].ToString();

            teTemp.Text = chgValues.Count >= 2 ? (chgValues[1]+ tempset[0]).ToString() : "0";
            teHeatTime.Text = chgValues.Count >= 3 ? (chgValues[2]+ tempset[1]).ToString() : "0";
            teKeepTime.Text = chgValues.Count >= 4 ? (chgValues[3]+ tempset[1]).ToString() : "0";

            if (Pf.loadPlateFromFur1(plateNo))
            {
                ceFromFur1.Checked = true;
            }
           else
                ceFromFur1.Checked = false;


            setLevel1Allow();
        }

        private void sbtChecked_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtChecked_Click_1(object sender, EventArgs e)
        {
            //if (MessageBox.Show("是否将该批次钢板核对数据发送给PLC?", "上料操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            if (lcAllowColor.BackColor == Color.Red)
            {
                if (MessageBox.Show("PLC上料不允许,是否继续操作", "上料操作", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

            }
            int heattime = Convert.ToInt32(teHeatTime.Text);
            int heattemp = Convert.ToInt32(teTemp.Text);
            int keeptime = Convert.ToInt32(teKeepTime.Text);
            List<string> PlateNo = new List<string>();
            if (lcPlateNo1.Text != "")
                PlateNo.Add(lcPlateNo1.Text);
            if (lcPlateNo2.Text != "")
                PlateNo.Add(lcPlateNo2.Text);
            if (lcPlateNo3.Text != "")
                PlateNo.Add(lcPlateNo3.Text);
            if (lcPlateNo4.Text != "")
                PlateNo.Add(lcPlateNo4.Text);
            if (lcPlateNo5.Text != "")
                PlateNo.Add(lcPlateNo5.Text);
            if (PlateNo.Count == 0)
                return;
            Pf.Checked(PlateNo,heattime, heattemp, keeptime, chgValues[0]);

            LogUtil.Log(DateTime.Now.ToString() + "|核对,PlateNo=" + lcPlateNo1.Text  + lcPlateNo2.Text + lcPlateNo3.Text + lcPlateNo4.Text + lcPlateNo5.Text + "保温时间"+ keeptime.ToString()+"保温温度"+ heattemp.ToString());
            ////生产模式 chgValues[0]
            //if (ceFromFur1.Checked == true)
            //    {
            //    Pf.Checked(heattime, heattemp, keeptime,true);
            //}
            //    else
            //{
            //    Pf.Checked(heattime, heattemp, keeptime);
            //}


            //MessageBox.Show("上料操作成功.", "上料操作", MessageBoxButtons.OK);
            this.Close();
            //}
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            setLevel1Allow();
        }
        private void setLevel1Allow()
        {
            if (Pf.Level1CheckAllow())
                lcAllowColor.BackColor = Color.Green;
            else
                lcAllowColor.BackColor = Color.Red;
        }
    }
}