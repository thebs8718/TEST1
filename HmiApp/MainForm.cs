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
using System.Threading;
using DevExpress.XtraBars;
using TxyLib;
using System.Net;

namespace HmiApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public static string[] barname = new string[] { "bbi1", "bbi2", "bbi3", "bbi4", "bbi5", "bbi6", "bbi7", "bbiexit", "barSTime" };

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, 1080);
            this.Width = 1920;
            this.Width = 1080;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle; //固定单行边框
            ThreadStart thStart = new ThreadStart(Pro);//threadStart委托 
            Thread thread = new Thread(thStart);
            thread.Priority = ThreadPriority.Highest;
            thread.IsBackground = true; //关闭窗体继续执行
            thread.Start();
            barManager1.BeginUpdate();
            //添加菜单选项
            //bar1.AddItems(getMenuList());
            //添加点击事件
            barManager1.ItemClick += new ItemClickEventHandler(barButtonItem1_ItemClick);
            barManager1.EndUpdate();

            
            frmCheck frm = new frmCheck();
            frm.Tag = 1;
            frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
            frm.Show();
            xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口

            GobalVal.ComputerName = Dns.GetHostName();
        }
        //点击事件的实现
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BarButtonItem buttonItem = e.Item as BarButtonItem;
            BarManager manager = sender as BarManager;

            //MessageBox.Show(sender + "===" + buttonItem.Caption);
            if (buttonItem == null)
                return;

            int idx = GetBarName(buttonItem.Name);
            ShowForm(idx);
        }
        public void Pro()
        {
            while (true)
            {
                if (barSTime != null)
                    barSTime.Caption = "    " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "    ";
                Task.Delay(900);
            }

        }
        public int GetBarName(string BtName)
        {
            int idx = 0;
            for (idx = 0; idx < 9; idx++)
            {
                if (BtName == barname[idx])
                    return idx;
            }
            return idx;
        }
        public void ShowForm(int frmIdx)
        {
            bool isOpen = false;
            for (int idx = 0; idx < xtraTabbedMdiManager1.Pages.Count; idx++)
            {
                //Form temp = xtraTabbedMdiManager1.Pages[idx].MdiChild;
                //if (xtraTabbedMdiManager1.Pages[idx].MdiChild.Name == barname[idx])
                //{
                //    isOpen = true;
                //    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages[idx].MdiChild];    //使得标签的选择为当前新建的窗口
                //    break;
                //}
                if (Convert.ToInt32(xtraTabbedMdiManager1.Pages[idx].MdiChild.Tag) == frmIdx)
                {
                    isOpen = true;
                    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages[idx].MdiChild];    //使得标签的选择为当前新建的窗口
                    break;
                }
            }
            if (!isOpen)
            {
                //listform[frmIdx].MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                //listform[frmIdx].Show();
                //xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[listform[frmIdx]];    //使得标签的选择为当前新建的窗口
                if (frmIdx == 0)
                {
                    frmPdi frm = new frmPdi();
                    frm.Tag = frmIdx;
                    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                    frm.Show();
                    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                }
                else if (frmIdx == 1)
                {
                    frmCheck frm = new frmCheck();
                    frm.Tag = frmIdx;
                    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                    frm.Show();
                    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                }
                else if (frmIdx == 2)
                {
                    frmAllView frm = new frmAllView();
                    frm.Tag = frmIdx;
                    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                    frm.Show();
                    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                }
                else if (frmIdx == 3)
                {
                    frmDischangePlate frm = new frmDischangePlate();
                    frm.Tag = frmIdx;
                    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                    frm.Show();
                    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                }
                //else if (frmIdx == 3)
                //{
                //    frmCoilCurve2 frm = new frmCoilCurve2();
                //    frm.Tag = frmIdx;
                //    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                //    frm.Show();
                //    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                //}
                //else if (frmIdx == 4)
                //{
                //    frmBase frm = new frmBase();
                //    frm.Tag = frmIdx;
                //    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                //    frm.Show();
                //    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                //}
                //else if (frmIdx == 5)
                //{
                //    frmReport2 frm = new frmReport2();
                //    frm.Tag = frmIdx;
                //    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                //    frm.Show();
                //    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                //}
                //else if (frmIdx == 6)
                //{
                //    frmComm frm = new frmComm();
                //    frm.Tag = frmIdx;
                //    frm.MdiParent = this;    //设置新建窗体的父表单为当前活动窗口
                //    frm.Show();
                //    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[frm];    //使得标签的选择为当前新建的窗口
                //}
                else if (frmIdx == 7)
                {
                    this.Close();
                    Application.Exit();
                }
            }
        }
    }
}