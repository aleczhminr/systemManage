using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    /// <summary>
    /// 优惠券详情列表
    /// </summary>
    public class T_Order_CouponListDAL : Base.T_Order_CouponListBaseDAL
    {
        /// <summary>
        /// 获取单个店铺的优惠券
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<ShopOrderCoupon> GetCouponByAccId(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select id,groupId,couponValue,couponStatus,endDate,createDate,usedDate into #list from i200.dbo.T_Order_CouponList where toAccId=@accid order by createDate desc; ");
            strSql.Append(" select #list.id,#list.groupId,#list.endDate,#list.createDate,#list.usedDate,couponType,couponDesc,couponValue,couponStatus from #list inner join ( ");
            strSql.Append(" select id,couponType,couponDesc from i200.dbo.T_Order_CouponInfo where id in(select groupId from #list) ) a on a.id=#list.groupId; ");
            strSql.Append(" drop table #list; ");
            return DapperHelper.Query<ShopOrderCoupon>(strSql.ToString(), new
            {
                accid
                    = accid
            }).ToList();
        }
        /// <summary>
        /// 绑定店铺
        /// <para>{-1:处理错误，0：优惠券不存在，1：优惠券已使用或者已经作废，2：绑定成功}</para>
        /// </summary>
        /// <param name="accountid">店铺ID</param>
        /// <param name="CouponID">优惠券编号</param>
        /// <returns></returns>
        public int BindingAccount(int accountid, string CouponID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare @stat int; ");

            strSql.Append(" select @stat=couponStatus from T_Order_CouponList  where couponId=@CouponID; ");
            strSql.Append(" if(@stat is null) ");
            strSql.Append(" begin ");
            strSql.Append(" 	select 0; ");
            strSql.Append(" end ");
            strSql.Append(" else if(@stat=0) ");
            strSql.Append(" begin ");
            strSql.Append(" 	update T_Order_CouponList set couponStatus=2,toAccId=@account,receiveDate=getdate(),bindWay=1 where couponId=@CouponID; ");
            strSql.Append(" 	select 2; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" 	select 1; ");

            object rId = HelperForFrontend.ExecuteScalar(strSql.ToString(), new { account = accountid, CouponID = CouponID });
            if (rId != null)
            {
                return Convert.ToInt32(rId);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public new List<OrderCouponListInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }
            strSql.Append(" SELECT id,couponId,couponStatus,endDate,receiveDate,usedDate,toAccId,useAccId,remark into #list FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, * from I200.dbo.T_Order_CouponList T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.Row between @startIndex and @endIndex; ");

            strSql.Append("select #list.id,couponId,couponStatus,endDate,receiveDate,usedDate,toAccId,a.CompanyName toAccName,useAccId,remark from #list left outer join(");
            strSql.Append("select ID,CompanyName from i200.dbo.T_Account where ID in(select toAccId from #list)) a on a.ID=#list.toAccId;");
            strSql.Append("drop table #list;");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<OrderCouponListInfo>(strSql.ToString(), parm).ToList();
        }


        /// <summary>
        /// 得到所有可用优惠券列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetAllCoupon()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT id, couponId FROM i200.dbo.T_Order_CouponList");
            strSql.Append(" where couponStatus in (0,2)");

            return DapperHelper.Query(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 生成优惠券
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="couponCode"></param>
        /// <param name="couponValue"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int CreateCouponGroup(int groupId, string couponCode, int couponValue, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.Append("INSERT INTO T_Order_CouponList(groupId, couponId, couponValue, couponStatus, createDate, endDate, toAccId)");
            strSql.Append(" VALUES (@couponInfoId,@couponCode,@couponValue,0,GETDATE(),@endDate,0)");

            int rt = HelperForFrontend.Execute(strSql.ToString(), new { couponInfoId = groupId, couponCode = couponCode, couponValue = couponValue, endDate = endTime });
            return rt;
        }

        /// <summary>
        /// 得到 一个店铺的 优惠券汇总
        /// </summary>
        /// <param name="accid"></param>
        /// <returns>{couponNum:总优惠券数,useCouponNum:使用的优惠券数}</returns>
        public dynamic GetSummarizeByAccId(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(id) couponNum,sum(case when useAccId >0 then 1 else 0 end) useCouponNum from i200.dbo.T_Order_CouponList where toAccId=@accid;");
            return DapperHelper.GetModel<dynamic>(strSql.ToString(), new { accid = accid });
        }

        /// <summary>
        /// 根据优惠券编号获取优惠券信息
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public dynamic GetCouponInfoByCode(string couponCode)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select ci.couponDesc,ci.endDate from i200.dbo.T_Order_CouponInfo ci " +
                          "left join [i200].[dbo].[T_Order_CouponList] cl " +
                          "on ci.id=cl.groupId " +
                          "where cl.couponId=@couponCode;");

            try
            {
                return DapperHelper.GetModel<dynamic>(strSql.ToString(), new { couponCode = couponCode });
            }
            catch (Exception ex)
            {
                Logger.Error("获取优惠券信息出错！", ex);
                return null;
            }
            
        }
    }
}
