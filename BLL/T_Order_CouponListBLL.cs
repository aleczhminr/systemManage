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
    /// 优惠券列表
    /// </summary>
    public static class T_Order_CouponListBLL
    {
        /// <summary>
        /// 获取单个店铺的优惠券
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<ShopOrderCoupon> GetCouponByAccId(int accid)
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            var list= dal.GetCouponByAccId(accid);

            foreach (ShopOrderCoupon item in list)
            {
                item.couponStatusName = Enum.GetName(typeof(Model.Enum.CouponEnum.CouponListStatus), item.couponStatus);
                item.couponTypeName = Enum.GetName(typeof(Model.Enum.CouponEnum.CouponType), item.couponType);
            }

            return list;
        }
        /// <summary>
        /// 绑定店铺
        /// <para>{-1:处理错误，0：优惠券不存在，1：优惠券已使用或者已经作废，2：绑定成功}</para>
        /// </summary>
        /// <param name="accountid">店铺ID</param>
        /// <param name="CouponID">优惠券编号</param>
        /// <returns></returns>
        public static int BindingAccount(int accountid, string CouponID)
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            return dal.BindingAccount(accountid, CouponID);
        }
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public static List<OrderCouponListInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            List<OrderCouponListInfo> list = dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].couponStatusName = Enum.GetName(typeof(Model.Enum.CouponEnum.CouponListStatus), list[i].couponStatus);
            }


            return list;
        }
        /// <summary>
        /// 生成优惠券
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="couponCode"></param>
        /// <param name="couponValue"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int CreateCouponGroup(int groupId, string couponCode, int couponValue, DateTime endTime)
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            return dal.CreateCouponGroup(groupId, couponCode, couponValue, endTime);
        }
        
        /// <summary>
        /// 得到所有可用优惠券列表
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetAllCoupon()
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            return dal.GetAllCoupon();
        }
        /// <summary>
        /// 得到 一个店铺的 优惠券汇总
        /// </summary>
        /// <param name="accid"></param>
        /// <returns>{couponNum:总优惠券数,useCouponNum:使用的优惠券数}</returns>
        public static dynamic GetSummarizeByAccId(int accid)
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            return dal.GetSummarizeByAccId(accid);
        }

        /// <summary>
        /// 根据优惠券编号获取优惠券信息
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public static dynamic GetCouponInfoByCode(string couponCode)
        {
            T_Order_CouponListDAL dal = new T_Order_CouponListDAL();
            return dal.GetCouponInfoByCode(couponCode);
        }
    }
}
