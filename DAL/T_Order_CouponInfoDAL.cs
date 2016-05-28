using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 优惠券信息
    /// </summary>
    public class T_Order_CouponInfoDAL : Base.T_Order_CouponInfoBaseDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_Order_CouponInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into i200.dbo.T_Order_CouponInfo(");
            strSql.Append("couponDesc,maxLimitNum,createDate,endDate,operatorId,operarorIp,operatorTime,remark,couponType,bindType,bindValue,bindName,ruleType,ruleValue,couponValue,couponStatus,prefixAgent");
            strSql.Append(") values (");
            strSql.Append("@couponDesc,@maxLimitNum,@createDate,@endDate,@operatorId,@operarorIp,@operatorTime,@remark,@couponType,@bindType,@bindValue,@bindName,@ruleType,@ruleValue,@couponValue,@couponStatus,@prefixAgent");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");

            object obj =HelperForFrontend.ExecuteScalar<int>(strSql.ToString(), new
            {
                couponDesc = model.couponDesc,
                maxLimitNum = model.maxLimitNum,
                createDate = model.createDate,
                endDate = model.endDate,
                operatorId = model.operatorId,
                operarorIp = model.operarorIp,
                operatorTime = model.operatorTime,
                remark = model.remark,
                couponType = model.couponType,
                bindType = model.bindType,
                bindValue = model.bindValue,
                bindName = model.bindName,
                ruleType = model.ruleType,
                ruleValue = model.ruleValue,
                couponValue = model.couponValue,
                couponStatus = model.couponStatus,
                prefixAgent = model.prefixAgent
            });

            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }

        /// <summary>
        /// 得到优惠券列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <returns></returns>
        public new List<OrderCouponInfoListItem> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder="")
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
            if (filedOrder == "")
            {
                filedOrder = " id desc";
            }

            strSql.Append(" SELECT id,couponDesc,maxLimitNum,createDate,endDate,operatorId,remark,couponStatus into #list FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, * from I200.dbo.T_Order_CouponInfo T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.Row between @startIndex and @endIndex; ");

            strSql.Append(" select #list.id,couponDesc,maxLimitNum,createDate,endDate,operatorId,isnull(b.name,'系统') opertorName,  ");
            strSql.Append(" remark,couponStatus,isnull(produceNum,0)produceNum,isnull(bindingNum,0)bindingNum,isnull(useNum,0) useNum   ");
            strSql.Append(" from #list left outer join(  ");
            strSql.Append(" select groupId,COUNT(id) produceNum,sum(case when toAccId>0 then 1 else 0 end) bindingNum,SUM(case when useAccId>0 then 1 else 0 end) useNum   ");
            strSql.Append(" from i200.dbo.T_Order_CouponList where groupId in(select id from #list) group by groupId  ");
            strSql.Append(" ) a on a.groupId=#list.id left outer join(  ");
            strSql.Append(" select Id,name from Sys_Manage_User   ");
            strSql.Append(" where Id in(select operatorId from #list)) b on b.Id=#list.operatorId  ");
            strSql.Append(" drop table #list  ");


            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<OrderCouponInfoListItem>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 优惠券内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new OrderCouponInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" declare @produceNum int,@bindingNum int,@useNum int; ");
            strSql.Append(" select @produceNum= COUNT(id),@bindingNum= sum(case when toAccId>0 then 1 else 0 end),@useNum=SUM(case when useAccId>0 then 1 else 0 end)  ");
            strSql.Append("  from i200.dbo.T_Order_CouponList where groupId=@id ; ");
            strSql.Append(" select id,couponType,bindType,bindValue,ruleType,ruleValue,couponStatus,couponValue,couponDesc,maxLimitNum,createDate,endDate,operatorId,remark, ");
            strSql.Append(" @produceNum produceNum,@bindingNum bindingNum,@useNum useNum from i200.dbo.T_Order_CouponInfo where id=@id order by id desc; ");
            OrderCouponInfo infoModel = DapperHelper.GetModel<OrderCouponInfo>(strSql.ToString(), new { id = id });
            return infoModel;
        }

    }
}
