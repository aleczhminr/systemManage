using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Controls.AlipayInfo
{
    public static class AlipayInfo
    {
        delegate bool AlipayInfoLogDALDelegate(T_AlipayInfoLogModel logModel);
        public static string getAlipayInfoModel(int id)
        {
            var model = T_AlipayInfoBLL.GetAlipayInfoModel(id);
            return CommonLib.Helper.JsonSerializeObject(model);
        }
        public static string getAlipayInfoRecord(int pageIndex, int userType, int displayType, string accId, string start, string end)
        {
            string Column = string.Empty;
            string strWhere = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"RowCount",""},
                {"PageCount",""},
                {"list",""}
            };
            if (Convert.ToDateTime(end) > Convert.ToDateTime(start))
            {
                if (start != "")
                {
                    DateTime stTime = Convert.ToDateTime(start);
                    strWhere += " createTime >='" + stTime.ToString("yyyy-MM-dd") + "' and";
                }
                if (end != "")
                {
                    DateTime edTime = Convert.ToDateTime(end);
                    strWhere += " createTime <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' and";
                }
            }
            if (userType != -99)
            {
                strWhere += " companyType=" + userType.ToString() + " and ";
            }
            if (displayType != -99)
            {
                strWhere += " status=" + displayType + " and ";
            }
            if (accId != "")
            {
                strWhere += " accId=" + accId + " and ";
            }
            if (strWhere.Length > 0)
            {
                strWhere = strWhere.Substring(0, strWhere.LastIndexOf('a'));
            }

            int pageCount = T_AlipayInfoBLL.GetPageCount(strWhere);
            int pageSize = 15;
            if (pageCount != 0)
            {
                dic["RowCount"] = pageCount.ToString();
            }
            else
            {
                dic["RowCount"] = "0";
            }
            if (pageCount > 0)
            {
                dic["PageCount"] = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(pageCount) / Convert.ToDecimal(pageSize))).ToString();
            }
            else
            {
                dic["PageCount"] = "0";
            }
            List<AlipayInfoModel> listitem = T_AlipayInfoBLL.GetPageCount(pageIndex, pageSize, Column, strWhere);
            dic["list"] = CommonLib.Helper.JsonSerializeObject(listitem, "yyyy-MM-dd HH:mm:ss");
            return CommonLib.Helper.JsonSerializeObject(dic);
        }

        public static string updateStatus(int accid, int oldstatus, int status, bool isGoNextStep, string remark, int alipayId, int operatorId, string operatorIP)
        {
            SaveLogForStatus(accid, oldstatus, status, operatorId, operatorIP);
            if (isGoNextStep)
            {
               SaveLogForRemark(accid, remark, operatorId, operatorIP); 
            }                                
            return T_AlipayInfoBLL.UpdateStatus(status, isGoNextStep, remark, alipayId);
        }

        private static void SaveLogForRemark(int accid, string remark, int operatorId, string operatorIP)
        {
            var logmodel = new T_AlipayInfoLogModel();
            logmodel.oldValue = "";
            logmodel.nowValue = remark;
            logmodel.createTime = DateTime.Now;
            logmodel.columnName = "remark";
            logmodel.accId = accid;
            logmodel.lgUserId = operatorId.ToString();
            logmodel.lgUserIp = operatorIP;
            AddAlipayInfoLog(logmodel);
        }

        private static void SaveLogForStatus(int accid, int oldstatus, int status, int operatorId, string operatorIP)
        {
            var logmodel = new T_AlipayInfoLogModel();
            logmodel.oldValue = oldstatus.ToString();
            logmodel.nowValue = status.ToString();
            logmodel.createTime = DateTime.Now;
            logmodel.columnName = "status";
            logmodel.accId = accid;
            logmodel.lgUserId = operatorId.ToString();
            logmodel.lgUserIp = operatorIP;
            AddAlipayInfoLog(logmodel);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="logmodel"></param>
        private static void AddAlipayInfoLog(T_AlipayInfoLogModel logmodel)
        {
            try
            {
                AlipayInfoLogDALDelegate FnDelegate = new AlipayInfoLogDALDelegate(AlipayInfoLogBLL.AddAlipayInfoLogBase);
                IAsyncResult iResult = FnDelegate.BeginInvoke(logmodel, new AsyncCallback(ar =>
                {
                    try
                    {
                        AlipayInfoLogDALDelegate dele = (AlipayInfoLogDALDelegate)((AsyncResult)ar).AsyncDelegate;
                        dele.EndInvoke(ar);
                    }
                    catch (Exception ex)
                    {

                    }
                }), null);
            }
            catch
            {

            }
        }
    }
}
