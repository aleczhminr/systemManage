using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
namespace Controls.IntegralManage
{
    /// <summary>
    /// 积分商城
    /// </summary>
    public static class IntegralStore
    {

        /// <summary>
        /// 得到兑换列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="accid"></param>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetExchangeLogList(int pageIndex, int pageSize, int accid, string projectName, DateTime? startTime, DateTime? endTime, int eState)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> whereList = new List<DapperWhere>();

            if (accid > 0)
            {
                whereList.Add(new DapperWhere("accid", accid));
            }

            if (projectName.Length > 0)
            {
                whereList.Add(new DapperWhere("eProjectName", projectName, " eProjectName like '%'+ @eProjectName +'%' "));
            }

            if (startTime != null)
            {
                whereList.Add(new DapperWhere("startTime", startTime.Value.Date, " eInsertTime>=@startTime "));
            }
            if (endTime != null)
            {
                whereList.Add(new DapperWhere("endTime", endTime.Value.Date.Add(new TimeSpan(23, 59, 59)), " eInsertTime<=@endTime "));
            }

            if (eState > -1)
            {
                whereList.Add(new DapperWhere("eState", eState));
            }


            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_ExchangeLogBaseBLL.GetCount(whereList);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            int sumInt = BLL.Base.T_ExchangeLogBaseBLL.GetAllInt(whereList);

            List<T_ExchangeLogInfo> listModel = BLL.Base.T_ExchangeLogBaseBLL.GetList<T_ExchangeLogInfo>(pageIndex, pageSize, " * ", whereList, " id desc");

            List<int> accidList=new List<int>();
            foreach (T_ExchangeLogInfo model in listModel)
            {
                accidList.Add(model.accid);
                model.eStateName = Enum.GetName(typeof(Model.Enum.ExchangeLogEnum.State), model.eState);
            }
            if (accidList.Count > 0)
            {
               Dictionary<int,string> accountName = BLL.T_AccountBLL.GetCompanyName(accidList.ToArray());

               foreach (T_ExchangeLogInfo model in listModel)
               {
                   model.CompanyName = accountName[model.accid];
               }
            }


            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = listModel;
            list["sumInt"] = sumInt;

            return list;
        }
        /// <summary>
        /// 更新发货信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginstics">物流名</param>
        /// <param name="loginsticsnumber">物流号</param>
        /// <param name="sysName"></param>
        /// <returns></returns>
        public static bool UpdateLogistics(int id,string loginstics,string loginsticsnumber,string sysName)
        {
            return BLL.T_ExchangeLogBLL.UpdateLogistics(id, loginstics, loginsticsnumber, sysName);
        }

        /// <summary>
        /// 获取某日积分商城统计
        /// </summary>
        /// <returns></returns>
        public static IntegralExchangeModel GetDailyExchangeModel(DateTime dayDate)
        {
            return Sys_DailyIntegralExchangeBLL.GetDailyExchangeModel(dayDate);
        }

        /// <summary>
        /// 更新积分商城信息
        /// </summary>
        /// <param name="exchangeJson"></param>
        /// <returns></returns>
        public static string UpdateExchangeModel(DateTime dayDate, string exchangeJson)
        {
            IntegralExchangeModel model = new IntegralExchangeModel();
            Dictionary<int, string> exData = new Dictionary<int, string>();

            model = GetDailyExchangeModel(dayDate); //获取当日的model用以更新

            try
            {
                exData = CommonLib.Helper.JsonDeserializeObject<Dictionary<int, string>>(exchangeJson);
            }
            catch (Exception ex)
            {
                return "";
            }

            if (exData != null && exData.Count > 0)
            {
                foreach (KeyValuePair<int, string> item in exData)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        model.DataList.Find(x => x.ProductId == item.Key).VisitNum = Convert.ToInt32(item.Value);
                    }
                    else
                    {
                        model.DataList.Find(x => x.ProductId == item.Key).VisitNum = 0;
                    }
                }

                int reVal = Sys_DailyIntegralExchangeBLL.ModifyRecord(dayDate, model);

                if (reVal == 2)
                {
                    return "success";
                }
                else
                {
                    return "";
                }
            }
            
            else
            {
                return "";
            }

        }

        public static chartDataModel GetIntegralChart(DateTime stDate, DateTime edDate)
        {
            Dictionary<string, string> ColumnList = new Dictionary<string, string>();
            ColumnList.Add("IntegralPaid", "积分支出");
            ColumnList.Add("IntegralExchange", "积分兑换");

            List<dynamic> dataList = T_LogInfoBLL.GetIntegrals(stDate, edDate);

            chartDataModel chartModel = new chartDataModel();
            return chartModel.SetDataAboutDataTime(stDate, edDate, dataList, ColumnList, "积分兑换统计");
        }

        public static IntegralPieModel GetIntegralPieModel(DateTime stDate, DateTime edDate)
        {
            return IntegralStatBLL.GetIntegralPieModel(stDate, edDate);
        }
    }
}

