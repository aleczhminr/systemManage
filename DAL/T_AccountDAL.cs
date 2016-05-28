using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Model.Model4View;

namespace DAL
{
    public class T_AccountDAL : Base.T_AccountBaseDAL
    {

        /// <summary>
        /// 得到店铺的基本信息
        /// <para>
        /// 包含 店铺基本信息，店铺版本，最后登录时间等
        /// </para>
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public T_AccountBasic GetAccountBasic(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1 a.ID,CompanyName,UserRealName,shotName,RegTime,CompanyAddress,ServiceManager,LoginTimeWeb,LoginTimeLast,AgentId,aotjb,endtime,gsreguser,integral,active,smsSurplusNum,SmsBilling,PhoneNumber,UserEmail,AgentName,a.Remark from( ");
            strSql.Append(" select ID,CompanyName,UserRealName,shotName,RegTime,CompanyAddress,ServiceManager,LoginTimeWeb,LoginTimeLast,AgentId,Remark from i200.dbo.T_Account  where ID=@accid) a ");
            strSql.Append(" left outer join (select accountid,aotjb,endtime,gsreguser,integral,active,SmsBilling,dxunity smsSurplusNum from i200.dbo.T_Business where accountid=@accid) b on a.ID=b.accountid ");
            strSql.Append(" left outer join (select accountid,PhoneNumber,UserEmail from i200.dbo.T_Account_User where accountid=@accid and grade='管理员') c on a.ID=c.accountid " +
                          " left outer join (select AgentName,ID from Sys_agent_mess) d on d.ID=a.AgentId; ");
            return DapperHelper.GetModel<T_AccountBasic>(strSql.ToString(), new { accid = accid });
        }
        /// <summary>
        /// 得到店铺今日汇总信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public T_AccountSummarize.TodaySummarize GetAccountTodaySummarize(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare @saleNum int,@saleMoney decimal(18,2),@userNum int,@goodsNum int,@orderNum int,");
            strSql.Append("@orderMoney decimal(18,2),@smsNum int,@outlayNum int,@outlayMoney decimal(18,2),@couponNum int,@useCouponNum int;");
            strSql.Append(" select @saleNum=COUNT(saleID),@saleMoney=SUM(RealMoney) from i200.dbo.T_SaleInfo where DATEDIFF(day,insertTime,GETDATE())=0 and accID=@accid; ");
            strSql.Append(" select @userNum=COUNT(uid) from i200.dbo.T_UserInfo where  DATEDIFF(day,uInsertTime,GETDATE())=0 and accID=@accid; ");
            strSql.Append(" select @goodsNum=COUNT(gid) from i200.dbo.T_GoodsInfo where DATEDIFF(DAY,gInsertDate,GETDATE())=0 and accID=@accid; ");
            strSql.Append(" select @orderNum=COUNT(oid),@orderMoney=SUM(RealPayMoney) from i200.dbo.T_OrderInfo where  ");
            strSql.Append(" DATEDIFF(day,T_OrderInfo.transactionDate,GETDATE())=0 and accId=@accid and (orderStatus=2 or (OrderStatus=1 and OrderTypeId=2)); ");
            strSql.Append(" select @smsNum=COUNT(id) from i200.dbo.T_Sms_List where DATEDIFF(DAY,sendtime,GETDATE())=0 and accID=@accid and  smsStatus=1; ");
            strSql.Append("select @outlayNum=COUNT(ID),@outlayMoney=SUM(PaySum) from i200.dbo.t_PayRecord  where DATEDIFF(day,paydate,getdate())=0 and ShopperId=@accid;");
            strSql.Append(
                "select @couponNum=count(*) from i200.dbo.T_Order_CouponList where Datediff(day,createDate,getdate())=0 and toAccId=@accid;");
            strSql.Append(
                "select @useCouponNum=count(*) from i200.dbo.T_Order_CouponList where Datediff(day,createDate,getdate())=0 and useAccId=@accid;");
            strSql.Append(" select @saleNum saleNum,isnull(@saleMoney,0) saleMoney,@userNum userNum,@goodsNum goodsNum,@orderNum orderNum, ");
            strSql.Append("isnull(@orderMoney,0) orderMoney,@smsNum smsNum,@outlayNum outlayNum,isnull(@outlayMoney,0) outlayMoney,@couponNum couponNum,@useCouponNum useCouponNum;");
            


            return DapperHelper.GetModel<T_AccountSummarize.TodaySummarize>(strSql.ToString(), new { accid = accid });
        }

        /// <summary>
        /// 更新  RandomNumber 主要用户手机橱窗和 优惠券  
        /// <para>如果已经存在 就不在 更新</para>
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="randomNumber"></param>
        /// <returns></returns>
        public bool UpdateRandomNumber(int accid, string randomNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" 	declare @randomNumber varchar(100);");
            strSql.Append(" 	select @randomNumber=RandomNumber from T_Account where ID=@accid;");
            strSql.Append(" 	if(isnull(@randomNumber,'')='')");
            strSql.Append(" 	begin");
            strSql.Append(" 		update T_Account set RandomNumber=@rm where ID=@accid;");
            strSql.Append(" 	    select @rm;");
            strSql.Append(" 	end ");
            strSql.Append(" 	else ");
            strSql.Append(" 	begin");
            strSql.Append(" 	    select @randomNumber;");
            strSql.Append(" 	end ");
            object r = HelperForFrontend.ExecuteScalar(strSql.ToString(), new { rm = randomNumber, accid = accid });
            if (r != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 回访专用

        /// <summary>
        /// 得到数据列表  回访专用
        /// </summary>
        /// <param name="Column">列名</param>
        /// <param name="strWhere">条件集合</param>
        /// <returns></returns>
        public List<T_Account> GetVisitList(string Column, string strWhere)
        {
            Column = Column.Length > 0 ? Column : "*";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" " + Column + " ");
            strSql.Append(" from I200.dbo.T_Account ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(";");
            return HelperForFrontend.Query<T_Account>(strSql.ToString()).ToList();
        }

        #endregion

        /// <summary>
        /// 获取店铺查找的结果集
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWhere">条件</param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetSearchList(int pageIndex, int pageSize, List<DapperWhere> dapperWhere, string filedOrder, string searchStr = "")
        {

            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWhere)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }


            StringBuilder strSql = new StringBuilder();

            strSql.Append("create table #userinfor(" +
                          "ID int, UserRealName nvarchar(50), " +
                          "PhoneNumber nvarchar(20)," +
                          "regLength int," +
                          "useremail nvarchar(50)," +
                          "CompanyName nvarchar(100), " +
                          "LoginTimeLast datetime," +
                          "LoginTimeWeb int," +
                          "TureAddress nvarchar(200)," +
                          "Edit int,Orde int,OrdeMoney money," +
                          "Revisit int," +
                          "LoginLast int," +
                          "Club int," +
                          "Goods int," +
                          "sale int," +
                          "salemoney money," +
                          "pay int," +
                          "paymoney money," +
                          "sms int,havasms int,Remark varchar(200));  " +
                          " " +
                          "insert into #userinfor (ID,UserRealName,PhoneNumber,useremail,CompanyName,LoginLast,LoginTimeLast,LoginTimeWeb,regLength,Remark) ");

            strSql.Append(" SELECT ID,UserRealName,PhoneNumber,useremail,CompanyName,LoginLast,LoginTimeLast,LoginTimeWeb,regleng,accountRm FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.ID  desc");
            }
            strSql.Append(" )AS rowNumber,ID,UserRealName,PhoneNumber,useremail,CompanyName,datediff(dd,LoginTimeLast,getdate()) as LoginLast," +
                          "     LoginTimeLast,LoginTimeWeb," +
                          " datediff(day,regtime,getdate()) regleng,isnull(Remark,'') accountRm" +
                          " from i200.dbo.T_Account T where [State]=1 ");

            if (string.IsNullOrEmpty(searchStr))
            {
                if (where.Length > 0)
                {
                    strSql.Append(" and " + where);
                }
            }
            else
            {
                strSql.Append(" and (phoneNumber like '%" + searchStr + "%' or ");
                strSql.Append(" CompanyName like '%" + searchStr + "%' or ");
                strSql.Append(" UserRealName like '%" + searchStr + "%' or ");
                strSql.Append(" UserEmail like '%" + searchStr + "%' or ");
                strSql.Append(" BBS_Uid like '%" + searchStr + "%' ");
                if (Regex.IsMatch(searchStr, "^((\\+|-)\\d)?\\d*$"))
                {
                    strSql.Append(" or ID=" + searchStr);
                }
                strSql.Append(" )");
            }
            

            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.rowNumber between @startIndex and @endIndex; ");

            strSql.Append(
                "update #userinfor set TureAddress=TrueConty from i200.dbo.T_Account_User inner join #userinfor on #userinfor.ID=T_Account_User.accountid where grade='管理员'; " +
                "update #userinfor set Edit=aotjb,havasms=dxunity from i200.dbo.T_Business inner join #userinfor on accountid=#userinfor.ID;  " +
                "update #userinfor set Orde=orderList.num,OrdeMoney=sumMoney from #userinfor inner join (select accId,COUNT(oid) num,SUM(RealPayMoney) sumMoney  from i200.dbo.T_OrderInfo where accId in (select ID from #userinfor)  and orderStatus=2 and DATEDIFF(day,createDate,getdate())=0 group by accId) orderList on orderList.accId=#userinfor.ID; " +
                //"update #userinfor set Revisit=relist.num from #userinfor inner join (select accID,COUNT(id) as num from T_SysRevisit_Info where T_SysRevisit_Info.accID in (select ID from #userinfor) group by accID) relist on relist.accID=#userinfor.ID;  " +
                "update #userinfor set Club=userlist.num from #userinfor inner join (select accID,COUNT(uid) as num from i200.dbo.T_UserInfo where T_UserInfo.accID in (select ID from #userinfor) and DATEDIFF(day,uInsertTime,getdate())=0 group by accID) userlist on userlist.accID=#userinfor.ID;  " +
                "update #userinfor set Goods=goodslist.num from #userinfor inner join (select accID,COUNT(gid) as num from i200.dbo.T_GoodsInfo where T_GoodsInfo.accID in (select ID from #userinfor) and DATEDIFF(day,gAddTime,getdate())=0  group by accID) goodslist on goodslist.accID=#userinfor.ID;  " +
                "update #userinfor set sale=salelist.num,salemoney=rm from #userinfor inner join (select accID,COUNT(uid) as num,SUM(RealMoney) rm from i200.dbo.T_SaleInfo where T_SaleInfo.accID in (select ID from #userinfor)  and DATEDIFF(day,insertTime,getdate())=0 group by accID) salelist on salelist.accID=#userinfor.ID;  " +
                "update #userinfor set pay=paylist.num,paymoney=pMoney from #userinfor inner join (select ShopperId,COUNT(id) as num,SUM(paysum) pMoney from i200.dbo.t_PayRecord where t_PayRecord.ShopperId in (select ID from #userinfor)  and DATEDIFF(day,PayDate,getdate())=0 group by ShopperId) paylist on paylist.ShopperId=#userinfor.ID;  " +
                "update #userinfor set sms=smslist.num from #userinfor inner join (select accID,COUNT(id) as num from i200.dbo.T_Sms_List where T_Sms_List.accID in (select ID from #userinfor)  and DATEDIFF(day,sendtime,getdate())=0 group by accID) smslist on smslist.accID=#userinfor.ID; " +
                "select * from #userinfor; " +
                "drop table #userinfor;");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return HelperForFrontend.Query<dynamic>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 获取待审核店铺列表
        /// </summary>
        /// <returns></returns>
        public List<UncheckedShopModel> GetUncheckedShopList(DateTime regTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ID,CompanyName,UserRealName,RegTime,Reg_Code,Remark,PhoneNumber,useremail,0 regOk into #list from T_Account  ");
            strSql.Append(" where State=0 and DATEDIFF(day,RegTime,@regTime)=0 ");
            strSql.Append(" update #list set PhoneNumber=b.PhoneNumber,UserEmail=b.UserEmail from T_Account_User b where  ");
            strSql.Append(" #list.ID=b.accountid and grade='管理员' ");
            strSql.Append(" select id,CompanyName,PhoneNumber,useremail into #RegOk from T_Account where State=1 and  ");
            strSql.Append(" RegTime>CAST(@regTime as date) and DATEDIFF(HOUR,CAST(@regTime as date),RegTime)<28 ");
            strSql.Append(" update #RegOk set PhoneNumber=b.PhoneNumber,UserEmail=b.UserEmail from T_Account_User b where  ");
            strSql.Append(" #RegOk.ID=b.accountid and grade='管理员' ");
            strSql.Append(" update #list set regOk=1 where CompanyName in(select CompanyName from #RegOk) or  ");
            strSql.Append(" PhoneNumber in(select PhoneNumber from #RegOk) or UserEmail in(select UserEmail from #RegOk); ");
            strSql.Append(" select Id,CompanyName,PhoneNumber,UserEmail,RegTime,UserRealName,Reg_Code,regOk from #list order by RegTime desc; ");
            strSql.Append(" drop table #list; ");
            strSql.Append(" drop table #RegOk; ");

            return HelperForFrontend.Query<UncheckedShopModel>(strSql.ToString(), new { regTime=regTime }).ToList();
        }

        /// <summary>
        /// 审核店铺信息
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="type">0-邮件|1-手机</param>
        /// <returns></returns>
        public int CheckShop(int accid, int type)
        {
            int result = 0;

            var param = new DynamicParameters();
            param.Add("@Accountid", accid);
            param.Add("@Type", type);

            try
            {
                result = HelperForFrontend.ExecuteProc("dbo.sys_shopConfirm", param);
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// 审核店铺时删除店铺
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public int DeleteShopDuringChecking(int accid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "if(exists(select * from T_Account where ID=@accid and State=0 ))" +
                " begin " +
                "delete from T_Account where ID =@accid and State=0;" +
                "delete from T_Account_User where Accountid = @accid ;" +
                "delete from T_Business where accountid = @accid ;" +
                " end ");

            int result = 0;

            try
            {
                result = HelperForFrontend.Execute(strSql.ToString(), new { accid = accid });
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        ///// <summary>
        ///// 获取符合条件的指定列
        ///// </summary>
        ///// <param name="column"></param>
        ///// <param name="strWhere"></param>
        ///// <returns></returns>
        //public List<T_Account> GetListByColumn(string column, string strWhere)
        //{
        //    List<T_Account> accList = new List<T_Account>();
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("select " + column + " from T_Account where " + strWhere);

        //    try
        //    {
        //        accList = HelperForFrontend.Query<T_Account>(strSql.ToString()).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        accList = null;
        //    }

        //    return accList;
        //}


        /// <summary>
        /// 得到分店的信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns>{CompanyName,ID}</returns>
        public List<dynamic> GetSubbranchInfo(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CompanyName,ID from i200.dbo.T_Account where max_shop=@accid and ID!=@accid;");
            return DapperHelper.Query(strSql.ToString(), new { accid = accid }).ToList();
        }
        /// <summary>
        /// 得到总店信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns>{CompanyName,ID}</returns>
        public dynamic GetHeadquartersInfo(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CompanyName,ID from i200.dbo.T_Account where ID in(select max_shop from i200.dbo.T_Account where ID=@accid) and ID !=@accid;");
            return DapperHelper.GetModel<dynamic>(strSql.ToString(), new { accid = accid });
        }
        /// <summary>
        /// 获取符合条件的指定列
        /// </summary>
        /// <param name="column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<T_Account> GetListByColumn(string column, string strWhere)
        {
            List<T_Account> accList = new List<T_Account>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select " + column + " from T_Account ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where " + strWhere);
            }

            try
            {
                accList = HelperForFrontend.Query<T_Account>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                accList = null;
            }

            return accList;
        }
        /// <summary>
        /// 得到店铺名称
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public string GetCompanyName(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CompanyName from i200.dbo.T_Account where ID=@id");
            object r = DapperHelper.ExecuteScalar(strSql.ToString(), new { id = accid });
            if (r != null)
            {
                return r.ToString();
            }else{
                return "";
            }
        }
        /// <summary>
        /// 得到店铺名称
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetCompanyName(int[] accid)
        {
            Dictionary<int, string> rL = new Dictionary<int, string>();
            if (accid.Length > 0)
            {
                string listId = "";
                foreach (int id in accid)
                {
                    listId += "," + id.ToString();
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ID,CompanyName from i200.dbo.T_Account where ID in(" + listId.Trim(',') + ")");

                List<dynamic> obj = DapperHelper.Query<dynamic>(strSql.ToString()).ToList();
                if (obj != null)
                {
                    foreach (dynamic objItem in obj)
                    {
                        rL[Convert.ToInt32(objItem.ID)] = objItem.CompanyName;
                    }
                }
            }
            return rL;
        }

        /// <summary>
        /// 获取用户号码
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="accId"></param>
        /// <returns></returns>
        public string GetAccountNumber(int channelId, int accId)
        {
            StringBuilder strSql = new StringBuilder();            

            switch (channelId)
            {
                case 1://短信
                    strSql.Append("select isnull(PhoneNumber,'') from [i200].[dbo].[T_Account] ");
                    break;
                case 4://电子邮件
                    strSql.Append("select isnull(useremail,'') from [i200].[dbo].[T_Account] ");
                    break;
                default:
                    strSql.Append("select '' from [i200].[dbo].[T_Account] ");
                    break;
            }

            strSql.Append(" where ID=" + accId);

            return DapperHelper.ExecuteScalar<string>(strSql.ToString());

        }

        /// <summary>
        /// 注册来源分析
        /// </summary>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<SourceAnalyzeModel> RegSourceAnalyze(DateTime startTime, DateTime endTime, string[] sourceList,string detailMark = "")
        {
            StringBuilder strSql = new StringBuilder();
            string sourceStr = "";
            if (sourceList.Length > 0 && string.IsNullOrEmpty(detailMark))
            {
                foreach (int id in sourceList.Select(x => int.Parse(x)).ToList())
                {
                    sourceStr += ",'" + id.ToString()+"'";
                }

                if (sourceStr.Length > 0)
                {
                    sourceStr = sourceStr.Trim(',');
                }
                else
                {
                    sourceStr = "'0','8','11','13','10'";
                }

                strSql.Append(" create table #table (id int,regtime datetime,remark nvarchar(10)) ");
                strSql.Append(" insert into #table(id,regtime,remark) ");
                strSql.Append(" select ID,CAST(RegTime as date),remark from i200.dbo.T_Account where  ");
                strSql.Append(" RegTime>@statTime and RegTime<@endTime and State=1 and remark in ("+ sourceStr + "); ");               
                strSql.Append(" select regtime,remark,COUNT(id) countNum from #table group by regtime,remark ");
                strSql.Append(" drop table #table; ");

            }
            else if (sourceList.Length > 0 && !string.IsNullOrEmpty(detailMark))//细分来源
            {
                foreach (string id in sourceList)
                {
                    sourceStr += "," + id.ToString();
                }

                if (sourceStr.Length > 0)
                {
                    sourceStr = sourceStr.Trim(',');
                }
                else
                {
                    sourceStr = "'market_360','market_huawei','market_baidu','market_xiaomi'";
                }

                strSql.Append(" create table #table (id int,regtime datetime,fromName nvarchar(100)) ");
                strSql.Append(" insert into #table(id,regtime,fromName) ");
                strSql.Append(" select ID,CAST(RegTime as date),fromName from i200.dbo.T_Account where  ");
                strSql.Append(" RegTime>@statTime and RegTime<@endTime and State=1 and fromName in(" + sourceStr + "); ");

                strSql.Append(" select regtime,fromName,COUNT(id) countNum from #table group by regtime,fromName ");
                strSql.Append(" drop table #table; ");
            }                                    

            //List<dynamic> itemList = DapperHelper.Query(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            //Dictionary<string, SourceAnalyzeModel> sourceModleList = new Dictionary<string, SourceAnalyzeModel>();
            //SourceAnalyzeModel赋值
            List<dynamic> itemList = DapperHelper.Query(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            Dictionary<string, SourceAnalyzeModel> sourceModleList = new Dictionary<string, SourceAnalyzeModel>();
            foreach (dynamic item in itemList)
            {
                string timeString = Convert.ToDateTime(item.regtime).ToString("yyyy-MM-dd");
                if (!sourceModleList.ContainsKey(timeString))
                {
                    sourceModleList[timeString] = new SourceAnalyzeModel()
                    {
                        DateTime = Convert.ToDateTime(item.regtime)
                    };
                }
                

                SourceAnalyzeItemList sourceItemList = new SourceAnalyzeItemList();
                if (item.remark != null)
                {
                    int sourceid = Convert.ToInt32(item.remark);
                    sourceItemList.SourceId = sourceid;
                    sourceItemList.CountValue = Convert.ToDecimal(item.countNum);
                    sourceModleList[timeString].ItemList.Add(sourceid.ToString(), sourceItemList);
                }
                else
                {
                    string detailSource = item.fromName.ToString();
                    sourceItemList.DetailSource = detailSource;
                    sourceItemList.CountValue = Convert.ToDecimal(item.countNum);
                    sourceModleList[timeString].ItemList.Add(detailSource, sourceItemList);
                }
                
                
                sourceModleList[timeString].CountValue += sourceItemList.CountValue;
                sourceModleList[timeString].count++;
            }
            List<SourceAnalyzeModel> modelList = new List<SourceAnalyzeModel>();

            DateTime historyTime = startTime;

            while (historyTime.Date <= endTime.Date)
            {
                string timeString = historyTime.ToString("yyyy-MM-dd");
                if (sourceModleList.ContainsKey(timeString))
                {
                    SourceAnalyzeModel sm = sourceModleList[timeString];
                    foreach (KeyValuePair<string, SourceAnalyzeItemList> il in sm.ItemList)
                    {
                        il.Value.ValueScore = (il.Value.CountValue / sm.CountValue);
                        il.Value.weekend = ((int)sm.DateTime.DayOfWeek).ToString();
                    }
                    modelList.Add(sm);
                }
                else
                {
                    SourceAnalyzeModel sm = new SourceAnalyzeModel();
                    sm.DateTime = historyTime;
                    sm.CountValue = 0;
                    sm.count = 0;
                    sm.ItemList = new Dictionary<string, SourceAnalyzeItemList>();
                    if (string.IsNullOrEmpty(detailMark))
                    {
                        foreach (int s in sourceList.Select(x => int.Parse(x)).ToList())
                        {
                            sm.ItemList[s.ToString()] = new SourceAnalyzeItemList()
                            {
                                SourceId = s,
                                CountValue = 0,
                                ValueScore = 0
                            };
                        }
                    }
                    else
                    {
                        foreach (string s in sourceList)
                        {
                            sm.ItemList[s.ToString()] = new SourceAnalyzeItemList()
                            {
                                DetailSource = s,
                                CountValue = 0,
                                ValueScore = 0
                            };
                        } 
                    }
                    

                    modelList.Add(sm);
                }


                historyTime = historyTime.AddDays(1);
            }
            //foreach (dynamic item in itemList)
            //{
            //    string timeString = Convert.ToDateTime(item.regtime).ToString("yyyy-MM-dd");
            //    if (!sourceModleList.ContainsKey(timeString))
            //    {
            //        sourceModleList[timeString] = new SourceAnalyzeModel()
            //        {
            //            DateTime = Convert.ToDateTime(item.regtime)
            //        };
            //    }
            //    int sourceid = Convert.ToInt32(item.tag_id);

            //    SourceAnalyzeItemList sourceItemList = new SourceAnalyzeItemList();
            //    sourceItemList.SourceId = sourceid;
            //    sourceItemList.CountValue = Convert.ToDecimal(item.countNum);

            //    sourceModleList[timeString].ItemList.Add(sourceid.ToString(), sourceItemList);

            //    sourceModleList[timeString].CountValue += sourceItemList.CountValue;
            //    sourceModleList[timeString].count++;
            //}
            //List<SourceAnalyzeModel> modelList = new List<SourceAnalyzeModel>();

            //DateTime historyTime = startTime;

            //while (historyTime.Date <= endTime.Date)
            //{
            //    string timeString = historyTime.ToString("yyyy-MM-dd");
            //    if (sourceModleList.ContainsKey(timeString))
            //    {
            //        SourceAnalyzeModel sm = sourceModleList[timeString];
            //        foreach (KeyValuePair<string, SourceAnalyzeItemList> il in sm.ItemList)
            //        {
            //            il.Value.ValueScore = (il.Value.CountValue / sm.CountValue);
            //            il.Value.weekend = ((int)sm.DateTime.DayOfWeek).ToString();
            //        }
            //        modelList.Add(sm);
            //    }
            //    else
            //    {
            //        SourceAnalyzeModel sm = new SourceAnalyzeModel();
            //        sm.DateTime = historyTime;
            //        sm.CountValue = 0;
            //        sm.count = 0;
            //        sm.ItemList = new Dictionary<string, SourceAnalyzeItemList>();
            //        foreach (int s in sourceList)
            //        {
            //            sm.ItemList[s.ToString()] = new SourceAnalyzeItemList()
            //            {
            //                SourceId = s,
            //                CountValue = 0,
            //                ValueScore = 0
            //            };
            //        }

            //        modelList.Add(sm);
            //    }


            //    historyTime = historyTime.AddDays(1);
            //}

            //strSql.Clear();

            //strSql.Append("select cast(OperDate as date) dayDate,'tPC' terminal,count(distinct Accountid) logCount from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and OperDate>@statTime and OperDate<@endTime group by cast(OperDate as date) ");
            //strSql.Append(" union all ");
            //strSql.Append("select cast(OperDate as date) dayDate,'tWEB' terminal,count(distinct Accountid) logCount from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and OperDate>@statTime and OperDate<@endTime group by cast(OperDate as date) ");
            //strSql.Append(" union all ");
            //strSql.Append("select cast(createTime as date) dayDate,'tIOS' terminal,count(distinct AccId) logCount from i200.dbo.T_Token_Api where (AppKey like 'iP%') and createTime>@statTime and createTime<@endTime group by cast(createTime as date) ");
            //strSql.Append(" union all ");
            //strSql.Append("select cast(createTime as date) dayDate,'tAndroid' terminal,count(distinct AccId) logCount from i200.dbo.T_Token_Api where (AppKey like 'Android%') and createTime>@statTime and createTime<@endTime group by cast(createTime as date);");


            //List<LoginTrend> loginTypeList = DapperHelper.Query<LoginTrend>(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            //List<LoginTrend> tmpList = new List<LoginTrend>();
            //SourceAnalyzeItemList tmpItem = new SourceAnalyzeItemList();
            ////KeyValuePair<string, SourceAnalyzeItemList> tmpDic = new KeyValuePair<string, SourceAnalyzeItemList>();

            //foreach (var item in modelList)
            //{
            //    if (loginTypeList.Exists(x => x.dayDate == item.DateTime))
            //    {
            //        tmpList = loginTypeList.FindAll(x => x.dayDate == item.DateTime);
            //        foreach (var listItem in tmpList)
            //        {
            //            //tmpDic = item.ItemList.First();

            //            //tmpItem.weekend = tmpDic.Value.weekend;
            //            //tmpItem.CountValue=listItem.logCount;

            //            modelList.Find(x => x.DateTime == item.DateTime).ItemList.Add(listItem.terminal, new SourceAnalyzeItemList(listItem.logCount));
            //        }

            //    }
            //}
            //foreach (KeyValuePair<string, SourceAnalyzeModel> keyValue in sourceModleList)
            //{
            //}
            
            return modelList;
        }

        public int GetAccIdBybbsId(int bbsId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select ID from i200.dbo.T_Account where BBS_Uid=@bbsId;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { bbsId = bbsId });
        }

    }
}
