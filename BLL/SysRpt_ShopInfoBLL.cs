using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace BLL
{
    /// <summary>
    /// 后台店铺汇总信息
    /// </summary>
    public static class SysRpt_ShopInfoBLL
    {
        /// <summary>
        /// 得到店铺的操作汇总信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountSummarize.Summarize GetAccountSummarize(int accid)
        {
            SysRpt_ShopInfoDAL dal = new SysRpt_ShopInfoDAL();
            T_AccountSummarize.Summarize summarize = dal.GetAccountSummarize(accid);
            if (summarize != null)
            {
                dynamic couponSummarize = T_Order_CouponListBLL.GetSummarizeByAccId(accid);
                summarize.couponNum = Convert.ToInt32(couponSummarize.couponNum);
                summarize.useCouponNum = Convert.ToInt32(couponSummarize.useCouponNum);
            }
            else
            {
                summarize = new T_AccountSummarize.Summarize();
            }
            return summarize;
        }
        /// <summary>
        /// 数据筛选专用
        /// </summary>
        /// <returns></returns>
        public static FiltrateData.AllMax GetAllMax()
        {
            SysRpt_ShopInfoDAL dal = new SysRpt_ShopInfoDAL();
            return dal.GetAllMax();
        }

        public static List<LogClientDic> GetLogClient(int accId)
        {
            SysRpt_ShopInfoDAL dal = new SysRpt_ShopInfoDAL();
            return dal.GetLogClient(accId);
        }

        public static string GetLatestLogClient(int accId)
        {
            SysRpt_ShopInfoDAL dal = new SysRpt_ShopInfoDAL();
            return dal.GetLatestLogClient(accId);
        }
    }
}
