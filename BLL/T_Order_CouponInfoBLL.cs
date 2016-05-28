using DAL;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public static class T_Order_CouponInfoBLL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(T_Order_CouponInfo model)
        {
            T_Order_CouponInfoDAL dal = new T_Order_CouponInfoDAL();
            return dal.Add(model);

        }

        /// <summary>
        /// 得到优惠券列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <returns></returns>
        public static List<OrderCouponInfoListItem> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder="")
        {
            T_Order_CouponInfoDAL dal = new T_Order_CouponInfoDAL();
            List<OrderCouponInfoListItem> iteList = dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);

            for (int i = 0; i < iteList.Count; i++)
            {
                iteList[i].couponStatusName = Enum.GetName(typeof(Model.Enum.CouponEnum.CouponStatus), iteList[i].couponStatus);
            }

            return iteList;
        }
        /// <summary>
        /// 优惠券内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OrderCouponInfo GetModel(int id)
        {
            T_Order_CouponInfoDAL dal = new T_Order_CouponInfoDAL();
            OrderCouponInfo infoModel = dal.GetModel(id);

            if (infoModel.operatorId > 0)
            {
                infoModel.operatorName = Sys_Manage_UserBLL.GetManageUserNameById(infoModel.operatorId);
            }
            else
            {
                infoModel.operatorName = "系统";
            }

            infoModel.couponTypeName = Enum.GetName(typeof(Model.Enum.CouponEnum.CouponType), infoModel.couponType);

            infoModel.bindTypeName = Enum.GetName(typeof(Model.Enum.CouponEnum.BindType), infoModel.bindType);
            if (infoModel.bindType == 0)
            {
                infoModel.bindName = "无限制";
            }
            else if (infoModel.bindType == 1)
            {
                infoModel.bindName = Enum.GetName(typeof(Model.Enum.OrderEnum.ItemType), infoModel.bindValue);
            }
            else if (infoModel.bindType == 2)
            {
                infoModel.bindName = BLL.Base.T_Order_ProjectBaseBLL.GetModel(infoModel.bindValue).displayName;
            }
            infoModel.ruleTypeName = Enum.GetName(typeof(Model.Enum.CouponEnum.RuleType), infoModel.ruleType);
            infoModel.couponStatusName = Enum.GetName(typeof(Model.Enum.CouponEnum.CouponStatus), infoModel.couponStatus);



            return infoModel;
        }

    }
}
