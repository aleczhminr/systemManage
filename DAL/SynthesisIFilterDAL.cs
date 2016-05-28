using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DAL
{
    /// <summary>
    /// 数据筛选器
    /// </summary>
    public class SynthesisIFilterDAL
    {
        /// <summary>
        /// 得到筛选数据列表
        /// </summary>
        /// <param name="topNum">前面几行</param>
        /// <param name="whereModel">条件信息</param>
        /// <param name="orderWhere">排序</param>
        /// <param name="userId">查询人ID</param>
        /// <param name="verification">验证</param>
        /// <param name="userName">筛选人名称</param>
        /// <returns></returns>
        public Dictionary<string, object> GetIFilterList(int topNum, List<SynthesisIFilter> whereModels, string orderWhere, int userId, string verification, string userName)
        {
            StringBuilder whereSql = new StringBuilder();
            foreach (SynthesisIFilter itemModel in whereModels)
            {
                whereSql.Append(GetSqlWhereByModel(itemModel));
            }


            StringBuilder strSql = new StringBuilder();




            strSql.Append(" declare @list varchar(max),@count int,@logId int; ");

            strSql.Append(" insert into I200_Log.dbo.SynthesisIFilterLog(strSql,userid ");
            strSql.Append(" ,verification,inserName,inserTime) ");
            strSql.Append(" values(@strSql,@userid,@verifi,@inserName,GETDATE());");
            strSql.Append(" select @logId=@@IDENTITY;");

            strSql.Append("create table #list(accid int); ");
            strSql.Append(" insert into #list(accid) ");
            strSql.Append(" select distinct accountid from SysRpt_ShopInfo where taskStat>-10 ");
            if (whereSql.ToString().Length > 0)
            {
                strSql.Append(" " + whereSql.ToString() + " ");
            }
            strSql.Append("; ");


            strSql.Append(" set @list='0'; ");
            strSql.Append(" select @count=COUNT(accid) from #list; ");
            strSql.Append(" select @list= Isnull(@list,'0')+','+ltrim(accid) from #list ; ");

            strSql.Append(" update I200_Log.dbo.SynthesisIFilterLog set resultCount=@count,resultData=@list where id=@logId; ");

            strSql.Append(" select a.ID,a.CompanyName ,a.UserRealName ,f.PhoneNumber,f.UserEmail,a.RegTime,  ");
            strSql.Append(" case when b.aotjb=3 then '高级' when b.aotjb=2 then '标准' else '免费' end aotjb  ");
            strSql.Append(" ,b.endtime aotjbEndtime,c.userNum,c.goodsNum,c.saleNum,c.smsNum,b.dxunity,c.outlayNum,e.insertName returnInsertTime ,  ");
            strSql.Append(" d.allCount ,d.userCount,a.LoginTimeWeb,LoginTimeLast,  ");
            strSql.Append(" case when c.active=1 then '新注册' when c.active=3 then '需关怀' when c.active=5 then '活跃'   ");
            strSql.Append(" when c.active=7 then '忠诚' when c.active=-1 then '休眠' when c.active=-3 then '流失' else '新注册' end active,  ");
            strSql.Append(" c.orderMoney ,g.AgentName from i200.dbo.T_Account a left outer join i200.dbo.T_Business b on a.ID=b.accountid   ");
            strSql.Append(" left outer join SysRpt_ShopInfo c on a.ID=c.accountid left outer join (  ");
            strSql.Append(" select toAccId,COUNT(id) allCount,sum(case when useAccId IS null then 0 else 1 end) userCount  from i200.dbo.T_Order_CouponList   ");
            strSql.Append(" where toAccId in(select accid from #list) group by toAccId) d on a.ID=d.toAccId  ");
            strSql.Append(" left outer join(  ");
            strSql.Append(" select a.accid,a.insertName from Sys_VisitInfo a inner join(  ");
            strSql.Append(" select accid,MAX(insertTime) it from Sys_VisitInfo   where    ");
            strSql.Append(" accid in(select accid from #list)  group by accid ) b on a.insertTime=b.it and a.accid=b.accid) e on a.ID=e.accid  ");
            strSql.Append(" left outer join (  ");
            strSql.Append(" select accountid,PhoneNumber,UserEmail from i200.dbo.T_Account_User where grade='管理员') f on a.ID=f.accountid  left outer join Sys_agent_mess g on a.AgentId=g.ID ");
            strSql.Append(" where a.ID in( ");

            if (topNum > 0)
            {

                strSql.Append(" select top " + topNum + " accid from #list ");
            }
            else
            {
                strSql.Append(" select accid from #list ");
            }
            strSql.Append(" ) ; ");
            strSql.Append(" drop table #list  ");

            var selectList = DapperHelper.Query(strSql.ToString(), new
            {
                strSql = strSql.ToString(),
                userid = userId,
                verifi = verification,
                inserName = userName
            }).ToList();


            Dictionary<string, object> list = new Dictionary<string, object>();
            if (selectList != null && selectList.Count > 0)
            {
                list["data"] = selectList;

                list["count"] = SelectLogAboutCount(userId, verification);
                list["verifi"] = verification;
            }
            return list;
        }

        /// <summary>
        /// 直接传入规则生成的Where条件的筛选器版本
        /// </summary>
        /// <param name="topNum"></param>
        /// <param name="where"></param>
        /// <param name="orderWhere"></param>
        /// <param name="userId"></param>
        /// <param name="verification"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetFilterDataByRule(int topNum, string where, string orderWhere, int userId, string verification, string userName)
        {
            StringBuilder whereSql = new StringBuilder();

            whereSql.Append(where);

            StringBuilder strSql = new StringBuilder();




            strSql.Append(" declare @list varchar(max),@count int,@logId int; ");

            strSql.Append(" insert into I200_Log.dbo.SynthesisIFilterLog(strSql,userid ");
            strSql.Append(" ,verification,inserName,inserTime) ");
            strSql.Append(" values(@strSql,@userid,@verifi,@inserName,GETDATE());");
            strSql.Append(" select @logId=@@IDENTITY;");

            strSql.Append("create table #list(accid int); ");
            strSql.Append(" insert into #list(accid) ");
            strSql.Append(" select distinct accountid from SysRpt_ShopInfo where taskStat>-10 ");//限制在200个用户
            if (whereSql.ToString().Length > 0)
            {
                strSql.Append(" " + whereSql.ToString() + " ");
            }
            strSql.Append("; ");


            strSql.Append(" set @list='0'; ");
            strSql.Append(" select @count=COUNT(accid) from #list; ");
            strSql.Append(" select @list= Isnull(@list,'0')+','+ltrim(accid) from #list ; ");

            strSql.Append(" update I200_Log.dbo.SynthesisIFilterLog set resultCount=@count,resultData=@list where id=@logId; ");

            strSql.Append(" select a.ID,a.CompanyName ,a.UserRealName ,f.PhoneNumber,f.UserEmail,a.RegTime,  ");
            strSql.Append(" case when b.aotjb=3 then '高级' when b.aotjb=2 then '标准' else '免费' end aotjb  ");
            strSql.Append(" ,b.endtime aotjbEndtime,c.userNum,c.goodsNum,c.saleNum,c.smsNum,b.dxunity,c.outlayNum,e.insertName returnInsertTime ,  ");
            strSql.Append(" d.allCount ,d.userCount,a.LoginTimeWeb,LoginTimeLast,  ");
            strSql.Append(" case when c.active=1 then '新注册' when c.active=3 then '需关怀' when c.active=5 then '活跃'   ");
            strSql.Append(" when c.active=7 then '忠诚' when c.active=-1 then '休眠' when c.active=-3 then '流失' else '新注册' end active,  ");
            strSql.Append(" c.orderMoney ,g.AgentName from i200.dbo.T_Account a left outer join i200.dbo.T_Business b on a.ID=b.accountid   ");
            strSql.Append(" left outer join SysRpt_ShopInfo c on a.ID=c.accountid left outer join (  ");
            strSql.Append(" select toAccId,COUNT(id) allCount,sum(case when useAccId IS null then 0 else 1 end) userCount  from i200.dbo.T_Order_CouponList   ");
            strSql.Append(" where toAccId in(select accid from #list) group by toAccId) d on a.ID=d.toAccId  ");
            strSql.Append(" left outer join(  ");
            strSql.Append(" select a.accid,a.insertName from Sys_VisitInfo a inner join(  ");
            strSql.Append(" select accid,MAX(insertTime) it from Sys_VisitInfo   where    ");
            strSql.Append(" accid in(select accid from #list)  group by accid ) b on a.insertTime=b.it and a.accid=b.accid) e on a.ID=e.accid  ");
            strSql.Append(" left outer join (  ");
            strSql.Append(" select accountid,PhoneNumber,UserEmail from i200.dbo.T_Account_User where grade='管理员') f on a.ID=f.accountid  left outer join Sys_agent_mess g on a.AgentId=g.ID ");
            strSql.Append(" where a.ID in( ");

            if (topNum > 0)
            {

                strSql.Append(" select top " + topNum + " accid from #list ");
            }
            else
            {
                strSql.Append(" select accid from #list ");
            }
            strSql.Append(" ) ; ");
            strSql.Append(" drop table #list  ");

            var selectList = DapperHelper.Query(strSql.ToString(), new
            {
                strSql = strSql.ToString(),
                userid = userId,
                verifi = verification,
                inserName = userName
            }).ToList();

            strSql.Clear();

            strSql.Append(" select count(distinct accountid) from SysRpt_ShopInfo where taskStat>-10 ");//限制在200个用户
            if (whereSql.ToString().Length > 0)
            {
                strSql.Append(" " + whereSql.ToString() + " ");
            }
            strSql.Append("; ");
            int count = 0;

            try
            {
                count = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
                {
                    strSql = strSql.ToString(),
                    userid = userId,
                    verifi = verification,
                    inserName = userName
                });
            }
            catch (Exception ex)
            {
                Logger.Error("获取筛选器数据出错！", ex);
                count = 0;
            }

            Dictionary<string, object> list = new Dictionary<string, object>();
            if (selectList != null && selectList.Count > 0)
            {
                list["data"] = selectList;
                list["count"] = count;
                //list["count"] = SelectLogAboutCount(userId, verification);
                list["verifi"] = verification;
            }
            return list;
        }

        public int SelectLogAboutCount(int uid, string verification)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 resultCount from I200_Log.dbo.SynthesisIFilterLog where userid=@userid and verification=@vaer order by id desc");
            object r = DapperHelper.ExecuteScalar(strSql.ToString(), new
            {
                userid = uid,
                vaer = verification
            });
            if (r != null)
            {
                return Convert.ToInt32(r);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="whereModel">条件</param>
        /// <param name="userId">查询人ID</param>
        /// <param name="verification">查询验证</param>
        /// <param name="userName">查询人名称</param>
        /// <returns></returns>
        public int SelectIfilterList(List<SynthesisIFilter> whereModels, int userId, string verification, string userName)
        {

            List<DapperWhere> parmsList = new List<DapperWhere>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("create table #list(accid int); ");


            strSql.Append(" insert into #list(accid) ");
            strSql.Append(" select distinct accountid from SysRpt_ShopInfo where taskStat>-10 ");

            foreach (SynthesisIFilter itemModel in whereModels)
            {
                strSql.Append(GetSqlWhereByModel(itemModel));
            }

            strSql.Append("; ");












            strSql.Append(" declare @list varchar(max),@count int; ");
            strSql.Append(" set @list='0'; ");
            strSql.Append(" select @count=COUNT(accid) from #list; ");
            strSql.Append(" select @list= Isnull(@list,'0')+','+ltrim(accid) from #list ; ");
            strSql.Append(" insert into SynthesisIFilterLog(strSql,resultData, ");
            strSql.Append(" resultCount,userid,verification,inserName,inserTime) ");
            strSql.Append(" values(@strSql,@list,@count,@userid,@verifi,@inserName,GETDATE());");
            strSql.Append(";select @@IDENTITY;");

            strSql.Append(" drop table #list ; ");


            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), new
            {
                strSql = strSql.ToString(),
                userid = userId,
                verifi = verification,
                inserName = userName
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
        /// 根据条件 得到 SQL 内容
        /// </summary>
        /// <param name="whereModel"></param>
        /// <returns></returns>
        private StringBuilder GetSqlWhereByModel(SynthesisIFilter whereModel)
        {

            SynthesisIfilterItemInt sItemIntInfo = new SynthesisIfilterItemInt();
            SynthesisIfilterItemString sItemStringInfo = new SynthesisIfilterItemString();
            StringBuilder whereSql = new StringBuilder();


            StringBuilder T_OrderInfo = new StringBuilder();
            StringBuilder Sys_TagNexus = new StringBuilder();
            StringBuilder T_Account = new StringBuilder();
            StringBuilder T_Log = new StringBuilder();
            StringBuilder T_Business = new StringBuilder();

            string itemWhere = "";


            #region 店铺信息

            #region 注册时间
            if (whereModel.RegTimeByTime != null)
            {
                sItemStringInfo = whereModel.RegTimeByTime;
                if (sItemStringInfo.max != null && sItemStringInfo.max != "")
                {
                    T_Account.Append("and RegTime<='" + sItemStringInfo.max + "  23:59:59' ");
                }
                if (sItemStringInfo.min != null && sItemStringInfo.min != "")
                {
                    T_Account.Append("and RegTime>='" + sItemStringInfo.min + "' ");
                }


            }
            #endregion

            #region 最后登录时间
            if (whereModel.finallyLoginTime != null)
            {
                sItemStringInfo = whereModel.finallyLoginTime;
                itemWhere = "";

                if (sItemStringInfo.max != null && sItemStringInfo.max != "")
                {
                    T_Account.Append("and LoginTimeLast<='" + sItemStringInfo.max + " 23:59:59' ");
                }
                if (sItemStringInfo.min != null && sItemStringInfo.min != "")
                {
                    T_Account.Append("and LoginTimeLast>='" + sItemStringInfo.min + "' ");
                }
            }
            #endregion
            #region 登录次数
            if (whereModel.loginTimes != null)
            {
                sItemIntInfo = whereModel.loginTimes;
                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    T_Account.Append("and LoginTimeWeb<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    T_Account.Append("and LoginTimeWeb>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion

            #endregion
            #region 店铺基本信息  T_Business
            #region 剩余短信数
            if (whereModel.smsResidue != null)
            {
                sItemIntInfo = whereModel.smsResidue;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    T_Business.Append("dxunity<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    T_Business.Append((T_Business.Length > 0 ? " and " : "") + " dxunity>=" + sItemIntInfo.min + " ");
                }
            }

            #endregion
            #region 店铺状态
            if (whereModel.AccountActive != null)
            {
                string activeStr = "";
                if (whereModel.AccountActive.Length > 0)
                {
                    foreach (int active in whereModel.AccountActive)
                    {
                        activeStr += "," + active.ToString();
                    }
                }
                if (activeStr.Length > 0)
                {
                    T_Business.Append((T_Business.Length > 0 ? " and " : "") + " active in(" + activeStr.Trim(',') + ") ");
                }
            }
            #endregion

            #endregion

            #region 订单信息
            #region 按订单购买时间
            if (whereModel.BuyOrderByTime != null)
            {
                if (whereModel.BuyOrderByTime.min != null && whereModel.BuyOrderByTime.min != "")
                {
                    T_OrderInfo.Append("and transactionDate>='" + whereModel.BuyOrderByTime.min + "' ");
                }
                if (whereModel.BuyOrderByTime.max != null && whereModel.BuyOrderByTime.max != "")
                {
                    T_OrderInfo.Append("and transactionDate<='" + whereModel.BuyOrderByTime.max + " 23:59:59' ");
                }
            }
            #endregion
            #region 购买产品
            if (whereModel.OrderProject != null && whereModel.OrderProject.Length > 0)
            {
                string OPStr = "";
                foreach (int opid in whereModel.OrderProject)
                {
                    OPStr += "," + opid.ToString();
                }
                if (OPStr.Length > 0)
                {
                    T_OrderInfo.Append("and busId in(" + OPStr.Trim(',') + ") ");
                }
            }
            #endregion


            #endregion

            #region 代理商信息
            if (whereModel.SearchByAgent != null && whereModel.SearchByAgent.Length > 0)
            {
                string OPStr = "";
                foreach (int opid in whereModel.SearchByAgent)
                {
                    OPStr += "," + opid.ToString();
                }
                if (OPStr.Length > 0)
                {
                    T_Account.Append("and agentId in(" + OPStr.Trim(',') + ") ");
                }
            }
            #endregion


            #region 标签信息
            #region 标签信息
            if (whereModel.TagInfo != null && whereModel.TagInfo.Length > 0)
            {
                string TagList = "";
                foreach (int tid in whereModel.TagInfo)
                {
                    TagList += "," + tid.ToString();
                }
                if (TagList.Length > 0)
                {
                    Sys_TagNexus.Append(" tag_id in(" + TagList.Trim(',') + ") ");
                }

            }

            #endregion
            #endregion

            #region 登录日志
            #region 某段时间登录过
            if (whereModel.WhileLoggedOn != null)
            {
                if (whereModel.WhileLoggedOn.min != null && whereModel.WhileLoggedOn.min != "")
                {
                    T_Log.Append(" OperDate>='" + whereModel.WhileLoggedOn.min + "' ");
                }
                if (whereModel.WhileLoggedOn.max != null && whereModel.WhileLoggedOn.max != "")
                {
                    T_Log.Append((T_Log.Length > 0 ? " and " : "") + " OperDate<='" + whereModel.WhileLoggedOn.max + " 23:59:59' ");
                }
            }
            #endregion

            #endregion

            #region 默认信息
            #region 短信数小于会员数
            if (whereModel.SmsLesserThanUser != null)
            {
                whereSql.Append("and userNum>allSmsNum ");
            }
            #endregion
            #region 会员数量
            if (whereModel.userNumber != null)
            {
                sItemIntInfo = whereModel.userNumber;
                if (sItemIntInfo.max != null && sItemIntInfo.max >= 0)
                {
                    whereSql.Append("and userNum<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and userNum>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 销售数量
            if (whereModel.saleNumber != null)
            {
                sItemIntInfo = whereModel.saleNumber;
                if (sItemIntInfo.max != null && sItemIntInfo.max >= 0)
                {
                    whereSql.Append("and saleNum<=" + sItemIntInfo.max + " ");
                }

                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and saleNum>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 连续未登录天数

            if (whereModel.LastLoginTime != null)
            {
                //lastLoginTime
                sItemIntInfo = whereModel.LastLoginTime;
                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and loginType=2 and ContinuousDay<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and loginType=2 and ContinuousDay>=" + sItemIntInfo.min + " ");
                }
            }

            #endregion
            #region 订单金额
            if (whereModel.orderMoney != null)
            {
                sItemIntInfo = whereModel.orderMoney;
                if (sItemIntInfo.max != null && sItemIntInfo.max >= 0)
                {
                    whereSql.Append("and orderMoney<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and orderMoney>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 发送短信数量

            if (whereModel.smsNumber != null)
            {
                sItemIntInfo = whereModel.smsNumber;
                if (sItemIntInfo.max != null && sItemIntInfo.max >= 0)
                {
                    whereSql.Append("and smsNum<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and smsNum>=" + sItemIntInfo.min + " ");
                }
            }

            #endregion
            #region 商品数量
            if (whereModel.goodsNumber != null)
            {
                sItemIntInfo = whereModel.goodsNumber;
                if (sItemIntInfo.max != null && sItemIntInfo.max >= 0)
                {
                    whereSql.Append("and goodsNum<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and goodsNum>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 支出数量
            if (whereModel.outlayNumber != null)
            {
                sItemIntInfo = whereModel.outlayNumber;
                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and outlayNum<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and outlayNum>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 使用代金券
            if (whereModel.useVoucher != null)
            {
                sItemIntInfo = whereModel.useVoucher;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and useVoucher<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and useVoucher>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 总积分
            if (whereModel.accAllIntegral != null)
            {
                sItemIntInfo = whereModel.accAllIntegral;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and (isnull(useIntegral,0) + isnull(integral,0))<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and (isnull(useIntegral,0) + isnull(integral,0))>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 店铺积分
            if (whereModel.accIntegral != null)
            {
                sItemIntInfo = whereModel.accIntegral;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and integral<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and integral>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 店铺已使用积分
            if (whereModel.accUseIntegral != null)
            {
                sItemIntInfo = whereModel.accUseIntegral;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and useIntegral<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and useIntegral>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 店员数
            if (whereModel.shopAssistant != null)
            {
                sItemIntInfo = whereModel.shopAssistant;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and shopAssistant<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and shopAssistant>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 分店数
            if (whereModel.subbranch != null)
            {
                sItemIntInfo = whereModel.subbranch;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and subbranch<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and subbranch>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 客服次数
            if (whereModel.serviceNumber != null)
            {
                sItemIntInfo = whereModel.serviceNumber;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and serviceNumber<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and serviceNumber>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 连续登录天数
            if (whereModel.continuousLogin != null)
            {
                sItemIntInfo = whereModel.continuousLogin;

                if (sItemIntInfo.max != null && sItemIntInfo.max > 0)
                {
                    whereSql.Append("and loginType=1 and ContinuousDay<=" + sItemIntInfo.max + " ");
                }
                if (sItemIntInfo.min != null && sItemIntInfo.min > 0)
                {
                    whereSql.Append("and loginType=1 and ContinuousDay>=" + sItemIntInfo.min + " ");
                }
            }
            #endregion
            #region 排除以下店铺
            if (whereModel.excludeAccount != null)
            {
                if (whereModel.excludeAccount.Length > 0)
                {
                    string accidList = "";
                    foreach (int accid in whereModel.excludeAccount)
                    {
                        accidList += "," + accid;
                    }
                    if (accidList.Length > 0)
                    {
                        whereSql.Append("and accountid not in(" + accidList.Trim(',') + ") ");
                    }
                }
            }
            #endregion


            #region 只查询以下店铺
            if (whereModel.soleAccount != null)
            {
                if (whereModel.soleAccount.Length > 0)
                {
                    string accidList = "";
                    foreach (int accid in whereModel.soleAccount)
                    {
                        accidList += "," + accid;
                    }
                    if (accidList.Length > 0)
                    {
                        whereSql.Append("and accountid in(" + accidList.Trim(',') + ") ");
                    }
                }
            }
            #endregion


            #endregion















            #region 当前首次活跃
            if (whereModel.MonthIndexActive != null && whereModel.MonthIndexActive != "")
            {
                whereSql.Append("and accountid in (select DISTINCT accid from SysRpt_ShopActive where id in(select firstID from SysRpt_ShopActive where active>3 and stateVal=0) and maxactive<5) ");
            }
            #endregion

            #region 当前活跃流失
            if (whereModel.MonthActiveDrain != null && whereModel.MonthActiveDrain != "")
            {
                whereSql.Append("and accountid in (select DISTINCT accid from SysRpt_ShopActive where active=-3 and maxactive>3 and stateVal=0) ");
            }
            #endregion



            if (T_OrderInfo.Length > 0)
            {
                itemWhere = "";

                itemWhere = "and accountid in(select accId from I200.dbo.T_OrderInfo where orderStatus=2 " + T_OrderInfo.ToString() + ") ";

                whereSql.Append(itemWhere);
            }

            if (Sys_TagNexus.Length > 0)
            {
                itemWhere = "";
                itemWhere = "and accountid in(select acc_id from Sys_I200.dbo.Sys_TagNexus where " + Sys_TagNexus.ToString() + ") ";
                whereSql.Append(itemWhere);
            }
            if (T_Account.Length > 0)
            {
                itemWhere = "";
                itemWhere = "and accountid in(select ID from i200.dbo.T_Account where State=1 " + T_Account.ToString() + ") ";
                whereSql.Append(itemWhere);
            }

            if (T_Log.Length > 0)
            {
                itemWhere = "";
                itemWhere = "and accountid in(select Accountid from i200.dbo.T_LOG where " + T_Log.ToString() + ") ";
                whereSql.Append(itemWhere);
            }

            if (T_Business.Length > 0)
            {
                itemWhere = "";
                itemWhere = "and accountid in(select accountid from i200.dbo.T_Business where " + T_Business.ToString() + ") ";
                whereSql.Append(itemWhere);
            }























            return whereSql;
        }


        /// <summary>
        /// 得到分析数据
        /// </summary>
        /// <param name="AccountList"></param>
        /// <returns></returns>
        public List<dynamic> GetSummarizingData(string AccountList)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("create table #list(accid int); ");
            strSql.Append(" insert into #list(accid) ");
            strSql.Append(" select distinct id from I200.dbo.T_Account where id in( ");
            strSql.Append(" " + AccountList + " ");
            strSql.Append(" ); ");

            strSql.Append(" select a.ID,a.CompanyName ,a.UserRealName ,f.PhoneNumber,f.UserEmail,a.RegTime,  ");
            strSql.Append(" case when b.aotjb=3 then '高级' when b.aotjb=2 then '标准' else '免费' end aotjb  ");
            strSql.Append(" ,b.endtime aotjbEndtime,c.userNum,c.goodsNum,c.saleNum,c.smsNum,b.dxunity,c.outlayNum,e.insertName returnInsertTime ,  ");
            strSql.Append(" d.allCount ,d.userCount,a.LoginTimeWeb,LoginTimeLast,  ");
            strSql.Append(" case when c.active=1 then '新注册' when c.active=3 then '需关怀' when c.active=5 then '活跃'   ");
            strSql.Append(" when c.active=7 then '忠诚' when c.active=-1 then '休眠' when c.active=-3 then '流失' else '新注册' end active,  ");
            strSql.Append(" c.orderMoney ,g.AgentName from i200.dbo.T_Account a left outer join i200.dbo.T_Business b on a.ID=b.accountid   ");
            strSql.Append(" left outer join SysRpt_ShopInfo c on a.ID=c.accountid left outer join (  ");
            strSql.Append(" select toAccId,COUNT(id) allCount,sum(case when useAccId IS null then 0 else 1 end) userCount  from i200.dbo.T_Order_CouponList   ");
            strSql.Append(" where toAccId in(select accid from #list) group by toAccId) d on a.ID=d.toAccId  ");
            strSql.Append(" left outer join(  ");
            strSql.Append(" select a.accid,a.insertName from Sys_VisitInfo a inner join(  ");
            strSql.Append(" select accid,MAX(insertTime) it from Sys_VisitInfo   where    ");
            strSql.Append(" accid in(select accid from #list)  group by accid ) b on a.insertTime=b.it and a.accid=b.accid) e on a.ID=e.accid  ");
            strSql.Append(" left outer join (  ");
            strSql.Append(" select accountid,PhoneNumber,UserEmail from i200.dbo.T_Account_User where grade='管理员') f on a.ID=f.accountid  left outer join Sys_agent_mess g on a.AgentId=g.ID   ");
            strSql.Append(" where a.ID in(  select accid from #list ) ; ");

            strSql.Append(" drop table #list  ");
            return DapperHelper.Query(strSql.ToString()).ToList();
        }

    }
}
