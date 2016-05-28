using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model.Model4View;

namespace BLL
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    public static class T_AccountBLL
    {
        /// <summary>
        /// 得到店铺基本信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountBasic GetAccountBasic(int accid)
        {
            T_AccountDAL DAL = new T_AccountDAL();
            T_AccountBasic model = DAL.GetAccountBasic(accid);
            if (model != null)
            {
                model.aotjbName = Enum.GetName(typeof(Model.Enum.AccountEnum.StoreVer), model.aotjb);
                model.activeName = Enum.GetName(typeof(Model.Enum.AccountEnum.ActiveStatus), model.active);
            }

            return model;
        }
        /// <summary>
        /// 得到店铺今日汇总信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountSummarize.TodaySummarize GetAccountTodaySummarize(int accid)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetAccountTodaySummarize(accid);
        }
        /// <summary>
        /// 更新  RandomNumber 主要用户手机橱窗和 优惠券  
        /// <para>如果已经存在 就不在 更新</para>
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="randomNumber"></param>
        /// <returns></returns>
        public static bool UpdateRandomNumber(int accid, string randomNumber)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.UpdateRandomNumber(accid, randomNumber);
        }

        /// <summary>
        /// 获取回访列表
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static List<T_Account> GetVisitList(string Column, string strWhere)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetVisitList(Column, strWhere);
        }

        /// <summary>
        /// 返回店铺搜索分页后的列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="columnName"></param>
        /// <param name="content"></param>
        /// <param name="filedOrder"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        public static List<dynamic> GetSearchList(int pageIndex, int pageSize, List<DapperWhere> dapperWhere,
            string filedOrder,string searchStr = "")
        {
            T_AccountDAL dal = new T_AccountDAL();
            var list = dal.GetSearchList(pageIndex, pageSize, dapperWhere, filedOrder, searchStr);

            foreach (dynamic item in list)
            {
                string remark = item.Remark.ToString();
                switch (remark)
                {
                    case "10":
                        item.Remark = "IOS注册";
                        break;
                    case "8":
                        item.Remark = "PC客户端注册";
                        break;
                    case "9":
                        item.Remark = "SEM注册";
                        break;
                    case "11":
                        item.Remark = "Android注册";
                        break;
                    default:
                        item.Remark = "网站注册";
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// 获取未审核店铺的列表
        /// </summary>
        /// <returns></returns>
        public static List<UncheckedShopModel> GetUncheckedShopList(DateTime regTime)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetUncheckedShopList(regTime);
        }

        /// <summary>
        /// 执行审核操作
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int CheckShop(int accid, int type)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.CheckShop(accid, type);
        }

        /// <summary>
        /// 在审核店铺时删除店铺
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static int DeleteShopDuringChecking(int accid)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.DeleteShopDuringChecking(accid);
        }
        /// <summary>
        /// 得到分店的信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<dynamic> GetSubbranchInfo(int accid)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetSubbranchInfo(accid);
        }
        /// <summary>
        /// 得到总店信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns>{CompanyName,ID}</returns>
        public static dynamic GetHeadquartersInfo(int accid)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetHeadquartersInfo(accid);
        }
        /// <summary>
        /// 得到店铺名称
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static string GetCompanyName(int accid)
        {
            T_AccountDAL dal = new T_AccountDAL();
            Dictionary<int, string> list = dal.GetCompanyName(new int[] { accid });
            if (list != null)
            {
                return list[accid];
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到店铺名称列表
        /// </summary>
        /// <param name="accids"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetCompanyName(int[] accids)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetCompanyName(accids);
        }
        /// <summary>
        /// 注册来源分析
        /// </summary>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<SourceAnalyzeModel> RegSourceAnalyze(DateTime startTime, DateTime endTime, string[] sourceList, string detailMark = "")
        {
            T_AccountDAL dal = new T_AccountDAL();

            endTime = endTime.AddHours(23).AddMinutes(59).AddSeconds(59);
            if (endTime > DateTime.Now.AddDays(1))
            {
                endTime = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
            }
            return dal.RegSourceAnalyze(startTime, endTime, sourceList, detailMark);
        }

        /// <summary>
        /// 获取单个店铺的名称
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static string GetSingleCompanyName(int accId)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetCompanyName(accId);
        }

        public static string GetAccountNumber(int channelId, int accId)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetAccountNumber(channelId, accId);
        }

        public static int GetAccIdBybbsId(int bbsId)
        {
            T_AccountDAL dal = new T_AccountDAL();
            return dal.GetAccIdBybbsId(bbsId);
        }
    }
}
