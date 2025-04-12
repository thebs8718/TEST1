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

namespace HmiApp
{
    public partial class frmRequestPdi : DevExpress.XtraEditors.XtraForm
    {
        public frmRequestPdi()
        {
            InitializeComponent();
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void setReadonly(bool isTrue)
        {
            textEdit1.ReadOnly = isTrue;
            textEdit2.ReadOnly = isTrue;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            setReadonly(false);
        }

        private void checkEdit1_Click(object sender, EventArgs e)
        {
            if  (checkEdit1.Checked)
            {
                setReadonly(true);
                checkEdit2.Checked = true;
            }
            else
            {
                setReadonly(false);
                checkEdit2.Checked = false;
            }
        }

        private void checkEdit2_Click(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
            {
                setReadonly(false);
                checkEdit1.Checked = true;
            }
            else
            {
                setReadonly(true);
                checkEdit1.Checked = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int requestType = 0;
            if (checkEdit1.Checked)
            {
                string PlateNo = textEdit1.Text;
                string HeatNo = textEdit2.Text;
                Pf.HmiRequestPdi(requestType, PlateNo, HeatNo);
            }
            else if(checkEdit2.Checked)
            {

                Pf.HmiRequestPdi(3, "1", "1");
            }
            this.Close();
        }
    }
}