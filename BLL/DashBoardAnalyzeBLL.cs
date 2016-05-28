using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class DashBoardAnalyzeBLL
    {
        /// <summary>
        /// 用于调试额临时方法
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static int TestGet(DateTime nowDay)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            dal.GetActiveUsrProp(nowDay);
            return 0;
        }

        /// <summary>
        /// 返回当天登录用户数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static DashBoardModel GetLoginDashBoardData(DateTime nowDay)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetLoginType(nowDay);
        }

        /// <summary>
        /// 返回当天活跃用户数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static DashBoardModel GetActiveDashBoardData(DateTime nowDay)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetActiveUsrProp(nowDay);
        }

        /// <summary>
        /// 返回当天新增用户的使用数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static DashBoardModel GetNewUsrDashBoardData(DateTime nowDay)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetNewUsrBehave(nowDay);
        }

        /// <summary>
        /// 返回当天新增用户的使用数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static DashBoardModel GetNewUsrDashBoardDataDetail(DateTime nowDay)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetNewUsrBehaveDetail(nowDay);
        }

        public static RegUsrDiff GetRegUsrDiff(DateTime dt)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetUsrRegDiff(dt);
        }

        public static LoginTypeDiff GetLoginTypeDiff(DateTime dt)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetLoginTypeDiff(dt);
        }

        /// <summary>
        /// 返回KPI数据和区分用户活跃类型后的数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static List<DashBoardModel> GetStatusType(DateTime nowDay)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetKPIProp(nowDay);
        }

        /// <summary>
        /// 获取转化率漏斗图模型
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static dynamic GetConversionFunnel(string sourceType,DateTime stDate, DateTime edDate)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();

            if (sourceType=="platform")
            {
                dynamic funnel = dal.GetUsrStatusNum(stDate, edDate);

                ConversionFunnel funnelModel = new ConversionFunnel();
                funnelModel.RegNum = funnel.RegNum;
                funnelModel.ActiveNum = funnel.ActiveNum;
                funnelModel.PayNum = funnel.PayNum;

                return funnelModel;
            }
            else
            {
                //系统来源
                if (sourceType.IndexOf('_') < 0)
                {
                    return dal.GetSpecFunnel(1, sourceType, stDate, edDate);
                }
                //百度系
                else if (sourceType == "market_baidu")
                {
                    return dal.GetSpecFunnel(2, sourceType, stDate, edDate);
                }
                //安卓其它渠道
                else
                {
                    return dal.GetSpecFunnel(3, sourceType, stDate, edDate);
                }
            }            
            
        }

        public static dynamic GetConversionFunnelEx(string activeList)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetSpecFunnelEx(activeList);
        }

        /// <summary>
        /// 返回当日登录用户ID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public static int[] GetLoginAccountId(int pageIndex, string active)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetLoginAccountId(pageIndex,active);
        }

        /// <summary>
        /// 返回活跃用户ID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int[] GetActiveAccountId(int pageIndex, int index)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetActiveAccountId(pageIndex,index);
        }

        /// <summary>
        /// 返回当日所有付费用户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int[] GetPaidAllAccountId(int pageIndex, int index)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetPaidAllAccountId(pageIndex, index);
        }

        public static int[] GetRetentionUserDetail(int pageIndex,int index,string date,string day)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetRetentionUserDetail(pageIndex, index, date,day);
        }

        public static int[] GetFunnelDetailUser(int pageIndex,string keyword,string activeList)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetFunnelDetailUser(pageIndex, keyword, activeList);
        }

        public static int[] GetLocationAccId(int pageIndex, string location)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetLocationAccId(pageIndex, location);
        }

        /// <summary>
        /// 返回不同活跃类型用户Id的通用方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int[] GetGeneralAccountId(int pageIndex, string type, int index)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetGeneralAccountId(pageIndex, type, index);
        }

        /// <summary>
        /// 获取详情数据列表
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static List<SysShopSummarizeInfo> GetGeneralList(int[] accId, string order = "", int pageIndex = 1)
        {
            SysRpt_ShopInfoDAL shopDal = new SysRpt_ShopInfoDAL();
            return shopDal.GetAccountSummarize(accId, order, pageIndex);
        }

        public static LocationUsrStatus GetLocationUsrStatus(int[] accId)
        {
            SysRpt_ShopInfoDAL shopDal = new SysRpt_ShopInfoDAL();
            return shopDal.GetLocationUsrStatus(accId);
        }

        /// <summary>
        /// 获取首页数据面板对应的用户数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int GetDashBoardUsrCount(string type, int index)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetDashBoardDetailUsrCount(type, index);
        }

        public static ActiveUsrList GetActiveListForVenn(DateTime stDate, DateTime edDate)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetActiveListForVenn(stDate, edDate);
        }

        public static ActiveUsrList GetVennUsrList(string type, DateTime stDate, DateTime edDate)
        {
            DashBoardAnalyzeDAL dal = new DashBoardAnalyzeDAL();
            return dal.GetUsrListForVenn(type, stDate, edDate);
        }
    }
}
