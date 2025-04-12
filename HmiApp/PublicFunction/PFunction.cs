using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmiApp
{
   public class PFunction
    {
        public static void BindCustomDrawRowIndicator(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.IndicatorWidth = CalcIndicatorDefaultWidth(view);
            view.CustomDrawRowIndicator += (s, e) =>
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            };
            view.TopRowChanged += (s, e) =>
            {
                int width = CalcIndicatorBestWidth(view);
                if ((view.IndicatorWidth - 4 < width || view.IndicatorWidth + 4 > width) && view.IndicatorWidth != width)
                {
                    view.IndicatorWidth = width;
                }
            };

        }
        /// <summary>
        /// 计算行头宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        static int CalcIndicatorBestWidth(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            Graphics graphics = new Control().CreateGraphics();
            SizeF sizeF = new SizeF();
            int count = view.TopRowIndex + ((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)view.GetViewInfo()).RowsInfo.Count;
            if (count == 0)
            {
                count = 30;
            }
            sizeF = graphics.MeasureString(count.ToString(), view.Appearance.Row.Font);
            return Convert.ToInt32(sizeF.Width) + 20;
        }
        /// <summary>
        /// 计算默认的宽度
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        static int CalcIndicatorDefaultWidth(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            var grid = view.GridControl;
            Graphics graphics = new Control().CreateGraphics();
            SizeF sizeF = new SizeF();
            int rowHeight = 22;//22是Row的估计高度
            if (view.RowHeight > 0)
            {
                rowHeight = view.RowHeight;
            }
            int count = grid != null ? grid.Height / rowHeight : 30;
            sizeF = graphics.MeasureString(count.ToString(), view.Appearance.Row.Font);
            return Convert.ToInt32(sizeF.Width) + 20;
        }


        
    }
}
