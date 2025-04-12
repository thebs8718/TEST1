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
    public partial class frmModifyPdi : DevExpress.XtraEditors.XtraForm
    {
        string[] modify = new string[6];
        public frmModifyPdi()
        {
            InitializeComponent();
        }

        public void SetModify(string[] temp)
        {
            modify = temp;
        }

        private void frmModifyPdi_Load(object sender, EventArgs e)
        {
            te1.Text = modify[0];
            te2.Text = modify[1];
            te3.Text = modify[2];
            te4.Text = modify[3];
            te5.Text = modify[4];
            te6.Text = modify[5];
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string[] newvalue = new string[6];
            newvalue[0] = te1.Text;
            newvalue[1] = te2.Text;
            newvalue[2] = te3.Text;
            newvalue[3] = te4.Text;
            newvalue[4] = te5.Text;
            newvalue[5] = te6.Text;
            for (int idx = 0; idx < 6; idx++)
            {
                if( (newvalue[idx] != modify[idx])|| (cePdiFlag.Checked))
                {
                    StringBuilder strSql = new StringBuilder();
                    if (cePdiFlag.Checked)
                    {
                        strSql.Append(string.Format("update b_pdi_data set PLATE_LENGTH={0},PLATE_WIDTH={1},PLATE_THICK={2},PLATE_WEIGHT={3} ,flag = 0 where heat_no = '{4}' and  plate_no='{5}'",
                      newvalue[2], newvalue[3], newvalue[4], newvalue[5], newvalue[0], newvalue[1]));
                    }
                    else
                    {
                        strSql.Append(string.Format("update b_pdi_data set PLATE_LENGTH={0},PLATE_WIDTH={1},PLATE_THICK={2},PLATE_WEIGHT={3} where heat_no = '{4}' and  plate_no='{5}'",
                      newvalue[2], newvalue[3], newvalue[4], newvalue[5], newvalue[0], newvalue[1]));
                    }
              
                  
                    DBHelper.Query(strSql.ToString());
                    break;

                }
            }
            this.Close();
        }


    }
}