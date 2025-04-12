using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TxyLib;
namespace HmiApp
{
    public static class Pf
    {
        public static bool LoadData(string planno,string plateno)
        {
            bool returnval = false;
            StringBuilder strSql = new StringBuilder();
           
            try
            {
                strSql.Append(string.Format(@"select trim(heat_no) as 计划号 ,trim(plate_no) as 钢板号,trim(steel_grade) as 钢种,HEAT_MODE as 处理类型,
                                            KEEP_TEMP_AIM as 保温温度,HEAT_TIME_AIM as 在炉时间, KEEP_TIME_MIN_CORE as 保温时间,
                                            PLATE_LENGTH as 长度,PLATE_WIDTH as 宽度,PLATE_THICK as 厚度,PLATE_WEIGHT as 重量 FROM B_PDI_DATA where flag = 0 "));

            }
            catch
            {
                return returnval;
            }
            return returnval;
        }

        public static string PreCheck(string heatno,string plateno,int productmode)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format(@"select * from B_PDI_DATA where heat_no ='{0}' and plate_no = '{1}'", heatno, plateno));
                B_PDI_DATA pdidata = DBHelper.FindBySql<B_PDI_DATA>(strSql.ToString());
                if (pdidata == null)
                    return "未查询到钢板为["+ plateno+"]的生产计划,无法预上料";

                strSql.Clear();
                strSql.Append(string.Format(@"select * from B_CHARGE_ROLLER_DATA where flag = 0 order by INSIDE_SEQ_NO asc"));
                List<B_CHARGE_ROLLER_DATA> lstcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                int platenum = lstcrd.Count;
                if (productmode == DbFun.ProductModeNormal)
                {
                    if (platenum > 0)
                        return "正常模式钢板不能超过 一 块,请确认钢板数量";
                }
                else if (productmode == DbFun.ProductModeJuxt)
                {
                    if (platenum >= 2)
                        return "并板模式钢板不能超过 两 块,请确认钢板数量";
                }
                else if (productmode == DbFun.ProductModeStack)
                {
                    if (platenum >= 5)
                        return "叠板模式钢板不能超过 五 块,请确认钢板数量"; ;
                }
                foreach(var temp in lstcrd)
                {
                    if (temp.product_mode != productmode)
                        return "预上料钢板与已上料钢板生产模式不一致,请确认生产类型!" ;
                }
                int seqno = 0;
                int insideseqno = 0;
                if (platenum ==0)
                {
                    strSql.Clear();
                    strSql.Append(string.Format("select * from B_CHARGE_SEQ"));
                    B_CHARGE_SEQ bce = DBHelper.FindBySql<B_CHARGE_SEQ>(strSql.ToString());
                    if( bce.SEQ_DATA.ToString("yyMMdd") != DateTime.Now.ToString("yyMMdd"))
                    {
                        bce.SEQ_NO = 0;
                    }
                    bce.SEQ_NO += 1;
                    bce.SEQ_DATA = DateTime.Now;
                    insideseqno = 1;
                    string temp2 = bce.SEQ_DATA.ToString("yyMMdd").Substring(1); //20220119 防止数值过大导致类型容量异常,只取年的最后一位数
                    seqno = Convert.ToInt32(temp2) * 10000 + bce.SEQ_NO; ////2022 防止数值过大导致类型容量异常,只取年的最后一位数
                   // seqno = Convert.ToInt32(bce.SEQ_DATA.ToString("yyMMdd")) * 10000 + bce.SEQ_NO;

                    DBHelper.Update(bce);
                }
                else
                {
                   
                    seqno = lstcrd[0].seq_no  ;
                    insideseqno = lstcrd[0].inside_seq_no + lstcrd.Count ;
                }
                B_CHARGE_ROLLER_DATA bcrd = new B_CHARGE_ROLLER_DATA();
                bcrd.seq_no = seqno;
                bcrd.inside_seq_no = insideseqno;
                bcrd.product_mode = productmode;
                bcrd.heat_no = heatno;
                bcrd.plate_no = plateno;
                bcrd.heat_mode = pdidata.heat_mode;
                bcrd.keep_temp_aim = pdidata.keep_temp_aim;
                bcrd.keep_time_aim = pdidata.keep_time_min_sur;
                //bcrd.heat_time_aim = pdidata.heat_time_aim;
                int heattime = GetHeatTime(pdidata.plate_thick, pdidata.keep_temp_aim);
                bcrd.heat_time_aim = heattime >0? heattime: pdidata.heat_time_aim;
                bcrd.plate_length = pdidata.plate_length;
                bcrd.plate_width = pdidata.plate_width;
                bcrd.plate_thick = pdidata.plate_thick;
                bcrd.plate_weight = pdidata.plate_weight;
                bcrd.head_position = 20000;
                DBHelper.Insert(bcrd);

                pdidata.flag = 1;
                DBHelper.Update(pdidata);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return returnval;
        }

        public static int GetHeatTime(int thick,int discTemp)
        {
            int returnVal = 0;
            StringBuilder strSql = new StringBuilder();

            try
            {
                double thickmin = (double)thick / 100 - 0.01;
                double thickmax = (double)thick / 100 + 0.01;

                strSql.Append(string.Format("select  sum(HEAT_TIME) ,count(*) as slabnum    from  M_HEAT_TIME  where DISCHARGE_TEMP > {0}-5 and DISCHARGE_TEMP <= {0}+5  and  thick  > {1}   and  thick   < {2}", discTemp, thickmin, thickmax));
                DataSet ds =  DBHelper.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count == 0)
                    return 0;

                double totalval = Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                int totalnum = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                returnVal = Convert.ToInt32( totalval / totalnum);
            }
            catch(Exception  ex)
            {

            }
            return returnVal;
        }
        public static bool IsChecked(string plateno)
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where plate_no = '{0}' and flag = 0", plateno));
                if (DBHelper.Exists(strSql.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
            public static string PreCheckReturn( string plateno )
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                int idx = 0;
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                foreach(var temp in bcrd)
                {
                    if (idx > 0)
                    {
                        temp.inside_seq_no -= 1;
                        DBHelper.Update(temp);
                    }

                    if (temp.plate_no.Trim() == plateno)
                    {
                        idx = temp.inside_seq_no;
                        strSql.Clear();
                        strSql.Append(string.Format(" plate_no = '{0}'", temp.plate_no));

                        DBHelper.Delete<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                        strSql.Clear();
                        strSql.Append(string.Format("update B_PDI_DATA set flag = 0 where plate_no = '{0}'", plateno));
                        DBHelper.Query(strSql.ToString());
                    }
                
                }
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }

        public static string ChargeReturn(List<string> plateno)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                foreach(var tempPlateno in plateno)
                {
                    int idx = 0;
                    strSql.Clear();
                    strSql.Append(string.Format("select * from B_FUR_TRACK where plate_no = {0}", tempPlateno));
                    B_FUR_TRACK bftdata = DBHelper.FindBySql<B_FUR_TRACK>(strSql.ToString());
                    if (bftdata == null)
                        continue;

                    strSql.Clear();
                    strSql.Append(string.Format("delete B_FUR_TRACK where plate_no = {0}", tempPlateno));
                    DBHelper.Query(strSql.ToString());

                    strSql.Clear();
                    strSql.Append(string.Format("update b_pdi_data set flag = 0 where plate_no = {0} and heat_no ='{1}'", tempPlateno, bftdata.heat_no));
                    DBHelper.Query(strSql.ToString());

                    //发送模型
                    strSql.Clear();
                    strSql.Append(string.Format("select MDL_SEQ.nextval as mdlseq from dual"));
                    DataSet tempSeq2 = DBHelper.Query(strSql.ToString());
                    int mdlseq = Convert.ToInt32(tempSeq2.Tables[0].Rows[0]["mdlseq"]);

                    strSql.Clear();
                    strSql.Append(string.Format("insert into M_MDL_TRIGGER values({0},'{1}',2,0,sysdate,sysdate)", mdlseq, bftdata.plate_no));
                    DBHelper.Query(strSql.ToString());
                    LogUtil.Log(DateTime.Now.ToString() + "|装钢返回,PlateNo=" + bftdata.plate_no);

                }

            }
            catch (Exception ex)
            {

            }

            return returnval;
        }
        public static string PreCheckReturn()
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                int idx = 0;
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where flag = 1 order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                foreach (var temp in bcrd)
                {
                     
                    idx = temp.inside_seq_no;
                    strSql.Clear();
                    strSql.Append(string.Format(" plate_no = '{0}'", temp.plate_no));

                    DBHelper.Delete<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                    strSql.Clear();
                    strSql.Append(string.Format("update B_PDI_DATA set flag = 0 where plate_no = '{0}'",temp.plate_no));
                    DBHelper.Query(strSql.ToString());

                    LogUtil.Log(DateTime.Now.ToString() + "|取消核对,PlateNo=" + temp.plate_no);
                }
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }
        public static string PreMoveUp(string plateno)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                int idx = 0;
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                foreach (var temp in bcrd)
                {

                    if (temp.plate_no.Trim() == plateno)
                    {
                        idx = temp.inside_seq_no;
                        if (idx == 1)//第一项,无法上移
                        {
                            return "已经是第一项,无法移动!";
                        }

                        temp.inside_seq_no  = idx-1;
                        bcrd[idx - 1 - 1].inside_seq_no = idx;
                        DBHelper.Update(temp);
                        DBHelper.Update(bcrd[idx - 1 - 1]);
                        break;
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }

        public static string PreMoveDn(string plateno)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                int idx = 0;
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                foreach (var temp in bcrd)
                {

                    if (temp.plate_no.Trim() == plateno)
                    {
                        idx = temp.inside_seq_no;
                        if (idx == bcrd.Count)//最后一项,无法下移
                        {
                            return "已经是最后一项,无法移动!";
                        }

                        temp.inside_seq_no = idx + 1;
                        bcrd[idx -1 + 1].inside_seq_no = idx;
                        DBHelper.Update(temp);
                        DBHelper.Update(bcrd[idx - 1 + 1]);
                        break;
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }

        public static void Checked()
        {
                StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                int plate_length = 0;
                int plate_width = 0;
                int plate_thick = 0;
                foreach (var temp in bcrd)
                {
                    plate_length = plate_length > temp.plate_length ? plate_length : temp.plate_length;
                    plate_width = plate_width > temp.plate_width ? plate_width : temp.plate_width;
                    plate_thick = plate_thick > temp.plate_thick ? plate_thick : temp.plate_thick;
                    temp.flag = 1;
                    DBHelper.Update(temp);
                }

                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域PLC
                C_S_PLC_CHECK csmd = new C_S_PLC_CHECK();
                csmd.snd_seq = sndseq;
                int platerow = 1;
                if (bcrd[0].product_mode <= 2)
                    platerow = bcrd.Count;
                else
                    platerow = bcrd.Count + 1;
               
                csmd.plate_row = platerow;
                csmd.plate_length = plate_length;
                csmd.plate_width = plate_width;
                csmd.plate_thick = plate_thick;
                csmd.target_temp = bcrd[0].keep_temp_aim;
                csmd.heat_time = bcrd[0].heat_time_aim;
                csmd.plate_no = bcrd.Count>=1? bcrd[0].plate_no:" ";
                csmd.plate_no2 = bcrd.Count >= 2 ? bcrd[1].plate_no : " ";
                csmd.plate_no3 = bcrd.Count >= 3 ? bcrd[2].plate_no : " ";
                csmd.plate_no4 = bcrd.Count >= 4 ? bcrd[3].plate_no : " ";
                csmd.plate_no5 = bcrd.Count >= 5 ? bcrd[4].plate_no : " ";
                if (bcrd[0].heat_mode == "T")
                    csmd.heat_mode = 4;
                else if (bcrd[0].heat_mode == "A")
                    csmd.heat_mode = 1;
                else if (bcrd[0].heat_mode == "N")
                    csmd.heat_mode = 4;

                csmd.toc = DateTime.Now;
                csmd.tom = DateTime.Now;

                DBHelper.Insert(csmd);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.PlcCommName;
                snd_telegram.tel_type = 1;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);
            }
            catch
            {

            }
        }

        public static void UnloadToPlc( )
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

               

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.PlcCommName;
                snd_telegram.tel_type = 4;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);
            }
            catch
            {

            }
        }
        public static void Checked(List<string> PlateNo,int heattime, int heattemp, int keeptime,int product_mode)
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                 int plate_length = 0;
                int plate_width = 0;
                int plate_thick = 0;
                string heatmode = "T";


                foreach (var temp in PlateNo)
                {
                    strSql.Clear();
                    strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where plate_no = '{0}'",temp));
                    B_CHARGE_ROLLER_DATA  bcrd = DBHelper.FindBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());

                    plate_length = plate_length > bcrd.plate_length ? plate_length : bcrd.plate_length;
                    plate_width = plate_width > bcrd.plate_width ? plate_width : bcrd.plate_width;
                    plate_thick = plate_thick > bcrd.plate_thick ? plate_thick : bcrd.plate_thick;
                    bcrd.keep_time_aim = keeptime;
                    bcrd.flag = 1;
                    bcrd.heat_time_aim = heattime;
                    bcrd.keep_temp_aim = heattemp;
                    bcrd.plate_fur1 = 0;
                    heatmode = bcrd.heat_mode;
                    DBHelper.Update(bcrd);
                }

                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域L2
                C_S_PLC_CHECK csmd = new C_S_PLC_CHECK();
                csmd.snd_seq = sndseq;
                int platerow = 1;
                if (product_mode <= 2)
                    platerow = PlateNo.Count;
                else
                    platerow = PlateNo.Count + 1;

                csmd.plate_row = platerow;
     
                csmd.plate_length = plate_length;
                csmd.plate_width = plate_width;
                csmd.plate_thick = plate_thick;
                csmd.target_temp = heattemp;
                csmd.heat_time = heattime;
                csmd.keep_time = keeptime;
                csmd.plate_no = PlateNo.Count >= 1 ? PlateNo[0]  : " ";
                csmd.plate_no2 = PlateNo.Count >= 2 ? PlateNo[1]  : " ";
                csmd.plate_no3 = PlateNo.Count >= 3 ? PlateNo[2]  : " ";
                csmd.plate_no4 = PlateNo.Count >= 4 ? PlateNo[3]  : " ";
                csmd.plate_no5 = PlateNo.Count >= 5 ? PlateNo[4]  : " ";
                if (heatmode == "T")
                    csmd.heat_mode = 4;
                else if (heatmode == "A")
                    csmd.heat_mode = 1;
                else if (heatmode == "N")
                    csmd.heat_mode = 4;

                csmd.toc = DateTime.Now;
                csmd.tom = DateTime.Now;

                DBHelper.Insert(csmd);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.PlcCommName;
                snd_telegram.tel_type = 1;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);
            }
            catch(Exception ex)
            {

            }
        }


        public static void Checked(int heattime, int heattemp, int keeptime,bool plateFromFur1 = false)
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                int plate_length = 0;
                int plate_width = 0;
                int plate_thick = 0; 
                foreach (var temp in bcrd)
                {
                    plate_length = plate_length > temp.plate_length ? plate_length : temp.plate_length;
                    plate_width = plate_width > temp.plate_width ? plate_width : temp.plate_width;
                    plate_thick = plate_thick > temp.plate_thick ? plate_thick : temp.plate_thick;
                    temp.keep_time_aim = keeptime;
                    temp.flag = 1;
                    temp.heat_time_aim = heattime;
                    temp.keep_temp_aim = heattemp;
                    temp.plate_fur1 = 0;
                    DBHelper.Update(temp);
                    if (temp.plate_fur1 == 1)
                        plateFromFur1 = true;
                }

                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域L2
                C_S_PLC_CHECK csmd = new C_S_PLC_CHECK();
                csmd.snd_seq = sndseq;
                int platerow = 1;
                if (bcrd[0].product_mode <= 2)
                    platerow = bcrd.Count;
                else
                    platerow = bcrd.Count + 1;

                csmd.plate_row = platerow;
                //if (plateFromFur1)
                //{
                //    if (bcrd[0].product_mode == 1)
                //        csmd.plate_row = 11;
                //    else
                //        csmd.plate_row = 12;
                //}
                csmd.plate_length = plate_length;
                csmd.plate_width = plate_width;
                csmd.plate_thick = plate_thick;
                csmd.target_temp = heattemp;
                csmd.heat_time = heattime;
                csmd.keep_time = keeptime;
                csmd.plate_no = bcrd.Count >= 1 ? bcrd[0].plate_no : " ";
                csmd.plate_no2 = bcrd.Count >= 2 ? bcrd[1].plate_no : " ";
                csmd.plate_no3 = bcrd.Count >= 3 ? bcrd[2].plate_no : " ";
                csmd.plate_no4 = bcrd.Count >= 4 ? bcrd[3].plate_no : " ";
                csmd.plate_no5 = bcrd.Count >= 5 ? bcrd[4].plate_no : " ";
                if (bcrd[0].heat_mode == "T")
                    csmd.heat_mode = 4;
                else if (bcrd[0].heat_mode == "A")
                    csmd.heat_mode = 1;
                else if (bcrd[0].heat_mode == "N")
                    csmd.heat_mode = 4;

                csmd.toc = DateTime.Now;
                csmd.tom = DateTime.Now;

                DBHelper.Insert(csmd);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.PlcCommName;
                snd_telegram.tel_type = 1;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);
            }
            catch
            {

            }
        }
        public static void PdiHide(string PlateNo)
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("update b_pdi_Data set flag = 1 where plate_no = '{0}'",PlateNo));
                 DBHelper.Query(strSql.ToString());
                 
            }
            catch
            {

            }
        }
        public static void Reject()
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where flag = 1 order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                int plate_length = 0;
                int plate_width = 0;
                int plate_thick = 0;
                foreach (var temp in bcrd)
                {

                    strSql.Clear();
                    strSql.Append(string.Format("plate_no = '{0}'",temp.plate_no));
 
                    DBHelper.Delete<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                    LogUtil.Log(DateTime.Now.ToString() + "|计划吊销,PlateNo=" + temp.plate_no);
                }

                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域L2
                C_S_MES_REJECT csmr = new C_S_MES_REJECT();
                csmr.snd_seq = sndseq;
                int platerow = 1;
                if (bcrd[0].product_mode <= 2)
                    platerow = bcrd.Count;
                else
                    platerow = bcrd.Count + 1;
                int seqno = bcrd.Count;
                csmr.snd_seq = sndseq;
                csmr.plate_no = bcrd[0].plate_no.Trim();
                csmr.heat_no = bcrd[0].heat_no.Trim();
                csmr.reject_reason = "A";
                csmr.reject_position = "A";
                csmr.spare = " ";
                csmr.product_mode = bcrd[0].product_mode.ToString();
                csmr.plate_number = seqno.ToString();
                csmr.plate_no2 = seqno >= 2 ? bcrd[1].plate_no.Trim() : " ";
                csmr.plate_no3 = seqno >= 3 ? bcrd[2].plate_no.Trim() : " ";
                csmr.plate_no4 = seqno >= 4 ? bcrd[3].plate_no.Trim() : " ";
                csmr.plate_no5 = seqno >= 5 ? bcrd[4].plate_no.Trim() : " ";
                csmr.toc = DateTime.Now;
              
                DBHelper.Insert(csmr);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.MesCommName;
                snd_telegram.tel_type = 3;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);
            }
            catch(Exception ex)
            {

            }
        }

        public static string HmiModeChange(int productMode)
        {
            StringBuilder strSql = new StringBuilder();
            string returnval = "";
            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where flag = 0 order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());

                //切为正常模式
                if (productMode == 1)
                {
                    if (bcrd.Count > 1)
                    {
                        returnval = "正常模式钢板数量应该不超过 一 块!请确认钢板数量.";
                        return returnval;
                    }
                }
                else if (productMode == 2)
                {
                    if (bcrd.Count > 2)
                    {
                        returnval = "并板模式钢板数量应该不超过 两 块!请确认钢板数量.";
                        return returnval;
                    }
                }
                else if (productMode == 3)
                {
                    if (bcrd.Count > 5)
                    {
                        returnval = "并板模式钢板数量应该不超过 五 块!请确认钢板数量.";
                        return returnval;
                    }
                }
                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("update B_CHARGE_ROLLER_DATA set product_mode = {0} where flag = 0 ", productMode));
                DBHelper.Query(strSql.ToString());
                 
            }
            catch
            {

            }
            return returnval;
        }

        public static void HmiCharge()
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where flag = 1 order by inside_seq_no asc"));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                int maxlength = 0;
                foreach (var temp in bcrd)
                {
                    maxlength = maxlength > temp.plate_length ? maxlength : temp.plate_length;
                }
                foreach (var temp in bcrd)
                {

                    strSql.Clear();
                    strSql.Append(string.Format("plate_no = '{0}'", temp.plate_no));

                    DBHelper.Delete<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                    strSql.Clear();
                    strSql.Append(string.Format("select * from B_PDI_DATA where plate_no = '{0}' and heat_no = '{1}'",temp.plate_no,temp.heat_no));
                    B_PDI_DATA bpdData = DBHelper.FindBySql<B_PDI_DATA>(strSql.ToString());
                    if (bpdData == null)
                        continue;
                    B_FUR_TRACK tempBFT = new B_FUR_TRACK();
                    tempBFT.plate_no = bpdData.plate_no;
                    tempBFT.heat_no = bpdData.heat_no;
                    tempBFT.steel_grade = bpdData.steel_grade.Trim();
                    tempBFT.heat_mode = bpdData.heat_mode;
                    tempBFT.keep_temp_aim = temp.keep_temp_aim;
                    tempBFT.keep_temp_min = bpdData.keep_temp_min;
                    tempBFT.keep_temp_max = bpdData.keep_temp_max;
                    tempBFT.heat_time_min = bpdData.heat_time_min;
                    tempBFT.heat_time_aim = temp.heat_time_aim;
                    tempBFT.annealing_type = bpdData.annealing_type;
                    tempBFT.plate_thick = bpdData.plate_thick;
                    tempBFT.plate_width = bpdData.plate_width;
                    tempBFT.plate_length = bpdData.plate_length;
                    tempBFT.plate_weight = bpdData.plate_weight;
                    tempBFT.seq_no = temp.seq_no;
                    tempBFT.inside_seq_no = temp.inside_seq_no;
                    tempBFT.product_mode = temp.product_mode;
                    tempBFT.chargetime = DateTime.Now;
                    tempBFT.head_position = maxlength+ 56500;// bpdData.head_position;
                    tempBFT.move_speed = 0;
                    tempBFT.head_temp1 = 0;
                    tempBFT.head_temp2 = 0;
                    tempBFT.head_temp3 = 0;
                    tempBFT.mid_temp1 = 0;
                    tempBFT.mid_temp2 = 0;
                    tempBFT.mid_temp3 = 0;
                    tempBFT.tail_temp1 = 0;
                    tempBFT.tail_temp2 = 0;
                    tempBFT.tail_temp3 = 0;
                    tempBFT.toc = DateTime.Now;
                    tempBFT.tom = DateTime.Now;
                    tempBFT.chg_speed = 0;
                    tempBFT.keep_time_aim = temp.keep_time_aim;
                    tempBFT.keep_time_start = DateTime.Now.AddYears(100);
                    tempBFT.keep_time_remain = temp.keep_time_aim;
                    DBHelper.Insert(tempBFT);

                    //发送模型
                    strSql.Clear();
                    strSql.Append(string.Format("select MDL_SEQ.nextval as mdlseq from dual"));
                    DataSet tempSeq2 = DBHelper.Query(strSql.ToString());
                    int mdlseq = Convert.ToInt32(tempSeq2.Tables[0].Rows[0]["mdlseq"]);

                    strSql.Clear();
                    strSql.Append(string.Format("insert into M_MDL_TRIGGER values({0},'{1}',1,0,sysdate,sysdate)", mdlseq, tempBFT.plate_no));
                    DBHelper.Query(strSql.ToString());

                }

                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域L2
                C_S_MES_CHARGED csmc = new C_S_MES_CHARGED();
                csmc.snd_seq = sndseq;
                csmc.plate_no = bcrd.Count>=1 ? bcrd[0].plate_no:"n";
                csmc.heat_no = bcrd[0].heat_no;
                csmc.charge_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                csmc.spare = " ";

                csmc.product_mode = bcrd[0].product_mode.ToString();
                csmc.plate_number = bcrd.Count.ToString();
                //csmc.plate_no  = seqno >= 1 ? plateno[0]: " ";
                csmc.plate_no2 = bcrd.Count >= 2 ? bcrd[1].plate_no : "n";
                csmc.plate_no3 = bcrd.Count >= 3 ? bcrd[2].plate_no : "n";
                csmc.plate_no4 = bcrd.Count >= 4 ? bcrd[3].plate_no : "n";
                csmc.plate_no5 = bcrd.Count >= 5 ? bcrd[4].plate_no : "n";
                csmc.toc = DateTime.Now;
                csmc.tom = DateTime.Now;
                DBHelper.Insert(csmc);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.MesCommName;
                snd_telegram.tel_type = 2;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);

                LogUtil.Log(DateTime.Now.ToString() + "|手动装钢,PlateNo=" + csmc.plate_no +"|"+ csmc.plate_no2 + "|" + csmc.plate_no3 + "|" + csmc.plate_no4 + "|" + csmc.plate_no5);

            }
            catch (Exception ex)
            {

            }
        }
        public static string GetAllPlateNo(string plateno)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(string.Format("select SEQ_NO from  B_FUR_TRACK  where plate_no = '{0}'   ", plateno));
                DataSet ds = DBHelper.Query(strSql.ToString());
                string seqno = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
                strSql.Clear();
                strSql.Append(string.Format("select * from  B_FUR_TRACK  where seq_no = '{0}'   ", seqno));
                List<B_FUR_TRACK> lstData = DBHelper.FindListBySql<B_FUR_TRACK>(strSql.ToString());
                foreach(var temp in lstData)
                {
                    returnval += temp.plate_no.ToString()+ "\n";
                }
                if (returnval.Length > 0)
                    returnval = returnval.Substring(0, returnval.Length - 1);



            }
            catch
            {

            }
            return returnval;
        }

        //由于叠并.只取一块信息
        public static void HmiDisCharge(List<string> plateno)
        {

            StringBuilder strSql = new StringBuilder();
            try
            {
                int seqno = plateno.Count();
                if (seqno == 0 || seqno > 5)
                    return;
                string heatno = "";
                int keeptime = 0,totaltime = 0;
                TxyLib.B_FUR_TRACK bcrd = new B_FUR_TRACK();
                TxyLib.B_PDI_DATA bpdData = new B_PDI_DATA();
                foreach (var tempPlateNo in plateno)

                {
                    strSql.Clear();
                    strSql.Append(string.Format("select * from  B_PDI_DATA  where plate_no = '{0}' order by toc desc ", tempPlateNo));
                    bpdData = DBHelper.FindBySql<TxyLib.B_PDI_DATA>(strSql.ToString());

                    strSql.Clear();
                    strSql.Append(string.Format("select * from  B_FUR_TRACK  where plate_no = '{0}' order by toc desc ", tempPlateNo));
                    bcrd = DBHelper.FindBySql<TxyLib.B_FUR_TRACK>(strSql.ToString());

                    if ((bpdData == null) || (bcrd == null))
                    {
                        //错误处理
                        continue;
                    }
                    TimeSpan tskeep = DateTime.Now - bcrd.actual_keep_time;
                    keeptime = Convert.ToInt32(tskeep.TotalMinutes);
                    bcrd.disc_speed = 0;
                    bcrd.tom = DateTime.Now;

                    TimeSpan tstotal = bcrd.tom - bcrd.chargetime;
                    totaltime = Convert.ToInt32(tstotal.TotalMinutes);
                    strSql.Clear();
                    strSql.Append(string.Format("DELETE  B_FUR_TRACK  where plate_no = '{0}' ", tempPlateNo));

                    DBHelper.Query(strSql.ToString());
                    DBHelper.Insert(bcrd, "H_FUR_TRACK");

                    //发送模型
                    strSql.Clear();
                    strSql.Append(string.Format("select MDL_SEQ.nextval as mdlseq from dual"));
                    DataSet tempSeq2 = DBHelper.Query(strSql.ToString());
                    int mdlseq = Convert.ToInt32(tempSeq2.Tables[0].Rows[0]["mdlseq"]);

                    strSql.Clear();
                    strSql.Append(string.Format("insert into M_MDL_TRIGGER values({0},'{1}',2,0,sysdate,sysdate)", mdlseq, bcrd.plate_no));
                    DBHelper.Query(strSql.ToString());

                    LogUtil.Log(DateTime.Now.ToString() + "|手动出钢,PlateNo=" + bcrd.plate_no);
                }


                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域L2
                C_S_MES_DISCHARGED csmd = new C_S_MES_DISCHARGED();
                csmd.snd_seq = sndseq;
                csmd.heat_no = bcrd.heat_no;
                csmd.heat_mode = bcrd.heat_mode;
                csmd.fur_no = "2";
                csmd.charge_time = bcrd.chargetime.ToString("yyyyMMddHHmmss");
                csmd.discharge_time = DateTime.Now.ToString("yyyyMMddHHmmss"); ;
                csmd.keep_time_mode = bpdData.keep_time_mode.ToString();
                TimeSpan ts1 = DateTime.Now - bcrd.actual_keep_time;
                int totalmin = Convert.ToInt32(ts1.TotalMinutes);
                //csmd.keep_time_core = bpdData.keep_time_mode == 1 ? keeptime.ToString() : "0";
                //csmd.keep_time_surface = bpdData.keep_time_mode == 2 ? keeptime.ToString() : "0";
                //csmd.keep_time_avg = bpdData.keep_time_mode == 3 ? keeptime.ToString() : "0";
                csmd.keep_time_core = keeptime.ToString();
                csmd.keep_time_surface = keeptime.ToString();
                csmd.keep_time_avg = keeptime.ToString();
                ts1 = DateTime.Now - bcrd.chargetime;
                totalmin = Convert.ToInt32(ts1.TotalMinutes);
                csmd.heat_time = totaltime.ToString();
                csmd.discharge_temp_cal = bcrd.head_temp1.ToString();
                csmd.discharge_temp_pv = "0";
                csmd.plate_temp_core = bcrd.head_temp2.ToString(); ;
                csmd.plate_temp_surface = bcrd.head_temp1.ToString(); ;
                csmd.plate_temp_avg = bcrd.head_temp2.ToString(); ;
                csmd.charge_speed = bcrd.chg_speed < 9999 ? bcrd.chg_speed.ToString() : "0";
                csmd.discharge_speed = bcrd.disc_speed < 9999 ? bcrd.disc_speed.ToString() : "0";
                csmd.swing_mode = "0";
                csmd.spare = " ";
                csmd.product_mode = bcrd.product_mode.ToString();
                csmd.plate_number = seqno.ToString();
                csmd.plate_no = seqno >= 1 ? plateno[0] : "00000000000";
                csmd.plate_no2 = seqno >= 2 ? plateno[1] : "00000000000";
                csmd.plate_no3 = seqno >= 3 ? plateno[2] : "00000000000";
                csmd.plate_no4 = seqno >= 4 ? plateno[3] : "00000000000";
                csmd.plate_no5 = seqno >= 5 ? plateno[4] : "00000000000";
                csmd.toc = DateTime.Now;
                csmd.tom = DateTime.Now;

                DBHelper.Insert(csmd);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.MesCommName;
                snd_telegram.tel_type = 4;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);


            }
            catch (Exception ex)
            {

            }

        }
        public static void HmiReject(string PltNo)
        {

            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where plate_no = '{0}' order by toc desc", PltNo));
                List<B_CHARGE_ROLLER_DATA> bcrd = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
                int maxlength = 0;



            }
            catch (Exception ex)
            {

            }
        }

        public static void HmiDischargeAllow()
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.PlcCommName;
                snd_telegram.tel_type = 2;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);



            }
            catch (Exception ex)
            {

            }
        }

        public static List<B_CHARGE_ROLLER_DATA> RollerData()
        {
            List<B_CHARGE_ROLLER_DATA> list = new List<B_CHARGE_ROLLER_DATA>();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA  order by inside_seq_no asc"));
                list = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<B_CHARGE_ROLLER_DATA> PreRollerData(int flag)
        {
            List<B_CHARGE_ROLLER_DATA> list = new List<B_CHARGE_ROLLER_DATA>();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where flag = {0} order by inside_seq_no asc", flag));
                list = DBHelper.FindListBySql<B_CHARGE_ROLLER_DATA>(strSql.ToString());
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public static List<B_FUR_TRACK> FurData()
        {
            List<B_FUR_TRACK> list = new List<B_FUR_TRACK>();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_FUR_TRACK order by HEAD_POSITION desc"));
                list = DBHelper.FindListBySql<B_FUR_TRACK>(strSql.ToString());
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<int> FurTemp()
        {
            List<int> list = new List<int>();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from B_FUR_TEMP"));
                DataSet ds = DBHelper.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count ==1)
                {
                    for (int idx = 0; idx < 49; idx++)
                    {
                        list.Add(Convert.ToInt32( ds.Tables[0].Rows[0][idx + 1]));
                        
                    }
                }
                else
                {
                    for(int idx=0;idx<49;idx++)
                    {
                        list.Add(0);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<int> FurModelSetTemp()
        {
            List<int> list = new List<int>();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select * from C_S_PLC_SETPOINT"));
                DataSet ds = DBHelper.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    for (int idx = 0; idx < 24; idx++)
                    {
                        list.Add(Convert.ToInt32(ds.Tables[0].Rows[0][idx]));

                    }
                }
                else
                {
                    for (int idx = 0; idx < 24; idx++)
                    {
                        list.Add(0);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public static List<string> GetUnCheckPlate()
        {
            List<string> list = new List<string>();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append(string.Format("select PLATE_NO from B_CHARGE_ROLLER_DATA where flag = 0"));
                DataSet ds = DBHelper.Query(strSql.ToString());

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    list.Add(Convert.ToString(ds.Tables[0].Rows[idx][0]));

                }
                
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static decimal[] GetRollerSpeed()
        {
            decimal[] returnval = new decimal[138];
            StringBuilder strSql = new StringBuilder();
            for (int idx = 0; idx < 138; idx++)
            {
                returnval[idx] = 0;

            }
            try
            {
                strSql.Append(string.Format("select * from B_ROLLER_MAP "));
                DataSet ds = DBHelper.Query(strSql.ToString());
                if(ds.Tables[0].Rows.Count >0)
                {
                    for(int idx=0;idx< 138; idx++)
                    {
                        returnval[idx] = Convert.ToDecimal(ds.Tables[0].Rows[0][idx +1]);

                    }
                    
                }
                

            }
            catch
            {
                return returnval;
            }
            return returnval;
        }
        public static int[] GetTempsetCor()
        {
            int[] returnval = new int[3];
            StringBuilder strSql = new StringBuilder();
            for (int idx = 0; idx < 3; idx++)
            {
                returnval[idx] = 0;

            }
            try
            {
                strSql.Append(string.Format("select * from B_TEMPSET_CORR "));
                DataSet ds = DBHelper.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int idx = 0; idx < 3; idx++)
                    {
                        returnval[idx] = Convert.ToInt32(ds.Tables[0].Rows[0][idx + 1]);

                    }

                }


            }
            catch
            {
                return returnval;
            }
            return returnval;
        }
        public static List<int>  GetModelDefalut()
        {
            List<int>  returnval = new List<int>() ;
            StringBuilder strSql = new StringBuilder();
        
            try
            {
                strSql.Append(string.Format("select * from M_FURNACE "));
                DataSet ds = DBHelper.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {

                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][1]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][2]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][3]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][4]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][5]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][6]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][7]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][8]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][9]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][10]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][11]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][12]));
                    returnval.Add(Convert.ToInt32(ds.Tables[0].Rows[0][13]));


                }


            }
            catch
            {
                return returnval;
            }
            return returnval;
        }
        public static bool loadPlateFromFur1(List<string> plateNo)
        {
            StringBuilder strSql = new StringBuilder();

            try
            {

                foreach (var temp in plateNo)
                {
                    strSql.Append(string.Format("select PLATE_FUR1 from B_CHARGE_ROLLER_DATA where plate_no ='{0}'", temp));
                    DataSet ds = DBHelper.Query(strSql.ToString());
                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
                        return true;

                }
            }
            catch
            {

            }
            return false;
        }

        public static void HmiRequestPdi(int type,string heat_no, string plate_no )
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                 //插入发送
                strSql.Clear();
                strSql.Append(string.Format("select comm_seq.nextval as sndseq from dual"));
                DataSet tempSeq = DBHelper.Query(strSql.ToString());
                int sndseq = Convert.ToInt32(tempSeq.Tables[0].Rows[0]["sndseq"]);

                //发送区域L2
                C_S_MES_REQUESTPDI csmd = new C_S_MES_REQUESTPDI();
                csmd.snd_seq = sndseq;
                csmd.heat_no = heat_no;
                csmd.plate_no = plate_no;
                csmd.request_flag = type.ToString();

                csmd.toc = DateTime.Now;
                csmd.tom = DateTime.Now;

                DBHelper.Insert(csmd);

                C_S_TELEGRAM snd_telegram = new C_S_TELEGRAM();
                snd_telegram.snd_seq = sndseq;
                snd_telegram.comm_type = ConstData.MesCommName;
                snd_telegram.tel_type = 1;
                snd_telegram.flag = 1;
                snd_telegram.toc = DateTime.Now;
                snd_telegram.tom = DateTime.Now;
                DBHelper.Insert(snd_telegram);

            }
            catch
            {

            }
        }

        public static void CreateSeries(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName)
        {
            CreateSeries(chat, seriesName, seriesType, dataSource, xBindName, yBindName, null);

        }
        /// <summary>
        /// 创建Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series名字『诸如：理论电量』</param>
        /// <param name="seriesType">seriesType『枚举』</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="xBindName">ChartControl的X轴绑定</param>
        /// <param name="yBindName">ChartControl的Y轴绑定</param>
        /// <param name="createSeriesRule">Series自定义『委托』</param>
        public static void CreateSeries(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName, Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");

            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;

            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);

        }

        public static Series CreateSeries2(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName)
        {
            return CreateSeries2(chat, seriesName, seriesType, dataSource, xBindName, yBindName, null);

        }
        /// <summary>
        /// 创建Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series名字『诸如：理论电量』</param>
        /// <param name="seriesType">seriesType『枚举』</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="xBindName">ChartControl的X轴绑定</param>
        /// <param name="yBindName">ChartControl的Y轴绑定</param>
        /// <param name="createSeriesRule">Series自定义『委托』</param>
        public static Series CreateSeries2(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName, Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");

            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;

            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            //chat.Series.Add(_series);
            return _series;
        }
        public static DataTable LoadData(string plateNo)
        {
            DataTable dt = new DataTable();
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("select  *   from  M_FURNACE_PLATE_TEMP  where PLATE_NO = '{0}' order by PLATE_SEQ asc ", plateNo));

                List<M_FURNACE_PLATE_TEMP> lsttemp = DBHelper.FindListBySql<M_FURNACE_PLATE_TEMP>(strSql.ToString());
                if (lsttemp.Count == 0)
                    return null;


                dt.Columns.Add(new DataColumn("顺序", typeof(int)));
                dt.Columns.Add(new DataColumn("板号", typeof(string)));
                dt.Columns.Add(new DataColumn("时间", typeof(int)));
                dt.Columns.Add(new DataColumn("上表温度", typeof(decimal)));
                dt.Columns.Add(new DataColumn("中心温度", typeof(decimal)));
                dt.Columns.Add(new DataColumn("下表温度", typeof(decimal))); 
                int idx = 1;
                foreach(var temp in lsttemp)
                {
                    if (idx%12==1)
                    {
                        int seq = idx / 12;
                        dt.Rows.Add(new object[]
                          {  seq ,
                                temp.plate_no,
                               Math.Round( Convert.ToDecimal(temp.plate_seq / 12),2),
                                Convert.ToDecimal(temp.tail_temp1),
                                Convert.ToDecimal(temp.tail_temp2),
                                Convert.ToDecimal(temp.tail_temp3)
                              });
                    }
                   
                    idx++;
                }
       

            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public static DataTable LoadDiscPlateCurve(string plateNo)
        {
            DataTable dt = new DataTable();
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("select  chargetime   from  H_FUR_TRACK  where PLATE_NO = '{0}' order by chargetime desc ", plateNo));
                DataSet dstemp = DBHelper.Query(strSql.ToString());
                DateTime chgtime =Convert.ToDateTime( dstemp.Tables[0].Rows[0][0]);


                strSql.Clear();
                strSql.Append(string.Format("select  *   from  H_SLAB_TEMP  where PLATE_NO = '{0}' order by RECORD_TIME asc ", plateNo));

                List<H_SLAB_TEMP> lsttemp = DBHelper.FindListBySql<H_SLAB_TEMP>(strSql.ToString());
                if (lsttemp.Count == 0)
                    return null;


                dt.Columns.Add(new DataColumn("顺序", typeof(int)));
                dt.Columns.Add(new DataColumn("板号", typeof(string)));
                dt.Columns.Add(new DataColumn("时间", typeof(double)));
                dt.Columns.Add(new DataColumn("头部温度", typeof(decimal)));
                dt.Columns.Add(new DataColumn("中部温度", typeof(decimal)));
                dt.Columns.Add(new DataColumn("尾部温度", typeof(decimal)));
                dt.Columns.Add(new DataColumn("头部炉温", typeof(decimal)));
                dt.Columns.Add(new DataColumn("中部炉温", typeof(decimal)));
                dt.Columns.Add(new DataColumn("尾部炉温", typeof(decimal)));
                int idx = 1;
                foreach (var temp in lsttemp)
                {
                        TimeSpan timets = temp.record_time - chgtime;

                        dt.Rows.Add(new object[]
                          {  idx ,
                                temp.plate_no,
                               Math.Round( timets.TotalMinutes ,1),
                                Convert.ToDecimal(temp.head_temp1),
                                Convert.ToDecimal(temp.mid_temp2),
                                Convert.ToDecimal(temp.tail_temp1),

                                 Convert.ToDecimal(temp.furnace_bottom_temp_head/2+temp.furnace_top_temp_head/2),
                                Convert.ToDecimal(temp.furnace_bottom_temp_mid/2+temp.furnace_top_temp_mid/2),
                                Convert.ToDecimal(temp.furnace_bottom_temp_tail/2+temp.furnace_top_temp_tail/2) 

                              });
                   

                    idx++;
                }


            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public static string HmiUpdTempsetCor(int TempSet1, int TempSet2, int TempSet3)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(string.Format("update B_TEMPSET_CORR set KEEP_TEMP_CORR = {0}, KEEP_TIME_CORR = {1}, PYROMETER_TEMP_CORR = {2},tom=sysdate", TempSet1, TempSet2, TempSet3));
                DBHelper.Query(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }
        public static string HmiUpdDefaultTemp(int TempSet1)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(string.Format("update M_FURNACE set DEFAULT_TEMP = {0}", TempSet1));
                DBHelper.Query(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }

        public static string HmiUpdCorrTemp(List<int> CorrTemp)
        {
            string returnval = "";
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(string.Format("update M_FURNACE set ", CorrTemp[0]));
                for(int idx=0;idx<12;idx++)
                {
                    strSql.Append(string.Format("  ZONE{0}_COR = {1},", idx+1 , CorrTemp[idx]));
                }
                strSql.Append(string.Format(" tom =sysdate"));
                DBHelper.Query(strSql.ToString());
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }
        public static bool Level1CheckAllow()
        {
            bool returnval = false;
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(string.Format("select * from B_FUR_TRACK_MAP where PLATE_SEQ = 25"));
                DataSet temp = DBHelper.Query(strSql.ToString());
                if (temp.Tables[0].Rows.Count == 1)
                {
                    int sign =Convert.ToInt32( temp.Tables[0].Rows[0]["PLATE_SIGN"]);
                    if (sign == 0)
                        returnval = true;
                    else
                    {
                        string pltno = Convert.ToString(temp.Tables[0].Rows[0]["PLATE_NO"]);
                        strSql.Clear();
                        strSql.Append(string.Format("select * from B_CHARGE_ROLLER_DATA where PLATE_NO = '{0}' and flag = 1",pltno));
                        if(DBHelper.Exists(strSql.ToString()))
                            returnval = false;
                        else
                            returnval = true;
                    }
                }      
                else
                    returnval = false;
            }
            catch (Exception ex)
            {

            }

            return returnval;
        }





    }
}
