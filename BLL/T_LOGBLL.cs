using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public static class T_LOGBLL
    {
        /// <summary>
        /// 根据店铺ID 得到浏览器比率（店铺为0 则是所有）
        /// </summary>
        /// <param name="accountid">店铺ID</param>
        /// <param name="stat">开始时间</param>
        /// <param name="end">结束时间</param>
        public static List<dynamic> GetBrslastGroup(int accountid, DateTime statTime, DateTime endTime)
        {
            T_LOGDAL dal = new T_LOGDAL();
            return dal.GetBrslastGroup(accountid, statTime, endTime);
        }

        /// <summary>
        /// 登录分析
        /// </summary>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<SourceAnalyzeModel> LogSourceAnalyze(DateTime startTime, DateTime endTime, int[] sourceList)
        {
            T_LOGDAL dal = new T_LOGDAL();
            return dal.LogSourceAnalyze(startTime, endTime, sourceList);
        }
        /// <summary>
        /// 得到一个店铺的最后登录来源
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_LogAccountLast GetAccountLastLogSource(int accid)
        {
            T_LOGDAL dal = new T_LOGDAL();
            return dal.GetAccountLastLogSource(accid);
        }

        public static List<HourAnalysisItemList> GetLogRegTimeReportSourceBL(int timeType)
        { 
            T_LOGDAL dal = new T_LOGDAL();
            return dal.GetLogRegTimeReportSourceDAL(timeType);
        }

        public static List<HourAnalysisItemList> GetSaleRegTimeReportSourceBL(int timeType)
        {
            T_LOGDAL dal = new T_LOGDAL();
            return dal.GetSaleRegTimeReportSourceDAL(timeType);
        }

        public static List<HourAnalysisItemList> GetRegRegTimeReportSourceBL(int timeType)
        {
            T_LOGDAL dal = new T_LOGDAL();
            return dal.GetRegRegTimeReportSourceDAL(timeType);
        }

        public static List<HourAnalysisItemList> GetClientRegTimeReportSourceBL(int timeType)
        {
            T_LOGDAL dal = new T_LOGDAL();
            return dal.GetClientRegTimeReportSourceDAL(timeType);
        }
    }
}
